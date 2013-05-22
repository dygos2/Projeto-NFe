using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FN4Common;
using FN4Common.DataAccess;
using FN4Common.Helpers;
using System.Timers;

namespace FN4IntegracaoPostBackCtl
{
    public class IntegracaoPostBackMonitor
    {
        private readonly Timer _tm;
        private const string Servico = "IntegracaoPostBackService";

        public IntegracaoPostBackMonitor()
        {
            _tm = new Timer
                      {
                          Interval = double.Parse(Geral.get_Parametro("intervaloIntegracaoPostBack"))
                      };
            _tm.Elapsed += IniciaTrabalho;
        }

        public void Run()
        {
            _tm.Start();
            Log.registrarInfo("Serviço de Postback iniciado com sucesso.", Servico);
        }

        public void Pause()
        {
            _tm.Stop();
            Log.registrarInfo("Serviço de Postback parado com sucesso.", Servico);
        }

        private void IniciaTrabalho(object sender, ElapsedEventArgs e)
        {
            try
            {
                _tm.Stop();
                ExecutaPostBacks();
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
        
        public static void ExecutaPostBacks()
        {

                var notas_arr_ct = 0;
                var numeros = string.Empty;
                var series = string.Empty;
                var status = string.Empty;
                var tentativas = string.Empty;
                var chaves = string.Empty;
                var cstats = string.Empty;
                var motivos = string.Empty;
                var urlsDanfe = string.Empty;
                var tp_amb = string.Empty;
                var resultado = string.Empty;
                var empresa_cnpj = string.Empty;
                var empresa_postb = string.Empty;
                var pedido_num = string.Empty;

                var notas = FN4Common.DataAccess.notas.obterNotasParaPostBack();
                              
                if (notas.Count > 0)
                {
                    try
                    {
                        var timeout = Geral.get_Parametro("timeoutIntegracaoPostBack");
                        tp_amb = Geral.get_Parametro("tp_amb");

                        foreach (var nota in notas)
                        {

                            var empresa = empresaDAO.obterEmpresaComUrlDePostBack(nota.NFe_emit_CNPJ);
                            empresa_cnpj = string.Format("{0}{1}{2}", empresa.nome ," | ", empresa.cnpj);
                            empresa_postb = empresa.urlPostBack;
                            var empresa_postb_arr = empresa_postb.Split(new char[] { '|' });

                            var urlDanfe = "";
                            var tp_sys = configuracaoDAO.obterConfiguracao("tp_sys", empresa.idEmpresa);

                             if (tp_sys != null)//se retornar valor, é por que o usuário tem contratao recepção de PDF
                             {
                                 var anoMes = nota.NFe_ide_dEmi.ToString("yyyy") + nota.NFe_ide_dEmi.ToString("MM");
                                 urlDanfe = string.Format("{0}{1}{2}{3}{4}{5}", Geral.get_Parametro("urlDanfe"), nota.NFe_emit_CNPJ, "/", anoMes, "/", nota.NFe_infNFe_id + ".pdf");
                             } else {
                                //montar a chamada do servidor Danfe para passar para o postback
                                var pasta_trab_arr = nota.pastaDeTrabalho.Split(new char[] { '\\' });
                                //urlDanfe = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Geral.get_Parametro("servidorDanfe"), "cnpj=", empresa.cnpj, "&ano=", pasta_trab_arr[5], "&mes=", pasta_trab_arr[6], "&dia=", pasta_trab_arr[7], "&nnfe=", pasta_trab_arr[8], "&arq=", nota.NFe_ide_nNF, "_procNFe&ch=", nota.NFe_infNFe_id, "&srvid=", Geral.get_Parametro("srvid"), "&tp_amb=", empresa.homologacao + 1, "&dest_saida=D");
                                //se estiver em contingencia, monta parametros para emitir em contingencia
                                if (nota.statusDaNota == 51 || nota.statusDaNota == 5)
                                {
                                    urlDanfe = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Geral.get_Parametro("servidorDanfe"), "cnpj=", empresa.cnpj, "&ano=", pasta_trab_arr[5], "&mes=", pasta_trab_arr[6], "&dia=", pasta_trab_arr[7], "&nnfe=", pasta_trab_arr[8], "&arq=", nota.NFe_ide_nNF, "_assinado&ch=", nota.NFe_infNFe_id, "&srvid=", Geral.get_Parametro("srvid"), "&tp_amb=", empresa.homologacao + 1, "&dest_saida=D");
                                }
                                else
                                {
                                    urlDanfe = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", Geral.get_Parametro("servidorDanfe"), "cnpj=", empresa.cnpj, "&ano=", pasta_trab_arr[5], "&mes=", pasta_trab_arr[6], "&dia=", pasta_trab_arr[7], "&nnfe=", pasta_trab_arr[8], "&arq=", nota.NFe_ide_nNF, "_procNFe&ch=", nota.NFe_infNFe_id, "&srvid=", Geral.get_Parametro("srvid"), "&tp_amb=", empresa.homologacao + 1, "&dest_saida=D");
                                };
                             };

                            numeros = string.IsNullOrWhiteSpace(numeros) ? nota.NFe_ide_nNF.ToString() : numeros + "§" + nota.NFe_ide_nNF;
                            series = string.IsNullOrWhiteSpace(series) ? nota.serie.ToString() : series + "§" + nota.serie;
                            status = string.IsNullOrWhiteSpace(status) ? nota.statusDaNota.ToString() : status + "§" + nota.statusDaNota;
                            tentativas = string.IsNullOrWhiteSpace(tentativas) ? nota.tentativasDeInclusao.ToString() : tentativas + "§" + nota.tentativasDeInclusao;
                            chaves = string.IsNullOrWhiteSpace(chaves) ? nota.NFe_infNFe_id : chaves + "§" + nota.NFe_infNFe_id;
                            cstats = string.IsNullOrWhiteSpace(cstats) ? (nota.retEnviNFe_cStat ?? "§") : cstats + "§" + nota.retEnviNFe_cStat;
                            motivos = string.IsNullOrWhiteSpace(motivos) ? (nota.retEnviNFe_xMotivo ?? "§") : motivos + "§" + nota.retEnviNFe_xMotivo;
                            urlsDanfe = string.IsNullOrWhiteSpace(urlsDanfe) ? urlDanfe : urlsDanfe + "§" + urlDanfe;
                            //voltando o número do pedido para a loja virtual
                            pedido_num = string.IsNullOrWhiteSpace(pedido_num) ? nota.num_pedido : pedido_num + "§" + nota.num_pedido;

                            //antes de dar qualquer problema, muda o status para enviado
                            nota.ret_post_data = System.DateTime.Now;
                            FN4Common.DataAccess.notas.alterarNota(nota);

                            //testando se próxima nota ainda é da mesma empresa, se não for, envia os dados por post, loga e zera os campos
                            notas_arr_ct = notas_arr_ct + 1;

                            //inserindo historico de enviado postback
                            InserirHistorico("25", "", nota);

                            if (notas.Count > notas_arr_ct)
                            {
                                if (!string.Equals(nota.NFe_emit_CNPJ, notas[notas_arr_ct].NFe_emit_CNPJ))
                                {
                                    for (int ii = 0; ii < empresa_postb_arr.Length; ii++)
                                    {
                                        var httpPost = new PostSubmitter(empresa_postb_arr[ii], int.Parse(timeout));
                                        httpPost.PostItems.Add("nfe_ide_nnf", numeros);
                                        httpPost.PostItems.Add("serie", series);
                                        httpPost.PostItems.Add("statusdanota", status);
                                        httpPost.PostItems.Add("tentativasDeInclusao", tentativas);
                                        httpPost.PostItems.Add("chave_nfe", chaves);
                                        httpPost.PostItems.Add("retEnviNFe_cStat", cstats);
                                        httpPost.PostItems.Add("retEnviNFe_xMotivo", motivos);
                                        httpPost.PostItems.Add("url_danfe", urlsDanfe);
                                        httpPost.PostItems.Add("tp_amb", tp_amb);
                                        httpPost.PostItems.Add("pedido_num", pedido_num);
                                        httpPost.Type = PostSubmitter.PostTypeEnum.Post;
                                        httpPost.Post();

                                        Log.registrarInfo("Notas enviadas via postback com sucesso para a empresa " + empresa_cnpj, Servico);
                                        Log.registrarInfo("  -> Dados:Números - " + numeros + "/ Series - " + series + "/ Pedidos - " + pedido_num + " / Link- " + empresa_postb_arr[ii], Servico);
                                    };
                                        numeros = string.Empty;
                                        series = string.Empty;
                                        status = string.Empty;
                                        tentativas = string.Empty;
                                        chaves = string.Empty;
                                        cstats = string.Empty;
                                        motivos = string.Empty;
                                        urlsDanfe = string.Empty;
                                        resultado = string.Empty;
                                        empresa_cnpj = string.Empty;
                                        empresa_postb = string.Empty;
                                        pedido_num = string.Empty;
                                };
                            }
                            else
                            {
                                for (int ii = 0; ii < empresa_postb_arr.Length; ii++)
                                {
                                    var httpPost = new PostSubmitter(empresa_postb_arr[ii], int.Parse(timeout));

                                    httpPost.PostItems.Add("nfe_ide_nnf", numeros);
                                    httpPost.PostItems.Add("serie", series);
                                    httpPost.PostItems.Add("statusdanota", status);
                                    httpPost.PostItems.Add("tentativasDeInclusao", tentativas);
                                    httpPost.PostItems.Add("chave_nfe", chaves);
                                    httpPost.PostItems.Add("retEnviNFe_cStat", cstats);
                                    httpPost.PostItems.Add("retEnviNFe_xMotivo", motivos);
                                    httpPost.PostItems.Add("url_danfe", urlsDanfe);
                                    httpPost.PostItems.Add("tp_amb", tp_amb);
                                    httpPost.PostItems.Add("pedido_num", pedido_num);
                                    httpPost.Type = PostSubmitter.PostTypeEnum.Post;
                                    httpPost.Post();

                                    Log.registrarInfo("Notas enviadas via postback com sucesso para a empresa " + empresa_cnpj, Servico);
                                    Log.registrarInfo("  -> Dados:Números - " + numeros + "/ Series - " + series + "/ Pedidos - " + pedido_num + " / Link- " + empresa_postb_arr[ii], Servico);
                                };

                                numeros = string.Empty;
                                series = string.Empty;
                                status = string.Empty;
                                tentativas = string.Empty;
                                chaves = string.Empty;
                                cstats = string.Empty;
                                motivos = string.Empty;
                                urlsDanfe = string.Empty;
                                resultado = string.Empty;
                                empresa_cnpj = string.Empty;
                                empresa_postb = string.Empty;
                                pedido_num = string.Empty;
                            };

                        }
                    }
                    catch (Exception exception)
                    {
                        resultado = "Erro ao tentar fazer o postback para a empresa " + empresa_cnpj + ": " +  exception.Message;

                        Log.registrarErro(resultado, Servico);
                        Log.registrarInfo("  -> Dados:Números " + numeros + "/" + series + "/ Pedidos - " + pedido_num + " - Status- " + status + " - Tentativas - " + tentativas + " - Chaves- " + chaves + " - cStats- " + cstats + " - Motivos- " + motivos + " - urlDanfe- " + urlsDanfe + " - tp_amb- " + tp_amb + " - Link- " + empresa_postb, Servico);
                    }
                    finally
                    {


                    } 
                }

        }

        private static void InserirHistorico(string tipo, string texto, notaVO nota)
        {
            var historico = new historicoVO(tipo, texto, int.Parse(nota.NFe_ide_nNF.ToString()), nota.NFe_emit_CNPJ, nota.serie);

            notas.inserirHistorico(historico);
        }
    }
}
