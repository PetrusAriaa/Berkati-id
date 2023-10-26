using Berkati_Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Berkati_Backend.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("partner")]
    public class PartnerController : Controller
    {
        private readonly Partner partners;
        public PartnerController()
        {
            this.partners = new Partner();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Partner> _data = partners.GetAllPartner();
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

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                Partner _data = partners.GetPartnerById(id);
                if ( _data == null )
                {
                    return NotFound($"Partner with ID {id} is not found.");
                }
                var res = new
                {
                    data = _data,
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
        public IActionResult Post([FromBody] Partner partner)
        {
            try
            {
                Guid Id = partners.AddPartner(partner);
                return Created(Id.ToString(), partner);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]Guid id, [FromBody]Partner partner)
        {
            try
            {
                partner.Id =id;
                partners.UpdatePartner(partner);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]Guid id)
        {
            try
            {
                partners.DeletePartner(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
