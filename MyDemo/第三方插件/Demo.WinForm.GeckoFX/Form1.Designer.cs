using System;
using System.IO;

namespace Demo.WinForm.GeckoFX
{
    partial class Form1
    {
       

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Gecko_Web = new Gecko.GeckoWebBrowser();
            this.Begin_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Gecko_Web
            // 
            this.Gecko_Web.Location = new System.Drawing.Point(2, 2);
            this.Gecko_Web.Name = "Gecko_Web";
            this.Gecko_Web.Size = new System.Drawing.Size(775, 433);
            this.Gecko_Web.TabIndex = 0;
            this.Gecko_Web.UseHttpActivityObserver = false;
            // 
            // Begin_Btn
            // 
            this.Begin_Btn.Location = new System.Drawing.Point(666, 441);
            this.Begin_Btn.Name = "Begin_Btn";
            this.Begin_Btn.Size = new System.Drawing.Size(75, 23);
            this.Begin_Btn.TabIndex = 1;
            this.Begin_Btn.Text = "button1";
            this.Begin_Btn.UseVisualStyleBackColor = true;
            this.Begin_Btn.Click += new System.EventHandler(this.Begin_Btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 476);
            this.Controls.Add(this.Begin_Btn);
            this.Controls.Add(this.Gecko_Web);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Gecko.GeckoWebBrowser Gecko_Web;
        private System.Windows.Forms.Button Begin_Btn;

    }
}

