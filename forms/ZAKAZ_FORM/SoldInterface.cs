using SPM_Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class SoldInterface : UserControl
    {
        public enum ControlLayoutMode
        {
            Default,         // займає весь простір
            LeaveBottomSpace // залишає 42 пікселі внизу
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ControlLayoutMode LayoutMode { get; set; } = ControlLayoutMode.Default;

        public List<VitratyInfo> PRODUCTS { get {

                List<VitratyInfo> _out = new List<VitratyInfo>();

                foreach (SoldCart _cart in container.Controls)
                {
                    if (_cart.VALUE.ID != -1)
                    _out.Add(_cart.VALUE);
                }

                _out.AddRange(OTHER_PRODUCTS);

                return _out;
            } }

        public int ATR { get; set; } = 0;

        private readonly List<VitratyInfo> OTHER_PRODUCTS = new List<VitratyInfo>();

        private readonly Color par, nepar;
        private bool ColorNePar = true;
        private float globalDiscountKoef = 1f;
        private bool WITH_COMM_SPIS = false;

        public event EventHandler CHANGED;
        public event EventHandler<int> WidthChanged;

        public bool HAS_VALUE { get {
                foreach (SoldCart c in container.Controls)
                {
                    if (c.HAS_CHANGE) return true;
                }

                return false;
            } }


        public SoldInterface()
        {
            InitializeComponent();

            nepar = Color.NavajoWhite;
            par = Color.Violet;

            container.ControlAdded += (s, e) => { 
                SetColor(s, e);
                SoldCart c = (SoldCart)e.Control;

                c.Width = container.ClientSize.Width - container.Padding.Horizontal;

                WidthChanged += c.SetWidth;

                c.ID_CHANGED += (s1, e1) => {
                    c.CanDelete = e1.ID > -1;
                    CHANGED?.Invoke(this, EventArgs.Empty); 
                };
            };

            container.ControlRemoved += SetColor;

            container.ClientSizeChanged += (s, e) => {
                WidthChanged?.Invoke(s, container.ClientSize.Width - container.Padding.Horizontal);
            };
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (Parent != null)
            {
                BeginInvoke((Action)(() =>
                {
                    if (LayoutMode == ControlLayoutMode.LeaveBottomSpace)
                    {
                        Dock = DockStyle.None;
                        Width = Parent.ClientSize.Width;
                        Height = Parent.ClientSize.Height - 42;
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    }
                    else
                    {
                        Dock = DockStyle.Fill;
                    }
                }));
            }
        }

        public void SetDiscount(byte _percent)
        {
            globalDiscountKoef = (100f - _percent) / 100f;

            foreach (SoldCart _cart in container.Controls)
            {
                _cart.DiscountKoef = globalDiscountKoef;
            }

            foreach (SERVICE_ID_INFO inf in OTHER_PRODUCTS)
            {
                inf.COST *= globalDiscountKoef;
            }
        }

        public void Add(SERVICE_ID_INFO _input)
        {
            _input.COST *= globalDiscountKoef;

            OTHER_PRODUCTS.Add(new VitratyInfo(_input));
        }

        public void Remove(int _id)
        {
            int _delInd = OTHER_PRODUCTS
                .FindIndex(d => (d.ID == _id) || (d.ID == 19 && d.TYPE_ID == _id));

            if (_delInd > -1)
            {
                OTHER_PRODUCTS.RemoveAt(_delInd);
            }
        }

        public void SET(List<KOMPLEKT> _input = null, int atr = 0, bool _WITH_COMM_SPIS = false, byte DiscountPercent = 0)
        {
            ATR = atr;
            globalDiscountKoef = (100 - DiscountPercent) / 100f;
            WITH_COMM_SPIS = _WITH_COMM_SPIS;

            container.SuspendLayout();

            container.Controls.Clear();

            SoldCart _cart; int counter = 1;

            if (_input != null)
            foreach (SERVICE_ID_INFO inf in _input)
            {
                if (inf.ATR >= 0)
                {
                        _cart = new SoldCart(inf, ATR, WITH_COMM_SPIS)
                        {
                            DiscountKoef = globalDiscountKoef
                        };

                        _cart.NUMBER = counter++;

                     if (counter % 2 == 0)
                     {
                         _cart.BackColor = par;
                     }

                     _cart.ID_CHANGED += CartUpdated;

                     container.Controls.Add(_cart);
                }
            }

            _cart = new SoldCart(null, atr, WITH_COMM_SPIS)
            {
                NUMBER = counter,
                CanDelete = false,
                DiscountKoef = globalDiscountKoef
            };

            _cart.ID_CHANGED += CartUpdated;

            container.Controls.Add(_cart);

            container.ResumeLayout();
        }

        private void SetColor(object sender, ControlEventArgs e)
        {
            ColorNePar = true;

            foreach (Control _c in container.Controls)
            {
                if (ColorNePar)
                {
                    _c.BackColor = nepar;
                }
                else
                {
                    _c.BackColor = par;
                }

                ColorNePar = !ColorNePar;
            }
        }

        public void ReNumeric()
        {
            int counter = 1;

            foreach (SoldCart _cart in container.Controls)
            {
                _cart.NUMBER = counter++;
            }
        }

        private void CartUpdated(object sender, ZakazEventArgs e)
        {
            if (!e.CHECKED &&
                    container.Controls.Count > 1
                    && container.Controls.Count != e.Index + 1)
            {
                container.Controls.RemoveAt(e.Index);
                ReNumeric();
            }
            else if (e.CHECKED && container.Controls.Count == e.Index + 1)
            {
                container.SuspendLayout();

                SoldCart _cart = new SoldCart(null, ATR, WITH_COMM_SPIS) { CanDelete = false, DiscountKoef = globalDiscountKoef };

                _cart.ID_CHANGED += CartUpdated;

                container.Controls.Add(_cart);

                ReNumeric();

                container.ResumeLayout();
            }
        }

        public void ScrollContainer(int delta)
        {
            if (container.VerticalScroll.Visible)
            {
                int newVal = container.VerticalScroll.Value - delta;
                newVal = Math.Max(container.VerticalScroll.Minimum, Math.Min(container.VerticalScroll.Maximum, newVal));
                container.VerticalScroll.Value = newVal;
                container.PerformLayout();
            }
        }
    }
}
