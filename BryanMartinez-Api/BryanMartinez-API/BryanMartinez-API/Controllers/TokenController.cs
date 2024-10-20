using CapaLogica.interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BryanMartinez_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        IToken _token;
        

        private readonly string secretKey;
        private readonly string audience;
        private readonly string issuer;

        public TokenController(IToken token, IConfiguration config)
        {
            _token = token;

            secretKey = config.GetSection("settings")
                              .GetSection("secretKey")
                              .ToString()!;
            audience = config.GetSection("Jwt")
                              .GetSection("Audience")
                              .ToString()!;
            issuer = config.GetSection("Jwt")
                              .GetSection("Issuer")
                              .ToString()!;
        }

        [HttpPost]
        [Route("GeneraToken")]
        public IActionResult LoginUs([FromBody] Token_Request request)
        {
            bool response = _token.ComparaToken(request);
            if (response != false)
            {

                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "Usuario")
                };
                

                var token = new JwtSecurityToken(
                    issuer, audience, claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials
                    );

                string tokencreado = new JwtSecurityTokenHandler().WriteToken(token);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    Ok = true,
                    results = new
                    {
                        message = "Token generado correctamente"
                    },
                    token = tokencreado
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    ok = false,
                    results = new
                    {
                        message = "Pin no valido"
                    }
                });
            }

        }
    }
}
