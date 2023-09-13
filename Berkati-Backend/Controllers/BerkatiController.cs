using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Berkati_Backend.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("[controller]")]
    public class BerkatiController : ControllerBase
    {
    //    private static readonly string[] Summaries = new[]
    //    {
    //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

    //    private readonly ILogger<BerkatiController> _logger;

    //    public BerkatiController(ILogger<BerkatiController> logger)
    //    {
    //        _logger = logger;
    //    }

        [HttpGet]
        public IActionResult Get()
        {
            string getString = "Hello, GET!";
            return Ok(getString);
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