using System;
using System.Diagnostics;
using System.Threading.Tasks;

using WampSharp.Core.Listener;
using WampSharp.V2;
using WampSharp.V2.Client;
using WampSharp.V2.Core.Contracts;
using WampSharp.V2.Realm;

using Crossbar.Teste.Infra.Utils.Results.RPC;
using Crossbar.Teste.Infra.Connection;

namespace Crossbar.Teste.Infra.RPC.Caller
{
    public class Caller<TRetorno, TParametro>
        : IDisposable
            where TRetorno : class
    {
        #region Variables
        
        private IWampChannel _channel;
        private CrossbarConnection _conn;

        #endregion

        #region Constructors

        public Caller(CrossbarConnection conn)
        {
            this._conn = conn;

            this._channel = this._conn.Instance<IWampChannel>();
        }

        #endregion

        #region Methods

        public async Task<DateTime> Run(CallerResult<TRetorno, TParametro> rpcPub)
        {
            RegisterOptions registerOptions = new RegisterOptions();
            var callback = new AbstractCallerCallback<TRetorno>(rpcPub.CallbackCaller);
            bool result = true;

            IWampClientConnectionMonitor monitor = this._channel.RealmProxy.Monitor;

            monitor.ConnectionBroken += OnClose;
            monitor.ConnectionError += OnError;

            registerOptions.Invoke = "roundrobin";
            
            IWampRealmProxy realm = this._channel.RealmProxy;

            var parameters = new object[] { rpcPub.Parametros };

            realm.RpcCatalog.Invoke(callback, new CallOptions(), rpcPub.NomeMetodo, parameters);

            return DateTime.Now;
        }

        private static void OnClose(object sender, WampSessionCloseEventArgs e)
        {
            Debug.WriteLine("connection closed. reason: " + e.Reason);
        }

        private static void OnError(object sender, WampConnectionErrorEventArgs e)
        {
            Debug.WriteLine("connection error. error: " + e.Exception);
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
