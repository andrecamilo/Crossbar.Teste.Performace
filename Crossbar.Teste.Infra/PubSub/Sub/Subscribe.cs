using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using WampSharp.Core.Listener;
using WampSharp.V2;
using WampSharp.V2.Client;
using WampSharp.V2.Realm;

using Crossbar.Teste.Infra.Utils.Results.PubSub;
using Crossbar.Teste.Infra.Connection;

namespace Crossbar.Teste.Infra.PubSub.Sub
{
    public class Subscribe<TMensagem>
        : IDisposable
    {
        #region Variables
        
        private IWampChannel _channel;
        private CrossbarConnection _conn;

        #endregion

        #region Constructors

        public Subscribe(CrossbarConnection conn)
        {
            this._conn = conn;

            this._channel = this._conn.Instance<IWampChannel>();
        }

        #endregion

        #region Methods

        public async Task<bool> Run(SubContent<TMensagem> mensagem)
        {
            bool result = false;

            IWampClientConnectionMonitor monitor = this._channel.RealmProxy.Monitor;

            if (string.IsNullOrEmpty(mensagem.NomeFila))
                throw new Exception("Informe um tópico!!");

            monitor.ConnectionBroken += OnClose;
            monitor.ConnectionError += OnError;

            IWampRealmServiceProvider services = this._channel.RealmProxy.Services;

            ISubject<TMensagem> helloSubject =
                services.GetSubject<TMensagem>(mensagem.NomeFila);

            IDisposable subscription = helloSubject.Subscribe<TMensagem>(x => { mensagem.MessageListener(x); });

            result = true;

            return result;
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
