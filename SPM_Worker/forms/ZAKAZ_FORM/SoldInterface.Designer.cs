
namespace SPM_Worker
{
    partial class SoldInterface
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
            this.container = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.AutoScroll = true;
            this.container.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.container.BackColor = System.Drawing.Color.White;
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.container.Location = new System.Drawing.Point(0, 0);
            this.container.Margin = new System.Windows.Forms.Padding(0);
            this.container.Name = "container";
            this.container.Padding = new System.Windows.Forms.Padding(5);
            this.container.Size = new System.Drawing.Size(569, 207);
            this.container.TabIndex = 0;
            this.container.WrapContents = false;
            // 
            // SoldInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.container);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SoldInterface";
            this.Size = new System.Drawing.Size(569, 207);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel container;
    }
}
