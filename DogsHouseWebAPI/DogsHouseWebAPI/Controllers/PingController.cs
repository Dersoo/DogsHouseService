using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DogsHouseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        public PingController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Dogs house service. Version 1.0.1");
        }
    }
}
