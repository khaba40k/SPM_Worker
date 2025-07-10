using System;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class MAIN_FORM : Form
    {
        public Z_STATUS MAIN_STATUS = Z_STATUS.NEW;
        public MAIN_FORM()
        {
            InitializeComponent();

            buttAddZ.Click += (s, e) => {
                new ZAKAZ_FORM().ShowDialog();
            };
        }

    }
}
