using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using FN4Common;
using FN4Common.DataAccess;
using FN4ImpressaoCtl;

namespace FN4PdfDanfeApp
{
    public partial class frmMonitoramento : Form
    {
        private bool _firstLoad = false;
        private ImpressaoMonitor _mon;
        private bool _statusMonitoramento;

        public frmMonitoramento()
        {
            InitializeComponent();

            lblStatus.Text = string.Format(lblStatus.Tag.ToString(), "Parado");

            timer.Interval = int.Parse(Geral.get_Parametro("intervaloDeVarreduraDeSaida"));

            _mon = new ImpressaoMonitor();
        }

        private void AlterarStatusMonitoramento()
        {
            if (_statusMonitoramento)
            {
                _mon.pause();
                lblStatus.Text = string.Format(lblStatus.Tag.ToString(), "Parado");
                tsmIniciarMonitoramento.Text = "Iniciar Monitoramento";
            }
            else
            {
                _mon.run();
                lblStatus.Text = string.Format(lblStatus.Tag.ToString(), "Iniciado");
                tsmIniciarMonitoramento.Text = "Parar monitoramento";
            }
        }

        private void btnMonitorar_Click(object sender, EventArgs e)
        {
            AlterarStatusMonitoramento();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                GerarPdfs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro! " + ex.Message);
                throw;
            }   
        }

        private void GerarPdfs()
        {
            var nomeServico = "PdfDanfeService";
            
            timer.Stop();

            lblStatus.Text = string.Format(lblStatus.Tag.ToString(), "Trabalhando...");

            try
            {
                var notasParaDanfe = notas.obterNotasParaDanfe();

                if (notasParaDanfe.Count > 0)
                {

                    Log.registrarInfo("Encontradas " + notasParaDanfe.Count + " notas para gerar DANFE. Iniciando",
                                      nomeServico);

                    foreach (var nota in notasParaDanfe)
                    {
                        var pathXml = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF + "_procNFe.xml");
                        var nomePdf = nota.NFe_infNFe_id + ".pdf";
                        var pathLogo = Path.Combine(Geral.get_Parametro("pastaLogos"), nota.NFe_emit_CNPJ + ".jpg");
                        var pathDanfe = Geral.get_Parametro("pathDanfe") + " ";
    // ReSharper disable AssignNullToNotNullAttribute
                        var pathDirPdf = Path.Combine(Path.GetDirectoryName(pathDanfe),
    // ReSharper restore AssignNullToNotNullAttribute
                                                      "PDF" + @"\" + nota.NFe_ide_dEmi.ToString("yyyy") + nota.NFe_ide_dEmi.ToString("MM"));
                        var pdfArgs = "arquivo={0}";

                        if (!File.Exists(pathXml))
                        {
                            Log.registrarErro("Arquivo XML da nota " + nota.NFe_ide_nNF + " não existe", nomeServico);
                            nota.imprimeDanfe = 0;
                            notas.alterarNota(nota);
                            continue;
                        }

                        pdfArgs = string.Format(pdfArgs, pathXml);

                        if (File.Exists(pathLogo))
                        {
                            pdfArgs += " logotipo={0}";
                            pdfArgs = string.Format(pdfArgs, pathLogo);
                        }

                        if (!Geral.get_Parametro("formatoPDF").Equals(string.Empty))
                        {
                            pdfArgs += " configuracao={0}";
                            pdfArgs = string.Format(pdfArgs, Geral.get_Parametro("formatoPDF"));
                        }

                        var processoUnidanfe = new Process
                                                   {
                                                       StartInfo =
                                                           {
                                                               FileName = Geral.get_Parametro("pathDanfe"),
                                                               Arguments = pdfArgs,
                                                               Verb = "runas",
                                                               UseShellExecute = true
                                                           }
                                                   };

                        processoUnidanfe.Start();
                        processoUnidanfe.WaitForExit();

                        Log.registrarInfo(
                            "Executando o comando: " + processoUnidanfe.StartInfo.FileName + " " +
                            processoUnidanfe.StartInfo.Arguments, nomeServico);

                        var pathPdf = Path.Combine(pathDirPdf, nomePdf);

                        var contador = 0;

                        while (contador < int.Parse(Geral.get_Parametro("tempoSleepGeracaoPDF")) && !File.Exists(pathPdf))
                        {
                            contador++;
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (!File.Exists(pathPdf))
                        {
                            Log.registrarErro("Erro ao criar PDF para a nota " + nota.NFe_ide_nNF + " e série " + nota.serie + "\nO path é: " + pathPdf, nomeServico);
                            nota.imprimeDanfe = 0;
                            notas.alterarNota(nota);
                            continue;
                        }

                        nota.imprimeDanfe = 0;
                        nota.impressaoSolicitada = 1;
                        notas.alterarNota(nota);

                        Log.registrarInfo(
                            "DANFE da nota " + nota.NFe_ide_nNF + " - série " + nota.serie + " impresso com sucesso.",
                            nomeServico);
                    }
                }
            }
            catch (Exception exception)
            {
                Log.registrarErro("Erro: " + Geral.ObterExceptionMessagesEmCascata(exception), nomeServico);
            }
            
            lblStatus.Text = string.Format(lblStatus.Tag.ToString(), "Iniciado");
            timer.Start();
        }

        private void frmMonitoramento_Resize(object sender, EventArgs e)
        {
            MinimizarOuRestaurar();
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            MinimizarOuRestaurar();
        }

        private void tsmIniciarMonitoramento_Click(object sender, EventArgs e)
        {
            AlterarStatusMonitoramento();
        }

        private void tsmRestaurar_Click(object sender, EventArgs e)
        {
            MinimizarOuRestaurar();
        }

        private void MinimizarOuRestaurar()
        {
            if (!_firstLoad)
            {
                _firstLoad = true;
                return;
            }
            
            if (FormWindowState.Normal == WindowState)
            {
                Hide();
                WindowState = FormWindowState.Minimized;
                tsmRestaurar.Text = "Restaurar";
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
                tsmRestaurar.Text = "Minimizar";
            }
        }

        private void tsmSair_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void frmMonitoramento_FormClosing(object sender, FormClosingEventArgs e)
        {
            var resposta = MessageBox.Show("Deseja realmente sair do sistema de monitoramento de DANFE?",
                                           "Confirmação de saída", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resposta == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
