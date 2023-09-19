using Berkati_Backend.Models;
using Berkati_Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Berkati_Backend.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepos;

        public UserController()
        {
            this.userRepos = new UserRepository();
        }

        [HttpGet]
        public List<User> Get()
        {
            return userRepos.GetAllUser();
        }

        [HttpPost]
        public void Post(User user)
        {
            userRepos.AddUser(user);
        }

        [HttpPut("{id}")]
        public void Put(User user)
        {
            userRepos.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            userRepos.DeleteUser(id);
        }
    }
}