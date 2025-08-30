using SPM_Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class SoldCart : UserControl
    {
        public int NUMBER
        {
            get => _number; set
            {
                _number = value;
                lb_num.Text = _number.ToString();
            }
        }
        private int _number = 1;
        public VitratyInfo VALUE { get; private set; } = new VitratyInfo();
        public bool HAS_CHANGE { get { return cb_name.SelectedIndex > -1; } }
        public event EventHandler<ZakazEventArgs> ID_CHANGED;
        private bool HAS_DEFFAULT_VALUE = false;
        public float DiscountKoef
        {
            get => _globDiscKoef; set
            {
                _globDiscKoef = value;

                if (_globDiscKoef < 1f)
                {
                    tb_cost.ForeColor = Color.Green;
                }
                else
                {
                    tb_cost.ForeColor = Color.Black;
                }

                tb_cost.Text = !HAS_DEFFAULT_VALUE 
                    ? (VALUE.COST * value).ToString("F2")
                    : VALUE.COST.ToString();
            }
        }
        private float _globDiscKoef = 1f;
        public bool CanDelete
        {
            get => but_del.Visible; set
            {
                but_del.Visible = value;
            }
        }

        public SoldCart(SERVICE_ID_INFO _input = null, int _atr = 0, bool withCOMM = false)
        {
            INIT(_input, _atr, withCOMM);
        }

        private void INIT(SERVICE_ID_INFO _input, int _atr, bool _withCOMM)
        {
            InitializeComponent();

            HAS_DEFFAULT_VALUE = _input != null;

            cb_name.MouseWheel += ComboBox_MouseWheel;
            cb_type.MouseWheel += ComboBox_MouseWheel;
            cb_color.MouseWheel += ComboBox_MouseWheel;

            List<ONE_SERVICE_INFO> ID_NAME_list = SERVICE_INFO
                .GetAllServiceNameList(_atr, _input == null);

            tb_cost.Text = (0f).ToString("F2");

            cb_name.Items.Add(new ONE_SERVICE_INFO());

            foreach (ONE_SERVICE_INFO i in ID_NAME_list)
            {
                cb_name.Items.Add(i);

                if (_input != null && _input.ID == i.ID)
                {
                    cb_name.SelectedItem = i;
                }
            }

            BackColorChanged += (s, e) =>
            {
                Color newColor = ((Control)s).BackColor;

                SetBackColorRecursive(this, newColor);
            };

            tb_cost.TextChanged += (s, e) =>
            {
                if (float.TryParse(tb_cost.Text, out float val))
                    VALUE.COST = val * (VALUE.SPISANNJA ? -1:1);
                else
                    VALUE.COST = 0f;
            };

            cb_name.SelectedIndexChanged += (s, e) =>
            {
                ONE_SERVICE_INFO _item = (ONE_SERVICE_INFO)cb_name.SelectedItem 
                ?? new ONE_SERVICE_INFO() { ID = -1, NAME = "" };

                ZakazEventArgs _ea = new ZakazEventArgs(_item.ID ?? -1);

                _ea.Index = NUMBER - 1;

                ID_CHANGED?.Invoke(this, _ea);

                if (cb_name.SelectedIndex > 0)
                {
                    VALUE.ID = _item.ID ?? -1;

                    tb_cost.Text = (VALUE.COUNT
                                     * (SERVICE_INFO.GetCost(VALUE.ID, VALUE.TYPE_ID)
                                     * DiscountKoef))
                                     .ToString("F2");

                    if (!InitTYPES()) InitCOLORS();

                    if (VALUE.IS_POSLUGA && !VALUE.HAS_COLOR)
                    {
                        tb_count.Text = "1";
                    }

                    tb_count.Enabled = !VALUE.IS_POSLUGA || VALUE.HAS_COLOR;
                }
                else
                {
                    VALUE.ID = -1;
                    VALUE.TYPE_ID = 1;
                    VALUE.COLOR_ID = null;

                    cb_type.Items.Clear();
                    cb_color.Items.Clear();

                    cb_type.Enabled = false;
                    cb_type.Visible = false;
                    cb_color.Enabled = false;
                    cb_color.Visible = false;
                }

                HAS_DEFFAULT_VALUE = false;
            };

            cb_type.SelectedIndexChanged += (s, e) => {
                ONE_TYPE_INFO _item = (ONE_TYPE_INFO)cb_type.SelectedItem;

                VALUE.TYPE_ID = _item.TYPE_ID;

                tb_cost.Text = (VALUE.COUNT
                                * (SERVICE_INFO.GetCost(VALUE.ID, VALUE.TYPE_ID)
                                * DiscountKoef))
                                .ToString("F2");

                InitCOLORS();
            };

            cb_color.SelectedIndexChanged += (s, e) =>
            {
                ONE_COLOR_INFO _item = (ONE_COLOR_INFO)cb_color.SelectedItem;

                VALUE.COLOR_ID = _item.ID;
            };

            cb_color.DrawItem += ComboBoxColor_DrawItem;

            tb_count.TextChanged += (s, e) => {
                if (!string.IsNullOrWhiteSpace(tb_count.Text) &&
                    int.TryParse(tb_count.Text, out int _val))
                    VALUE.COUNT = _val * (VALUE.SPISANNJA ? -1:1);
                else
                    VALUE.COUNT = (VALUE.SPISANNJA ? -1 : 1);

                if (!VALUE.SPISANNJA && tb_cost.Enabled)
                tb_cost.Text = (VALUE.COUNT
                * (SERVICE_INFO.GetCost(VALUE.ID, VALUE.TYPE_ID)
                * DiscountKoef))
                .ToString("F2");
            };

            tb_count.KeyPress += new KeyPressEventHandler(IntTextBox_KeyPress);

            tb_cost.KeyPress += new KeyPressEventHandler(FloatTextBox_KeyPress);

            tb_count.MouseClick += (s, e) =>
            {
                tb_count.SelectAll();
            };

            tb_cost.MouseClick += (s, e) =>
            {
                if (float.TryParse(tb_cost.Text, out float _val) && _val == 0)
                {
                    tb_cost.SelectAll();
                }
            };

            but_del.Click += (s, e) => {
                if (DialogResult.Yes != CustomMessage.Show(
                    $"{VALUE.NAME} {VALUE.TYPE_NAME ?? ""} " +
                    $"{VALUE.COLOR_NAME ?? ""} [{VALUE.COST}]",
                    "Видалення",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    new MessButText { Yes = "Видалити зі списку", No = "Залишити" }
                    )) return;

                cb_name.SelectedIndex = 0;
            };

            if (_withCOMM)
            {
                Height += 20;

                CheckBox _spis = new CheckBox();
                _spis.Text = "Списання";
                _spis.Location = new Point(30, Height - 23);
                _spis.AutoSize = false;
                _spis.Width = 100;
                _spis.CheckedChanged += (s, e) =>
                {
                    VALUE.SPISANNJA = _spis.Checked;

                    VALUE.COUNT *= -1;

                    if (VALUE.SPISANNJA)
                    {
                        tb_cost.Text = "0";
                        tb_cost.Enabled = false;
                    }
                    else
                    {
                        tb_cost.Enabled = true;
                        tb_cost.Text = (VALUE.GetDeffaultCost() * VALUE.COUNT).ToString();
                    }
                };

                _spis.TabStop = false;
                _spis.Cursor = Cursors.Hand;
                _spis.Checked = false;

                tb_count.EnabledChanged += (s, e) =>
                {
                    _spis.Visible = tb_count.Enabled;
                };

                Controls.Add(_spis);

                Label _commLabel = new Label();
                _commLabel.AutoSize = false;
                _commLabel.Text = "Коментар:";
                _commLabel.Height = 20;
                _commLabel.Width = 80;
                _commLabel.TextAlign = ContentAlignment.MiddleRight;
                _commLabel.Location = new Point(_spis.Location.X + _spis.Width + 10, Height - 23);

                Controls.Add(_commLabel);

                TextBox _comm = new TextBox();
                _comm.Margin = new Padding(0);
                _comm.Location = new Point(_commLabel.Location.X + _commLabel.Width + 10, Height - 23);
                _comm.Width = Width - _comm.Location.X - 7;
                _comm.MaxLength = 200;
                _comm.WordWrap = false;
                _comm.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                _comm.ForeColor = tb_count.ForeColor;

                _comm.TextChanged += (s, e) =>
                {
                    VALUE.COMM = !string.IsNullOrWhiteSpace(_comm.Text) ? _comm.Text.Trim() : null;
                };

                Controls.Add(_comm);
            }

            if (_input != null)
            {
                INPUT_SET(_input);
            }
            else
            {
                VALUE.COUNT = 1;
                VALUE.COST = 0f;
            }
        }

        private void ComboBoxColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var combo = (ComboBox)sender;
            var item = (ONE_COLOR_INFO)combo.Items[e.Index];

            // Малюємо фон
            using (var brush = new SolidBrush(item.COLOR))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            // Малюємо текст
            TextRenderer.DrawText(e.Graphics, item.ToString(), combo.Font,
                e.Bounds, Color.White, TextFormatFlags.Left);

            e.DrawFocusRectangle();
        }

        public void SetWidth(object sender, int newWidth)
        {
            Width = newWidth;
        }

        private void SetBackColorRecursive(Control parent, Color color)
        {
            foreach (Control child in parent.Controls)
            {
                child.BackColor = color;

                // Рекурсія для вкладених контролів
                if (child.HasChildren)
                {
                    SetBackColorRecursive(child, color);
                }
            }
        }

        private bool InitTYPES()
        {
            List<ONE_TYPE_INFO> _types = SERVICE_INFO.GetAllTypeNames(VALUE.ID);

            cb_type.Enabled = _types.Count > 0;
            cb_type.Visible = _types.Count > 0;

            if (cb_type.Enabled)
            {
                cb_type.Items.Clear();

                foreach (ONE_TYPE_INFO t in _types)
                {
                    cb_type.Items.Add(t);
                }

                cb_type.SelectedIndex = 0;
            }
            else
            {
                VALUE.TYPE_ID = 1;
                return false;
            }

            return true;
        }

        private bool InitCOLORS()
        {
            VALUE.COLOR_ID = null;

            List<ONE_COLOR_INFO> _colors = SERVICE_INFO
                .GetAllColorNames(VALUE.ID, VALUE.TYPE_ID);

            cb_color.Enabled = _colors.Count > 0;
            cb_color.Visible = _colors.Count > 0;

            if (cb_color.Enabled)
            {
                cb_color.Items.Clear();

                foreach (ONE_COLOR_INFO c in _colors)
                {
                    cb_color.Items.Add(c);
                }

                cb_color.SelectedIndex = 0;
            }
            else
            {
                return false;
            }

            return true;
        }

        private void INPUT_SET(SERVICE_ID_INFO _input)
        {
            VALUE = new VitratyInfo(_input);

            cb_type.Enabled = VALUE.TYPE_NAME != null;
            cb_type.Visible = VALUE.TYPE_NAME != null;

            if (cb_type.Enabled)
            {
                List<ONE_TYPE_INFO> _types = SERVICE_INFO.GetAllTypeNames(VALUE.ID);

                cb_type.Items.Clear();

                foreach (ONE_TYPE_INFO k in _types)
                {
                    cb_type.Items.Add(k);

                    if (VALUE.TYPE_ID == k.TYPE_ID) {
                        cb_type.SelectedItem = k; 
                    }
                }
            }

            cb_color.Enabled = VALUE.COLOR_ID != null;
            cb_color.Visible = VALUE.COLOR_ID != null;

            if (cb_color.Enabled)
            {
                cb_color.Items.Clear();

                List<ONE_COLOR_INFO> _colors = SERVICE_INFO
                    .GetAllColorNames(_input.ID, _input.TYPE_ID);

                foreach (ONE_COLOR_INFO k in _colors)
                {
                    cb_color.Items.Add(k);

                    if (VALUE.COLOR_ID == k.ID) {
                        cb_color.SelectedItem = k;
                    }
                }
            }

            tb_count.Text = _input.COUNT.ToString();

            tb_cost.Text = (_input.COST * DiscountKoef).ToString();
        }

        private void ComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;

            Control parent = this.Parent;
            while (parent != null && !(parent is SoldInterface))
                parent = parent.Parent;

            if (parent is SoldInterface container)
            {
                container.ScrollContainer(e.Delta);
            }
        }

        private void FloatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == '.')
            {
                e.KeyChar = ','; // замінюємо кому на крапку
            }

            // Дозволяємо цифри, керуючі клавіші (backspace), крапку або кому
            if (!char.IsControl(ch) && !char.IsDigit(ch) && ch != '.' && ch != ',')
            {
                e.Handled = true; // блокувати інші символи
            }

            // Заборонити вводити більше однієї крапки або коми
            TextBox tb = sender as TextBox;
            if ((ch == '.' || ch == ',') && (tb.Text.Contains(".") || tb.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void IntTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsControl(ch) && !char.IsDigit(ch))
            {
                e.Handled = true; // блокувати інші символи
            }
        }
    }
}
