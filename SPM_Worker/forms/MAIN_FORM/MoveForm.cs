using System.Drawing;
using System.Windows.Forms;
using SPM_Core;

namespace SPM_Worker
{
    public partial class MoveForm : Form
    {
        public string WORKER { get; private set; }
        public string TTN { get; private set; }
        public string COST { get; private set; }

        public IDoc_TTN_INFO TTN_INFO;

        private bool isCreateTTN = false;

        public MoveForm(ZAKAZ _inputZakaz)
        {
            InitializeComponent();

            NP_PANEL.Visible = false;

            Width = MinimumSize.Width;

            using (Font _f = AppFonts.LogoFont(12))
            {
                lb0.Font = _f;

                if (_inputZakaz.STATUS == Z_STATUS.NEW)
                {
                    ClientSize = new Size(MinimumSize.Width, MinimumSize.Height);
                    buttonOK.Enabled = true;

                    lb0.Text = "ТТН (вх)";
                    tb0.TextChanged += (s, e) => TTN = tb0.Text;

                    tb0.Text = _inputZakaz.TTN_IN ?? "";
                }
                else
                {
                    lb1.Font = _f;

                    lb0.Text = "Працівник";
                    tb0.TextChanged += (s, e) => WORKER = tb0.Text;

                    tb0.Text = _inputZakaz.WORKER ?? "";

                    ttnNeed.Visible = true;

                    rb_yes.Font = _f;
                    rb_no.Font = _f;

                    NP_FORM.SetAddr(_inputZakaz.REQV ?? "");

                    rb_yes.CheckedChanged += Rb_CheckedChanged;
                    rb_no.CheckedChanged += Rb_CheckedChanged;

                    buttonOK.Enabled = false;

                    tb1.Visible = true;
                    lb1.Visible = true;
                    lb1.Text = "Сума (факт)";

                    tb1.TextChanged += (s, e) =>
                    {
                        COST = tb1.Text;

                        if (float.TryParse(COST, out float _cost))
                        {
                            NP_FORM.Cost = _cost;
                        }
                        else
                        {
                            NP_FORM.Cost = 0f;
                        }
                    };

                    tb1.Text = _inputZakaz.SUM.ToString();
                }

                buttonOK.Font = _f;
                buttonCancel.Font = _f;
            }

            if (string.IsNullOrWhiteSpace(tb0.Text))
            {
                tb0.Select();
            }
            else
            {
                tb1.Select();
            }
        }

        private void Rb_CheckedChanged(object sender, System.EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            byte _tag = byte.Parse(rb.Tag.ToString());

            if (_tag == 1) 
            {
                buttonOK.Enabled = false;

                Width += NP_PANEL.Width;
                NP_PANEL.Visible = true;
                isCreateTTN = true;
            }
            else
            {
                NP_PANEL.Visible = false;
                isCreateTTN = false;
                Width = MinimumSize.Width;

                buttonOK.Enabled = true;
            }
        }

        private void NP_FORM_KontrolOplatySelected(object sender, System.EventArgs e)
        {
            buttonOK.Enabled = true;
        }

        private void MoveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TTN_INFO = isCreateTTN ? NP_FORM.INFO() : null;
        }

        private void NP_FORM_ValueChanged(object sender, bool e)
        {
            buttonOK.Enabled = e;
        }
    }
}
