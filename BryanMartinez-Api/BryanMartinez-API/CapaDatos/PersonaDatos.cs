
using Models.Persona.Request;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class PersonaDatos
    {
        private readonly SqlConnection con = new SqlConnection("data source=MSI\\MSI; initial catalog=ExamenImSoftware;uid=sa;pwd=bryan123;Trusted_Connection=False");

        public DataTable GetPersonas()
        {
            return ProcesaConsultas("SP_GetPersonas", false, []);
        }

        public string[] NuevaPersona(NuevaPersona_Request request)
        {
            //SP_NuevaPersona
            SqlParameter nombre = new SqlParameter("@Nombre", SqlDbType.NVarChar);
            nombre.Value = request.Nombre;
            SqlParameter edad = new SqlParameter("@Edad", SqlDbType.Int);
            edad.Value = request.Edad;
            SqlParameter email = new SqlParameter("@Email", SqlDbType.NVarChar);
            email.Value = request.Email;
            SqlParameter[] parametros = { nombre, edad, email };
            return ProcesaRegstro("SP_NuevaPersona", true, parametros);
        }

        private DataTable ProcesaConsultas(string SP, bool usaparametros, params SqlParameter[] parametros)
        {
            using (SqlCommand cmd = new SqlCommand(SP, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (usaparametros)
                {
                    cmd.Parameters.AddRange(parametros);
                }
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        con.Close();
                        return dt;
                    }
                }
            }
        }

        private string[] ProcesaRegstro(string SP, bool usaparametros, params SqlParameter[] parametros)
        {
            string[] respuesta = new string[2];
            using (SqlCommand cmd = new SqlCommand(SP, con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (usaparametros)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }
                    con.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        con.Close();
                        respuesta[0] = "true";
                        respuesta[1] = "Registrado correctamente";
                    }
                    else 
                    {
                        con.Close();
                        respuesta[0] = "false";
                        respuesta[1] = "No se pudo registrar";
                    }
                }
                catch (Exception ex)
                {
                    respuesta[0] = "false";
                    respuesta[1] = ex.Message.ToString();
                    
                }
            }
            return respuesta;
        }
    }
        
}
