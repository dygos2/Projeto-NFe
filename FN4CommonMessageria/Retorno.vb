Imports System.IO

Public Class Retorno
    Public Shared Sub escreverRetorno(ByVal nota As notaVO, ByVal dataStatus As Date, ByVal idProcessamento As Integer, ByVal dsProcessamento As String)
        Try
            'salvar o txt transformado na pasta de trabalho
            If Directory.Exists(Geral.Parametro("pastaDeSaida")) Then
                Dim escrita As New StreamWriter(Geral.Parametro("pastaDeSaida") & nota.NFe_emit_CNPJ & "_" & nota.NFe_ide_nNF & "_" & nota.serie & ".LOG", True, System.Text.Encoding.GetEncoding("ISO-8859-1"))
                escrita.WriteLine(dataStatus.ToString & "|" & nota.statusDaNota & "|" & nota.statusDaNotaString & "|" & idProcessamento & "|" & dsProcessamento & "|" & IIf(nota.NFe_infNFe_id <> "", nota.NFe_infNFe_id, "Chave de acesso não gerada"))
                escrita.Flush()
                escrita.Close()
            Else
                Directory.CreateDirectory(Geral.Parametro("pastaDeSaida"))
                Dim escrita As New StreamWriter(Geral.Parametro("pastaDeSaida") & nota.NFe_emit_CNPJ & "_" & nota.NFe_ide_nNF & "_" & nota.serie & ".LOG", True, System.Text.Encoding.GetEncoding("ISO-8859-1"))
                escrita.WriteLine(dataStatus.ToString & "|" & nota.statusDaNota & "|" & nota.statusDaNotaString & "|" & idProcessamento & "|" & dsProcessamento & "|" & IIf(nota.NFe_infNFe_id <> "", nota.NFe_infNFe_id, "Chave de acesso não gerada"))
                escrita.Flush()
                escrita.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
      

    End Sub
End Class
