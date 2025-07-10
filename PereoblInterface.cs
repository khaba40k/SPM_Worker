using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace SPM_Worker
{
    public partial class PereoblInterface : UserControl
    {
        public List<SERVICE_ID_INFO> PRODUCTS { get => _ANSWER().INFO; set { } }
        public PereoblInterface()
        {
            InitializeComponent();
        }

        public void SET(List<KOMPLEKT> _inp = null)
        {
            List<SERVICE_ID_INFO> _input = null;

            if (_inp != null)
            {
                _input = _inp.Select(x => (SERVICE_ID_INFO)x).ToList();
            }

            ServiceCart temp;

            List<SERVICE_ID_INFO> products = SERVICE_INFO.SERVICE_LIST
                .FindAll(s => ((s.ATR & 2) == 2) && s.ATR >= 0);

            int[] _IDs = SERVICE_INFO.GetAllID(products);

            List<ServiceCart> _col = new List<ServiceCart>();

            foreach (int _id in _IDs)
            {
                temp = new ServiceCart(products.FindAll(i => i.ID == _id), _input != null);

                _col.Add(temp);
            }

            _col.Sort((x, y) => y.IS_POSLUGA.CompareTo(x.IS_POSLUGA));

            foreach (ServiceCart _cart in _col)
            {
                _cart.Name = $"Cart{_cart.ID}";

                container.Controls.Add(_cart);

                if (_input != null)
                {
                    ServiceCart _c = (ServiceCart)container
                         .Controls.Find($"Cart{_cart.ID}", false)[0];

                    _c.SELECTED_SERVICE = _input.Find(i => i.ID == _cart.ID);
                }
            }
        }

        private SERVICE_ID_LIST _ANSWER()
        {
            SERVICE_ID_LIST _ans = new SERVICE_ID_LIST();

            foreach (Control _c in container.Controls)
            {
                ServiceCart _cart = (ServiceCart)_c;

                if (_cart.SELECTED_SERVICE != null &&
                    _cart.SELECTED)
                {
                    _ans.INFO.Add(_cart.OUT);
                }
            }

            return _ans;
        }

    }
}

