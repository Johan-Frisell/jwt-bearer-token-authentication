using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JwtBearerTokenAuthentication.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        [HttpGet("/CarsRequireToken")]
        public IActionResult GetCarsForToken()
        {
            var cars = new List<string>() { "SCODA", "VW", "KIA" };

            return Ok(JsonConvert.SerializeObject(cars));
        }

        [HttpGet("/CarsRequireTokenWithRole")]
        [Authorize(Policy = "RolePolicy")]
        public IActionResult GetCarsForRole()
        {
            var cars = new List<string>() { "BMW", "PORCHE" };

            return Ok(JsonConvert.SerializeObject(cars));
        }

        [HttpGet("/CarsRequireTokenWithSpecialClaim")]
        [Authorize(Policy = "SpecialClaimPolicy")]
        public IActionResult GetCarsForSpecialClaim()
        {
            var cars = new List<string>() { "LAMBORGHINI" };

            return Ok(JsonConvert.SerializeObject(cars));
        }
    }
}
