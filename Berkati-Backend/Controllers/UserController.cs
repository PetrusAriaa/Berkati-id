using Berkati_Backend.Models;
using Berkati_Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Berkati_Backend.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepos;

        public UserController()
        {
            this.userRepos = new UserRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<User> _data = userRepos.GetAllUser();
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
            Guid userId = userRepos.AddUser(user);
            return Created(userId.ToString(), user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(User user)
        {
            userRepos.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            userRepos.DeleteUser(id);
            return NoContent();
        }
    }
}