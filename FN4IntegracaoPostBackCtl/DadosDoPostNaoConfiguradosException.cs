using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FN4IntegracaoPostBackCtl
{
    public class DadosDoPostNaoConfiguradosException : Exception
    {
        public string Campos { get; set; }
        
        public DadosDoPostNaoConfiguradosException(string mensagem, IEnumerable<string> campos) : base(message:mensagem)
        {
            foreach (var campo in campos)
            {
                if (string.IsNullOrWhiteSpace(Campos))
                {
                    this.Campos = campo;
                }
                else
                {
                    this.Campos += ", " + campo;
                }
            }

            mensagem = campos.Count() == 1 ? "Erro: um campo necessário para fazer o post não foi configurado: {0}" : "Erro: alguns campos necessários para fazer o post não foram configurados: {0}";

            string.Format(mensagem, this.Campos);
        }
    }
}
