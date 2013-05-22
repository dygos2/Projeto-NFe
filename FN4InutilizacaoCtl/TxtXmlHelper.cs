using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace FN4InutilizacaoCtl
{
    public class TxtXmlHelper
    {
        private static StringBuilder _resultadoValidacao;

        public static string ValidarXmlDeInutilizacao(string pathXmlEnvio)
        {
            var meuEvento = new ValidationEventHandler(EventoDeValidacao);

            var pathXsd = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XSD\inutNFe_v2.00.xsd");

            var schema = XmlSchema.Read(new XmlTextReader(pathXsd), meuEvento);

            var xSettings = new XmlReaderSettings
                                {
                                    ValidationType = ValidationType.Schema
                                };

            xSettings.Schemas.Add(schema);

            xSettings.ValidationEventHandler += meuEvento;

            using (var xReader = XmlReader.Create(pathXmlEnvio, xSettings))
            {
                while (xReader.Read())
                {
                    
                }
            }

            return _resultadoValidacao.ToString();
        }

        public static void EventoDeValidacao(object sender, ValidationEventArgs e)
        {
            _resultadoValidacao = new StringBuilder();
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    _resultadoValidacao.AppendLine("Erro(linha " + e.Exception.LineNumber + " posição " +
                                                  e.Exception.LinePosition + "): " + e.Message);
                    _resultadoValidacao.AppendLine("---");
                    break;
                case XmlSeverityType.Warning:
                    _resultadoValidacao.AppendLine("Alerta: " + e.Message);
                    _resultadoValidacao.AppendLine("---");
                    break;
            }
        }
    }
}
