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

        [HttpGet("{userId}")]
        public IActionResult GetByUserId([FromRoute]Guid userId)
        {
            try
            {
                List<Requests> _data = requests.GetRequestByUserId(userId);
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
        public IActionResult Post([FromBody] Requests _request)
        {
            try
            {
                Guid reqId = requests.AddRequest(_request);
                return Created(reqId.ToString(), _request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Requests _request)
        {
            try
            {
                _request.Id = id;
                requests.UpdateRequest(_request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("finish/{id}")]
        public IActionResult Put(Guid id)
        {
            try
            {
                requests.FinishRequest(id);
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
                requests.DeleteRequest(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
