
namespace SPM_Worker
{
    partial class NP_form
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
            this.but_OK = new System.Windows.Forms.Button();
            this.cb_obl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_city = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_vidd = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_rjn = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.butCancel = new System.Windows.Forms.Button();
            this.butUpdate = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbSearch = new System.Windows.Forms.ListBox();
            this.tb_number = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // but_OK
            // 
            this.but_OK.BackColor = System.Drawing.Color.Gray;
            this.but_OK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.but_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.but_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.but_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.but_OK.ForeColor = System.Drawing.Color.White;
            this.but_OK.Location = new System.Drawing.Point(362, 264);
            this.but_OK.Margin = new System.Windows.Forms.Padding(4);
            this.but_OK.Name = "but_OK";
            this.but_OK.Size = new System.Drawing.Size(128, 58);
            this.but_OK.TabIndex = 5;
            this.but_OK.Text = "OK";
            this.but_OK.UseVisualStyleBackColor = false;
            // 
            // cb_obl
            // 
            this.cb_obl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_obl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_obl.FormattingEnabled = true;
            this.cb_obl.Location = new System.Drawing.Point(101, 6);
            this.cb_obl.Margin = new System.Windows.Forms.Padding(4);
            this.cb_obl.Name = "cb_obl";
            this.cb_obl.Size = new System.Drawing.Size(345, 30);
            this.cb_obl.Sorted = true;
            this.cb_obl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Область";
            // 
            // cb_city
            // 
            this.cb_city.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_city.DropDownWidth = 300;
            this.cb_city.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_city.FormattingEnabled = true;
            this.cb_city.Location = new System.Drawing.Point(176, 74);
            this.cb_city.Margin = new System.Windows.Forms.Padding(4);
            this.cb_city.Name = "cb_city";
            this.cb_city.Size = new System.Drawing.Size(312, 30);
            this.cb_city.Sorted = true;
            this.cb_city.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Населений пункт";
            // 
            // cb_vidd
            // 
            this.cb_vidd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_vidd.DropDownWidth = 550;
            this.cb_vidd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_vidd.FormattingEnabled = true;
            this.cb_vidd.Location = new System.Drawing.Point(228, 109);
            this.cb_vidd.Margin = new System.Windows.Forms.Padding(4);
            this.cb_vidd.Name = "cb_vidd";
            this.cb_vidd.Size = new System.Drawing.Size(260, 30);
            this.cb_vidd.TabIndex = 4;
            this.cb_vidd.SelectedIndexChanged += new System.EventHandler(this.cb_vidd_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Відділення №:";
            // 
            // cb_rjn
            // 
            this.cb_rjn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_rjn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_rjn.FormattingEnabled = true;
            this.cb_rjn.Location = new System.Drawing.Point(101, 40);
            this.cb_rjn.Margin = new System.Windows.Forms.Padding(4);
            this.cb_rjn.Name = "cb_rjn";
            this.cb_rjn.Size = new System.Drawing.Size(345, 30);
            this.cb_rjn.Sorted = true;
            this.cb_rjn.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(7, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "Район";
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.BackColor = System.Drawing.Color.DarkGreen;
            this.butCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butCancel.ForeColor = System.Drawing.Color.Yellow;
            this.butCancel.Location = new System.Drawing.Point(453, 0);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(47, 67);
            this.butCancel.TabIndex = 6;
            this.butCancel.Text = "X";
            this.butCancel.UseVisualStyleBackColor = false;
            // 
            // butUpdate
            // 
            this.butUpdate.BackColor = System.Drawing.Color.Maroon;
            this.butUpdate.BackgroundImage = global::SPM_Worker.Properties.Resources.printdone_z;
            this.butUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.butUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butUpdate.ForeColor = System.Drawing.Color.Yellow;
            this.butUpdate.Location = new System.Drawing.Point(12, 264);
            this.butUpdate.Name = "butUpdate";
            this.butUpdate.Size = new System.Drawing.Size(343, 59);
            this.butUpdate.TabIndex = 7;
            this.butUpdate.Text = "!!! ОНОВИТИ кеш Нової Пошти !!!";
            this.butUpdate.UseVisualStyleBackColor = false;
            this.butUpdate.Visible = false;
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(94, 143);
            this.tbSearch.MaxLength = 30;
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(394, 28);
            this.tbSearch.TabIndex = 0;
            this.tbSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(6, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "Пошук:";
            // 
            // lbSearch
            // 
            this.lbSearch.Enabled = false;
            this.lbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbSearch.FormattingEnabled = true;
            this.lbSearch.ItemHeight = 16;
            this.lbSearch.Location = new System.Drawing.Point(12, 173);
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.Size = new System.Drawing.Size(478, 84);
            this.lbSearch.TabIndex = 10;
            // 
            // tb_number
            // 
            this.tb_number.Location = new System.Drawing.Point(143, 109);
            this.tb_number.MaxLength = 7;
            this.tb_number.Name = "tb_number";
            this.tb_number.ShortcutsEnabled = false;
            this.tb_number.Size = new System.Drawing.Size(78, 28);
            this.tb_number.TabIndex = 11;
            this.tb_number.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_number.WordWrap = false;
            this.tb_number.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tb_number_MouseClick);
            this.tb_number.TextChanged += new System.EventHandler(this.tb_number_TextChanged);
            // 
            // NP_form
            // 
            this.AcceptButton = this.but_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(500, 332);
            this.Controls.Add(this.tb_number);
            this.Controls.Add(this.lbSearch);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.butUpdate);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_rjn);
            this.Controls.Add(this.cb_vidd);
            this.Controls.Add(this.cb_city);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_obl);
            this.Controls.Add(this.but_OK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NP_form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button but_OK;
        private System.Windows.Forms.ComboBox cb_obl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_city;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_vidd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_rjn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butUpdate;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbSearch;
        private System.Windows.Forms.TextBox tb_number;
    }
}