using Berkati_Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Berkati_Backend.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly User users;

        public UserController()
        {
            this.users = new User();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<User> _data = users.GetAllUser();
            var res = new
            {
                data = _data,
                length = _data.Count,
                accessedAt = DateTime.UtcNow
            };
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            Guid userId = users.AddUser(user);
            return Created(userId.ToString(), user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(User user)
        {
            users.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            users.DeleteUser(id);
            return NoContent();
        }
    }
}