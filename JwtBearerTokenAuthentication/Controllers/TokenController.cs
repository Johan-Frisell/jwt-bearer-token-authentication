using Microsoft.AspNetCore.Mvc;

namespace JwtBearerTokenAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        [HttpGet("/Token")]
        public IActionResult GenerateToken()
        {
            return Ok("TOKEN");
        }
    }
}
