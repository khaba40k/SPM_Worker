using API_NovaPoshta;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class NP_InternetDocument : UserControl
    {
        public float Cost
        {
            get => _cost; set
            {
                _cost = value;

                tb_cost.Text = _cost.ToString();

                if (tb_ko.Enabled) tb_ko.Text = _cost.ToString();
            }
        }

        public string ZakazNumber { set { tb_comm.Text += $" №{value}"; } }

        private float _cost = 0f;

        public event EventHandler<bool> ValueChanged;
        private NovaPoshta NP = new NovaPoshta("4c0e5cf1a2509ca7880c979a68b986a8",
                Path.Combine(Environment
                .GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "spm", "NP")
            );

        private int WarehouseNumber = 0;
        private bool pakParametrLock = false;

        public NP_InternetDocument()
        {
            InitializeComponent();
        }

        public NP_InternetDocument(float cost, string addr)
        {
            InitializeComponent();
            Cost = cost;
            tb_CitySearch.Text = addr;
            SetAddres();
        }

        public void SetAddr(string Addr)
        {
            tb_CitySearch.Text = Addr;

            SetAddres();
        }

        private void NP_InternetDocument_Load(object sender, EventArgs e)
        {
            dateTime.Value = DateTime.Now;

            cb_payer.Items.AddRange(new PayerInfo[] { 
                new PayerInfo { Name = "Одержувач", Value = "Recipient" },
                new PayerInfo { Name = "Відправник", Value = "Sender" }
            });

            cb_payer.SelectedIndex = 0;

            cb_pakList.Items.Add(new OptionsSeat("Середня", 40, 30, 20));
            cb_pakList.Items.Add(new OptionsSeat("Велика <20кг", 60, 40, 30));
            cb_pakList.Items.Add(new OptionsSeat("Велика <30кг", 70, 40, 42));

            cb_pakList.SelectedIndexChanged += Cb_pakList_SelectedIndexChanged;

            cb_pakList.SelectedIndex = 0;

            rb_ko_yes.CheckedChanged += Rb_ko_CheckedChanged;
            rb_ko_no.CheckedChanged += Rb_ko_CheckedChanged;

            tbPakLeng.TextChanged += tbPakParametrChanged;
            tbPakWidth.TextChanged += tbPakParametrChanged;
            tbPakHeig.TextChanged += tbPakParametrChanged;

            tbPakLeng.KeyPress += floatTextBox_KeyPress;
            tbPakWidth.KeyPress += floatTextBox_KeyPress;
            tbPakHeig.KeyPress += floatTextBox_KeyPress;

            tb_cost.KeyPress += floatTextBox_KeyPress;
            tb_ko.KeyPress += floatTextBox_KeyPress;
            tb_weight.KeyPress += floatTextBox_KeyPress;

            tb_comm.TextChanged += (s, ea) =>
            {
                ValueChanged?.Invoke(this, Verification());
            };
        }

        private void Rb_ko_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton _rb = sender as RadioButton;

            if (_rb.Tag.ToString() == "1")
            {
                tb_ko.Enabled = true;
                tb_ko.Text = Cost.ToString();
            }
            else
            {
                tb_ko.Enabled = false;
                tb_ko.Text = "0";
            }

            ValueChanged?.Invoke(this, Verification());
        }

        private void floatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            string _txt = (sender as TextBox)?.Text ?? "";

            if (string.IsNullOrWhiteSpace(_txt))
            {
                e.KeyChar = '0';
                return;
            }

            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                e.KeyChar = separator[0];

                if (_txt.Contains(separator)) e.Handled = true;
            }
            else if (!char.IsControl(e.KeyChar) 
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }else if (!char.IsControl(e.KeyChar) 
                && _txt.IndexOf(separator) > -1
                && _txt.IndexOf(separator) == _txt.Length - 3)
            {
                e.Handled = true;
            }

            ValueChanged?.Invoke(this, Verification());
        }

        private void Cb_pakList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _cb = sender as ComboBox;
            OptionsSeat _pak = _cb.SelectedItem as OptionsSeat;

            pakParametrLock = true;

            tbPakLeng.Text = _pak.volumetricLength.ToString();
            tbPakWidth.Text = _pak.volumetricWidth.ToString();
            tbPakHeig.Text = _pak.volumetricHeight.ToString();

            pakParametrLock = false;

            tb_weight.Text = _pak.Weight.ToString();

            ValueChanged?.Invoke(this, Verification());
        }

        private void tbPakParametrChanged(object sender, EventArgs e)
        {
            if (pakParametrLock) return;

            try
            {
                OptionsSeat _op = new OptionsSeat()
                {
                    volumetricLength = byte.Parse(tbPakLeng.Text),
                    volumetricWidth = byte.Parse(tbPakWidth.Text),
                    volumetricHeight = byte.Parse(tbPakHeig.Text)
                };

                tb_weight.Text = _op.Weight.ToString();

            }
            catch
            {
                tb_weight.Text = "0";
            }

            ValueChanged?.Invoke(this, Verification());
        }

        private void cbCityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCityList.SelectedIndex < 0) return;

            NP_CityInfo _city = cbCityList.SelectedItem as NP_CityInfo;

            cb_Warehouses.Items.AddRange(NP.GetWarehouseList(_city.Ref).ToArray());

            if (WarehouseNumber > 0)
            {
                foreach (WrhInfo _wrh in cb_Warehouses.Items)
                {
                    if (_wrh.NUMBER == WarehouseNumber.ToString())
                    {
                        cb_Warehouses.SelectedItem = _wrh;
                        break;
                    }
                }
            }

            ValueChanged?.Invoke(this, Verification());
        }

        private void NP_InternetDocument_VisibleChanged(object sender, EventArgs e)
        {
            rb_ko_yes.Checked = false;
            rb_ko_no.Checked = false;
        }

        public IDoc_TTN_INFO INFO()
        {
            byte LWH = 0;

            IDoc_TTN_INFO _ans = new IDoc_TTN_INFO
            {
                Date = dateTime.Value,
                Cost = tb_cost.Text.Trim(),
                ControlOplaty = tb_ko.Text.Trim(),
                Payer = (cb_payer.SelectedItem as PayerInfo)?.Value,
                Comm = tb_comm.Text.Trim(),
                Seat = new OptionsSeat
                {
                    volumetricLength = byte.TryParse(tbPakLeng.Text.Trim(), out LWH) ? LWH : byte.MinValue,
                    volumetricWidth = byte.TryParse(tbPakWidth.Text.Trim(), out LWH) ? LWH : byte.MinValue,
                    volumetricHeight = byte.TryParse(tbPakHeig.Text.Trim(), out LWH) ? LWH : byte.MinValue,
                    weight = tb_weight.Text.Trim()
                },
                Weight = tb_weight.Text.Trim(),
                RecepientCitiRef = (cbCityList.SelectedItem as NP_CityInfo)?.Ref,
                RecepientWarehouse = (cb_Warehouses.SelectedItem as WrhInfo)?.Ref
            };

            return _ans;
        }

        private bool Verification()
        {
            if (!rb_ko_yes.Checked && !rb_ko_no.Checked) return false; 

            string[] _tb = new string[] 
            {
                tb_cost.Text,
                tb_ko.Text,
                tbPakLeng.Text,
                tbPakWidth.Text,
                tbPakHeig.Text,
                tb_weight.Text
            };
            
            foreach (string _temp in _tb)
            {
                if (!float.TryParse(_temp, out _)) return false;
            }

            if (string.IsNullOrWhiteSpace(tb_comm.Text)) return false;

            if (cbCityList.SelectedIndex == -1) return false;
            if (cb_Warehouses.SelectedIndex == -1) return false;

            return true;
        }

        private void but_search_Click(object sender, EventArgs e)
        {
            SetAddres();
        }

        private void SetAddres()
        {
            cbCityList.Items.Clear();
            cb_Warehouses.Items.Clear();

            if (tb_CitySearch.Text.TrimStart().Length < 3
                || string.IsNullOrWhiteSpace(tb_CitySearch.Text)) return;

            AddressParts find = AddressParts.Parse(tb_CitySearch.Text);

            if (find.Number == null || !int.TryParse(find.Number, out WarehouseNumber))
            {
                WarehouseNumber = 0;
            }

            List<NP_CityInfo> _cities = NP.FindByCityName(find.CityName, NovaPoshta.FindIn.LocalCitiFile);

            List<FindedCiti> _fullCities = _cities.Select(c => FindedCiti.FromBase(c)).ToList();

            if (find.Oblast != null)
            {
                _fullCities = _fullCities.Where(c => c.AreaDescription.ToLower()
                    .Contains(find.Oblast.ToLower()))
                    .ToList();
            }

            cbCityList.Items.AddRange(_fullCities.ToArray());

            if (cbCityList.Items.Count > 0)
            {
                cbCityList.SelectedIndex = 0;
            }
        }
    }

    public class PayerInfo
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }

    public class IDoc_TTN_INFO {
        public DateTime Date;
        public string Cost;
        public string ControlOplaty;
        public string Payer;
        public string Comm;
        public OptionsSeat Seat;
        public string Weight;
        public string RecepientCitiRef;
        public string RecepientWarehouse;
    }

}
