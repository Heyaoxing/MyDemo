using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace Demo.ZeroMQToNet.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Sink.Start();
        }


        /// <summary>
        /// 响应
        /// </summary>
        public static void Response()
        {
            using (NetMQSocket serverSocket = new ResponseSocket())
            {
                serverSocket.Bind("tcp://127.0.0.1:5555");
                while (true)
                {
                    string message1 = serverSocket.ReceiveFrameString();

                    Console.WriteLine("Receive message :\r\n{0}\r\n", message1);

                    string[] msg = message1.Split(':');
                    string message = msg[1];


                    #region 根据接收到的消息，返回不同的信息
                    if (message == "Hello")
                    {
                        serverSocket.SendFrame("World");
                    }
                    else if (message == "ni hao ")
                    {
                        serverSocket.SendFrame("你好！");
                    }
                    else if (message == "hi")
                    {
                        serverSocket.SendFrame("HI");
                    }
                    else
                    {
                        serverSocket.SendFrame(message);
                    }
                    #endregion

                    if (message == "exit")
                    {
                        break;
                    }
                }
            }
        }


        static readonly ManualResetEvent _terminateEvent = new ManualResetEvent(false);
        /// <summary>
        /// NetMQ 发布端
        /// </summary>
        public static void Start()
        {
            string[] weathers = new string[6] { "晴朗", "多云", "阴天", "霾", "雨", "雪" };

            System.Console.WriteLine("发布多个地区天气预报：");


            using (var publisher = new PublisherSocket())
            {
                publisher.Options.IPv4Only = false;
                publisher.Connect("tcp://127.0.0.1:5557");

                var rng = new Random();
                string msg;
                int sleeptime = 1000;//1秒

                //指定发布的时间间隔，1秒
                while (_terminateEvent.WaitOne(1000) == false)
                {
                    //随机生成天气数据
                    int zipcode = rng.Next(0, 99);
                    int temperature = rng.Next(-50, 50);
                    int weatherId = rng.Next(0, 5);

                    msg = string.Format("{0} {1} {2}", zipcode, temperature, weathers[weatherId]);
                    publisher.SendMoreFrame("A").SendFrame(msg);

                    System.Console.WriteLine(msg);
                    Thread.Sleep(sleeptime);
                }
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            System.Console.WriteLine("exit……");
            _terminateEvent.Set();
        }
    }
}
