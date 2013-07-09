<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="upload_eventos.aspx.vb" Inherits="FN4MonitorWS.upload_eventos" %><%@ Import Namespace="System.Net" %><%@ Import Namespace="System.IO" %><%@ Import Namespace="FN4Common" %><html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="Application/X-unknown" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%
        
        Try
            Dim uriString As String = Geral.Parametro("servidor_pdf_eventos") '"http://72.167.54.28/index_cce.php"
            Dim drive As String = Geral.Parametro("pastaDeProcessadas")
            Dim path_pdf As String
            
            'todo: tirar
            'uriString = "http://amazon-nfe4web.elasticbeanstalk.com/nfe4web/gera_pdfs/index_cce.php"
            'drive = "C:\temp\"
            
            Dim arr_path As Array
            arr_path = drive.Split("\")
            
            ' Create a new WebClient instance
            Dim myWebClient As New WebClient()
            'Console.WriteLine(ControlChars.Cr + "Please enter the data to be posted to the URI {0}:", uriString)
            
            Dim file1, file2, cnpj, ano, mes, dia, nfe, serie, pasta_atual, email, seq, tp As String
            
            cnpj = Request.QueryString("cnpj")
            ano = Request.QueryString("ano")
            mes = Request.QueryString("mes")
            dia = Request.QueryString("dia")
            nfe = Request.QueryString("nfe")
            serie = Request.QueryString("serie")
            pasta_atual = Request.QueryString("pasta_atual")
            email = Request.QueryString("email")
            seq = Request.QueryString("seq")
            tp = Request.QueryString("tp")
            
            path_pdf = drive & "evento_" & seq & "_" & tp & ".pdf"
                        
            file1 = arr_path(0) & "\Fisconet5\" & pasta_atual & "\process\" & cnpj & "\" & ano & "\" & mes & "\" & dia & "\" & nfe & "-" & serie & "\" & nfe & "_procNFe.xml"
            'TODO: tirar
            'file1 = "C:\temp\123_procNFe.xml"
            
            file2 = "C:\Fisconet5\" & pasta_atual & "process\" & cnpj & "\" & ano & "\" & mes & "\" & dia & "\" & nfe & "-" & serie & "\proc_evento_" & seq & "_" & tp & ".xml"
            'TODO: tirar
            'file2 = "C:\temp\proc_evento_2_110110.xml"
            'email = "1"
            'nfe = 1
            'serie = 1
                        
            'file1 = "c:/Fisconet5/procNFe.xml"
            'file2 = "c:/Fisconet5/retornoConsultaProt.xml"
            '&cnpj=09399526000160&ano=2012&mes=7&dia=5&nfe=93&serie=1
            'file1=C:\Fisconet5\5_procNFe.xml&file2=C:\Fisconet5\Fisconet5_Homologacao2\process\08352434000162\2012\3\30\1-10\3_retornoConsultaProt.xml
            
            Dim postData As String = file1 '"C:\Fisconet5\Fisconet5_Homologacao2\process\08352434000162\2012\3\30\1-10\3_procNFe.xml"
                        
            ' Apply ASCII Encoding to obtain the string as a byte array.
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)
            Dim responseArray As Byte() = myWebClient.UploadFile(uriString, postData)
            Dim txt_ret As String = Encoding.ASCII.GetString(responseArray)

            postData = file2 '"C:\Fisconet5\Fisconet5_Homologacao2\process\08352434000162\2012\3\30\1-10\3_retornoConsultaProt.xml"

            ' Apply ASCII Encoding to obtain the string as a byte array.
            byteArray = Encoding.ASCII.GetBytes(postData)
            responseArray = myWebClient.UploadFile(uriString & "?file1=" & txt_ret & "&email=" & email, postData)
            
            Dim FS As New FileStream(path_pdf, FileMode.CreateNew, FileAccess.ReadWrite)
            FS.Write(responseArray, 0, responseArray.Length)
            FS.Close()
            
            ' comentando, pois agora irá salvar na pasta
            
            'Response.BufferOutput = True
            'Response.ClearContent()
            'Response.ClearHeaders()
            'Response.AddHeader("cache-control", "max-age=1")
            'Response.AddHeader("Cache-Control", "private")
            
            'Response.ContentType = "Application/X-unknown"
            'Response.AddHeader("content-length", responseArray.Length.ToString())
            'Response.AppendHeader("Content-Disposition", "attachment; filename=cce_eventos_" & seq & "-" & tp & ".pdf")
            'Response.AppendHeader("Accept-Ranges", "none")
            
            ''Response.TransmitFile(responseArray)
            'Response.BinaryWrite(responseArray.ToArray())
            'Response.Flush()
            'Response.End()
            
            'HttpContext.Current.ApplicationInstance.CompleteRequest()
            'Response.Write(Encoding.ASCII.GetString(responseArray))
            Response.Write("PDF salvo em " & path_pdf)
        Catch ex As System.Net.WebException
            Throw ex
            'Error in accessing the resource, handle it 
        End Try

     %>
    </div>
    </form>
</body>
</html>
