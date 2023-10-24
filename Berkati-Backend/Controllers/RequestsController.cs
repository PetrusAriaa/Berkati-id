using Berkati_Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Berkati_Backend.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("requests")]
    public class RequestsController : ControllerBase
    {
        private readonly Requests requests;

        public RequestsController()
        {
            this.requests = new Requests();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Requests> _data = requests.GetRequests();
                var res = new
                {
                    data = _data,
                    length = _data.Count,
                    accessedAt = DateTime.UtcNow
                };
                return Ok(res);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
