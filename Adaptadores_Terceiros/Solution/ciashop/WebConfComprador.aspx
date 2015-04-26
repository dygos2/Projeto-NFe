<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebConfComprador.aspx.vb" Inherits="Solution.WebConfComprador" %>
<%@ Import Namespace="System.Xml" %>
    <% 
        Response.ContentType = "application/json; charset=utf-8" 
        Try
                                                                                                                    
            Dim xmlload As Xml
            Dim doc As XmlDocument = New XmlDocument()
            doc.LoadXml("<item><name>wrench</name></item>")
                                                                                                                    
            Dim proxy As Solution.wsciashop.wsIntegracaoSoapClient = New Solution.wsciashop.wsIntegracaoSoapClient()
            'Dim itens As Pedidos.PedidoItem = New Pedidos.PedidoItem()
            Dim xmldoc As String
            xmldoc = "<?xml version=""1.0"" encoding=""ISO-8859-1"" standalone=""no""?>" +
                       "     <shopper_resultList xmlns=""https://wssmb.ciashop.com.br/WSIntegSMB/dsReceipt.xsd"">" +
                       "         <shopper_result xmlns="""" shopper_id=""1"" processed=""1"" />" +
                       "</shopper_resultList>"
            Dim retorno As Integer
            
            '
            'pass = Request.QueryString("token")
            'user = "svcmacrotecs"
            'pass = "$vcm@cr0tecs"

            'proxy.ClientCredentials.HttpDigest.ClientCredential.UserName = User
            'proxy.ClientCredentials.HttpDigest.ClientCredential.Password = pass
            'proxy.ClientCredentials.UserName.UserName = "101810"
            'proxy.ClientCredentials.UserName.Password = "integra.2008"
                                                                                                                
            retorno = proxy.ConfirmaCompradores("101810", "integra.2008", xmldoc)
            MsgBox(xmldoc)
                                                                                                                    
    
        Catch ex As Exception
            Response.Write(Request.QueryString("callback") & "([Erro na conexao: " & ex.Message & "]);")
        Finally
            ' Verifica se o proxy não está fechado ou com erro e chama o close para fechar a conexão
        End Try


    %>                                                                                                                
                





