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
            this.Next_Btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Start_Btn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            this.Begin_Btn.Location = new System.Drawing.Point(611, 442);
            this.Begin_Btn.Name = "Begin_Btn";
            this.Begin_Btn.Size = new System.Drawing.Size(75, 23);
            this.Begin_Btn.TabIndex = 1;
            this.Begin_Btn.Text = "拖动";
            this.Begin_Btn.UseVisualStyleBackColor = true;
            this.Begin_Btn.Click += new System.EventHandler(this.Begin_Btn_Click);
            // 
            // Next_Btn
            // 
            this.Next_Btn.Location = new System.Drawing.Point(692, 441);
            this.Next_Btn.Name = "Next_Btn";
            this.Next_Btn.Size = new System.Drawing.Size(75, 23);
            this.Next_Btn.TabIndex = 4;
            this.Next_Btn.Text = "刷新";
            this.Next_Btn.UseVisualStyleBackColor = true;
            this.Next_Btn.Click += new System.EventHandler(this.Next_Btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(132, 443);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(184, 21);
            this.textBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 447);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "输入拖动验证码网址";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(322, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Start_Btn
            // 
            this.Start_Btn.Location = new System.Drawing.Point(423, 441);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(75, 23);
            this.Start_Btn.TabIndex = 8;
            this.Start_Btn.Text = "填充";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(518, 441);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "截图";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 476);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Start_Btn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Next_Btn);
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
        private System.Windows.Forms.Button Next_Btn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Start_Btn;
        private System.Windows.Forms.Button button2;

    }
}

