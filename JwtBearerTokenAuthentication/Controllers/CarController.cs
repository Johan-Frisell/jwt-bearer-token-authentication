using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JwtBearerTokenAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        [HttpGet("/Cars")]
        public IActionResult Index()
        {
            var cars = new List<string>() { "BMW", "OPEL", "KIA" };

            return Ok(JsonConvert.SerializeObject(cars));
        }
    }
}
