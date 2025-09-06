
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MAIN_FORM));
            this.MENU_CONTEXT = new System.Windows.Forms.MenuStrip();
            this.mcFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCSVhistory = new System.Windows.Forms.ToolStripMenuItem();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.tESTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MENU_CONTEXT.SuspendLayout();
            this.SuspendLayout();
            // 
            // MENU_CONTEXT
            // 
            this.MENU_CONTEXT.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MENU_CONTEXT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mcFile,
            this.tESTToolStripMenuItem});
            this.MENU_CONTEXT.Location = new System.Drawing.Point(0, 0);
            this.MENU_CONTEXT.Name = "MENU_CONTEXT";
            this.MENU_CONTEXT.Size = new System.Drawing.Size(982, 28);
            this.MENU_CONTEXT.TabIndex = 2;
            // 
            // mcFile
            // 
            this.mcFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCSVhistory});
            this.mcFile.Name = "mcFile";
            this.mcFile.Size = new System.Drawing.Size(59, 24);
            this.mcFile.Text = "Файл";
            // 
            // saveCSVhistory
            // 
            this.saveCSVhistory.Image = global::SPM_Worker.Properties.Resources.printdone_z;
            this.saveCSVhistory.Name = "saveCSVhistory";
            this.saveCSVhistory.Size = new System.Drawing.Size(190, 26);
            this.saveCSVhistory.Text = "Історія товару";
            this.saveCSVhistory.Visible = false;
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.Color.Transparent;
            this.panelContent.Location = new System.Drawing.Point(185, 28);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(797, 525);
            this.panelContent.TabIndex = 3;
            // 
            // panelMenu
            // 
            this.panelMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMenu.BackColor = System.Drawing.Color.Transparent;
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelMenu.Location = new System.Drawing.Point(0, 28);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(179, 525);
            this.panelMenu.TabIndex = 4;
            this.panelMenu.WrapContents = false;
            // 
            // tESTToolStripMenuItem
            // 
            this.tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            this.tESTToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.tESTToolStripMenuItem.Text = "TEST";
            this.tESTToolStripMenuItem.Click += new System.EventHandler(this.tESTToolStripMenuItem_Click);
            // 
            // MAIN_FORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SPM_Worker.Properties.Resources.fon;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.MENU_CONTEXT);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MENU_CONTEXT;
            this.Name = "MAIN_FORM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SHOLOM";
            this.MENU_CONTEXT.ResumeLayout(false);
            this.MENU_CONTEXT.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip MENU_CONTEXT;
        private System.Windows.Forms.ToolStripMenuItem mcFile;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.FlowLayoutPanel panelMenu;
        private System.Windows.Forms.ToolStripMenuItem saveCSVhistory;
        private System.Windows.Forms.ToolStripMenuItem tESTToolStripMenuItem;
    }
}