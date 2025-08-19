using SPM_Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class ZakazInfo : UserControl, IEquatable<ZakazInfo>
    {
        public bool Checked { get { return cb_Index.Checked; } set { cb_Index.Checked = value; } }
        public byte TYPE { get { return (byte)INFO.TYPE; } }
        private int RightButWidth = 0;
        public DateTime? DATE
        {
            get
            {
                if (INFO.STATUS == Z_STATUS.ARCHIVE)
                {
                    return INFO.DATE_OUT;
                }
                else
                {
                    return INFO.DATE_MAX;
                }
            }
        }
        public string REDAKTOR => INFO.REDAKTOR;
        public ZAKAZ INFO { get; private set; }

        Color bcgrndColorDef;
        Color checkedColor = Color.Aqua;
        Color hoverColor = Color.LightBlue;

        private Image _image;

        public event EventHandler<ZakazEventArgs> WriteZakaz;
        public event EventHandler<ZakazEventArgs> CheckedChange;
        public event EventHandler<ZakazEventArgs> MoveZakaz;
        public event EventHandler RemoveZakaz;
        public event EventHandler<byte> PrintZakaz;

        public ZakazInfo(ZAKAZ _input)
        {
            InitializeComponent();

            DoubleBuffered = true;

            INFO = _input;

            _image = INFO.TYPE == Z_TYPE.SOLD ? Properties.Resources.sold_type
                : Properties.Resources.pereobl_type;

            DoubleBuffered = true;

            HandleCreated += (s, e) => BeginInvoke((MethodInvoker)INIT_DATA);
        }

        private void INIT_DATA()
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += ToolTip1_Draw;
            toolTip1.Popup += ToolTip1_Popup;

            TOOL_TIP_INIT();

            #region Mouse enter/leave logic

            cb_Index.MouseEnter += Shared_MouseEnter;
            MouseEnter += Shared_MouseEnter;

            cb_Index.MouseLeave += Shared_MouseLeave;
            MouseLeave += Shared_MouseLeave;

            #endregion

            MouseClick += MyUserControl_MouseClick;

            DoubleClick += (s, e) =>
            {
                WriteZakaz?.Invoke(INFO, new ZakazEventArgs(INFO.ID));
            };

            bcgrndColorDef = BackColor;

            cb_Index.CheckedChanged += (s, e) => {

                CheckBox cb = (CheckBox)s;

                CheckedChange?.Invoke(this, new ZakazEventArgs(INFO.ID, INFO.STATUS, cb.Checked));

                if (!cb.Checked)
                {
                    BackColor = bcgrndColorDef;
                    BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    BackColor = checkedColor;
                    BorderStyle = BorderStyle.Fixed3D;
                }
            };

            Color _statusColor = Color.White;

            switch (INFO.STATUS)
            {
                case Z_STATUS.NEW:
                    _statusColor = Color.Red;
                    break;
                case Z_STATUS.ACTIVE:
                    _statusColor = Color.Yellow;
                    break;
                case Z_STATUS.ARCHIVE:
                    _statusColor = Color.Green;
                    break;
            }

            cb_Index.BackColor = _statusColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            //IMAGE_TYPE

            if (_image != null)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Масштабування по висоті
                float scale = (float)Height / _image.Height;
                int drawWidth = (int)(_image.Width * scale);
                int drawHeight = Height;

                int drawX = Width - drawWidth;
                int drawY = 0;

                // Малюємо PNG з альфою
                g.DrawImage(_image, new Rectangle(drawX, drawY, drawWidth, drawHeight));
            }

            int leftPos = cb_Index.Width;
            int GlobalSplitCellWidth = Width - Padding.Horizontal - leftPos;
            int[] CellWidth = new int[4] { 70, 0, 100, 150 };

            CellWidth[1] = GlobalSplitCellWidth - CellWidth[0] - CellWidth[2] - CellWidth[3];

            Font _font = AppFonts.LogoFont(45, FontStyle.Bold);
            SolidBrush _brush = new SolidBrush(Color.White);

            //REDAKTOR

            g.DrawString(INFO.REDAKTOR, _font,
                    _brush, new PointF(leftPos + CellWidth[0] + (CellWidth[1] / 4), -14));

            _brush.Color = BackColor;

            g.DrawString(INFO.REDAKTOR, _font,
                    _brush, new PointF(leftPos + CellWidth[0] + (CellWidth[1] / 4) + 5, -11));

            //INFO.NUMBER
            if (INFO.NUMBER > 0)
            {
                _font = AppFonts.DigitFont(INFO.DISCOUNT != null ? 16 : 32);
                _brush.Color = Color.Black;
                g.DrawString(INFO.NUMBER.ToString(), _font,
                    _brush, new PointF(leftPos, 0));
            }

            //INFO.CLIENT_NAME
            _font = AppFonts.TextFont(16);

            string _clientName = GetShortName(INFO.CLIENT_NAME);

            if (INFO.TERMINOVO)
            {
                _clientName = _clientName.ToUpper();
                _brush.Color = Color.Red;
            }
            else
            {
                _brush.Color = Color.BlueViolet;
            }

            g.DrawString(_clientName.ToString(), _font,
                  _brush, new PointF(leftPos + CellWidth[0], 14));

            //INFO.PHONE

            _font = AppFonts.DigitFont(18);
            _brush.Color = Color.SlateGray;

            string _phone = GetCorrectPhone(INFO.PHONE);

            g.DrawString(_phone, _font,
                   _brush, new PointF(leftPos + CellWidth[0] +  CellWidth[1], 10));

            //DATE

            _brush = new SolidBrush(Color.BlueViolet);

            if (INFO.STATUS != Z_STATUS.ARCHIVE
                    && INFO.DATE_MAX < new DateTime(
                       DateTime.Now.Year, 
                       DateTime.Now.Month, 
                       DateTime.Now.Day))
            {
                _brush.Color = Color.Red;
            }

            string _date;

            if (INFO.STATUS != Z_STATUS.ARCHIVE)
            {
                _date = ((DateTime)DATE).ToShortDateString();
            }
            else
            {
                _date = ((DateTime)DATE).ToShortDateString() + " "
                    + ((DateTime)DATE).ToShortTimeString();
            }

            g.DrawString(_date, _font,
                  _brush, new PointF(leftPos + CellWidth[0] + CellWidth[1] + CellWidth[2], 10));

            //INFO.DISCOUNT

            if (INFO.DISCOUNT != null)
            {
                _font = AppFonts.DigitFont(16, FontStyle.Bold);
                _brush.Color = Color.Green;

                g.DrawString("-" + INFO.DISCOUNT.ToString() + "%", _font,
                  _brush, new PointF(cb_Index.Width, Height / 2));
            }

            if (RightButWidth > 0)
            {
                _font = AppFonts.DigitFont(32, FontStyle.Bold);
                _brush.Color = Color.Blue;
                g.DrawString(">", _font,
                    _brush, new PointF(Width - RightButWidth, 0));
            }

            _font?.Dispose();
            _brush?.Dispose();
        }

        public string GetShortName(string _input)
        {
            string[] _split = _input.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (_split.Length > 1)
            {
                string _out = _split[0];

                for (int i = 1; i < _split.Length; i++)
                {
                    _out += " " + _split[i].Substring(0, 1).ToUpper() + ".";
                }

                return _out;
            }
            else
            {
                return _input.Trim();
            }
        }

        public string GetCorrectPhone(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "?";

            // Шаблон: шукаємо можливі початки телефонів українських операторів
            // +380, 380, 80, 0 і далі мінімум 8 цифр (разом буде 10 цифр для формату 0XXXXXXXXX)
            var regex = new Regex(@"(?:\+?380|\b380|\b80|\b0)([\s\-]?\d){8,12}", RegexOptions.Compiled);

            var matches = regex.Matches(input);

            if (matches.Count == 0)
                return "?";

            // Беремо останній збіг
            string rawNumber = matches[matches.Count - 1].Value;

            // Залишаємо тільки цифри
            string digits = new string(rawNumber.Where(char.IsDigit).ToArray());

            // Нормалізуємо у формат 0XXXXXXXXX
            if (digits.StartsWith("380") && digits.Length >= 12)
                digits = digits.Substring(digits.Length - 10);
            else if (digits.StartsWith("80") && digits.Length >= 11)
                digits = digits.Substring(digits.Length - 10);
            else if (digits.StartsWith("0") && digits.Length >= 10)
                digits = digits.Substring(0, 10);
            else
                return "?";

            return digits.Length == 10 ? digits : "?";
        }

        public void SetWidth(object sender, SizeEventHandler e)
        {
            Width = e.Width;
            Refresh();
        }

        #region GPT
        private void TOOL_TIP_INIT()
        {
            toolTip1.ToolTipTitle = "Інформація по замовленню" + (INFO.NUMBER > 0 ? " №" + INFO.NUMBER.ToString() : "");

            string TTtext = $"{INFO.DATE_IN} -> {INFO.DATE_MAX.ToShortDateString()}\n" +
                            $"{INFO.COMM}\n\n" +
                            new SERVICE_ID_LIST(INFO.KOMPLEKT).ToString() +
                            $"{INFO.REDAKTOR}";

            toolTip1.SetToolTip(this, TTtext);
        }

        // Малюємо тултіп з фіксованим шрифтом
        private void ToolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            using (Font monoFont = new Font("Consolas", 9))
            {
                e.Graphics.FillRectangle(SystemBrushes.Info, e.Bounds);
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
                e.Graphics.DrawString(e.ToolTipText, monoFont, Brushes.Black, new PointF(2, 2));
            }
        }

        private void ToolTip1_Popup(object sender, PopupEventArgs e)
        {
            using (Font monoFont = new Font("Consolas", 9))
            {
                string text = toolTip1.GetToolTip(e.AssociatedControl);
                Size textSize = TextRenderer.MeasureText(text, monoFont);  // or use Graphics.MeasureString if needed
                e.ToolTipSize = new Size(textSize.Width + 4, textSize.Height + 4);
            }
        }
        #endregion

        public string GetNameAndType(int _id, int _type = 1, int _totalWidth = 30)
        {
            SERVICE_ID_INFO SN;

            string OUT_NAME = "", OUT_TYPE = "";

            if (_id == 19)
            {
                OUT_NAME = SERVICE_INFO.SERVICE_LIST.Find(n => n.ID == 19 && n.TYPE_ID == 1).NAME;
                OUT_TYPE = SERVICE_INFO.SERVICE_LIST.Find(n => n.ID == _type && n.TYPE_ID == 1).NAME;
            }
            else
            {
                SN = SERVICE_INFO.SERVICE_LIST.Find(n => n.ID == _id && n.TYPE_ID == _type);
                OUT_NAME = SN.NAME;
                OUT_TYPE = SN.TYPE_NAME;
            }

            string _out = OUT_NAME + (OUT_TYPE != null ? " (" + OUT_TYPE + ")" : "");

            return _out.PadRight(_totalWidth);
        }

        private void MyUserControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (RightButWidth > 0 && (Width - e.X) <= RightButWidth && e.Button == MouseButtons.Left)
            {
                MoveZakaz?.Invoke(INFO, new ZakazEventArgs(INFO.ID, INFO.STATUS));
            } 
        }

        private void Shared_MouseEnter(object sender, EventArgs e)
        {
            BackColor = cb_Index.Checked ? checkedColor : hoverColor;

            Focus();

            if (INFO.STATUS != Z_STATUS.ARCHIVE && RightButWidth == 0)
            {
                RightButWidth = 40;

                Refresh();
            }
        }

        private void Shared_MouseLeave(object sender, EventArgs e)
        {
            BackColor = cb_Index.Checked ? checkedColor : bcgrndColorDef;

            if (INFO.STATUS != Z_STATUS.ARCHIVE && RightButWidth > 0)
            {
                RightButWidth = 0;

                Refresh();
            }
        }

        public bool Equals(ZakazInfo other) => INFO.Equals(other.INFO);

        public override bool Equals(object obj) => Equals(obj as ZakazInfo);

        public override int GetHashCode()
        {
            return INFO?.GetHashCode() ?? 0;
        }

        private void cmWriteZakaz_Click(object sender, EventArgs e)
        {
            WriteZakaz?.Invoke(INFO, new ZakazEventArgs(INFO.ID));
        }

        private void cmPrint_Click(object sender, EventArgs e)
        {
            PrintZakaz?.Invoke(this, 0);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.P))
            {
                PrintZakaz?.Invoke(this, 255);
                return true;
            }else if (keyData == Keys.Enter)
            {
                WriteZakaz?.Invoke(INFO, new ZakazEventArgs(INFO.ID));
                return true;
            }

            return false;
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {
            RemoveZakaz?.Invoke(this, EventArgs.Empty);
        }

        private void cmPrintFromWorker_Click(object sender, EventArgs e)
        {
            PrintZakaz?.Invoke(this, 1);
        }

        private void cmPrintShort_Click(object sender, EventArgs e)
        {
            PrintZakaz?.Invoke(this, 2);
        }

        private void cmShablon_Click(object sender, EventArgs e)
        {
            FullShablon();
        }

        private void cbShablonBottom_Click(object sender, EventArgs e)
        {
            ZAKAZ _shablon = new ZAKAZ(INFO.KOMPLEKT, INFO);

            WriteZakaz?.Invoke(_shablon, new ZakazEventArgs(_shablon.ID));
        }

        private void cbShablonTop_Click(object sender, EventArgs e)
        {
            ZAKAZ _shablon = new ZAKAZ(INFO, false);

            WriteZakaz?.Invoke(_shablon, new ZakazEventArgs(_shablon.ID));
        }

        private void cbShablonAll_Click(object sender, EventArgs e)
        {
            FullShablon();
        }

        private void FullShablon()
        {
            ZAKAZ _shablon = new ZAKAZ(INFO, true);

            WriteZakaz?.Invoke(_shablon, new ZakazEventArgs(_shablon.ID));
        }

        public override string ToString()
        {
            return INFO.CLIENT_NAME;
        }
    }

    public class ZakazEventArgs : EventArgs
    {
        public int Index { get; set; }
        public int ID { get; }
        public bool CHECKED { get; set; } = false;
        public Z_STATUS STATUS { get; set; } = Z_STATUS.NULL;

        public ZakazEventArgs(int id, Z_STATUS stat = Z_STATUS.NEW, bool checkedStat = false)
        {
            ID = id;
            STATUS = stat;
            CHECKED = checkedStat && id > -1;
        }

        public ZakazEventArgs(int id)
        {
            ID = id;
            CHECKED = id > -1;
        }
    }

}
