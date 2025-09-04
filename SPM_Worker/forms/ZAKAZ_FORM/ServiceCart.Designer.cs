
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
            this.cb_color = new System.Windows.Forms.ComboBox();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.tb_sum = new System.Windows.Forms.TextBox();
            this.rb0 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cb_color
            // 
            this.cb_color.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_color.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_color.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_color.FormattingEnabled = true;
            this.cb_color.Location = new System.Drawing.Point(378, 36);
            this.cb_color.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cb_color.Name = "cb_color";
            this.cb_color.Size = new System.Drawing.Size(116, 28);
            this.cb_color.TabIndex = 6;
            this.cb_color.Visible = false;
            // 
            // cb_type
            // 
            this.cb_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Location = new System.Drawing.Point(8, 36);
            this.cb_type.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(366, 27);
            this.cb_type.TabIndex = 7;
            this.cb_type.Visible = false;
            // 
            // tb_sum
            // 
            this.tb_sum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_sum.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_sum.Location = new System.Drawing.Point(300, 4);
            this.tb_sum.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tb_sum.MaxLength = 12;
            this.tb_sum.Name = "tb_sum";
            this.tb_sum.ShortcutsEnabled = false;
            this.tb_sum.Size = new System.Drawing.Size(50, 28);
            this.tb_sum.TabIndex = 8;
            this.tb_sum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_sum.WordWrap = false;
            // 
            // rb0
            // 
            this.rb0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb0.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb0.AutoSize = true;
            this.rb0.BackColor = System.Drawing.Color.LimeGreen;
            this.rb0.Checked = true;
            this.rb0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb0.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.rb0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb0.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb0.ForeColor = System.Drawing.Color.White;
            this.rb0.Location = new System.Drawing.Point(461, 4);
            this.rb0.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rb0.Name = "rb0";
            this.rb0.Size = new System.Drawing.Size(34, 28);
            this.rb0.TabIndex = 10;
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
            this.rb2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Olive;
            this.rb2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb2.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb2.ForeColor = System.Drawing.SystemColors.Info;
            this.rb2.Location = new System.Drawing.Point(349, 4);
            this.rb2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(67, 28);
            this.rb2.TabIndex = 11;
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
            this.rb1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Olive;
            this.rb1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb1.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb1.ForeColor = System.Drawing.SystemColors.Info;
            this.rb1.Location = new System.Drawing.Point(414, 4);
            this.rb1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(46, 28);
            this.rb1.TabIndex = 12;
            this.rb1.Text = "ТАК";
            this.rb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb1.UseVisualStyleBackColor = false;
            // 
            // ServiceCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Controls.Add(this.tb_sum);
            this.Controls.Add(this.rb0);
            this.Controls.Add(this.rb2);
            this.Controls.Add(this.rb1);
            this.Controls.Add(this.cb_color);
            this.Controls.Add(this.cb_type);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.MinimumSize = new System.Drawing.Size(400, 0);
            this.Name = "ServiceCart";
            this.Size = new System.Drawing.Size(500, 66);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ServiceCart_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_color;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.TextBox tb_sum;
        private System.Windows.Forms.RadioButton rb0;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb1;
    }
}
