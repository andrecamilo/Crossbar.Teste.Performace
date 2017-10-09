using Crossbar.Teste.Infra.Connection;
using Crossbar.Teste.Infra.RPC.Caller;
using Crossbar.Teste.Infra.Utils.Results.RPC;
using Crossbar.Teste.ThreadPool;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crossbar.Teste.ThreadPool
{
    delegate void SetTextCallback();

    public partial class frmAdmin : Form
    {

        CrossbarConnection _connection;
        Thread _thAtualizaProcessoFinalizados;

        Crossbar.Teste.Infra.ThreadPool threadPool;
        int _qtdProcessosFinalizados = 0;
        int _tempoPerdido = 0;
        int _tempoTotal = 0;
        int _tempoServicos = 0;
        int tempo = 0;
        int _numeroChamada = 0;
        object _lock = new object();

        int processosFinalizados = 0;

        List<string> _items = new List<string>();

        public frmAdmin()
        {
            InitializeComponent();
            InitListView();

            this.AbrirConexao();
        }

        private bool AbrirConexao()
        {
            this._connection = new CrossbarConnection();

            this._connection.Initilize(new ConnectionParameters
            {
                ServerAddress = "192.168.228.55",
                Port = 8080,
                ServerRealm = "realm1"
            });

            var resConn = this._connection.Connect();

            var res = resConn.Result;

            return res;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            InicializarDados();
            timer2.Start();
            _numeroChamada = 0;
            tempo = 0;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            tempo++;
            lblTempoGasto.Text = tempo.ToString();

            if (tempo <= Convert.ToInt32(txtSegundos.Text))
            {
                threadPool = new Crossbar.Teste.Infra.ThreadPool(Environment.ProcessorCount, "Teste performance");
                for (int i = 0; i < Convert.ToInt32(txtQtdChamadas.Text); i++)
                {
                    _numeroChamada++;
                    var workItemCalc1 = threadPool.EnqueueWorkItem(
                        NovaThreadCalc1,
                        _numeroChamada);
                }
            }

            lblRequisicoes.Text = _numeroChamada.ToString();
            this.Refresh();
        }

        private void InicializarDados()
        {
            this.lblProcessosFinalizados.Text = "0";
            this.processosFinalizados = 0;

            this._tempoPerdido = 0;
            this._tempoServicos = 0;
            this._tempoTotal = 0;
            this._qtdProcessosFinalizados = 0;

            this.lblTempoPerdido.Text = "";
            this.lblTempoServicos.Text = "";
            this.lblProcessosFinalizados.Text = "";
            this.lblTempo.Text = "";
        }

        private void NovaThreadCalc1(int numeroChamada)
        {
            var milissegundos = Convert.ToInt32(this.txtMilissegundosEspera.Text);
            var inicio = DateTime.Now;

            using (var callerCalculo1 = new Caller<CalculoCalc1Retorno, CalculoCalc1Parametro>(this._connection))
            {
                Task<DateTime> execCalc1 = callerCalculo1.Run(new CallerResult<CalculoCalc1Retorno, CalculoCalc1Parametro>
                {
                    NomeMetodo = "br.com.Crossbar.Teste.Calculo1.calc",
                    Parametros = new CalculoCalc1Parametro
                    {
                        TempoEspera = milissegundos,
                        NumeroChamada = numeroChamada,
                        DataInicio = inicio
                    },
                    CallbackCaller = this.CallbackAtualizaDadosCalc1
                });
            }

            this._thAtualizaProcessoFinalizados = new Thread(new ThreadStart(this.CallbackEscrita));
        }

        private void CallbackAtualizaDadosCalc1(CalculoCalc1Retorno valorCalc1)
        {
            processosFinalizados++;

            var now = DateTime.Now;

            this._tempoTotal = Convert.ToInt32((valorCalc1.DataInicio - now).TotalMilliseconds) * (-1);
            this._tempoPerdido = (Convert.ToInt32((valorCalc1.DataInicio - now).TotalMilliseconds) * (-1)) - Convert.ToInt32(valorCalc1.TempoExecucao);
            this._tempoServicos = Convert.ToInt32(valorCalc1.TempoExecucao);

            this._qtdProcessosFinalizados = processosFinalizados;

            if ((this._thAtualizaProcessoFinalizados.ThreadState != System.Threading.ThreadState.Running) && (this._thAtualizaProcessoFinalizados.ThreadState != System.Threading.ThreadState.Aborted))
            {
                this._thAtualizaProcessoFinalizados.Interrupt();
            }

            if (_numeroChamada == valorCalc1.NumeroChamada)
            {
                timer2.Stop();
                MessageBox.Show("Procedimento finalizado");
            }

            CallbackEscrita();
        }

        private void CallbackEscrita()
        {
            if (this.lblProcessosFinalizados.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AtualizaCampos);
                this.Invoke(d);
            }
        }

        private void AtualizaCampos()
        {
            lock (this._lock)
            {
                this.lblProcessosFinalizados.Text = this._qtdProcessosFinalizados.ToString();
                this.lblTempo.Text = this._tempoTotal.ToString();
                this.lblTempoPerdido.Text = this._tempoPerdido.ToString();
                this.lblTempoServicos.Text = this._tempoServicos.ToString();

                var item = new ListViewItem(new[]
                {
                    this.txtQtdChamadas.Text.ToString(),
                    this._tempoServicos.ToString(),
                    this._tempoTotal.ToString(),
                    this._tempoPerdido.ToString()
                });

                this.lvwItensResultados.Items.Add(item);
            }
        }

        private void InitListView()
        {
            this.lvwItensResultados.View = View.Details;

            this.lvwItensResultados.Columns.Clear();
            this.lvwItensResultados.Items.Clear();

            this.lvwItensResultados.Columns.Add("Chamadas por segundo", -2, HorizontalAlignment.Center);
            this.lvwItensResultados.Columns.Add("Tempo serviço", -2, HorizontalAlignment.Center);
            this.lvwItensResultados.Columns.Add("Tempo total", -2, HorizontalAlignment.Center);
            this.lvwItensResultados.Columns.Add("Tempo perdido", -2, HorizontalAlignment.Center);
        }
    }
}
