using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtBearerTokenAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        [HttpGet("/Token")]
        public IActionResult GenerateToken()
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var subject = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "ClaimTypes.NameIdentifier"),
                new Claim(ClaimTypes.Name, "ClaimTypes.Name"),
                new Claim(ClaimTypes.Email, "ClaimTypes.Email"),
                new Claim(ClaimTypes.MobilePhone, "ClaimTypes.MobilePhone"),
            };

            var claims = new Dictionary<string, object>();
            claims.Add("Firstname", "Firstname_test");
            claims.Add("Lastname", "Lastname_test")
;
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Subject = new ClaimsIdentity(subject),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = "localhost",
                Audience = "user"
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(tokenHandler.WriteToken(token));
        }
    }
}
