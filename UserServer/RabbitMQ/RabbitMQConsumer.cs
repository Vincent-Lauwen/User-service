using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ
{
    public class RabbitMQConsumer : BackgroundService
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        // initialize the connection, channel and queue inside the constructor to persist them for until the service (or the application) runs
        public RabbitMQConsumer()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };
            
            _connection = _factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare("orders");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("start background service");
            // when the service is stopping, dispose these references to prevent leaks
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }

            // create a consumer that listens on the channel (queue)
            var consumer = new EventingBasicConsumer(_channel);

            // handle the Received event on the consumer this is triggered whenever a new message is added to the queue by the producer
            consumer.Received += (model, ea) =>
            {
                // read the message bytes
                var body = ea.Body.ToArray();
                var message = Encoding.Unicode.GetString(body);

                Console.WriteLine(" [x] Received {0}", message);

            };

            _channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
