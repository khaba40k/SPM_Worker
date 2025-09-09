
namespace SPM_Worker
{
    partial class MoveForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tb0 = new System.Windows.Forms.TextBox();
            this.lb0 = new System.Windows.Forms.Label();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.lb2 = new System.Windows.Forms.Label();
            this.ttnNeed = new System.Windows.Forms.GroupBox();
            this.rb_no = new System.Windows.Forms.RadioButton();
            this.rb_yes = new System.Windows.Forms.RadioButton();
            this.nP_InternetDocument1 = new SPM_Worker.NP_InternetDocument();
            this.ttnNeed.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Enabled = false;
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOK.Location = new System.Drawing.Point(211, 450);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 32);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonCancel.Location = new System.Drawing.Point(12, 450);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 32);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "відміна";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // tb0
            // 
            this.tb0.Location = new System.Drawing.Point(12, 40);
            this.tb0.MaxLength = 20;
            this.tb0.Name = "tb0";
            this.tb0.Size = new System.Drawing.Size(277, 24);
            this.tb0.TabIndex = 0;
            this.tb0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb0.WordWrap = false;
            // 
            // lb0
            // 
            this.lb0.AutoSize = true;
            this.lb0.BackColor = System.Drawing.Color.Transparent;
            this.lb0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb0.ForeColor = System.Drawing.Color.White;
            this.lb0.Location = new System.Drawing.Point(12, 17);
            this.lb0.Name = "lb0";
            this.lb0.Size = new System.Drawing.Size(91, 22);
            this.lb0.TabIndex = 3;
            this.lb0.Text = "ТТН (вх)";
            // 
            // tb2
            // 
            this.tb2.Location = new System.Drawing.Point(12, 179);
            this.tb2.MaxLength = 20;
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(274, 24);
            this.tb2.TabIndex = 2;
            this.tb2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb2.Visible = false;
            this.tb2.WordWrap = false;
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.BackColor = System.Drawing.Color.Transparent;
            this.lb2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb2.ForeColor = System.Drawing.Color.White;
            this.lb2.Location = new System.Drawing.Point(12, 156);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(91, 22);
            this.lb2.TabIndex = 3;
            this.lb2.Text = "ТТН (вх)";
            this.lb2.Visible = false;
            // 
            // ttnNeed
            // 
            this.ttnNeed.BackColor = System.Drawing.Color.Transparent;
            this.ttnNeed.Controls.Add(this.rb_no);
            this.ttnNeed.Controls.Add(this.rb_yes);
            this.ttnNeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ttnNeed.ForeColor = System.Drawing.Color.White;
            this.ttnNeed.Location = new System.Drawing.Point(13, 65);
            this.ttnNeed.Name = "ttnNeed";
            this.ttnNeed.Size = new System.Drawing.Size(276, 88);
            this.ttnNeed.TabIndex = 5;
            this.ttnNeed.TabStop = false;
            this.ttnNeed.Text = "Накладна";
            // 
            // rb_no
            // 
            this.rb_no.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb_no.BackColor = System.Drawing.Color.Red;
            this.rb_no.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb_no.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_no.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rb_no.Location = new System.Drawing.Point(149, 27);
            this.rb_no.Name = "rb_no";
            this.rb_no.Size = new System.Drawing.Size(103, 42);
            this.rb_no.TabIndex = 0;
            this.rb_no.Tag = "0";
            this.rb_no.Text = "НІ";
            this.rb_no.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb_no.UseVisualStyleBackColor = false;
            // 
            // rb_yes
            // 
            this.rb_yes.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb_yes.BackColor = System.Drawing.Color.Chartreuse;
            this.rb_yes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rb_yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb_yes.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_yes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rb_yes.Location = new System.Drawing.Point(23, 27);
            this.rb_yes.Name = "rb_yes";
            this.rb_yes.Size = new System.Drawing.Size(103, 42);
            this.rb_yes.TabIndex = 0;
            this.rb_yes.Tag = "1";
            this.rb_yes.Text = "ТАК";
            this.rb_yes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb_yes.UseVisualStyleBackColor = false;
            // 
            // nP_InternetDocument1
            // 
            this.nP_InternetDocument1.BackColor = System.Drawing.Color.Red;
            this.nP_InternetDocument1.Location = new System.Drawing.Point(295, 3);
            this.nP_InternetDocument1.Name = "nP_InternetDocument1";
            this.nP_InternetDocument1.Size = new System.Drawing.Size(276, 484);
            this.nP_InternetDocument1.TabIndex = 6;
            this.nP_InternetDocument1.Load += new System.EventHandler(this.nP_InternetDocument1_Load);
            // 
            // MoveForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(577, 494);
            this.Controls.Add(this.nP_InternetDocument1);
            this.Controls.Add(this.ttnNeed);
            this.Controls.Add(this.lb2);
            this.Controls.Add(this.lb0);
            this.Controls.Add(this.tb2);
            this.Controls.Add(this.tb0);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MoveForm";
            this.ttnNeed.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox tb0;
        private System.Windows.Forms.Label lb0;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.GroupBox ttnNeed;
        private System.Windows.Forms.RadioButton rb_no;
        private System.Windows.Forms.RadioButton rb_yes;
        private NP_InternetDocument nP_InternetDocument1;
    }
}