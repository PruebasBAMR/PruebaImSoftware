using CapaDatos;
using CapaLogica.interfaz;
using Models.Token;


namespace CapaLogica.clase
{

    public class TokenLogica : IToken
    {
        private readonly TokenDatos _datos = new TokenDatos();

        public bool ComparaToken (Token_Request request) 
        {
            int pinvalido = _datos.RetornaPinValida();
            if (request.PIN == pinvalido)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
