#region

using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Training.DAL.Interfaces.Models;
using Training.DAL.Services;
using Training.Services;

#endregion

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
                var queueName = "hello";
                var context = new UserContext();
                var repository = new UserRepository(context);
                channel.QueueDeclare(queueName, false, false, false, null);
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (sender, basicDeliverEventArgs) =>
                {
                    string message;
                    var command = GetCommand(basicDeliverEventArgs, out message);
                    var user = GetUser(command);
                    await ProcessCommand(repository, command, user);
                    Console.WriteLine(" [x] Received {0}", message);
                };

                channel.BasicConsume(queueName, true, consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static async Task ProcessCommand(UserRepository repository, Command command, User user)
        {
            switch (command.Operation)
            {
                case Operation.Create:
                    user.CreatedDate = DateTime.Now;
                    await repository.AddAsync(user);
                    break;
                case Operation.Update:
                    var userToEdit = repository.FindOneBy(x => x.IdentityId.Equals(user.IdentityId));
                    user.Id = userToEdit.Id;
                    user.UpdatedDate = DateTime.Now;
                    await repository.UpdateAsync(us => us.IdentityId.Equals(user.IdentityId), user);
                    break;
            }
        }

        private static User GetUser(Command command)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Command, User>(); });
            var mapper = config.CreateMapper();
            var user = mapper.Map<Command, User>(command);
            return user;
        }

        private static Command GetCommand(BasicDeliverEventArgs ea, out string message)
        {
            var body = ea.Body;
            message = Encoding.UTF8.GetString(body);
            return JsonConvert.DeserializeObject<Command>(message);
        }
    }
}