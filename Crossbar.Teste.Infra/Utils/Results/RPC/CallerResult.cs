using Crossbar.Teste.Infra.Utils.Delegates;

namespace Crossbar.Teste.Infra.Utils.Results.RPC
{
    public class CallerResult<TRetorno, TParametro>
    {
        public string NomeMetodo { get; set; }

        public TParametro Parametros { get; set; }

        public ResultCaller<TRetorno> CallbackCaller { get; set; }
    }
}
