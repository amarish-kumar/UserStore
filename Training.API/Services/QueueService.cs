using System.Text;
using RabbitMQ.Client;

namespace TrainingTake2.Services
{
    internal class QueueService : IQueueService
    {
        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //todo: move queueName to settings
                var queueName = "hello";

                channel.QueueDeclare(queueName,  
                    false,
                    false,
                    false,
                    null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("",
                    queueName,
                    null,
                    body);
            }
        }
    }
}