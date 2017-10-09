using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using WampSharp.Core.Listener;
using WampSharp.V2;
using WampSharp.V2.Client;
using WampSharp.V2.Realm;

using Crossbar.Teste.Infra.Utils.Requests.PubSub;
using Crossbar.Teste.Infra.Connection;

namespace Crossbar.Teste.Infra.PubSub.Pub
{
    public class Publish<TMensagem>
        : IDisposable
    {
        #region Variables
        
        private IWampChannel _channel;
        private CrossbarConnection _conn;

        #endregion

        #region Constructors

        public Publish(CrossbarConnection conn)
        {
            this._conn = conn;

            this._channel = this._conn.Instance<IWampChannel>();
        }

        #endregion

        #region Attributes

        public bool ChannelOpen
        {
            get { return this._channel.RealmProxy.Monitor.IsConnected; }
        }

        #endregion

        #region Methods        
        
        public async Task<bool> Run(PubContent<TMensagem> mensagem)
        {
            bool result = false;

            IWampClientConnectionMonitor monitor = this._channel.RealmProxy.Monitor;

            if (string.IsNullOrEmpty(mensagem.NomeFila))
                throw new Exception("Informe um tópico!!");

            monitor.ConnectionBroken += OnClose;
            monitor.ConnectionError += OnError;

            ISubject<TMensagem> subject = null;

            try
            {
                subject = this._channel.RealmProxy.Services.GetSubject<TMensagem>(mensagem.NomeFila);
                subject.OnNext(mensagem.Mensagem);

                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
