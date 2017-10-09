using System.Threading;
using System.Threading.Tasks;

namespace Crossbar.Teste.Calculo2
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 50000; i++)
            {
                Task.Run(() => new Startup().Configure());
            }

            Thread.Sleep(System.Threading.Timeout.Infinite);
        }
    }
}
