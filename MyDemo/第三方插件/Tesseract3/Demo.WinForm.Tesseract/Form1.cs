using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tesseract;

namespace Demo.WinForm.Tesseract
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string testImagePath = @"C:\Users\pc\Desktop\Images\3.png";
        private void Start_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var engine = new TesseractEngine("tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(testImagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            result_label.Text = page.GetText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result_label.Text = ex.Message;
            }
            Console.Read();
        }

        private void Export_Btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                testImagePath = openFileDialog.FileName;
                if (string.IsNullOrWhiteSpace(testImagePath))
                    return;


            }
        }
    }
}
