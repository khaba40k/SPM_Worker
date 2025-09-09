using SPM_Core;
using SPM_print;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class ZakazListControl : UserControl
    {
        public IEnumerable<ZakazInfo> Items => allItems ?? new List<ZakazInfo>();
        private IEnumerable<ZakazInfo> FilteredItems => _filtered();
        private bool PAUSE_JSON_QUERY = false;
        private IEnumerable<ZakazInfo> CheckedZakazList => Items.Where(z => z.Checked == true);
        public JSON_ZAKAZ_LIST ZAKAZ_LIST { get; set; } = new JSON_ZAKAZ_LIST();
        private ZAKAZ_CATCH _CATCH = new ZAKAZ_CATCH();
        private Dictionary<Z_STATUS, DateTime?> LastUpdateAt = new Dictionary<Z_STATUS, DateTime?>(); 
        public event EventHandler<SizeEventHandler> _SizeChanged;
        private bool SOME_CHANGED { get; set; } = false;
        public Z_STATUS STATUS { get; set; } = Z_STATUS.NEW;
        private Z_STATUS prew_status = Z_STATUS.NULL;
        private System.Threading.Timer backgroundTimer;
        private const byte AUTO_REFRESH_TIME = 3;

        private List<ZakazInfo> allItems = new List<ZakazInfo>();

        public ZakazListControl()
        {
            InitializeComponent();

            dateTimeSort.MinDate = DateTime.Parse("01.01.2023");
            dateTimeSort.MaxDate = DateTime.Now.AddHours(3);

            HandleCreated += (s, e) => BeginInvoke((MethodInvoker)INIT_DATA);

            dateTimeSort.ValueChanged += (s, e) =>
            {
                SOME_CHANGED = true;
            };

            filterPanel.ControlAdded += (s, e) =>
            {
                e.Control.MinimumSize = new System.Drawing.Size(0, filterPanel.Height);
            };

            rb0.CheckedChanged += (s, e) => {

                RadioButton rb = (RadioButton)s;
                if (!rb.Checked) return;

                dateTimeSort.CustomFormat = "dd.MM.yy";
                dateTimeSort.ShowUpDown = false;
                label_date.Text = "Показати від:";

                STATUS = Z_STATUS.NEW;

                REFRESH_LIST(STATUS);
            };

            rb1.CheckedChanged += (s, e) => {

                RadioButton rb = (RadioButton)s;
                if (!rb.Checked) {
                    label_date.Visible = true;
                    dateTimeSort.Visible = true;

                    return; 
                }

                dateTimeSort.CustomFormat = "dd.MM.yy";
                dateTimeSort.ShowUpDown = false;
                label_date.Visible = false;
                dateTimeSort.Visible = false;

                PAUSE_JSON_QUERY = true;
                dateTimeSort.Value = DateTime.Now.AddHours(-3);
                PAUSE_JSON_QUERY = false;

                STATUS = Z_STATUS.ACTIVE;

                REFRESH_LIST(STATUS);
            };

            rb2.CheckedChanged += (s, e) => {

                RadioButton rb = (RadioButton)s;
                if (!rb.Checked) return;

                PAUSE_JSON_QUERY = true;
                dateTimeSort.CustomFormat = "MM.yyyy";
                dateTimeSort.ShowUpDown = true;
                label_date.Text = "За місяць:";
                dateTimeSort.Value = dateTimeSort.MaxDate;
                PAUSE_JSON_QUERY = false;

                STATUS = Z_STATUS.ARCHIVE;

                REFRESH_LIST(STATUS);
            };

            flpList.ClientSizeChanged += FlpList_ClientSizeChanged;

            flpList.ControlAdded += (s, e) =>
            {
                ZakazInfo _item = (ZakazInfo)e.Control;

                _item.Width = flpList.ClientSize.Width - flpList.Padding.Horizontal;

                label_count.Text = $"ЗА СПИСКОМ: {flpList.Controls.Count}";
            };

            flpList.ControlRemoved += (s, e) =>
            {
                label_count.Text = $"ЗА СПИСКОМ: {flpList.Controls.Count}";
            };

            butDelete.Click += (s, e) => Delete(CheckedZakazList.ToList());
        }

        private void FlpList_ClientSizeChanged(object sender, EventArgs e)
        {
            _SizeChanged?.Invoke(this,
                    new SizeEventHandler(flpList.ClientSize.Width - flpList.Padding.Horizontal));
        }

        private void SetFilter(object sender, EventArgs e)
        {
            ApplyFilterAndSort();
        }

        private void moveZakaz(object sender, ZakazEventArgs e)
        {
            ZAKAZ _inf = (ZAKAZ)sender;
            PrintType toPrint = PrintType.EMPTY;

            if (e.STATUS == Z_STATUS.NEW)
            {
                using (MoveForm _form = new MoveForm(new string[] { "ТТН (вхідна)" }))
                {
                    if (_form.ShowDialog() == DialogResult.OK)
                    {
                        _inf.TTN_IN = !string.IsNullOrWhiteSpace(_form.VALUE0)
                            ? _form.VALUE0.Trim()
                            : null;

                        SOME_CHANGED = _inf.TTN_IN != null;
                        toPrint = SOME_CHANGED ? PrintType.Worker : PrintType.EMPTY;
                    }
                }
            }
            else
            {
                using (MoveForm _form = new MoveForm(new string[] {
                            "Працівник",
                            "Створити накладну",
                            "Сума (факт)"
                        }, new string[] {
                            _inf.WORKER,
                            _inf.TTN_OUT,
                            _inf.SUM.ToString()
                        }))
                {
                    if (_form.ShowDialog() == DialogResult.OK)
                    {
                        _inf.WORKER = 
                            !string.IsNullOrWhiteSpace(_form.VALUE0)
                            ? _form.VALUE0.Trim()
                            : null;

                        _inf.TTN_OUT = !string.IsNullOrWhiteSpace(_form.VALUE1)
                        ? _form.VALUE1.Trim()
                        : null;

                        _inf.SUM = TEXT_TO._FLOAT(_form.VALUE2);

                        _inf.DATE_OUT = DateTime.Now;

                        SOME_CHANGED = _inf.TTN_OUT != null;
                        toPrint = SOME_CHANGED ? PrintType.Short : PrintType.EMPTY;
                    }
                }
            }

            if (SOME_CHANGED && SERVICE_INFO.SAVE_ZAKAZ(_inf, out string mes))
            {
                if (toPrint == PrintType.EMPTY)
                {
                    CustomMessage.Show(mes, "Статус",
                           MessageBoxIcon.Information);
                }
                else
                {
                    if (DialogResult.Yes == CustomMessage.Show(mes, "Статус",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Information,
                           new MessButText { Yes = "Друкувати", No = "ОК" }))
                    {
                        List<ZakazInfo> curZakaz = new List<ZakazInfo>();
                        curZakaz.Add(new ZakazInfo(_inf));

                        PrintZakaz(curZakaz, toPrint);
                    }
                }
            }
        }

        private void Delete(object _zakaz, EventArgs e)
        {
            List<ZakazInfo> _temp = new List<ZakazInfo>();

            _temp.Add((ZakazInfo)_zakaz);

            Delete(_temp);
        }

        private void Delete(List<ZakazInfo> inputList)
        {
            string _out = "Буде видалено наступні заявки:\n\n"; int counter = 0;

            string[] selectedIds = new string[inputList.Count];

            foreach (ZakazInfo ZI in inputList)
            {
                selectedIds[counter] = ZI.INFO.ID.ToString();
                _out += $"{++counter}: " +
                $"{(ZI.INFO.NUMBER > 0 ? "№ " + ZI.INFO.NUMBER.ToString() + " -> " : "")}" +
                $"{ZI.INFO.CLIENT_NAME}\n";
            }

            if (DialogResult.Yes == CustomMessage.Show(_out,
                $"Видалення заявок: [{selectedIds.Length}]",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {

                if (SERVICE_INFO.DELETE_ZAKAZ_BY_ID(selectedIds, out string message))
                {
                    CustomMessage.Show(message, "Результат видалення",
                        MessageBoxIcon.Information);
                    SOME_CHANGED = true;
                }
                else
                {
                    CustomMessage.Show(message, "Результат видалення",
                        MessageBoxIcon.Error);
                }
            }
        }

        private void INIT_DATA()
        {
            cbFilter.TagsChanged += SetFilter;
            dateTimeSort.Value = DateTime.Now.AddDays(-6);
            StartBackgroundTimer();
        }

        private void REFRESH_LIST(Z_STATUS _status)
        {
            if (PAUSE_JSON_QUERY) return;

            if (SOME_CHANGED && LastUpdateAt.ContainsKey(_status))
            {
                LastUpdateAt.Remove(_status);
            }

            DateTime? _lud = LastUpdateAt.ContainsKey(_status) ? LastUpdateAt[_status] : null;

            JSON_ZAKAZ_LIST _temp = GET_LIST(_status, _lud);

            if (!SOME_CHANGED && prew_status == STATUS && !_temp.NeedUpdate) return;

            if (!SOME_CHANGED 
                && _temp.Items.Count == 0 
                && _CATCH.HAS(_status, _temp.LAST_CHANGE))
            {//Кеш підходить
                _temp.Items = _CATCH.GET(STATUS);
                _temp.Count_ACTIVE = ZAKAZ_LIST.Count_ACTIVE;
                _temp.Count_NEW = ZAKAZ_LIST.Count_NEW;
                _temp.Count_ARCHIVE = ZAKAZ_LIST.Count_ARCHIVE;
            }
            else
            {//Кеш не підходить/Щось змінено
                if (LastUpdateAt.ContainsKey(_status))
                    LastUpdateAt.Remove(_status);

                LastUpdateAt.Add(_status, _temp.LAST_CHANGE);

                _CATCH.SET(_temp);

                rb0.Text = $"НОВІ ({_temp.Count_NEW})";
                rb1.Text = $"РОБОТА ({_temp.Count_ACTIVE})";
                rb2.Text = $"АРХІВ ({_temp.Count_ARCHIVE})";
            }

            ZAKAZ_LIST = _temp;

            cbFilter.Items.Clear();

            List<CheckComboBoxItem> _filters = new List<CheckComboBoxItem>();

            ZAKAZ _zFilter = ZAKAZ_LIST.Items.Find(f => f.TYPE == Z_TYPE.CONV);

            if (_zFilter != null)
            {
                _filters.Add(new CheckComboBoxItem(
                    "> ПЕРЕОБЛ.",
                    "type:0", 
                    cbFilter.GetMemoryChecked("type:0")));
            }

            _zFilter = ZAKAZ_LIST.Items.Find(f => f.TYPE == Z_TYPE.SOLD);

            if (_zFilter != null)
            {
                _filters.Add(new CheckComboBoxItem(
                    "> ПРОДАЖ",
                    "type:1",
                    cbFilter.GetMemoryChecked("type:1")));
            }
            
            string[] redaktors = ZAKAZ_LIST.Items.Select(r => r.REDAKTOR).Distinct().ToArray();

            foreach (string _red in redaktors)
            {
                _filters.Add(new CheckComboBoxItem(
                    "> " + SERVICE_INFO.GetUserName(_red),
                    $"redaktor:{_red}",
                    cbFilter.GetMemoryChecked($"redaktor:{_red}")));
            }

            cbFilter.Items.AddRange(_filters.ToArray());

            ClearAll();

            AddItemOnPanel(ZAKAZ_LIST.Items.ToArray());

            prew_status = STATUS;

            SOME_CHANGED = false;

            ApplyFilterAndSort();
        }

        private JSON_ZAKAZ_LIST GET_LIST(
                        Z_STATUS _status = Z_STATUS.NEW,
                        DateTime? _udateDate = null,
                        int limit = 50)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string _limit = _status != Z_STATUS.ARCHIVE ? $"limit={limit}&" : "";
            string _last_change = "";

            if (_udateDate != null)
            {
                _last_change = $"last_change={ToMySqlDateTime(_udateDate)}&";
            }

            string url = "https://sholompromax.com/ii/json_get_list";
            string postData = $"{_last_change}{_limit}token={SERVICE_INFO.TOKEN}&status={(byte)_status}";

            if (_status == Z_STATUS.NEW)
            {
                DateTime _d = dateTimeSort.Value;

                postData += $"&afterDate={_d.Year}-{_d.Month.ToString().PadLeft(2, '0')}-{_d.Day.ToString().PadLeft(2, '0')}";
            }
            else if (_status == Z_STATUS.ARCHIVE)
            {
                DateTime _d = dateTimeSort.Value;

                postData += $"&afterDate={_d.Year}-{_d.Month.ToString().PadLeft(2, '0')}-01";
            }

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

            try
            {
                // Отримання відповіді
                string responseText;
                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }

                // Парсинг JSON
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Deserialize<JSON_ZAKAZ_LIST>(responseText);
            }
            catch
            {
                return new JSON_ZAKAZ_LIST() { NeedUpdate = false };
            }
        }

        private void StartBackgroundTimer()
        {
            backgroundTimer = new System.Threading.Timer(TimerRefreshCallback, null, 0, AUTO_REFRESH_TIME * 1000);
        }

        private void TimerRefreshCallback(object state)
        {
            if (PAUSE_JSON_QUERY) return;

            try
            {
                BeginInvoke((Action)(() => REFRESH_LIST(STATUS)));
            }
            catch
            {

            }
        }

        public void ClearAll()
        {
            foreach (ZakazInfo _info in flpList.Controls)
                if (_info.Checked) _info.Checked = false;

            lb_count_checked.Visible = false;
            but_print.Visible = false;
            butDelete.Visible = false;

            allItems.Clear();
        }

        private IEnumerable<ZakazInfo> _filtered()
        {
            string[] _filters = cbFilter.CheckedTags;

            string search = txtSearch.Text.Trim().ToLower();

            List<byte> _types = new List<byte>();
            List<string> redaktors = new List<string>();
            string[] split;

            foreach (string f in _filters)
            {
                split = f.Split(':');
                if (split.Length > 1)
                {
                    switch (split[0])
                    {
                        case "type":
                            if (byte.TryParse(split[1], out byte _t))
                            {
                                _types.Add(_t);
                            }
                            break;
                        case "redaktor":
                            redaktors.Add(split[1]);
                            break;
                    }
                }
            }

            // Фільтрація

            IEnumerable<ZakazInfo> _tempList = Items.Where(i =>
                _types.Contains(i.TYPE)
                && redaktors.Contains(i.REDAKTOR));

            if (!string.IsNullOrWhiteSpace(search))
            {
                _tempList = _tempList.Where(item =>
                item.INFO.NUMBER.ToString().Contains(search) ||
                item.INFO.CLIENT_NAME.ToLower().Contains(search) ||
                (item.INFO.COMM?.ToLower() ?? "").Contains(search) ||
                item.INFO.PHONE.ToLower().Contains(search) ||
                item.INFO.KOMPLEKT.Find(k => item.GetNameAndType(k.service_ID, k.type_ID).ToLower().Contains(search)) != null
               );
            }

            return _tempList;
        }

        public void AddItemOnPanel(ZAKAZ[] z)
        {
            int i;
            int StartNewInd = z.Length;

            //Видаляємо зайві
            if (flpList.Controls.Count > z.Length)
            {
                int curInd = flpList.Controls.Count - 1;

                for (i = curInd; i >= z.Length; i--)
                flpList.Controls.RemoveAt(i);
            }

            //Додаємо невистачаючі
            if (z.Length > flpList.Controls.Count)
            {
                List<ZakazInfo> _temp = new List<ZakazInfo>();

                StartNewInd = flpList.Controls.Count;

                for (i = StartNewInd; i < z.Length; i++)
                {
                    ZakazInfo nz = new ZakazInfo(z[i]);
                    SetEvents(nz);

                    _temp.Add(nz);
                }

                flpList.Controls.AddRange(_temp.ToArray());

                allItems.AddRange(_temp);
            }

            for (i = 0; i < StartNewInd; i++)
            {
                ZakazInfo _inf = flpList.Controls[i] as ZakazInfo;
                _inf.INFO = z[i];

                allItems.Add(_inf);
            }
        }

        private void SetEvents(ZakazInfo _inf)
        {
            _inf.WriteZakaz += WriteZakaz;
            _inf.CheckedChange += CheckedChanged;
            _inf.RemoveZakaz += Delete;
            _inf.PrintZakaz += Print;
            _inf.MoveZakaz += moveZakaz;
            _SizeChanged += _inf.SetWidth;
        }

        private void ApplyFilterAndSort()
        {
            if (Items.Count() == 0) return;

            flpList.SuspendLayout();

            List<ZakazInfo> _filtered = FilteredItems.ToList();

            int countShow = flpList.Controls.Count;

            foreach (ZakazInfo _fltr in flpList.Controls)
            {
                if (!_filtered.Contains(_fltr))
                {
                    _fltr.Hide();
                    countShow--;
                }
                else if (!_fltr.Visible)
                {
                    _fltr.Show();
                }
            }

            label_count.Text = $"ЗА СПИСКОМ: {countShow}";

            flpList.ResumeLayout();
        }

        public void WriteZakaz(object sender, ZakazEventArgs e)
        {
            using (ZAKAZ_FORM _zakazForm = new ZAKAZ_FORM((ZAKAZ)sender))
            {
                PAUSE_JSON_QUERY = true;

                _zakazForm.ShowDialog();

                SOME_CHANGED = _zakazForm.CHANGED;

                PAUSE_JSON_QUERY = false;

                if (SOME_CHANGED)
                {
                    REFRESH_LIST_WITH_DELAY(50);

                    if (_zakazForm.TO_PRINT_FROM_WORKER)
                    {
                        PrintZakaz(new List<ZakazInfo> { new ZakazInfo(_zakazForm.INFO) },
                             PrintType.Worker);
                    }
                }
            }
        }

        public void CheckedChanged(object sender, ZakazEventArgs e)
        {
            ZakazInfo _finded = allItems.Find(z => z.INFO.ID == e.ID);

            _finded.Checked = e.CHECKED;

            if (CheckedZakazList.Count() > 0)
            {
                lb_count_checked.Text = $"Вибрано: {CheckedZakazList.Count()}";
                lb_count_checked.Visible = true;
                butDelete.Visible = true;
                but_print.Visible = true;
            }
            else
            {
                lb_count_checked.Text = "Вибрано: 0";
                lb_count_checked.Visible = false;
                butDelete.Visible = false;
                but_print.Visible = false;
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SOME_CHANGED = true;
            ApplyFilterAndSort();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";

            if (STATUS != Z_STATUS.ARCHIVE && dateTimeSort.Value != DateTime.Now.AddDays(-31))
            {
                dateTimeSort.Value = DateTime.Now.AddDays(-31);
            }else if (STATUS == Z_STATUS.ARCHIVE && dateTimeSort.Value < DateTime.Now)
            {
                try
                {
                    dateTimeSort.Value = DateTime.Now.AddHours(-1);
                }
                catch (Exception ex)
                {
                    CustomMessage.Show(ex.ToString(), "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimeSort.Value = DateTime.Now.AddHours(-12);
                }
            }

            cbFilter.ResetFilters();

            ApplyFilterAndSort();
        }

        private static string ToMySqlDateTime(DateTime? dt)
        {
            if (dt == null) 
            { 
                return "";
            }
            else
            {
                DateTime _out = (DateTime)dt;
                return _out.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        private void dateTimeSort_ValueChanged(object sender, EventArgs e)
        {
            REFRESH_LIST_WITH_DELAY(500);
        }

        private async void REFRESH_LIST_WITH_DELAY(int milisec = 1000)
        {
            await Task.Delay(milisec);
            REFRESH_LIST(STATUS);
        }

        private void but_print_Click(object sender, EventArgs e)
        {
            PrintZakaz(CheckedZakazList.ToList());
        }

        private void Print(object sender, byte stat)
        {
            //0 - standart, 1 - worker, 2 - short
            List<ZakazInfo> _info = new List<ZakazInfo>();

            _info.Add((ZakazInfo)sender);

            PrintZakaz(_info, (PrintType)stat);
        }

        private void PrintZakaz(List<ZakazInfo> _inputList, PrintType pt = PrintType.EMPTY)
        {
            List<PRINT_ZAKAZ_INFO> _toPrintList = new List<PRINT_ZAKAZ_INFO>();

            foreach (ZakazInfo _info in _inputList)
            {
                _toPrintList.Add(_info.INFO);
            }

            List<PRINT_TABLE_INFO> _out = new List<PRINT_TABLE_INFO>();

            foreach (PRINT_ZAKAZ_INFO _z in _toPrintList)
            {
                _out.Add(_z.ToBase());
            }

            if (pt == PrintType.EMPTY)
            {
                switch (STATUS)
                {
                    case Z_STATUS.ACTIVE:
                        pt = PrintType.Worker;
                        break;
                    case Z_STATUS.ARCHIVE:
                        pt = PrintType.Short;
                        break;
                    default:
                        pt = PrintType.Standart;
                        break;
                }
            }

            PrintInfo.Print(_out, pt);
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            using (ZAKAZ_FORM _formCreate = new ZAKAZ_FORM())
            {
                PAUSE_JSON_QUERY = true;

                _formCreate.ShowDialog();

                SOME_CHANGED = _formCreate.CHANGED;

                PAUSE_JSON_QUERY = false;

                if (SOME_CHANGED)
                {
                    REFRESH_LIST_WITH_DELAY(100);

                    if (_formCreate.TO_PRINT_FROM_WORKER)
                    {
                        PrintZakaz(new List<ZakazInfo> { new ZakazInfo(_formCreate.INFO) },
                             PrintType.Worker);
                    }
                }
            }
        }
    }

    public class PRINT_ZAKAZ_INFO : PRINT_TABLE_INFO
    {
        public PRINT_TABLE_INFO ToBase()
        {
            return new PRINT_TABLE_INFO
            {
                TYPE = TYPE,
                NUMBER = NUMBER,
                DATE_IN = DATE_IN,
                DATE_MAX = DATE_MAX,
                DATE_OUT = DATE_OUT,
                PHONE = PHONE,
                CLIENT_NAME = CLIENT_NAME,
                REQV = REQV,
                TTN_IN = TTN_IN,
                TTN_OUT = TTN_OUT,
                WORKER = WORKER,
                REDAKTOR = REDAKTOR,
                COMM = COMM,
                DISCOUNT = DISCOUNT,
                SUM = SUM,
                SERVICES = SERVICES
            };
        }

        public static implicit operator PRINT_ZAKAZ_INFO(ZAKAZ _other)
        {
            return new PRINT_ZAKAZ_INFO
            {
                TYPE = (byte)_other.TYPE,
                NUMBER = _other.NUMBER.ToString(),
                DATE_IN = _other.DATE_IN.ToString(),
                DATE_MAX = _other.DATE_MAX.ToString(),
                DATE_OUT = _other.DATE_OUT.ToString(),
                PHONE = _other.PHONE,
                CLIENT_NAME = _other.CLIENT_NAME,
                REQV = _other.REQV,
                TTN_IN = _other.TTN_IN,
                TTN_OUT = _other.TTN_OUT,
                WORKER = _other.WORKER,
                REDAKTOR = _other.REDAKTOR,
                COMM = _other.COMM,
                DISCOUNT = _other.DISCOUNT.ToString(),
                SUM = _other.SUM.ToString(),
                SERVICES = _other.KOMPLEKT
                      .Select(k => 
                              new Service(
                                  SERVICE_INFO.GetServiceName(k.service_ID),
                                  SERVICE_INFO.GetTypeName(k.service_ID, k.type_ID),
                                  k.count, 
                                  SERVICE_INFO.GetColorName(k.color), 
                                  k.costs,
                                  SERVICE_INFO.GetOrder(k.service_ID)))
                      .ToList()
            };
        }
    }

    class ZAKAZ_CATCH
    {
        private DateTime?[] _dateChange = new DateTime?[3] { null, null, null };
        private List<ZAKAZ>[] _catch = new List<ZAKAZ>[3] { 
            new List<ZAKAZ>(),
            new List<ZAKAZ>(),
            new List<ZAKAZ>()
        };

        public void SET(JSON_ZAKAZ_LIST _input)
        {
            if (_input == null || _input.Items.Count == 0) return;

            Z_STATUS _stat = _input.Items[0].STATUS;

            int _indx = 0;

            if (_stat == Z_STATUS.ACTIVE)
            {
                _indx = 1;
            }else if (_stat == Z_STATUS.ARCHIVE)
            {
                _indx = 2;
            }

            _catch[_indx].Clear();

            foreach (ZAKAZ z in _input.Items)
            {
                _catch[_indx].Add(z);
            }

            _dateChange[_indx] = _input.LAST_CHANGE;
        }

        public List<ZAKAZ> GET(Z_STATUS _stat)
        {
            int _indx = 0;

            if (_stat == Z_STATUS.ACTIVE)
            {
                _indx = 1;
            }
            else if (_stat == Z_STATUS.ARCHIVE)
            {
                _indx = 2;
            }

            return _catch[_indx];
        }

        public bool HAS(Z_STATUS _stat, DateTime? _d)
        {
            if (_stat == Z_STATUS.NULL || _d == null) return false;

            int _indx = 0;

            if (_stat == Z_STATUS.ACTIVE)
            {
                _indx = 1;
            }
            else if (_stat == Z_STATUS.ARCHIVE)
            {
                _indx = 2;
            }

            if (_dateChange[_indx] != _d ||
                _catch[_indx] == null) return false;

            return true;
        }

        //bool AreListsEqualUnordered<ZAKAZ>(List<ZAKAZ> list1, List<ZAKAZ> list2)
        //{
        //    if (list1 == null && list2 == null) return true;
        //    if (list1 == null || list2 == null) return false;
        //    if (list1.Count != list2.Count) return false;

        //    var set1 = new HashSet<ZAKAZ>(list1);
        //    var set2 = new HashSet<ZAKAZ>(list2);

        //    return set1.SetEquals(set2);
        //}
    }

    public class SizeEventHandler : EventArgs
    {
        public SizeEventHandler(int _w)
        {
            Width = _w;
        }

        public SizeEventHandler(int _w, int _h)
        {
            Width = _w;
            Height = _h;
        }

        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
    }
}


