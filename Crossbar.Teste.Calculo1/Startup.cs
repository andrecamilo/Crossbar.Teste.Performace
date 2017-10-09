using Crossbar.Teste.Infra.Connection;
using Crossbar.Teste.Infra.RPC.Callee;
using Crossbar.Teste.Infra.Utils.Requests.RPC;
using Crossbar.Teste.Calculo1.Domain;
using Crossbar.Teste.Calculo1.DTOs;

namespace Crossbar.Teste.Calculo1
{
    public class Startup
    {
        private CrossbarConnection _conn;

        public Startup()
        {
            this._conn = new CrossbarConnection();

            this._conn.Initilize(new ConnectionParameters
            {
                ServerAddress = "192.168.228.55",
                Port = 8080,
                ServerRealm = "realm1"
            });

            var con = this._conn.Connect();

            var res = con.Result;
        }

        public void Configure()
        {
            var calleeCalc1 = new Callee(this._conn);

            var response = calleeCalc1.Run(new CalleeRequest<CalculoCalc1Retorno, CalculoCalc1Parametro>
            {
                NomeMetodo = "br.com.Crossbar.Teste.Calculo1.calc",
                Run = new Calcular().Run
            });
        }
    }
}
