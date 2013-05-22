Imports FN4Common
Imports System.Xml
Imports FN4Common.Helpers

Public Class Form1
    Private url As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        url = "https://homologacao.nfe.sefazvirtual.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx"
        pesquisar()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        url = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeconsulta.asmx"
        pesquisar()
    End Sub

    Private Sub pesquisar()
        Dim naoProcessadas As List(Of notaVO) = notaDAO.obterNotas

        Dim nota As notaVO
        For Each nota In naoProcessadas

            Dim consNfe As New XmlDocument
            consNfe.Load(System.AppDomain.CurrentDomain.BaseDirectory() & "XML\consSitNfe.xml")

            'carrega o cabecalho da mensagem
            Dim cabecMsg As XmlDocument = XmlHelper.obterUmCabecalho(consNfe.SelectSingleNode("/*[local-name()='consSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/@versao").InnerXml)

            'acertar o ambiente
            consNfe.SelectSingleNode("/*[local-name()='consSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = Geral.Parametro("ambienteDeExecucao")

            'adicionar a chave de acesso
            consNfe.SelectSingleNode("/*[local-name()='consSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='chNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = nota.NFe_infNFe_id

            Dim retorno As String
            Dim ws As New NFe.NfeConsulta.NfeConsulta


            ws.Url = url
            ws.ClientCertificates.Add(FN4Common.Geral.Certificado)

            Try
                'Log.registrarInfo("Enviando nota" & nota.NFe_ide_nNF & " para " & ws.Url, "RetornoService")
                Application.DoEvents()
                retorno = ws.nfeConsultaNF(cabecMsg.InnerXml, consNfe.InnerXml)
                'Log.registrarInfo("Recebido o retorno " & vbCrLf & retorno, "RetornoService")
                ws.Dispose()
                ws = Nothing
            Catch ex As Exception
                Throw ex
            End Try

            Try
                Dim xmlRetorno As New XmlDocument
                xmlRetorno.LoadXml(retorno)
                xmlRetorno.PreserveWhitespace = True

                Dim cstat As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                Dim xmotivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText

                TextBox1.Text += "Consulta a nota " & nota.NFe_infNFe_id & " - " & cstat & " - " & xmotivo
                If cstat = "100" Then
                    If Not nota.statusDaNota = 21 Then
                        nota.statusDaNota = 22
                    End If
                    nota.retEnviNFe_xMotivo = xmotivo
                    nota.protNfe_nProt = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                    notaDAO.alterarNota(nota)
                ElseIf cstat = "101" Then
                    nota.statusDaNota = 4
                    nota.retEnviNFe_xMotivo = xmotivo
                    nota.protNfe_nProt = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                    notaDAO.alterarNota(nota)
                End If
                TextBox1.Text += vbCrLf & nota.NFe_ide_nNF & " - Status = " & nota.retEnviNFe_cStat & " - " & nota.retEnviNFe_xMotivo & vbCrLf
                Application.DoEvents()


            Catch ex As Exception
                TextBox1.Text += vbCrLf & nota.NFe_ide_nNF & " - Erro na consulta"
            End Try

        Next
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
