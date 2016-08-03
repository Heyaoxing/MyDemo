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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Next_Btn = new System.Windows.Forms.Button();
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
            this.Begin_Btn.Location = new System.Drawing.Point(508, 442);
            this.Begin_Btn.Name = "Begin_Btn";
            this.Begin_Btn.Size = new System.Drawing.Size(75, 23);
            this.Begin_Btn.TabIndex = 1;
            this.Begin_Btn.Text = "执行";
            this.Begin_Btn.UseVisualStyleBackColor = true;
            this.Begin_Btn.Click += new System.EventHandler(this.Begin_Btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(44, 442);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(250, 442);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 3;
            // 
            // Next_Btn
            // 
            this.Next_Btn.Location = new System.Drawing.Point(655, 442);
            this.Next_Btn.Name = "Next_Btn";
            this.Next_Btn.Size = new System.Drawing.Size(75, 23);
            this.Next_Btn.TabIndex = 4;
            this.Next_Btn.Text = "下一步";
            this.Next_Btn.UseVisualStyleBackColor = true;
            this.Next_Btn.Click += new System.EventHandler(this.Next_Btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 476);
            this.Controls.Add(this.Next_Btn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Begin_Btn);
            this.Controls.Add(this.Gecko_Web);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gecko.GeckoWebBrowser Gecko_Web;
        private System.Windows.Forms.Button Begin_Btn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Next_Btn;

    }
}

