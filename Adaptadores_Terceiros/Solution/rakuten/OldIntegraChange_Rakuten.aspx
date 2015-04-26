<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OldIntegraChange_Rakuten.aspx.vb" Inherits="Solution.IntegraChange_Rakuten" %>

<%@ Import Namespace ="Solution" %>
<%@ Import Namespace ="System" %>
<%

    Dim proxy As Solution.Pedidos_hm.OrderServiceSoapClient = New Solution.Pedidos_hm.OrderServiceSoapClient("OrderServiceSoap")
    
    Dim pedidosEmpresa = PedidosDAO.pegaPedidos("0000", 123456)
    
    '  If Funcs.validaEmpresa("11240161000179", "961994351510911317124414110695153162119143") Then
            
    Dim n As Integer
    Dim orderList(pedidosEmpresa.Count) As Decimal
    
    For n = 0 To pedidosEmpresa.Count - 1
        orderList(n) = pedidosEmpresa(n).num_pedido
    Next n
             
    Try
        Dim retorno = proxy.IsIntegratedNewOrder(orderList, "15773074000138", "901cea3a-28a8-4cba-bf58-de6f2d973a38", "444F9356-AE66-4AC5-BCF1-BF30384E2ED7")
          
        For n = 0 To pedidosEmpresa.Count - 1
            PedidosDAO.baixarPedido(pedidosEmpresa(n).num_pedido, pedidosEmpresa(n).NFe_emit_CNPJ, pedidosEmpresa(n).fk_id_plataforma)
        Next n
                      
    Catch ex As Exception
        Response.Write(Request.QueryString("callback") & "([Erro na conexao: " & ex.Message & "]);")
    End Try
                
    '  Else
    'MsgBox("Não Validou!")
    ' End If
    %>
