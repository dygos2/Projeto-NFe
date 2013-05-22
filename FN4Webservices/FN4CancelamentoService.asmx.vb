Imports System.Web.Services
Imports System.ComponentModel
Imports FN4Common
Imports System.Xml
Imports FN4Common.Helpers

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://fisconet.com.br/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class FN4CancelamentoService
    Inherits System.Web.Services.WebService


    <WebMethod()> _
  Public Function cancelarNota(ByVal nNf As Integer, ByVal justificativa As String, ByVal CNPJ As String) As String
        Try
            Dim nota As notaVO
            Dim retorno As String

            Dim ht As New Hashtable
            ht.Add("numeroDaNota", nNf)
            ht.Add("CNPJEmitente", CNPJ)

            nota = IBatisNETHelper.Instance.QueryForObject("obterNotaPorNumeroCNPJ", ht)

            'cancelar
            retorno = cancelarNFe(nota, justificativa)

            Return retorno

        Catch ex As Exception
            Log.registrarErro("Erro de cancelamento - " & ex.Message & vbCrLf & ex.StackTrace, "CancelamentoService")
            Return "Ocorreu um erro no cancelamento, contate o suporte. (" & ex.Message & "-" & ex.StackTrace & ")"
        End Try
    End Function

    Private Function cancelarNFe(ByVal nota As notaVO, ByVal justificativa As String) As String
        Try
            Log.registrarInfo("Solicitando cancelamento", "CancelamentoService")
            'gerar o xml de cancelamento
            Dim cancNFe As New XmlDocument
            cancNFe.Load(Server.MapPath(".") & "/XML/cancNFe.xml")

            '       carrega o cabecalho da mensagem
            '       TODO Criar parâmetros de versão
            Dim cabecMsg As New System.Xml.XmlDocument
            cabecMsg.Load(Server.MapPath(".") & "/XML/cabecMsg.xml")

            'colocar o id
            cancNFe.SelectSingleNode("/*[local-name()='cancNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/@Id").InnerText = "ID" & nota.NFe_infNFe_id
            cancNFe.SelectSingleNode("/*[local-name()='cancNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = Geral.Parametro("ambienteDeExecucao")
            cancNFe.SelectSingleNode("/*[local-name()='cancNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='chNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = nota.NFe_infNFe_id
            cancNFe.SelectSingleNode("/*[local-name()='cancNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = nota.protNfe_nProt
            cancNFe.SelectSingleNode("/*[local-name()='cancNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infCanc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xJust' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = justificativa

            'assinar cancelamento
            cancNFe = XmlHelper.assinarNFeXML(cancNFe, cancNFe.GetElementsByTagName("infCanc")(0).Attributes.ItemOf("Id").InnerText)

            'cria o webservice
            Dim ws As New NFe.Cancelamento.NfeCancelamento
            ws.Url = Geral.Parametro("servicoCancelamento")

            ws.ClientCertificates.Add(Geral.Certificado)

            Dim strRetorno As String

            Try
                Log.registrarInfo("Enviando cancelamento: " & vbCrLf & cancNFe.InnerXml, "CancelamentoService")
                inserirHistorico(19, "", nota)
                'envia
                strRetorno = ws.nfeCancelamentoNF(cabecMsg.InnerXml, cancNFe.InnerXml)
                ws.Dispose()
                ws = Nothing
                Log.registrarInfo("Recebido o retorno: " & vbCrLf & strRetorno, "CancelamentoService")
            Catch ex As Exception
                inserirHistorico(21, ex.Message, nota)
                Throw ex
            End Try

            Dim xmlRetorno As New XmlDocument
            xmlRetorno.LoadXml(strRetorno.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))

            'atualizar o status no banco 
            If xmlRetorno.SelectSingleNode("/retCancNFe/infCanc/cStat").InnerText = "101" Then
                inserirHistorico(20, "", nota)
                nota.statusDaNota = 4
                IBatisNETHelper.Instance.Update("alterarNota", nota)
                Return xmlRetorno.SelectSingleNode("/retCancNFe/infCanc/xMotivo").InnerText
            Else
                inserirHistorico(21, xmlRetorno.SelectSingleNode("/retCancNFe/infCanc/xMotivo").InnerText, nota)
                Return xmlRetorno.SelectSingleNode("/retCancNFe/infCanc/xMotivo").InnerText
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub inserirHistorico(ByVal tipo As String, ByVal descricao As String, ByVal nota As notaVO)
        Dim hist As New historicoVO(tipo, descricao, nota.NFe_ide_nNF, nota.NFe_emit_CNPJ, nota.serie)
        IBatisNETHelper.Instance.Insert("inserirHistorico", hist)

        Retorno.escreverRetorno(nota, DateTime.Now, tipo, descricao)
    End Sub
End Class