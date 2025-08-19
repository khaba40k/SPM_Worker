using SPM_Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class MAIN_FORM : Form
    {
        public Z_STATUS MAIN_STATUS = Z_STATUS.NEW;
        private ZakazListControl _zlc = new ZakazListControl();
        private Color _buttonBackColor = Color.Olive;
        private Color _buttonMenuHoverColor = Color.Orange;

        public MAIN_FORM(AutorizeInfo _autorizeinfo)
        {
            SERVICE_INFO.AUTORIZE_INFO = _autorizeinfo;

            InitializeComponent();

            Text = $"SholomProMax Користувач: [{_autorizeinfo.data.NAME.ToUpper()}]";

            panelMenu.ControlAdded += (s, e) => {
                Button _btn = (Button)e.Control;

                _btn.Cursor = Cursors.Hand;
                _btn.AutoSize = true;
                _btn.Width = panelMenu.Width - panelMenu.Padding.Horizontal - panelMenu.Margin.Horizontal;
                _btn.BackColor = _buttonBackColor;
                _btn.ForeColor = Color.White;
                _btn.Font = new Font(e.Control.Font, FontStyle.Bold);
                _btn.Margin = new Padding(0);
                _btn.Padding = new Padding(0, 5, 0, 5);
                _btn.Click += (s1, e1) => {
                    ResetBackButColor();
                    _btn.BackColor = _buttonMenuHoverColor;
                };
            };

            panelContent.ControlAdded += (s, e) =>
            {
                e.Control.MinimumSize = new Size(0, 0);
                e.Control.Dock = DockStyle.Fill;
            };

            //СТВОРЕННЯ КНОПОК МЕНЮ
            Button _ZakazListBut = new Button() { Text = "Замовлення" };
            Button _VitratyBut = new Button() { Text = "Витрати" };

            //ПРИСВОЄННЯ ДІЇ

            _ZakazListBut.Click += SetZakazList;
            _VitratyBut.Click += SetVitraty;

            //ПРОРИСОВКА КНОПОК

            panelMenu.Controls.Add(_ZakazListBut);
            panelMenu.Controls.Add(_VitratyBut);

            if (_autorizeinfo.data.ID == 0)
            {
                Button _LogsBut = new Button() { Text = "Логи" };
                _LogsBut.Click += ShowLogs;
                panelMenu.Controls.Add(_LogsBut);
            }

            //ПЕРШЕ ВІКНО ПІСЛЯ ЗАПУСКУ
            _ZakazListBut.BackColor = _buttonMenuHoverColor;
            SetZakazList(this, EventArgs.Empty);
        }

        private void ResetBackButColor()
        {
            foreach (Control _c in panelMenu.Controls)
            {
                _c.BackColor = Color.Olive;
            }
        }

        private void SetZakazList(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            panelContent.Controls.Add(_zlc);
        }

        private void ShowLogs(object sender, EventArgs e)
        {
            using (LogList _ll = new LogList())
            {
                _ll.ShowDialog();
            }
        }

        private void SetVitraty(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            Vitraty _vitr = new Vitraty();

            panelContent.Controls.Add(_vitr);
        }
    }
}
