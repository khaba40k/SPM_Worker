
namespace SPM_Worker
{
    partial class ZakazListControl
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.label_count = new System.Windows.Forms.Label();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.rb0 = new System.Windows.Forms.RadioButton();
            this.butDelete = new System.Windows.Forms.Button();
            this.but_print = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cbSortOrder = new System.Windows.Forms.ComboBox();
            this.cbSortBy = new System.Windows.Forms.ComboBox();
            this.label_date = new System.Windows.Forms.Label();
            this.dateTimeSort = new System.Windows.Forms.DateTimePicker();
            this.btnReset = new System.Windows.Forms.Button();
            this.lb_count_checked = new System.Windows.Forms.Label();
            this.flpList = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTop.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label_count);
            this.panelTop.Controls.Add(this.rb2);
            this.panelTop.Controls.Add(this.rb1);
            this.panelTop.Controls.Add(this.rb0);
            this.panelTop.Controls.Add(this.butDelete);
            this.panelTop.Controls.Add(this.but_print);
            this.panelTop.Controls.Add(this.flowLayoutPanel1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(861, 70);
            this.panelTop.TabIndex = 0;
            // 
            // label_count
            // 
            this.label_count.AutoSize = true;
            this.label_count.Location = new System.Drawing.Point(463, 13);
            this.label_count.Name = "label_count";
            this.label_count.Size = new System.Drawing.Size(176, 17);
            this.label_count.TabIndex = 7;
            this.label_count.Text = "ВСЬОГО ЗА СПИСКОМ: 0";
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb2.Location = new System.Drawing.Point(277, 9);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(76, 22);
            this.rb2.TabIndex = 6;
            this.rb2.Text = "АРХІВ";
            this.rb2.UseVisualStyleBackColor = true;
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb1.Location = new System.Drawing.Point(133, 9);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(97, 22);
            this.rb1.TabIndex = 6;
            this.rb1.Text = "РОБОТА";
            this.rb1.UseVisualStyleBackColor = true;
            // 
            // rb0
            // 
            this.rb0.AutoSize = true;
            this.rb0.Checked = true;
            this.rb0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb0.Location = new System.Drawing.Point(15, 9);
            this.rb0.Name = "rb0";
            this.rb0.Size = new System.Drawing.Size(69, 22);
            this.rb0.TabIndex = 6;
            this.rb0.TabStop = true;
            this.rb0.Text = "НОВІ";
            this.rb0.UseVisualStyleBackColor = true;
            // 
            // butDelete
            // 
            this.butDelete.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.butDelete.BackgroundImage = global::SPM_Worker.Properties.Resources.del_z;
            this.butDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butDelete.Location = new System.Drawing.Point(809, 3);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(49, 43);
            this.butDelete.TabIndex = 3;
            this.butDelete.UseVisualStyleBackColor = true;
            this.butDelete.Visible = false;
            // 
            // but_print
            // 
            this.but_print.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.but_print.BackgroundImage = global::SPM_Worker.Properties.Resources.print;
            this.but_print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.but_print.Cursor = System.Windows.Forms.Cursors.Hand;
            this.but_print.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.but_print.Location = new System.Drawing.Point(755, 3);
            this.but_print.Name = "but_print";
            this.but_print.Size = new System.Drawing.Size(49, 43);
            this.but_print.TabIndex = 3;
            this.but_print.UseVisualStyleBackColor = true;
            this.but_print.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.txtSearch);
            this.flowLayoutPanel1.Controls.Add(this.cbSortOrder);
            this.flowLayoutPanel1.Controls.Add(this.cbSortBy);
            this.flowLayoutPanel1.Controls.Add(this.label_date);
            this.flowLayoutPanel1.Controls.Add(this.dateTimeSort);
            this.flowLayoutPanel1.Controls.Add(this.btnReset);
            this.flowLayoutPanel1.Controls.Add(this.lb_count_checked);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 37);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(861, 33);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSearch.Location = new System.Drawing.Point(3, 6);
            this.txtSearch.MaxLength = 20;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(167, 24);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.WordWrap = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cbSortOrder
            // 
            this.cbSortOrder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbSortOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSortOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSortOrder.FormattingEnabled = true;
            this.cbSortOrder.Location = new System.Drawing.Point(176, 5);
            this.cbSortOrder.MaxDropDownItems = 2;
            this.cbSortOrder.MaxLength = 2;
            this.cbSortOrder.Name = "cbSortOrder";
            this.cbSortOrder.Size = new System.Drawing.Size(64, 24);
            this.cbSortOrder.TabIndex = 1;
            this.cbSortOrder.SelectedIndexChanged += new System.EventHandler(this.cbSortOrder_SelectedIndexChanged);
            // 
            // cbSortBy
            // 
            this.cbSortBy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSortBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSortBy.FormattingEnabled = true;
            this.cbSortBy.Location = new System.Drawing.Point(246, 5);
            this.cbSortBy.Name = "cbSortBy";
            this.cbSortBy.Size = new System.Drawing.Size(70, 24);
            this.cbSortBy.TabIndex = 2;
            this.cbSortBy.SelectedIndexChanged += new System.EventHandler(this.cbSortBy_SelectedIndexChanged);
            // 
            // label_date
            // 
            this.label_date.BackColor = System.Drawing.Color.Transparent;
            this.label_date.Location = new System.Drawing.Point(322, 0);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(113, 33);
            this.label_date.TabIndex = 3;
            this.label_date.Text = "Показати від:";
            this.label_date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimeSort
            // 
            this.dateTimeSort.CustomFormat = "dd.MM.yy";
            this.dateTimeSort.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeSort.Location = new System.Drawing.Point(441, 8);
            this.dateTimeSort.Name = "dateTimeSort";
            this.dateTimeSort.Size = new System.Drawing.Size(110, 22);
            this.dateTimeSort.TabIndex = 4;
            this.dateTimeSort.ValueChanged += new System.EventHandler(this.dateTimeSort_ValueChanged);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnReset.Location = new System.Drawing.Point(557, 6);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(31, 24);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "X";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lb_count_checked
            // 
            this.lb_count_checked.Location = new System.Drawing.Point(594, 3);
            this.lb_count_checked.Name = "lb_count_checked";
            this.lb_count_checked.Size = new System.Drawing.Size(113, 30);
            this.lb_count_checked.TabIndex = 7;
            this.lb_count_checked.Text = "Вибрано: 0";
            this.lb_count_checked.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_count_checked.Visible = false;
            // 
            // flpList
            // 
            this.flpList.AutoScroll = true;
            this.flpList.BackColor = System.Drawing.Color.CadetBlue;
            this.flpList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpList.Location = new System.Drawing.Point(0, 70);
            this.flpList.Name = "flpList";
            this.flpList.Size = new System.Drawing.Size(861, 156);
            this.flpList.TabIndex = 1;
            this.flpList.WrapContents = false;
            // 
            // ZakazListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpList);
            this.Controls.Add(this.panelTop);
            this.Name = "ZakazListControl";
            this.Size = new System.Drawing.Size(861, 226);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.FlowLayoutPanel flpList;
        private System.Windows.Forms.Button but_print;
        private System.Windows.Forms.Button butDelete;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.RadioButton rb0;
        private System.Windows.Forms.Label label_count;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cbSortOrder;
        private System.Windows.Forms.ComboBox cbSortBy;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.DateTimePicker dateTimeSort;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lb_count_checked;
    }
}
