<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="api2.aspx.vb" Inherits="Solution.api2" %><%
    Response.ContentType = "application/json; charset=utf-8"
    
                                                                            
                                                                                                        Dim proxy As Solution.Pedidos.PedidoWSClient = New Solution.Pedidos.PedidoWSClient()
                                                                                                        Dim peds As Solution.Pedidos.Pedido = New Solution.Pedidos.Pedido()
    'Dim itens As Pedidos.PedidoItem = New Pedidos.PedidoItem()

    Dim user, pass As String
    user = Request.QueryString("StoreName")
    pass = Request.QueryString("token")
    'user = "svcmacrotecs"
    'pass = "$vcm@cr0tecs"
                                                                                                        'api2.aspx?storename=svcmacrotecs&token=$vcm@cr0tecs
                                                                                                        
    proxy.ClientCredentials.HttpDigest.ClientCredential.UserName = user
    proxy.ClientCredentials.HttpDigest.ClientCredential.Password = pass
    proxy.ClientCredentials.UserName.UserName = user
    proxy.ClientCredentials.UserName.Password = pass

                                                                                                        Dim PedidoFiltro As Solution.Pedidos.PedidoFiltro = New Solution.Pedidos.PedidoFiltro()

    Dim ped_id, ped_stat, dat_ini, dat_fim As String
    
    ped_id = Request.QueryString("para3")
    ped_stat = Request.QueryString("para4")
    dat_ini = Request.QueryString("para1")
    dat_fim = Request.QueryString("para2")
    
    If ped_id Is Nothing And ped_stat Is Nothing And dat_ini Is Nothing And dat_fim Is Nothing Then
        PedidoFiltro.PedidoID = 10
    End If
        
                                                                                                        If ped_id IsNot Nothing Then
                                                                                                            PedidoFiltro.PedidoID = Int(ped_id)
                                                                                                        End If
    
    If ped_stat IsNot Nothing Then
        PedidoFiltro.StatusID = Int(ped_stat)
    End If

    If dat_ini IsNot Nothing Then
        Dim data_parser_ini As Date = dat_ini
                                                                                                            PedidoFiltro.DataInicial = data_parser_ini
                                                                                                            'no teste1, passando #5/8/2013#
    End If
    
    If dat_fim IsNot Nothing Then
        Dim data_parser_fim As Date = dat_fim
                                                                                                            PedidoFiltro.DataFinal = data_parser_fim
                                                                                                            'no teste1, passando #5/8/2013#
    End If
    
                                                                                                        Dim resp_json As String = ""
    
    
    Try
        
                                                                                                            Dim answer As Solution.Pedidos.RespostaProcessamento = proxy.PedidosDisponiveisComFiltro(PedidoFiltro)
                                                                                                            Dim txt_nome As String
                                                                                                            If answer.Erros.Length = 0 Then
                                                                                                                
                                                                                                            
                                                                                                                For Each item As Solution.Pedidos.Pedido In answer.Resultado
                                                                                                                    txt_nome = item.EntregaNome.Replace("'", "`").ToUpper
                                                                                                                    resp_json = resp_json & "{'xOrderID':'" & item.PedidoID & "','xProcDate':'" & item.DataVenda & "','xCustName':'" & txt_nome & "','xQtyItems':'" & item.Itens.Length & "','xOrderStatusID':'" & item.StatusID & "'},"
                                                                                                                Next
                                                                                                                If resp_json.Length > 0 Then
                                                                                                                    resp_json = Left(resp_json, resp_json.Length - 1)
                                                                                                                    Response.Write(Request.QueryString("callback") & "([" & resp_json & "]);")
                                                                                                                Else
                                                                                                                    Response.Write(Request.QueryString("callback") & "([Sem resultados]);")
                                                                                                                End If
                                                                                                            Else
                                                                                                                Response.Write(Request.QueryString("callback") & "([Erro: " & answer.Erros(0).Mensagem & "]);")
                                                                                                            End If

    
                                                                                                        Catch ex As Exception
                                                                                                            Response.Write(Request.QueryString("callback") & "([Erro na conexao: " & ex.Message & "]);")
                                                                                                        Finally
                                                                                                            ' Verifica se o proxy não está fechado ou com erro e chama o close para fechar a conexão
                                                                                                            Try
                                                                                                                proxy.Close()
                                                                                                            Catch ex As Exception
                                                                                                                proxy.Abort()
                                                                                                            End Try
        
                                                                                                        End Try
 %>