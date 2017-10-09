using Crossbar.Teste.Infra.Utils.Delegates;

namespace Crossbar.Teste.Infra.Utils.Requests.RPC
{
    public class CalleeRequest<TRetorno, TParametro>
    {
        public string NomeMetodo { get; set; }

        public ExecutarCallee<TRetorno, TParametro> Run { get; set; }
    }
}
