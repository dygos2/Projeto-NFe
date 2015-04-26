Imports Solution
Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Data
Imports System.Xml
Imports Solution.Log
Imports log4net
Imports Solution.Funcs
Imports Solution.Pedidos_ItensDAO
Public Class WebSincronia
    Public Shared Function SincronizaPedido(ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal login As String, ByVal senha As String) As ObjReturn
        Dim objRetorno As ObjReturn = New ObjReturn
        Dim proxy As Solution.wsciashop.wsIntegracaoSoapClient = New Solution.wsciashop.wsIntegracaoSoapClient()
        Dim pedidosEmpresa = PedidosDAO.pegaPedidos(cnpj, id_plataforma)
        Log.CriaLog("Sincronização de pedidos")

        Dim xmlEnvia As String
        Dim receiptPedidos As String
        Dim n As Integer

        For n = 0 To pedidosEmpresa.Count - 1
            receiptPedidos = receiptPedidos + "<receipt_result xmlns="""" order_id=""" + pedidosEmpresa(n).num_pedido + """ processed=""1"" />"
        Next n

        xmlEnvia = "<?xml version=""1.0"" encoding=""ISO-8859-1"" standalone=""no"" ?>" +
                    "<receipt_resultList xmlns=""https://wssmb.ciashop.com.br/WSIntegSMB/dsReceipt.xsd"">" +
                    receiptPedidos +
                    "</receipt_resultList>"

        Try
            If proxy.ConfirmaPedidos(login, senha, xmlEnvia) Then
                For n = 0 To pedidosEmpresa.Count - 1
                    PedidosDAO.baixarPedido(pedidosEmpresa(n).num_pedido, pedidosEmpresa(n).NFe_emit_CNPJ, pedidosEmpresa(n).fk_id_plataforma)
                Next n
            Else
                objRetorno.erro = "Falha na sincronização"
            End If

            objRetorno.sincronizados = n

            Return objRetorno
        Catch ex As Exception
            objRetorno.erro = "Erro ao baixar os pedidos, favor abrir chamado técnico com a NFE4WEB, informando o problema." + CStr(ex.Message)
            Return objRetorno
        End Try



    End Function

End Class
