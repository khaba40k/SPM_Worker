using SPM_Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class ServiceCart : UserControl
    {
        private List<SERVICE_ID_INFO> ALL_INPUT_SERVICES;

        public SERVICE_ID_INFO SELECTED_SERVICE => new SERVICE_ID_INFO {
            ID = CART_SERVICE_ID,
            TYPE_ID = CART_TYPE_ID,
            COLOR_ID = CART_COLOR_ID,
            COST = CART_COST
        };

        private string SERVICE_NAME => ALL_INPUT_SERVICES?.First()?.NAME.ToUpper() ?? "";
        public int CART_SERVICE_ID => !SELECTED_ZMV ? (ALL_INPUT_SERVICES?.First()?.ID ?? -1) : 19;
        private int CART_TYPE_ID = 1;
        private int? CART_COLOR_ID = null;
        private float CART_COST
        {
            get => _cart_cost; set
            {
                _cart_cost = value;

                tb_sum.Text = value.ToString();
            }
        }
        private float _cart_cost = 0f;

        private SERVICE_ID_INFO DEFF_VALUE { get; set; }

        private bool DeffValueLoaded = false;

        public bool HAS_DEFFAULT_VALUE => DEFF_VALUE != null;
        public bool HAS_GLOBAL_DEFF_VALUES
        {
            get => _has_global_deff; set
            {
                _has_global_deff = value;
                if (IS_POSLUGA && !HAS_DEFFAULT_VALUE)
                    SELECTED_NO = value;
            }
        }
        private bool _has_global_deff = false;
        public bool HAS_CHANGE => SELECTED_ANY && !SELECTED_NO;
        private bool LockDeffCost = true;
        public float DiscountKoef
        {
            get => _globDiscKoef; set
            {
                if (_globDiscKoef != value)
                {
                    _globDiscKoef = value;

                    if ((DeffValueLoaded && !LockDeffCost) || !DeffValueLoaded)
                         CART_COST *= _globDiscKoef;
                }
            }
        }
        private float _globDiscKoef = 1f;

        private bool SELECTED_ZMV
        {
            get => rb2?.Checked ?? false; set
            {
                if (rb2.Checked == value) return;

                rb2.Checked = value;
            }
        }
        public bool SELECTED_ANY => SELECTED_YES ^ SELECTED_NO ^ SELECTED_ZMV;
        public bool SELECTED_YES
        {
            get => rb1.Checked; set
            {
                if (rb1.Checked == value) return;

                rb1.Checked = value;
            }
        }
        public bool SELECTED_NO
        {
            get=> rb0.Checked; set
            {
                if (rb0.Checked == value) return;

                rb0.Checked = value;
            }
        }
        public bool IS_POSLUGA => ALL_INPUT_SERVICES.First().IS_POSLUGA;
        private bool CLOSED = false;

        public bool HAS_COLOR => ALL_INPUT_SERVICES.FindIndex(c => c.COLOR_ID != null) > -1;
        public bool HAS_TYPE => ALL_INPUT_SERVICES.FindIndex(t => t.TYPE_NAME != null) > -1;

        public int DeffaultColor = -1;

        public event EventHandler<ZakazEventArgs> ID_CHANGED;
        public event EventHandler<int> TypeChanged;
        public event EventHandler<int?> ColorChanged;
        public event EventHandler<float> PriceChanged;

        /// <summary>
        /// Рисовка пункту переобладнання за ID
        /// </summary>
        /// <param name="ServiceTypeVariants">Список продуктів за одним ID</param>
        public ServiceCart(List<SERVICE_ID_INFO> ServiceTypeVariants, SERVICE_ID_INFO _deff = null)
        {
            InitializeComponent();

            ALL_INPUT_SERVICES = new List<SERVICE_ID_INFO>(ServiceTypeVariants);

            DEFF_VALUE = _deff != null ? new SERVICE_ID_INFO(_deff) : null;

            if (IS_POSLUGA)
            {
                rb2.Enabled = false;
                rb2.Visible = false;
            }

            #region Mouse and draw
            cb_type.MouseWheel += ComboBox_MouseWheel;
            cb_color.MouseWheel += ComboBox_MouseWheel;
            cb_color.DrawItem += ComboBoxColor_DrawItem;
            #endregion

            TypeChanged += (s, type) =>
            {
                CART_TYPE_ID = type;

                cb_color.Visible = SetColorList(CART_TYPE_ID);

                CART_COST = DiscountKoef * (ALL_INPUT_SERVICES.Find(f => f.TYPE_ID == type)?.COST ?? 0f);
            };

            ColorChanged += (s, color) =>
            {
                CART_COLOR_ID = color;
            };

            PriceChanged += (s, e) =>
            {
                _cart_cost = e;

                if (_cart_cost < (!SELECTED_ZMV ? (ALL_INPUT_SERVICES
                     .Find(f => f.TYPE_ID == CART_TYPE_ID)?.COST ?? 0f)
                     : SERVICE_INFO.GetCost(19)))
                {
                    tb_sum.ForeColor = Color.Green;
                }
                else
                {
                    tb_sum.ForeColor = Color.Black;
                }
            };

            tb_sum.TextChanged += (s, e) => {
                
                if (float.TryParse(tb_sum.Text, out float _out))
                {
                    PriceChanged?.Invoke(this, _out);
                }
                else
                {
                    tb_sum.Text = "0";
                    tb_sum.SelectAll();
                }
            };

            cb_type.SelectedIndexChanged += (s, e) => {
                ComboBox _cb = (ComboBox)s;

                if (_cb.SelectedIndex == -1 && _cb.Items.Count == 1)
                {
                    _cb.SelectedIndex = 0;
                }

                ONE_TYPE_INFO _type = (ONE_TYPE_INFO)_cb.SelectedItem;

                TypeChanged?.Invoke(this, _type.TYPE_ID);
            };

            cb_color.SelectedIndexChanged += (s1, e1) =>
            {
                ComboBox _cb = (ComboBox)s1;

                if (_cb.SelectedIndex == -1) return;

                ONE_COLOR_INFO _color = (ONE_COLOR_INFO)_cb.SelectedItem;

                Refresh();

                ColorChanged?.Invoke(this, _color?.ID);
            };

            INIT_CART();

            ID_CHANGED += (s, e) => {
                if (e.CHECKED)
                {
                    tb_sum.Font = new Font(tb_sum.Font, FontStyle.Bold);
                }
                else
                {
                    tb_sum.Font = new Font(tb_sum.Font, FontStyle.Regular | FontStyle.Italic);
                }
            };

            rb0.CheckedChanged += (s, e) =>
            {
                RadioButton _rb = (RadioButton)s;

                if (!_rb.Checked) return;

                INIT_CART();

                ID_CHANGED?.Invoke(this, new ZakazEventArgs(CART_SERVICE_ID) { CHECKED = false });
            };

            rb1.CheckedChanged += (s, e) =>
            {
                RadioButton _rb = (RadioButton)s;

                if (!_rb.Checked) return;

                INIT_CART();

                ID_CHANGED?.Invoke(this, new ZakazEventArgs(CART_SERVICE_ID) { CHECKED = true });
            };

            rb2.CheckedChanged += (s, e) =>
            {
                RadioButton _rb = (RadioButton)s;

                if (!_rb.Checked) return;

                INIT_CART();

                ID_CHANGED?.Invoke(this, new ZakazEventArgs(CART_SERVICE_ID) { CHECKED = true });
            };
        }

        public void UnLockDeffCost()
        {
            LockDeffCost = false;
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

        /// <summary>
        /// Перемальовка (Своє/Так/Ні) при перемиканні radioButton
        /// </summary>
        /// <param name="_initInfo">0 - ні, 1 - так, 2 - своє</param>
        private void INIT_CART()
        {
            if (SELECTED_YES)
            {
                CART_TYPE_ID = 1;
                CART_COST = DiscountKoef * ALL_INPUT_SERVICES
                    .Find(f => f.TYPE_ID == CART_TYPE_ID)?
                    .COST ?? 0f;
            } 
            else if (SELECTED_ZMV)
            {
                CART_TYPE_ID = ALL_INPUT_SERVICES.First().ID;
                CART_COST = SERVICE_INFO.GetCost(19) * DiscountKoef;
            }
            else
            {
                CART_TYPE_ID = 1;
                CART_COST = DiscountKoef * ALL_INPUT_SERVICES
                    .Find(f => f.TYPE_ID == 1)?
                    .GetDeffaultCost(true) ?? 0f;
            }

            if (!SetDEFF()) SetBottomMenu();

            if (!SELECTED_NO)
            {
                BorderStyle = BorderStyle.FixedSingle;
                Refresh();
            }
            else
            {
                BorderStyle = BorderStyle.None;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ONE_COLOR_INFO _color = (ONE_COLOR_INFO)cb_color.SelectedItem;

            using (Font _f = new Font("Arial", 15, FontStyle.Bold))
            using (SolidBrush _b = new SolidBrush(Color.White))
            {
                e.Graphics.DrawString(SERVICE_NAME, _f,
                  _b, new PointF(4, 4));

                if (!SELECTED_NO && SELECTED_ANY)
                {
                    _b.Color = _color?.COLOR ?? Color.Green;

                    e.Graphics.DrawString(SERVICE_NAME, _f,
                    _b, new PointF(5, 6));
                }
            }
        }

        private void ComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            // Блокуємо стандартну зміну ComboBox
            ((HandledMouseEventArgs)e).Handled = true;

            // Знаходимо контейнер PereoblInterface серед батьків
            Control parent = this.Parent;
            while (parent != null && !(parent is PereoblInterface))
                parent = parent.Parent;

            if (parent is PereoblInterface container)
            {
                // Передаємо прокрутку в контейнер
                container.ScrollContainer(e.Delta);
            }
        }
        private bool SetDEFF()
        {
            if (!HAS_DEFFAULT_VALUE || DeffValueLoaded) return false;

            if (DEFF_VALUE.ID != 19)
            {
                SELECTED_YES = true;
                SetBottomMenu();

                if (HAS_TYPE)
                    foreach (ONE_TYPE_INFO _type in cb_type.Items)
                    {
                        if (_type.TYPE_ID == DEFF_VALUE?.TYPE_ID)
                        {
                            cb_type.SelectedItem = _type;
                            break;
                        }
                    }

                if (HAS_COLOR)
                    foreach (ONE_COLOR_INFO _color in cb_color.Items)
                    {
                        if (_color.ID == DEFF_VALUE?.COLOR_ID)
                        {
                            cb_color.SelectedItem = _color;
                            break;
                        }
                    }
            }
            else
            {
                CART_TYPE_ID = ALL_INPUT_SERVICES.First().ID;

                SELECTED_ZMV = true;

                SetBottomMenu();
            }

            tb_sum.Font = new Font(tb_sum.Font, FontStyle.Bold);

            CART_COST = DEFF_VALUE.COST;

            //DEFF_VALUE = null;

            DeffValueLoaded = true;

            return true;
        }
        private bool SetBottomMenu()
        {
            bool ans = SELECTED_YES && (HAS_TYPE || HAS_COLOR);

            if (!ans)
            {
                if (!CLOSED)
                {
                    Height /= 2;
                    CLOSED = true;
                }
            }
            else if (CLOSED)
            {
                Height *= 2;
                CLOSED = false;
            }

            if (ans)
            {
                cb_type.Visible = SetTypeList();
                cb_color.Visible = SetColorList(1);
            }
            else
            {
                cb_type.Visible = false;
                cb_color.Visible = false;
            }

            return ans;
        }
        private bool SetTypeList(int deff_type = 1)
        {
            cb_type.Items.Clear();

            if (!HAS_TYPE || SELECTED_NO) return false;

            List<SERVICE_ID_INFO> _types = ALL_INPUT_SERVICES
                    .FindAll(x => x.TYPE_NAME != null)
                    .GroupBy(g => g.TYPE_ID)
                    .Select(x => x.First())
                    .ToList();

            foreach (SERVICE_ID_INFO info in _types)
            {
                ONE_TYPE_INFO _item = new ONE_TYPE_INFO()
                {
                    SERV_ID = info.ID,
                    TYPE_ID = info.TYPE_ID,
                    NAME = info.TYPE_NAME
                };

                cb_type.Items.Add(_item);

                if (_item.TYPE_ID == deff_type) cb_type.SelectedItem = _item;
            }

            return cb_type.Items.Count > 0;
        }

        private bool SetColorList(int type, int? deff_color = null)
        {
            cb_color.Items.Clear();

            if (!HAS_COLOR) return false;

            foreach (SERVICE_ID_INFO _c in ALL_INPUT_SERVICES.Where(s=>s.TYPE_ID == type))
            {
                if (_c.COLOR_NAME != null)
                {
                    ONE_COLOR_INFO _item = new ONE_COLOR_INFO() { 
                       ID = (int)_c.COLOR_ID,
                       NAME = _c.COLOR_NAME
                    };

                    cb_color.Items.Add(_item);

                    if (cb_color.SelectedIndex < 0 && (deff_color == _item.ID 
                        || (deff_color == null && DeffaultColor == _item.ID)))
                    {
                        cb_color.SelectedItem = _item;
                    }
                }
            }

            if (cb_color.Items.Count > 0 && cb_color.SelectedIndex == -1)
            {
                cb_color.SelectedIndex = 0;
            }

            return cb_color.Items.Count > 0;
        }

        private void ServiceCart_MouseClick(object sender, MouseEventArgs e)
        {
            if ((SELECTED_NO || !SELECTED_ANY) && e.Button == MouseButtons.Left)
            {
                SELECTED_YES = true;
            }else if (!SELECTED_NO && e.Button == MouseButtons.Right)
            {
                SELECTED_NO = true;
            }
        }

        public override string ToString()
        {
            return SERVICE_NAME + " " + CART_COST.ToString("F2");
        }
    }
  
}
