using SPM_Core;
using API_NovaPoshta;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace SPM_Worker
{

    public partial class ZAKAZ_FORM : Form
    {
        private bool HIDDEN_TOP = false;

        private int PANEL_TOP_HEIGHT, PANEL_BOTTOM_HEIGHT, PANEL_DIFF;

        private TabPage HIDDEN_TAB;

        public ZAKAZ INFO { get; private set; }

        public bool CHANGED { get; private set; } = false;

        private string DISCOUNT_CODE = null;
        private byte? DISCOUNT_PERCENT = null;

        private event EventHandler<byte> DiscountChanged;

        public ZAKAZ_FORM(ZAKAZ _input = null)
        {
            InitializeComponent();

            cb_redaktor.Items.AddRange(SERVICE_INFO.AUTORIZE_INFO.data.downUserList.ToArray());

            string selectedLogin;

            if (_input == null)
            {
                selectedLogin = SERVICE_INFO.AUTORIZE_INFO.data.LOGIN;
            }
            else
            {
                INFO = _input;
                selectedLogin = _input.REDAKTOR;
            }

            cb_worker.Items.AddRange(SERVICE_INFO.AUTORIZE_INFO.data.workerInfo);

            INFO_TOOL_TIP.OwnerDraw = true;
            INFO_TOOL_TIP.Draw += ToolTip1_Draw;
            INFO_TOOL_TIP.Popup += ToolTip1_Popup;

            foreach (UserInfo _u in cb_redaktor.Items)
            {
                if (_u.LOGIN == selectedLogin)
                {
                    cb_redaktor.SelectedItem = _u;
                    break;
                }
            }

            PANEL_TOP_HEIGHT = panel1.Height;
            PANEL_BOTTOM_HEIGHT = panel2.Height;
            PANEL_DIFF = 25;

            pereoblInterface1.CHANGED += INIT_TAB;
            soldInterface1.CHANGED += INIT_TAB;

            HookMouseEnterAll();
            SetupDynamicHandlers();

            panel1.MouseEnter += (s, e) => {
                if (!HIDDEN_TOP) return;

                panel1.Height = PANEL_TOP_HEIGHT;
                panel2.Height = PANEL_BOTTOM_HEIGHT;

                panel1.BackColor = SystemColors.Control;

                HIDDEN_TOP = false;
            };

            buttonSave.Click += (s, e) => {

                ZAKAZ OUT = INFO;

                if (!Serialize(out OUT, out string errorMes))
                {
                    CustomMessage.Show(errorMes, "Заповніть наступні дані:",
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                SERVICE_ID_LIST _komplekt = new SERVICE_ID_LIST(OUT.KOMPLEKT);

                string messageText = $"Телефон: {OUT.PHONE}\n" +
                       $"Прізвище: {OUT.CLIENT_NAME}\nНП: {OUT.REQV}\n" +
                       $"{OUT.COMM}\n" +
                       $"{_komplekt}";

                if (OUT == INFO || DialogResult.Yes != CustomMessage.Show(
                    messageText,
                    "Підтвердження",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    new MessButText { Yes = "Зберегти", No = "Редагувати" }
                    )) return;

                //ЗАПИС В БАЗУ

                if (SERVICE_INFO.SAVE_ZAKAZ(OUT, out string message))
                {
                    CustomMessage.Show(message,
                    "Статус",
                    MessageBoxIcon.Information);

                    CHANGED = true;
                    Close();
                }
                else
                {
                    CustomMessage.Show(message, 
                        "Помилка запису!", 
                         MessageBoxIcon.Error);
                }
            };

            cb_term.CheckedChanged += (s, e) => {
                CheckBox _cb = (CheckBox)s;

                if (_cb.Checked)
                {
                    SERVICE_ID_INFO _outTerm = SERVICE_INFO.GetService(21);

                    if (_input != null)
                    {
                        KOMPLEKT k = _input.KOMPLEKT.Find(km => km.service_ID == 21);

                        if (k != null) { 
                            _outTerm.COST = k.costs; 
                        }
                        else
                        {
                            _outTerm.COST = _outTerm.GetDeffaultCost();
                        }
                    }


                    pereoblInterface1.Add(_outTerm);
                    soldInterface1.Add(_outTerm);
                }
                else
                {
                    pereoblInterface1.Remove(21);
                    soldInterface1.Remove(21);

                    if (_input != null)
                    {
                        KOMPLEKT k = _input.KOMPLEKT.Find(km => km.service_ID == 21);

                        if (k != null) _input.KOMPLEKT.Remove(k);
                    }
                }
            };

            string[] _messengers = new string[] {
                 "",
                 "Телеграм",
                 "Вотсап",
                 "Інстаграм",
                 "Вайбер",
                 "Телефон",
                 "Наручно",
                 "Сигнал",
                 "ТікТок"
            };

            int _mesIndex = 0;

            cb_messendger.Items.AddRange(_messengers);

            if (_input != null)
            {
                if (_input.ID > -1) Text = $"Редагування замовлення{(INFO.NUMBER > 0 ? $" № {INFO.NUMBER}" : "")}";

                if (INFO.DATE_OUT != null)
                {
                    dt_done.Value = Convert.ToDateTime(INFO.DATE_OUT);
                    dt_done.Visible = true; dt_done.Enabled = true;
                }

                tb_phone.Text = INFO.PHONE;
                tb_phone.Select(tb_phone.Text.Length, 0);

                tb_name.Text = INFO.CLIENT_NAME;
                tb_ttnin.Text = INFO.TTN_IN;
                tb_ttnout.Text = INFO.TTN_OUT;

                byte _disc = Convert.ToByte(INFO.DISCOUNT);

                tb_discount.Text = _disc.ToString();

                if (_disc > 0) tb_discount.Enabled = false;

                tb_reqv.Text = INFO.REQV;

                string _firstLetter = (INFO.COMM ?? "").Split(' ')[0].ToLower();

                if (_firstLetter != string.Empty)
                for (int i = 1; i < _messengers.Length; i++)
                {
                    if (_messengers[i].ToLower().Contains(_firstLetter))
                    {
                        INFO.COMM = INFO.COMM.Remove(0, _firstLetter.Length).TrimStart();
                        _mesIndex = i;
                        break;
                    }
                }

                tb_comm.Text = INFO.COMM;
                cb_term.Checked = INFO.TERMINOVO;
                cb_worker.Text = INFO.WORKER;
                cb_redaktor.Text = INFO.REDAKTOR;
            }
            else
            {
                INFO = new ZAKAZ();

                INFO.STATUS = Z_STATUS.NEW;
                INFO.NUMBER = 0;

                tb_phone.Select();
            }

            if (INFO.STATUS != Z_STATUS.ARCHIVE &&
                (_input == null
                || (_input != null 
                && _input.DISCOUNT == null)))
            {
                tb_discount.TextChanged += (s, e) =>
                {
                    if (!tb_discount.Enabled) return;

                    string _text = tb_discount.Text.Trim().ToUpper();

                    if ((byte.TryParse(_text, out byte b)
                    && b > 0
                    && b < 100)
                    || _text.Length == 5)
                    {
                        butDiscount.Enabled = true;
                    }
                    else
                    {
                        butDiscount.Enabled = false;
                    }
                };

                butDiscount.Click += (s, e) =>
                {
                    string _text = tb_discount.Text.Trim().ToUpper();

                    if (!string.IsNullOrWhiteSpace(_text))
                    {
                        byte _discount = 0;

                        if (!byte.TryParse(_text, out _discount))
                        {
                            //Витягти знижку по коду онлайн

                            if (_text.Length == 5)
                            {
                                _discount = SERVICE_INFO.GetDiscountByCode(_text);
                            }
                            else
                            {
                                return;
                            }
                        }

                        if (_discount > 0
                        && _discount < 100
                        && DialogResult.Yes == CustomMessage.Show(
                            $"Задіяти знижку {_discount}% для поточної заявки?",
                            "ЗНИЖКА",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Exclamation))
                        {
                            //Задіяти знижку

                            tb_discount.Enabled = false;
                            butDiscount.Visible = false;
                            tb_discount.Width += butDiscount.Width;
                            DISCOUNT_CODE = _text.Length == 5 ? _text : null;
                            DISCOUNT_PERCENT = _discount;
                            DiscountChanged?.Invoke(this, (byte)DISCOUNT_PERCENT);
                            tb_discount.Text = _discount.ToString();
                        }
                    }
                };
            } 
            else
            {
                butDiscount.Visible = false;
                tb_discount.Enabled = false;
                tb_discount.Width += butDiscount.Width;
            }

            DiscountChanged += (s, e) => {
                pereoblInterface1.SetDiscount(e);
                soldInterface1.SetDiscount(e);
            };

            dt_vid.Value = INFO.DATE_IN;
            dt_do.Value = INFO.DATE_MAX;

            cb_messendger.SelectedIndex = _mesIndex;

            if (_input != null)
            {
                if (_input.TYPE == Z_TYPE.CONVERSION)
                {
                    pereoblInterface1.SET(_input.KOMPLEKT, _input.DISCOUNT ?? 0);
                }
                else
                {
                    soldInterface1.SET(_input.KOMPLEKT, 8, false, _input.DISCOUNT ?? 0);
                }

                HideTab();
            }
            else
            { 
                pereoblInterface1.SET();
                soldInterface1.SET(null, 8);
            }

            FormClosed += (s, e) => { SERVICE_INFO.ResetCountCost(); };
        }

        private void INIT_TAB(object sender, EventArgs e)
        {
            if (INFO.TYPE == Z_TYPE.NULL 
                && pereoblInterface1.HAS_VALUE ^ soldInterface1.HAS_VALUE)
            {
                INFO.TYPE = pereoblInterface1.HAS_VALUE ? Z_TYPE.CONVERSION : Z_TYPE.SOLD;
                HideTab();
            } else if (INFO.TYPE != Z_TYPE.NULL
                && !pereoblInterface1.HAS_VALUE && !soldInterface1.HAS_VALUE)
            {
                ShowTab();
                INFO.TYPE = Z_TYPE.NULL;
            }
        }

        private void ToolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            using (Font monoFont = new Font("Consolas", 9))
            {
                e.Graphics.FillRectangle(SystemBrushes.Info, e.Bounds);
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
                e.Graphics.DrawString(e.ToolTipText, monoFont, Brushes.Black, new PointF(2, 2));
            }
        }

        private void ToolTip1_Popup(object sender, PopupEventArgs e)
        {
            using (Font monoFont = new Font("Consolas", 9))
            {
                string text = INFO_TOOL_TIP.GetToolTip(e.AssociatedControl);
                Size textSize = TextRenderer.MeasureText(text, monoFont);  // or use Graphics.MeasureString if needed
                e.ToolTipSize = new Size(textSize.Width + 4, textSize.Height + 4);
            }
        }

        private void ShowTab()
        {
            if (HIDDEN_TAB != null)
            {
                int _ind = (INFO.TYPE == Z_TYPE.CONVERSION ? 1 : 0);

                tabControl1.TabPages.Insert(_ind, HIDDEN_TAB);

                HIDDEN_TAB = null;

                if (_ind == 0)
                {
                    tb_ttnin.Enabled = true;
                }
            }
        }

        private void HideTab()
        {
            if (HIDDEN_TAB != null) return;

            int _ind = (INFO.TYPE == Z_TYPE.CONVERSION ? 1 : 0);

            HIDDEN_TAB = tabControl1.TabPages[_ind];

            tabControl1.TabPages.RemoveAt(_ind);

            if (_ind == 0)
            {
                tb_ttnin.Enabled = false;
                tb_ttnin.Text = "";
            }
        }

        public bool Serialize(out ZAKAZ ANSWER, out string message)
        {
            ANSWER = new ZAKAZ(INFO); message = string.Empty;

            int counter = 1;

            if (ANSWER.TYPE == Z_TYPE.NULL)
            {
                message = $"{counter++}) Жодного товару/послуги не додано!\n";
            }

            ANSWER.DATE_IN = dt_vid.Value;
            ANSWER.DATE_MAX = dt_do.Value;

            if (dt_done.Enabled)
            {
                ANSWER.DATE_OUT = dt_done.Value;
            }
            else
            {
                ANSWER.DATE_OUT = null;
            }

            #region requaired

            ANSWER.PHONE = NullIfEmpty(tb_phone.Text);
            ANSWER.CLIENT_NAME = NullIfEmpty(tb_name.Text);
            ANSWER.REQV = NullIfEmpty(tb_reqv.Text);
            ANSWER.REDAKTOR = NullIfEmpty(cb_redaktor.Text);

            if (ANSWER.CLIENT_NAME == null)
            {
                message += $"{counter++}) Ім'я клієнта.\n";
            }

            if (ANSWER.PHONE == null)
            {
                message += $"{counter++}) Номер телефону.\n";
            }

            if (ANSWER.REQV == null)
            {
                message += $"{counter++}) Реквізити НП.\n";
            }

            if (ANSWER.REDAKTOR == null)
            {
                message += $"{counter++}) Відповідальний.\n";
            }

            if (ANSWER.TYPE == Z_TYPE.CONVERSION && pereoblInterface1.HAS_UNCHECKED)
            {
                message += $"{counter++}) Оберіть (Так/Ні) зі списку послуг!";
            }

            #endregion

            ANSWER.TTN_IN = NullIfEmpty(tb_ttnin.Text);
            ANSWER.TTN_OUT = NullIfEmpty(tb_ttnout.Text);
            ANSWER.COMM = NullIfEmpty((cb_messendger.Text + " " + tb_comm.Text.TrimStart()));

            if (DISCOUNT_PERCENT != null)
            ANSWER.DISCOUNT = DISCOUNT_PERCENT;

            ANSWER.WORKER = NullIfEmpty(cb_worker.Text);

            UserInfo _red = (UserInfo)(cb_redaktor.SelectedItem);

            ANSWER.REDAKTOR = _red.LOGIN;

            ANSWER.KOMPLEKT.Clear();

            ANSWER.KOMPLEKT.AddRange(GetKomplekt());

            return message == string.Empty;
        }

        private List<KOMPLEKT> GetKomplekt()
        {
            List<VitratyInfo> _products;

            if (INFO.TYPE == Z_TYPE.CONVERSION)
            {
                _products = pereoblInterface1.PRODUCTS.Select(s=>new VitratyInfo(s))
                    .ToList();
            }
            else
            {
                _products = soldInterface1.PRODUCTS;
            }

            List<KOMPLEKT> _out = new List<KOMPLEKT>();

            foreach (KOMPLEKT _k in _products)
            {
                _out.Add(_k);
            }

            return _out;
        }

        private string NullIfEmpty(string _text)
        {
            return string.IsNullOrWhiteSpace(_text) ? null : _text.Trim();
        }

        private void MouseEnterBottom(object sender, EventArgs e)
        {
            if (HIDDEN_TOP) return;

            panel1.Height = PANEL_DIFF;

            panel2.Height += (PANEL_TOP_HEIGHT - PANEL_DIFF);

            panel1.BackColor = SystemColors.ControlDarkDark;

            HIDDEN_TOP = true;
        }

        private void AddMouseEnterRecursive(Control ctrl)
        {
            ctrl.MouseEnter += MouseEnterBottom;

            foreach (Control child in ctrl.Controls)
            {
                AddMouseEnterRecursive(child);
            }
        }

        private void HookMouseEnterAll()
        {
            // Підключаємо на всі вкладки
            foreach (TabPage tab in tabControl1.TabPages)
            {
                AddMouseEnterRecursive(tab);
            }
        }

        private void SetupDynamicHandlers()
        {
            tab0.ControlAdded += Tab_ControlAdded;
            tab1.ControlAdded += Tab_ControlAdded;

            pereoblInterface1.ControlAdded += Tab_ControlAdded;
            soldInterface1.ControlAdded += Tab_ControlAdded;
        }

        private void butGetNpList_Click(object sender, EventArgs e)
        {
            NP_form _np =  new NP_form(tb_reqv.Text);

            _np.ShowDialog();

            if (_np.DialogResult == DialogResult.OK)
            {
                tb_reqv.Text = _np.ANSWER.ToString();
            }
        }

        private void butSetNPformat_Click(object sender, EventArgs e)
        {
            AddressParts _reqv = AddressParts.Parse(tb_reqv.Text.Trim());

            tb_reqv.Text = _reqv.ToString();
        }

        private void tb_discount_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tb_discount.SelectAll();
            }
        }

        private void tb_discount_Enter(object sender, EventArgs e)
        {
            AcceptButton = butDiscount;
        }

        private void tb_discount_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void buttonSave_MouseEnter(object sender, EventArgs e)
        {
            SERVICE_ID_LIST _showKomplekt = new SERVICE_ID_LIST(GetKomplekt());

            INFO_TOOL_TIP.SetToolTip((Button)sender, _showKomplekt.ToString());
        }

        private void Tab_ControlAdded(object sender, ControlEventArgs e)
        {
            AddMouseEnterRecursive(e.Control);
        }
    }
}
