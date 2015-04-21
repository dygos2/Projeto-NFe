<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="upload_eventos.aspx.vb" Inherits="FN4MonitorWS.upload_eventos" %><%@ Import Namespace="System.Net" %><%@ Import Namespace="System.IO" %><%@ Import Namespace="FN4Common" %><html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="Application/X-unknown" />
<script type="text/javascript">

   <%            
            Dim file1, file2, cnpj, ano, mes, dia, nfe, serie, pasta_atual, email As String, tp, seq
            
            cnpj = Request.QueryString("cnpj")
            ano = Request.QueryString("ano")
            mes = Request.QueryString("mes")
            dia = Request.QueryString("dia")
            nfe = Request.QueryString("nfe")
            serie = Request.QueryString("serie")
            pasta_atual = Request.QueryString("pasta_atual")
	    tp = Request.QueryString("tp")
	    seq = Request.QueryString("seq")

            file1 = "process/" & cnpj & "/" & ano & "/" & mes & "/" & dia & "/" & nfe & "-" & serie & "/evento_" & seq & "_" & tp & ".pdf"
     %>
          window.location.href = "<% response.write(file1) %>"
 </script>
</head>
</html>