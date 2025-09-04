
namespace SPM_Worker
{
    partial class SoldCart
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
            this.lb_num = new System.Windows.Forms.Label();
            this.cb_name = new System.Windows.Forms.ComboBox();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.cb_color = new System.Windows.Forms.ComboBox();
            this.tb_count = new System.Windows.Forms.TextBox();
            this.tb_cost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.but_del = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_num
            // 
            this.lb_num.AutoSize = true;
            this.lb_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_num.Location = new System.Drawing.Point(0, 6);
            this.lb_num.Name = "lb_num";
            this.lb_num.Size = new System.Drawing.Size(21, 24);
            this.lb_num.TabIndex = 0;
            this.lb_num.Text = "1";
            // 
            // cb_name
            // 
            this.cb_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_name.BackColor = System.Drawing.SystemColors.Info;
            this.cb_name.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_name.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_name.ForeColor = System.Drawing.Color.Blue;
            this.cb_name.ItemHeight = 16;
            this.cb_name.Location = new System.Drawing.Point(34, 2);
            this.cb_name.Margin = new System.Windows.Forms.Padding(2);
            this.cb_name.Name = "cb_name";
            this.cb_name.Size = new System.Drawing.Size(237, 24);
            this.cb_name.TabIndex = 0;
            this.cb_name.TabStop = false;
            // 
            // cb_type
            // 
            this.cb_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_type.BackColor = System.Drawing.SystemColors.Info;
            this.cb_type.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_type.Enabled = false;
            this.cb_type.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_type.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_type.ForeColor = System.Drawing.Color.Blue;
            this.cb_type.Location = new System.Drawing.Point(60, 27);
            this.cb_type.Margin = new System.Windows.Forms.Padding(2);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(211, 24);
            this.cb_type.TabIndex = 2;
            this.cb_type.TabStop = false;
            this.cb_type.Visible = false;
            // 
            // cb_color
            // 
            this.cb_color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_color.BackColor = System.Drawing.SystemColors.Info;
            this.cb_color.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_color.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_color.Enabled = false;
            this.cb_color.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_color.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_color.ForeColor = System.Drawing.Color.Blue;
            this.cb_color.Location = new System.Drawing.Point(276, 2);
            this.cb_color.Margin = new System.Windows.Forms.Padding(2);
            this.cb_color.Name = "cb_color";
            this.cb_color.Size = new System.Drawing.Size(120, 23);
            this.cb_color.TabIndex = 3;
            this.cb_color.TabStop = false;
            this.cb_color.Visible = false;
            // 
            // tb_count
            // 
            this.tb_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_count.BackColor = System.Drawing.SystemColors.Info;
            this.tb_count.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_count.ForeColor = System.Drawing.Color.Blue;
            this.tb_count.Location = new System.Drawing.Point(322, 28);
            this.tb_count.Margin = new System.Windows.Forms.Padding(2);
            this.tb_count.MaxLength = 3;
            this.tb_count.Name = "tb_count";
            this.tb_count.ShortcutsEnabled = false;
            this.tb_count.Size = new System.Drawing.Size(74, 24);
            this.tb_count.TabIndex = 0;
            this.tb_count.Text = "1";
            this.tb_count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_count.WordWrap = false;
            // 
            // tb_cost
            // 
            this.tb_cost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_cost.BackColor = System.Drawing.SystemColors.Info;
            this.tb_cost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_cost.ForeColor = System.Drawing.Color.Blue;
            this.tb_cost.Location = new System.Drawing.Point(403, 28);
            this.tb_cost.Margin = new System.Windows.Forms.Padding(2);
            this.tb_cost.MaxLength = 10;
            this.tb_cost.Name = "tb_cost";
            this.tb_cost.ShortcutsEnabled = false;
            this.tb_cost.Size = new System.Drawing.Size(95, 24);
            this.tb_cost.TabIndex = 1;
            this.tb_cost.Text = "0.00";
            this.tb_cost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_cost.WordWrap = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "к-ть:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(400, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Вартість:";
            // 
            // but_del
            // 
            this.but_del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.but_del.Cursor = System.Windows.Forms.Cursors.Hand;
            this.but_del.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.but_del.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.but_del.ForeColor = System.Drawing.Color.Red;
            this.but_del.Location = new System.Drawing.Point(472, 0);
            this.but_del.Name = "but_del";
            this.but_del.Size = new System.Drawing.Size(27, 26);
            this.but_del.TabIndex = 6;
            this.but_del.TabStop = false;
            this.but_del.Text = "X";
            this.but_del.UseVisualStyleBackColor = true;
            // 
            // SoldCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.but_del);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_cost);
            this.Controls.Add(this.tb_count);
            this.Controls.Add(this.cb_color);
            this.Controls.Add(this.cb_type);
            this.Controls.Add(this.cb_name);
            this.Controls.Add(this.lb_num);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(0, 2, 0, 3);
            this.Name = "SoldCart";
            this.Size = new System.Drawing.Size(501, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_num;
        private System.Windows.Forms.ComboBox cb_name;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.ComboBox cb_color;
        private System.Windows.Forms.TextBox tb_count;
        private System.Windows.Forms.TextBox tb_cost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button but_del;
    }
}
