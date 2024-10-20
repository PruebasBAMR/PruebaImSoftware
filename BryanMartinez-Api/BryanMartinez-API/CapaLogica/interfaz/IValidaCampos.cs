using Models.Persona.Request;
using Models.Persona.Response;


namespace CapaLogica.interfaz
{
    public interface IValidaCampos
    {
        public ValidaCampos_Response ValidarCampos(NuevaPersona_Request response);
    }
}
