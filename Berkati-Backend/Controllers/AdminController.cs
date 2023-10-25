using Berkati_Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
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
        
        [HttpPost]
        public IActionResult Post([FromBody]Admin admin)
        {
            try
            {
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
        public IActionResult Login([FromBody] JsonElement requestBody)
        {
            try
            {
                if (requestBody.TryGetProperty("username", out var usernameProperty) && requestBody.TryGetProperty("password", out var passwordProperty))
                {
                    string username = usernameProperty.GetString();
                    string password = passwordProperty.GetString();

                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        return BadRequest("Invalid login request.");
                    }

                    bool isLogin = admins.Login(username, password);
                    var res = new
                    {
                        data = isLogin,
                        accessedAt = DateTime.UtcNow
                    };
                    return Ok(res);
                }
                else
                {
                    return BadRequest("Invalid login request format.");
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
