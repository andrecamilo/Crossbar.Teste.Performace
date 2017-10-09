using System;
using System.Threading.Tasks;

using WampSharp.V2;
using WampSharp.V2.Client;

namespace Crossbar.Teste.Infra.Connection
{
    public class CrossbarConnection
        : IDisposable
    {
        #region Variables

        private DefaultWampChannelFactory _factory = new DefaultWampChannelFactory();
        private IWampChannel _channel;
        private ConnectionParameters _parameters = null;
        private IWampClientAuthenticator _authenticator;

        #endregion

        #region Attributes
        
        public IWampChannel Instance<IWampChannel>()
        {
            return (IWampChannel)this._channel;
        }

        public ConnectionParameters ConnectionParameters
        {
            get { return this._parameters; }
        }

        #endregion

        #region Methods

        private string GetURI()
        {
            string result = string.Empty;

            if (this._parameters.Port > 0)
            {
                result = string.Format("ws://{0}:{1}/ws", this._parameters.ServerAddress, this._parameters.Port.ToString());
            }
            else
            {
                result = string.Format("ws://{0}/ws", this._parameters.ServerAddress);
            }

            return result;
        }

        public void Initilize(ConnectionParameters parameters)
        {
            this._parameters = parameters as ConnectionParameters;
        }

        public async Task<bool> Connect()
        {
            if (this._channel == null)
            {
                if (string.IsNullOrEmpty(this._parameters.Login))
                {
                    this._channel = this._factory.CreateJsonChannel(this.GetURI(), this._parameters.ServerRealm);
                }
                else
                {
                    if (string.IsNullOrEmpty(this._parameters.Salt))
                    {
                        this._authenticator =
                            new WampCraClientAuthenticator
                            (
                                authenticationId: this._parameters.Login,
                                secret: this._parameters.Password
                            );
                    }
                    else
                    {
                        this._authenticator =
                            new WampCraClientAuthenticator
                            (
                                authenticationId: this._parameters.Login,
                                secret: this._parameters.Password,
                                salt: this._parameters.Salt,
                                iterations: this._parameters.Iterations,
                                keyLen: this._parameters.KeyLen
                            );
                    }

                    this._channel = this._factory.CreateJsonChannel(this.GetURI(), this._parameters.ServerRealm, this._authenticator);
                }

                await this._channel.Open().ConfigureAwait(false);
            }

            return this._channel.RealmProxy.Monitor.IsConnected;
        }

        public bool Disconnect()
        {
            if (this._channel.RealmProxy.Monitor.IsConnected)
            {
                this._channel.Close();
            }

            return !this._channel.RealmProxy.Monitor.IsConnected;
        }

        public void Dispose()
        {
            this.Disconnect();

            this._factory = null;
            this._channel = null;
            this._parameters = null;
        }

        #endregion
    }
}
