using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class ZakazInfo : UserControl
    {
        public bool Checked { get { return cb_Index.Checked; } set { cb_Index.Checked = value; } }
        public int index { get; set; }
        public string TYPE { get { return GetTypeAsString(INFO.TYPE); } }
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
        public bool TERMINOVO { get { return INFO.TERMINOVO; } }

        public ZAKAZ INFO { get; private set; }

        Color bcgrndColorDef;

        public event EventHandler<ZakazEventArgs> WriteZakaz;
        public event EventHandler<ZakazEventArgs> CheckedChange;

        public ZakazInfo(ZAKAZ _input)
        {
            BackColor = SystemColors.Control;

            InitializeComponent();

            DoubleBuffered = true;

            INFO = _input;

            #region Mouse enter/leave logic

            lb_clientName.MouseEnter += Shared_MouseEnter;
            lb_Number.MouseEnter += Shared_MouseEnter;
            lb_phone.MouseEnter += Shared_MouseEnter;
            lb_terminovo.MouseEnter += Shared_MouseEnter;
            lb_type.MouseEnter += Shared_MouseEnter;
            cb_Index.MouseEnter += Shared_MouseEnter;
            butCHANGE.MouseEnter += Shared_MouseEnter;
            dateTimePicker1.MouseEnter += Shared_MouseEnter;
            this.MouseEnter += Shared_MouseEnter;

            lb_clientName.MouseLeave += Shared_MouseLeave;
            lb_Number.MouseLeave += Shared_MouseLeave;
            lb_phone.MouseLeave += Shared_MouseLeave;
            lb_terminovo.MouseLeave += Shared_MouseLeave;
            lb_type.MouseLeave += Shared_MouseLeave;
            cb_Index.MouseLeave += Shared_MouseLeave;
            butCHANGE.MouseLeave += Shared_MouseLeave;
            dateTimePicker1.MouseLeave += Shared_MouseLeave;
            this.MouseLeave += Shared_MouseLeave;

            #endregion

            cb_Index.CheckedChanged += (s, e) => {

                CheckBox cb = (CheckBox)s;

                CheckedChange?.Invoke(this, new ZakazEventArgs(INFO.ID, INFO.STATUS, cb.Checked));

                if (!cb.Checked)
                {
                    cb_Index.BackColor = Color.Transparent;
                    BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    cb_Index.BackColor = Color.Blue;
                    BorderStyle = BorderStyle.Fixed3D;
                }
            };

            if (INFO.STATUS != Z_STATUS.ARCHIVE)
            {
                dateTimePicker1.CustomFormat = "dd.MM.yy";
            }

            butCHANGE.Click += (s, e) => {
                WriteZakaz?.Invoke(this, new ZakazEventArgs(INFO.ID));
            };

            bcgrndColorDef = BackColor;

            lb_terminovo.Visible = TERMINOVO;

            lb_clientName.Text = INFO.CLIENT_NAME;
            if (INFO.STATUS != Z_STATUS.NEW)
            {
                lb_Number.Text = INFO.NUMBER.ToString();
            }
            else
            {
                lb_Number.Visible = false;
            }
            lb_phone.Text = INFO.PHONE;
            lb_type.Text = TYPE;
            dateTimePicker1.Value = (DateTime)DATE;

            TOOL_TIP_INIT();
        }

        private void TOOL_TIP_INIT()
        {
            toolTip1.ToolTipTitle = "Інформація по замовленню" + (INFO.NUMBER > 0 ? " №" + INFO.NUMBER.ToString() : "");

            string TTtext = "";

            TTtext += $"{INFO.DATE_IN} -> {INFO.DATE_MAX.ToShortDateString()}\n";
            TTtext += $"{INFO.COMM}\n\n";

            int counter = 0;

            foreach (KOMPLEKT K in INFO.KOMPLEKT)
            {
                string _color = K.color != null ? SERVICE_INFO.GetColorName((int)K.color) : "";

                TTtext += $"{(++counter).ToString().PadLeft(2, '0')} |" +
                    $" {K.costs.ToString().PadLeft(8, '_')} |" +
                    $" {GetNameAndType(K.service_ID, K.type_ID)} " +
                    $"{_color}\n";
            }

            TTtext += $"\n\t\t\t {INFO.SUM} грн.\n";
            TTtext += $"{INFO.REDAKTOR}";

            toolTip1.SetToolTip(this, TTtext);

            
            SetToolTip(Controls, TTtext);
        }

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

            _out = _out.PadRight(_totalWidth);

            return _out;
        }

        /// <summary>
        /// Рекурсивне присвоєння підсказки
        /// </summary>
        /// <param name="_controls">Колекція дочірніх контролів</param>
        /// <param name="_txt">Підсказка</param>
        private void SetToolTip(ControlCollection _controls, string _txt)
        {
            foreach (Control child in _controls)
            {
                toolTip1.SetToolTip(child, _txt);

                if (child.HasChildren)
                {
                    SetToolTip(child.Controls, _txt);
                }
            }

        }

        string GetTypeAsString(Z_TYPE _inp)
        {
            switch (_inp)
            {
                case Z_TYPE.SOLD:
                    return "Продаж";
                default:
                    return "Переобладнання";
            }
        }

        private void Shared_MouseEnter(object sender, EventArgs e)
        {
            BackColor = SystemColors.ControlDark;
        }

        private void Shared_MouseLeave(object sender, EventArgs e)
        {
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                BackColor = bcgrndColorDef;
            }
        }
    }

    public class ZakazEventArgs : EventArgs
    {
        public int ID { get; }
        public bool CHECKED { get; set; }
        public Z_STATUS STATUS { get; set; }

        public ZakazEventArgs(int id, Z_STATUS stat = Z_STATUS.NEW, bool checkedStat = false)
        {
            ID = id;
            STATUS = stat;
            CHECKED = checkedStat;
        }
    }

    public enum Z_STATUS : byte
    {
        NULL = 255,
        NEW = 0,
        ACTIVE = 1,
        ARCHIVE = 2
    }
}
