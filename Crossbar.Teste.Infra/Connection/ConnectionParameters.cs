namespace Crossbar.Teste.Infra.Connection
{
    public class ConnectionParameters
    {
        public string ServerAddress { get; set; }

        public string ServerRealm { get; set; }

        public int Port { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public int Iterations { get; set; }

        public int KeyLen { get; set; }
    }
}
