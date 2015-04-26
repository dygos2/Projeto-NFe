<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OldRetrieveNewOrders_rakuten.aspx.vb" Inherits="Solution.RetrieveNewOrders_rakuten" %>

<%@ Import Namespace="Solution" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Xml" %>
<%@ import Namespace="Log.vb" %>    
<%@ Import Namespace = "log4net" %>
<%@ import Namespace="Funcs.vb" %>   
<%
    
    Dim doc As XmlDocument = New XmlDocument()
    Dim proxy As Solution.Pedidos_hm.OrderServiceSoapClient = New Solution.Pedidos_hm.OrderServiceSoapClient("OrderServiceSoap")
  
    'Dim doc As XmlDocument = New XmlDocument()
    If Funcs.validaEmpresa("11240161000179", "961994351510911317124414110695153162119143") Then
        
        Dim ped = proxy.RetrieveNewOrders("15773074000138", "901cea3a-28a8-4cba-bf58-de6f2d973a38", "444F9356-AE66-4AC5-BCF1-BF30384E2ED7", "0")
        Dim p As Integer
        For p = 0 To ped.Count - 1
            Dim pedido As New PedidosVO
            pedido.NFe_emit_CNPJ = ped(p).MerchantCNPJ
            pedido.num_pedido = ped(p).Code
            pedido.xName = ped(p).Customer.DisplayName
            pedido.xLgr = ped(p).ShipAddress.Street
            pedido.xMun = ped(p).ShipAddress.City
            pedido.UF = ped(p).ShipAddress.State
            pedido.CEP = Replace(ped(p).ShipAddress.ZipCode, "-", "")
            pedido.fone = Replace(ped(p).ShipAddress.Phones(0).Number, "-", "")
            pedido.CPF = ped(p).Customer.DisplayDocument
            pedido.nro = ped(p).ShipAddress.Number
            pedido.xCpl = ped(p).ShipAddress.Complement
            pedido.xBairro = ped(p).ShipAddress.District
            pedido.xProcDate = ped(p).ShipAddress.SaveDate
            Try
                PedidosDAO.inserirPedido(pedido)
            Catch ex As Exception
                PedidosDAO.alterarPedido(pedido)
            End Try
            
            Dim i As Integer
            For i = 0 To ped(p).OrderItems.Products.Count - 1
                Dim pedItem As New Pedidos_ItensVO
                pedItem.NFe_emit_CNPJ = ped(p).MerchantCNPJ
                pedItem.num_pedido = ped(p).Code
                pedItem.cProd = ped(p).OrderItems.Products(i).Code.ToString
                pedItem.xProd = ped(p).OrderItems.Products(i).Name.ToString
                pedItem.vUnCom = ped(p).OrderItems.Products(i).SKUs(i).RealPrice
                'pedItem.qCom = ped(p).OrderItems.Products(i).Store(i). 
                'pedItem.NCM = ped(p).OrderItems.Products(i).SKUs(i).
         
                Try
                    Pedidos_ItensDAO.inserirPedido_Item(pedItem)
                Catch ex As Exception
                    Pedidos_ItensDAO.alterarPedido_Item(pedItem)
                End Try
                
            Next i
        Next p
        
        '   proxy.IsIntegratedNewOrder(, ped(0).MerchantCNPJ, "901cea3a-28a8-4cba-bf58-de6f2d973a38", "444F9356-AE66-4AC5-BCF1-BF30384E2ED7")
        
    End If
        
 
    %>