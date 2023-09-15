using Berkati_Backend.Models;
using Berkati_Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Berkati_Backend.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("[controller]")]
    public class BerkatiController : ControllerBase
    {
        private readonly BerkatiRepository berkatiRepos;

        public BerkatiController()
        {
            this.berkatiRepos = new BerkatiRepository();
        }

        [HttpGet]
        //public IActionResult Get()
        //{
        //    string getString = "Hello, GET!";
        //    return Ok(getString);
        //}

        public List<User> Get()
        {
            return berkatiRepos.GetAllUser();
        }

        [HttpPost]
        public IActionResult Post()
        {
            string postString = "Hello, POST!";
            return Ok(postString);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            string putString = $"Hello, PUT ke-{id}!";
            return Ok(putString);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string deleteString = $"Hello, DELETE ke-{id}!";
            return Ok(deleteString);
        }
    }
}