using MediatR;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitNews.Domain.Entities;
using RabbitNews.Domain.NewsCommands.Commands;
using RabbitNews.Infrastructure.RabbitMqSettings;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitNews.Infrastructure.Jobs.NewsReceiverService
{
    public class NewsReceiverService : BackgroundService
    {
        private IMediator _mediator;
        private readonly IRabbitMqConnectionSettings _rabbitMqConnectionSettings;

        public NewsReceiverService(IMediator mediator, IRabbitMqConnectionSettings rabbitMqConnectionSettings)
        {            
            _mediator = mediator;
            _rabbitMqConnectionSettings = rabbitMqConnectionSettings;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionFactory = new ConnectionFactory() 
            { 
                Endpoint = new AmqpTcpEndpoint(_rabbitMqConnectionSettings.Hostname, _rabbitMqConnectionSettings.Port), 
                UserName=_rabbitMqConnectionSettings.Username, 
                Password=_rabbitMqConnectionSettings.Password 
            };
            bool connectionAvailable = false;
            IConnection connection = null;

            do
            {
                try
                {
                    connection = connectionFactory.CreateConnection();
                    connectionAvailable = true;
                }
                catch (BrokerUnreachableException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Waiting for connection to consume data from the queue...");
                    await Task.Delay(5000); 
                }
            } while (!connectionAvailable);


            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "newsQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            if (stoppingToken.IsCancellationRequested)
            {
                channel.Dispose();
                connection.Dispose();
                return;
            }

            // Create a consumer that listens on the channel (queue)
            var consumer = new EventingBasicConsumer(channel);

            // handle the Received event on the consumer -> this is triggered whenever a new messages
            // is added to the queue by the producer
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var JsonMessage = Encoding.UTF8.GetString(body);
                var newsObj = JsonSerializer.Deserialize<News>(JsonMessage);
                Console.WriteLine("Received {0}", JsonMessage);

                var command = new NewsErstellenCommand(newsObj);
                await _mediator.Send(command, stoppingToken);
            };

            channel.BasicConsume(queue: "newsQueue", autoAck: true, consumer: consumer);
        }
    }
}
