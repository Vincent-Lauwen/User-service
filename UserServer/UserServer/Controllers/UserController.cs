using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet("/rabbit")]
        public void RabbitMQConsumerTest()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };

            using IConnection connection = factory.CreateConnection();
            IModel model = connection.CreateModel();

            model.ExchangeDeclare("KwetterTest", ExchangeType.Fanout, true, true, null);
            model.QueueDeclare("KwetterQ");
            model.QueueBind("KwetterTest", "KwetterQ", "#");

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.Unicode.GetString(body);
                Console.WriteLine(" [x] {0}", message);
            };
            model.BasicConsume("KwetterQ",
                                 autoAck: true,
                                 consumer: consumer);
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
