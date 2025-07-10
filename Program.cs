using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SPM_Worker
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0 && args[0] == "test")
            {
                Application.Run(new MAIN_FORM());
            }
            else
            {
                Application.Run(new Form1());
            }

        }
    }

    /// <summary>
    /// Клас який тримає в памяті всі можливі варіанти товарів/послуг
    /// </summary>

    public class SERVICE_ID_LIST
    {
        public List<SERVICE_ID_INFO> INFO { get; set; }

        public SERVICE_ID_LIST()
        {
            INFO = new List<SERVICE_ID_INFO>();
        }

        public int[] GetIDListByAtr(int _atr)
        {
            List<int> _out = new List<int>();

            List<SERVICE_ID_INFO> AllIds = INFO
                .FindAll(i => (i.ATR & _atr) == _atr);

            AllIds.ForEach(i => _out.Add(i.ID));

            return _out.Distinct().ToArray();
        }
    }

    /// <summary>
    /// Клас який тримає в памяті конкретний товар/послугу
    /// з підтипами та їх кольорами
    /// </summary>

    public class SERVICE_ID_INFO
    {
        public static implicit operator SERVICE_ID_INFO(KOMPLEKT _other)
        {
            return new SERVICE_ID_INFO { 
                ID = _other.service_ID, 
                TYPE_ID = _other.type_ID,
                COLOR_ID = _other.color,
                COST = _other.costs,
                COUNT = _other.count,
                ATR = Convert.ToInt16(SERVICE_INFO.GetAtr(_other.service_ID))
            };
        }

        public bool IS_POSLUGA { get { 
            if (ATR >= 0)
                {
                    //1 замовлення (абонент)
                    //2 замовлення (вручну)
                    //4 покупка (абонент)
                    //8 покупка (вручну)
                    //16 витрати

                    return (ATR & 8) != 8;
                }
                else
                {
                    return true;
                }
            } private set { } }

        public int ID { get; set; }
        public int TYPE_ID { get; set; }
        public int? COLOR_ID { get; set; }
        public string NAME { get {
                return SERVICE_INFO.GetServiceName(ID);
            } private set { } }
        public string TYPE_NAME { get {
                return SERVICE_INFO.GetTypeName(ID, TYPE_ID);
            } private set { } }
        public string COLOR_NAME { get {
                if (COLOR_ID == null) return null;
                return SERVICE_INFO.GetColorName((int)COLOR_ID);
            } private set { } }
        public float COST { get; set; }
        public int ATR { get; set; }
        public int COUNT { get => _count; set { _count = value; } }

        private int _count = 1;
    }

    /// <summary>
    /// Налаштування та інформація
    /// </summary>
    static class SERVICE_INFO
    {
        /// <summary>
        /// Повертає список всіх зв'язок ATR -> ID -> TYPE -> COLOR
        /// </summary>
        public static List<SERVICE_ID_INFO> SERVICE_LIST { 
            get { 
                if (SERVICES != null)
                {
                    return SERVICES.INFO;
                }
                else
                {
                    SetServiceInfo();
                    return SERVICES.INFO;
                }
            } private set { 

            } 
        }

        private static SERVICE_ID_LIST SERVICES;
        
        private static SERVICE_ALL_STRING_NAMES NAMES { get {
            if (_names != null)
                {
                    return _names;
                }
                else
                {
                    SetServiceAllNames();
                    return _names;
                }
            } set { _names = value; } }

        private static SERVICE_ALL_STRING_NAMES _names;

        public static string GetServiceName(int id)
        {
            return NAMES.SERVICE_NAMES.Find(n => n.ID == id).NAME;
        }

        public static string GetTypeName(int servId, int typeId)
        {
            if (servId == 19) return GetServiceName(typeId);

            ALL_TYPE_NAMES _out = NAMES.TYPE_NAMES.Find(t => t.SERV_ID == servId && t.TYPE_ID == typeId); ;

            if (_out != null)
            {
                return _out.NAME;
            }
            else
            {
                return null;
            }

        }

        public static int? GetAtr(int servID)
        {
            return SERVICE_LIST.Find(a => a.ID == servID).ATR;
        }

        public static string GetColorName(int id)
        {
            return NAMES.COLOR_NAMES.Find(c => (c.ID == id && c.ID.GetType() == id.GetType())).NAME;
        }
        public static int[] GetAllID(List<SERVICE_ID_INFO> _inputList)
        {
            List<SERVICE_ID_INFO> _temp;

            _temp = _inputList.GroupBy(x=>x.ID).Select(v=>v.First()).ToList();

            int[] _out = new int[_temp.Count];

            int counter = 0;

            foreach(SERVICE_ID_INFO _t in _temp)
            {
                _out[counter++] = _t.ID;
            }

            return _out;
        }
        public static void SetServiceInfo()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_get_serv_info";
            string postData = "";

            // Створення запиту
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Відправка POST-даних
            byte[] data = Encoding.UTF8.GetBytes(postData);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            // Отримання відповіді
            string responseText;
            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseText = reader.ReadToEnd();
            }

            // Парсинг JSON
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //SERVICES = new SERVICE_ID_LIST();

            SERVICES = serializer.Deserialize<SERVICE_ID_LIST>(responseText);
        }

        public static void SetServiceAllNames()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_get_names";
            string postData = "";

            // Створення запиту
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Відправка POST-даних
            byte[] data = Encoding.UTF8.GetBytes(postData);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            // Отримання відповіді
            string responseText;
            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseText = reader.ReadToEnd();
            }

            // Парсинг JSON
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //SERVICES = new SERVICE_ID_LIST();

            NAMES = serializer.Deserialize<SERVICE_ALL_STRING_NAMES>(responseText);
        }
    }

    class SERVICE_ALL_STRING_NAMES
    {
        public List<ALL_SERVICE_NAMES> SERVICE_NAMES { get; set; }
        public List<ALL_TYPE_NAMES> TYPE_NAMES {get; set;}
        public List<ALL_COLOR_NAMES> COLOR_NAMES { get; set; }
    }

    class ALL_COLOR_NAMES {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CSS { get; set; }
    }

    class ALL_SERVICE_NAMES
    {
        public int ID { get; set; }
        public string NAME { get; set; }
    }

    class ALL_TYPE_NAMES
    {
        public int SERV_ID
        {
            get; set;
        }
        public int TYPE_ID { get; set; }
        public string NAME
        {
            get; set;
        }
    }
}
