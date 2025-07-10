
namespace SPM_Worker
{
    partial class ZakazInfo
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
            this.components = new System.ComponentModel.Container();
            this.butCHANGE = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lb_Number = new System.Windows.Forms.Label();
            this.lb_type = new System.Windows.Forms.Label();
            this.lb_terminovo = new System.Windows.Forms.Label();
            this.lb_clientName = new System.Windows.Forms.Label();
            this.lb_phone = new System.Windows.Forms.Label();
            this.cb_Index = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // butCHANGE
            // 
            this.butCHANGE.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.butCHANGE.BackColor = System.Drawing.Color.Yellow;
            this.butCHANGE.BackgroundImage = global::SPM_Worker.Properties.Resources.write_z;
            this.butCHANGE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butCHANGE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butCHANGE.Location = new System.Drawing.Point(614, 3);
            this.butCHANGE.Name = "butCHANGE";
            this.butCHANGE.Size = new System.Drawing.Size(57, 45);
            this.butCHANGE.TabIndex = 20;
            this.butCHANGE.UseVisualStyleBackColor = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker1.CustomFormat = "dd.MM.yy  HH:mm";
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(465, 3);
            this.dateTimePicker1.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker1.MinDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(145, 24);
            this.dateTimePicker1.TabIndex = 18;
            this.dateTimePicker1.TabStop = false;
            this.dateTimePicker1.Value = new System.DateTime(2025, 7, 2, 20, 51, 50, 0);
            // 
            // lb_Number
            // 
            this.lb_Number.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_Number.AutoSize = true;
            this.lb_Number.BackColor = System.Drawing.Color.Transparent;
            this.lb_Number.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_Number.Location = new System.Drawing.Point(52, -2);
            this.lb_Number.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Number.Name = "lb_Number";
            this.lb_Number.Size = new System.Drawing.Size(69, 29);
            this.lb_Number.TabIndex = 15;
            this.lb_Number.Text = "1234";
            // 
            // lb_type
            // 
            this.lb_type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_type.AutoSize = true;
            this.lb_type.BackColor = System.Drawing.Color.Transparent;
            this.lb_type.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lb_type.Location = new System.Drawing.Point(138, 3);
            this.lb_type.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_type.Name = "lb_type";
            this.lb_type.Size = new System.Drawing.Size(154, 20);
            this.lb_type.TabIndex = 16;
            this.lb_type.Text = "Переобладнання";
            // 
            // lb_terminovo
            // 
            this.lb_terminovo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lb_terminovo.BackColor = System.Drawing.Color.DarkRed;
            this.lb_terminovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_terminovo.ForeColor = System.Drawing.Color.IndianRed;
            this.lb_terminovo.Location = new System.Drawing.Point(381, 3);
            this.lb_terminovo.Name = "lb_terminovo";
            this.lb_terminovo.Size = new System.Drawing.Size(76, 42);
            this.lb_terminovo.TabIndex = 23;
            this.lb_terminovo.Text = "терм.";
            this.lb_terminovo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_clientName
            // 
            this.lb_clientName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_clientName.BackColor = System.Drawing.Color.Transparent;
            this.lb_clientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_clientName.ForeColor = System.Drawing.Color.Navy;
            this.lb_clientName.Location = new System.Drawing.Point(52, 25);
            this.lb_clientName.Name = "lb_clientName";
            this.lb_clientName.Size = new System.Drawing.Size(323, 25);
            this.lb_clientName.TabIndex = 24;
            this.lb_clientName.Text = "Тимошенко А.В.";
            // 
            // lb_phone
            // 
            this.lb_phone.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lb_phone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_phone.ForeColor = System.Drawing.Color.Navy;
            this.lb_phone.Location = new System.Drawing.Point(445, 27);
            this.lb_phone.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lb_phone.Name = "lb_phone";
            this.lb_phone.Size = new System.Drawing.Size(165, 21);
            this.lb_phone.TabIndex = 17;
            this.lb_phone.Text = "0985992689";
            this.lb_phone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_Index
            // 
            this.cb_Index.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_Index.BackColor = System.Drawing.Color.Transparent;
            this.cb_Index.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_Index.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_Index.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_Index.Location = new System.Drawing.Point(0, 0);
            this.cb_Index.Name = "cb_Index";
            this.cb_Index.Size = new System.Drawing.Size(52, 51);
            this.cb_Index.TabIndex = 25;
            this.cb_Index.UseVisualStyleBackColor = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 400;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 0;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Інфо";
            // 
            // ZakazInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cb_Index);
            this.Controls.Add(this.butCHANGE);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lb_Number);
            this.Controls.Add(this.lb_type);
            this.Controls.Add(this.lb_terminovo);
            this.Controls.Add(this.lb_clientName);
            this.Controls.Add(this.lb_phone);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 2);
            this.Name = "ZakazInfo";
            this.Size = new System.Drawing.Size(676, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button butCHANGE;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lb_Number;
        private System.Windows.Forms.Label lb_type;
        private System.Windows.Forms.Label lb_terminovo;
        private System.Windows.Forms.Label lb_clientName;
        private System.Windows.Forms.Label lb_phone;
        private System.Windows.Forms.CheckBox cb_Index;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
