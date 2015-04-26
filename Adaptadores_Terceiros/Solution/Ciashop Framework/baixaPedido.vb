Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Web
Imports System

Public Class baixaPedido

    Shared Function pegaProdutos(ByVal cnpj As String, ByVal id_plataforma As String, ByVal nomeLoja As String, ByVal token As String) As ObjReturn
        'Shared Function pegaPedidos(ByVal nomeLoja As String) As IList(Of PedidosVO)
        Dim objRetorno As ObjReturn = New ObjReturn
        Try
            Dim myWebClient = New WebClient()
            'Configurando dados para acesso
            myWebClient.Headers.Add("Content-Type", "application/json")
            myWebClient.Headers.Add("x-apilimit-remaining", "")
            myWebClient.Headers.Add("x-hasmore", "")
            myWebClient.Headers.Add("Authorization", "Bearer " + token)


            'Baixando lista de campos extras
            Dim UrlStatus = "https://" + nomeLoja + "/api/v1/Metafields/"
            Dim jsonGetMetas = myWebClient.DownloadString(UrlStatus)
            Dim listMeta = JsonConvert.DeserializeObject(jsonGetMetas)
            'Baixando lista de MetaFields
            UrlStatus = "https://" + nomeLoja + "/api/v1/products/"
            Dim jsonGetProducts = myWebClient.DownloadString(UrlStatus)
            Dim listProds = JsonConvert.DeserializeObject(jsonGetProducts)

            UrlStatus = "https://" + nomeLoja + "/api/v1/products/count"
            Dim jsonGetCount = myWebClient.DownloadString(UrlStatus)
            Dim countProds = JsonConvert.DeserializeObject(jsonGetCount)

            Dim nextProd

            Dim nroItens = CInt(countProds.item("count").ToString)

            Dim prodAtual

            'Inserindo produtos



            Dim produto As New produto_fiscalVO()
            produto.cProd = listProds(0).Item("id").ToString
            produto.xProd = listProds(0).Item("name").ToString
            produto.NFe_emit_CNPJ = cnpj
            produto.fk_id_plataforma = id_plataforma
            Try
                produto_fiscalDAO.inserirProduto(produto)
                objRetorno.novo += 1
            Catch ex As Exception
                produto_fiscalDAO.alterarProduto(produto)
                objRetorno.atualizados += 1
            End Try

            prodAtual = produto.cProd.ToString


            For prox = 0 To nroItens - 1

                prodAtual = CStr(CInt(prodAtual + 1))

                Try
                    UrlStatus = "https://" + nomeLoja + "/api/v1/products/" + prodAtual
                    jsonGetProducts = myWebClient.DownloadString(UrlStatus)
                    nextProd = JsonConvert.DeserializeObject(jsonGetProducts)

                    Dim prod As New produto_fiscalVO()
                    produto.cProd = nextProd.Item("id").ToString
                    produto.xProd = nextProd.Item("name").ToString
                    produto.NFe_emit_CNPJ = cnpj
                    produto.fk_id_plataforma = id_plataforma
                    Try
                        produto_fiscalDAO.inserirProduto(produto)
                        objRetorno.novo += 1
                    Catch ex As Exception
                        produto_fiscalDAO.alterarProduto(produto)
                        objRetorno.atualizados += 1
                    End Try

                    prodAtual = produto.cProd
                Catch ex As Exception

                End Try

            Next prox

            Dim metafields As List(Of produto_fiscalVO)

            Try
                metafields = Funcs.pegaMetas(listMeta, cnpj, id_plataforma)
                Dim contM As Integer
                For contM = 0 To metafields.Count - 1
                    produto_fiscalDAO.inseriMeta(metafields(contM))
                Next contM

            Catch ex As Exception
                objRetorno.erro = "Não foram encontrados Campos Extras nos produtos!"
            End Try

            'Inserindo MetaFields existente no produto correspondente


            Return objRetorno

        Catch ex As Exception
            objRetorno.erro = ex.Message

            Return objRetorno
        End Try

    End Function


    Shared Function pegaitens(ByVal numPed As String) As IList(Of Pedidos_ItensVO)



        Dim peditens As New Pedidos_ItensVO
        Return peditens
    End Function

End Class
