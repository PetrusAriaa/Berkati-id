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
        public List<Admin> Get()
        {
            return adminRepos.GetAllAdmin();
        }
        
        [HttpPost]
        public void Post([FromBody] Admin admin)
        {
            adminRepos.AddAdmin(admin);
        }

        [HttpPut("{id}")]
        public void Put(Admin admin)
        {
            adminRepos.UpdateAdmin(admin);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string deleteString = $"Hello, DELETE ke-{id}!";
            return Ok(deleteString);
        }

    }
}
