using CapaDatos;
using CapaLogica.interfaz;
using Models.Persona.Request;
using Models.Persona.Response;
using System.Data;

namespace CapaLogica.clase
{
    public class PersonaLogica : IPersona
    {
        PersonaDatos _datos = new PersonaDatos();
        
        
        public List<Persona_Response> GetPersonas()
        {
            DataTable Resultado = _datos.GetPersonas();
            List<Persona_Response> listaPersonas = new List<Persona_Response>();

            if (Resultado.Rows.Count > 0)
            {
                foreach (DataRow item in Resultado.Rows)
                {
                    Persona_Response respuestaOk = new Persona_Response
                    {
                        idpersona = (int)item["idpersona"],
                        Nombre = item["Nombre"].ToString(),
                        Edad = (int)item["Edad"],
                        Email = item["Email"].ToString(),
                    };
                    listaPersonas.Add(respuestaOk);
                }

            }

            return listaPersonas;
        }

        public NuevaPersona_Response NuevaPersona(NuevaPersona_Request request)
        {
            string[] Resultado = _datos.NuevaPersona(request);
            
            NuevaPersona_Response response = new NuevaPersona_Response()
            {
                Success = bool.Parse(Resultado[0]),
                Message = Resultado[1]
            };
            return response;
        }
    }
}
