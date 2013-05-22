using System;
using System.IO;
using System.Timers;
using System.Xml;
using FN4Common;
using FN4Common.DataAccess;
using FN4Common.Helpers;

namespace FN4CancelamentoCtl
{
    public class CancelamentoMonitor
    {
        private readonly Timer _tm;
        private const string Servico = "CancelamentoService";

        public CancelamentoMonitor()
        {
            try
            {
                _tm = new Timer
                {
                    Interval = double.Parse(Geral.get_Parametro("intervaloCancelamento")),
                    Enabled = false
                };
                _tm.Elapsed += executaCancelamentos;
            }
            catch (Exception exception)
            {
                Log.registrarErro(exception.Message, Servico);
            }
        }

        public void Run()
        {

            _tm.Start();
            Log.registrarInfo("Serviço de cancelamento INICIADO.", Servico);
        }

        public void Pause()
        {
            _tm.Stop();
            Log.registrarInfo("Serviço de cancelamento PARADO.", Servico);
        }

        private void executaCancelamentos(object sender, ElapsedEventArgs e)
        {
            try
            {
                _tm.Stop();
                
                CancelarNotas();
            }
            catch (Exception exception)
            {
                Log.registrarErro("Erro durante processo de cancelamento :" + exception.Message, Servico);
            }
            finally
            {
                _tm.Start();
            }
        }

        private static void CancelarNotas()
        {
            var notasACancelar = notas.obterNotasASeremCanceladas();

            if (notasACancelar == null || notasACancelar.Count == 0)
            {
                return;
            }

            foreach (var nota in notasACancelar)
            {
                try
                {
                    CancelarNota(nota);
                }
                catch (Exception exception)
                {
                    nota.statusDaNota = 22;
                    notas.alterarNota(nota);
                    Log.registrarErro("Erro inesperado: " + exception.Message, Servico);
                }
                finally
                {
                    justificativas.removerJustificativas(int.Parse(nota.NFe_ide_nNF.ToString()), nota.serie, nota.NFe_emit_CNPJ);
                }
            }
        }

        private static void CancelarNota(notaVO nota)
        {
            Log.registrarInfo("Solicitando cancelamento da nota " + nota.NFe_ide_nNF + ", série " + nota.serie + " e CNPJ " + nota.NFe_emit_CNPJ, "CancelamentoService");

            var empresa = empresaDAO.obterEmpresa(nota.NFe_emit_CNPJ, string.Empty);

            var cancNFe = new XmlDocument();
            var pathCancNfeXml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\cancNFe.xml");
            cancNFe.Load(pathCancNfeXml);

            var pathCabecMsgXml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\cabecMsg_canc.xml");//tive que mudar para um cabeçalho proprio pois era versao antiga
            var cabecMsg = new XmlDocument();

            var justificativa = justificativas.obterJustificativa(int.Parse(nota.NFe_ide_nNF.ToString()), nota.serie, nota.NFe_emit_CNPJ);

            // Obtendo id da nota original

            var pathProtocolo = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF + "_procNFe.xml");
            string idNota;

            // Se o arquivo de protocolo não existir
            if (!File.Exists(pathProtocolo))
            {
                // Carregamos o primeiro arquivo de nota
                var pathNotaOriginal = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF + ".xml");
                
                // Se o arquivo de nota não existir... Usamos o que tem no banco mesmo
                if (!File.Exists(pathNotaOriginal))
                {
                    idNota = "ID" + nota.NFe_infNFe_id;
                }
                else // Se existir
                {
                    var xmlOriginal = new XmlDocument();
                    xmlOriginal.Load(pathNotaOriginal);

                    // Carregamos o nó que contém o Id
                    var no = xmlOriginal.SelectSingleNode("/NFe/infNFe/@Id");

                    // Se o nó for nulo, mesma coisa que se o arquivo não existisse
                    if (no == null)
                    {
                        idNota = "ID" + nota.NFe_infNFe_id;
                    }
                    else
                    {
                        var idAntigo = no.InnerText;
                        idNota = idAntigo.Replace("NFe", string.Empty);
                    }
                }
            }
            else
            {
                idNota = nota.NFe_infNFe_id;
            }
            
            
            if (justificativa == null)
            {
                throw new InvalidOperationException("Não foi encontrada justificativa para cancelar essa nota.");
            }

            cabecMsg.Load(pathCabecMsgXml);

            cancNFe.SelectSingleNode("/*[local-name()='cancNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/@Id").InnerText = "ID" + idNota;
            cancNFe.SelectSingleNode("/*[local-name()='cancNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = (empresa.homologacao + 1).ToString();
            cancNFe.SelectSingleNode("/*[local-name()='cancNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='chNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = nota.NFe_infNFe_id;
            cancNFe.SelectSingleNode("/*[local-name()='cancNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = nota.protNfe_nProt;
            cancNFe.SelectSingleNode("/*[local-name()='cancNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xJust' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = justificativa.descricao;

            cancNFe = XmlHelper.assinarNFeXML(cancNFe, cancNFe.GetElementsByTagName("infCanc")[0].Attributes["Id"].InnerText, empresa.idEmpresa);

            var ws = new NFe.Cancelamento.NfeCancelamento2();

            var certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa);

            if (certificado.Handle == IntPtr.Zero)
            {
                // desfaz cancelamento

                nota.statusDaNota = 22;

                notas.alterarNota(nota);

                Log.registrarErro("Certificado da empresa " + empresa.nome + "/" + empresa.cnpj + " não encontrado.", Servico);
            }

            ws.ClientCertificates.Add(Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa));

            cancNFe.Save(Path.Combine(nota.pastaDeTrabalho, "cancNFe.xml"));

            InserirHistorico("19", string.Empty, nota);

            string uf;

            if (UfsSemWebServices.SVAN.Contains(empresa.uf))
            {
                uf = "SVAN";
            }
            else if (UfsSemWebServices.SVRS.Contains(empresa.uf))
            {
                uf = "SVRS";
            }
            else
            {
                uf = empresa.uf;
            }

            var webservice = webservices.obterURLWebservice(uf, "NfeCancelamento",
                                                            Geral.get_Parametro("VersaoProduto"), empresa.homologacao);

            if (webservice == null)
            {
                throw new Exception("Webservice de cancelamento não localizado.");
            }

            ws.Url = webservice.url;
            ws.nfeCabecMsgValue = new NFe.Cancelamento.nfeCabecMsg
                                      {
                                          versaoDados = cabecMsg.InnerText,
                                          cUF = UFs.ListaDeCodigos[empresa.uf].ToString()
                                      };

            var retorno = ws.nfeCancelamentoNF2(cancNFe);

            var resultado = retorno.SelectSingleNode("/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText;
            var motivo = retorno.SelectSingleNode("/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText;

            if (resultado.Equals("101") || resultado.Equals("151"))
            {
                nota.statusDaNota = 4;
                nota.retEnviNFe_xMotivo = motivo;
                nota.retEnviNFe_cStat = resultado;
                InserirHistorico("20", motivo, nota);
                notas.alterarNota(nota);
            }
            else if (resultado.Equals("420") || resultado.Equals("218"))
            {
                nota.statusDaNota = 4;
                notas.alterarNota(nota);
                InserirHistorico("21", motivo, nota);
            }
            else
            {
                nota.statusDaNota = 22;
                notas.alterarNota(nota);

                InserirHistorico("21", motivo, nota);
            }

            ws.Dispose();

            var xmlRetorno = new XmlDocument();
            var stringWriter = new StringWriter();
            var xmlTextWriter = new XmlTextWriter(stringWriter);

            retorno.WriteTo(xmlTextWriter);

            xmlRetorno.LoadXml(stringWriter.ToString());

            xmlRetorno.Save(Path.Combine(nota.pastaDeTrabalho, "retorno_cancNFe.xml"));
        }

        private static void InserirHistorico(string tipo, string texto, notaVO nota)
        {
            var historico = new historicoVO(tipo, texto, int.Parse(nota.NFe_ide_nNF.ToString()), nota.NFe_emit_CNPJ, nota.serie);

            notas.inserirHistorico(historico);
        }
    }
}
