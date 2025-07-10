using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class Form1 : Form
    {
        JsonAnswer CURRENT_INFO = null;
        int currentCountLines = 50;
        public bool SetFileReaded = false;
        public bool FindSelectedTextApply = false;
        string SetInfo;

        #region СТИЛІ

        TextStyle dateTimeStyle = new TextStyle(Brushes.Brown, Brushes.BurlyWood, FontStyle.Regular);
        TextStyle recentDateStyle = new TextStyle(Brushes.Red, Brushes.Yellow, FontStyle.Bold);
        TextStyle quotesStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle pathStyle = new TextStyle(Brushes.BlueViolet, null, FontStyle.Underline);
        TextStyle funcStyle = new TextStyle(Brushes.DarkRed, null, FontStyle.Bold);
        TextStyle fileNameStyle = new TextStyle(Brushes.ForestGreen, null, FontStyle.Bold | FontStyle.Italic);
        TextStyle varStyle = new TextStyle(Brushes.BlueViolet, Brushes.White, FontStyle.Bold);
        TextStyle digStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle warningStyle = new TextStyle(Brushes.Red, Brushes.Yellow, FontStyle.Bold);
        TextStyle fatalErrStyle = new TextStyle(Brushes.White, Brushes.Red, FontStyle.Bold);
        TextStyle stackTraceStyle = new TextStyle(null, Brushes.White, FontStyle.Italic | FontStyle.Underline);

        TextStyle findStyle = new TextStyle(Brushes.Yellow, Brushes.DarkBlue, FontStyle.Bold);

        private void SET_STYLE()
        {
            // Потім динамічне виділення свіжих дат
            var regex = new Regex(@"\b\d{2}\.\d{2}\.\d{2} \d{2}:\d{2}:\d{2}\b");

            foreach (Match match in regex.Matches(log_text.Text))
            {
                if (DateTime.TryParseExact(match.Value, "dd.MM.yy HH:mm:ss", CultureInfo.InvariantCulture,
                                            DateTimeStyles.None, out DateTime logTime))
                {
                    if (logTime >= DateTime.Now.AddHours(-2))
                    {
                        var startPlace = log_text.PositionToPlace(match.Index);
                        var endPlace = log_text.PositionToPlace(match.Index + match.Length);
                        var matchRange = new Range(log_text, startPlace, endPlace);

                        // Очистити попередній стиль, якщо був
                        //matchRange.ClearStyle(dateTimeStyle);

                        // І тільки після цього — задати новий стиль
                        matchRange.SetStyle(recentDateStyle);
                    }
                }
            }

            // Спочатку всі загальні стилі
            log_text.Range.SetStyle(dateTimeStyle, @"\b\d{2}\.\d{2}\.\d{2} \d{2}:\d{2}:\d{2}\b");
            log_text.Range.SetStyle(quotesStyle, "\".*?\"");
            log_text.Range.SetStyle(fileNameStyle, @"(?<=\/)[^\/\s]+\.[a-zA-Z0-9]+");
            log_text.Range.SetStyle(varStyle, @"\$\w+");
            log_text.Range.SetStyle(pathStyle, @"([a-zA-Z]:)?([\/\\][\w\-.]+)+(\.\w+)?");
            log_text.Range.SetStyle(funcStyle, @"\b\w+\s*\(.*?\)");
            log_text.Range.SetStyle(digStyle, @"\d");
            log_text.Range.SetStyle(warningStyle, @"Warning:");
            log_text.Range.SetStyle(fatalErrStyle, @"Fatal error:");
            log_text.Range.SetStyle(stackTraceStyle, @"Stack trace:");
        }

        #endregion

        public Form1()
        {
            InitializeComponent();

            minDateBar.MaxDate = DateTime.Now;
            minDateBar.Value = DateTime.Now.AddDays(-3);

            listFolders.SelectedIndexChanged += (s, e) => {
                title.Text = GetPathFromList();
            };

            FindOnLogTextBox.KeyUp += (s, e) => {
                //log_text.Range.ClearStyle(findStyle);
                //log_text.Range.SetStyle(findStyle, FindOnLogTextBox.Text.TrimStart(), RegexOptions.IgnoreCase);
                List<int> _ids = new List<int>();
                _ids = log_text.FindLines(FindOnLogTextBox.Text.TrimStart(), RegexOptions.IgnoreCase);

                if (_ids.Count > 0)
                {
                    log_text.SetSelectedLine(_ids[0]);
                }

            };

            rb50.CheckedChanged += (s, e) => { if (rb50.Checked) AppendTextBox(50); };
            rb100.CheckedChanged += (s, e) => { if (rb100.Checked) AppendTextBox(100); };
            rb200.CheckedChanged += (s, e) => { if (rb200.Checked) AppendTextBox(200); };
            rbAll.CheckedChanged += (s, e) => { if (rbAll.Checked) AppendTextBox(1000); };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "File name: error_log";

            if (!SetFileReaded)
            {
                string setPath = Path.Combine(Application.StartupPath, "setting.ini");

                if (!File.Exists(setPath))
                {
                    FileStream _f = File.Create(setPath);

                    StreamWriter _writer = new StreamWriter(_f);

                    _writer.WriteLine("Substitution:{Word to change1,OutWord1,Word to change2,OutWord2}");

                    _writer.WriteLine("ColorSet[index 0-1-2... ]:{StartStringOrChar,EndStringOrChar,FontColor,BackGroundColor}");

                    _writer.WriteLine("ColorSet[exampleIndex]:{[,],#ff000000,deffault}");

                    _writer.Close();

                    Process.Start("notepad.exe", setPath);

                    Application.Exit();
                }
                else
                {
                    StreamReader _reader = new StreamReader(setPath);

                    SetInfo = _reader.ReadToEnd();

                    _reader.Close();

                    SetFileReaded = true;
                }

            }

            REFRESH();
        }

        void REFRESH(bool NEW_REQUEST = false)
        {
            if (CURRENT_INFO == null || NEW_REQUEST) {
                log_text.Clear();
                CURRENT_INFO = JSON_ANSWER(); 
            }

            string lt = "";

            if (listFolders.SelectedIndex > -1)
            {
                lt = GetPathFromList();
            }

            List<string> _dirList = new List<string>();

            foreach (var pth in CURRENT_INFO.answer)
            {
                if (pth.lines.Count > 0) _dirList.Add(pth.lastDate + " # " + pth.path);
            }

            setDirList(_dirList.ToArray(), lt);
        }

        string ReadSetting(string setName, bool _trim = true)
        {
            int ind = SetInfo.IndexOf(setName + ":");

            if (ind == -1) return "";

            int ind1 = SetInfo.IndexOf("{", ind) + 1;
            int ind2 = SetInfo.IndexOf("}", ind);

            string _OUT = SetInfo.Substring(ind1, ind2-ind1);

            if (_trim) _OUT = _OUT.Trim();

            return _OUT;
        }

        void setDirList(string[] _dirs, string _select = "")
        {
            listFolders.Items.Clear();

            foreach (string _dir in _dirs)
            {
                listFolders.Items.Add(_dir);
            }

            if (_select != "")
            {
                for (int i = 0; i < listFolders.Items.Count; i++)
                {
                    if (listFolders.Items.Count == 1 || listFolders.Items[i].ToString().IndexOf(_select) > -1)
                    {
                        listFolders.SelectedIndex = i;
                        return;
                    }
                }
            }
            else if (listFolders.Items.Count > 0 && listFolders.SelectedIndex < 0)
            {
                listFolders.SelectedIndex = 0;
            }
        }

        JsonAnswer JSON_ANSWER(int _count = 50, string _path = "")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string _minDate = "";

            if (cbByMinDate.Checked)
            {
                _minDate = minDateBar.Value.ToShortDateString();
            }

            string url = "https://sholompromax.com/ii/error_log";
            string postData = $"count={_count}&path={_path}&minDate={_minDate}";

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
            return serializer.Deserialize<JsonAnswer>(responseText);
        }

        private void AppendTextBox(int count = 0)
        {
            if (currentCountLines != count)
            {
                CURRENT_INFO = JSON_ANSWER(count);
            }

            currentCountLines = count;

            setLogText(GetPathFromList());
        }

        string GetPathFromList()
        {
            string lt = listFolders.SelectedItem.ToString();

            return lt.Split(new char[] { '#' }).Length > 1 ? lt.Split(new char[] { '#' })[1].Trim() : lt;
        }

        void setLogText(string ftpPath)
        {
            log_text.Clear();

            try
            {
                foreach(AnswerEntry pth in CURRENT_INFO.answer)
                {
                    if (pth.path == ftpPath)
                    {
                        for (int i = 0; i < pth.lines.Count; i++)
                        {
                            log_text.AppendText(DateFormatToUkr(pth.lines[i]) + ((i + 1) < pth.lines.Count ? "\n" : ""));
                        }

                        log_text.GoEnd();

                        SET_STYLE();
                    }
                }
            }
            catch (Exception ex)
            {
                log_text.Text = ex.ToString();
                log_text.Visible = true;
                return;
            }

        }

        private void listFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupRadioCount.Enabled = true;
            AppendTextBox(currentCountLines);
        }

        string DateFormatToUkr(string _in)
        {
            string OUT = _in;

            int _startInd = _in.IndexOf('['), _endInd = _in.IndexOf(']');

            if (_startInd >= 0 && _endInd >= 0)
            {
                OUT = _in.Substring(_startInd + 1, _in.LastIndexOf(" ", _endInd) - 1 - _startInd);

                try
                {
                    OUT = Convert.ToDateTime(OUT).ToString("dd.MM.yy HH:mm:ss", new CultureInfo("uk-UA"));
                }
                catch { 

                }

                OUT += " " + _in.Substring(_endInd + 1);
            }

            return OUT;
        }

        private void Find(string _text)
        {

        }

        private void butSettingOpen_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", Path.Combine(Application.StartupPath, "setting.ini"));
        }

        private void butRefrSet_Click(object sender, EventArgs e)
        {
            StreamReader _reader = new StreamReader(Path.Combine(Application.StartupPath, "setting.ini"));

            SetInfo = _reader.ReadToEnd();

            _reader.Close();
        }

        private void clear_but_Click(object sender, EventArgs e)
        {
            string lt = GetPathFromList();

            if (DialogResult.Yes != MessageBox.Show(
                 $"Видалити логи: {lt}?", "ОЧИСТКА", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk
                )) return;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/error_log";
            string postData = $"path={lt}&clear=true";

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
            JsonCleareInfo ans = serializer.Deserialize<JsonCleareInfo>(responseText);

            REFRESH(true);

            MessageBox.Show(ans.message);
        }

        private void log_text_Load(object sender, EventArgs e)
        {

        }

        private void cbByMinDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbByMinDate.Checked)
            {
                minDateBar.Enabled = true;
            }
            else
            {
                minDateBar.Enabled = false;
            }

            REFRESH(true);
        }

        private void minDateBar_ValueChanged(object sender, EventArgs e)
        {
            REFRESH(true);
        }

        private void FindOnLogTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class JsonAnswer
    {
        public int count_line { get; set; }
        public List<AnswerEntry> answer { get; set; }
    }

    public class JsonCleareInfo
    {
        public int status { get; set; }
        public string message { get; set; }
    }

    public class AnswerEntry
    {
        public string lastDate { get; set; }
        public string path { get; set; }
        public List<string> lines { get; set; }
    }

    public class ColorLogInfo
    {
        public string StartChar;
        public string EndChar = "";
        public Color FontColor = Color.Empty;
        public Color BackGroundColor = Color.Empty;

        public ColorLogInfo(string sc, string ec = "", Color? fntc = null, Color? bckc = null)
        {
            StartChar = sc;

            if (ec != string.Empty) EndChar = ec; else EndChar = sc;

            if (fntc != null) FontColor = (Color)fntc;
            if (bckc != null) BackGroundColor = (Color)bckc;
        }

        public ColorLogInfo(string sc, Color fntc)
        {
            StartChar = sc;
            EndChar = sc;
            FontColor = fntc;
        }

        public ColorLogInfo(string[] SetLine)
        {
            try
            {
                if (SetLine[0].IndexOf(' ') > 0 || SetLine[1].IndexOf(' ') > 0)
                {
                    StartChar = "";
                }

                StartChar = SetLine[0];

                if (SetLine[1] == "" || SetLine[1].Trim() == "deffault")
                {
                    EndChar = StartChar;
                }
                else
                {
                    EndChar = SetLine[1];
                }

                if (SetLine[2].Trim() != string.Empty && SetLine[2].Trim() != "deffault")
                {
                    FontColor = Color.FromName(ColorTranslator.FromHtml(SetLine[2].Trim()).Name);
                }

                if (SetLine[3].Trim() != string.Empty && SetLine[3].Trim() != "deffault")
                {
                    BackGroundColor = Color.FromName(ColorTranslator.FromHtml(SetLine[3].Trim()).Name);
                }
            }
            catch
            {

            }
        }
    }
}




