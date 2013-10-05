using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace OneWayConsumer
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

            QueueingBasicConsumer consumer = new QueueingBasicConsumer(model);
            String consumerTag = model.BasicConsume("TRINUG", false, consumer);
            while (true)
            {
                try
                {
                    var e = (RabbitMQ.Client.Events.BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    IBasicProperties props = e.BasicProperties;
                    byte[] body = e.Body;
                    // ... process the message
                    Console.WriteLine(System.Text.Encoding.UTF8.GetString(body));

                    model.BasicAck(e.DeliveryTag, false);
                }
                catch (OperationInterruptedException ex)
                {
                    // The consumer was removed, either through
                    // channel or connection closure, or through the
                    // action of IModel.BasicCancel().
                    break;
                }
            }
        }
    }
}
