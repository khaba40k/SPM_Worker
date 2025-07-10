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
        private Z_STATUS _status = Z_STATUS.NEW;
        //public event EventHandler Status_Changed;
        private bool PAUSE_JSON_QERY = false;
        private List<ZakazInfo> CheckedZakazList = new List<ZakazInfo>();
        public JSON_ZAKAZ_LIST ZAKAZ_LIST = new JSON_ZAKAZ_LIST();
        public Z_STATUS STATUS { 
            get => _status; 
            set { 
                if (_status != value){
                    _status = value;
                    STATUS_CHANGED(EventArgs.Empty);
                }
            } 
        }

        private List<ZakazInfo> allItems = new List<ZakazInfo>();

        protected virtual void STATUS_CHANGED(EventArgs e)
        {
            REFRESH_LIST(_status);
        }

        public ZakazListControl()
        {
            InitializeComponent();
            cbSortBy.Items.AddRange(new string[] { "Дата", "Тип" });
            cbSortOrder.Items.AddRange(new string[] { "↓", "↑" });
            cbSortBy.SelectedIndex = 0;
            cbSortOrder.SelectedIndex = 0;

            Load += (s, e) => {
                dateTimeSort.MinDate = DateTime.Parse("01.01.2023");
                dateTimeSort.MaxDate = DateTime.Now.AddHours(3);
                dateTimeSort.Value = DateTime.Now.AddDays(-31);
            };

            rb0.CheckedChanged += (s, e) => {

                RadioButton rb = (RadioButton)s;
                if (!rb.Checked) return;

                dateTimeSort.CustomFormat = "dd.MM.yy";
                dateTimeSort.ShowUpDown = false;
                label_date.Text = "Показати від:";
                STATUS = Z_STATUS.NEW; 
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

                PAUSE_JSON_QERY = true;
                dateTimeSort.Value = DateTime.Now.AddHours(-3);
                PAUSE_JSON_QERY = false;

                STATUS = Z_STATUS.ACTIVE; 
            };
            rb2.CheckedChanged += (s, e) => {

                RadioButton rb = (RadioButton)s;
                if (!rb.Checked) return;

                PAUSE_JSON_QERY = true;
                dateTimeSort.CustomFormat = "MM.yyyy";
                dateTimeSort.ShowUpDown = true;
                label_date.Text = "За місяць:";
                dateTimeSort.Value = dateTimeSort.MaxDate;
                PAUSE_JSON_QERY = false;
                STATUS = Z_STATUS.ARCHIVE;
            };

            flpList.SizeChanged += (s, e) =>
            {
                 foreach (Control ctrl in flpList.Controls)
                 {
                     ctrl.Width = flpList.ClientSize.Width - ctrl.Margin.Horizontal;
                 }
            };

            butDelete.Click += (s, e) => {

                string _out = "Буде видалено наступні заявки:\n\n"; int counter = 0;

                foreach (ZakazInfo ZI in CheckedZakazList)
                {
                    _out += $"{++counter}: {(ZI.INFO.NUMBER > 0 ? "№ " + ZI.INFO.NUMBER.ToString() + " -> ":"")}{ZI.INFO.CLIENT_NAME}\n";
                }

                if (DialogResult.Yes != MessageBox.Show(_out, $"Видалення заявок: [{CheckedZakazList.Count}]", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) return;
            };
        }

        void REFRESH_LIST(Z_STATUS _status = Z_STATUS.NEW)
        {
            if (PAUSE_JSON_QERY) return;

            ClearAll();

            ZAKAZ_LIST = GET_LIST(_status);

            rb0.Text = $"НОВІ ({ZAKAZ_LIST.Count_NEW})";
            rb1.Text = $"РОБОТА ({ZAKAZ_LIST.Count_ACTIVE})";
            rb2.Text = $"АРХІВ ({ZAKAZ_LIST.Count_ARCHIVE})";
            label_count.Text = $"ВСЬОГО ЗА СПИСКОМ: {ZAKAZ_LIST.CurrentCount}";

            AddZakaz(ZAKAZ_LIST.Items, SERVICE_INFO.SERVICE_LIST);

        }

        private void AddZakaz(List<ZAKAZ> _input, List<SERVICE_ID_INFO> _names)
        {
            ZakazInfo zakaz;

            SuspendLayout();

            foreach (ZAKAZ Z in _input)
            {
                zakaz = new ZakazInfo(Z)
                {
                    index = allItems.Count
                };
                zakaz.WriteZakaz += WriteZakaz;
                zakaz.CheckedChange += CheckedChanged;
                zakaz.Width = flpList.ClientSize.Width - zakaz.Margin.Horizontal;
                flpList.Controls.Add(zakaz);
                allItems.Add(zakaz);
            }

            ApplyFilterAndSort();

            ResumeLayout();
        }

        public void WriteZakaz(object sender, ZakazEventArgs e)
        {
            ZAKAZ _z = ZAKAZ_LIST.Items.Find(i => i.ID == e.ID);

            new ZAKAZ_FORM(_z).ShowDialog();
        }

        public void CheckedChanged(object sender, ZakazEventArgs e)
        {
            ZakazInfo _finded = allItems.Find(z => z.INFO.ID == e.ID);

            _finded.Checked = e.CHECKED;

            List<ZakazInfo> temp = allItems.FindAll(z => z.Checked == true);

            CheckedZakazList = temp;

            if (temp.Count > 0)
            {
                lb_count_checked.Text = $"Вибрано: {temp.Count}";
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

        public void ClearAll()
        {
            CheckedZakazList = new List<ZakazInfo>();
            lb_count_checked.Text = "Вибрано: 0";
            lb_count_checked.Visible = false;
            allItems.Clear();
            ApplyFilterAndSort();
        }

        public IEnumerable<ZakazInfo> Items => allItems;

        private void ApplyFilterAndSort()
        {
            string search = txtSearch.Text.Trim().ToLower();
            string sortBy = cbSortBy.SelectedItem?.ToString() ?? "Date";
            bool ascending = cbSortOrder.SelectedIndex == 1;

            // Фільтрація

            IEnumerable<ZakazInfo> filtered;

            if (search != string.Empty)
            {
                filtered = allItems.Where(item =>
                item.INFO.NUMBER.ToString().Contains(search) ||
                item.INFO.CLIENT_NAME.ToLower().Contains(search) ||
                item.INFO.PHONE.ToLower().Contains(search) ||
                item.INFO.KOMPLEKT.Find(k => item.GetNameAndType(k.service_ID, k.type_ID).ToLower().Contains(search)) != null
               );
            }
            else
            {
                filtered = allItems;
            }

                        // Сортування
            if (sortBy == "Type")
            {
                if (ascending)
                    filtered = filtered.OrderBy(i => i.TYPE).ThenBy(i => i.DATE);
                else
                    filtered = filtered.OrderByDescending(i => i.TYPE).ThenByDescending(i => i.DATE);
            }
            else if (sortBy == "Date")
            {
                if (ascending)
                    filtered = filtered.OrderBy(i => i.DATE);
                else
                    filtered = filtered.OrderByDescending(i => i.DATE);
            }
            // else залишаємо як є — не сортуємо


            // Відображення
            flpList.SuspendLayout();
            flpList.Controls.Clear();

            int idx = 1;
            foreach (var item in filtered)
            {
                item.index = idx++;
                flpList.Controls.Add(item);
            }

            flpList.ResumeLayout();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilterAndSort();
        }

        private void cbSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilterAndSort();
        }

        private void cbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilterAndSort();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cbSortBy.SelectedIndex = 0;
            cbSortOrder.SelectedIndex = 0;

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
                    MessageBox.Show(ex.ToString(), "Попередження!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimeSort.Value = DateTime.Now.AddHours(-12);
                }
            }

            ApplyFilterAndSort();
        }

        JSON_ZAKAZ_LIST GET_LIST(Z_STATUS status = Z_STATUS.NEW)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_get_list";
            string postData = $"limit=50&status={(byte)status}";

            if (status == Z_STATUS.NEW)
            {
                DateTime _d = dateTimeSort.Value;

                postData += $"&afterDate={_d.Year}-{_d.Month.ToString().PadLeft(2, '0')}-{_d.Day.ToString().PadLeft(2, '0')}";
            }else if (status == Z_STATUS.ARCHIVE)
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

        private void dateTimeSort_ValueChanged(object sender, EventArgs e)
        {
            if (PAUSE_JSON_QERY) return;
            REFRESH_LIST_WITH_DELAY(500);
        }

        private async void REFRESH_LIST_WITH_DELAY(int milisec = 1000)
        {
            await Task.Delay(milisec);
            REFRESH_LIST(_status);
        }

    }

    public class JSON_ZAKAZ_LIST {
        public List<ZAKAZ> Items { get; set; }
        public int Count_NEW { get; set; }
        public int Count_ACTIVE { get; set; }
        public int Count_ARCHIVE { get; set; }

        public int CurrentCount
        {
            get
            {
                return Items.Count;
            }
        }

        public void Add(ZAKAZ _input)
        {
            Items.Add(_input);
        }
    }

    public class ZAKAZ
    {
        public int ID { get;  set; }
        public Z_TYPE TYPE { get; set; }
        public int? NUMBER { get;  set; }
        public DateTime DATE_IN { get;  set; }
        public DateTime DATE_MAX { get;  set; }
        public DateTime? DATE_OUT { get;  set; }
        public string PHONE { get;  set; }
        public string CLIENT_NAME { get;  set; }
        public string REQV { get;  set; }
        public string TTN_IN { get;  set; }
        public string TTN_OUT { get;  set; }
        public string COMM { get;  set; }
        public byte? DISCOUNT { get;  set; }
        public byte CALLBACK { get;  set; }
        public string WORKER { get;  set; }
        public string REDAKTOR { get;  set; }
        public bool TERMINOVO { get;  set; }
        public Z_STATUS STATUS { get;  set; }
        public List<KOMPLEKT> KOMPLEKT { get;  set; }
        public float SUM
        {
            get
            {
                float ans = 0;
                foreach (KOMPLEKT l in KOMPLEKT)
                {
                    ans += l.costs;
                }
                return ans;
            }
        }
    }

    public class KOMPLEKT
    {
        public int service_ID { get;  set; }
        public int type_ID { get;  set; }
        public int? color { get;  set; }
        public int count { get;  set; }
        public float costs { get;  set; }
    }

    public enum Z_TYPE
    {
        CONVERSION = 0,
        SOLD = 1
    }
}
