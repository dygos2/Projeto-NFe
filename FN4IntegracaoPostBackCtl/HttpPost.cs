using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FN4IntegracaoPostBackCtl
{
    public class HttpPost
    {
        public string Url { get; set; }
        public string Dados { get; set; }
        
        public HttpPost()
        {
            this.Url = string.Empty;
            this.Dados = string.Empty;
        }
        
        public HttpPost(string url)
        {
            this.Url = url;
        }

        public HttpPost(string url, string dados)
        {
            this.Url = url;
            this.Dados = dados;
        }

        public ResultadoPost ExecutarPost()
        {
            var camposVazios = new List<string>();
            
            if (string.IsNullOrEmpty(this.Url))
            {
                camposVazios.Add("Url");
            }

            if (string.IsNullOrWhiteSpace(this.Dados))
            {
                camposVazios.Add("Dados");
            }

            if (camposVazios.Count > 0)
            {
                throw new DadosDoPostNaoConfiguradosException(string.Empty, camposVazios);
            }

            try
            {
                var request = WebRequest.Create(this.Url);

                ((HttpWebRequest)request).UserAgent = "Fisconet 5 - Serviço de Integração";

                request.Method = "POST";

                var arrayDeDados = Encoding.UTF8.GetBytes(this.Dados);

                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = arrayDeDados.Length;

                var dataStream = request.GetRequestStream();

                dataStream.Write(arrayDeDados, 0, arrayDeDados.Length);

                dataStream.Close();

                var resposta = request.GetResponse();

                var textoResposta = string.Empty;

                using (var sr = new StreamReader(resposta.GetResponseStream()))
                {
                    textoResposta = sr.ReadToEnd();
                }

                return new ResultadoPost { Mensagem = ((HttpWebResponse)resposta).StatusDescription, Status = StatusPost.Sucesso};
            }
            catch (Exception exception)
            {
                return new ResultadoPost { Status = StatusPost.Falha, Mensagem = exception.Message };
            }
        }
    }

    public class ResultadoPost
    {
        public StatusPost Status { get; set; }
        public String Mensagem { get; set; }
    }

    public enum StatusPost
    {
        Sucesso,
        Falha
    }
}
