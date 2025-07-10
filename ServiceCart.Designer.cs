
namespace SPM_Worker
{
    partial class ServiceCart
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.rb0 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.title_service = new System.Windows.Forms.Label();
            this.panelBody = new System.Windows.Forms.Panel();
            this.tb_sum = new System.Windows.Forms.TextBox();
            this.cb_color = new System.Windows.Forms.ComboBox();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.panelHeader.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSize = true;
            this.panelHeader.BackColor = System.Drawing.Color.Teal;
            this.panelHeader.Controls.Add(this.tb_sum);
            this.panelHeader.Controls.Add(this.rb0);
            this.panelHeader.Controls.Add(this.rb2);
            this.panelHeader.Controls.Add(this.rb1);
            this.panelHeader.Controls.Add(this.title_service);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelHeader.MinimumSize = new System.Drawing.Size(0, 36);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(500, 38);
            this.panelHeader.TabIndex = 0;
            // 
            // rb0
            // 
            this.rb0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb0.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb0.AutoSize = true;
            this.rb0.BackColor = System.Drawing.Color.LimeGreen;
            this.rb0.Checked = true;
            this.rb0.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.rb0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb0.ForeColor = System.Drawing.Color.White;
            this.rb0.Location = new System.Drawing.Point(457, 4);
            this.rb0.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rb0.Name = "rb0";
            this.rb0.Size = new System.Drawing.Size(38, 31);
            this.rb0.TabIndex = 3;
            this.rb0.TabStop = true;
            this.rb0.Text = "НІ";
            this.rb0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb0.UseVisualStyleBackColor = false;
            // 
            // rb2
            // 
            this.rb2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb2.AutoSize = true;
            this.rb2.BackColor = System.Drawing.Color.LimeGreen;
            this.rb2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Olive;
            this.rb2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb2.ForeColor = System.Drawing.SystemColors.Info;
            this.rb2.Location = new System.Drawing.Point(321, 4);
            this.rb2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(79, 31);
            this.rb2.TabIndex = 4;
            this.rb2.Text = "Від зам.";
            this.rb2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb2.UseVisualStyleBackColor = false;
            // 
            // rb1
            // 
            this.rb1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb1.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb1.AutoSize = true;
            this.rb1.BackColor = System.Drawing.Color.LimeGreen;
            this.rb1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Olive;
            this.rb1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb1.ForeColor = System.Drawing.SystemColors.Info;
            this.rb1.Location = new System.Drawing.Point(403, 4);
            this.rb1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(51, 31);
            this.rb1.TabIndex = 5;
            this.rb1.Text = "ТАК";
            this.rb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb1.UseVisualStyleBackColor = false;
            // 
            // title_service
            // 
            this.title_service.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.title_service.BackColor = System.Drawing.Color.Transparent;
            this.title_service.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.title_service.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title_service.ForeColor = System.Drawing.Color.Yellow;
            this.title_service.Location = new System.Drawing.Point(4, 0);
            this.title_service.Name = "title_service";
            this.title_service.Size = new System.Drawing.Size(253, 37);
            this.title_service.TabIndex = 2;
            this.title_service.Text = "Навушники";
            // 
            // panelBody
            // 
            this.panelBody.Controls.Add(this.cb_color);
            this.panelBody.Controls.Add(this.cb_type);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 38);
            this.panelBody.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(500, 33);
            this.panelBody.TabIndex = 1;
            // 
            // tb_sum
            // 
            this.tb_sum.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tb_sum.Location = new System.Drawing.Point(262, 7);
            this.tb_sum.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tb_sum.MaxLength = 10;
            this.tb_sum.Name = "tb_sum";
            this.tb_sum.Size = new System.Drawing.Size(55, 27);
            this.tb_sum.TabIndex = 1;
            this.tb_sum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cb_color
            // 
            this.cb_color.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cb_color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_color.Enabled = false;
            this.cb_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_color.FormattingEnabled = true;
            this.cb_color.Location = new System.Drawing.Point(378, 3);
            this.cb_color.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cb_color.Name = "cb_color";
            this.cb_color.Size = new System.Drawing.Size(116, 27);
            this.cb_color.TabIndex = 0;
            this.cb_color.Visible = false;
            // 
            // cb_type
            // 
            this.cb_type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_type.Enabled = false;
            this.cb_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Location = new System.Drawing.Point(8, 3);
            this.cb_type.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(366, 27);
            this.cb_type.TabIndex = 0;
            this.cb_type.Visible = false;
            // 
            // ServiceCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Cyan;
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelHeader);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 7);
            this.MinimumSize = new System.Drawing.Size(400, 0);
            this.Name = "ServiceCart";
            this.Size = new System.Drawing.Size(500, 71);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelBody.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.RadioButton rb0;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.Label title_service;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.TextBox tb_sum;
        private System.Windows.Forms.ComboBox cb_color;
        private System.Windows.Forms.ComboBox cb_type;
    }
}
