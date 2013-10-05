using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace PubSubPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = "LocalHost";
            IConnection connection = connectionFactory.CreateConnection();
            IModel model = connection.CreateModel();
            
            //Declare exchange here
           
            
            IBasicProperties basicProperties = model.CreateBasicProperties();
            model.BasicPublish("", "TRINUG", basicProperties, System.Text.Encoding.UTF8.GetBytes("Hello TRINUG: "));
            Console.WriteLine("Message Sent");

            model.Close();
            connection.Close();
        }
    }
}
