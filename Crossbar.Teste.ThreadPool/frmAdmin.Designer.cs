namespace Crossbar.Teste.ThreadPool
{
    partial class frmAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTempoServicos = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTempoPerdido = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMilissegundosEspera = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblProcessosFinalizados = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTempo = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.lvwItensResultados = new System.Windows.Forms.ListView();
            this.txtQtdChamadas = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTempoGasto = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtSegundos = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblRequisicoes = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(645, 441);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.lblRequisicoes);
            this.tabPage1.Controls.Add(this.txtSegundos);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.lblTempoGasto);
            this.tabPage1.Controls.Add(this.txtQtdChamadas);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lvwItensResultados);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.lblTempoServicos);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.lblTempoPerdido);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtMilissegundosEspera);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.lblProcessosFinalizados);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.lblTempo);
            this.tabPage1.Controls.Add(this.btnIniciar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(637, 415);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Threads";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(95, 378);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Tempo serviços:";
            // 
            // lblTempoServicos
            // 
            this.lblTempoServicos.AutoSize = true;
            this.lblTempoServicos.Location = new System.Drawing.Point(177, 378);
            this.lblTempoServicos.Name = "lblTempoServicos";
            this.lblTempoServicos.Size = new System.Drawing.Size(83, 13);
            this.lblTempoServicos.TabIndex = 25;
            this.lblTempoServicos.Text = "-tempoServicos-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(99, 351);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Tempo perdido:";
            // 
            // lblTempoPerdido
            // 
            this.lblTempoPerdido.AutoSize = true;
            this.lblTempoPerdido.Location = new System.Drawing.Point(177, 351);
            this.lblTempoPerdido.Name = "lblTempoPerdido";
            this.lblTempoPerdido.Size = new System.Drawing.Size(78, 13);
            this.lblTempoPerdido.TabIndex = 23;
            this.lblTempoPerdido.Text = "-tempoPerdido-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 323);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Tempo total:";
            // 
            // txtMilissegundosEspera
            // 
            this.txtMilissegundosEspera.Location = new System.Drawing.Point(154, 73);
            this.txtMilissegundosEspera.Name = "txtMilissegundosEspera";
            this.txtMilissegundosEspera.Size = new System.Drawing.Size(100, 20);
            this.txtMilissegundosEspera.TabIndex = 21;
            this.txtMilissegundosEspera.Text = "10000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Milissegundos espera:";
            // 
            // lblProcessosFinalizados
            // 
            this.lblProcessosFinalizados.AutoSize = true;
            this.lblProcessosFinalizados.Location = new System.Drawing.Point(177, 296);
            this.lblProcessosFinalizados.Name = "lblProcessosFinalizados";
            this.lblProcessosFinalizados.Size = new System.Drawing.Size(34, 13);
            this.lblProcessosFinalizados.TabIndex = 17;
            this.lblProcessosFinalizados.Text = "-proc-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Número de processos finalizados:";
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Location = new System.Drawing.Point(177, 323);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(35, 13);
            this.lblTempo.TabIndex = 13;
            this.lblTempo.Text = "-timer-";
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(554, 378);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 8;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // lvwItensResultados
            // 
            this.lvwItensResultados.GridLines = true;
            this.lvwItensResultados.Location = new System.Drawing.Point(8, 112);
            this.lvwItensResultados.MultiSelect = false;
            this.lvwItensResultados.Name = "lvwItensResultados";
            this.lvwItensResultados.Size = new System.Drawing.Size(621, 170);
            this.lvwItensResultados.TabIndex = 28;
            this.lvwItensResultados.UseCompatibleStateImageBehavior = false;
            // 
            // txtQtdChamadas
            // 
            this.txtQtdChamadas.Location = new System.Drawing.Point(154, 47);
            this.txtQtdChamadas.Name = "txtQtdChamadas";
            this.txtQtdChamadas.Size = new System.Drawing.Size(100, 20);
            this.txtQtdChamadas.TabIndex = 30;
            this.txtQtdChamadas.Text = "10000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Qtd chamadas:";
            // 
            // lblTempoGasto
            // 
            this.lblTempoGasto.AutoSize = true;
            this.lblTempoGasto.Location = new System.Drawing.Point(558, 22);
            this.lblTempoGasto.Name = "lblTempoGasto";
            this.lblTempoGasto.Size = new System.Drawing.Size(42, 13);
            this.lblTempoGasto.TabIndex = 32;
            this.lblTempoGasto.Text = "-tempo-";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(419, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Tempo de processamento:";
            // 
            // txtSegundos
            // 
            this.txtSegundos.Location = new System.Drawing.Point(154, 19);
            this.txtSegundos.Name = "txtSegundos";
            this.txtSegundos.Size = new System.Drawing.Size(100, 20);
            this.txtSegundos.TabIndex = 35;
            this.txtSegundos.Text = "10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(90, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Segundos:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(388, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(164, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Quantidade de requisições feitas:";
            // 
            // lblRequisicoes
            // 
            this.lblRequisicoes.AutoSize = true;
            this.lblRequisicoes.Location = new System.Drawing.Point(558, 42);
            this.lblRequisicoes.Name = "lblRequisicoes";
            this.lblRequisicoes.Size = new System.Drawing.Size(28, 13);
            this.lblRequisicoes.TabIndex = 36;
            this.lblRequisicoes.Text = "-req-";
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 441);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmAdmin";
            this.Text = "ThreadPool";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Label lblTempo;
        private System.Windows.Forms.Label lblProcessosFinalizados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMilissegundosEspera;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTempoPerdido;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTempoServicos;
        private System.Windows.Forms.ListView lvwItensResultados;
        private System.Windows.Forms.TextBox txtQtdChamadas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTempoGasto;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSegundos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblRequisicoes;
    }
}