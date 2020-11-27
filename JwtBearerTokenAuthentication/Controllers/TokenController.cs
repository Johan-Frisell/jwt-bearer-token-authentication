using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        [HttpGet("/Token")]
        public IActionResult GenerateToken()
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A970556E-583C-4833-89D7-FD2D74ABE8E4"));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

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
                SigningCredentials = signingCredentials,
                Issuer = "localhost",
                Audience = "user"
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return Ok(jwtSecurityTokenHandler.WriteToken(securityToken));
        }
    }
}
