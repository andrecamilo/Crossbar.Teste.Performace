using Crossbar.Teste.Infra.Utils.Delegates;

namespace Crossbar.Teste.Infra.Utils.Results.PubSub
{
    public class SubContent<TMensagem>
    {
        public string NomeFila { get; set; }

        public GetMessage<TMensagem> MessageListener { get; set; }
    }
}
