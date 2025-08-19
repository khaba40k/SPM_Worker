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

        public MoveForm(string[] input_array, string[] defVal = null)
        {
            InitializeComponent();

            Height = 60 + (60 * input_array.Length);

            using (Font _f = AppFonts.LogoFont(12))
            {
                if (input_array.Length >= 1)
                {
                    lb0.Font = _f;
                    lb0.Text = input_array[0];

                    tb0.TextChanged += (s, e) =>
                    {
                        VALUE0 = tb0.Text;
                    };
                }

                if (input_array.Length >= 2)
                {
                    tb1.Visible = true;
                    lb1.Visible = true;
                    lb1.Font = _f;
                    lb1.Text = input_array[1];

                    tb1.TextChanged += (s, e) =>
                    {
                        VALUE1 = tb1.Text;
                    };
                }

                if (input_array.Length == 3)
                {
                    tb2.Visible = true;
                    lb2.Visible = true;
                    lb2.Font = _f;
                    lb2.Text = input_array[2];

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
            else if (string.IsNullOrWhiteSpace(tb1.Text))
            {
                tb1.Select();
            }
            else
            {
                tb2.Select();
            }

            if (defVal != null && defVal.Length > 0)
            {
                tb0.Text = defVal[0];

                if (defVal.Length > 1)
                {
                    tb1.Text = defVal[1];
                }

                if (defVal.Length > 2)
                {
                    tb2.Text = defVal[2];
                }
            }
        }
    }
}
