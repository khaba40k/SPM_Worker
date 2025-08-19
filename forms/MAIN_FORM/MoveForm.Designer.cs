
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
            this.tb1 = new System.Windows.Forms.TextBox();
            this.lb1 = new System.Windows.Forms.Label();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.lb2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOK.Location = new System.Drawing.Point(163, 196);
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
            this.buttonCancel.Location = new System.Drawing.Point(12, 196);
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
            this.tb0.Size = new System.Drawing.Size(226, 24);
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
            this.lb0.Size = new System.Drawing.Size(93, 24);
            this.lb0.TabIndex = 3;
            this.lb0.Text = "ТТН (вх)";
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(12, 93);
            this.tb1.MaxLength = 20;
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(226, 24);
            this.tb1.TabIndex = 1;
            this.tb1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb1.Visible = false;
            this.tb1.WordWrap = false;
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.BackColor = System.Drawing.Color.Transparent;
            this.lb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb1.ForeColor = System.Drawing.Color.White;
            this.lb1.Location = new System.Drawing.Point(12, 70);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(93, 24);
            this.lb1.TabIndex = 3;
            this.lb1.Text = "ТТН (вх)";
            this.lb1.Visible = false;
            // 
            // tb2
            // 
            this.tb2.Location = new System.Drawing.Point(12, 150);
            this.tb2.MaxLength = 20;
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(226, 24);
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
            this.lb2.Location = new System.Drawing.Point(12, 127);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(93, 24);
            this.lb2.TabIndex = 3;
            this.lb2.Text = "ТТН (вх)";
            this.lb2.Visible = false;
            // 
            // MoveForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::SPM_Worker.Properties.Resources.logo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(250, 240);
            this.Controls.Add(this.lb2);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.lb0);
            this.Controls.Add(this.tb2);
            this.Controls.Add(this.tb1);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox tb0;
        private System.Windows.Forms.Label lb0;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.Label lb2;
    }
}