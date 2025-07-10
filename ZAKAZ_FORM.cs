using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPM_Worker
{

    public partial class ZAKAZ_FORM : Form
    {
        private bool HIDDEN_TOP = false;

        private int PANEL_TOP_HEIGHT, PANEL_BOTTOM_HEIGHT, PANEL_DIFF;

        private ZAKAZ INFO;
        public ZAKAZ_FORM(ZAKAZ _input = null)
        {
            InitializeComponent();

            PANEL_TOP_HEIGHT = panel1.Height;
            PANEL_BOTTOM_HEIGHT = panel2.Height;
            PANEL_DIFF = 25;

            foreach (TabPage _tp in tabControl1.TabPages)
            {
                _tp.MouseEnter += MouseEnterBottom;
            }

            panel1.MouseEnter += (s, e) => {
                if (!HIDDEN_TOP) return;

                panel1.Height = PANEL_TOP_HEIGHT;
                panel2.Height = PANEL_BOTTOM_HEIGHT;

                panel1.BackColor = SystemColors.Control;

                HIDDEN_TOP = false;
            };

            buttonSave.Click += (s, e) => {

                string _mes = "";

                foreach (SERVICE_ID_INFO _c in pereoblInterface1.PRODUCTS)
                {
                    _mes += $"ID: {_c.ID}->{_c.TYPE_ID} : {_c.NAME}/{_c.TYPE_NAME} ({_c.COLOR_ID}: {_c.COLOR_NAME})\n";
                }

                MessageBox.Show(_mes);
            };

            string[] _messengers = new string[] {
                 "",
                 "Телеграм",
                 "Вотсап",
                 "Інстаграм",
                 "Вайбер",
                 "Телефон",
                 "Наручно",
                 "Сигнал",
                 "ТікТок"
            };

            int _mesIndex = 0;

            cb_messendger.Items.AddRange(_messengers);

            if (_input != null)
            {
                INFO = _input;

                Text = $"Редагування замовлення{(INFO.NUMBER > 0 ? $" № {INFO.NUMBER}" : "")}";

                dt_vid.Value = INFO.DATE_IN;
                dt_do.Value = INFO.DATE_MAX;
                if (INFO.DATE_OUT != null)
                {
                    dt_done.Value = Convert.ToDateTime(INFO.DATE_OUT);
                    dt_done.Visible = true; dt_done.Enabled = true;
                }
                tb_phone.Text = INFO.PHONE;
                tb_phone.Select(tb_phone.Text.Length, 0);

                tb_name.Text = INFO.CLIENT_NAME;
                tb_ttnin.Text = INFO.TTN_IN;
                tb_ttnout.Text = INFO.TTN_OUT;
                tb_discount.Text = Convert.ToByte(INFO.DISCOUNT).ToString();
                tb_reqv.Text = INFO.REQV;

                string _firstLetter = INFO.COMM.Split(' ')[0].ToLower();

                for (int i = 1; i < _messengers.Length; i++)
                {
                    if (_messengers[i].ToLower().Contains(_firstLetter))
                    {
                        INFO.COMM = INFO.COMM.Remove(0, _firstLetter.Length).TrimStart();
                        _mesIndex = i;
                        break;
                    }
                }

                tb_comm.Text = INFO.COMM;
                cb_term.Checked = INFO.TERMINOVO;
                cb_worker.Text = INFO.WORKER;
                cb_redaktor.Text = INFO.REDAKTOR;
            }
            else
            {
                tb_phone.Select();
                dt_vid.Value = DateTime.Now;
                dt_do.Value = DateTime.Now.AddDays(3);
            }

            cb_messendger.SelectedIndex = _mesIndex;

            if (_input != null)
                pereoblInterface1.SET(_input.KOMPLEKT);
            else
                pereoblInterface1.SET();
        }

        private void MouseEnterBottom(object sender, EventArgs e)
        {
            if (HIDDEN_TOP) return;

            panel1.Height = PANEL_DIFF;

            panel2.Height += (PANEL_TOP_HEIGHT - PANEL_DIFF);

            panel1.BackColor = SystemColors.ControlDarkDark;

            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - PANEL_DIFF);

            HIDDEN_TOP = true;
        }

        class NP_INFO
        {
            public string OBLAST { get; set; }
            public string RAJON { get; set; }
            public string NASEL_PUNKT { get; set; }
            public string STREET { get; set; }
            public string HOUSE_NUMBER { get; set; }
            public string VIDDILENNIA { get; set; }
            public int NUMBER_VIDDIL { get; set; }
            public NP_INFO(string _input = "")
            {

            }

            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}
