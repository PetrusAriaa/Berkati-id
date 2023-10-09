using Berkati_Backend.Models;
using Berkati_Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Berkati_Backend.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("requests")]
    public class RequestsController : ControllerBase
    {
        private readonly RequestsRepository requestsRepos;

        public RequestsController()
        {
            this.requestsRepos = new RequestsRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Requests> _data = requestsRepos.GetRequests();
            var res = new
            {
                data = _data,
                length = _data.Count,
                accessedAt = DateTime.UtcNow
            };
            return Ok(res);
        }
    }
}
