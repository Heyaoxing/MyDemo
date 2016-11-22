using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Demo.Word.JieBaWord;
using Shove.WordSplit;
using System.Net;
using System.IO;
using Demo.Infrastructure;
using System.Drawing;
using System.Drawing.Imaging;

namespace Demo.UnitTest.StringRepeat
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string alertMsg = "发布--产品s";
                MemoryStream mstream = new MemoryStream();

                Font strFont = new Font("Courier New", 14);
                Image newBitmap = new Bitmap(400, 100, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(newBitmap);
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 200, 200, 200)), new Rectangle(0, 0, 400, 100));
                g.DrawString("这个是demo", strFont, new SolidBrush(Color.Green), 10, 10);
                newBitmap.Save(mstream, ImageFormat.Jpeg);

                DrawHelper.ImageWaterMarkText(newBitmap, DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg", alertMsg, 1, 100, "Courier New",14);
                Console.WriteLine("成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();

        }


        //获取内网IP
        private static string GetInternalIP()
        {
            IPHostEntry host;
            string localIP = "000";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }



        /// <summary>
        /// 增加随机数
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string AddRandom(string txt)
        {
            try
            {
               

            }
            catch { }
            return txt;
        }

        /// <summary>
        /// 过滤数字
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string FilterNumber(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
                return txt;

            return System.Text.RegularExpressions.Regex.Replace(txt, @"\d", "");
        }


        /// <summary>
        /// 过滤英文
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string FilterEnglish(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
                return txt;

            return System.Text.RegularExpressions.Regex.Replace(txt, @"[a-zA-Z]", "");
        }


        /// <summary>
        /// 过滤标题中重复出现的关键字---需求647
        /// </summary>
        /// <param name="words"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        private static string FilterWordsByTitle(List<string> words, string title)
        {
            string _title = title;
            if (words == null || !words.Any())
                return _title;
            try
            {
                Shove.WordSplit.ShoveWordSplit SlpitWords = new ShoveWordSplit("ChineseDictionary.dat");
                List<string> splitWords = new List<string>();
                foreach (var word in words)
                {
                    splitWords.AddRange(SlpitWords.GetSlpitWords(word));
                    splitWords.RemoveAll(p => p.Length <= 1);
                }
                splitWords = splitWords.Distinct().ToList();

                foreach (var splitWord in splitWords)
                {
                     if (Regex.Matches(_title, splitWord).Count >1)
                    {
                        int index = _title.IndexOf(splitWord);
                        _title = _title.Replace(splitWord, "").Insert(index, splitWord);
                    }
                }
            }
            catch
            {
                _title = title;
            }
            return _title;
        }


        //private static string FilterWordsByTitle(List<string> words, string title)
        //{
        //    string _title = title;
        //    if (words == null || !words.Any())
        //        return _title;
        //    try
        //    {
        //        Shove.WordSplit.ShoveWordSplit SlpitWords = new ShoveWordSplit("ChineseDictionary.dat");
        //        List<string> splitWords = new List<string>();
        //        foreach (var word in words)
        //        {
        //            splitWords.AddRange(SlpitWords.GetSlpitWords(word));
        //            splitWords.RemoveAll(p => p.Length <= 1);
        //        }

        //        foreach (var splitWord in splitWords)
        //        {
        //            if (Regex.Matches(_title, splitWord).Count > 1) //匹配关键字在标题中出现2次的情况
        //            {
        //                int index = _title.IndexOf(splitWord);
        //                _title = _title.Replace(splitWord, "").Insert(index, splitWord);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        _title = title;
        //    }
        //    return _title;
        //}
    }
}
