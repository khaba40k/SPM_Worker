
namespace SPM_Worker
{
    partial class MAIN_FORM
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
            this.menuPanel = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.zakazListControl1 = new SPM_Worker.ZakazListControl();
            this.buttAddZ = new System.Windows.Forms.Button();
            this.menuPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.Controls.Add(this.buttAddZ);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuPanel.Location = new System.Drawing.Point(0, 28);
            this.menuPanel.MinimumSize = new System.Drawing.Size(150, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(187, 542);
            this.menuPanel.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(966, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.zakazListControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(187, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(779, 542);
            this.panel1.TabIndex = 3;
            // 
            // zakazListControl1
            // 
            this.zakazListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zakazListControl1.Location = new System.Drawing.Point(0, 0);
            this.zakazListControl1.Name = "zakazListControl1";
            this.zakazListControl1.Size = new System.Drawing.Size(779, 542);
            this.zakazListControl1.STATUS = SPM_Worker.Z_STATUS.NEW;
            this.zakazListControl1.TabIndex = 0;
            // 
            // buttAddZ
            // 
            this.buttAddZ.AutoSize = true;
            this.buttAddZ.Image = global::SPM_Worker.Properties.Resources.createz;
            this.buttAddZ.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttAddZ.Location = new System.Drawing.Point(4, 4);
            this.buttAddZ.MinimumSize = new System.Drawing.Size(0, 76);
            this.buttAddZ.Name = "buttAddZ";
            this.buttAddZ.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.buttAddZ.Size = new System.Drawing.Size(177, 76);
            this.buttAddZ.TabIndex = 0;
            this.buttAddZ.Text = "Додати";
            this.buttAddZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttAddZ.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttAddZ.UseVisualStyleBackColor = true;
            // 
            // MAIN_FORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 570);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MAIN_FORM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MAIN_FORM";
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.Button buttAddZ;
        private System.Windows.Forms.Panel panel1;
        private ZakazListControl zakazListControl1;
    }
}