using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;
using Pendientes_Api.Models;

namespace Pendientes_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthPendientesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthPendientesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("token")]
        public IActionResult GetToken([FromBody] LoginModel model)
        {
            if (model.Username == "joel001" && model.Password == "produmar001") // Implementar lógica de validación real aquí
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, model.Username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }
            return Unauthorized();
        }
    }
}
