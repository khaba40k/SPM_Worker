
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
            this.filterPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label_date = new System.Windows.Forms.Label();
            this.dateTimeSort = new System.Windows.Forms.DateTimePicker();
            this.btnReset = new System.Windows.Forms.Button();
            this.lb_count_checked = new System.Windows.Forms.Label();
            this.flpList = new System.Windows.Forms.FlowLayoutPanel();
            this.cbFilter = new SPM_Core.CheckComboBox();
            this.panelTop.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Transparent;
            this.panelTop.Controls.Add(this.label_count);
            this.panelTop.Controls.Add(this.rb2);
            this.panelTop.Controls.Add(this.rb1);
            this.panelTop.Controls.Add(this.rb0);
            this.panelTop.Controls.Add(this.butDelete);
            this.panelTop.Controls.Add(this.but_print);
            this.panelTop.Controls.Add(this.filterPanel);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(861, 70);
            this.panelTop.TabIndex = 0;
            // 
            // label_count
            // 
            this.label_count.AutoSize = true;
            this.label_count.Location = new System.Drawing.Point(477, 13);
            this.label_count.Name = "label_count";
            this.label_count.Size = new System.Drawing.Size(115, 17);
            this.label_count.TabIndex = 7;
            this.label_count.Text = "ЗА СПИСКОМ: 0";
            // 
            // rb2
            // 
            this.rb2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb2.BackColor = System.Drawing.Color.Green;
            this.rb2.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rb2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb2.ForeColor = System.Drawing.Color.DarkBlue;
            this.rb2.Location = new System.Drawing.Point(324, 5);
            this.rb2.Margin = new System.Windows.Forms.Padding(0);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(150, 30);
            this.rb2.TabIndex = 6;
            this.rb2.Text = "АРХІВ";
            this.rb2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb2.UseVisualStyleBackColor = false;
            // 
            // rb1
            // 
            this.rb1.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb1.BackColor = System.Drawing.Color.Yellow;
            this.rb1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb1.ForeColor = System.Drawing.Color.DarkBlue;
            this.rb1.Location = new System.Drawing.Point(163, 5);
            this.rb1.Margin = new System.Windows.Forms.Padding(0);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(150, 30);
            this.rb1.TabIndex = 6;
            this.rb1.Text = "РОБОТА";
            this.rb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb1.UseVisualStyleBackColor = false;
            // 
            // rb0
            // 
            this.rb0.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb0.BackColor = System.Drawing.Color.Red;
            this.rb0.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb0.Checked = true;
            this.rb0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rb0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb0.ForeColor = System.Drawing.Color.DarkBlue;
            this.rb0.Location = new System.Drawing.Point(3, 5);
            this.rb0.Margin = new System.Windows.Forms.Padding(0);
            this.rb0.Name = "rb0";
            this.rb0.Size = new System.Drawing.Size(150, 30);
            this.rb0.TabIndex = 6;
            this.rb0.TabStop = true;
            this.rb0.Text = "НОВІ";
            this.rb0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb0.UseVisualStyleBackColor = false;
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
            this.but_print.Click += new System.EventHandler(this.but_print_Click);
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.Color.Transparent;
            this.filterPanel.Controls.Add(this.buttonCreate);
            this.filterPanel.Controls.Add(this.txtSearch);
            this.filterPanel.Controls.Add(this.cbFilter);
            this.filterPanel.Controls.Add(this.label_date);
            this.filterPanel.Controls.Add(this.dateTimeSort);
            this.filterPanel.Controls.Add(this.btnReset);
            this.filterPanel.Controls.Add(this.lb_count_checked);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.filterPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.filterPanel.Location = new System.Drawing.Point(0, 37);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(861, 33);
            this.filterPanel.TabIndex = 8;
            // 
            // buttonCreate
            // 
            this.buttonCreate.BackColor = System.Drawing.Color.Transparent;
            this.buttonCreate.BackgroundImage = global::SPM_Worker.Properties.Resources.createz;
            this.buttonCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonCreate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCreate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCreate.Location = new System.Drawing.Point(3, 3);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(33, 23);
            this.buttonCreate.TabIndex = 11;
            this.buttonCreate.TabStop = false;
            this.buttonCreate.UseVisualStyleBackColor = false;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSearch.Location = new System.Drawing.Point(42, 3);
            this.txtSearch.MaxLength = 20;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(167, 24);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.WordWrap = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label_date
            // 
            this.label_date.BackColor = System.Drawing.Color.Transparent;
            this.label_date.Location = new System.Drawing.Point(295, 0);
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
            this.dateTimeSort.Location = new System.Drawing.Point(414, 3);
            this.dateTimeSort.Name = "dateTimeSort";
            this.dateTimeSort.Size = new System.Drawing.Size(110, 22);
            this.dateTimeSort.TabIndex = 4;
            this.dateTimeSort.ValueChanged += new System.EventHandler(this.dateTimeSort_ValueChanged);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Location = new System.Drawing.Point(530, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(31, 24);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "X";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lb_count_checked
            // 
            this.lb_count_checked.Location = new System.Drawing.Point(567, 0);
            this.lb_count_checked.Name = "lb_count_checked";
            this.lb_count_checked.Size = new System.Drawing.Size(113, 30);
            this.lb_count_checked.TabIndex = 7;
            this.lb_count_checked.Text = "Вибрано: 0";
            this.lb_count_checked.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_count_checked.Visible = false;
            // 
            // flpList
            // 
            this.flpList.AccessibleRole = System.Windows.Forms.AccessibleRole.ListItem;
            this.flpList.AutoScroll = true;
            this.flpList.BackColor = System.Drawing.Color.CadetBlue;
            this.flpList.BackgroundImage = global::SPM_Worker.Properties.Resources.logo;
            this.flpList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.flpList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpList.Location = new System.Drawing.Point(0, 70);
            this.flpList.Margin = new System.Windows.Forms.Padding(0);
            this.flpList.Name = "flpList";
            this.flpList.Padding = new System.Windows.Forms.Padding(3);
            this.flpList.Size = new System.Drawing.Size(861, 156);
            this.flpList.TabIndex = 1;
            this.flpList.WrapContents = false;
            // 
            // cbFilter
            // 
            this.cbFilter.AutoSize = true;
            this.cbFilter.BackColor = System.Drawing.SystemColors.Control;
            this.cbFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbFilter.Location = new System.Drawing.Point(215, 3);
            this.cbFilter.MinimumSize = new System.Drawing.Size(74, 16);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Padding = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.cbFilter.Size = new System.Drawing.Size(74, 16);
            this.cbFilter.TabIndex = 12;
            // 
            // ZakazListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flpList);
            this.Controls.Add(this.panelTop);
            this.Name = "ZakazListControl";
            this.Size = new System.Drawing.Size(861, 226);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
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
        private System.Windows.Forms.FlowLayoutPanel filterPanel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.DateTimePicker dateTimeSort;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lb_count_checked;
        private System.Windows.Forms.Button buttonCreate;
        private SPM_Core.CheckComboBox cbFilter;
    }
}
