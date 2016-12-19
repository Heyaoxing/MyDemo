using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Demo.Word.JieBaWord;
using Shove.WordSplit;
using System.Net;
using System.IO;
using Demo.Class.Elasticsearchs;
using Demo.Class.Log4Net;
using Demo.ORM.MySqlSugar;
using log4net;
using log4net.Config;

namespace Demo.UnitTest.StringRepeat
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                List<t_synth_title_list> list = new List<t_synth_title_list>();
                list.Add(new t_synth_title_list()
                {
                    id = 1,
                    content = "hello"
                });
                list.Add(new t_synth_title_list()
                {
                    id = 2,
                    content = "word"
                });

                foreach (var item in list)
                {
                    if (item.id == 1)
                        list.Remove(item);

                    item.id = 21;
                    item.content = "das";
                }

                foreach (var tSynthTitleList in list)
                {
                    Console.WriteLine(tSynthTitleList.id);
                    Console.WriteLine(tSynthTitleList.content);
                }

                //ElasticClientBase elasticClient = new CustomerService();
                //var _elasticClient = elasticClient.GetClient("articles");
                //using (var db = SugarDao.GetInstance())
                //{
                //    int pageIndex = 1;
                //    int pageSize =20;
                //    int count = db.GetInt("select count(1) from t_synth_title_list");
                //    Console.WriteLine("总共需要插入到es库有:" + count);
                //    Stopwatch watch = new Stopwatch();
                //    while (count > (pageIndex * pageSize))
                //    {
                //        watch.Start();
                //        var list = db.SqlQuery<t_synth_title_list>("select id,content from t_synth_title_list").Skip((pageIndex - 1) * pageSize).Take(pageSize);
                //        if (list != null && list.Any())
                //        {
                //            foreach (var item in list)
                //            {
                //                _elasticClient.Index(item);
                //            }
                //        }
                //        watch.Stop();
                //        Console.WriteLine(string.Format("完成插入到es库共:{0},用时:{1}", list.Count(), watch.Elapsed.Seconds));
                //        watch.Reset();
                //        pageIndex++;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();

        }

        private static void write()
        {
            InitLog4Net();
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(GetSrc(new StackTrace()) + "消息");
            logger.Warn(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + "警告");
            logger.Error("异常");
            logger.Fatal("错误");
            logger.Info(logger.IsDebugEnabled);
            logger.Info(logger.IsErrorEnabled);
            logger.Info(logger.IsFatalEnabled);
            logger.Info(logger.IsInfoEnabled);
            logger.Info(logger.IsInfoEnabled);
        }

        private const string SRC = " {0}.{1}: ";

        private static string GetSrc(StackTrace trace)
        {
            if (trace != null)
            {
                var methodBase = trace.GetFrame(1).GetMethod();
                if (methodBase != null)
                {
                    string src = string.Format(SRC, methodBase.DeclaringType.FullName, methodBase.Name);
                    return src;
                }
            }
            return null;
        }

        private static void InitLog4Net()
        {
            //  var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            //    XmlConfigurator.ConfigureAndWatch(logCfg);
            XmlConfigurator.Configure();
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
                    if (Regex.Matches(_title, splitWord).Count > 1)
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
