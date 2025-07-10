using System.Collections.Generic;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class ServiceCart : UserControl
    {
        private List<SERVICE_ID_INFO> ALL_INPUT_SERVICES;

        public SERVICE_ID_INFO SELECTED_SERVICE
        {
            get => _selectedService;
            set
            {
                PreSet = false;
                if (value == null) return;
                PreSet = true;
                _selectedService = value;

                if (_selectedService.ID != 19)
                {
                    rb1.Checked = true;
                }
                else
                {
                    rb2.Checked = true;
                }
            }
        }

        public bool PreSet { get; set; }

        private SERVICE_ID_INFO _selectedService;

        public int ID { get => _selectedService.ID; private set { } }
        public int TYPE_ID { get => _selectedService.TYPE_ID; private set { } }
        public int? COLOR_ID { get => _selectedService.COLOR_ID; private set { } }
        public float COST { get => _selectedService.COST; set { _selectedService.COST = value; } }
        public string NAME { get => _selectedService.NAME; private set { } }
        public string TYPE_NAME { get => _selectedService.TYPE_NAME; private set { } }
        public string COLOR_NAME { get => _selectedService.COLOR_NAME; private set { } }

        public bool SELECTED
        {
            get
            {
                return !rb0.Checked && (rb1.Checked || rb2.Checked);
            }
            private set { }
        }
        public bool IS_POSLUGA { get => _selectedService.IS_POSLUGA; private set { } }
        private bool CLOSED = false;

        /// <summary>
        /// Масив співставляє ComboBox.SelectedIndex <=> Type_ID
        /// </summary>
        private Dictionary<int, int> cur_Index_TypeID = new Dictionary<int, int>();
        private Dictionary<int, int?> cur_Index_ColorID = new Dictionary<int, int?>();

        public bool HAS_COLOR
        {
            get
            {
                return ALL_INPUT_SERVICES.FindIndex(c => c.COLOR_ID != null) > -1;
            }
            private set { }
        }
        public bool HAS_TYPE
        {
            get
            {
                return ALL_INPUT_SERVICES.FindIndex(t => t.TYPE_NAME != null) > -1;
            }
            private set { }
        }

        public SERVICE_ID_INFO OUT
        {
            get
            {
                if (rb1.Checked)
                {
                    return _selectedService;
                }
                else if (rb2.Checked)
                {
                    SERVICE_ID_INFO _ans = _selectedService;

                    _ans.TYPE_ID = _selectedService.ID;
                    _ans.ID = 19;
                    _ans.COLOR_ID = null;
                    return _ans;
                }
                return null;
            }
            private set { }
        }
        /// <summary>
        /// Рисовка пункту переобладнання за ID
        /// </summary>
        /// <param name="_input">Список продуктів за одним ID</param>
        public ServiceCart(List<SERVICE_ID_INFO> _input, bool _preSet = false)
        {
            PreSet = _preSet;

            ALL_INPUT_SERVICES = _input;

            if (_selectedService == null)
            {
                _selectedService = _input.Find(s => s.TYPE_ID == 1);

                if (_selectedService == null && _input.Count > 0)
                {
                    _selectedService = _input[0];
                }
                else if (_selectedService == null)
                {
                    return;
                }
            }

            InitializeComponent();

            cb_type.MouseWheel += comboBox_MouseWheel;
            cb_color.MouseWheel += comboBox_MouseWheel;

            if (_selectedService.IS_POSLUGA)
            {
                rb2.Enabled = false;
                rb2.Visible = false;
                if (!PreSet) rb0.Checked = false;
            }

            rb0.CheckedChanged += (s, e) =>
            {
               // if (HANDS_FREE) return;

                RadioButton _rb = (RadioButton)s;

                if (!_rb.Checked) return;

                Init();
            };

            rb1.CheckedChanged += (s, e) =>
            {
               // if (HANDS_FREE) return;

                RadioButton _rb = (RadioButton)s;

                if (!_rb.Checked) return;

                Init(1);
            };

            rb2.CheckedChanged += (s, e) =>
            {
               // if (HANDS_FREE) return;

                RadioButton _rb = (RadioButton)s;

                if (!_rb.Checked) return;

                Init(2);
            };

            cb_type.SelectedIndexChanged += (s, e) => {
               // if (HANDS_FREE) return;

                ComboBox _cb = (ComboBox)s;

                if (_cb.SelectedIndex == -1 && _cb.Items.Count == 1)
                {
                    _cb.SelectedIndex = 0;
                    return;
                }

                int _type = cur_Index_TypeID[cb_type.SelectedIndex];

                List<SERVICE_ID_INFO> _temp = ALL_INPUT_SERVICES
                .FindAll(ss => ss.TYPE_ID == _type);

                cb_color.Enabled = HAS_COLOR;
                cb_color.Visible = HAS_COLOR;

                if (HAS_COLOR)
                {
                    SetColors(_temp);

                    _selectedService = ALL_INPUT_SERVICES.Find(x =>
                    x.TYPE_ID == cur_Index_TypeID[cb_type.SelectedIndex]
                    && x.COLOR_ID == cur_Index_ColorID[cb_color.SelectedIndex]);
                }
                else
                {
                    _selectedService = ALL_INPUT_SERVICES.Find(x =>
                    x.TYPE_ID == cur_Index_TypeID[cb_type.SelectedIndex]);
                }

                tb_sum.Text = _selectedService.COST.ToString();
            };

            cb_color.SelectedIndexChanged += (s1, e1) =>
            {
               // if (HANDS_FREE) return;

                ComboBox _cb = (ComboBox)s1;

                if (_cb.SelectedIndex == -1 || cur_Index_ColorID.Count == 0) return;

                int _typeID = cb_type.SelectedIndex == -1 ?
                1 : cur_Index_TypeID[cb_type.SelectedIndex];

                _selectedService = ALL_INPUT_SERVICES.Find(x =>
                x.TYPE_ID == _typeID
                && x.COLOR_ID == cur_Index_ColorID[cb_color.SelectedIndex]);
            };

            Init();
        }

        private void SetColors(List<SERVICE_ID_INFO> _input)
        {
            if (!HAS_COLOR) return;

            int counter = 0, _slIndex = 0;

            cb_color.Items.Clear();
            cur_Index_ColorID.Clear();

            foreach (SERVICE_ID_INFO _c in _input)
            {
                cur_Index_ColorID.Add(counter++, _c.COLOR_ID != null ? _c.COLOR_ID : -1);
                cb_color.Items.Add(_c.COLOR_NAME);

                if (PreSet && _selectedService.COLOR_ID == _c.COLOR_ID)
                {
                    _slIndex = counter - 1;
                    PreSet = false;
                }
            }

            cb_color.SelectedIndex = _slIndex;
        }
        /// <summary>
        /// Перемальовка (Своє/Так/Ні) при перемиканні radioButton
        /// </summary>
        /// <param name="_initInfo">0 - ні, 1 - так, 2 - своє</param>
        private void Init(int _initInfo = 0)
        {
            bool ShowCb = _initInfo == 1;

            cb_type.Visible = HAS_TYPE && ShowCb;
            cb_type.Enabled = HAS_TYPE && ShowCb;
            cb_color.Enabled = HAS_COLOR && ShowCb;
            cb_color.Visible = HAS_COLOR && ShowCb;

            cur_Index_TypeID.Clear();
            cur_Index_ColorID.Clear();

            cb_type.Items.Clear();
            cb_color.Items.Clear();

            panelBody.Visible = ShowCb;
            panelBody.Enabled = ShowCb;

            if (!ShowCb)
            {
                if (!CLOSED)
                {
                    Height -= panelBody.Height;
                    CLOSED = true;
                }

                if (!PreSet)
                _selectedService = ALL_INPUT_SERVICES.Find(s => s.TYPE_ID == 1);

                title_service.Text = _selectedService.NAME;

                tb_sum.Text = _selectedService.COST.ToString();

                return;
            }
            else if (CLOSED && (HAS_TYPE || HAS_COLOR))
            {
                Height += 33;
                CLOSED = false;
            }

            int counter = 0;

            foreach (SERVICE_ID_INFO info in ALL_INPUT_SERVICES)
            {
                if (!cur_Index_TypeID.ContainsValue(info.TYPE_ID))
                {
                    cur_Index_TypeID.Add(counter++, info.TYPE_ID);

                    if (info.TYPE_NAME != null)
                    {
                        cb_type.Items.Add(info.TYPE_NAME);

                        if (PreSet && _selectedService.TYPE_ID == info.TYPE_ID)
                            cb_type.SelectedIndex = counter - 1;
                    }

                    
                }
            }

            if (cb_type.Items.Count == 0)
            {
                cb_type.Enabled = false;
                cb_type.Visible = false;

                if (HAS_COLOR)
                {
                    SetColors(ALL_INPUT_SERVICES.FindAll(x => x.TYPE_ID == _selectedService.TYPE_ID));
                }
            }

        }

        private void comboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

    }
}
