using CapaLogica.interfaz;
using Models.Persona.Request;
using Models.Persona.Response;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CapaLogica.clase
{
    public class ValidaCampos : IValidaCampos
    {
        public ValidaCampos_Response ValidarCampos(NuevaPersona_Request request)
        {
            ValidaCampos_Response validaCampos;

            validaCampos = ValidarNombre(request);
            if (validaCampos.Success != true)
            {
                return validaCampos;
            }

            validaCampos = IsValidEmail(request);
            if (validaCampos.Success != true)
            {
                return validaCampos;
            }


            return validaCampos;
        }


        private ValidaCampos_Response ValidarNombre(NuevaPersona_Request request)
        {
            ValidaCampos_Response campos = new ValidaCampos_Response();
            if (request.Nombre.Count() > 50)
            {
                campos.Success = false;
                campos.Message = "El nombre no puede ser mayor a 50 caracteres";
            }
            else
            {
                campos.Success = true;
                campos.Message = "";
            }
            return campos;
        }
        private ValidaCampos_Response IsValidEmail(NuevaPersona_Request request)
        {
            ValidaCampos_Response campos = new ValidaCampos_Response();
            string email = request.Email;
            if (string.IsNullOrWhiteSpace(email))
            {
                campos.Success = false;
                campos.Message = "Ingrese Email";
                return campos;
            }
            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                campos.Success = false;
                campos.Message = e.Message;
            }
            catch (ArgumentException e)
            {
                campos.Success = false;
                campos.Message = e.Message;
            }

            try
            {
                bool regex = Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

                if (regex)
                {
                    campos.Success = true;
                    campos.Message = "";
                }
                else
                {
                    campos.Success = false;
                    campos.Message = "Correo no válido";
                }

            }
            catch (RegexMatchTimeoutException e)
            {
                campos.Success = false;
                campos.Message = e.Message;
                
            }

            return campos;
        }
    }
}
