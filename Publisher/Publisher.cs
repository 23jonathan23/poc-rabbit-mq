using System;
using System.Text;
using RabbitMQ.Client;

namespace POC_rabbit.Publisher
{
    public static class Publisher
    {
        public static void SendMessage()
        {
            //Connection create and open
            //name or ip machine where is rabbit server
            //Declare queue and create message in string for bytes
            //Create queue if will be not exist
            //Content message is byte matrix,for you encode anything.
            //Method for publisher message in queue
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

                    string message = "13634563234234;200000;JONATHAN RAPHAEL;VISA;CAPPTA;10213801913;2021-11-30;2021-11-30";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "TransactionsPJAApp",
                        basicProperties: null,
                        body: body
                    );

                    Console.WriteLine("Mensagem Enviada!");
                }
            }
        }
    }
}
