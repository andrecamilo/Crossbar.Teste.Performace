namespace Crossbar.Teste.Infra.Utils.Requests.PubSub
{
    public class PubContent<TMensagem>
    {
        public string NomeFila { get; set; }

        public TMensagem Mensagem { get; set; }
    }
}
