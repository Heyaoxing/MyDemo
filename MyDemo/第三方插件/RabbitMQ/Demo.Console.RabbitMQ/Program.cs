using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Demo.Console.RabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Producer("hello word");
            //var factory = new ConnectionFactory();
            //factory.HostName = "localhost";
            //factory.UserName = "hyx";
            //factory.Password = "123456";

            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        channel.QueueDeclare("hello", false, false, false, null);

            //        var consumer = new QueueingBasicConsumer(channel);
            //        channel.BasicConsume("hello", true, consumer);
            //        while (true)
            //        {
            //            var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body);

            //        }
            //    }
            //}
            System.Console.Read();
        }

        public static void Producer(string value)
        {
            try
            {
                var qName = "lhtest1";
                var exchangeName = "fanoutchange3";
                var exchangeType = "topic";//topic、fanout
                var routingKey = "hello.*";
                var factory = new ConnectionFactory
                {
                    UserName = "hyx",
                    Password = "123456",
                    RequestedHeartbeat = 0,
                    HostName = "localhost"
                };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        //设置交换器的类型
                        channel.ExchangeDeclare(exchangeName, exchangeType);
                        //声明一个队列，设置队列是否持久化，排他性，与自动删除
                        channel.QueueDeclare(qName, true, false, false, null);
                        //绑定消息队列，交换器，routingkey
                        channel.QueueBind(qName, exchangeName, routingKey);
                        channel.QueueBind("hello.01", exchangeName, routingKey);
                        channel.QueueBind("hello.02", exchangeName, routingKey);
                        channel.QueueBind("hello.03.01", exchangeName, routingKey);
                        var properties = channel.CreateBasicProperties();
                        //队列持久化
                        properties.Persistent = true;
                        var body = Encoding.UTF8.GetBytes(value);
                        //发送信息
                        channel.BasicPublish(exchangeName, routingKey, properties, body);
                    }
                }

                Task.Factory.StartNew(SynPrdoctsTask);
            }
            catch (Exception ex)
            {
               System.Console.WriteLine(ex.Message);
            }
        }

        private static void SynPrdoctsTask()
        {
            try
            {
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.Message);
            }
        }
    }
}

