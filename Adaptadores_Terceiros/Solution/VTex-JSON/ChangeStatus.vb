Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Web
Imports System


Public Class ChangeStatus
    Public Shared Function atualizaStatus(ByVal nomeLoja As String, ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal key As String) As ObjReturn

        Dim objRetorno As ObjReturn = New ObjReturn
        Try
            Log.CriaLog("Sincronização de pedidos")
            Dim listPedidos = PedidosDAO.pegaPedidos(cnpj, id_plataforma)

            Dim i As Integer
            For i = 0 To listPedidos.Count - 1
                Try
                    Dim data As Date = Now()

                    Dim JSON As String

                    Dim valor = listPedidos(i).tot_ped
                    Dim nrNota = 1

                    'JSON = "{""invoiceNumber"":""" + listPedidos(i).NFe_ide_nNF.ToString +
                    JSON = "{""invoiceNumber"":""2"",""invoiceValue"":""" + valor +
                        """,""courier"":"""",""invoiceUrl"":"""",""embeddedInvoice"":""""," +
                           """issuanceDate"":""" + Format(data, "Short Date") + """,""trackingNumber"":""""}"
                    '       """issuanceDate"":""" + Format(data, "Short Date") + """,""trackingNumber"":""""}"




                    Dim OrderIdChangeStatus = listPedidos(i).num_pedido
                    Dim request = WebRequest.Create("http://" + nomeLoja + ".vtexcommercestable.com.br/api/oms/pvt/orders/" + OrderIdChangeStatus + "/shipping-notification")

                    Dim myWebClient = New WebClient()
                    myWebClient.Headers.Add("X-VTEX-API-AppToken", token)
                    myWebClient.Headers.Add("X-VTEX-API-AppKey", key)
                    request.Method = "POST"
                    request.ContentType = "application/json"
                    request.Headers.Add("X-VTEX-API-AppToken", token)
                    request.Headers.Add("X-VTEX-API-AppKey", key)

                    'Dim d As Byte() = Encoding.ASCII.GetBytes(JSON)
                    'Dim res As Byte() = myWebClient.UploadData("http://" + nomeLoja + ".vtexcommercebeta.com.br/api/oms/pvt/orders/" + OrderIdChangeStatus + "/shipping-notification", "POST", d)
                    Dim writer = New StreamWriter(request.GetRequestStream())
                    writer.Write(JSON)
                    writer.Close()
                    '/*As duas linhas abaixo são para capturar o que retorna do servidor, caso tenha sido TRUE é porque integrou com sucesso */
                    Dim SR = New StreamReader(request.GetResponse().GetResponseStream())
                    Dim Result = SR.ReadToEnd()

                    PedidosDAO.baixarPedido(listPedidos(i).num_pedido, cnpj, id_plataforma)


                Catch ex As Exception
                    'MsgBox("falha na sinzonização do pedido nº " + listPedidos(i).num_pedido)
                End Try

            Next i
            objRetorno.sincronizados = i

            Return objRetorno
        Catch ex As Exception
            objRetorno.erro = "Erro ao baixar os pedidos, favor abrir chamado técnico com a NFE4WEB, informando o problema." + CStr(ex.Message)
            Return objRetorno
        End Try


    End Function

End Class
