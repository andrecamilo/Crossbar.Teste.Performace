using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using WampSharp.Core.Serialization;
using WampSharp.V2.Core.Contracts;
using WampSharp.V2.Rpc;

using Crossbar.Teste.Infra.Utils.Delegates;

namespace Crossbar.Teste.Infra.RPC.Caller
{
    public class AbstractCallerCallback<TRetorno> 
        : IWampRawRpcOperationClientCallback 
            where TRetorno : class
    {
        #region Variables

        private ResultCaller<TRetorno> _resultRPCCaller;

        #endregion

        #region Constructors

        public AbstractCallerCallback(ResultCaller<TRetorno> resultRPCCaller)
        {
            this._resultRPCCaller = resultRPCCaller;
        }

        #endregion

        #region Methods

        public void Result<TMessage>(IWampFormatter<TMessage> formatter, ResultDetails details)
        {
            throw new NotImplementedException();
        }

        public void Result<TMessage>(IWampFormatter<TMessage> formatter, ResultDetails details, TMessage[] arguments)
        {
            List<TRetorno> _resultList = new List<TRetorno>();

            arguments.ToList().ForEach(i =>
            {
                try
                {
                    var vl = JsonConvert.DeserializeObject<TRetorno>(i.ToString());

                    _resultList.Add(vl);
                }
                catch (InvalidCastException)
                {
                    //TODO: Log de Erro!
                }
            });

            this._resultRPCCaller(_resultList[0]);
        }

        public void Result<TMessage>(IWampFormatter<TMessage> formatter,
                                     ResultDetails details,
                                     TMessage[] arguments,
                                     IDictionary<string, TMessage> argumentsKeywords)
        {
            List<TRetorno> _resultList = new List<TRetorno>();

            int c = formatter.Deserialize<int>(argumentsKeywords["c"]);
            int ci = formatter.Deserialize<int>(argumentsKeywords["ci"]);

            Console.WriteLine("Got result: " + new { c, ci });

            _resultList.Clear();

            arguments.ToList().ForEach(i =>
            {
                try
                {
                    _resultList.Add(i as TRetorno);
                }
                catch (InvalidCastException)
                {
                    //TODO: Log de Erro!
                }
            });
        }

        public void Error<TMessage>(IWampFormatter<TMessage> formatter, TMessage details, string error)
        {
            throw new NotImplementedException();
        }

        public void Error<TMessage>(IWampFormatter<TMessage> formatter, TMessage details, string error, TMessage[] arguments)
        {
            throw new NotImplementedException();
        }

        public void Error<TMessage>(IWampFormatter<TMessage> formatter, TMessage details, string error, TMessage[] arguments,
                                    TMessage argumentsKeywords)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
