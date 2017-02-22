using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Training.DAL.Interfaces.Models;
using Training.DAL.Services;
using Training.Services;

namespace Training.UserStore
{
    internal class Receiver
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //todo: move queueName to settings
                var queueName = "hello";
                var context = new UserContext();
                var repository = new UserRepository(context);
                channel.QueueDeclare(queueName, false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    //todo: handle different commands
                    var command = JsonConvert.DeserializeObject<CreateUserCommand>(message);
                    //var config = new MapperConfiguration(cfg => { cfg.CreateMap<CreateUserCommand, User>(); });
                    //var mapper = config.CreateMapper();
                    //var command = mapper.Map<UserModel, CreateUserCommand>(user);

                    var user = new User
                    {
                        FirstName = command.FirstName,
                        Surname = command.Surname,
                        DoB = command.DoB,
                        Email = command.Email,
                        CreatedDate = DateTime.Now
                    };

                    await repository.AddAsync(user);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queueName, true, consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}