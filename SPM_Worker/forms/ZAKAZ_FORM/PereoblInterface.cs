using SPM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class PereoblInterface : UserControl
    {
        private List<SERVICE_ID_INFO> InputList = null;
        public List<SERVICE_ID_INFO> PRODUCTS { get => _ANSWER(); }
        private List<SERVICE_ID_INFO> OTHER_PRODUCTS = new List<SERVICE_ID_INFO>();
        public event EventHandler CHANGED;
        private float globalDiscountKoef = 1f;
        public bool HAS_UNCHECKED { get {

                bool _out = false;

                foreach (ServiceCart _cart in container.Controls)
                {
                    if (!_cart.SELECTED_ANY)
                    {
                        _out = true;
                        break;
                    }
                }

                return _out;

            } }

        public bool HAS_VALUE { get { 
            
                foreach (ServiceCart c in container.Controls)
                {
                    if (c.HAS_CHANGE) return true;
                }

                return false;
            } }

        public PereoblInterface()
        {
            InitializeComponent();

            container.ControlAdded += (s, e) => {
                ServiceCart c = (ServiceCart)e.Control;

                c.ID_CHANGED += (s1, e1) => { CHANGED?.Invoke(this, EventArgs.Empty); };
            };
        }

        public void Add(SERVICE_ID_INFO _input)
        {
            OTHER_PRODUCTS.Add(_input);
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

        public void SET(List<KOMPLEKT> _inp = null, byte PercentDiscount = 0)
        {
            if (_inp != null)
            {
                InputList = _inp.Select(x => (SERVICE_ID_INFO)x).ToList();
            }

            ServiceCart temp;

            List<SERVICE_ID_INFO> products = SERVICE_INFO.SERVICE_LIST
                .FindAll(s => ((s.ATR & 2) == 2) && s.ATR >= 0);

            int[] _IDs = SERVICE_INFO.GetAllID(products);

            List<ServiceCart> _col = new List<ServiceCart>();

            SERVICE_ID_INFO _def;
            float _discKoef = (100 - PercentDiscount) / 100f;

            foreach (int _id in _IDs)
            {
                _def =  InputList?.Find(d => (d.ID == _id) 
                          || (d.ID == 19 && d.TYPE_ID == _id));

                temp = new ServiceCart(products
                    .FindAll(i => i.ID == _id), _def)
                { HAS_GLOBAL_DEFF_VALUES = InputList != null,
                DiscountKoef  = _discKoef };

                _col.Add(temp);
            }

            foreach (ServiceCart _cart in _col)
            {
                _cart.ColorChanged += SetDeffColor;
                container.Controls.Add(_cart);
            }
        }

        void SetDeffColor(object sender, int? ColorIndex)
        {
            foreach (ServiceCart _cart in container.Controls)
            {
                _cart.DeffaultColor = ColorIndex ?? 0;
            }
        }

        public void SetDiscount(byte _percent, bool onlyNewest = false)
        {
            globalDiscountKoef = (100f - _percent) / 100f;

            foreach (ServiceCart _cart in container.Controls)
            {
                _cart.UnLockDeffCost();
                _cart.DiscountKoef = globalDiscountKoef;
            }

            foreach (SERVICE_ID_INFO _inf in OTHER_PRODUCTS)
            {
                _inf.COST *= globalDiscountKoef;
            }
        }

        private List<SERVICE_ID_INFO> _ANSWER()
        {
            List<SERVICE_ID_INFO> _ans = new List<SERVICE_ID_INFO>();

            foreach (ServiceCart _cart in container.Controls)
            {
                if (_cart.SELECTED_SERVICE != null
                    && _cart.SELECTED_ANY 
                    && !_cart.SELECTED_NO)
                {
                    _ans.Add(_cart.SELECTED_SERVICE);
                }
            }

            _ans.AddRange(OTHER_PRODUCTS);

            return _ans;
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

