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

        public MoveForm(string[] input_labels, string[] defVal = null)
        {
            InitializeComponent();

            Height = 80 + (80 * input_labels.Length);

            using (Font _f = AppFonts.LogoFont(12))
            {
                if (input_labels.Length >= 1)
                {
                    lb0.Font = _f;
                    lb0.Text = input_labels[0];

                    tb0.TextChanged += (s, e) => VALUE0 = tb0.Text;
                }

                if (input_labels.Length >= 2 
                    && !string.IsNullOrWhiteSpace(defVal[1]))
                {
                    ttnNeed.Text = input_labels[1];
                    ttnNeed.Visible = true;
                    ttnNeed.Font = _f;

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

            byte _tag = (byte)rb.Tag;

            if (_tag == 1) 
            {
                buttonOK.Enabled = false;



            }
            else
            {
                buttonOK.Enabled = true;
            }
        }

        private void nP_InternetDocument1_Load(object sender, System.EventArgs e)
        {

        }
    }
}
