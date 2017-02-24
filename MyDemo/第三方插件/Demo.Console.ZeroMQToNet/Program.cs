using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace Demo.Console.ZeroMQToNet
{
    class Program
    {
        static void Main(string[] args)
        {
           Ventilator.Start();
        }


        /// <summary>
        /// 请求响应模式
        /// </summary>
        public static void Request()
        {
            using (NetMQSocket clientSocket = new RequestSocket())
            {
                Random rd = new Random();
                int num = rd.Next(0, 100);
                clientSocket.Connect("tcp://127.0.0.1:5555");
                while (true)
                {
                    System.Console.WriteLine(num + ",Please enter your message:");
                    string message = System.Console.ReadLine();
                    clientSocket.SendFrame(num + ":" + message);

                    string answer = clientSocket.ReceiveFrameString();

                    System.Console.WriteLine("Answer from server:{0}", answer);

                    if (message == "exit")
                    {
                        break;
                    }
                }
            }
        }




        public delegate void GetDataHandler(string message);
        public static event GetDataHandler OnGetData;
        /// <summary>
        /// NetMQ 订阅端
        /// </summary>
        public static void Start()
        {
            var rng = new Random();
            int zipcode = rng.Next(0, 99);
            System.Console.WriteLine("接收本地天气预报{0}……", zipcode);
            OnGetData += new GetDataHandler(ProcessData);
            using (var subscriber = new SubscriberSocket())
            {
                subscriber.Connect("tcp://127.0.0.1:5557");
                //设置过滤字符串
               // subscriber.Subscribe(zipcode.ToString(CultureInfo.InvariantCulture));
                //订阅所有的发布端内容
                //subscriber.Subscribe("");
                subscriber.Subscribe("A");
                while (true)
                {
                    var results = subscriber.ReceiveMultipartStrings();
                    foreach (var result in results)
                    {
                        System.Console.WriteLine(result);
                    }
                    //System.Console.WriteLine(".");
                    //string[] split = results.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    //int zip = int.Parse(split[0]);
                    //if (zip == zipcode)
                    //{
                    //    OnGetData(results);
                    //}
                }
            }
        }
        private static void ProcessData(string message)
        {
            System.Console.WriteLine("天气情况：" + message);
        }
    }
}
