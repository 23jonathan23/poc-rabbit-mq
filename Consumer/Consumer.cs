using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace POC_rabbit.Consumer
{
    public static class Consumer
    {
        public static void GetMessageOnQueue()
        {
            //Connection create and open
            //name or ip machine where is rabbit server
            //Consumer create and received event with message sent
            //Content message is byte matrix, then will be to convert to string.
            //Puting consumer in channel for listening events queue,and too config queue and autoAck.
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "TransactionsPJAApp",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.Span;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(message);
                    };

                    channel.BasicConsume(
                        queue: "TransactionsPJAApp",
                        autoAck: true,
                        consumer: consumer
                    );

                    Console.WriteLine("Consumer Funcionando");
                    Console.ReadLine();
                }
            }
        }
    }
}
