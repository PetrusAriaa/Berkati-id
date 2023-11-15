using Berkati_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data;

public class LoginBody
{
    public string Username { get; set; }
    public string Password { get; set; }
}

namespace Berkati_Backend.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly Admin admins;
        public AdminController()
        {
            this.admins = new Admin();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var headers = HttpContext.Request.Headers;
                if (headers.TryGetValue("Authorization", out var _authHeader))
                {
                    string authHeader= _authHeader;
                    string[] authStrings = authHeader.Split(' ');
                    string authToken = authStrings[1];
                    
                };
                List<Admin> _data = admins.GetAllAdmin();
                var res = new
                {
                    data = _data,
                    length = _data.Count,
                    accessedAt = DateTime.UtcNow
                };
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [Authorize(Roles = "SuperUser")]
        [HttpPost]
        public IActionResult Post([FromBody]Admin admin)
        {
            try
            {
                var claims = User.Claims.Select(c => new { c.Type, c.Value });
                Guid Id = admins.AddAdmin(admin);
                return Created(Id.ToString(), admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Admin admin)
        {
            try
            {
                admin.Id = id;
                admins.UpdateAdmin(admin);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "SuperUser")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                admins.DeleteAdmin(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginBody data)
        {
            try
            {
                if(string.IsNullOrEmpty(data.Username) || string.IsNullOrEmpty(data.Password))
                {
                    return BadRequest("Invalid login request.");
                }
                string token = admins.Login(data.Username, data.Password);
                var res = new
                {
                    token,
                    accessedAt = DateTime.UtcNow
                };
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
