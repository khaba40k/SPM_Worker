using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using File = System.IO.File;

namespace API_NovaPoshta
{
    public class NovaPoshta : IDisposable
    {
        private string TOKEN { get; set; }
        private string CASH_PATH { get; set; }
        private Dictionary<int, NP_CITIES_TO_WH> CITIES_VIRTUAL = new Dictionary<int, NP_CITIES_TO_WH>();
        private const string FILENAME_CITIES_CASH = "CITIES.json";
        private const string FILENAME_AREAS_CASH = "AREAS.json";
        private const string API_URL = "https://api.novaposhta.ua/v2.0/json/";
        private const byte DAYS_FROM_CITIES_OVERRIDE = 15;
        private const byte DAYS_FROM_AREAS_OVERRIDE = 5;

        public bool LOCAL_CITIES_EXIST => Is_Exist(FILENAME_CITIES_CASH);
        public bool IS_ACTUAL_LOCAL_CITIES => Is_Actual(Path.Combine(CASH_PATH, FILENAME_CITIES_CASH),
            DAYS_FROM_CITIES_OVERRIDE);

        public bool IS_ACTUAL_LOCAL_AREAS_ALL => isActualAreasFiles();

        private WebClient WEB_CLIENT = new WebClient() { Encoding = Encoding.UTF8 };

        private JavaScriptSerializer SERIALIZER = new JavaScriptSerializer();
        public NovaPoshta(string token, string cashPath)
        {
            SERIALIZER.MaxJsonLength = int.MaxValue;

            TOKEN = token;
            CASH_PATH = Path.GetFullPath(cashPath);
            AREAS = GetAreasList();
        }

        public NP_Descr_Ref[] AREAS;

        public Action<int> ReportProgress;
        public event Action UpdateFinished;

        private NP_Descr_Ref[] GetAreasList()
        {
            if (!DeSerializeIfActual(FILENAME_AREAS_CASH, out NP_Descr_Ref[] _answer, 0))
            {
                var json = new
                {
                    modelName = "AddressGeneral",
                    calledMethod = "getSettlementAreas",
                    methodProperties = new { },
                    apiKey = TOKEN
                };

                string jsonRequest = JsonConvert.SerializeObject(json);

                WEB_CLIENT.Headers[HttpRequestHeader.ContentType] = "application/json";

                try
                {
                    string response = WEB_CLIENT.UploadString(API_URL, "POST", jsonRequest);

                    NP_AREAS_DATA _ans = SERIALIZER.Deserialize<NP_AREAS_DATA>(response);

                    _answer = _ans.data.ToArray();

                    SaveToJsonFile(FILENAME_AREAS_CASH, _answer);
                }
                catch { return new NP_Descr_Ref[0]; }

            }

            return _answer;
        }

        private bool isActualAreasFiles()
        {
            string _fileName;

            foreach (NP_Descr_Ref _area in AREAS)
            {
                _fileName = Path.Combine(CASH_PATH, _area.Ref + ".json");
                if (!Is_Actual(_fileName, DAYS_FROM_AREAS_OVERRIDE)) return false;
            }

            return true;
        }

        private void WriteAreaCash(string[] areaRefs, bool _overrideNoActual = true)
        {
            foreach (string area in areaRefs)
            {
                if (_overrideNoActual || !Is_Actual(Path.Combine(CASH_PATH, area + ".json"), DAYS_FROM_AREAS_OVERRIDE))
                {
                    WriteAreaCash(area);
                }
            }
        }

        private void WriteAreaCash(string areaRef)
        {
            NP_CITIES _ans = new NP_CITIES() { data = new List<NP_CityInfo>() };

            List<NP_CityInfo> _temp;

            for (int i = 1; ; i++)
            {
                _temp = GetAreasWeb(areaRef, i);

                if (_temp == null) break;

                _ans.data.AddRange(_temp);

                Thread.Sleep(30);
            }

            if (_ans.data.Count > 0) SaveToJsonFile(areaRef, _ans);
        }

        private List<NP_CityInfo> GetAreasWeb(string areaRef, int _page)
        {
            var json = new
            {
                modelName = "AddressGeneral",
                calledMethod = "getSettlements",
                methodProperties = new
                {
                    AreaRef = areaRef,
                    Warehouse = 1,
                    Limit = "500",
                    Page = _page.ToString()
                },
                apiKey = TOKEN
            };

            string jsonRequest = JsonConvert.SerializeObject(json);

            WEB_CLIENT.Headers[HttpRequestHeader.ContentType] = "application/json";
            try
            {
                string response = WEB_CLIENT.UploadString(API_URL, "POST", jsonRequest);
                NP_CITIES _ans = SERIALIZER.Deserialize<NP_CITIES>(response);

                return _ans.data.Count > 0 ? _ans.data : null;
            }
            catch { return null; }
        }

        public void ACTUALIZE()
        {
            string _fileName;

            int processed = 1;

            if (!IS_ACTUAL_LOCAL_AREAS_ALL)
                foreach (NP_Descr_Ref _area in AREAS)
                {
                    _fileName = Path.Combine(CASH_PATH, _area.Ref + ".json");
                    if (!Is_Actual(_fileName, DAYS_FROM_AREAS_OVERRIDE))
                    {
                        //Ітерація для progressBar
                        WriteAreaCash(_area.Ref);
                    }

                    if (ReportProgress != null)
                        ReportProgress(processed++);
                }

            if (!IS_ACTUAL_LOCAL_CITIES)
            {
                CITIES_VIRTUAL = new Dictionary<int, NP_CITIES_TO_WH>();

                for (int i = 1; ; i++)
                {
                    //Ітерація для progressBar
                    if (GetCitiesFromWeb(i) == null) break;

                    if (ReportProgress != null)
                        ReportProgress(processed++); // або інше значення
                }

                CreateLocalCities(true);
            }

            UpdateFinished?.Invoke();
        }

        public NP_Descr_Ref[] GetRegionList(string _area)
        {
            if (!DeSerializeIfActual(_area, out NP_CITIES _ans, DAYS_FROM_AREAS_OVERRIDE))
            {
                WriteAreaCash(_area);

                if (!DeSerializeIfActual(_area, out _ans, 0)) return new NP_Descr_Ref[0];
            }

            if (_ans != null && _ans.Regions == null) { _ans.SetRegions(); }

            return _ans != null ? _ans.Regions.ToArray() : new NP_Descr_Ref[0];
        }

        public NP_CityInfo[] GetCitiesList(string _areaRef, string _regionRef = null)
        {
            if (!DeSerializeIfActual(_areaRef, out NP_CITIES _ans, DAYS_FROM_AREAS_OVERRIDE))
            {
                WriteAreaCash(_areaRef);

                if (!DeSerializeIfActual(_areaRef, out _ans, 0)) return new NP_CityInfo[0];
            }

            if (_ans == null) return new NP_CityInfo[0];

            List<NP_CityInfo> _cityes = _ans.GetCityesByRegion(_regionRef);

            return _cityes.ToArray();
        }

        public List<NP_CityInfoToWrh> GetCitiesFromWeb(int page = 1)
        {
            WEB_CLIENT.Headers[HttpRequestHeader.ContentType] = "application/json";

            try
            {
                var json = new
                {
                    modelName = "AddressGeneral",
                    calledMethod = "getCities",
                    methodProperties = new
                    {
                        Limit = "500",
                        Page = page.ToString()
                    },
                    apiKey = TOKEN
                };

                string jsonRequest = JsonConvert.SerializeObject(json);

                string response = WEB_CLIENT.UploadString(API_URL, "POST", jsonRequest);
                NP_CITIES_TO_WH _temp = SERIALIZER.Deserialize<NP_CITIES_TO_WH>(response);

                if (_temp == null || _temp.data == null || _temp.data.Count == 0)
                    return null;
                else
                {
                    if (CITIES_VIRTUAL.ContainsKey(page)) CITIES_VIRTUAL.Remove(page);

                    CITIES_VIRTUAL.Add(page, _temp);

                    return _temp.data;
                }
            }
            catch { }

            return null;
        }
        /// <summary>
        /// Надає список відділень (пошук міст в локальному CITIES.json)
        /// </summary>
        /// <param name="CityDesc"></param>
        /// <param name="TypeRef"></param>
        /// <param name="AreaDesc"></param>
        /// <returns></returns>
        public List<WrhInfo> GetWarehouseList(
            string CityDesc,
            string TypeRef,
            string AreaDesc)
        {
            if (!DeSerializeIfActual(FILENAME_CITIES_CASH, out NP_CITIES _ans, DAYS_FROM_CITIES_OVERRIDE))
            {
                return null;
            }

            List<NP_CityInfoToWrh> _cities = _ans.data
                .Select(i => (NP_CityInfoToWrh)i)
                .Where(i => i.Description.Contains(CityDesc) &&
                            i.SettlementType == TypeRef &&
                            i.AreaDescription == AreaDesc)
                .ToList();

            if (_cities.Count != 1) return null;

            var json = new
            {
                modelName = "AddressGeneral",
                calledMethod = "getWarehouses",
                methodProperties = new
                {
                    CityRef = _cities[0].Ref
                },
                apiKey = TOKEN
            };

            string jsonRequest = JsonConvert.SerializeObject(json);

            WEB_CLIENT.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";

            try
            {
                string response = WEB_CLIENT.UploadString(API_URL, "POST", jsonRequest);
                WarehouseList _warehouses = SERIALIZER.Deserialize<WarehouseList>(response);

                _warehouses.data.Sort((w1, w2) => w1.intNUMBER.CompareTo(w2.intNUMBER));

                return _warehouses.data;
            }
            catch { return null; }
        }

        public List<WrhInfo> GetWarehouseList(string cityRef)
        {
            var json = new
            {
                modelName = "AddressGeneral",
                calledMethod = "getWarehouses",
                methodProperties = new
                {
                    CityRef = cityRef
                },
                apiKey = TOKEN
            };

            string jsonRequest = JsonConvert.SerializeObject(json);

            WEB_CLIENT.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";

            try
            {
                string response = WEB_CLIENT.UploadString(API_URL, "POST", jsonRequest);
                WarehouseList _warehouses = SERIALIZER.Deserialize<WarehouseList>(response);

                _warehouses.data.Sort((w1, w2) => w1.intNUMBER.CompareTo(w2.intNUMBER));

                return _warehouses.data;
            }
            catch (Exception ex) {
                string err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Після проходження всіх сторінок в getCities зберігає кеш в файл
        /// і обнуляє пам'ять. Виконується після серії GetCitiesFromWeb(page)
        /// </summary>
        /// <param name="if_actual">true - перезаписує навіть актуальне; false - зберігає якщо застаріли</param>
        public void CreateLocalCities(bool if_actual = false)
        {
            if (CITIES_VIRTUAL.Count > 0)
            {
                if (!if_actual && IS_ACTUAL_LOCAL_CITIES) return;

                List<NP_CITIES_TO_WH> _sorted = CITIES_VIRTUAL
                    .OrderBy(k => k.Key)
                    .Select(k => k.Value)
                    .ToList();

                NP_CITIES_TO_WH _out = new NP_CITIES_TO_WH();

                _out.data = new List<NP_CityInfoToWrh>();

                foreach (NP_CITIES_TO_WH _par in _sorted)
                {
                    _out.data.AddRange(_par.data);
                }

                SaveToJsonFile(FILENAME_CITIES_CASH, _out);

                CITIES_VIRTUAL = new Dictionary<int, NP_CITIES_TO_WH>();
            }
        }

        public enum FindIn : byte
        {
            LocalCitiFile,
            LocalCitiList,
            APICities,
            APISettlements
        }

        public List<NP_CityInfo> FindByCityName(string _cityName, FindIn findIn = FindIn.LocalCitiList)
        {
            List<NP_CityInfo> _finded = new List<NP_CityInfo>();

            NP_CITIES _searchIn;

            if (findIn == FindIn.LocalCitiFile) {
                if (File.Exists(Path.Combine(CASH_PATH, FILENAME_CITIES_CASH)))
                {
                    if (DeSerializeIfActual(FILENAME_CITIES_CASH, out _searchIn, 0))
                    {
                        _finded.AddRange(_searchIn.FindByString(_cityName));
                    }
                }
                else
                {
                    return _finded;
                }
            }
            else
            {
                string[] _files = Directory.GetFiles(CASH_PATH, "*.json");


                foreach (string _file in _files)
                {
                    if (Path.GetFileName(_file).Contains("-"))
                    {
                        if (DeSerializeIfActual(Path.GetFileName(_file), out _searchIn, 0))
                        {
                            _finded.AddRange(_searchIn.FindByString(_cityName));
                        }
                    }
                }
            }

            string search = _cityName.ToLower();
            //Відокремлення повних збігів

            List<NP_CityInfo> _temp = _finded
                .Where(c => c.Description.ToLower() == search).ToList();

            if (_temp.Count > 0)
            {
                return _temp
                    .OrderBy(c => c.AreaDescription, StringComparer.OrdinalIgnoreCase)
                    .ThenBy(c => c.RegionsDescription, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            else
            {
                // Сортування за власними правилами
                return _finded
                    .OrderBy(c =>
                    {
                        string desc = c.Description.ToLower();

                        if (desc == search)
                            return 0;  // Повне співпадіння → найвищий пріоритет
                        else if (desc.Contains(search))
                            return 1;  // Часткове співпадіння → середній пріоритет
                        else
                            return 2;  // Відсутність → найнижчий пріоритет
                    })
                    // Додатково сортуємо алфавітно всередині груп
                    .ThenBy(c => c.Description, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
        }

        private void SaveToJsonFile(string _fileName, object input)
        {
            _fileName = Path.HasExtension(_fileName) ? _fileName.Trim() : _fileName.Trim() + ".json";

            _fileName = Path.Combine(CASH_PATH, _fileName);

            string directory = Path.GetDirectoryName(_fileName);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonConvert.SerializeObject(input, Formatting.Indented);
            File.WriteAllText(_fileName, json);
        }

        private bool DeSerializeIfActual<T>(string _fileName, out T _answer, byte _daysToUpdate = 1)
        {
            _answer = default(T);

            _fileName = Path.HasExtension(_fileName) ? _fileName.Trim() : _fileName.Trim() + ".json";
            _fileName = Path.Combine(CASH_PATH, _fileName);

            if (!File.Exists(_fileName)) return false;

            try
            {
                string json = File.ReadAllText(_fileName);
                _answer = JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return false;
            }

            return _daysToUpdate > 0 ? Is_Actual(_fileName, _daysToUpdate) : true;
        }

        private bool Is_Exist(string fileName) => File.Exists(Path.Combine(CASH_PATH, fileName));

        private bool Is_Actual(string fileFullPath, byte days)
        {
            if (!File.Exists(fileFullPath)) return false;

            DateTime lastWrite = File.GetLastWriteTime(fileFullPath);

            return (DateTime.Now - lastWrite).TotalHours <= (24 * days);
        }

        public CreateContactJsonAnswer CreateRecepient(
            ref string phone,
            string LastName,
            string FirstName,
            string MiddleName = "")
        {
            if (string.IsNullOrWhiteSpace(FirstName)
                || string.IsNullOrWhiteSpace(LastName)) return null;

            StringBuilder _phone = new StringBuilder();

            foreach (char d in phone)
            {
                if (char.IsDigit(d)) _phone.Append(d);
            }

            if (_phone.Length < 10 || _phone.Length > 12) return null;

            phone = _phone.ToString().PadLeft(12, 'X');
            phone = phone.Replace("XX", "38");
            phone = phone.Replace("X", "3");

            WEB_CLIENT.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";

            try
            {
                var request = new
                {
                    apiKey = TOKEN,
                    modelName = "Counterparty",
                    calledMethod = "save",
                    methodProperties = new
                    {
                        CounterpartyProperty = "Recipient",
                        CounterpartyType = "PrivatePerson",
                        FirstName = FirstName.Trim(),
                        MiddleName = MiddleName.Trim(),
                        LastName = LastName.Trim(),
                        Phone = phone
                    }
                };

                string jsonRequest = JsonConvert.SerializeObject(request);

                string response = WEB_CLIENT.UploadString(API_URL, jsonRequest);

                return SERIALIZER.Deserialize<CreateContactJsonAnswer>(response);
            }
            catch
            {
                return null;
            }
        }

        public CreateDocumentJsonAnswer CreateDocument(CreateDocumentParams _input)
        {
            _input.TOKEN = TOKEN;

            try
            {
                string response = WEB_CLIENT.UploadString(API_URL, _input.ToString());

                return SERIALIZER.Deserialize<CreateDocumentJsonAnswer>(response);
            }
            catch (Exception ex)
            {
                string err = ex.Message;

                return null;
            }
        }

        public List<DocumentInfo> GetDocumentList(DateTime _before, bool andArchive = false)
        {
            var request = new
            {
                apiKey = TOKEN,
                modelName = "InternetDocument",
                calledMethod = "getDocumentList",
                methodProperties = new
                {
                    DateTimeFrom = _before.AddDays(-90).ToShortDateString(),
                    DateTimeTo = _before.ToShortDateString(),
                    Page = "1",
                    GetFullList = andArchive ? 1 : 0
                }
            };

            string stringRequest = JsonConvert.SerializeObject(request);

            string response = WEB_CLIENT.UploadString(API_URL, stringRequest);

            var jsonAns = new
            {
                success = false,
                data = new List<DocumentInfo>()
            };

            jsonAns = JsonConvert.DeserializeAnonymousType(response, jsonAns);

            return jsonAns.success ? jsonAns.data:null;
        }

        public bool RemoveDocument(string intNumber)
        {
            List<DocumentInfo> _docs = GetDocumentList(DateTime.Now);

            if (_docs == null || _docs.Count == 0)
            {
                return false;
            }

            DocumentInfo _doc = _docs.Find(d=>d.IntDocNumber == intNumber);

            if (_doc == null) return false;

            var request = new
            {
                apiKey = TOKEN,
                modelName = "InternetDocumentGeneral",
                calledMethod = "delete",
                methodProperties = new
                {
                    DocumentRefs = _doc.Ref
                }
            };

            string stringRequest = JsonConvert.SerializeObject(request);

            string response = WEB_CLIENT.UploadString(API_URL, stringRequest);

            var jsonAns = new
            {
                success = false
            };

            jsonAns = JsonConvert.DeserializeAnonymousType(response, jsonAns);

            return jsonAns.success;
        }


        public void Dispose() => WEB_CLIENT?.Dispose();
    }

    public class AddressParts
    {
        public string CityType;
        public string CityName;
        public string Rajon;
        public string Oblast;
        public string Number;

        public static AddressParts Parse(string input)
        {
            var result = new AddressParts();

            string working = input.Trim();
            List<int> usedIndices = new List<int>();

            // Обробка дужок: видалити або розкрити вміст, якщо є патерни
            while (true)
            {
                int open = working.IndexOf('(');
                if (open == -1) break;
                int close = working.IndexOf(')', open + 1);
                if (close == -1) break;

                string content = working.Substring(open + 1, close - open - 1);
                string lower = content.ToLowerInvariant();

                if (Regex.IsMatch(lower, @"\b(обл\.?|область|області|район|рай\.?|р-н|номер|#|№)\b"))
                    working = working.Substring(0, open) + content + working.Substring(close + 1);
                else
                    working = working.Substring(0, open) + working.Substring(close + 1);
            }

            string lowered = working.ToLowerInvariant();

            // ====== Number ======
            Match numberMatch = Regex.Match(input, @"(?:№|#)\s*(\d+)(?!\d)");

            if (numberMatch.Success)
            {
                result.Number = numberMatch.Groups[1].Value;
                usedIndices.Add(numberMatch.Index);
            }
            else
            {
                // 2. Якщо номера з маркером нема, шукаємо останнє "чисте" число
                MatchCollection allNumbers = Regex.Matches(input, @"\b\d+\b");
                for (int i = allNumbers.Count - 1; i >= 0; i--)
                {
                    var n = allNumbers[i];
                    // Перевірка чи не входить у вже виявлені індекси (область, район)
                    bool overlaps = false;
                    foreach (var idx in usedIndices)
                    {
                        if (n.Index >= idx && n.Index < idx + 10) // 10 — максимальна довжина для слова/числа, можна збільшити
                        {
                            overlaps = true;
                            break;
                        }
                    }
                    if (!overlaps)
                    {
                        result.Number = n.Value;
                        usedIndices.Add(n.Index);
                        break;
                    }
                }
            }


            // ====== Oblast ======
            Match om = Regex.Match(working, @"(?<=^|\s)([^\s]+?)(?=\s)?\s*(обл\.|обл|область|області)\b", RegexOptions.IgnoreCase);
            if (om.Success)
            {
                result.Oblast = om.Groups[1].Value;
                usedIndices.Add(om.Index);
            }

            // ====== Rajon ======
            Match rm = Regex.Match(working, @"(?<=^|\s)([^\s]+?)(?=\s)?\s*(район|р-н|р-н\.|району|рай\.|рйн\.)\b", RegexOptions.IgnoreCase);
            if (rm.Success)
            {
                result.Rajon = rm.Groups[1].Value;
                usedIndices.Add(rm.Index);
            }

            // ====== CityType & CityName ======
            string[] typePatterns = new string[] {
            "с.м.т.", "смт", "селище міського типу", "селище", "місто", "м.", "м", "село", "с.", "с"
        };

            int firstCut = working.Length;
            foreach (int idx in usedIndices)
                if (idx < firstCut) firstCut = idx;

            string[] _redSymbols = new[] { " ", "." };

            foreach (string pattern in typePatterns)
            {
                int idx = lowered.IndexOf(pattern);
                if (idx >= 0 && idx < firstCut
                    && _redSymbols.Contains(lowered.Substring(idx + pattern.Length, 1)))
                {
                    result.CityType = NormalizeType(pattern);
                    int from = idx + pattern.Length;
                    if (from < firstCut)
                        result.CityName = working.Substring(from, firstCut - from).Trim();
                    else
                        result.CityName = working.Substring(from).Trim();
                    break;
                }
            }

            // Якщо тип не знайдено — спроба визначити CityName до першого знайденого індексу
            if (string.IsNullOrEmpty(result.CityName))
            {
                result.CityName = working.Substring(0, firstCut).Trim();
            }

            return result;
        }

        private static string NormalizeType(string raw)
        {
            raw = raw.ToLowerInvariant();
            if (raw == "м" || raw == "місто") return "м.";
            if (raw == "с" || raw == "село") return "с.";
            if (raw == "смт" || raw == "с.м.т." || raw == "селище міського типу") return "смт";
            if (raw == "селище") return "селище";
            if (raw == "м.") return "м.";
            if (raw == "с.") return "с.";
            return raw;
        }

        public override string ToString()
        {
            string type = string.IsNullOrEmpty(CityType) ? "" : NormalizeType(CityType) + " ";
            string name = string.IsNullOrEmpty(CityName) ? "" : CityName;

            string geo = "";
            if (!string.IsNullOrEmpty(Oblast)) geo += Oblast + " обл.";
            if (!string.IsNullOrEmpty(Rajon))
            {
                if (!string.IsNullOrEmpty(geo)) geo += ", ";
                geo += Rajon + " р-н";
            }

            string geoStr = string.IsNullOrEmpty(geo) ? "" : " (" + geo + ")";
            string numStr = string.IsNullOrEmpty(Number) ? "" : ", № " + Number;

            return type + name + geoStr + numStr;
        }
    }

    public class NP_AREAS_DATA
    {
        public List<NP_Descr_Ref> data { get; set; }
    }

    public class NP_CITIES_TO_WH
    {
        public static implicit operator NP_CITIES_TO_WH(NP_CITIES _other)
        {
            if (_other == null || _other.data == null) return null;

            return new NP_CITIES_TO_WH
            {
                data = _other.data.Select(s => (NP_CityInfoToWrh)s).ToList()
            };
        }
        public List<NP_CityInfoToWrh> data { get; set; }
    }

    public class NP_CITIES
    {
        public static implicit operator NP_CITIES(NP_CITIES_TO_WH _other)
        {
            if (_other == null || _other.data == null) return null;

            return new NP_CITIES
            {
                data = _other.data.Select(NP_CityInfo.FromBase).ToList()
            };
        }

        public List<NP_CityInfo> data { get; set; }

        public List<NP_Descr_Ref> Regions
        {
            get;
            private set;
        }

        public void SetRegions()
        {
            List<NP_Descr_Ref> _out = new List<NP_Descr_Ref>();

            data.GroupBy(r => r.Region)
                .Select(s => s.First())
                .ToList()
                .ForEach(x => _out
                .Add(new NP_Descr_Ref(x.Region, x.RegionsDescription)));

            Regions = _out;
        }

        public List<NP_CityInfo> GetCityesByRegion(string _regionRef)
        {
            List<NP_CityInfo> _out = new List<NP_CityInfo>();

            return data.FindAll(d => d.Region == _regionRef);
        }

        public List<NP_CityInfo> FindByString(string _findString)
        {
            string _findStr = _findString.ToLower().Trim();

            List<NP_CityInfo> _out = new List<NP_CityInfo>();

            _out.AddRange(FindByCityName(_findStr, true));

            if (_out.Count == 0)
                _out.AddRange(FindByCityName(_findStr));

            if (_out.Count == 0)
                _out.AddRange(FindByNoCityName(_findStr));

            return _out;
        }
        /// <summary>
        /// Шукає за назвою населеного пункту
        /// </summary>
        /// <param name="_cityName"></param>
        /// <param name="fullApply">
        /// true: Тільки повні співпадіння; 
        /// false: тільки часткові співпадіння</param>
        /// <returns></returns>
        public List<NP_CityInfo> FindByCityName(string _cityName, bool fullApply = false)
        {
            List<NP_CityInfo> _out = new List<NP_CityInfo>();

            bool _apply;

            foreach (NP_CityInfo _one in data)
            {
                _apply = (fullApply ? _one.Description.ToLower().Trim() == _cityName :
                    (_one.Description.ToLower().Trim() != _cityName
                    && _one.Description.ToLower().Contains(_cityName)));

                if (_apply) _out.Add(_one);
            }

            return _out;
        }

        public List<NP_CityInfo> FindByNoCityName(string _findString)
        {
            List<NP_CityInfo> _out = new List<NP_CityInfo>();

            foreach (NP_CityInfo _one in data)
            {
                if (!_one.Description.ToLower().Contains(_findString)
                    && (_one.AreaDescription.ToLower().Contains(_findString)))
                {
                    _out.Add(_one);
                }
            }

            return _out;
        }
    }

    public class NP_CityInfoToWrh
    {
        public string AreaDescription { get; set; }
        public string Ref { get; set; }
        public string SettlementType { get; set; }
        public string SettlementTypeDescription { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return Description;
        }
    }

    public class NP_CityInfo : NP_CityInfoToWrh
    {
        public static NP_CityInfo FromBase(NP_CityInfoToWrh baseCity)
        {
            return new NP_CityInfo
            {
                Ref = baseCity.Ref,
                SettlementType = baseCity.SettlementType,
                Description = baseCity.Description,
            };
        }
        public string Region { get; set; }
        public string RegionsDescription { get; set; }
        public override string ToString()
        {
            return (ConvertCityType() + " " + RemoweScob(base.ToString())).TrimEnd();
        }

        private string ConvertCityType()
        {
            string _out = "";

            string[] _spl = SettlementTypeDescription?.Split(' ');

            if (_spl != null)
            {
                foreach (string s in _spl)
                {
                    _out += s.Substring(0, 1) + ".";
                }
            }

            return _out.Trim();
        }

        private string RemoweScob(string _origin)
        {
            int _start = _origin.IndexOf("(");

            if (_start > -1)
            {
                return _origin.Substring(0, _start);
            }
            else
            {
                return _origin;
            }
        }
    }

    public class FindedCiti : NP_CityInfo
    {
        public static implicit operator FindedCiti(AddressParts _other)
        {
            return new FindedCiti
            {
                Description = _other.CityName,
                AreaDescription = _other.Oblast,
                RegionsDescription = _other.Rajon,
                SettlementTypeDescription = _other.CityType
            };
        }
        public static FindedCiti FromBase(NP_CityInfo city)
        {
            return new FindedCiti
            {
                Ref = city.Ref,
                SettlementType = city.SettlementType,
                Description = city.Description,
                SettlementTypeDescription = city.SettlementTypeDescription,
                Region = city.Region,
                RegionsDescription = city.RegionsDescription,
                AreaDescription = city.AreaDescription
            };
        }

        public override string ToString()
        {
            return base.ToString() + " | " + AreaDescription + " обл."
                + (!string.IsNullOrEmpty(RegionsDescription) ? " | " + RegionsDescription + " р-н" : "");
        }
    }

    public class WarehouseList
    {
        public List<WrhInfo> data { get; set; }
    }

    public class WrhInfo
    {
        public string Ref { get; set; }
        public string Description { get; set; }
        public string NUMBER { get; set; }
        public int intNUMBER => int.Parse(NUMBER);
        public override string ToString() {
            string _out = Description.Replace("\"Нова Пошта\"", "НП");
            _out = _out.Replace($" №{NUMBER}:", ":");
            return NUMBER + ") " + _out;
        }
    }

    public class NP_Descr_Ref
    {
        public string Description { get; set; }
        public string Ref { get; set; }

        public NP_Descr_Ref()
        {
            Description = null;
            Ref = null;
        }

        public NP_Descr_Ref(string _ref, string _desc)
        {
            Description = _desc;
            Ref = _ref;
        }

        public override string ToString()
        {
            return Description;
        }

    }

    public class CreateDocumentParams
    {
        public string TOKEN { get; set; }
        public CreateDocumentParams(CreateContactJsonAnswer _recepientInfo, string Phone)
        {
            RecipientsPhone = Phone;
            Recipient = _recepientInfo.Recipient;
            ContactRecipient = _recepientInfo.ContactRecipient;
        }

        public string PayerType { get; set; } = "Recipient";
        public string PaymentMethod { get; set; } = "Cash";
        public string CargoType { get; set; } = "Parcel";
        public byte SeatsAmount => (byte)OptionsSeat.Count;
        public List<OptionsSeat> OptionsSeat { get; set; } = new List<OptionsSeat>();
        public float Weight { get; set; }
        public float TotalSeatsWeigth => OptionsSeat.Sum(op => op.Weight);
        public string ServiceType { get; set; } = "WarehouseWarehouse";
        public string Description { get; set; } = "Шолом";
        public float Cost { get; set; }
        public float AfterpaymentOnGoodsCost { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string CitySender { get; set; } = "db5c88dc-391c-11dd-90d9-001a92567626";
        public string Sender { get; set; } = "491b82bf-8982-11f0-a1d5-48df37b921da";
        public string ContactSender { get; set; } = "b8a4a84d-8983-11f0-a1d5-48df37b921da";
        public string SenderAddress { get; set; } = "8b06dbb3-0ac5-11e5-8a92-005056887b8d";
        public string SendersPhone { get; set; } = "380953410218";

        public string CityRecipient { get; set; }
        public string RecipientAddress { get; set; }
        private string RecipientsPhone { get; set; }
        private string Recipient { get; set; }
        private string ContactRecipient { get; set; }

        private const string MODELNAME = "InternetDocument";
        private const string CALLEDMETHOD = "save";

        public override string ToString()
        {
            var json = new
            {
                apiKey = TOKEN,
                modelName = MODELNAME,
                calledMethod = CALLEDMETHOD,
                methodProperties = new
                {
                    PayerType = PayerType,
                    PaymentMethod = PaymentMethod,
                    CargoType = CargoType,
                    SeatsAmount = SeatsAmount,
                    Weight = Weight.ToString(),
                    ServiceType = ServiceType,
                    Description = Description,
                    Cost = Cost.ToString(),
                    AfterpaymentOnGoodsCost = AfterpaymentOnGoodsCost.ToString(),
                    DateTime = DateTime.ToShortDateString(),
                    CitySender = CitySender,
                    Sender = Sender,
                    ContactSender = ContactSender,
                    SenderAddress = SenderAddress,
                    SendersPhone = SendersPhone,
                    CityRecipient = CityRecipient,
                    RecipientAddress = RecipientAddress,
                    RecipientsPhone = RecipientsPhone,
                    Recipient = Recipient,
                    ContactRecipient = ContactRecipient,
                    OptionsSeat = OptionsSeat.ToArray()
                }
            };

            return JsonConvert.SerializeObject(json, Formatting.Indented);
        }
    }

    public class OptionsSeat
    {
        private string LABEL = "";
        public OptionsSeat()
        {

        }

        public OptionsSeat(string label, byte L, byte W, byte H)
        {
            LABEL = label ?? "";
            volumetricLength = L;
            volumetricWidth = W;
            volumetricHeight = H;
        }

        public byte volumetricLength { get; set; }
        public byte volumetricWidth { get; set; }
        public byte volumetricHeight { get; set; }
        public float Weight => (volumetricLength * volumetricWidth * volumetricHeight) / 4000f;
        public string weight { get; set; }
        public override string ToString()
        {
            return (LABEL + "  " + string.Join("*", volumetricLength, volumetricWidth, volumetricHeight)).TrimStart();
        }
    }
    /// <summary>
    /// Counterparty=>save (Інформація по створеному отримувачу для ТТН)
    /// </summary>
    public class CreateContactJsonAnswer
    {
        public bool success { get; set; }
        public List<ClientCounterparty> data { get; set; }
        public string Recipient => data?.First()?.Ref;
        public string ContactRecipient => data?.First()?.ContactPerson?.data?.First()?.Ref;
    }
    /// <summary>
    /// Інформація по створеному отримувачу
    /// </summary>
    public class ClientCounterparty
    {
        public string Ref { get; set; }
        public ContactPersonJsonAnswer ContactPerson { get; set; }
    }
    /// <summary>
    /// Інформація по контакту створеного контрагента (отримувача)
    /// </summary>
    public class ContactPersonJsonAnswer
    {
        public bool success { get; set; }
        public List<ContactPerson> data { get; set; }
    }
    /// <summary>
    /// Відповідь на CounterpartyGeneral=>getCounterparties
    /// </summary>
    public class ContactPersonCounterpartyJsonAnswer
    {
        public bool success { get; set; }
        public List<RefInfo> data { get; set; }
    }

    public class RefInfo
    {
        public string Ref { get; set; }
    }

    public class ContactPerson
    {
        public string Ref { get; set; }
        public string Description { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
    }

    public class CreateDocumentJsonAnswer
    {
        public bool success { get; set; }
        public List<CreatedDocumentInfo> data { get; set; }
    }
    /// <summary>
    /// Інформація по створеній ТТН
    /// </summary>
    public class CreatedDocumentInfo
    {
        public string Ref { get; set; }
        public float CostOnSite { get; set; }
        public string EstimatedDeliveryDate { get; set; }
        public string IntDocNumber { get; set; }
        public string TypeDocument { get; set; }
    }

    public class RecepientAdress
    {
        public string CityRef { get; set; }
        public string WarehouseRef { get; set; }
    }

    public class DocumentInfo
    {
        public string Ref { get; set; }
        public string IntDocNumber { get; set; }
        public byte SeatsAmount { get; set; }
        public string Weight { get; set; }
        public DateTime DateTime { get; set; }
        public string Cost {  get; set; }
        public string Description { get; set; }
        public string RecipientsPhone { get; set; }
        public string StateName { get; set; }
        public string RecipientContactPerson { get; set; }
        public string CityRecipientDescription { get; set; }
        public string RecipientAddressDescription { get; set; }
    }
}
