Imports System.Xml.Schema
Imports FN4Common.DataAccess
Imports FN4Common
Imports System.Xml
Imports FN4Common.Helpers
Imports System.IO
Imports System.Xml.Xsl



Public Class Form1
    Public Sub chama()
        'Dim empresa As empresaVO = empresaDAO.obterEmpresa("10979982000169", String.Empty)
        Dim todasempresas As List(Of empresaVO) = empresaDAO.obterTodasEmpresas
        '       Dim nota As notaVO
        '        Dim empresa As empresaVO
        MsgBox(todasempresas.Count)
        'Dim pathXmlConsultaCanonico = "C:\New folder\1.xml" 'notaun.pastaDeTrabalho
        'TextBox1.Text += "inicio" & vbCrLf

        Dim objWriter As New System.IO.StreamWriter("C:\arquivo.txt")

        For Each empresa In todasempresas


            'TextBox1.Select(TextBox1.Text.Length - 1, 0)
            'TextBox1.ScrollToCaret()

            TextBox1.Text = "Lendo cliente " & empresa.nome & vbCrLf
            'TextBox1.Text = TextBox1.Text & "==========================================" & vbCrLf
            objWriter.WriteLine("Lendo cliente " & empresa.nome)
            objWriter.WriteLine("==========================================")

            Application.DoEvents()

            Dim todasnotas As List(Of notaVO) = notas.obterNotasdoCnpj(empresa.cnpj)
            'TextBox1.Text = TextBox1.Text & "==========================================" & vbCrLf
            'TextBox1.Text = TextBox1.Text & todasnotas.Count & " notas na base" & vbCrLf
            objWriter.WriteLine("==========================================")
            objWriter.WriteLine(todasnotas.Count & " notas na base")

            For Each notaun In todasnotas
                Dim pathXmlConsultaCanonico = notaun.pastaDeTrabalho & notaun.NFe_ide_nNF & "_procNFe.xml"
                TextBox1.Text = TextBox1.Text & "Lendo nota " & notaun.NFe_ide_nNF & vbCrLf
                'TextBox1.Text = TextBox1.Text & "==========================================" & vbCrLf
                objWriter.WriteLine("Lendo nota " & notaun.NFe_ide_nNF)
                objWriter.WriteLine("==========================================")

                Application.DoEvents()
                Try
                    Dim envConsulta As New XmlDocument
                    envConsulta.Load(pathXmlConsultaCanonico)
                    Dim rtesp_det As XmlNodeList = envConsulta.GetElementsByTagName("det")
                    Dim cfop1 As String
                    cfop1 = ""
                    objWriter.WriteLine("Entrou no arquivo - " & rtesp_det.Count & " itens")
                    TextBox1.Text = TextBox1.Text & "Entrou no arquivo - " & rtesp_det.Count & " itens" & vbCrLf
                    For Each detalhe In rtesp_det
                        Dim cfop As String = detalhe.GetElementsByTagName("prod")(0).GetElementsByTagName("CFOP")(0).innertext
                        If cfop1 <> cfop And cfop1 <> "" Then
                            'TextBox1.Text = TextBox1.Text & "ERRO: O CFOP - " & cfop & " é diferente do CFOP " & cfop1 & vbCrLf
                            objWriter.WriteLine("ERRO: O CFOP - " & cfop & " é diferente do CFOP " & cfop1)
                        Else
                            cfop1 = cfop
                        End If
                        Application.DoEvents()
                    Next
                    notaun.cfop = cfop1
                    notaDAO.alterarNota(notaun)

                    Application.DoEvents()
                Catch ex As Exception
                    'TextBox1.Text = TextBox1.Text & "Erro no arquivo - " & pathXmlConsultaCanonico & " - " & ex.Message & vbCrLf
                    objWriter.WriteLine("Erro no arquivo - " & pathXmlConsultaCanonico & " - " & ex.Message)
                    Application.DoEvents()
                    Continue For
                End Try
            Next
        Next
        objWriter.Close()
        'TextBox1.Text = TextBox1.Text & "Fim"
        MsgBox("Fim")
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        chama()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
