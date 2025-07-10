
namespace SPM_Worker
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.FindOnLogTextBox = new System.Windows.Forms.TextBox();
            this.groupRadioCount = new System.Windows.Forms.GroupBox();
            this.minDateBar = new System.Windows.Forms.DateTimePicker();
            this.cbByMinDate = new System.Windows.Forms.CheckBox();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rb200 = new System.Windows.Forms.RadioButton();
            this.rb100 = new System.Windows.Forms.RadioButton();
            this.rb50 = new System.Windows.Forms.RadioButton();
            this.listFolders = new System.Windows.Forms.ListBox();
            this.butSettingOpen = new System.Windows.Forms.Button();
            this.butRefrSet = new System.Windows.Forms.Button();
            this.clear_but = new System.Windows.Forms.Button();
            this.log_text = new FastColoredTextBoxNS.FastColoredTextBox();
            this.title = new System.Windows.Forms.Label();
            this.groupRadioCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.log_text)).BeginInit();
            this.SuspendLayout();
            // 
            // FindOnLogTextBox
            // 
            this.FindOnLogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindOnLogTextBox.BackColor = System.Drawing.Color.White;
            this.FindOnLogTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FindOnLogTextBox.Location = new System.Drawing.Point(709, 6);
            this.FindOnLogTextBox.MaxLength = 100;
            this.FindOnLogTextBox.Name = "FindOnLogTextBox";
            this.FindOnLogTextBox.Size = new System.Drawing.Size(317, 24);
            this.FindOnLogTextBox.TabIndex = 0;
            this.FindOnLogTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FindOnLogTextBox.WordWrap = false;
            this.FindOnLogTextBox.TextChanged += new System.EventHandler(this.FindOnLogTextBox_TextChanged);
            // 
            // groupRadioCount
            // 
            this.groupRadioCount.Controls.Add(this.minDateBar);
            this.groupRadioCount.Controls.Add(this.cbByMinDate);
            this.groupRadioCount.Controls.Add(this.rbAll);
            this.groupRadioCount.Controls.Add(this.rb200);
            this.groupRadioCount.Controls.Add(this.rb100);
            this.groupRadioCount.Controls.Add(this.rb50);
            this.groupRadioCount.Enabled = false;
            this.groupRadioCount.Location = new System.Drawing.Point(4, 2);
            this.groupRadioCount.Name = "groupRadioCount";
            this.groupRadioCount.Size = new System.Drawing.Size(275, 94);
            this.groupRadioCount.TabIndex = 1;
            this.groupRadioCount.TabStop = false;
            this.groupRadioCount.Text = "Показати";
            // 
            // minDateBar
            // 
            this.minDateBar.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.minDateBar.Location = new System.Drawing.Point(98, 58);
            this.minDateBar.Name = "minDateBar";
            this.minDateBar.Size = new System.Drawing.Size(171, 22);
            this.minDateBar.TabIndex = 2;
            this.minDateBar.ValueChanged += new System.EventHandler(this.minDateBar_ValueChanged);
            // 
            // cbByMinDate
            // 
            this.cbByMinDate.AutoSize = true;
            this.cbByMinDate.Checked = true;
            this.cbByMinDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbByMinDate.Location = new System.Drawing.Point(8, 58);
            this.cbByMinDate.Name = "cbByMinDate";
            this.cbByMinDate.Size = new System.Drawing.Size(54, 21);
            this.cbByMinDate.TabIndex = 1;
            this.cbByMinDate.Text = "Від:";
            this.cbByMinDate.UseVisualStyleBackColor = true;
            this.cbByMinDate.CheckedChanged += new System.EventHandler(this.cbByMinDate_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(223, 31);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(46, 21);
            this.rbAll.TabIndex = 0;
            this.rbAll.Text = "всі";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rb200
            // 
            this.rb200.AutoSize = true;
            this.rb200.Location = new System.Drawing.Point(146, 31);
            this.rb200.Name = "rb200";
            this.rb200.Size = new System.Drawing.Size(53, 21);
            this.rb200.TabIndex = 0;
            this.rb200.Text = "200";
            this.rb200.UseVisualStyleBackColor = true;
            // 
            // rb100
            // 
            this.rb100.AutoSize = true;
            this.rb100.Location = new System.Drawing.Point(71, 31);
            this.rb100.Name = "rb100";
            this.rb100.Size = new System.Drawing.Size(53, 21);
            this.rb100.TabIndex = 0;
            this.rb100.Text = "100";
            this.rb100.UseVisualStyleBackColor = true;
            // 
            // rb50
            // 
            this.rb50.AutoSize = true;
            this.rb50.Checked = true;
            this.rb50.Location = new System.Drawing.Point(8, 31);
            this.rb50.Name = "rb50";
            this.rb50.Size = new System.Drawing.Size(45, 21);
            this.rb50.TabIndex = 0;
            this.rb50.TabStop = true;
            this.rb50.Text = "50";
            this.rb50.UseVisualStyleBackColor = true;
            // 
            // listFolders
            // 
            this.listFolders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listFolders.FormattingEnabled = true;
            this.listFolders.ItemHeight = 16;
            this.listFolders.Location = new System.Drawing.Point(4, 93);
            this.listFolders.Name = "listFolders";
            this.listFolders.Size = new System.Drawing.Size(275, 468);
            this.listFolders.TabIndex = 2;
            this.listFolders.SelectedIndexChanged += new System.EventHandler(this.listFolders_SelectedIndexChanged);
            // 
            // butSettingOpen
            // 
            this.butSettingOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSettingOpen.BackColor = System.Drawing.Color.Teal;
            this.butSettingOpen.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.butSettingOpen.Location = new System.Drawing.Point(1083, 2);
            this.butSettingOpen.Name = "butSettingOpen";
            this.butSettingOpen.Size = new System.Drawing.Size(129, 30);
            this.butSettingOpen.TabIndex = 4;
            this.butSettingOpen.Text = "Налаштування";
            this.butSettingOpen.UseVisualStyleBackColor = false;
            this.butSettingOpen.Click += new System.EventHandler(this.butSettingOpen_Click);
            // 
            // butRefrSet
            // 
            this.butRefrSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butRefrSet.BackColor = System.Drawing.Color.Teal;
            this.butRefrSet.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.butRefrSet.Location = new System.Drawing.Point(1032, 1);
            this.butRefrSet.Name = "butRefrSet";
            this.butRefrSet.Size = new System.Drawing.Size(45, 30);
            this.butRefrSet.TabIndex = 4;
            this.butRefrSet.Text = "upl";
            this.butRefrSet.UseVisualStyleBackColor = false;
            this.butRefrSet.Click += new System.EventHandler(this.butRefrSet_Click);
            // 
            // clear_but
            // 
            this.clear_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clear_but.BackColor = System.Drawing.Color.Red;
            this.clear_but.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clear_but.ForeColor = System.Drawing.Color.White;
            this.clear_but.Location = new System.Drawing.Point(1218, 2);
            this.clear_but.Name = "clear_but";
            this.clear_but.Size = new System.Drawing.Size(132, 31);
            this.clear_but.TabIndex = 0;
            this.clear_but.Text = "ОЧИСТИТИ";
            this.clear_but.UseVisualStyleBackColor = false;
            this.clear_but.Click += new System.EventHandler(this.clear_but_Click);
            // 
            // log_text
            // 
            this.log_text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log_text.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.log_text.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);";
            this.log_text.AutoScrollMinSize = new System.Drawing.Size(0, 22);
            this.log_text.BackBrush = null;
            this.log_text.BackColor = System.Drawing.Color.Transparent;
            this.log_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.log_text.CharHeight = 22;
            this.log_text.CharWidth = 9;
            this.log_text.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.log_text.DefaultMarkerSize = 8;
            this.log_text.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.log_text.Font = new System.Drawing.Font("Courier New", 9F);
            this.log_text.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.log_text.IsReplaceMode = false;
            this.log_text.LineInterval = 6;
            this.log_text.Location = new System.Drawing.Point(285, 33);
            this.log_text.Name = "log_text";
            this.log_text.PaddingBackColor = System.Drawing.Color.Black;
            this.log_text.Paddings = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.log_text.PreferredLineWidth = 17;
            this.log_text.ReadOnly = true;
            this.log_text.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.log_text.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("log_text.ServiceColors")));
            this.log_text.Size = new System.Drawing.Size(1065, 557);
            this.log_text.TabIndex = 5;
            this.log_text.WordWrap = true;
            this.log_text.Zoom = 100;
            this.log_text.Load += new System.EventHandler(this.log_text_Load);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title.ForeColor = System.Drawing.Color.Maroon;
            this.title.Location = new System.Drawing.Point(310, 8);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(61, 22);
            this.title.TabIndex = 6;
            this.title.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 602);
            this.Controls.Add(this.title);
            this.Controls.Add(this.log_text);
            this.Controls.Add(this.clear_but);
            this.Controls.Add(this.butRefrSet);
            this.Controls.Add(this.butSettingOpen);
            this.Controls.Add(this.listFolders);
            this.Controls.Add(this.groupRadioCount);
            this.Controls.Add(this.FindOnLogTextBox);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error_log";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupRadioCount.ResumeLayout(false);
            this.groupRadioCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.log_text)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FindOnLogTextBox;
        private System.Windows.Forms.GroupBox groupRadioCount;
        private System.Windows.Forms.ListBox listFolders;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rb200;
        private System.Windows.Forms.RadioButton rb100;
        private System.Windows.Forms.RadioButton rb50;
        private System.Windows.Forms.Button butSettingOpen;
        private System.Windows.Forms.Button butRefrSet;
        private System.Windows.Forms.Button clear_but;
        private FastColoredTextBoxNS.FastColoredTextBox log_text;
        private System.Windows.Forms.DateTimePicker minDateBar;
        private System.Windows.Forms.CheckBox cbByMinDate;
        private System.Windows.Forms.Label title;
    }
}

