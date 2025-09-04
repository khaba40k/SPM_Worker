
namespace SPM_Worker
{
    partial class Vitraty
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabWrite = new System.Windows.Forms.TabPage();
            this.butSave = new System.Windows.Forms.Button();
            this.soldInterface1 = new SPM_Worker.SoldInterface();
            this.tabJurnal = new System.Windows.Forms.TabPage();
            this.tableJurnal = new System.Windows.Forms.DataGridView();
            this.cellTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cellServiceNameType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cellColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cellCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cellCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cellComm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cellDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.datetimeJurnal = new System.Windows.Forms.DateTimePicker();
            this.dateTimeIn = new System.Windows.Forms.DateTimePicker();
            this.tabControl1.SuspendLayout();
            this.tabWrite.SuspendLayout();
            this.tabJurnal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableJurnal)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabWrite);
            this.tabControl1.Controls.Add(this.tabJurnal);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(600, 400);
            this.tabControl1.TabIndex = 0;
            // 
            // tabWrite
            // 
            this.tabWrite.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabWrite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabWrite.Controls.Add(this.dateTimeIn);
            this.tabWrite.Controls.Add(this.butSave);
            this.tabWrite.Controls.Add(this.soldInterface1);
            this.tabWrite.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabWrite.Location = new System.Drawing.Point(33, 4);
            this.tabWrite.Margin = new System.Windows.Forms.Padding(0);
            this.tabWrite.Name = "tabWrite";
            this.tabWrite.Size = new System.Drawing.Size(563, 392);
            this.tabWrite.TabIndex = 1;
            this.tabWrite.Text = "РЕДАГУВАННЯ";
            // 
            // butSave
            // 
            this.butSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butSave.BackColor = System.Drawing.Color.Green;
            this.butSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.butSave.Location = new System.Drawing.Point(415, 353);
            this.butSave.Margin = new System.Windows.Forms.Padding(0);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(140, 30);
            this.butSave.TabIndex = 0;
            this.butSave.Text = "ЗБЕРЕГТИ";
            this.butSave.UseVisualStyleBackColor = false;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // soldInterface1
            // 
            this.soldInterface1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soldInterface1.ATR = 0;
            this.soldInterface1.AutoSize = true;
            this.soldInterface1.BackColor = System.Drawing.Color.Transparent;
            this.soldInterface1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.soldInterface1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.soldInterface1.LayoutMode = SPM_Worker.SoldInterface.ControlLayoutMode.LeaveBottomSpace;
            this.soldInterface1.Location = new System.Drawing.Point(0, 0);
            this.soldInterface1.Margin = new System.Windows.Forms.Padding(0);
            this.soldInterface1.MinimumSize = new System.Drawing.Size(2, 50);
            this.soldInterface1.Name = "soldInterface1";
            this.soldInterface1.Size = new System.Drawing.Size(561, 348);
            this.soldInterface1.TabIndex = 1;
            // 
            // tabJurnal
            // 
            this.tabJurnal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabJurnal.Controls.Add(this.tableJurnal);
            this.tabJurnal.Controls.Add(this.datetimeJurnal);
            this.tabJurnal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabJurnal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabJurnal.Location = new System.Drawing.Point(33, 4);
            this.tabJurnal.Margin = new System.Windows.Forms.Padding(0);
            this.tabJurnal.Name = "tabJurnal";
            this.tabJurnal.Size = new System.Drawing.Size(563, 392);
            this.tabJurnal.TabIndex = 0;
            this.tabJurnal.Text = "ЖУРНАЛ";
            this.tabJurnal.UseVisualStyleBackColor = true;
            // 
            // tableJurnal
            // 
            this.tableJurnal.AllowUserToAddRows = false;
            this.tableJurnal.AllowUserToDeleteRows = false;
            this.tableJurnal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tableJurnal.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.tableJurnal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableJurnal.ColumnHeadersVisible = false;
            this.tableJurnal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cellTime,
            this.cellServiceNameType,
            this.cellColor,
            this.cellCount,
            this.cellCost,
            this.cellComm,
            this.cellDel});
            this.tableJurnal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableJurnal.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tableJurnal.Location = new System.Drawing.Point(0, 33);
            this.tableJurnal.MultiSelect = false;
            this.tableJurnal.Name = "tableJurnal";
            this.tableJurnal.ReadOnly = true;
            this.tableJurnal.RowHeadersVisible = false;
            this.tableJurnal.RowHeadersWidth = 51;
            this.tableJurnal.RowTemplate.Height = 24;
            this.tableJurnal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tableJurnal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tableJurnal.ShowCellErrors = false;
            this.tableJurnal.ShowCellToolTips = false;
            this.tableJurnal.ShowEditingIcon = false;
            this.tableJurnal.ShowRowErrors = false;
            this.tableJurnal.Size = new System.Drawing.Size(559, 355);
            this.tableJurnal.TabIndex = 2;
            // 
            // cellTime
            // 
            this.cellTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cellTime.HeaderText = "Час";
            this.cellTime.MaxInputLength = 5;
            this.cellTime.MinimumWidth = 40;
            this.cellTime.Name = "cellTime";
            this.cellTime.ReadOnly = true;
            this.cellTime.Width = 40;
            // 
            // cellServiceNameType
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cellServiceNameType.DefaultCellStyle = dataGridViewCellStyle7;
            this.cellServiceNameType.HeaderText = "Назва";
            this.cellServiceNameType.MaxInputLength = 100;
            this.cellServiceNameType.MinimumWidth = 30;
            this.cellServiceNameType.Name = "cellServiceNameType";
            this.cellServiceNameType.ReadOnly = true;
            // 
            // cellColor
            // 
            this.cellColor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = "-";
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cellColor.DefaultCellStyle = dataGridViewCellStyle8;
            this.cellColor.HeaderText = "Колір";
            this.cellColor.MaxInputLength = 15;
            this.cellColor.MinimumWidth = 15;
            this.cellColor.Name = "cellColor";
            this.cellColor.ReadOnly = true;
            this.cellColor.Width = 15;
            // 
            // cellCount
            // 
            this.cellCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Format = "0 шт";
            dataGridViewCellStyle9.NullValue = "-";
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cellCount.DefaultCellStyle = dataGridViewCellStyle9;
            this.cellCount.FillWeight = 53.96825F;
            this.cellCount.HeaderText = "К-ть";
            this.cellCount.MaxInputLength = 4;
            this.cellCount.MinimumWidth = 10;
            this.cellCount.Name = "cellCount";
            this.cellCount.ReadOnly = true;
            this.cellCount.Width = 10;
            // 
            // cellCost
            // 
            this.cellCost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = "-";
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cellCost.DefaultCellStyle = dataGridViewCellStyle10;
            this.cellCost.FillWeight = 53.96825F;
            this.cellCost.HeaderText = "Ціна";
            this.cellCost.MaxInputLength = 15;
            this.cellCost.MinimumWidth = 6;
            this.cellCost.Name = "cellCost";
            this.cellCost.ReadOnly = true;
            this.cellCost.Width = 6;
            // 
            // cellComm
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cellComm.DefaultCellStyle = dataGridViewCellStyle11;
            this.cellComm.FillWeight = 53.96825F;
            this.cellComm.HeaderText = "Коментар";
            this.cellComm.MaxInputLength = 500;
            this.cellComm.MinimumWidth = 6;
            this.cellComm.Name = "cellComm";
            this.cellComm.ReadOnly = true;
            // 
            // cellDel
            // 
            this.cellDel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle12.NullValue = "X";
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cellDel.DefaultCellStyle = dataGridViewCellStyle12;
            this.cellDel.FillWeight = 20F;
            this.cellDel.HeaderText = "Видалити";
            this.cellDel.MinimumWidth = 20;
            this.cellDel.Name = "cellDel";
            this.cellDel.ReadOnly = true;
            this.cellDel.Text = "X";
            this.cellDel.Width = 20;
            // 
            // datetimeJurnal
            // 
            this.datetimeJurnal.CustomFormat = "MMMM yyyy";
            this.datetimeJurnal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.datetimeJurnal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetimeJurnal.Location = new System.Drawing.Point(0, 0);
            this.datetimeJurnal.Margin = new System.Windows.Forms.Padding(0);
            this.datetimeJurnal.MaxDate = new System.DateTime(2040, 12, 31, 0, 0, 0, 0);
            this.datetimeJurnal.MinDate = new System.DateTime(2023, 10, 1, 0, 0, 0, 0);
            this.datetimeJurnal.Name = "datetimeJurnal";
            this.datetimeJurnal.ShowUpDown = true;
            this.datetimeJurnal.Size = new System.Drawing.Size(270, 30);
            this.datetimeJurnal.TabIndex = 1;
            this.datetimeJurnal.ValueChanged += new System.EventHandler(this.datetimeJurnal_ValueChanged);
            // 
            // dateTimeIn
            // 
            this.dateTimeIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimeIn.CustomFormat = " dd.MM.yyyyр. [HH:mm]";
            this.dateTimeIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeIn.Location = new System.Drawing.Point(3, 353);
            this.dateTimeIn.MinDate = new System.DateTime(2023, 10, 1, 0, 0, 0, 0);
            this.dateTimeIn.Name = "dateTimeIn";
            this.dateTimeIn.ShowUpDown = true;
            this.dateTimeIn.Size = new System.Drawing.Size(271, 30);
            this.dateTimeIn.TabIndex = 2;
            this.dateTimeIn.TabStop = false;
            // 
            // Vitraty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Vitraty";
            this.Size = new System.Drawing.Size(600, 400);
            this.SizeChanged += new System.EventHandler(this.Vitraty_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabWrite.ResumeLayout(false);
            this.tabWrite.PerformLayout();
            this.tabJurnal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableJurnal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabJurnal;
        private System.Windows.Forms.TabPage tabWrite;
        private System.Windows.Forms.DateTimePicker datetimeJurnal;
        private System.Windows.Forms.DataGridView tableJurnal;
        private System.Windows.Forms.DataGridViewTextBoxColumn cellTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cellServiceNameType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cellColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cellCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cellCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn cellComm;
        private System.Windows.Forms.DataGridViewButtonColumn cellDel;
        private SoldInterface soldInterface1;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.DateTimePicker dateTimeIn;
    }
}
