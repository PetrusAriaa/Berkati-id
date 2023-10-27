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
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // BUAT TAMPILAN LIST BERBAGI PAKAI INI YA :D
        [HttpGet("requests")]
        public IActionResult GetUserRequest()
        {
            try
            {
                List<User> listUser = users.GetAllUser();
                var _data = listUser
                    .SelectMany(user => user.Requests
                        .Select(request =>
                            new
                            {
                                userId = user.Id,
                                nama = user.Nama,
                                telp = user.Telp,
                                requestId = request.Id,
                                tanggal = request.Tanggal,
                                jalan = request.Jalan,
                                rt = request.Rt,
                                rw = request.Rw,
                                kel = request.Kel,
                                kec = request.Kec,
                                kab_kota = request.Kab_kota,
                                provinsi = request.Provinsi,
                                kodepos = request.Kodepos,
                                est_jumlah = request.Est_jumlah,
                                status = request.Status
                            }
                        )
                    ).ToList();

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
        public IActionResult Post([FromBody]User user)
        {
            try
            {
                Guid userId = users.AddUser(user);
                return Created(userId.ToString(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(User user)
        {
            try
            {
                users.UpdateUser(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }  
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                users.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}