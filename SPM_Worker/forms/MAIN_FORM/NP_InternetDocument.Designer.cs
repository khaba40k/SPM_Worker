namespace SPM_Worker
{
    partial class NP_InternetDocument
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateTime = new System.Windows.Forms.DateTimePicker();
            this.tb_ko = new System.Windows.Forms.TextBox();
            this.tb_comm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPakHeig = new System.Windows.Forms.TextBox();
            this.tbPakWidth = new System.Windows.Forms.TextBox();
            this.tbPakLeng = new System.Windows.Forms.TextBox();
            this.cb_pakList = new System.Windows.Forms.ComboBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tb_weight = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_CitySearch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbCityList = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_Warehouses = new System.Windows.Forms.ComboBox();
            this.cb_payer = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rb_ko_yes = new System.Windows.Forms.RadioButton();
            this.rb_ko_no = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_cost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.but_search = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTime
            // 
            this.dateTime.CustomFormat = "відправка: dd.MM.yyyy";
            this.dateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTime.Location = new System.Drawing.Point(4, 4);
            this.dateTime.Name = "dateTime";
            this.dateTime.Size = new System.Drawing.Size(240, 22);
            this.dateTime.TabIndex = 0;
            // 
            // tb_ko
            // 
            this.tb_ko.Enabled = false;
            this.tb_ko.Location = new System.Drawing.Point(5, 93);
            this.tb_ko.Name = "tb_ko";
            this.tb_ko.Size = new System.Drawing.Size(239, 22);
            this.tb_ko.TabIndex = 1;
            this.tb_ko.Text = "0";
            this.tb_ko.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ko.WordWrap = false;
            // 
            // tb_comm
            // 
            this.tb_comm.Location = new System.Drawing.Point(50, 151);
            this.tb_comm.MaxLength = 20;
            this.tb_comm.Name = "tb_comm";
            this.tb_comm.Size = new System.Drawing.Size(194, 22);
            this.tb_comm.TabIndex = 1;
            this.tb_comm.Text = "Шолом";
            this.tb_comm.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(4, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Опис";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbPakHeig);
            this.groupBox1.Controls.Add(this.tbPakWidth);
            this.groupBox1.Controls.Add(this.tbPakLeng);
            this.groupBox1.Controls.Add(this.cb_pakList);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(7, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Розмір коробки";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(155, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Вис.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(85, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Шир.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Довж.";
            // 
            // tbPakHeig
            // 
            this.tbPakHeig.Location = new System.Drawing.Point(158, 72);
            this.tbPakHeig.MaxLength = 3;
            this.tbPakHeig.Name = "tbPakHeig";
            this.tbPakHeig.Size = new System.Drawing.Size(64, 22);
            this.tbPakHeig.TabIndex = 1;
            this.tbPakHeig.Text = "0";
            this.tbPakHeig.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbPakHeig.WordWrap = false;
            // 
            // tbPakWidth
            // 
            this.tbPakWidth.Location = new System.Drawing.Point(88, 72);
            this.tbPakWidth.MaxLength = 3;
            this.tbPakWidth.Name = "tbPakWidth";
            this.tbPakWidth.Size = new System.Drawing.Size(64, 22);
            this.tbPakWidth.TabIndex = 1;
            this.tbPakWidth.Text = "0";
            this.tbPakWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbPakWidth.WordWrap = false;
            // 
            // tbPakLeng
            // 
            this.tbPakLeng.Location = new System.Drawing.Point(18, 72);
            this.tbPakLeng.MaxLength = 3;
            this.tbPakLeng.Name = "tbPakLeng";
            this.tbPakLeng.Size = new System.Drawing.Size(64, 22);
            this.tbPakLeng.TabIndex = 1;
            this.tbPakLeng.Text = "0";
            this.tbPakLeng.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbPakLeng.WordWrap = false;
            // 
            // cb_pakList
            // 
            this.cb_pakList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_pakList.FormattingEnabled = true;
            this.cb_pakList.Location = new System.Drawing.Point(18, 21);
            this.cb_pakList.Name = "cb_pakList";
            this.cb_pakList.Size = new System.Drawing.Size(204, 24);
            this.cb_pakList.TabIndex = 0;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(129, 281);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(115, 22);
            this.textBox7.TabIndex = 1;
            // 
            // tb_weight
            // 
            this.tb_weight.Location = new System.Drawing.Point(129, 282);
            this.tb_weight.MaxLength = 4;
            this.tb_weight.Name = "tb_weight";
            this.tb_weight.Size = new System.Drawing.Size(115, 22);
            this.tb_weight.TabIndex = 1;
            this.tb_weight.Text = "0";
            this.tb_weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_weight.WordWrap = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(4, 283);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Вага";
            // 
            // tb_CitySearch
            // 
            this.tb_CitySearch.Location = new System.Drawing.Point(7, 328);
            this.tb_CitySearch.MaxLength = 20;
            this.tb_CitySearch.Name = "tb_CitySearch";
            this.tb_CitySearch.Size = new System.Drawing.Size(202, 22);
            this.tb_CitySearch.TabIndex = 4;
            this.tb_CitySearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_CitySearch.WordWrap = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(4, 309);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Пошук насел.пункта";
            // 
            // cbCityList
            // 
            this.cbCityList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCityList.DropDownWidth = 350;
            this.cbCityList.FormattingEnabled = true;
            this.cbCityList.Location = new System.Drawing.Point(7, 356);
            this.cbCityList.Name = "cbCityList";
            this.cbCityList.Size = new System.Drawing.Size(237, 24);
            this.cbCityList.TabIndex = 6;
            this.cbCityList.SelectedIndexChanged += new System.EventHandler(this.cbCityList_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(4, 383);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 16);
            this.label9.TabIndex = 5;
            this.label9.Text = "Відділення";
            // 
            // cb_Warehouses
            // 
            this.cb_Warehouses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Warehouses.DropDownWidth = 400;
            this.cb_Warehouses.FormattingEnabled = true;
            this.cb_Warehouses.Location = new System.Drawing.Point(7, 402);
            this.cb_Warehouses.Name = "cb_Warehouses";
            this.cb_Warehouses.Size = new System.Drawing.Size(237, 24);
            this.cb_Warehouses.TabIndex = 6;
            // 
            // cb_payer
            // 
            this.cb_payer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_payer.FormattingEnabled = true;
            this.cb_payer.Location = new System.Drawing.Point(73, 121);
            this.cb_payer.Name = "cb_payer";
            this.cb_payer.Size = new System.Drawing.Size(171, 24);
            this.cb_payer.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(4, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 16);
            this.label10.TabIndex = 2;
            this.label10.Text = "Платник";
            // 
            // rb_ko_yes
            // 
            this.rb_ko_yes.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb_ko_yes.BackColor = System.Drawing.Color.LawnGreen;
            this.rb_ko_yes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb_ko_yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb_ko_yes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_ko_yes.Location = new System.Drawing.Point(149, 60);
            this.rb_ko_yes.Margin = new System.Windows.Forms.Padding(0);
            this.rb_ko_yes.Name = "rb_ko_yes";
            this.rb_ko_yes.Size = new System.Drawing.Size(49, 30);
            this.rb_ko_yes.TabIndex = 7;
            this.rb_ko_yes.TabStop = true;
            this.rb_ko_yes.Tag = "1";
            this.rb_ko_yes.Text = "так";
            this.rb_ko_yes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb_ko_yes.UseVisualStyleBackColor = false;
            // 
            // rb_ko_no
            // 
            this.rb_ko_no.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb_ko_no.BackColor = System.Drawing.Color.Red;
            this.rb_ko_no.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb_ko_no.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb_ko_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_ko_no.Location = new System.Drawing.Point(201, 60);
            this.rb_ko_no.Name = "rb_ko_no";
            this.rb_ko_no.Size = new System.Drawing.Size(43, 30);
            this.rb_ko_no.TabIndex = 7;
            this.rb_ko_no.TabStop = true;
            this.rb_ko_no.Tag = "0";
            this.rb_ko_no.Text = "ні";
            this.rb_ko_no.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb_ko_no.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Контроль оплати";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_cost
            // 
            this.tb_cost.Location = new System.Drawing.Point(129, 32);
            this.tb_cost.MaxLength = 0;
            this.tb_cost.Name = "tb_cost";
            this.tb_cost.Size = new System.Drawing.Size(115, 22);
            this.tb_cost.TabIndex = 1;
            this.tb_cost.Text = "0";
            this.tb_cost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_cost.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Оцінка";
            // 
            // but_search
            // 
            this.but_search.BackColor = System.Drawing.Color.White;
            this.but_search.BackgroundImage = global::SPM_Worker.Properties.Resources.Search;
            this.but_search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.but_search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.but_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.but_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.but_search.ForeColor = System.Drawing.Color.Red;
            this.but_search.Location = new System.Drawing.Point(215, 328);
            this.but_search.Name = "but_search";
            this.but_search.Size = new System.Drawing.Size(28, 23);
            this.but_search.TabIndex = 9;
            this.but_search.UseVisualStyleBackColor = false;
            this.but_search.Click += new System.EventHandler(this.but_search_Click);
            // 
            // NP_InternetDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.but_search);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rb_ko_no);
            this.Controls.Add(this.rb_ko_yes);
            this.Controls.Add(this.cb_Warehouses);
            this.Controls.Add(this.cb_payer);
            this.Controls.Add(this.cbCityList);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_CitySearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_cost);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_weight);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.tb_comm);
            this.Controls.Add(this.tb_ko);
            this.Controls.Add(this.dateTime);
            this.Name = "NP_InternetDocument";
            this.Size = new System.Drawing.Size(247, 429);
            this.Load += new System.EventHandler(this.NP_InternetDocument_Load);
            this.VisibleChanged += new System.EventHandler(this.NP_InternetDocument_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTime;
        private System.Windows.Forms.TextBox tb_ko;
        private System.Windows.Forms.TextBox tb_comm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPakHeig;
        private System.Windows.Forms.TextBox tbPakWidth;
        private System.Windows.Forms.TextBox tbPakLeng;
        private System.Windows.Forms.ComboBox cb_pakList;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox tb_weight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_CitySearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbCityList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_Warehouses;
        private System.Windows.Forms.ComboBox cb_payer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rb_ko_yes;
        private System.Windows.Forms.RadioButton rb_ko_no;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_cost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button but_search;
    }
}
