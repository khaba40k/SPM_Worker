
namespace SPM_Worker
{
    partial class ZAKAZ_FORM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butSetNPformat = new System.Windows.Forms.Button();
            this.butGetNpList = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_reqv = new System.Windows.Forms.TextBox();
            this.butDiscount = new System.Windows.Forms.Button();
            this.cb_messendger = new System.Windows.Forms.ComboBox();
            this.cb_term = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_discount = new System.Windows.Forms.TextBox();
            this.tb_comm = new System.Windows.Forms.TextBox();
            this.tb_ttnout = new System.Windows.Forms.TextBox();
            this.tb_ttnin = new System.Windows.Forms.TextBox();
            this.tb_name3 = new System.Windows.Forms.TextBox();
            this.tb_name2 = new System.Windows.Forms.TextBox();
            this.tb_name1 = new System.Windows.Forms.TextBox();
            this.tb_phone = new System.Windows.Forms.TextBox();
            this.dt_do = new System.Windows.Forms.DateTimePicker();
            this.dt_done = new System.Windows.Forms.DateTimePicker();
            this.dt_vid = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab0 = new System.Windows.Forms.TabPage();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.cb_worker = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_redaktor = new System.Windows.Forms.ComboBox();
            this.INFO_TOOL_TIP = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSave = new System.Windows.Forms.Button();
            this.pereoblInterface1 = new SPM_Worker.PereoblInterface();
            this.soldInterface1 = new SPM_Worker.SoldInterface();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab0.SuspendLayout();
            this.tab1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butSetNPformat);
            this.panel1.Controls.Add(this.butGetNpList);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.tb_reqv);
            this.panel1.Controls.Add(this.butDiscount);
            this.panel1.Controls.Add(this.cb_messendger);
            this.panel1.Controls.Add(this.cb_term);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tb_discount);
            this.panel1.Controls.Add(this.tb_comm);
            this.panel1.Controls.Add(this.tb_ttnout);
            this.panel1.Controls.Add(this.tb_ttnin);
            this.panel1.Controls.Add(this.tb_name3);
            this.panel1.Controls.Add(this.tb_name2);
            this.panel1.Controls.Add(this.tb_name1);
            this.panel1.Controls.Add(this.tb_phone);
            this.panel1.Controls.Add(this.dt_do);
            this.panel1.Controls.Add(this.dt_done);
            this.panel1.Controls.Add(this.dt_vid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 193);
            this.panel1.TabIndex = 0;
            // 
            // butSetNPformat
            // 
            this.butSetNPformat.BackColor = System.Drawing.Color.DarkGray;
            this.butSetNPformat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSetNPformat.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butSetNPformat.ForeColor = System.Drawing.Color.White;
            this.butSetNPformat.Location = new System.Drawing.Point(98, 74);
            this.butSetNPformat.Name = "butSetNPformat";
            this.butSetNPformat.Size = new System.Drawing.Size(55, 27);
            this.butSetNPformat.TabIndex = 16;
            this.butSetNPformat.TabStop = false;
            this.butSetNPformat.Text = "норм";
            this.INFO_TOOL_TIP.SetToolTip(this.butSetNPformat, "Привести реквізити до стандартного вигляду");
            this.butSetNPformat.UseVisualStyleBackColor = false;
            this.butSetNPformat.Click += new System.EventHandler(this.butSetNPformat_Click);
            // 
            // butGetNpList
            // 
            this.butGetNpList.BackColor = System.Drawing.Color.Red;
            this.butGetNpList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butGetNpList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butGetNpList.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butGetNpList.ForeColor = System.Drawing.Color.White;
            this.butGetNpList.Location = new System.Drawing.Point(688, 74);
            this.butGetNpList.Name = "butGetNpList";
            this.butGetNpList.Size = new System.Drawing.Size(32, 23);
            this.butGetNpList.TabIndex = 4;
            this.butGetNpList.TabStop = false;
            this.butGetNpList.Text = "...";
            this.butGetNpList.UseVisualStyleBackColor = false;
            this.butGetNpList.Click += new System.EventHandler(this.butGetNpList_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "Реквізити НП";
            // 
            // tb_reqv
            // 
            this.tb_reqv.Location = new System.Drawing.Point(156, 75);
            this.tb_reqv.MaxLength = 100;
            this.tb_reqv.Name = "tb_reqv";
            this.tb_reqv.ShortcutsEnabled = false;
            this.tb_reqv.Size = new System.Drawing.Size(526, 22);
            this.tb_reqv.TabIndex = 5;
            this.tb_reqv.WordWrap = false;
            // 
            // butDiscount
            // 
            this.butDiscount.BackColor = System.Drawing.SystemColors.Control;
            this.butDiscount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butDiscount.Enabled = false;
            this.butDiscount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.butDiscount.Location = new System.Drawing.Point(210, 161);
            this.butDiscount.Margin = new System.Windows.Forms.Padding(0);
            this.butDiscount.Name = "butDiscount";
            this.butDiscount.Size = new System.Drawing.Size(78, 25);
            this.butDiscount.TabIndex = 11;
            this.butDiscount.Text = "задіяти";
            this.butDiscount.UseVisualStyleBackColor = false;
            // 
            // cb_messendger
            // 
            this.cb_messendger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_messendger.FormattingEnabled = true;
            this.cb_messendger.Location = new System.Drawing.Point(437, 103);
            this.cb_messendger.Name = "cb_messendger";
            this.cb_messendger.Size = new System.Drawing.Size(283, 24);
            this.cb_messendger.TabIndex = 7;
            // 
            // cb_term
            // 
            this.cb_term.AutoSize = true;
            this.cb_term.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_term.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_term.ForeColor = System.Drawing.Color.Red;
            this.cb_term.Location = new System.Drawing.Point(305, 164);
            this.cb_term.Name = "cb_term";
            this.cb_term.Size = new System.Drawing.Size(98, 20);
            this.cb_term.TabIndex = 10;
            this.cb_term.TabStop = false;
            this.cb_term.Text = "Терміново";
            this.cb_term.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Дисконт (знижка %)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(330, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Коментар";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(330, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Зв\'язок";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "ТТН (вихідна)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(536, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "По батькові";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(365, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 16);
            this.label11.TabIndex = 4;
            this.label11.Text = "Ім\'я";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Прізвище";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "ТТН (вхідна)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(0, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Номер тел.";
            // 
            // tb_discount
            // 
            this.tb_discount.AutoCompleteCustomSource.AddRange(new string[] {
            "3",
            "5",
            "10",
            "30"});
            this.tb_discount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tb_discount.Location = new System.Drawing.Point(142, 164);
            this.tb_discount.MaxLength = 5;
            this.tb_discount.Name = "tb_discount";
            this.tb_discount.ShortcutsEnabled = false;
            this.tb_discount.Size = new System.Drawing.Size(65, 22);
            this.tb_discount.TabIndex = 10;
            this.tb_discount.Text = "0";
            this.tb_discount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.INFO_TOOL_TIP.SetToolTip(this.tb_discount, "Можна вводить як цифрами (від 1 до 99) так і код знижки");
            this.tb_discount.WordWrap = false;
            this.tb_discount.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tb_discount_MouseClick);
            this.tb_discount.Enter += new System.EventHandler(this.tb_discount_Enter);
            this.tb_discount.Leave += new System.EventHandler(this.tb_discount_Leave);
            // 
            // tb_comm
            // 
            this.tb_comm.Location = new System.Drawing.Point(437, 133);
            this.tb_comm.MaxLength = 200;
            this.tb_comm.Multiline = true;
            this.tb_comm.Name = "tb_comm";
            this.tb_comm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_comm.ShortcutsEnabled = false;
            this.tb_comm.Size = new System.Drawing.Size(283, 54);
            this.tb_comm.TabIndex = 9;
            this.INFO_TOOL_TIP.SetToolTip(this.tb_comm, "Якщо потрібно окремо коментар для робітника,\r\nпостав перед ним !!! Все що буде на" +
        "писано перед !!!\r\nне роздрукується робітнику");
            // 
            // tb_ttnout
            // 
            this.tb_ttnout.Location = new System.Drawing.Point(99, 133);
            this.tb_ttnout.MaxLength = 100;
            this.tb_ttnout.Name = "tb_ttnout";
            this.tb_ttnout.ShortcutsEnabled = false;
            this.tb_ttnout.Size = new System.Drawing.Size(225, 22);
            this.tb_ttnout.TabIndex = 8;
            this.tb_ttnout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ttnout.WordWrap = false;
            // 
            // tb_ttnin
            // 
            this.tb_ttnin.Location = new System.Drawing.Point(99, 105);
            this.tb_ttnin.MaxLength = 100;
            this.tb_ttnin.Name = "tb_ttnin";
            this.tb_ttnin.ShortcutsEnabled = false;
            this.tb_ttnin.Size = new System.Drawing.Size(225, 22);
            this.tb_ttnin.TabIndex = 6;
            this.tb_ttnin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ttnin.WordWrap = false;
            // 
            // tb_name3
            // 
            this.tb_name3.Location = new System.Drawing.Point(539, 49);
            this.tb_name3.MaxLength = 100;
            this.tb_name3.Name = "tb_name3";
            this.tb_name3.ShortcutsEnabled = false;
            this.tb_name3.Size = new System.Drawing.Size(181, 22);
            this.tb_name3.TabIndex = 3;
            this.tb_name3.WordWrap = false;
            // 
            // tb_name2
            // 
            this.tb_name2.Location = new System.Drawing.Point(363, 49);
            this.tb_name2.MaxLength = 100;
            this.tb_name2.Name = "tb_name2";
            this.tb_name2.ShortcutsEnabled = false;
            this.tb_name2.Size = new System.Drawing.Size(170, 22);
            this.tb_name2.TabIndex = 2;
            this.tb_name2.WordWrap = false;
            // 
            // tb_name1
            // 
            this.tb_name1.Location = new System.Drawing.Point(165, 49);
            this.tb_name1.MaxLength = 100;
            this.tb_name1.Name = "tb_name1";
            this.tb_name1.ShortcutsEnabled = false;
            this.tb_name1.Size = new System.Drawing.Size(192, 22);
            this.tb_name1.TabIndex = 1;
            this.tb_name1.WordWrap = false;
            // 
            // tb_phone
            // 
            this.tb_phone.Location = new System.Drawing.Point(4, 49);
            this.tb_phone.MaxLength = 13;
            this.tb_phone.Name = "tb_phone";
            this.tb_phone.ShortcutsEnabled = false;
            this.tb_phone.Size = new System.Drawing.Size(155, 22);
            this.tb_phone.TabIndex = 0;
            this.tb_phone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_phone.WordWrap = false;
            // 
            // dt_do
            // 
            this.dt_do.CustomFormat = "термін: dd.MM.yy";
            this.dt_do.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_do.Location = new System.Drawing.Point(182, 4);
            this.dt_do.Name = "dt_do";
            this.dt_do.Size = new System.Drawing.Size(172, 22);
            this.dt_do.TabIndex = 1;
            this.dt_do.TabStop = false;
            // 
            // dt_done
            // 
            this.dt_done.CustomFormat = "виконано:  dd MMMM yyyyр.  HH:mm";
            this.dt_done.Enabled = false;
            this.dt_done.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dt_done.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_done.Location = new System.Drawing.Point(363, 4);
            this.dt_done.MaxDate = new System.DateTime(2030, 7, 7, 0, 0, 0, 0);
            this.dt_done.MinDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.dt_done.Name = "dt_done";
            this.dt_done.Size = new System.Drawing.Size(357, 22);
            this.dt_done.TabIndex = 2;
            this.dt_done.TabStop = false;
            this.dt_done.Value = new System.DateTime(2025, 7, 7, 0, 0, 0, 0);
            this.dt_done.Visible = false;
            // 
            // dt_vid
            // 
            this.dt_vid.CustomFormat = "від: dd.MM.yy HH:mm";
            this.dt_vid.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_vid.Location = new System.Drawing.Point(4, 4);
            this.dt_vid.MaxDate = new System.DateTime(2030, 7, 7, 0, 0, 0, 0);
            this.dt_vid.MinDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.dt_vid.Name = "dt_vid";
            this.dt_vid.Size = new System.Drawing.Size(172, 22);
            this.dt_vid.TabIndex = 0;
            this.dt_vid.TabStop = false;
            this.dt_vid.Value = new System.DateTime(2025, 7, 7, 0, 0, 0, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 193);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(726, 313);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab0);
            this.tabControl1.Controls.Add(this.tab1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(726, 313);
            this.tabControl1.TabIndex = 13;
            // 
            // tab0
            // 
            this.tab0.BackColor = System.Drawing.Color.Silver;
            this.tab0.Controls.Add(this.pereoblInterface1);
            this.tab0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tab0.Location = new System.Drawing.Point(4, 27);
            this.tab0.Name = "tab0";
            this.tab0.Padding = new System.Windows.Forms.Padding(3);
            this.tab0.Size = new System.Drawing.Size(718, 282);
            this.tab0.TabIndex = 0;
            this.tab0.Text = "Переобладнання";
            // 
            // tab1
            // 
            this.tab1.BackColor = System.Drawing.Color.LightGray;
            this.tab1.Controls.Add(this.soldInterface1);
            this.tab1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tab1.Location = new System.Drawing.Point(4, 27);
            this.tab1.Name = "tab1";
            this.tab1.Size = new System.Drawing.Size(718, 282);
            this.tab1.TabIndex = 1;
            this.tab1.Text = "Продаж";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 523);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "Працівник";
            // 
            // cb_worker
            // 
            this.cb_worker.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cb_worker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_worker.FormattingEnabled = true;
            this.cb_worker.Location = new System.Drawing.Point(82, 520);
            this.cb_worker.Name = "cb_worker";
            this.cb_worker.Size = new System.Drawing.Size(200, 24);
            this.cb_worker.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(285, 523);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "Відповідальний";
            // 
            // cb_redaktor
            // 
            this.cb_redaktor.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cb_redaktor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_redaktor.FormattingEnabled = true;
            this.cb_redaktor.Location = new System.Drawing.Point(400, 520);
            this.cb_redaktor.Name = "cb_redaktor";
            this.cb_redaktor.Size = new System.Drawing.Size(200, 24);
            this.cb_redaktor.TabIndex = 15;
            // 
            // INFO_TOOL_TIP
            // 
            this.INFO_TOOL_TIP.AutomaticDelay = 200;
            this.INFO_TOOL_TIP.AutoPopDelay = 10000;
            this.INFO_TOOL_TIP.InitialDelay = 400;
            this.INFO_TOOL_TIP.OwnerDraw = true;
            this.INFO_TOOL_TIP.ReshowDelay = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.BackColor = System.Drawing.Color.White;
            this.buttonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSave.ForeColor = System.Drawing.Color.Green;
            this.buttonSave.Image = global::SPM_Worker.Properties.Resources.Load;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.Location = new System.Drawing.Point(606, 509);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(116, 44);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "OK";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.MouseEnter += new System.EventHandler(this.buttonSave_MouseEnter);
            // 
            // pereoblInterface1
            // 
            this.pereoblInterface1.AutoSize = true;
            this.pereoblInterface1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pereoblInterface1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pereoblInterface1.Location = new System.Drawing.Point(3, 3);
            this.pereoblInterface1.Margin = new System.Windows.Forms.Padding(4);
            this.pereoblInterface1.Name = "pereoblInterface1";
            this.pereoblInterface1.Size = new System.Drawing.Size(712, 276);
            this.pereoblInterface1.TabIndex = 0;
            this.pereoblInterface1.TabStop = false;
            // 
            // soldInterface1
            // 
            this.soldInterface1.ATR = 0;
            this.soldInterface1.AutoSize = true;
            this.soldInterface1.BackColor = System.Drawing.Color.Transparent;
            this.soldInterface1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soldInterface1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.soldInterface1.LayoutMode = SPM_Worker.SoldInterface.ControlLayoutMode.Default;
            this.soldInterface1.Location = new System.Drawing.Point(0, 0);
            this.soldInterface1.Margin = new System.Windows.Forms.Padding(4);
            this.soldInterface1.Name = "soldInterface1";
            this.soldInterface1.Size = new System.Drawing.Size(718, 282);
            this.soldInterface1.TabIndex = 0;
            // 
            // ZAKAZ_FORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 558);
            this.Controls.Add(this.cb_redaktor);
            this.Controls.Add(this.cb_worker);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZAKAZ_FORM";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Нова заявка";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tab0.ResumeLayout(false);
            this.tab0.PerformLayout();
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dt_do;
        private System.Windows.Forms.DateTimePicker dt_done;
        private System.Windows.Forms.DateTimePicker dt_vid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_discount;
        private System.Windows.Forms.TextBox tb_ttnout;
        private System.Windows.Forms.TextBox tb_ttnin;
        private System.Windows.Forms.TextBox tb_name1;
        private System.Windows.Forms.TextBox tb_phone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_comm;
        private System.Windows.Forms.ComboBox cb_messendger;
        private System.Windows.Forms.CheckBox cb_term;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab0;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cb_worker;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_redaktor;
        private PereoblInterface pereoblInterface1;
        private SoldInterface soldInterface1;
        private System.Windows.Forms.ToolTip INFO_TOOL_TIP;
        private System.Windows.Forms.Button butDiscount;
        private System.Windows.Forms.Button butSetNPformat;
        private System.Windows.Forms.Button butGetNpList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_reqv;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_name2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_name3;
    }
}