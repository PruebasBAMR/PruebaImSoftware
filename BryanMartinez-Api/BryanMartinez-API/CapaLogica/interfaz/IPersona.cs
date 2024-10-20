using Models.Persona.Request;
using Models.Persona.Response;


namespace CapaLogica.interfaz
{
    public interface IPersona
    {
        List<Persona_Response> GetPersonas();
        NuevaPersona_Response NuevaPersona(NuevaPersona_Request request);
    }
}
