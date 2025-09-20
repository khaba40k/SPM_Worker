using Newtonsoft.Json;
using SPM_Core.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SPM_Core
{
    public static class AppFonts
    {
        private static PrivateFontCollection privateFonts;

        static AppFonts()
        {
            privateFonts = new PrivateFontCollection();

            //LogoFont
            byte[] fontData = Properties.Resources.AMAZOOSTROVFINE;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            privateFonts.AddMemoryFont(fontPtr, fontData.Length);
            Marshal.FreeCoTaskMem(fontPtr);

            //DigitFont
            fontData = Properties.Resources.ONYX;
            fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            privateFonts.AddMemoryFont(fontPtr, fontData.Length);
            Marshal.FreeCoTaskMem(fontPtr);

            //TextFont
            fontData = Properties.Resources.bahnschrift;
            fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            privateFonts.AddMemoryFont(fontPtr, fontData.Length);
            Marshal.FreeCoTaskMem(fontPtr);
        }

        public static Font LogoFont(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font(privateFonts.Families[0], size, style);
        }

        public static Font DigitFont(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font(privateFonts.Families[13], size, style);
        }

        public static Font TextFont(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font(privateFonts.Families[1], size, style);
        }
    }

    public static class TEXT_TO
    {
        public static float _FLOAT(string _inp)
        {
            string _out = _inp.Replace(".", ",");
            _out = _inp.Replace(" ", "");

            if (float.TryParse(_out, out float ans))
            {
                return ans;
            }

            return 0f;
        }
    }

    public static class CustomMessage
    {
        private static SoundPlayer player = new SoundPlayer();

        public static DialogResult Show(string mes, string _custButText = "ОК")
        {
            return Show(mes, "", MessageBoxButtons.OK, MessageBoxIcon.None, new MessButText { OK = _custButText });
        }
        public static DialogResult Show(string mes, string capt, string _custButText = "ОК")
        {
            return Show(mes, capt, MessageBoxButtons.OK, MessageBoxIcon.None, new MessButText { OK = _custButText });
        }
        public static DialogResult Show(string mes, MessageBoxIcon _icon, string _custButText = "ОК")
        {
            return Show(mes, "", MessageBoxButtons.OK, _icon, new MessButText { OK = _custButText });
        }
        public static DialogResult Show(string mes, string capt, MessageBoxIcon _icon, string _custButText = "ОК")
        {
            return Show(mes, capt, MessageBoxButtons.OK, _icon, new MessButText { OK = _custButText });
        }
        public static DialogResult Show(string mes, MessageBoxButtons _butt, MessButText _custButText = null)
        {
            return Show(mes, "", _butt, MessageBoxIcon.None, _custButText);
        }
        public static DialogResult Show(string mes, string capt, MessageBoxButtons _butt, MessButText _custButText = null)
        {
            return Show(mes, capt, _butt, MessageBoxIcon.None, _custButText);
        }
        public static DialogResult Show(string mes, 
            string caption, 
            MessageBoxButtons _buttons = MessageBoxButtons.OK,
            MessageBoxIcon _icon = MessageBoxIcon.None,
            MessButText _customButtonText = null)
        {
            using (Form customMessage = new Form())
            using (Font _fontConsolas = new Font("Consolas", 9, FontStyle.Bold))
            {
                customMessage.Text = !string.IsNullOrWhiteSpace(caption) ? caption : "SholomProMax";
                customMessage.AutoSize = false;
                customMessage.ShowIcon = false;
                customMessage.FormBorderStyle = FormBorderStyle.FixedDialog;
                customMessage.MaximizeBox = false;
                customMessage.MinimizeBox = false;
                customMessage.BackColor = Color.White;
                customMessage.ShowInTaskbar = false;

                string[] lines = mes.Trim().Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

                lines = CorrectLines(lines);

                int padding = 10;
                int maxWidth = 0;
                int lineHeight = 0;
                const int BUTTON_HEIGHT = 30;
                const int MINIMUM_FORM_WIDTH = 200;

                customMessage.StartPosition = FormStartPosition.CenterParent;

                foreach (string line in lines)
                {
                    Size size = TextRenderer.MeasureText(line, _fontConsolas);
                    if (size.Width > maxWidth) maxWidth = size.Width > MINIMUM_FORM_WIDTH ? size.Width : MINIMUM_FORM_WIDTH;
                    if (size.Height > lineHeight) lineHeight = size.Height;
                }

                int totalHeight = lines.Length * (lineHeight) + (padding * 2) + BUTTON_HEIGHT;
                customMessage.ClientSize = new Size(maxWidth + padding * 2, totalHeight);

                // Малюємо у Paint
                customMessage.Paint += (s, e) =>
                {
                    using (Brush semi = new SolidBrush(Color.FromArgb(128, Color.AntiqueWhite)))
                    {
                        e.Graphics.FillRectangle(semi, new Rectangle(0, 0, customMessage.ClientSize.Width, customMessage.ClientSize.Height));
                    }

                    int y = padding;

                    foreach (string line in lines)
                    {
                        Rectangle rect = new Rectangle(padding, y, maxWidth, lineHeight);
                        TextRenderer.DrawText(e.Graphics, line, _fontConsolas, rect, Color.Black,
                            TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                        y += lineHeight;
                    }
                };

                //customMessage.ClientSize += new Size(0, ButtonLine);

                int buttoncount = 2;

                switch (_buttons)
                {
                    case MessageBoxButtons.OK:
                        buttoncount = 1;
                        break;
                    case MessageBoxButtons.AbortRetryIgnore:
                    case MessageBoxButtons.YesNoCancel:
                        buttoncount = 3;
                        break;
                }

                MessButText butText = _customButtonText ?? new MessButText();

                string _butText = GetButtonText(_buttons, butText, 0, out DialogResult _dr);
                int _butWidth = TextRenderer.MeasureText(_butText, _fontConsolas).Width + padding;

                customMessage.MinimumSize = new Size(150, totalHeight + 50);

                customMessage.ControlAdded += (s, c) =>
                {
                    if (c.Control is Button){
                        Button _b = (Button)c.Control;
                        _b.Font = _fontConsolas;
                        _b.TabStop = false;
                        _b.Margin = new Padding(0, 0, padding, padding);
                        _b.Padding = new Padding(3, 4, 3, 4);
                        _b.AutoSize = true;
                        _b.AutoSizeMode = AutoSizeMode.GrowOnly;
                        _b.Cursor = Cursors.Hand;
                        _b.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                        _b.Height = BUTTON_HEIGHT;
                        _b.SizeChanged += (b, e) =>
                        {
                            Button bb = (Button)b;
                            customMessage.MinimumSize += new Size(bb.Width, 0);
                        };
                    }
                };


                Button but0 = new Button
                {
                    Text = _butText,
                    DialogResult = _dr,
                    Width = _butWidth,
                    Location = new Point((maxWidth > MINIMUM_FORM_WIDTH ? maxWidth : MINIMUM_FORM_WIDTH) - _butWidth,
                                   totalHeight - BUTTON_HEIGHT),
                };

                customMessage.Controls.Add(but0);

                if (buttoncount > 1)
                {
                    _butText = GetButtonText(_buttons, butText, 1, out _dr);
                    _butWidth = TextRenderer.MeasureText(_butText, _fontConsolas).Width + padding;

                    Button but1 = new Button
                    {
                        Text = _butText,
                        DialogResult = _dr,
                        Width = _butWidth,
                        Location = new Point((maxWidth > MINIMUM_FORM_WIDTH ? maxWidth : MINIMUM_FORM_WIDTH) - but0.Width - _butWidth - padding,
                                       totalHeight - BUTTON_HEIGHT),
                    };

                    customMessage.Controls.Add(but1);

                    if (buttoncount > 2)
                    {
                        _butText = GetButtonText(_buttons, butText, 2, out _dr);
                        _butWidth = TextRenderer.MeasureText(_butText, _fontConsolas).Width + padding;

                        Button but2 = new Button
                        {
                            Text = _butText,
                            DialogResult = _dr,
                            Width = _butWidth,
                            Location = new Point((maxWidth > MINIMUM_FORM_WIDTH ? maxWidth : MINIMUM_FORM_WIDTH) - but0.Width - but1.Width - _butWidth - (padding * 2), 
                                           totalHeight - BUTTON_HEIGHT),
                        };

                        customMessage.Controls.Add(but2);
                    }
                }

                int allButWidth = padding;

                foreach (Control _c in customMessage.Controls)
                {
                    if (_c is Button)
                    {
                        Button _b = (Button)_c;

                        if (_b.DialogResult == DialogResult.OK
                            || _b.DialogResult == DialogResult.Yes
                            || _b.DialogResult == DialogResult.Retry)
                        {
                            _b.Font = new Font(_b.Font, FontStyle.Bold);
                            _b.Text = _b.Text.ToUpper();
                            _b.ForeColor = Color.Blue;
                            customMessage.AcceptButton = _b;
                        }

                        allButWidth += (_b.Width + _b.Margin.Horizontal);
                    }
                }

                allButWidth += padding;

                if (customMessage.ClientSize.Width < allButWidth)
                {
                    customMessage.ClientSize += 
                        new Size((allButWidth - customMessage.ClientSize.Width), 0);
                }

                if (_icon != MessageBoxIcon.None)
                {
                    Bitmap bcgrnd;
                    switch (_icon)
                    {
                        case MessageBoxIcon.Stop:
                            bcgrnd = new Bitmap(Resources.stop_2);
                            player.Stream = Resources.audio_error;
                            break;
                        case MessageBoxIcon.Information:
                            bcgrnd = new Bitmap(Resources.property);
                            player.Stream = Resources.audio_information;
                            break;
                        case MessageBoxIcon.Exclamation:
                            bcgrnd = new Bitmap(Resources.attention);
                            player.Stream = Resources.audio_exclamation;
                            break;
                        default:
                            bcgrnd = new Bitmap(Resources.question);
                            player.Stream = Resources.audio_question;
                            break;
                    }

                    player.Play();

                    customMessage.BackgroundImageLayout = ImageLayout.Zoom;
                    customMessage.BackgroundImage = bcgrnd;
                }

                return customMessage.ShowDialog();
            }
        }

        private static string GetButtonText(MessageBoxButtons _but, MessButText _text, int index, out DialogResult _result)
        {
            if (index == 2)
            {
                switch (_but)
                {
                    case MessageBoxButtons.YesNoCancel:
                        _result = DialogResult.Yes;
                        return _text.Yes;
                    case MessageBoxButtons.AbortRetryIgnore:
                        _result = DialogResult.Abort;
                        return _text.Abort;
                }
            } 
            else if (index == 1)
            {
                switch (_but)
                {
                    case MessageBoxButtons.YesNo:
                        _result = DialogResult.Yes;
                        return _text.Yes;
                    case MessageBoxButtons.YesNoCancel:
                        _result = DialogResult.No;
                        return _text.No;
                    case MessageBoxButtons.OKCancel:
                        _result = DialogResult.OK;
                        return _text.OK;
                    case MessageBoxButtons.AbortRetryIgnore:
                    case MessageBoxButtons.RetryCancel:
                        _result = DialogResult.Retry;
                        return _text.Retry;
                }
            }else if (index == 0)
            {
                switch (_but)
                {
                    case MessageBoxButtons.OK:
                        _result = DialogResult.OK;
                        return _text.OK;
                    case MessageBoxButtons.YesNo:
                        _result = DialogResult.No;
                        return _text.No;
                    case MessageBoxButtons.OKCancel:
                    case MessageBoxButtons.RetryCancel:
                    case MessageBoxButtons.YesNoCancel:
                        _result = DialogResult.Cancel;
                        return _text.Cancel;
                    case MessageBoxButtons.AbortRetryIgnore:
                        _result = DialogResult.Ignore;
                        return _text.Ignore;
                }
            }

            _result = DialogResult.None;
            return "Кнопка";
        }

        public static string[] CorrectLines(string[] inputLines)
        {
            const int MIN_CUT_LENGTH = 40;

            if (inputLines == null || inputLines.Length == 0)
                return new string[0];

            int[] lengths = inputLines.Select(s => s?.Length ?? 0).ToArray();
            int maxLength = lengths.Max();

            // Якщо найбільший рядок <= MIN_CUT_LENGTH — повертаємо як є
            if (maxLength <= MIN_CUT_LENGTH)
                return inputLines;

            var groups = lengths.GroupBy(l => l)
                                .Select(g => new { Length = g.Key, Count = g.Count() })
                                .OrderByDescending(g => g.Count)
                                .ThenByDescending(g => g.Length)
                                .ToList();

            int etalonLength;

            if (groups.Count == 0)
                return inputLines;
            else if (groups.Count == 1)
                etalonLength = groups[0].Length;
            else
                etalonLength = (groups[0].Count == groups[1].Count)
                    ? Math.Max(groups[0].Length, groups[1].Length)
                    : groups[0].Length;

            if (groups[0].Count == 1)
            {
                var sortedLengths = lengths.OrderByDescending(l => l).ToArray();
                if (sortedLengths.Length > 1)
                {
                    double ratio = (double)sortedLengths[0] / sortedLengths[1];
                    if (ratio <= 1.3)
                        return inputLines;
                    etalonLength = sortedLengths[1];
                }
                else
                    return inputLines;
            }

            // Якщо etalonLength менша за MIN_CUT_LENGTH, обрізати по MIN_CUT_LENGTH
            int cutLength = etalonLength < MIN_CUT_LENGTH ? MIN_CUT_LENGTH : etalonLength;

            List<string> result = new List<string>();

            foreach (string line in inputLines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    result.Add(line);
                    continue;
                }

                int start = 0;
                while (start < line.Length)
                {
                    int len = Math.Min(cutLength, line.Length - start);
                    string part = line.Substring(start, len).TrimStart();
                    result.Add(part);
                    start += len;
                }
            }

            return result.ToArray();
        }
    }

    public class MessButText
    {
        public string Yes { get; set; } = "Так";
        public string No { get; set; } = "Ні";
        public string OK { get; set; } = "ОК";
        public string Cancel { get; set; } = "Відміна";
        public string Abort { get; set; } = "Назад";
        public string Retry { get; set; } = "Заново";
        public string Ignore { get; set; } = "Ігнорувати";
    }

    /// <summary>
    /// Налаштування та інформація
    /// </summary>
    public static class SERVICE_INFO
    {
        public static string APP_DATA_PATCH = Path.Combine(new string[2] {
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "spm" });
        /// <summary>
        /// Повертає список всіх зв'язок ATR -> ID -> TYPE -> COLOR
        /// </summary>
        public static List<SERVICE_ID_INFO> SERVICE_LIST
        {
            get
            { 
                if (SERVICES != null)
                {
                    return new List<SERVICE_ID_INFO>(SERVICES.INFO);
                }
                else
                {
                    SetServiceInfo();
                    return SERVICE_LIST;
                }
            }
        }

        private static SERVICE_ID_LIST SERVICES;

        public static SERVICE_ALL_STRING_NAMES NAMES
        {
            get
            {
                if (_names != null)
                {
                    return _names;
                }
                else
                {
                    SetServiceAllNames();
                    return _names;
                }
            }
            private set { _names = value; }
        }

        private static SERVICE_ALL_STRING_NAMES _names;

        public static AutorizeInfo AUTORIZE_INFO { get; set; } = null;

        public static string TOKEN => AUTORIZE_INFO?.data.TOKEN;

        /// <summary>
        /// Зв'язка ID -> NAME
        /// </summary>
        /// <returns></returns>
        public static List<ONE_SERVICE_INFO> GetAllServiceNameList(int _atr, bool WithPrice = false)
        {
            List<SERVICE_ID_INFO> _sorted = SERVICE_LIST
                  .FindAll(a => a.ATR > -1
                           && (_atr == 0 || (a.ATR & _atr) == _atr)
                           && (_atr == 0 || (WithPrice ? a.COST > 0 : true)));

            _sorted.Sort((x, y) => y.IS_POSLUGA.CompareTo(x.IS_POSLUGA));

            List<ONE_SERVICE_INFO> _out = _sorted
                  .GroupBy(x => x.ID)
                  .Select(g => new ONE_SERVICE_INFO() { 
                      NAME = g.First().NAME,
                      ID = g.First().ID
                  } )
                  .ToList();

            return _out;
        }
        /// <summary>
        /// Зв'язка TypeID -> TypeName
        /// </summary>
        /// <param name="_id">Service ID</param>
        /// <returns></returns>
        public static List<ONE_TYPE_INFO> GetAllTypeNames(int _id)
        {
            List<ONE_TYPE_INFO> _out = SERVICE_LIST
                .FindAll(x => x.ID == _id && x.TYPE_NAME != null)
                .GroupBy(x => x.TYPE_ID)
                .Select(g => new ONE_TYPE_INFO() 
                {
                    SERV_ID = g.First().ID,
                    TYPE_ID = g.First().TYPE_ID, 
                    NAME = g.First().TYPE_NAME 
                })
                .ToList();

            return _out;
        }
        /// <summary>
        /// Зв'язка ColorID -> ColorName (за параметрами ID->TypeID)
        /// </summary>
        /// <param name="_serviceId"></param>
        /// <param name="_typeId"></param>
        /// <returns></returns>
        public static List<ONE_COLOR_INFO> GetAllColorNames(int _serviceId, int _typeId = 1)
        {
            List<ONE_COLOR_INFO> _out = new List<ONE_COLOR_INFO>();

            List<SERVICE_ID_INFO> _outList = SERVICE_LIST
                .FindAll(c => c.ID == _serviceId
                              && c.TYPE_ID == _typeId
                              && c.COLOR_ID != null);

            foreach (SERVICE_ID_INFO c in _outList)
            {
                _out.Add(new ONE_COLOR_INFO() { 
                    ID = (int)c.COLOR_ID, 
                    NAME = c.COLOR_NAME
                });
            }

            return _out;
        }

        public static Jurnal GetJurnal(int Year, byte Month)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_get_jurnal";
            string postData = $"token={TOKEN}&year={Year}&month={Month}";

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

            return serializer.Deserialize<Jurnal>(responseText);
        }

        public static int GetNextID()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_get_next_id";
            string postData = $"token={TOKEN}";

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

            return Convert.ToInt32(responseText.Trim());
        }

        public static byte GetDiscountByCode(string code)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_get_discount";
            string postData = $"code={code}&token={TOKEN}";

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

            return byte.Parse(string.IsNullOrWhiteSpace(responseText) ? "0" : responseText.Trim());
        }

        public static string GetServiceName(int id)
        {
            if (id < 0) return null;
            ONE_SERVICE_INFO _ans = NAMES.SERVICE_NAMES.Find(n => n.ID == id);
            return _ans?.NAME ?? "";
        }

        public static string GetTypeName(int servId, int typeId)
        {
            if (servId == 19) return GetServiceName(typeId);

            ONE_TYPE_INFO _out = NAMES.TYPE_NAMES.Find(t => t.SERV_ID == servId && t.TYPE_ID == typeId); ;

            return _out?.NAME;
        }

        public static SERVICE_ID_INFO GetService(int _servId, int _typeId = 1, int? _colorId = null)
        {
            return SERVICE_LIST
                .Find(s => s.ID == _servId
                           && s.TYPE_ID == _typeId
                           && s.COLOR_ID == _colorId);
        }

        public static string GetUserName(string login)
        {
            return AUTORIZE_INFO?.data.downUserList.Find(u => u.LOGIN == login)?.NAME ?? login;
        }
        public static int? GetAtr(int servID)
        {
            return SERVICE_LIST.Find(a => a.ID == servID)?.ATR;
        }

        public static bool GetHasColor(int servID, int typeID = 1)
        {
            return SERVICE_LIST.Find(a => a.ID == servID 
            && a.TYPE_ID == typeID && a.COLOR_ID != null) != null;
        }

        public static byte GetOrder(int servID)
        {
            return SERVICE_LIST.Find(a => a.ID == servID)?.ORDER ?? byte.MaxValue;
        }

        public static float GetCost(int _servId, int _typeId = 1)
        {
            SERVICE_ID_INFO _ans = SERVICE_LIST.Find(c => (c.ID == _servId) && (c.TYPE_ID == _typeId));
            return _ans?.GetDeffaultCost() ?? 0f;
        }

        public static string GetColorName(int? id)
        {
            return NAMES.COLOR_NAMES.Find(c => c.ID == id)?.NAME;
        }

        public static Color GetColorByID(int? colorID)
        {
            string CSS_COLOR = NAMES.COLOR_NAMES.Find(c => c.ID == colorID)?.CSS;

            return ColorTranslator.FromHtml(CSS_COLOR ?? "#FFFFFF");
        }

        public static int[] GetAllID(List<SERVICE_ID_INFO> _inputList)
        {
            List<SERVICE_ID_INFO> _temp = _inputList.GroupBy(x => x.ID).Select(v => v.First()).ToList();

            _temp.Sort((x, y) => x.ORDER.CompareTo(y.ORDER));

            return _temp.Select(s => s.ID).ToArray();
        }

        public static List<EXCEL_HISTORY_LINE> GetServHistInfo(int servId, int typeId = 1, int? colorId = null)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/csv_get_service_history";
            string postData = $"service_ID={servId}&type_ID={typeId}&color_ID={colorId}";

            // Створення запиту
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["Authorization"] = "Bearer " + TOKEN;

            // Відправка POST-даних
            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.ContentLength = data.Length;

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

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                return serializer.Deserialize<List<EXCEL_HISTORY_LINE>>(responseText);
            }
            catch
            {
                return null;
            }
        }

        public static void ResetCountCost()
        {
            foreach (SERVICE_ID_INFO _info in SERVICES.INFO)
            {
                _info.GetDeffaultCost(true);
            }
        }
        public static void SetServiceInfo()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_get_serv_info";
            string postData = $"token={TOKEN}";

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

            SERVICES = serializer.Deserialize<SERVICE_ID_LIST>(responseText);
        }

        public static void SetServiceAllNames()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_get_names";
            string postData = $"token={TOKEN}";

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

            NAMES = serializer.Deserialize<SERVICE_ALL_STRING_NAMES>(responseText);
        }

        class ZAKAZ_WITH_TOKEN : ZAKAZ
        {
            public string token { get; private set; }

            public ZAKAZ_WITH_TOKEN(ZAKAZ Z, string T)
            {
                token = T;
                ID = Z.ID;
                TYPE = Z.TYPE;
                NUMBER = Z.NUMBER;
                DATE_IN = Z.DATE_IN;
                DATE_MAX = Z.DATE_MAX;
                DATE_OUT = Z.DATE_OUT;
                PHONE = Z.PHONE;
                CLIENT_NAME = Z.CLIENT_NAME;
                REQV = Z.REQV;
                TTN_IN = Z.TTN_IN;
                TTN_OUT = Z.TTN_OUT;
                COMM = Z.COMM;
                DISCOUNT = Z.DISCOUNT;
                CALLBACK = Z.CALLBACK;
                WORKER = Z.WORKER;
                REDAKTOR = Z.REDAKTOR;
                TERMINOVO = Z.TERMINOVO;
                STATUS = Z.STATUS;
                KOMPLEKT = Z.KOMPLEKT;
            }
        }

        public static bool SAVE_ZAKAZ(ref ZAKAZ Z, out string mes)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ZAKAZ_WITH_TOKEN _upload = new ZAKAZ_WITH_TOKEN(Z, TOKEN);

            string url = "https://sholompromax.com/ii/json_save_zakaz";
            string postData = JsonConvert.SerializeObject(_upload, Formatting.None);

            // Створення запиту
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            // Відправка POST-даних
            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.ContentLength = data.Length;

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

                var jsonAns = new
                {
                    success = false,
                    number = 0,
                    message = ""
                };

                jsonAns = JsonConvert.DeserializeAnonymousType(responseText, jsonAns);

                mes = jsonAns.message;

                if (jsonAns.number != 0)
                {
                    Z.NUMBER = jsonAns.number;
                }

                return jsonAns.success;
            }
            catch (Exception ex)
            {
                mes = ex.Message;

                return false;
            }
        }

        public static bool DELETE_ZAKAZ_BY_ID(string[] idArr, out string mes)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_delete_zakaz";

            string idList = "";

            if (idArr.Length == 1)
            {
                idList = "&id=" + idArr[0];
            }
            else
            {
                foreach (string id in idArr)
                {
                    idList += "&id[]=" + id;
                }
            }

            string postData = $"token={TOKEN}{idList}";

            // Створення запиту
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Відправка POST-даних
            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.ContentLength = data.Length;

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

            var jsonAns = new
            {
                success = false,
                message = ""
            };

            jsonAns = JsonConvert.DeserializeAnonymousType(responseText, jsonAns);

            mes = jsonAns.message;

            return jsonAns.success;
        }

        public static List<SERVICE_ID_INFO> GET_SKLAD(DateTime _date)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_get_sklad";
            string postData = $"date={_date.ToShortDateString()}";

            // Створення запиту
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["Authorization"] = "Bearer " + TOKEN;

            // Відправка POST-даних
            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.ContentLength = data.Length;

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

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                List<SERVICE_ID_INFO> _answer = serializer
                    .Deserialize<List<SERVICE_ID_INFO>>(responseText);

                return _answer;
            }
            catch (Exception ex)
            {
                CustomMessage.Show(ex.Message);
                return null;
            }
        }

        public static bool SAVE_VITRATY(VitratyList _input, out string mes)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_save_vitraty";
            string postData = JsonConvert.SerializeObject(_input, Formatting.None);

            // Створення запиту
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            // Відправка POST-даних
            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.ContentLength = data.Length;

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

                var _answer = new
                {
                    success = false,
                    message = ""
                };

                _answer = JsonConvert.DeserializeAnonymousType(responseText, _answer);

                mes = _answer.message;

                return _answer.success;
            }
            catch (Exception ex)
            {
                mes = ex.Message;

                return false;
            }
        }

        public static bool DELETE_VITRATY_BY_ID(int[] idArr, out string mes)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/json_delete_vitraty";

            string idList = "";

            if (idArr.Length == 1)
            {
                idList = "&id=" + idArr[0];
            }
            else
            {
                foreach (int id in idArr)
                {
                    idList += "&id[]=" + id;
                }
            }

            string postData = $"token={TOKEN}{idList}";

            // Створення запиту
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            // Відправка POST-даних
            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.ContentLength = data.Length;

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

            var _answer = new
            {
                success = false,
                message = ""
            };

            _answer = JsonConvert.DeserializeAnonymousType(responseText, _answer);

            mes = _answer.message;

            return _answer.success;
        }
    }

    public class EXCEL_HISTORY_LINE
    {
        public DateTime DATE { get; set; }
        public string STATUS { get; set; }
        public string COLOR { get; set; }
        public int COUNT { get; set; } = 0;
        public float COSTS { get; set; } = 0f;
        public string COMM { get; set; } = null;
        public string REDAKTOR { get; set; }
    }

    public class VitratyList
    {
        public string token { get; set; }//->REDAKTOR
        public DateTime Date { get; set; } = DateTime.Now;
        public List<VitratyInfo> data { get; set; } = new List<VitratyInfo>();
        public VitratyList(List<SERVICE_ID_INFO> _input)
        {
            data = _input.Select(s => new VitratyInfo(s)).ToList();
        }
        public VitratyList(List<VitratyInfo> _input)
        {
            data = _input;
        }
        public override string ToString()
        {
            if (data == null || data.Count < 0) return "";

            // Н з/п // Назва // Колір // Кількість* // Ціна // Коментар*

            int[] MaxWidth = new int[] { 2, 35, 12, 3, 10, 25 };

            int[] ColumnWidth = new int[] { 2, 0, 0, 3, 0, 0 };

            float _sum = 0f;

            foreach (VitratyInfo _inf in data)
            {
                int _CellWidth = (_inf.NAME + (_inf.TYPE_NAME != null ?
                    " (" + _inf.TYPE_NAME + ")" : "")).Length;

                if (_CellWidth > ColumnWidth[1]) ColumnWidth[1] = _CellWidth;

                _CellWidth = (_inf.COLOR_NAME ?? "").Length;

                if (_CellWidth > ColumnWidth[2]) ColumnWidth[2] = _CellWidth;

                _CellWidth = _inf.COUNT.ToString().Length;

                if (_CellWidth > ColumnWidth[3]) ColumnWidth[3] = _CellWidth;

                _CellWidth = _inf.COST.ToString().Length;

                if (_CellWidth > ColumnWidth[4]) ColumnWidth[4] = _CellWidth;

                _CellWidth = _inf.COMM?.Length ?? 0;

                if (_CellWidth > ColumnWidth[5]) ColumnWidth[5] = _CellWidth;

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

            int _counter = 1;

            string[] _val;

            foreach (VitratyInfo _data in data)
            {
                _val = new string[] {
                    _counter++.ToString(),
                    _data.NAME + (_data.TYPE_NAME != null ? $" ({_data.TYPE_NAME})":""),
                    _data.COLOR_NAME ?? "",
                    _data.COUNT.ToString(),
                    _data.COST.ToString(),
                    _data.COMM ?? ""
                };

                _out += GetRow(ColumnWidth, _val);
            }

            if (_counter > 2)
            {
                _val = new string[] { "", "", "", "", _sum.ToString(), "" };

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

    public class VitratyInfo : SERVICE_ID_INFO
    {
        public VitratyInfo(SERVICE_ID_INFO _base, string comm = null, bool spis = false)
        {
            ID = _base.ID;
            TYPE_ID = _base.TYPE_ID;
            COLOR_ID = _base.COLOR_ID;
            COST = _base.COST;
            COUNT = _base.COUNT;
            COMM = comm;
            SPISANNJA = spis;
        }
        public VitratyInfo()
        {
            ID = -1;
            TYPE_ID = 1;
            COLOR_ID = null;
        }
        public string COMM { get; set; } = null;
        public bool SPISANNJA { get; set; } = false;
    }

    public class SERVICE_ALL_STRING_NAMES
    {
        public List<ONE_SERVICE_INFO> SERVICE_NAMES { get; set; }
        public List<ONE_TYPE_INFO> TYPE_NAMES { get; set; }
        public List<ONE_COLOR_INFO> COLOR_NAMES { get; set; }
    }

    public class ONE_SERVICE_INFO
    {
        public static implicit operator ONE_SERVICE_INFO(string _other)
        {
            return new ONE_SERVICE_INFO
            {
                NAME = _other
            };
        }
        public int? ID { get; set; } = -1;
        public string NAME { get; set; } = "";
        public override string ToString()
        {
            return NAME?.ToUpper() ?? "";
        }
    }

    public class ONE_COLOR_INFO : ONE_SERVICE_INFO
    {
        public static implicit operator ONE_COLOR_INFO(string _other)
        {
            return new ONE_COLOR_INFO
            {
                NAME = _other
            };
        }
        public string CSS
        {
            get => _css ?? SetCSS(); set
            {
                _css = value;
            }
        }

        private string _css = null;

        public Color COLOR => SERVICE_INFO.GetColorByID(ID);

        public string SetCSS()
        {
            _css = SERVICE_INFO.NAMES.COLOR_NAMES.Find(c => c.ID == ID)?.CSS;

            return _css;
        }

        private bool Equals(ONE_COLOR_INFO _other)
        {
            if (_other == null) return false;

            return ID == _other.ID;
        }

        public override bool Equals(object obj) => Equals(obj as ONE_COLOR_INFO);

        public override int GetHashCode() => ID ?? 100;
    }

    public class ONE_TYPE_INFO
    {
        public static implicit operator ONE_TYPE_INFO(string _other)
        {
            return new ONE_TYPE_INFO{
                 NAME = _other
            };
        }
        public int SERV_ID { get; set; } = -1;
        public int TYPE_ID { get; set; } = 1;
        public string NAME { get; set; }
        public override string ToString()
        {
            return NAME;
        }
    }

    public class AutorizeInfo
    {
        public bool success { get; set; } = false;
        public AutorizeData data { get; set; } = null;
        public string message { get; set; } = "";
    }

    public class AutorizeData
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string TOKEN { get; set; }
        public string LOGIN { get; set; }
        public List<UserInfo> downUserList { get; set; }
        public string[] workerInfo { get; set; }
    }

    public class UserInfo
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string LOGIN { get; set; }
        public override string ToString()
        {
            return NAME;
        }
    }

    public class Jurnal
    {
        public bool success { get; set; } = false;
        public List<JurnalRecord> data { get; set; } = new List<JurnalRecord>();

        public List<JurnalRecord> GroupByDayRed(byte day, string redaktor)
        {
            List<JurnalRecord> _out = new List<JurnalRecord>();

            _out = data.Where(j => j.DATE.Day == day && j.REDAKTOR == redaktor).ToList();

            return _out;
        }

        public string[] GetRedaktors()
        {
            string[] _out = data.Select(r => r.REDAKTOR).Distinct().ToArray();

            return _out;
        }
    }

    public class JurnalRecord
    {
        public int ID { get; set; }
        public DateTime DATE { get; set; }
        public int SERVICE_ID { get; set; }
        public string SERVICE_NAME => SERVICE_INFO.GetServiceName(SERVICE_ID);
        public byte TYPE_ID { get; set; } = 1;
        public string TYPE_NAME => SERVICE_INFO.GetTypeName(SERVICE_ID, TYPE_ID);
        public byte? COLOR_ID { get; set; }
        public string COLOR_NAME => SERVICE_INFO.GetColorName(COLOR_ID);
        public int COUNT { get; set; } = 1;
        public float COSTS { get; set; }
        public string COMM { get; set; } = null;
        public string REDAKTOR { get; set; }
    }

    public partial class TitleTextBox : TextBox
    {
        private Label _titleLbl = new Label() { 
            BackColor = Color.Transparent,
            ForeColor = Color.Gray,
            AutoSize = true,
            Location = new Point(2, 0),
            Padding = new Padding(0),
            Margin = new Padding(0)
        };

        public TitleTextBox() {

            _titleLbl.Enter += _titleLbl_Enter;

            Enter += TitleTextBox_Enter; ;
            Leave += TitleTextBox_Leave;

            TextChanged += TitleTextBox_TextChanged;

            Controls.Add(_titleLbl);
        }

        private void TitleTextBox_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Text)) { HideTitle(); }
        }

        private void TitleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Text)) 
            { 
                ShowTitle();
            }
            else
            {
                HideTitle();
            }
        }

        private void TitleTextBox_Leave(object sender, EventArgs e)
        {
            ShowTitle();
        }

        private void _titleLbl_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Text)) { HideTitle(); }

            Focus();
        }

        private void HideTitle()
        {
            _titleLbl.Visible = false;
            
        }

        private void ShowTitle()
        {
            _titleLbl.Visible = true;
        }

        // Властивості
        [Browsable(true)]
        public string Title {
            get => _titleLbl.Text;
            set => _titleLbl.Text = value;
        }
    }


    public class CheckComboBox : UserControl
    {
        private CheckedListBox _listBox = new CheckedListBox();
        private event EventHandler CloseDropDown;
        public event EventHandler TagsChanged;
        private Form dd = new Form();
        private bool DroppedDown { get => _droppedDown; set {

                if (_droppedDown == value) return;

                _droppedDown = value;

                dd.Size = new Size(Width * 2,
                               _listBox.PreferredHeight);

                if (_droppedDown)
                {
                    dd.Location = PointToScreen(new Point(0, Height));
                    dd.Show();
                }
                else
                {
                    CloseDropDown?.Invoke(this, EventArgs.Empty);
                }
            } }

        private bool _droppedDown = false;

        public CheckComboBoxItemList Items { get; set; } = new CheckComboBoxItemList();
        private List<string> UncheckedTagMemory = new List<string>();
        public string[] CheckedTags => Items.Items.FindAll(f => f.Checked).Select(i => i.Tag).ToArray();
        public string[] UnCheckedTags => Items.Items.FindAll(f => !f.Checked).Select(i => i.Tag).ToArray();

        private bool IgnoreCheckChange = false;

        public CheckComboBox()
        {
            BackColor = SystemColors.Control;
            Cursor = Cursors.Hand;
            Font = AppFonts.TextFont(14);
            ForeColor = Color.Blue;

            dd.Font = Font;
            dd.ForeColor = ForeColor;

            _listBox.Dock = DockStyle.Fill;
            _listBox.SelectionMode = SelectionMode.One;
            _listBox.CheckOnClick = true;
            _listBox.Margin = new Padding(0);
            
            //_listBox.Font = Font;

            dd.Padding = new Padding(0);
            dd.FormBorderStyle = FormBorderStyle.None;

            Items.ControlsCleared += (s, e) =>
            {
                _listBox.Items.Clear();
                dd.Height = 0;
            };

            _listBox.MouseLeave += (s, e) =>
            {
                dd?.Hide();
                _droppedDown = false;
            };

            _listBox.ControlAdded += (s, e) =>
            {
                CheckBox _cb = e.Control as CheckBox;

                _cb.Checked = true;

                dd.Height += e.Control.Height;
            };

            CloseDropDown += (s, e) => { dd?.Hide(); };

            dd.Shown += (s, e) =>
            {
                dd.Size = new Size(Width * 2,
                               _listBox.PreferredHeight);
                dd.Location = PointToScreen(new Point(0, Height));
            };

            dd.Controls.Add(_listBox);

            Click += ClickOnHead;

            Items.ControlAdded += AddCheckBox;

            _listBox.ItemCheck += _listBox_ItemCheck;
        }

        private void RememberFilters()
        {
            foreach (string _tag in UnCheckedTags)
            {
                if (!UncheckedTagMemory?.Contains(_tag) ?? false) UncheckedTagMemory.Add(_tag);
            }
        }

        public bool GetMemoryChecked(string _tag)
        {
            return !UncheckedTagMemory?.Contains(_tag) ?? true;
        }

        public void ResetFilters()
        {
            UncheckedTagMemory?.Clear();

            IgnoreCheckChange = true;

            foreach (CheckComboBoxItem _item in Items.Items) 
                if (!_item.Checked) _item.Checked = true;

            IgnoreCheckChange = false;

            Refresh();
        }

        private void _listBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Items.Items[e.Index].Checked = (e.NewValue == CheckState.Checked);

            if (!IgnoreCheckChange) TagsChanged?.Invoke(this, EventArgs.Empty);

            if (e.NewValue == CheckState.Checked)
            {
                UncheckedTagMemory?.Remove(Items.Items[e.Index].Tag);
            }

            RememberFilters();

            Refresh();
        }

        private void AddCheckBox(object sender, CheckComboBoxItem _item)
        {
            IgnoreCheckChange = true;
            _listBox.Items.Add(_item, _item.Checked);
            IgnoreCheckChange = false;
            Refresh();
        }

        private void ClickOnHead(object snder, EventArgs e)
        {
            DroppedDown = !DroppedDown;
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            return new Size(90, 20);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int _count = Items.Items.FindAll(i => !i.Checked)?.Count ?? 0;

            e.Graphics.DrawString("Фільтр" + (_count > 0 ? " (" + _count + ")" : ""), 
                Font, 
                new SolidBrush(ForeColor), 
                new PointF(0, 0));
        }
    }

    public class CheckComboBoxItemList
    {
        public List<CheckComboBoxItem> Items { get; private set; }
        public int Count => Items.Count;
        public event EventHandler<ItemCheckEventArgs> SomeCheckStateChanged;
        public event EventHandler<CheckComboBoxItem> ControlAdded;
        public event EventHandler<int> ControlRemoved;
        public event EventHandler ControlsCleared;

        public CheckComboBoxItemList(List<CheckComboBoxItem> _input = null)
        {
            Items = _input ?? new List<CheckComboBoxItem>();

            int counter = 0;

            foreach (CheckComboBoxItem _item in Items)
            {
                _item.CheckedChanged += CheckStateChanged;
                _item.Index = counter++;
            }
        }

        public void Clear() { 
            Items.Clear();
            ControlsCleared?.Invoke(this, EventArgs.Empty);
        }

        private void CheckStateChanged(object sender, ItemCheckEventArgs e)
        {
            SomeCheckStateChanged?.Invoke((CheckComboBoxItem)sender, e);
        }

        public void Add(CheckComboBoxItem _item)
        {
            _item.CheckedChanged += CheckStateChanged;
            _item.Index = Items.Count - 1;

            Items.Add(_item);

            ControlAdded?.Invoke(this, _item);
        }

        public void AddRange(CheckComboBoxItem[] _items)
        {
            foreach (CheckComboBoxItem _item in _items) Add(_item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
            ControlRemoved?.Invoke(this, index);
        }
    }

    public class CheckComboBoxItem
    {
        public event EventHandler<ItemCheckEventArgs> CheckedChanged;
        public int Index { get; set; } = 0;
        public string Text { get; set; }
        public string Tag { get; set; }
        public bool Checked
        {
            get => CurCheckState; set
            {
                if (CurCheckState == value) return;

                CheckState _NewState = CheckState.Indeterminate;
                CheckState _CurState = CheckState.Indeterminate;

                if (value == true)
                {
                    _NewState = CheckState.Checked;
                    _CurState = CheckState.Unchecked;
                }
                else
                {
                    _NewState = CheckState.Unchecked;
                    _CurState = CheckState.Checked;
                }

                CurCheckState = value;

                CheckedChanged?.Invoke(this, new ItemCheckEventArgs(Index, _NewState, _CurState));
            }
        }

        private bool CurCheckState = true;

        public CheckComboBoxItem(string _text, string _tag, bool _checked = true)
        {
            Text = _text;
            Tag = _tag;
            CurCheckState = _checked;
        }

        public CheckComboBoxItem(string _text, bool _checked = true)
        {
            Text = _text;
            CurCheckState = _checked;
        }

        public CheckComboBoxItem(bool _checked = true)
        {
            CurCheckState = _checked;
        }

        public override string ToString() => Text ?? "";
    }

    public static class UpdateINFO
    {
        public static bool CheckUpdate(out string[] _toUpdate, params string[] exclude)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = "https://sholompromax.com/ii/soft/json_get_version";

            // Створення запиту
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";

            // Отримання відповіді
            string responseText;
            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseText = reader.ReadToEnd();
            }

            // Парсинг JSON
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<UploadFiles> _ans = serializer
                .Deserialize<List<UploadFiles>>(responseText);

            List<UploadFiles> _loc = UploadFilesHelper
                .GetFilesFromFolder(AppDomain
                    .CurrentDomain
                    .BaseDirectory, 
                "*.exe", "*.dll");

            UploadFiles _localFile;
            List<string> _outFiles = new List<string>();

            foreach (UploadFiles _serverFile in _ans)
            {
                _localFile = _loc.Find(f => f.NAME == _serverFile.NAME);

                if (_localFile == null 
                       || _localFile.VERSION < _serverFile.VERSION)
                {
                    if (!exclude.Contains(_serverFile.NAME))
                        _outFiles.Add(_serverFile.NAME);
                }
            }

            _toUpdate = _outFiles.ToArray();

            return _toUpdate.Length > 0;
        }
    }

    public class UploadFiles : IEquatable<UploadFiles>
    {
        public string NAME { get; set; } = "";
        public string VersionString { get; set; } = "0.0.0.0";

        public Version VERSION => new Version(VersionString);

        public override string ToString() => $"{NAME} {VERSION}";

        // Equals враховує всі поля
        public bool Equals(UploadFiles other)
        {
            if (other == null) return false;

            return string.Equals(NAME, other.NAME, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(VersionString, other.VersionString, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UploadFiles);
        }

        // GetHashCode враховує всі поля
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + StringComparer.OrdinalIgnoreCase.GetHashCode(NAME);
                hash = hash * 31 + StringComparer.OrdinalIgnoreCase.GetHashCode(VersionString);
                return hash;
            }
        }
    }


    public static class UploadFilesHelper
    {
        public static List<UploadFiles> GetFilesFromFolder(string folderPath, params string[] filters)
        {
            var result = new List<UploadFiles>();

            foreach (var filter in filters)
            {
                var files = Directory.GetFiles(folderPath, filter, SearchOption.TopDirectoryOnly);

                foreach (var filePath in files)
                {
                    try
                    {
                        var versionInfo = FileVersionInfo.GetVersionInfo(filePath);

                        var file = new UploadFiles
                        {
                            NAME = Path.GetFileName(filePath),
                            VersionString = string.IsNullOrEmpty(versionInfo.FileVersion)
                                        ? "0.0.0.0"
                                        : versionInfo.FileVersion
                        };

                        result.Add(file);
                    }
                    catch
                    {
                        result.Add(new UploadFiles
                        {
                            NAME = Path.GetFileName(filePath),
                            VersionString = "0.0.0.0"
                        });
                    }
                }
            }

            return result;
        }
    }

}
