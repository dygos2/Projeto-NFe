Imports Solution
Imports System.Web.Services.Protocols.SoapHttpClientProtocol
Imports System.Diagnostics
Imports System.Xml
Imports System.Data
Imports System.ServiceModel
Imports Solution.Log
Imports log4net
Imports Solution.Funcs
Imports Solution.Pedidos_ItensDAO
Imports System.Security



Public Class fWSImportaPedidos

    Public Shared Function NovosPedidos(ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal idLoja As Integer, ByVal login As String, ByVal senha As String) As ObjReturn

        Dim proxy As wsTrayVirtJo.TrayWebServicewsdl = New wsTrayVirtJo.TrayWebServicewsdl()



        Dim objRetorno As ObjReturn = New ObjReturn

        Dim credential As New System.Net.NetworkCredential(login, senha)
        proxy.Credentials = credential

        Try

            Dim c As Integer
            '  For c = 0 To cliente.Count - 1

            Dim ped = proxy.fWSImportaPedidos(idLoja, login, senha, "2009-11-16", "", "", "", "6")
            Dim p As Integer
            For p = 0 To ped.Count - 1
                Dim pedido As New PedidosVO
                pedido.NFe_emit_CNPJ = cnpj
                pedido.fk_id_plataforma = id_plataforma
                pedido.num_pedido = ped(p).id_pedido.ToString

                Dim cliente = proxy.fWSImportaClientePorPedidoAvancado(idLoja, login, senha, pedido.num_pedido)
                Dim itens = proxy.fWSImportaItensPedidoPorId(idLoja, login, senha, ped(p).id_pedido)

                pedido.xLgr = cliente(c).logradouro.ToString
                pedido.xMun = cliente(c).cidade.ToString
                pedido.UF = cliente(c).uf.ToString
                pedido.CEP = Replace(cliente(c).cep, "-", "").ToString
                pedido.fone = Replace(cliente(c).telefone, "-", "").ToString

                If cliente(c).cnpj_cpf > 11 Then
                    pedido.CNPJ = cliente(c).cnpj_cpf.ToString
                    pedido.xName = cliente(c).cliente.ToString
                Else
                    pedido.CPF = cliente(c).cnpj_cpf.ToString
                    pedido.xName = cliente(c).cliente.ToString
                End If

                pedido.nro = cliente(c).numero.ToString
                pedido.xCpl = cliente(c).complemento.ToString
                pedido.xBairro = cliente(c).bairro.ToString
                pedido.xProcDate = ped(p).data_pedido
                pedido.email = cliente(c).email.ToString
                pedido.xQtyItems = itens.Count
                pedido.fk_xOrderstatusID = 1

                Try
                    PedidosDAO.inserirPedido(pedido)
                    objRetorno.novo += 1

                    '//ITENS
                    Dim totValor = 0.0
                    'totValor = ped.Lista(p).ValorFreteCobrado
                    Dim i = 0
                    For i = 0 To itens.Count - 1

                        Dim pedItem As New Pedidos_ItensVO
                        pedItem.NFe_emit_CNPJ = cnpj
                        pedItem.fk_id_plataforma = id_plataforma
                        pedItem.num_pedido = ped(p).id_pedido.ToString
                        pedItem.cProd = itens(i).id_produto.ToString
                        pedItem.xProd = itens(i).nome_produto.ToString
                        pedItem.qCom = itens(i).quantidade
                        pedItem.vUnCom = itens(i).preco
                        pedItem.vDesc = 0
                        pedItem.vFrete = 0

                        Try
                            Pedidos_ItensDAO.inserirPedido_Item(pedItem)
                        Catch ex As Exception
                            Pedidos_ItensDAO.alterarPedido_Item(pedItem)
                        End Try
                    Next i

                Catch ex As Exception
                    '  PedidosDAO.alterarPedido(pedido)
                    objRetorno.atualizados += 1
                End Try

            Next p
            'Next c
            Return objRetorno

        Catch ex As Exception
            objRetorno.erro = "(Erro ao baixar os pedidos, favor abrir chamado técnico com a NFE4WEB, informando o problema.);"
            Return objRetorno
        End Try

    End Function

End Class
