Imports Solution

Public Class ChangeOrderEC
    Public Shared Function SincronizaPedido(ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal chavePedido As String, ByVal codLoja As String) As ObjReturn
        Dim proxy As wsRakutenEC.PedidoSoapClient = New wsRakutenEC.PedidoSoapClient("PedidoSoap")
        Dim objRetorno As ObjReturn = New ObjReturn
        Dim pedidosEmpresa = PedidosDAO.pegaPedidos(cnpj, id_plataforma)

        'Dieta Dukan
        'chavePedido = "901cea3a-28a8-4cba-bf58-de6f2d973a38"
        'codLoja = "444F9356-AE66-4AC5-BCF1-BF30384E2ED7"

        Dim n As Integer
        Dim request As New wsRakutenEC.AlterarStatusRequest
        Dim orderList(pedidosEmpresa.Count) As Decimal

        For n = 0 To pedidosEmpresa.Count - 1
            request.LojaCodigo = 0
            request.CodigoPedido = CInt(pedidosEmpresa(n).num_pedido)
            request.CodigoInternoPedido = CInt(pedidosEmpresa(n).num_pedido)
            request.PedidoStatus = 17
            request.StatusInternoPedido = "17"
            request.ObjetoSedex = ""
            request.NotaFiscal = 0
            request.Serie = "1"
            Dim retorno = proxy.wsRakutenEC_PedidoSoap_AlterarStatus(request)
        Next n

        Try
            For n = 0 To pedidosEmpresa.Count - 1
                PedidosDAO.baixarPedido(pedidosEmpresa(n).num_pedido, pedidosEmpresa(n).NFe_emit_CNPJ, pedidosEmpresa(n).fk_id_plataforma)
            Next n
            objRetorno.sincronizados = n
            Return (objRetorno)
        Catch ex As Exception
            objRetorno.erro = "Erro ao baixar os pedidos, favor abrir chamado técnico com a NFE4WEB, informando o problema." + CStr(ex.Message)
            Return objRetorno
        End Try
    End Function



End Class
