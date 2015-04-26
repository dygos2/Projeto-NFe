<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="api3.aspx.vb" Inherits="Solution.api3" %><%
    Response.ContentType = "application/json; charset=utf-8"
    
    Dim proxy As Solution.Pedidos.PedidoWSClient = New Solution.Pedidos.PedidoWSClient()
    Dim peds As Solution.Pedidos.Pedido = New Solution.Pedidos.Pedido()
    'Dim itens As Pedidos.PedidoItem = New Pedidos.PedidoItem()

    Dim user, pass As String
    user = Request.QueryString("StoreName")
    pass = Request.QueryString("token")
    'user = "svcmacrotecs"
    'pass = "$vcm@cr0tecs"

    proxy.ClientCredentials.HttpDigest.ClientCredential.UserName = user
    proxy.ClientCredentials.HttpDigest.ClientCredential.Password = pass
    proxy.ClientCredentials.UserName.UserName = user
    proxy.ClientCredentials.UserName.Password = pass

    Dim PedidoFiltro As Solution.Pedidos.PedidoFiltro = New Solution.Pedidos.PedidoFiltro()

    Dim ped_id As String
    
    ped_id = Request.QueryString("para1")

    If ped_id Is Nothing Then
        PedidoFiltro.PedidoID = 10
    End If
        
    If ped_id IsNot Nothing Then
        PedidoFiltro.PedidoID = Int(ped_id)
    End If

    Dim resp_json, resp_json_itens As String
    resp_json_itens = ""
    'Dim time1 As DateTime = DateTime.Now
    'Dim time2 As DateTime = DateTime.Now
    'time1 = time1.AddMonths(-12)
    'Dim thisDate2 As Date = #12/9/2013 9:05:14 PM#
    
    'PedidoFiltro.DataInicial = time1
    'PedidoFiltro.DataFinal = time2
                                                                                                        Try
                                                                                                            Dim txt_nome, txt_cidade, txt_log, txt_comp, txt_bairro As String
                                                                                                            Dim answer As Solution.Pedidos.RespostaProcessamento = proxy.PedidosDisponiveisComFiltro(PedidoFiltro)
                                                                                                            Dim PrecoUnitario As Double
                                                                                                            Dim ped_uniq As Solution.Pedidos.Pedido = answer.Resultado(0)
                                                                                                            
                                                                                                            txt_nome = ped_uniq.EntregaNome.Replace("'", "`").ToUpper
                                                                                                            txt_cidade = ped_uniq.EntregaCidade.Replace("'", "`")
                                                                                                            txt_log = ped_uniq.EntregaLogradouro.Replace("'", "`")
                                                                                                            txt_comp = ped_uniq.EntregaComplementoEndereco.Replace("'", "`")
                                                                                                            txt_bairro = ped_uniq.EntregaBairro.Replace("'", "`")
                                                                                                            Dim cpf = ""
                                                                                                            Dim cnpj = ""
                                                                                                            If (ped_uniq.TipoCliente="PF")
                                                                                                                cpf = ped_uniq.NumeroRegistroCliente
                                                                                                            Else
                                                                                                                cnpj = ped_uniq.NumeroRegistroCliente
                                                                                                            End If
                                                                                                            
                                                                                                            
                                                                                                            resp_json = "{'CNPJ':'" + cpf + "','CPF':'" + cnpj + "','xName':'" & txt_nome & "','xLgr':'" & txt_log & "','nro':'" & ped_uniq.EntregaNumero & "','xCpl':'" & txt_comp & "','xBairro':'" & txt_bairro & "','cMun':'','xMun':'" & txt_cidade & "','UF':'" & ped_uniq.EntregaEstado & "','CEP':'" & ped_uniq.EntregaCEP & "','fone':'" & ped_uniq.EntregaTelefone & "','IE':'','email':'" & ped_uniq.EntregaEmail & "','Item_Object':[replace_here]}"
        
                                                                                                            Dim vfrete, vdesc As Double
                                                                                                            For Each item As Solution.Pedidos.Pedido In answer.Resultado
                                                                                                                If item.ValorFrete > 0 Then
                                                                                                                    vfrete = item.ValorFrete / item.Itens.Length
                                                                                                                    vfrete = Math.Round(vfrete, 2)
                                                                                                                Else
                                                                                                                    vfrete = 0
                                                                                                                End If

                                                                                                                'sistema estava informando desconto errado
                                                                                                                'If item.ValorDesconto > 0 Then
                                                                                                                ' vdesc = item.ValorDesconto / item.Itens.Length
                                                                                                                'vdesc = Math.Round(vdesc, 2)
                                                                                                                'Else
                                                                                                                vdesc = 0
                                                                                                                'End If
            
                                                                                                                For Each items As Solution.Pedidos.PedidoItem In item.Itens
                                                                                                                    PrecoUnitario = items.PrecoUnitario ' + vdesc
                                                                                                                    resp_json_itens = resp_json_itens & "{'cProd':'" & items.CodigoDoProduto & "','cEAN':'','xProd':'" & items.NomeDoProduto & "','NCM':'','uCom':'Unid','qCom':'" & items.Quantidade & "','subst':'0','vUnCom':'" & PrecoUnitario & "','vFrete':'" & vfrete & "','vDesc':'" & vdesc & "'},"
                                                                                                                Next
                                                                                                            Next

                                                                                                            If resp_json.Length > 0 Then
                                                                                                                resp_json_itens = Left(resp_json_itens, resp_json_itens.Length - 1)
                                                                                                                resp_json = resp_json.Replace("replace_here", resp_json_itens)
                                                                                                                Response.Write(Request.QueryString("callback") & "(" & resp_json & ");")
                                                                                                            Else
                                                                                                                Response.Write(Request.QueryString("callback") & "([Sem resultados]);")
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