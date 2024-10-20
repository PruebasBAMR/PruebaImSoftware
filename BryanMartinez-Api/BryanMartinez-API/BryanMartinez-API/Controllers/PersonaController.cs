using CapaLogica.interfaz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Persona.Request;
using Models.Persona.Response;

namespace BryanMartinez_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        IPersona _persona;
        IValidaCampos _validaCampos;

        public PersonaController(IPersona persona, IValidaCampos validaCampos)
        {
            _persona = persona;
            _validaCampos = validaCampos;
        }


        [HttpGet]
        [Route("GetPersonas")]
        public IActionResult CatalogoPeliculas()
        {
            var result = _persona.GetPersonas();
            if (result.Count() != 0)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    ok = true,
                    items = result.Count(),
                    results = result
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    ok = false,
                    items = result.Count(),
                    results = new
                    {
                        message = "Sin resultados"
                    }
                });
            }

        }


        [HttpPost]
        [Route("NuevaPersona")]
        public IActionResult NuevaPersona([FromBody] NuevaPersona_Request request)
        {
            ValidaCampos_Response response = _validaCampos.ValidarCampos(request);
            if (response.Success)
            {
                var result = _persona.NuevaPersona(request);
                if (result.Success == true)
                {
                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        ok = true,
                        results = new
                        {
                            message = result.Message
                        }
                    });
                }
                else
                {
                    return RespuestaError(result.Message);
                }
            }

            else
            {
                return RespuestaError(response.Message);
            }


        }

        private IActionResult RespuestaError(string? mensaje)
        {
            return StatusCode(StatusCodes.Status404NotFound, new
            {
                ok = false,
                results = new
                {
                    message = mensaje
                }
            });
        }
    }
}
