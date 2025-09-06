using API_NovaPoshta;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPM_Worker
{
    public partial class NP_form : Form
    {
        public AddressParts ANSWER { get {
                AddressParts _out = new AddressParts();
                WrhInfo _wrh = (WrhInfo)(cb_vidd.SelectedItem);
                _out.Number = _wrh != null ? _wrh.NUMBER.ToString() 
                    : (tb_number.Text.Trim() != string.Empty ? tb_number.Text.Trim():null);

                NP_CityInfo _city = (NP_CityInfo)cb_city.SelectedItem;
                _out.CityName = _city != null ? _city.Description : null;
                _out.CityType = _city != null ? _city.SettlementTypeDescription : null;

                NP_Descr_Ref _reg = (NP_Descr_Ref)cb_rjn.SelectedItem;
                _out.Rajon = _reg != null ? _reg.Description:null;

                NP_Descr_Ref _area = (NP_Descr_Ref)cb_obl.SelectedItem;
                _out.Oblast = _area != null ? _area.Description:null;

                return _out;
            } }

        private List<NP_Descr_Ref> _CurrentAreas = new List<NP_Descr_Ref>();

        private NovaPoshta NOVA_POSHTA = new NovaPoshta(
            "4c0e5cf1a2509ca7880c979a68b986a8",
            Path.Combine(new string[3] {
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "spm",
                "NP"
            })
        );

        private System.Windows.Forms.Timer _ButUpdateTimer;

        private ProgressBar NPProgressBar;

        public NP_form(string input)
        {
            InitializeComponent();

            if (!NOVA_POSHTA.IS_ACTUAL_LOCAL_CITIES ||
                !NOVA_POSHTA.IS_ACTUAL_LOCAL_AREAS_ALL)
            {
                butUpdate.Visible = true;
                NOVA_POSHTA.ReportProgress = new Action<int>(OnProgressReported);

                NOVA_POSHTA.UpdateFinished += OnUpdateFinished;

                butUpdate.Click += (s, e) =>
                {
                    StartUpdate();
                };

                _ButUpdateTimer = new System.Windows.Forms.Timer();

                _ButUpdateTimer.Interval = 1000;

                _ButUpdateTimer.Tick += (s, e) => {
                    butUpdate.BackColor = (butUpdate.BackColor == Color.Maroon ?
                    Color.Brown:Color.Maroon);
                };

                _ButUpdateTimer.Start();
            }

            NPProgressBar = new ProgressBar
            {
                Step = 1,
                Size = Size,
                Minimum = 0,
                Style = ProgressBarStyle.Continuous,
                Location = new Point(0, 0)
            };
            Controls.Add(NPProgressBar);
            NPProgressBar.Visible = false;
            NPProgressBar.BringToFront();

            cb_obl.SelectedIndexChanged += (s, e) =>
            {
                NP_Descr_Ref _item = (NP_Descr_Ref)cb_obl.SelectedItem;

                SetRegionsByRef(_item.Ref);

                cb_city.Items.Clear();
                cb_vidd.Items.Clear();
            };

            cb_rjn.SelectedIndexChanged += (s, e) =>
            {
                NP_Descr_Ref _AreaItem = (NP_Descr_Ref)cb_obl.SelectedItem;
                NP_Descr_Ref _item = (NP_Descr_Ref)cb_rjn.SelectedItem;

                SetCityByRegion(_AreaItem.Ref, _item.Ref);

                cb_vidd.Items.Clear();
            };

            cb_city.SelectedIndexChanged += (s, e) =>
            {
                NP_CityInfo _item = (NP_CityInfo)cb_city.SelectedItem;

                cb_vidd.Items.Clear();

                SetWarehosesByCity(_item.Description, _item.SettlementType, _item.AreaDescription);
            };

            tbSearch.KeyUp += (s, e) => {
                if (
                       e.KeyCode == Keys.Up ||
                       e.KeyCode == Keys.Down ||
                       e.KeyCode == Keys.Left ||
                       e.KeyCode == Keys.Right ||
                       e.KeyCode == Keys.Enter ||
                       e.KeyCode == Keys.Escape ||
                       e.KeyCode == Keys.Tab ||
                       e.KeyCode == Keys.ShiftKey ||
                       e.KeyCode == Keys.ControlKey ||
                       e.KeyCode == Keys.Alt || 
                       e.KeyCode == Keys.Space
                   ) return;

                string _input = tbSearch.Text.Trim();

                if (_input.Length < 3) {
                    lbSearch.Items.Clear();
                    lbSearch.Enabled = false;
                    return; 
                }

                AddressParts _part = AddressParts.Parse(_input);

                FindedCiti[] _finded = FindAndGet(_part.CityName).ToArray();

                if (_finded.Length > 0)
                {
                    lbSearch.BeginUpdate(); // опціонально — щоб не мигало
                    lbSearch.Items.Clear();
                    lbSearch.Items.AddRange(_finded);
                    lbSearch.Enabled = true;
                    lbSearch.EndUpdate();
                }
                else
                {
                    lbSearch.Items.Clear();
                    lbSearch.Enabled = false;
                }
            };

            lbSearch.DoubleClick += (s, e) =>
            {
                if (lbSearch.SelectedIndex < 0) return;

                FindedCiti _info = (FindedCiti)lbSearch.SelectedItem;

                FindAndSet(_info);

                tbSearch.Text = "";
                lbSearch.Items.Clear();
                lbSearch.Enabled = false;
            };

            SetObl();

            //Якщо всі дані є - заповнюємо одразу

            SetInput(input);
        }

        private async void SetInput(string input)
        {
            await Task.Delay(500);

            if (!string.IsNullOrEmpty(input))
            {
                AddressParts _temp = AddressParts.Parse(input);

                bool FullDone = false;

                if (!string.IsNullOrEmpty(_temp.Oblast))
                {
                    string _tempStr = _temp.Oblast.ToLower();

                    foreach (NP_Descr_Ref item in cb_obl.Items)
                    {
                        if (item.Description.ToLower().IndexOf(_tempStr) > -1)
                        {
                            cb_obl.SelectedItem = item;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(_temp.Rajon))
                    {
                        _tempStr = _temp.Rajon.ToLower();

                        foreach (NP_Descr_Ref item in cb_rjn.Items)
                        {
                            if (item.Description.ToLower().IndexOf(_tempStr) > -1)
                            {
                                cb_rjn.SelectedItem = item;
                                break;
                            }
                        }

                        if (!string.IsNullOrEmpty(_temp.CityName))
                        {
                            _tempStr = _temp.CityName.ToLower();

                            foreach (NP_CityInfo item in cb_city.Items)
                            {
                                if (item.Description.ToLower().IndexOf(_tempStr) > -1)
                                {
                                    cb_city.SelectedItem = item;
                                    break;
                                }
                            }

                            if (!string.IsNullOrEmpty(_temp.Number))
                            {
                                tb_number.Text = _temp.Number;
                            }
                        }
                    }
                }

                //Немає деяких/всіх вхідних даних
                if (!FullDone && !string.IsNullOrEmpty(_temp.CityName))
                {
                    FindAndSet(_temp, _temp.Number);
                }
            }
        }

        private void OnProgressReported(int value)
        {
            if (NPProgressBar.InvokeRequired)
            {
                NPProgressBar.Invoke(new Action(() =>
                {
                    NPProgressBar.Value = Math.Min(NPProgressBar.Maximum, value);
                }));
            }
            else
            {
                NPProgressBar.Value = Math.Min(NPProgressBar.Maximum, value);
            }
        }

        private void OnUpdateFinished()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnUpdateFinished));
                return;
            }

            // вже в UI-потоці
            NPProgressBar.Visible = false;
            butUpdate.Visible = false;
        }

        private void StartUpdate()
        {
            if (DialogResult.Yes != MessageBox.Show(
                "Оновлення довідників Нової Пошти періодично необхідне та може зайняти до хвилини часу! Почати?",
                "Нова Пошта: оновлення",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                )) return;

            NPProgressBar.Value = 0;
            NPProgressBar.Maximum = 48;
            NPProgressBar.Visible = true;
            // В окремому потоці, щоб не блокувати UI
            new Thread(() => {
                NOVA_POSHTA.ACTUALIZE();
            }).Start();
        }

        private List<FindedCiti> FindAndGet(string _cityName)
        {
            List<FindedCiti> _out = new List<FindedCiti>();

            if (_cityName == string.Empty) return _out;

            List<NP_CityInfo> _finded = NOVA_POSHTA.FindByCityName(_cityName);

            if (_finded.Count == 0) return _out;

            foreach (var _findedOne in _finded)
            {
                _out.Add(FindedCiti.FromBase(_findedOne));
            }

            return _out;
        }

        private void FindAndSet(FindedCiti _input, string _number = null)
        {
            List<NP_CityInfo> _finded = NOVA_POSHTA.FindByCityName(_input.Description);

            if (_finded.Count > 0)//Локально шукаємо
            {
                List<NP_CityInfo> _filter = new List<NP_CityInfo>();

                _filter = _finded.Where(f => f.Description.ToLower()
                                 == _input.Description.ToLower()).ToList();

                if (_filter.Count == 0)
                _filter = _finded.Where(f=>f.Description.ToLower()
                                 .Contains(_input.Description.ToLower())).ToList();

                if (!string.IsNullOrEmpty(_input.AreaDescription))
                {
                    _filter = _filter.Where(f => f.AreaDescription.ToLower()
                                 .Contains(_input.AreaDescription.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(_input.RegionsDescription))
                {
                    _filter = _filter.Where(f => f.RegionsDescription.ToLower()
                                 .Contains(_input.RegionsDescription.ToLower())).ToList();
                }

                if (_filter.Count > 1)
                {
                    _finded = _filter;

                    if (!string.IsNullOrEmpty(_input.AreaDescription))
                    {
                        _filter = _finded.Where(f => f.AreaDescription.ToLower()
                                 .Contains(_input.AreaDescription.ToLower())).ToList();
                    }

                    if (_filter.Count > 1 && !string.IsNullOrEmpty(_input.RegionsDescription))
                    {
                        _finded = _filter;

                        _filter = _finded.Where(f => f.RegionsDescription.ToLower()
                                 .Contains(_input.RegionsDescription.ToLower())).ToList();

                        if (_filter.Count > 0) _finded = _filter;
                    }
                } 
                else if (_filter.Count == 1)
                {
                    _finded = _filter;
                }
            }
            else//Шукаємо онлайн
            {

            }

            if (_finded.Count >= 1)
            {
                foreach(NP_Descr_Ref item in cb_obl.Items)
                {
                    if (item.Description == _finded[0].AreaDescription)
                    {
                        cb_obl.SelectedItem = item;
                        break;
                    }
                }

                foreach (NP_Descr_Ref item in cb_rjn.Items)
                {
                    if (item.Description == _finded[0].RegionsDescription)
                    {
                        cb_rjn.SelectedItem = item;
                        break;
                    }
                }

                foreach (NP_CityInfo item in cb_city.Items)
                {
                    if (item.Description == _finded[0].Description)
                    {
                        cb_city.SelectedItem = item;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(_number))
                {
                    tb_number.Text = _number;
                }
            }
        }

        private void SetObl()
        {
            cb_obl.Items.Clear();
            cb_obl.Items.AddRange(NOVA_POSHTA.AREAS);
        }

        private void SetRegionsByRef(string _ref)
        {
            cb_rjn.Items.Clear();
            cb_rjn.Items.AddRange(NOVA_POSHTA.GetRegionList(_ref));
        }

        private void SetCityByRegion(string _areaRef, string _regionRef)
        {
            cb_city.Items.Clear();
            cb_city.Items.AddRange(NOVA_POSHTA.GetCitiesList(_areaRef, _regionRef));
        }

        private async void SetWarehosesByCity(string _description, string _typeRef, string _areaDesc)
        {
            if (!NOVA_POSHTA.LOCAL_CITIES_EXIST || !NOVA_POSHTA.IS_ACTUAL_LOCAL_CITIES)
            {
                await GetCitiesFromWebAsync(NOVA_POSHTA.LOCAL_CITIES_EXIST);
            }

            List<WrhInfo> _listWarehouses = NOVA_POSHTA
                .GetWarehouseList(_description, _typeRef, _areaDesc);

            cb_vidd.Items.Clear();

            if (_listWarehouses == null || _listWarehouses.Count == 0) {
                cb_vidd.Visible = false;
                return;
            }

            cb_vidd.Visible = true;
            cb_vidd.Enabled = true;

            cb_vidd.Items.AddRange(_listWarehouses.ToArray());

            if (_listWarehouses.Count == 1) { 
                cb_vidd.SelectedIndex = 0;
                cb_vidd.Enabled = false;
            }
        }

        /// <summary>
        /// Повертає список CITIES (створює локальний файл, якщо не існує)
        /// </summary>
        /// <param name="_isExist"></param>
        /// <returns></returns>
        public async Task<NP_CITIES_TO_WH> GetCitiesFromWebAsync(bool _isExist = true)
        {
            if (_isExist && DialogResult.Yes != MessageBox.Show(
                "Оновити базу актуальних міст НП?",
                "Оновлення баз Нової Пошти",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question
                )) return null;

            NPProgressBar.Value = 0;
            NPProgressBar.Maximum = 23;
            NPProgressBar.Visible = true;

            NP_CITIES_TO_WH _ans = new NP_CITIES_TO_WH()
            {
                data = new List<NP_CityInfoToWrh>()
            };

            try
            {
                await Task.Run(() =>
                {
                    List<NP_CityInfoToWrh> _temp;

                    for (int i = 1; ; i++)
                    {
                        _temp = NOVA_POSHTA.GetCitiesFromWeb(page: i);

                        if (_temp == null) break;

                        // оновлюємо дані
                        Invoke((MethodInvoker)(() =>
                        {
                            _ans.data.AddRange(_temp);
                            NPProgressBar.Value = Math.Min(i, NPProgressBar.Maximum);
                        }));
                    }

                    NOVA_POSHTA.CreateLocalCities();

                });
            }
            catch
            {
                return null;
            }
            finally
            {
                NPProgressBar.Visible = false;
            }

            return _ans;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _ButUpdateTimer?.Dispose();
            NOVA_POSHTA?.Dispose();
            NPProgressBar?.Dispose();
            base.OnFormClosed(e);
        }

        private void cb_vidd_SelectedIndexChanged(object sender, EventArgs e)
        {
            WrhInfo _wrh = (WrhInfo)cb_vidd.SelectedItem;

            tb_number.Text = _wrh.NUMBER.ToString();
        }

        private void tb_number_Enter(object sender, EventArgs e)
        {
            tb_number.Select(0, tb_number.Text.Length);
        }

        private void tb_number_MouseClick(object sender, MouseEventArgs e)
        {
            tb_number.Select(0, tb_number.Text.Length);
        }

        private void tb_number_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_number.Text.Trim(), out int _temp))
            {
                foreach (WrhInfo _w in cb_vidd.Items)
                {
                    if (_w.NUMBER == _temp)
                    {
                        cb_vidd.SelectedItem = _w;
                        break;
                    }
                }
            }
            else
            {
                tb_number.Text = "";
                tb_number.Focus();
            }
        }
    }

    class FindedCiti:NP_CityInfo
    {
        public static implicit operator FindedCiti(AddressParts _other)
        {
            return new FindedCiti
            {
                Description = _other.CityName,
                AreaDescription = _other.Oblast,
                RegionsDescription = _other.Rajon,
                SettlementTypeDescription = _other.CityType
            };
        }
        public static FindedCiti FromBase(NP_CityInfo city)
        {
            return new FindedCiti
            {
                Ref = city.Ref,
                SettlementType = city.SettlementType,
                Description = city.Description,
                SettlementTypeDescription = city.SettlementTypeDescription,
                Region = city.Region,
                RegionsDescription = city.RegionsDescription,
                AreaDescription = city.AreaDescription
            };
        }

        public override string ToString()
        {
            return base.ToString() + " | " + AreaDescription + " обл." 
                + (!string.IsNullOrEmpty(RegionsDescription) ? " | " + RegionsDescription + " р-н" : "");
        }
    }
    
}
