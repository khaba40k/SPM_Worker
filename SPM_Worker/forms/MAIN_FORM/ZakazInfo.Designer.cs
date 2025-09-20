
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
            this.cb_Index = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmPrintFromWorker = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPrintShort = new System.Windows.Forms.ToolStripMenuItem();
            this.cbShablonTop = new System.Windows.Forms.ToolStripMenuItem();
            this.cbShablonBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.cbShablonAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmWriteZakaz = new System.Windows.Forms.ToolStripMenuItem();
            this.cmShablon = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_npSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.zakazContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.np_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.print_ttn_zebra = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.delete_ttn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.print_ttn_Document = new System.Windows.Forms.ToolStripMenuItem();
            this.zakazContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_Index
            // 
            this.cb_Index.BackColor = System.Drawing.Color.Transparent;
            this.cb_Index.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_Index.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_Index.Dock = System.Windows.Forms.DockStyle.Left;
            this.cb_Index.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_Index.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_Index.Location = new System.Drawing.Point(0, 0);
            this.cb_Index.Name = "cb_Index";
            this.cb_Index.Size = new System.Drawing.Size(35, 50);
            this.cb_Index.TabIndex = 25;
            this.cb_Index.TabStop = false;
            this.cb_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_Index.UseVisualStyleBackColor = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.OwnerDraw = true;
            this.toolTip1.ReshowDelay = 500;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Інфо";
            // 
            // cmPrintFromWorker
            // 
            this.cmPrintFromWorker.Image = global::SPM_Worker.Properties.Resources.human;
            this.cmPrintFromWorker.Name = "cmPrintFromWorker";
            this.cmPrintFromWorker.Size = new System.Drawing.Size(192, 26);
            this.cmPrintFromWorker.Text = "Для робітника";
            this.cmPrintFromWorker.Click += new System.EventHandler(this.cmPrintFromWorker_Click);
            // 
            // cmPrintShort
            // 
            this.cmPrintShort.Image = global::SPM_Worker.Properties.Resources.img_prt;
            this.cmPrintShort.Name = "cmPrintShort";
            this.cmPrintShort.Size = new System.Drawing.Size(192, 26);
            this.cmPrintShort.Text = "Стисло";
            this.cmPrintShort.Click += new System.EventHandler(this.cmPrintShort_Click);
            // 
            // cbShablonTop
            // 
            this.cbShablonTop.Image = global::SPM_Worker.Properties.Resources.human;
            this.cbShablonTop.Name = "cbShablonTop";
            this.cbShablonTop.Size = new System.Drawing.Size(228, 26);
            this.cbShablonTop.Text = "Без комплектуючих";
            this.cbShablonTop.Click += new System.EventHandler(this.cbShablonTop_Click);
            // 
            // cbShablonBottom
            // 
            this.cbShablonBottom.Image = global::SPM_Worker.Properties.Resources.printdone_z;
            this.cbShablonBottom.Name = "cbShablonBottom";
            this.cbShablonBottom.Size = new System.Drawing.Size(228, 26);
            this.cbShablonBottom.Text = "Комплектуючі";
            this.cbShablonBottom.Click += new System.EventHandler(this.cbShablonBottom_Click);
            // 
            // cbShablonAll
            // 
            this.cbShablonAll.Image = global::SPM_Worker.Properties.Resources.document_move;
            this.cbShablonAll.Name = "cbShablonAll";
            this.cbShablonAll.Size = new System.Drawing.Size(228, 26);
            this.cbShablonAll.Text = "Перенести все";
            this.cbShablonAll.Click += new System.EventHandler(this.cbShablonAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
            // 
            // cmWriteZakaz
            // 
            this.cmWriteZakaz.Image = global::SPM_Worker.Properties.Resources.write_z;
            this.cmWriteZakaz.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmWriteZakaz.Name = "cmWriteZakaz";
            this.cmWriteZakaz.ShowShortcutKeys = false;
            this.cmWriteZakaz.Size = new System.Drawing.Size(224, 26);
            this.cmWriteZakaz.Text = "Редагувати | Enter";
            this.cmWriteZakaz.Click += new System.EventHandler(this.cmWriteZakaz_Click);
            // 
            // cmShablon
            // 
            this.cmShablon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbShablonAll,
            this.toolStripSeparator3,
            this.cbShablonTop,
            this.cbShablonBottom});
            this.cmShablon.Image = global::SPM_Worker.Properties.Resources.document_move;
            this.cmShablon.Name = "cmShablon";
            this.cmShablon.Size = new System.Drawing.Size(224, 26);
            this.cmShablon.Text = "Як шаблон...";
            this.cmShablon.Click += new System.EventHandler(this.cmShablon_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(225, 6);
            // 
            // cmPrint
            // 
            this.cmPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmPrintFromWorker,
            this.cmPrintShort});
            this.cmPrint.Image = global::SPM_Worker.Properties.Resources.print;
            this.cmPrint.Name = "cmPrint";
            this.cmPrint.Size = new System.Drawing.Size(224, 26);
            this.cmPrint.Text = "Друкувати | CTRL + P";
            this.cmPrint.Click += new System.EventHandler(this.cmPrint_Click);
            // 
            // ts_npSeparator
            // 
            this.ts_npSeparator.Name = "ts_npSeparator";
            this.ts_npSeparator.Size = new System.Drawing.Size(221, 6);
            this.ts_npSeparator.Visible = false;
            // 
            // cmDelete
            // 
            this.cmDelete.BackColor = System.Drawing.SystemColors.Control;
            this.cmDelete.ForeColor = System.Drawing.Color.Red;
            this.cmDelete.Image = global::SPM_Worker.Properties.Resources.del_z;
            this.cmDelete.Name = "cmDelete";
            this.cmDelete.Size = new System.Drawing.Size(224, 26);
            this.cmDelete.Text = "Видалити";
            this.cmDelete.Click += new System.EventHandler(this.cmDelete_Click);
            // 
            // zakazContext
            // 
            this.zakazContext.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.zakazContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmWriteZakaz,
            this.toolStripSeparator4,
            this.cmShablon,
            this.cmPrint,
            this.ts_npSeparator,
            this.np_tsmi,
            this.toolStripSeparator5,
            this.cmDelete});
            this.zakazContext.Name = "zakazContext";
            this.zakazContext.ShowItemToolTips = false;
            this.zakazContext.Size = new System.Drawing.Size(225, 180);
            this.zakazContext.Opening += new System.ComponentModel.CancelEventHandler(this.zakazContext_Opening);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(221, 6);
            // 
            // np_tsmi
            // 
            this.np_tsmi.BackColor = System.Drawing.Color.Red;
            this.np_tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.print_ttn_zebra,
            this.print_ttn_Document,
            this.toolStripSeparator1,
            this.delete_ttn});
            this.np_tsmi.ForeColor = System.Drawing.Color.White;
            this.np_tsmi.Name = "np_tsmi";
            this.np_tsmi.Size = new System.Drawing.Size(224, 26);
            this.np_tsmi.Text = "Нова Пошта";
            this.np_tsmi.Visible = false;
            // 
            // print_ttn_zebra
            // 
            this.print_ttn_zebra.Name = "print_ttn_zebra";
            this.print_ttn_zebra.Size = new System.Drawing.Size(245, 26);
            this.print_ttn_zebra.Text = "Друкувати ТТН (zebra)";
            this.print_ttn_zebra.Click += new System.EventHandler(this.print_ttn_zebra_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(242, 6);
            // 
            // delete_ttn
            // 
            this.delete_ttn.ForeColor = System.Drawing.Color.Red;
            this.delete_ttn.Image = global::SPM_Worker.Properties.Resources.img_del;
            this.delete_ttn.Name = "delete_ttn";
            this.delete_ttn.Size = new System.Drawing.Size(245, 26);
            this.delete_ttn.Text = "Видалити ТТН";
            this.delete_ttn.Click += new System.EventHandler(this.delete_ttn_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(221, 6);
            // 
            // print_ttn_Document
            // 
            this.print_ttn_Document.Name = "print_ttn_Document";
            this.print_ttn_Document.Size = new System.Drawing.Size(245, 26);
            this.print_ttn_Document.Text = "Друкувати ЕН";
            this.print_ttn_Document.Click += new System.EventHandler(this.print_ttn_Document_Click);
            // 
            // ZakazInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Khaki;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.zakazContext;
            this.Controls.Add(this.cb_Index);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.Name = "ZakazInfo";
            this.Size = new System.Drawing.Size(650, 50);
            this.zakazContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox cb_Index;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cmWriteZakaz;
        private System.Windows.Forms.ToolStripMenuItem cmShablon;
        private System.Windows.Forms.ToolStripMenuItem cbShablonAll;
        private System.Windows.Forms.ToolStripMenuItem cbShablonTop;
        private System.Windows.Forms.ToolStripMenuItem cbShablonBottom;
        private System.Windows.Forms.ToolStripMenuItem cmPrint;
        private System.Windows.Forms.ToolStripMenuItem cmPrintFromWorker;
        private System.Windows.Forms.ToolStripMenuItem cmPrintShort;
        private System.Windows.Forms.ToolStripSeparator ts_npSeparator;
        private System.Windows.Forms.ToolStripMenuItem cmDelete;
        private System.Windows.Forms.ContextMenuStrip zakazContext;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem np_tsmi;
        private System.Windows.Forms.ToolStripMenuItem print_ttn_zebra;
        private System.Windows.Forms.ToolStripMenuItem delete_ttn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem print_ttn_Document;
    }
}
