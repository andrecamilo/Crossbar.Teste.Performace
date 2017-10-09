using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Crossbar.Teste.Infra.Utils.Delegates;
using WampSharp.Core.Serialization;
using WampSharp.V2.Core.Contracts;
using WampSharp.V2.Rpc;

namespace Crossbar.Teste.Infra.RPC.Callee
{
    public class AbstractCalleeOperation<TRetorno, TParametro> : SyncLocalRpcOperation
    {
        #region Variables

        private readonly RpcParameter[] mParameters = new RpcParameter[0];
        private ExecutarCallee<TRetorno, TParametro> _execute;

        #endregion Variables

        #region Constructors

        public AbstractCalleeOperation(ExecutarCallee<TRetorno, TParametro> execute, string procedureURI)
            : base(procedureURI)
        {
            this._execute = execute;
        }

        #endregion Constructors

        #region Attributes

        public override CollectionResultTreatment CollectionResultTreatment { get { return CollectionResultTreatment.Multivalued; } }

        public override bool HasResult { get { return true; } }

        public override RpcParameter[] Parameters { get { return mParameters; } }

        #endregion Attributes

        #region Methods

        protected override object InvokeSync<TMessage>(IWampRawRpcOperationRouterCallback caller,
                                                        IWampFormatter<TMessage> formatter,
                                                        InvocationDetails details,
                                                        TMessage[] arguments,
                                                        IDictionary<string, TMessage> argumentsKeywords,
                                                        out IDictionary<string, object> outputs)
        {
            outputs = null;

            if (arguments != null)
            {
                List<TParametro> returns = new List<TParametro>();

                switch (details.Procedure)
                {
                    case "{AUTHENTICATE}": return Authenticate(arguments);  //Substituir {AUTHENTICATE} pela nome da fila de Autenticação do Server Crossbar.Io (se houver)
                    case "{AUTHORIZE}": return Authorize(arguments);        //Substituir {AUTHORIZE} pela nome da fila de Autorização do Server Crossbar.Io (se houver)
                    default: return DefaultMethod(arguments, returns);
                }
            }

            return new List<string>();
        }

        private object DefaultMethod<TMessage>(TMessage[] arguments, List<TParametro> returns)
        {
            arguments.ToList().ForEach(i =>
            {
                var vl = JsonConvert.DeserializeObject<TParametro>(i.ToString());
                returns.Add(vl);
            });

            return this._execute(returns[0]);
        }

        private object Authorize<TMessage>(TMessage[] arguments)
        {
            try
            {
                dynamic AuthorizeCredencials = new
                {
                    sessionId = ((dynamic)arguments[0]).session.ToString(),
                    uri = arguments[1].ToString(),
                    action = arguments[2].ToString(),
                };
                var vl = JsonConvert.DeserializeObject<TParametro>(JsonConvert.SerializeObject(AuthorizeCredencials));
                return this._execute(vl);
            }
            catch { return null; }
        }

        private object Authenticate<TMessage>(TMessage[] arguments)
        {
            try
            {
                dynamic AuthenticateCredencials = new
                {
                    sessionId = ((dynamic)arguments[2]).session.ToString(),
                    login = arguments[1].ToString(),
                };
                var vl = JsonConvert.DeserializeObject<TParametro>(JsonConvert.SerializeObject(AuthenticateCredencials));
                return this._execute(vl);
            }
            catch { return null; }
        }

        #endregion Methods
    }
}
