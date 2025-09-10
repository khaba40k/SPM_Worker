using System.Drawing;
using System.Windows.Forms;
using SPM_Core;

namespace SPM_Worker
{
    public partial class MoveForm : Form
    {
        public string VALUE0 { get; private set; }
        public string VALUE1 { get; private set; }
        public string VALUE2 { get; private set; }

        public MoveForm(string[] input_labels, string[] defVal = null, string addr = "")
        {
            InitializeComponent();

            NP_PANEL.Visible = false;

            Width = MinimumSize.Width;

            if (input_labels.Length <= 1 || !string.IsNullOrWhiteSpace(defVal[1]))
            {
                ClientSize = new Size(MinimumSize.Width, MinimumSize.Height);
                buttonOK.Enabled = true;
                VALUE1 = defVal?.Length > 1 ? defVal[1] : null;
            }

            using (Font _f = AppFonts.LogoFont(12))
            {
                if (input_labels.Length >= 1)
                {
                    lb0.Font = _f;
                    lb0.Text = input_labels[0];

                    tb0.TextChanged += (s, e) => VALUE0 = tb0.Text;
                }

                if (input_labels.Length >= 2)
                {
                    ttnNeed.Text = input_labels[1];
                    ttnNeed.Visible = true;

                    rb_yes.Font = _f;
                    rb_no.Font = _f;

                    NP_FORM.SetAddr(addr);
                    
                    rb_yes.CheckedChanged += Rb_CheckedChanged;
                    rb_no.CheckedChanged += Rb_CheckedChanged;

                    buttonOK.Enabled = false;
                }

                if (input_labels.Length == 3)
                {
                    tb2.Visible = true;
                    lb2.Visible = true;
                    lb2.Font = _f;
                    lb2.Text = input_labels[2];

                    tb2.TextChanged += (s, e) =>
                    {
                        VALUE2 = tb2.Text;

                        if (float.TryParse(VALUE2, out float _cost))
                        {
                            NP_FORM.Cost = _cost;
                        }
                    };
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
                tb2.Select();
            }

            if (defVal != null && defVal.Length > 0)
            {
                tb0.Text = defVal[0];

                if (defVal.Length > 2)
                {
                    tb2.Text = defVal[2];
                }
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

            }
            else
            {
                NP_PANEL.Visible = false;
                Width = MinimumSize.Width;

                buttonOK.Enabled = true;
            }
        }

        private void NP_FORM_KontrolOplatySelected(object sender, System.EventArgs e)
        {
            buttonOK.Enabled = true;
        }
    }
}
