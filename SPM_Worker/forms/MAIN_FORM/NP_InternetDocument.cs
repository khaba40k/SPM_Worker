using API_NovaPoshta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class NP_InternetDocument : UserControl
    {
        public float Cost { get; set; } = 0f;
        public event EventHandler KontrolOplatySelected;
        private NovaPoshta NP = new NovaPoshta("4c0e5cf1a2509ca7880c979a68b986a8",
                Path.Combine(Environment
                .GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "spm", "NP")
            );

        private int WarehouseNumber = 0;

        public NP_InternetDocument()
        {
            InitializeComponent();
        }

        public NP_InternetDocument(float cost, string addr)
        {
            InitializeComponent();
            Cost = cost;
            tb_CitySearch.Text = addr;
        }

        private void NP_InternetDocument_Load(object sender, System.EventArgs e)
        {
            dateTime.Value = DateTime.Now;

            cb_payer.Items.AddRange(new PayerInfo[] { 
                new PayerInfo{ Name = "Відправник", Value = "Sender" },
                new PayerInfo{ Name = "Отримувач", Value = "Recepient" }
            });

            cb_payer.SelectedIndex = 0;

            cb_pakList.Items.Add(new OptionsSeat(40, 30, 20));
            cb_pakList.Items.Add(new OptionsSeat(60, 40, 30));
            cb_pakList.Items.Add(new OptionsSeat(70, 40, 42));

            cb_pakList.SelectedIndex = 0;

            cb_pakList.SelectedIndexChanged += Cb_pakList_SelectedIndexChanged;

            rb_ko_yes.CheckedChanged += Rb_ko_CheckedChanged;
            rb_ko_no.CheckedChanged += Rb_ko_CheckedChanged;
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
                tb_ko.Text = "";
            }

            KontrolOplatySelected?.Invoke(this, EventArgs.Empty);
        }

        private void Cb_pakList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OptionsSeat _pak = cb_pakList.SelectedItem as OptionsSeat;

            tbPakLeng.Text = _pak.Length.ToString();
            tbPakWidth.Text = _pak.Width.ToString();
            tbPakHeig.Text = _pak.Height.ToString();

            tb_weight.Text = _pak.weigth.ToString();
        }

        private void tb_CitySearch_TextChanged(object sender, EventArgs e)
        {
            cbCityList.Items.Clear();
            cb_Warehouses.Items.Clear();

            AddressParts find = AddressParts.Parse(tb_CitySearch.Text);

            if (find.Number == null || !int.TryParse(find.Number, out WarehouseNumber))
            {
                WarehouseNumber = 0;
            }

            List<NP_CityInfo> _cities = NP.FindByCityName(find.CityName);

            if (find.Oblast != null)
            {
                _cities = _cities.Where(c => c.AreaDescription.ToLower()
                    .Contains(find.Oblast.ToLower()))
                    .ToList();
            }

            if (find.Rajon != null)
            {
                _cities = _cities.Where(c => c.RegionsDescription.ToLower()
                    .Contains(find.Rajon.ToLower()))
                    .ToList();
            }

            cbCityList.Items.AddRange(_cities.ToArray());

            if (cbCityList.Items.Count > 0)
            {
                cbCityList.SelectedIndex = 0;
            }
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
                    if (_wrh.NUMBER == WarehouseNumber)
                    {
                        cb_Warehouses.SelectedItem = _wrh;
                        break;
                    }
                }
            }
        }
    }

    public class PayerInfo
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
