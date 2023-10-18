using Berkati_Backend.Models;
using Berkati_Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Berkati_Backend.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly AdminRepository adminRepos;
        public AdminController()
        {
            this.adminRepos = new AdminRepository();
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            List<Admin> _data = adminRepos.GetAllAdmin();
            var res = new
            {
                data = _data,
                length = _data.Count,
                accessedAt = DateTime.UtcNow
            };
            return Ok(res);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]Admin admin)
        {
            Guid Id = adminRepos.AddAdmin(admin);
            return Created(Id.ToString(), admin);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Admin admin)
        {
            adminRepos.UpdateAdmin(admin);
            return NoContent();
        }

        // API DELETE nya jangan lupa ditambahin yaa :D
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            string deleteString = $"Hello, DELETE ke-{id}!";
            return NoContent();
        }

    }
}
