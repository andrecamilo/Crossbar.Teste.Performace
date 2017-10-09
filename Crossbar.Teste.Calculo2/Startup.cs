using Crossbar.Teste.Infra.Connection;
using Crossbar.Teste.Infra.RPC.Callee;
using Crossbar.Teste.Infra.Utils.Requests.RPC;
using Crossbar.Teste.Calculo2.Domain;
using Crossbar.Teste.Calculo2.DTOs;

namespace Crossbar.Teste.Calculo2
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
            var calleeCalc2 = new Callee(this._conn);

            var response = calleeCalc2.Run(new CalleeRequest<CalculoCalc2Retorno, CalculoCalc2Parametro>
            {
                NomeMetodo = "br.com.Crossbar.Teste.Calculo1.calc",
                Run = new Calcular().Run
            });
        }
    }
}
