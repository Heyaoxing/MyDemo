namespace Demo.WinForm.GeckoFX
{
    partial class Form3
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
            this.Gecko_Web = new Gecko.GeckoWebBrowser();
            this.Begin_Btn = new System.Windows.Forms.Button();
            this.ProcessMessae_LisView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Stop_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Gecko_Web
            // 
            this.Gecko_Web.Location = new System.Drawing.Point(2, 1);
            this.Gecko_Web.Name = "Gecko_Web";
            this.Gecko_Web.Size = new System.Drawing.Size(910, 668);
            this.Gecko_Web.TabIndex = 2;
            this.Gecko_Web.UseHttpActivityObserver = false;
            // 
            // Begin_Btn
            // 
            this.Begin_Btn.Location = new System.Drawing.Point(309, 695);
            this.Begin_Btn.Name = "Begin_Btn";
            this.Begin_Btn.Size = new System.Drawing.Size(75, 23);
            this.Begin_Btn.TabIndex = 4;
            this.Begin_Btn.Text = "开始抢";
            this.Begin_Btn.UseVisualStyleBackColor = true;
            this.Begin_Btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // ProcessMessae_LisView
            // 
            this.ProcessMessae_LisView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.ProcessMessae_LisView.Location = new System.Drawing.Point(918, 1);
            this.ProcessMessae_LisView.Name = "ProcessMessae_LisView";
            this.ProcessMessae_LisView.Size = new System.Drawing.Size(205, 668);
            this.ProcessMessae_LisView.TabIndex = 5;
            this.ProcessMessae_LisView.UseCompatibleStateImageBehavior = false;
            this.ProcessMessae_LisView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "日志";
            this.columnHeader1.Width = 193;
            // 
            // Stop_Btn
            // 
            this.Stop_Btn.Location = new System.Drawing.Point(628, 695);
            this.Stop_Btn.Name = "Stop_Btn";
            this.Stop_Btn.Size = new System.Drawing.Size(75, 23);
            this.Stop_Btn.TabIndex = 6;
            this.Stop_Btn.Text = "停止";
            this.Stop_Btn.UseVisualStyleBackColor = true;
            this.Stop_Btn.Click += new System.EventHandler(this.Stop_Btn_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 730);
            this.Controls.Add(this.Stop_Btn);
            this.Controls.Add(this.ProcessMessae_LisView);
            this.Controls.Add(this.Begin_Btn);
            this.Controls.Add(this.Gecko_Web);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);

        }

        #endregion

        private Gecko.GeckoWebBrowser Gecko_Web;
        private System.Windows.Forms.Button Begin_Btn;
        private System.Windows.Forms.ListView ProcessMessae_LisView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button Stop_Btn;
    }
}