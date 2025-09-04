using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SPM_Core
{
    public class JSON_ZAKAZ_LIST : IEquatable<JSON_ZAKAZ_LIST>
    {
        public Z_STATUS STATUS { get; set; } = Z_STATUS.NULL;
        public bool NeedUpdate { get; set; } = true;
        public DateTime LAST_CHANGE { get; set; }
        public List<ZAKAZ> Items { get; set; } = new List<ZAKAZ>();
        public int Count_NEW { get; set; } = 0;
        public int Count_ACTIVE { get; set; } = 0;
        public int Count_ARCHIVE { get; set; } = 0;
        public int CurrentCount => Items?.Count ?? 0;
        public void Add(ZAKAZ _input) => Items.Add(_input);

        public bool Equals(JSON_ZAKAZ_LIST other)
        {
            if (other == null) return false;

            if (Count_NEW != other.Count_NEW ||
                Count_ACTIVE != other.Count_ACTIVE ||
                Count_ARCHIVE != other.Count_ARCHIVE)
                return false;

            if (Items == null && other.Items == null)
                return true;
            if (Items == null || other.Items == null)
                return false;
            if (Items.Count != other.Items.Count)
                return false;

            var setA = new HashSet<ZAKAZ>(Items);
            var setB = new HashSet<ZAKAZ>(other.Items);

            return setA.SetEquals(setB);
        }

        public override bool Equals(object obj) => Equals(obj as JSON_ZAKAZ_LIST);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Count_NEW + 1;
                hash = (hash * 397) ^ Count_ACTIVE;
                hash = (hash * 397) ^ Count_ARCHIVE;

                if (Items != null)
                {
                    int itemsHash = 0;
                    foreach (ZAKAZ item in Items)
                    {
                        itemsHash ^= item.GetHashCode();
                    }
                    hash ^= itemsHash;
                }

                return hash;
            }
        }

        public static bool operator ==(JSON_ZAKAZ_LIST left, JSON_ZAKAZ_LIST right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left is null || right is null)
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(JSON_ZAKAZ_LIST left, JSON_ZAKAZ_LIST right)
        {
            return !(left == right);
        }
    }

    public class ZAKAZ : IEquatable<ZAKAZ>
    {
        public int ID { get; set; }
        public Z_TYPE TYPE { get; set; }
        public int? NUMBER { get; set; }
        public DateTime DATE_IN { get; set; }
        public DateTime DATE_MAX { get; set; }
        public DateTime? DATE_OUT
        {
            get
            {
                return !string.IsNullOrWhiteSpace(TTN_OUT) ? _date_out : null;
            }
            set { _date_out = value; }
        }

        private DateTime? _date_out = null;
        public string PHONE { get; set; }
        public string CLIENT_NAME { get; set; }
        public string REQV { get; set; }
        public string TTN_IN { get; set; } = null;
        public string TTN_OUT { get; set; } = null;
        public string COMM { get; set; } = null;
        public byte? DISCOUNT { get; set; } = null;
        public string DISCOUNT_CODE { get; set; } = null;
        public byte CALLBACK { get; set; }
        public string WORKER { get; set; } = null;
        public string REDAKTOR { get; set; }
        public bool TERMINOVO { get; set; }
        public Z_STATUS STATUS { get; set; }
        public List<KOMPLEKT> KOMPLEKT { get; set; } = new List<KOMPLEKT>();

        public float SUM
        {
            get
            {
                return KOMPLEKT?.Sum(k => k.costs) ?? 0f;
            }
            set
            {
                float _new = value;//470

                if (SUM != _new)
                {
                    float _koef = _new / SUM;

                    foreach (KOMPLEKT _k in KOMPLEKT)
                    {
                        _k.costs *= _koef;
                    }

                    if (_new != SUM && KOMPLEKT.Count > 0)
                    {
                        float _razn = _new - SUM;

                        KOMPLEKT[0].costs += _razn;
                    }
                }
            }
        }

        public ZAKAZ()
        {
            ID = -1;
            STATUS = Z_STATUS.NULL;
            TYPE = Z_TYPE.NULL;
            DATE_IN = DateTime.Now;
            DATE_MAX = DateTime.Now.AddDays(3);
        }

        public ZAKAZ(ZAKAZ _other)
        {
            ID = _other.ID;
            TYPE = _other.TYPE;
            NUMBER = _other.NUMBER;
            DATE_IN = _other.DATE_IN;
            DATE_MAX = _other.DATE_MAX;
            PHONE = _other.PHONE;
            CLIENT_NAME = _other.CLIENT_NAME;
            REQV = _other.REQV;
            COMM = _other.COMM;
            WORKER = _other.WORKER;
            REDAKTOR = _other.REDAKTOR;
            TERMINOVO = _other.TERMINOVO;
            DISCOUNT = _other.DISCOUNT;
            DISCOUNT_CODE = _other.DISCOUNT_CODE;
            STATUS = _other.STATUS;
            KOMPLEKT = new List<KOMPLEKT>(_other.KOMPLEKT);
        }
        /// <summary>
        /// ДЛЯ ШАБЛОНА НЕ ВСІ ПОЛЯ ПЕРЕНОСЯТЬСЯ!!!!
        /// </summary>
        /// <param name="_kompl"></param>
        /// <param name="_old"></param>
        public ZAKAZ(List<KOMPLEKT> _kompl, ZAKAZ _old)
        {
            ID = -1;
            NUMBER = 0;
            STATUS = Z_STATUS.NEW;
            TYPE = _old.TYPE;
            DATE_IN = DateTime.Now;
            DATE_MAX = DateTime.Now.AddDays(3);
            KOMPLEKT = new List<KOMPLEKT>(_kompl);
            WORKER = _old.WORKER;
            REDAKTOR = _old.REDAKTOR;
        }
        /// <summary>
        /// ДЛЯ ШАБЛОНА НЕ ВСІ ПОЛЯ ПЕРЕНОСЯТЬСЯ!!!!
        /// </summary>
        /// <param name="_other"></param>
        /// <param name="WithKomplekt"></param>
        public ZAKAZ(ZAKAZ _other, bool WithKomplekt)
        {
            ID = -1;
            TYPE = _other.TYPE;
            NUMBER = 0;
            DATE_IN = DateTime.Now;
            DATE_MAX = DateTime.Now.AddDays(3);
            PHONE = _other.PHONE;
            CLIENT_NAME = _other.CLIENT_NAME;
            REQV = _other.REQV;
            COMM = _other.COMM;
            WORKER = _other.WORKER;
            REDAKTOR = _other.REDAKTOR;
            TERMINOVO = _other.TERMINOVO;
            STATUS = Z_STATUS.NEW;
            if (WithKomplekt) KOMPLEKT = new List<KOMPLEKT>(_other.KOMPLEKT);
        }

        public static bool operator ==(ZAKAZ l, ZAKAZ r)
        {
            if (l is null && r is null) return true;
            if (l is null || r is null) return false;

            return l.Equals(r) || ReferenceEquals(l, r);
        }

        public static bool operator !=(ZAKAZ l, ZAKAZ r)
        {
            return !(l == r);
        }

        public bool Equals(ZAKAZ other)
        {
            if (other == null) return false;

            if (ID != other.ID ||
                TYPE != other.TYPE ||
                ((NUMBER.HasValue && other.NUMBER.HasValue && NUMBER.Value != other.NUMBER.Value) ||
                 (NUMBER.HasValue != other.NUMBER.HasValue)) ||
                DATE_IN != other.DATE_IN ||
                DATE_MAX != other.DATE_MAX ||
                ((DATE_OUT.HasValue && other.DATE_OUT.HasValue && DATE_OUT.Value != other.DATE_OUT.Value) ||
                 (DATE_OUT.HasValue != other.DATE_OUT.HasValue)) ||
                PHONE != other.PHONE ||
                CLIENT_NAME != other.CLIENT_NAME ||
                REQV != other.REQV ||
                TTN_IN != other.TTN_IN ||
                TTN_OUT != other.TTN_OUT ||
                COMM != other.COMM ||
                ((DISCOUNT.HasValue && other.DISCOUNT.HasValue && DISCOUNT.Value != other.DISCOUNT.Value) ||
                 (DISCOUNT.HasValue != other.DISCOUNT.HasValue)) ||
                CALLBACK != other.CALLBACK ||
                WORKER != other.WORKER ||
                REDAKTOR != other.REDAKTOR ||
                TERMINOVO != other.TERMINOVO ||
                STATUS != other.STATUS)
                return false;

            if (KOMPLEKT == null && other.KOMPLEKT == null)
                return true;
            if (KOMPLEKT == null || other.KOMPLEKT == null)
                return false;
            if (KOMPLEKT.Count != other.KOMPLEKT.Count)
                return false;

            var setA = new HashSet<KOMPLEKT>(KOMPLEKT);
            var setB = new HashSet<KOMPLEKT>(other.KOMPLEKT);
            return setA.SetEquals(setB);
        }

        public override bool Equals(object obj) => Equals(obj as ZAKAZ);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + ID;
                hash = hash * 31 + TYPE.GetHashCode();
                hash = hash * 31 + (NUMBER.HasValue ? NUMBER.Value + 100003 : 0);    // int?
                hash = hash * 31 + DATE_IN.GetHashCode();
                hash = hash * 31 + DATE_MAX.GetHashCode();
                hash = hash * 31 + (DATE_OUT.HasValue ? DATE_OUT.Value.GetHashCode() + 103 : 0);  // DateTime?
                hash = hash * 31 + (PHONE?.GetHashCode() ?? 0);
                hash = hash * 31 + (CLIENT_NAME?.GetHashCode() ?? 0);
                hash = hash * 31 + (REQV?.GetHashCode() ?? 0);
                hash = hash * 31 + (TTN_IN?.GetHashCode() ?? 0);
                hash = hash * 31 + (TTN_OUT?.GetHashCode() ?? 0);
                hash = hash * 31 + (COMM?.GetHashCode() ?? 0);
                hash = hash * 31 + (DISCOUNT.HasValue ? DISCOUNT.Value + 103 : 0); // byte?
                hash = hash * 31 + CALLBACK;
                hash = hash * 31 + (WORKER?.GetHashCode() ?? 0);
                hash = hash * 31 + (REDAKTOR?.GetHashCode() ?? 0);
                hash = hash * 31 + (TERMINOVO ? 1 : 0);
                hash = hash * 31 + STATUS.GetHashCode();

                int komplektHash = 0;
                if (KOMPLEKT != null)
                {
                    foreach (var k in new HashSet<KOMPLEKT>(KOMPLEKT))
                        komplektHash ^= k.GetHashCode();
                }
                hash = hash * 31 + komplektHash;

                return hash;
            }
        }
    }

    public class KOMPLEKT : IEquatable<KOMPLEKT>
    {
        public static implicit operator KOMPLEKT(SERVICE_ID_INFO _other)
        {
            if (_other == null) return null;

            return new KOMPLEKT
            {
                service_ID = _other.ID,
                type_ID = _other.TYPE_ID,
                count = _other.COUNT,
                costs = _other.COST,
                color = _other.COLOR_ID
            };
        }
        public int service_ID { get; set; }
        public int type_ID { get; set; }
        public int? color { get; set; }
        public int count { get; set; }
        public float costs { get; set; }
        public bool Equals(KOMPLEKT other)
        {
            if (other == null) return false;

            return service_ID == other.service_ID &&
                   type_ID == other.type_ID &&
                   ((color.HasValue && other.color.HasValue && color.Value == other.color.Value) ||
                    (!color.HasValue && !other.color.HasValue)) &&
                   count == other.count &&
                   Math.Abs(costs - other.costs) < 0.0001f;
        }

        public override bool Equals(object obj) => Equals(obj as KOMPLEKT);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + service_ID;
                hash = hash * 31 + type_ID;
                hash = hash * 31 + (color.HasValue ? color.Value + 103 : 0); // 103 - зміщення для null/0
                hash = hash * 31 + count;
                hash = hash * 31 + costs.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// Клас який тримає в памяті всі можливі варіанти товарів/послуг
    /// </summary>
    public class SERVICE_ID_LIST
    {
        public List<SERVICE_ID_INFO> INFO { get; set; }

        public SERVICE_ID_LIST(List<KOMPLEKT> _input)
        {
            INFO = _input.Select(x => (SERVICE_ID_INFO)x).ToList();
        }

        public SERVICE_ID_LIST(List<SERVICE_ID_INFO> _input)
        {
            INFO = _input;
        }

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

        public override string ToString()
        {
            if (INFO == null || INFO.Count < 0) return "";

            int[] MaxWidth = new int[] { 2, 30, 12, 10 };

            int[] ColumnWidth = new int[] { 2, 0, 0, 0 };

            float _sum = 0f; string _name;

            foreach (SERVICE_ID_INFO _inf in INFO)
            {
                if (_inf.COUNT > 1)
                {
                    _name = _inf.COUNT.ToString() + " x " + _inf.NAME;
                }
                else
                {
                    _name = _inf.NAME;
                }

                int _CellWidth = (_name + (_inf.TYPE_NAME != null ?
                    " (" + _inf.TYPE_NAME + ")" : "")).Length;

                if (_CellWidth > ColumnWidth[1]) ColumnWidth[1] = _CellWidth;

                _CellWidth = (_inf.COLOR_NAME ?? "").Length;

                if (_CellWidth > ColumnWidth[2]) ColumnWidth[2] = _CellWidth;

                _CellWidth = _inf.COST.ToString().Length;

                if (_CellWidth > ColumnWidth[3]) ColumnWidth[3] = _CellWidth;

                _sum += _inf.COST;
            }

            if (_sum.ToString().Length > ColumnWidth[3])
                ColumnWidth[3] = _sum.ToString().Length;

            for (int i = 0; i < MaxWidth.Length; i++)
            {
                if (ColumnWidth[i] > MaxWidth[i]) ColumnWidth[i] = MaxWidth[i];
            }

            string LineBorder = GetLine(ColumnWidth);

            string _out = LineBorder;

            //+--+------------------------+----------+---------+
            //|01|Установка від замовника |Мультикам |10000.50 |
            //|  |(Навушники)             |          |         |
            //+--+------------------------+----------+---------+
            //|02|Кавер                   |Піксель   |200      |
            //+--+------------------------+----------+---------+
            //|  |                        |          |10200.50 |
            //+--+------------------------+----------+---------+

            int _counter = 1;

            string[] _val;

            foreach (SERVICE_ID_INFO _info in INFO)
            {
                if (_info.COUNT > 1)
                {
                    _name = _info.COUNT.ToString() + " x " + _info.NAME;
                }
                else
                {
                    _name = _info.NAME;
                }

                _val = new string[] {
                    _counter++.ToString(),
                    _name + (_info.TYPE_NAME != null ? $" ({_info.TYPE_NAME})":""),
                    _info.COLOR_NAME ?? "",
                    _info.COST.ToString()
                };

                _out += GetRow(ColumnWidth, _val);
            }

            if (_counter > 2)
            {
                _val = new string[] { "", "", "", _sum.ToString() };

                return _out += GetRow(ColumnWidth, _val);
            }
            else
            {
                return _out;
            }

        }

        private string GetLine(int[] _width)
        {
            string _out = "";

            foreach (int w in _width)
            {
                _out += "+".PadRight(w + 1, '-');
            }

            return _out + "+\n";
        }
        private string GetRow(int[] _width, string[] _values)
        {
            string _out = "";
            string[] _newValues = new string[_values.Length];
            string _v;

            for (int i = 0; i < _width.Length && i < _values.Length; i++)
            {
                _v = _values[i].Substring(0,
                    _width[i] <= _values[i].Length ?
                    _width[i] : _values[i].Length);

                if (IsNotNumber(_v))
                {
                    _out += "|" + _v.PadRight(_width[i], ' ');
                }
                else
                {
                    _out += "|" + _v.PadLeft(_width[i], ' ');
                }

                _newValues[i] = _values[i].Length >= _width[i] ? _values[i].Substring(_width[i]) : "";
            }

            _out += "|\n";

            bool _stop = true;

            foreach (string s in _newValues)
            {
                if (s != string.Empty)
                {
                    _stop = false;
                    break;
                }
            }

            if (_stop)
            {
                return _out + GetLine(_width);
            }
            else
            {
                return _out + GetRow(_width, _newValues);
            }
        }
        bool IsNotNumber(string input)
        {
            if (input == string.Empty) return false;

            return !double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
        }
    }


    /// <summary>
    /// Клас який тримає в памяті конкретний товар/послугу
    /// з підтипами та їх кольорами
    /// </summary>
    public class SERVICE_ID_INFO : IEquatable<SERVICE_ID_INFO>
    {
        public static implicit operator SERVICE_ID_INFO(KOMPLEKT _other)
        {
            if (_other == null) return null;

            return new SERVICE_ID_INFO
            {
                ID = _other.service_ID,
                TYPE_ID = _other.type_ID,
                COLOR_ID = _other.color,
                COST = _other.costs,
                COUNT = _other.count,
                ATR = Convert.ToInt16(SERVICE_INFO.GetAtr(_other.service_ID) ?? 0),
                ORDER = SERVICE_INFO.GetOrder(_other.service_ID)
            };
        }

        public bool HAS_COLOR => COLOR_ID != null || SERVICE_INFO.GetHasColor(ID, TYPE_ID);

        public bool IS_POSLUGA
        {
            get
            {
                if (ATR >= 0)
                {
                    //1 замовлення (абонент)
                    //2 замовлення (вручну)
                    //4 покупка (абонент)
                    //8 покупка (вручну)
                    //16 витрати

                    return (ATR & 8) != 8;
                }

                return false;
            }
        }

        public SERVICE_ID_INFO()
        {
            ID = -1;
            TYPE_ID = 1;
            COLOR_ID = null;
        }

        public SERVICE_ID_INFO(SERVICE_ID_INFO _other)
        {
            ID = _other?.ID ?? -1;
            TYPE_ID = _other?.TYPE_ID ?? 1;
            COLOR_ID = _other?.COLOR_ID;
            ATR = _other?.ATR ?? 0;
            COST = _other?.COST ?? 0f;
            COUNT = _other?.COUNT ?? 1;
            ORDER = _other?.ORDER ?? 255;
        }

        private int _id = -1;

        public int ID
        {
            get => _id; set
            {
                _id = value;
                NAME = SERVICE_INFO.GetServiceName(value);
                TYPE_ID = 1;
            }
        }

        private int _typeId = 1;

        public int TYPE_ID
        {
            get => _typeId; set
            {
                _typeId = value;
                TYPE_NAME = SERVICE_INFO.GetTypeName(ID, value);
            }
        }

        private int? _colorId = null;

        public int? COLOR_ID
        {
            get => _colorId; set
            {
                _colorId = value;
                COLOR_NAME = SERVICE_INFO.GetColorName(value);
            }
        }
        public string NAME { get; private set; }
        public string TYPE_NAME { get; private set; }

        public string FULL_NAME => NAME + (TYPE_NAME != null ? " (" + TYPE_NAME + ")" : "");
        public string COLOR_NAME { get; private set; }

        public event EventHandler<float> CostChanged;
        public float COST
        {
            get=> _curCost; set
            {
                _curCost = value;

                if (_curCost != 0f && _deffCost == 0f)
                {
                    _deffCost = value;
                }

                CostChanged?.Invoke(this, value);
            }
        }

        private float _curCost = 0f;
        private float _deffCost = 0f;
        public int ATR
        {
            get=> atr ?? Convert.ToInt16(SERVICE_INFO.GetAtr(ID) ?? 0);
            set
            {
                atr = value;
            }
        }
        private int? atr = null;
        public int COUNT { get; set; } = 1;

        public byte ORDER { get; set; } = byte.MaxValue;

        public float GetDeffaultCost(bool ResetCost = false)
        {
            if (ResetCost) _curCost = _deffCost;

            return _deffCost;
        }

        public override string ToString()
        {
            return FULL_NAME
                + (COLOR_NAME != null ? " [" + COLOR_NAME + "]" : " ")
                + COUNT + "*" + COST;
        }

        public bool Equals(SERVICE_ID_INFO _other)
        {
            return ID == _other.ID
                && TYPE_ID == _other.TYPE_ID
                && COLOR_ID == _other.COLOR_ID
                && ATR == _other.ATR
                && COUNT == _other.COUNT
                && COST == _other.COST;
        }

        public bool OneFamily(SERVICE_ID_INFO _other)
        {
            return ID == (_other?.ID ?? -1)
                && TYPE_ID == (_other?.TYPE_ID ?? 1)
                && COLOR_ID == _other?.COLOR_ID;
        }

        public override bool Equals(object obj) => Equals(obj as SERVICE_ID_INFO);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + ID;
                hash = hash * 31 + TYPE_ID;
                hash = hash * 31 + (COLOR_ID.HasValue ? (int)COLOR_ID + 103 : 0);
                hash = hash * 31 + COUNT;
                hash = hash * 31 + COST.GetHashCode();
                return hash;
            }
        }

    }

    public enum Z_TYPE : byte
    {
        NULL = 255,
        CONV = 0,
        SOLD = 1
    }

    public enum Z_STATUS : byte
    {
        NULL = 255,
        NEW = 0,
        ACTIVE = 1,
        ARCHIVE = 2
    }
}