using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.Unicode.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
        }
    }
}
