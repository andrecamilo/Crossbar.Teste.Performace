using System;
using System.Diagnostics;
using System.Threading.Tasks;

using SystemEx;
using WampSharp.Core.Listener;
using WampSharp.V2;
using WampSharp.V2.Client;
using WampSharp.V2.Core.Contracts;
using WampSharp.V2.Realm;

using Crossbar.Teste.Infra.Utils.Requests.RPC;
using Crossbar.Teste.Infra.Connection;

namespace Crossbar.Teste.Infra.RPC.Callee
{
    public class Callee
        : IDisposable
    {
        #region Variables
        
        private IWampChannel _channel;
        private CrossbarConnection _conn;

        #endregion

        #region Constructors

        public Callee(CrossbarConnection conn)
        {
            this._conn = conn;
            this._channel = this._conn.Instance<IWampChannel>();
        }

        #endregion

        #region Methods

        public async Task<bool> Run<TRetorno, TParametro>(CalleeRequest<TRetorno, TParametro> calleeRequest) where TParametro : class
        {
            try
            {
                RegisterOptions registerOptions = new RegisterOptions();
                bool result = false;

                IWampClientConnectionMonitor monitor = this._channel.RealmProxy.Monitor;

                monitor.ConnectionEstablished += OnConnectionEstablished;
                monitor.ConnectionBroken += OnClose;
                monitor.ConnectionError += OnError;

                registerOptions.Invoke = "roundrobin";
                
                var operation = new AbstractCalleeOperation<TRetorno, TParametro>(calleeRequest.Run, calleeRequest.NomeMetodo);

                IWampRealmProxy realm = this._channel.RealmProxy;

                Task<IAsyncDisposable> registrationTask = realm.RpcCatalog.Register(operation, registerOptions);
                
                result = this._channel.RealmProxy.Monitor.IsConnected;

                return result;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        private static void OnClose(object sender, WampSessionCloseEventArgs e)
        {
            Debug.WriteLine("connection closed. reason: " + e.Reason);
        }

        private static void OnError(object sender, WampConnectionErrorEventArgs e)
        {
            Debug.WriteLine("connection error. error: " + e.Exception);
        }

        private static void OnConnectionEstablished(object sender, WampSessionCreatedEventArgs args)
        {
            Debug.WriteLine("connected session with ID " + args.SessionId);

            dynamic details = args.WelcomeDetails.OriginalValue.Deserialize<dynamic>();

            string authmethod = details.authmethod,
            authprovider = details.authprovider,
            authid = details.authid,
            authrole = details.authrole;

            Debug.WriteLine("authenticated using method '{0}' and provider '{1}'", authmethod,
                              authprovider);

            Debug.WriteLine("authenticated with authid '{0}' and authrole '{1}'", authid,
                              authrole);
        }

        public void Dispose()
        {
        }

        #endregion Methods
    }
}
