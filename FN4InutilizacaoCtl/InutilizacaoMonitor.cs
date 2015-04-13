using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Xml;
using FN4Common;
using FN4Common.DataAccess;
using System.Linq;
using FN4Common.Helpers;

namespace FN4InutilizacaoCtl
{
    public class InutilizacaoMonitor
    {
        private readonly Timer _tm;
        private const string Servico = "InutilizacaoService";

        public InutilizacaoMonitor()
        {
            _tm = new Timer
                     {
                         Interval = double.Parse(Geral.get_Parametro("intervaloInutilizacao"))
                     };
            _tm.Elapsed += ExecutaInutilizacoes;
        }

        public void Run()
        {
            _tm.Start();
            Log.registrarInfo("Serviço de Inutilização INICIADO.", Servico);
        }

        public void Pause()
        {
            _tm.Stop();
            Log.registrarInfo("Serviço de Inutilização PARADO.", Servico);
        }

        private void ExecutaInutilizacoes(object sender, ElapsedEventArgs e)
        {
            try
            {
                _tm.Stop();
                InutilizarNotas();
            }
            catch (Exception exception)
            {
                Log.registrarErro("Erro inesperado: " + exception.Message, Servico);
                throw;
            }
            finally
            {
                _tm.Start();
            }
        }

        private static void InutilizarNotas()
        {
            try
            {
                var notasASeremInutilizadas = notas.obterNotasASeremInutilizadas();

                if (notasASeremInutilizadas == null || notasASeremInutilizadas.Count == 0)
                {
                    return;
                }

                var series = notasASeremInutilizadas.Select(n => new
                                                                     {
                                                                         n.serie,
                                                                         cnpj = n.NFe_emit_CNPJ
                                                                     }).Distinct().ToList();

                foreach (var grupoPorSerie in
                    series.Select(s => notasASeremInutilizadas.Where(n => n.serie == s.serie && n.NFe_emit_CNPJ.Equals(s.cnpj)).OrderBy(o => o.NFe_ide_nNF).ToList()))
                {
                    InutilizarGrupoDeNotas(grupoPorSerie);
                }
            }
            catch (Exception exception)
            {
                Log.registrarErro("Erro inesperado: " + exception.Message, Servico);
                throw;
            }
        }

        private static void InutilizarGrupoDeNotas(IList<notaVO> notasAInutilizar)
        {
            if (notasAInutilizar == null) throw new ArgumentNullException("notasAInutilizar está nulo");

            var inutNFe = new XmlDocument();
            var pathInutNfe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\inutNFe.xml");
            var pathCabecMsg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XML\cabecMsg.xml");

            inutNFe.Load(pathInutNfe);

            var cabecMsg = new XmlDocument();
            cabecMsg.Load(pathCabecMsg);
            cabecMsg.SelectSingleNode("/*[local-name()='cabecMsg' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='versaoDados' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = Geral.get_Parametro("VersaoProduto");

            var empresa = empresaDAO.obterEmpresa(notasAInutilizar[0].NFe_emit_CNPJ, string.Empty);

            var justificativa = justificativas.obterJustificativa(int.Parse(notasAInutilizar[0].NFe_ide_nNF.ToString()), notasAInutilizar[0].serie, notasAInutilizar[0].NFe_emit_CNPJ);

            if (justificativa == null)
            {
                Log.registrarWarn("Não há justificativa para inutilização das notas de " + notasAInutilizar[0].NFe_ide_nNF + " a " + notasAInutilizar[notasAInutilizar.Count - 1].NFe_ide_nNF, Servico);
                
                ExcluirNotasAInutilizar(notasAInutilizar);

                return;
            }

            inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = (empresa.homologacao + 1).ToString();
            inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cUF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = UFs.ListaDeCodigos[empresa.uf].ToString();
            inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='ano' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = DateTime.Now.ToString("yy");
            inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='CNPJ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = empresa.cnpj;
            inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nNFIni' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = notasAInutilizar[0].NFe_ide_nNF.ToString();
            inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nNFFin' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = notasAInutilizar[notasAInutilizar.Count - 1].NFe_ide_nNF.ToString();
            inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xJust' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = justificativa.descricao;
            inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='serie' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = notasAInutilizar[0].serie.ToString();

            inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/@Id").InnerText = ObterIdInutilizacao(inutNFe);

            var uf = string.Empty;

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

            var webservice = webservices.obterURLWebservice(uf, "NfeInutilizacao", Geral.get_Parametro("VersaoProduto"), empresa.homologacao);

            if (webservice == null)
            {
                throw new Exception("Webservice de inutilização não localizado.");
            }

            var certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa, Servico);

            if (certificado.Handle == IntPtr.Zero)
            {
                ExcluirNotasAInutilizar(notasAInutilizar);

                Log.registrarErro("Certificado da empresa " + empresa.nome + "/" + empresa.cnpj + " não encontrado.", Servico);

                return;
            }

            inutNFe.PreserveWhitespace = true;

            inutNFe = XmlHelper.assinarNFeXML(inutNFe, inutNFe.GetElementsByTagName("infInut")[0].Attributes["Id"].InnerText, empresa.idEmpresa);

            Log.registrarInfo("idEmpresa: " + empresa.idEmpresa + " // CNPJ: " + empresa.cnpj + " // url: " + webservice.url + " // certificado: " + certificado.SerialNumber, Servico);

            var wspr = new NFe.InutilizacaoPR.NfeInutilizacao3 { Url = webservice.url };
            var ws = new NFe.Inutilizacao.NfeInutilizacao2 {Url = webservice.url};

            if(uf=="PR"){

                    wspr.ClientCertificates.Add(certificado);
                    wspr.nfeCabecMsgValue = new NFe.InutilizacaoPR.nfeCabecMsg
                                              {
                                                  versaoDados = cabecMsg.InnerText,
                                                  cUF = UFs.ListaDeCodigos[empresa.uf].ToString()
                                              };
            } else{ 
               
                    ws.ClientCertificates.Add(certificado);
                    ws.nfeCabecMsgValue = new NFe.Inutilizacao.nfeCabecMsg
                                {
                                    versaoDados = cabecMsg.InnerText,
                                    cUF = UFs.ListaDeCodigos[empresa.uf].ToString()
                                };
            };

            Log.registrarInfo("Webservice montado para utilização", Servico);

            var pathXmlInutilizacao = Geral.get_Parametro("pastaDeProcessadas");

            pathXmlInutilizacao = Path.Combine(pathXmlInutilizacao, notasAInutilizar[0].NFe_emit_CNPJ);
            pathXmlInutilizacao = Path.Combine(pathXmlInutilizacao, "Inutilizadas");

            Log.registrarInfo("pathXmlInutilizacao montado: " + pathXmlInutilizacao, Servico);

            if (!Directory.Exists(pathXmlInutilizacao))
            {
                Directory.CreateDirectory(pathXmlInutilizacao);
            }

            var pathXmlInutilizacaoSalvar = Path.Combine(pathXmlInutilizacao,
                                               string.Format("Envio{0}_{1}_{2}_{3}.xml", empresa.cnpj, notasAInutilizar[0].serie, notasAInutilizar[0].NFe_ide_nNF,
                                                             notasAInutilizar[notasAInutilizar.Count - 1].NFe_ide_nNF));

            Log.registrarInfo("pathXmlInutilizacaoSalvar montado: " + pathXmlInutilizacaoSalvar, Servico);
            inutNFe.Save(pathXmlInutilizacaoSalvar);

            Log.registrarInfo("Enviando inutilização das notas de " + notasAInutilizar[0].NFe_ide_nNF + " a " + notasAInutilizar[notasAInutilizar.Count - 1].NFe_ide_nNF + " da série " + notasAInutilizar[0].serie, Servico);

            var resultado = string.Empty;
            var motivo = string.Empty;

     
            pathXmlInutilizacaoSalvar = Path.Combine(pathXmlInutilizacao,
                                               string.Format("Retorno{0}_{1}_{2}_{3}.xml", empresa.cnpj, notasAInutilizar[0].serie, notasAInutilizar[0].NFe_ide_nNF,
                                                             notasAInutilizar[notasAInutilizar.Count - 1].NFe_ide_nNF));

            Log.registrarInfo("pathXmlInutilizacaoSalvar montado: " + pathXmlInutilizacaoSalvar, Servico);

            var xmlRetorno = new XmlDocument();
            var stringWriter = new StringWriter();
            var xmlTextWriter = new XmlTextWriter(stringWriter);

            if (uf == "PR")
            {
                var ret = wspr.nfeInutilizacaoNF(inutNFe);
                resultado = ret.SelectSingleNode("/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText;
                motivo = ret.SelectSingleNode("/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText;
                ret.WriteTo(xmlTextWriter);
            }
            else
            {
                var ret = ws.nfeInutilizacaoNF2(inutNFe);
                resultado = ret.SelectSingleNode("/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText;
                motivo = ret.SelectSingleNode("/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText;
                ret.WriteTo(xmlTextWriter);
            };

            Log.registrarInfo("Resultado: " + resultado + " // motivo: " + motivo, Servico);

            xmlRetorno.LoadXml(stringWriter.ToString());
            xmlRetorno.Save(pathXmlInutilizacaoSalvar);
            ws.Dispose();
            wspr.Dispose();

            if (resultado.Equals("102")) // sucesso na inutilização
            {
                foreach (var nota in notasAInutilizar)
                {
                    notas.inutilizarNota((int)nota.NFe_ide_nNF, nota.serie, nota.NFe_emit_CNPJ);
                    justificativas.removerJustificativas((int)nota.NFe_ide_nNF, nota.serie, nota.NFe_emit_CNPJ);
                    InserirHistorico("23", "Inutilização realizada com sucesso", nota);
                }
            }
            else
            {
                Log.registrarWarn("As notas de " + notasAInutilizar[0].NFe_ide_nNF + " a " + notasAInutilizar[notasAInutilizar.Count - 1].NFe_ide_nNF + ", da série " + notasAInutilizar[0].serie + " não puderam ser canceladas: " + motivo, Servico);
                ExcluirNotasAInutilizar(notasAInutilizar);
            }
        }

        private static string ObterIdInutilizacao(XmlDocument inutNFe)
        {
            var id = string.Empty;
            id += "ID" + inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cUF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText;
            id += inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='ano' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText;
            id += inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='CNPJ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText;
            id += inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='mod' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText;
            id += string.Format("{0:000}", int.Parse(inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='serie' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText));
            id += string.Format("{0:000000000}", int.Parse(inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nNFIni' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText));
            id += string.Format("{0:000000000}", int.Parse(inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nNFFin' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText));

            return id;
        }

        private static void ExcluirNotasAInutilizar(IEnumerable<notaVO> listaDeNotas)
        {
            foreach (var nota in listaDeNotas)
            {
                if (nota.statusDaNota != 6)
                {
                    notas.excluirNota(nota.NFe_ide_nNF, nota.serie, nota.NFe_emit_CNPJ);
                }

                justificativas.removerJustificativas((int)nota.NFe_ide_nNF, nota.serie, nota.NFe_emit_CNPJ);
            }
        }

        private static void InserirHistorico(string tipo, string texto, notaVO nota)
        {
            var historico = new historicoVO(tipo, texto, int.Parse(nota.NFe_ide_nNF.ToString()), nota.NFe_emit_CNPJ, nota.serie);

            notas.inserirHistorico(historico);
        }
    }
}
