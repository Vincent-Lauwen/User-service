using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using UserServer.Models;
using UserServer.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepo _repository;
        private readonly IMessageProducer _messagePublisher;

        public UserController(IUserRepo repo, IMessageProducer messagePublisher)
        {
            _repository = repo;
            _messagePublisher = messagePublisher;
        }

        // GET: api/<UserController>
        [HttpPost("/register")]
        public ActionResult<IEnumerable<string>> Register([FromBody] UserRegister user)
        {
            try
            {
                if (user == null)
                    throw new Exception("User is null");

                var token = _repository.RegisterUser(user);

                if (token == null)
                    throw new Exception("Token is null");

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/login")]
        public ActionResult<IEnumerable<string>> Login([FromBody] UserLogin user)
        {
            try
            {
                if (user == null)
                    throw new Exception("User is null");

                var token = _repository.LoginUser(user);

                if (token == null)
                    throw new Exception("Token is null");

                return Ok(token);  
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
