using System;
using RabbitMQ.Client;

namespace OneWayProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = "LocalHost";
            IConnection connection = connectionFactory.CreateConnection();
            IModel model = connection.CreateModel();
            model.QueueDeclare("TRINUG", false, false, false, null);
            IBasicProperties basicProperties = model.CreateBasicProperties();
            model.BasicPublish("", "TRINUG", basicProperties, System.Text.Encoding.UTF8.GetBytes("Hello TRINUG: "));
            Console.WriteLine("Message Sent");

            model.Close();
            connection.Close();

        }
    }
}
