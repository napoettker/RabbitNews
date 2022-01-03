using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RabbitNews.Domain.Entities;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewsGenerator
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var connectionfactory = new ConnectionFactory() { Endpoint = new AmqpTcpEndpoint("rabbitmq", 5672), UserName = "guest", Password = "guest" };
            bool connectionAvailable = false;
            IConnection connection = null;

            do
            {
                try
                {
                    connection = connectionfactory.CreateConnection();
                    connectionAvailable = true;
                }
                catch (BrokerUnreachableException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Waiting for connection to publish data to the queue...");
                    await Task.Delay(5000);
                }
            } while (!connectionAvailable);

            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "newsQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var iter = 0;

            while (true)
            {
                News message = GetNextNewsObject(iter);
                var jsonMessage = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);
                channel.BasicPublish(exchange: "", routingKey: "newsQueue", basicProperties: null, body: body);
                Console.WriteLine("Sent: " + jsonMessage);
                iter++;
                await Task.Delay(5000);
            }
        }

        public static News GetNextNewsObject(int iter)
        {
            return new News()
            {
                Id = $"{Guid.NewGuid()}_v{iter}",
                Title = $"Title_v{iter}",
                Message = $"Message_v{iter}",
                Date = DateTime.Now.ToUniversalTime().ToString(),
            };
        }
    }
}
