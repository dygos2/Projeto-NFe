
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Integracao.aspx.vb" Inherits="Solution.Integracao" %>
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
    
    Response.ContentType = "application/json; charset=utf-8"
    'PARA TESTE ciashop ?cnpj=19938457000170&id_plataforma=23&token=0&sinc=0&par1=109810&par2=lumilar.1036    Richardson- loja:109810 senha:richard.b2013    
    'PARA TESTE casa china  ?cnpj=16558268000183&id_plataforma=23&token=0&sinc=0&par1=108256&par2=urrara.4668
    'PARA TESTE  jam360 ?cnpj=18318365000124&id_plataforma=23&token=0&sinc=0&par1=207808&par2=integ.207808
    ' PARA TESTE hachi8 ciashop ?cnpj=15427609000119&id_plataforma=23&token=0&sinc=0&par1=108230&par2=ianque.1123
    'PARA TESTE vtex ?cnpj=99999999999999&id_plataforma=11&token=0&sinc=0&par1=&par2=123@mudar&par3=pandabrinquedos
    '?token=205252952181698023923877151344823824611180&id_plataforma=11&sinc=0&cnpj=17331504000197&par1=vtexappkey-antesqueamodapegue-ELYGVW&par2=AVKLPZHFKATVWKMOAEHAVVTOLAXLBHQHPCAIVGMNGKCGYLZQEPBZOFOECWAOCWEDJLGTJETURTUKTNPZAGGLFRTKOWGYFXLJGZXNFGGAVGHUKDLPHAOVQSXRSVFNYVPT&par3=aqmp&callback=?
    '?token=2055475143177130166092118124121710235116&id_plataforma=11&sinc=0&cnpj=13699177000170&par1=vtexappkey-spirabrasil-JGVHJR&par2=GYYGIJMCZNSSBOHJSTTVEFMMOPCEEVGNHLFRHHTGAYVKSKYDXPFPNFGBMMSTGIXDGELQJQCLANVGJBTDQZHPQRZCEDEUOAZRASDOZITHNTOAJASKTPCHBHMPEJLFLHXA&par3=spira&callback=?
    'PARA TESTE Rakuten Dukan ?cnpj=15773074000138&id_plataforma=25&token=63234149739324423170248216671797036139250&sinc=0&par1=910d52f6-76e9-4358-bdd2-d5914e1c59a1&par2=DD2D8842-4842-4CE8-AFEC-0E1717428D96
    'PARA TESTE Rakuten Panda ?cnpj=11512583000156&id_plataforma=25&token=2471018521424328254144471471593828249155183&sinc=0&par1=2bf140cc-7311-41a5-910a-7bb71f986622&par2=D1CA227A-59DA-4108-9B81-BF992DC06F7A
    'canela e mel hakuten ?token=188168177845317220928410517351652282279&id_plataforma=25&sinc=0&cnpj=17479504000139&par1=3cf7162b-f4b9-473e-803a-ec7cdda3a663&par2=347CD6BE-80DA-45E4-AA39-77EE492F71E9&par3=&callback=?
    'PARA TESTE vtex ?cnpj=67526301001015&id_plataforma=11&token=113248452356886781149119016382414010247&sinc=0&par1=integracaonf&par2=hp@9010#esc&par3=editoraescala   
    'PARA TESTE Rakuten Blue Beach ?cnpj=10655203000170&id_plataforma=25&token=21117820925068127232159514923011921321043209&sinc=0&par1=bc31f566-a8f0-4623-aad5-9c30bc740d73&par2=0c22ee2d-d813-4c20-95a6-e62cbbda1865
    'Para TESTE TRAY Virtual Joias  ?cnpj=10194987000186&token=2394812513961154572255250762271622236364&sinc=0&par1=virtualjoias&par2=472109vjag&par3=102346&id_plataforma=22
    'PARA TESTE Ciashop Framework ?cnpj=99999999999999&id_plataforma=33&token=0&sinc=0&par1=www9.ciashop.com.br/nfe4web&par2=eyJvIjoie1wiaVwiOlwiQ2lhc2hvcFwiLFwic1wiOltcIlJlYWRQcm9kdWN0c1wiLFwiUmVhZERlcGFydG1lbnRzXCIsXCJSZWFkT3JkZXJzXCIsXCJSZWFkb3JkZXJpdGVtc1wiLFwiUmVhZE1ldGFmaWVsZHNcIl0sXCJ1XCI6XCJuZmU0d2ViXCIsXCJhXCI6XCJiNjkwMDI0Ni1lNDU4LTQ2YTMtOTE0Yy1hMmI0M2I2NzVhNmRcIn0iLCJzIjoiRnNLZFUrMGNHYXU1WnJWOXpoaGNTQWEwYlpFVzQycFE0eEp2eUFCNTBFN1VISWsrMUc2L3ovbk5sT2x0MGxZOEppNWRIWFNWckxnelk4bnlmS29sUFVxUU5ROVhlS0dBMXUrQm5jNmRKbmd0ZTYxbGQ1Ly9aVVVPZnNJRGxIRHFXaU1EajNLbFlzVlhwemxUUlY4dElUNHFYSkN0aUNVa3NiNWV3VU9aNXpjPSJ9
    'teste vtex artluz ?token=21715312423813628100165516812112120220621124&id_plataforma=11&sinc=0&cnpj=15837832000134&par1=vtexappkey-artluznet-BZUEJE&par2=XKHKDHVQPKGVDNWROBMNGBNZIAXZEILDFAQGEIMDOSOALCOWJXVIHGWWWHBDKVLGZCQDXPMFOJZNNNOGLBQGNCWYDTKXHJTSHHAJLLZTKPUXHIJITUSIFETWGRADFPYE&par3=elumine&callback=?


    Try
        
        Dim cnpj = Request.QueryString("CNPJ")
        Dim id_plataforma = CInt(Request.QueryString("id_plataforma"))
        Dim token = Request.QueryString("token")
        Dim sinc = CInt(Request.QueryString("sinc")) '0-baixar pedidos / 1-atualizar pedidos
        Dim login = Request.QueryString("par1")
        Dim senha = Request.QueryString("par2").Trim()
        Dim url = Request.QueryString("par3")
        Dim chavePedido = Request.QueryString("par1")
        Dim codLoja = Request.QueryString("par2")
        Dim idLoja = Request.QueryString("par3")
        
        Dim novo As ObjReturn = New ObjReturn
        Dim sincronizados As ObjReturn = New ObjReturn
        Dim peds As ObjReturn = New ObjReturn
        Dim JSON As String

        'varreduraShopLine.Main()
        
        Log.CriaLog("Inicio da integração com CNPJ :" + CStr(cnpj) + "/ id_plataforma :" + CStr(id_plataforma) + "/ sinc :" +
                                                   CStr(sinc) + "/ login :" + CStr(login) + "/ senha :" + CStr(senha))
        
        
        If Funcs.validaEmpresa(cnpj, token) Then
    
            Select Case id_plataforma
                
                Case 11 'V-Tex
                    If sinc = 0 Then
                        If cnpj = "67526301001015" Then '**Editora e Distribuidora Edipress Ltda (ESCALA)**
                            novo = OrderGet.NovosPedidos(cnpj, id_plataforma, token, login, "hp@9010#esc", "editoraescala")
                        Else
                            novo = GetOrder.pegaPedidos(url, codLoja, chavePedido, cnpj, id_plataforma)
                        End If
                        
                    Else
                        If cnpj = "67526301001015" Then '**Editora e Distribuidora Edipress Ltda (ESCALA)**
                            sincronizados = ChangeOrder.SincronizaPedido(cnpj, id_plataforma, token, login, "hp@9010#esc", "editoraescala")
                        Else
                            sincronizados = ChangeStatus.atualizaStatus(url, cnpj, id_plataforma, codLoja, chavePedido)
                        End If
                                                
                    End If
                    
                Case 22 'Tray
                    If sinc = 0 Then
                       
                        novo = fWSImportaPedidos.NovosPedidos(cnpj, id_plataforma, token, idLoja, login, senha)
                    Else
                        sincronizados = fWSAtualizaListaToDo.SincronizaPedido(cnpj, id_plataforma, token, idLoja, login, senha)
                    End If
                    
                Case 23 'CIASHOP
                    If sinc = 0 Then
                        '//Framework
                        novo = WebPedidos.NovosPedidos(cnpj, id_plataforma, token, login, senha)
                    Else
                        sincronizados = WebSincronia.SincronizaPedido(cnpj, id_plataforma, token, login, senha)
                    End If
                    
                    
                Case 25 'Rakuten
                    If sinc = 0 Then
                        If cnpj = "10655203000170" Then '** RAKUTEN EC - Blue Beach **
                            novo = NewOrder.NovosPedidos(cnpj, id_plataforma, token, chavePedido, codLoja)
                        Else
                            novo = RetrieveNewOrdersRakuten.NovosPedidos(cnpj, id_plataforma, token, chavePedido, codLoja)
                        End If
                    Else
                        If cnpj = "10655203000170" Then '** RAKUTEN EC - Blue Beach **
                            sincronizados = ChangeOrderEC.SincronizaPedido(cnpj, id_plataforma, token, chavePedido, codLoja)
                        Else
                            sincronizados = IntegraChangeRakuten.SincronizaPedido(cnpj, id_plataforma, token, chavePedido, codLoja)
                        End If
                        
                    End If

                Case 33 'Ciashop Framework
                    If sinc = 0 Then
                        '?cnpj=99999999999999&id_plataforma=11&token=0&sinc=0&par1=www9.ciashop.com.br/nfe4web&par2=eyJvIjoie1wiaVwiOlwiQ2lhc2hvcFwiLFwic1wiOltcIlJlYWRQcm9kdWN0c1wiLFwiUmVhZERlcGFydG1lbnRzXCJdLFwidVwiOlwibmZlNHdlYlwiLFwiYVwiOlwiYjY5MDAyNDYtZTQ1OC00NmEzLTkxNGMtYTJiNDNiNjc1YTZkXCJ9IiwicyI6IlAyVFJYSTh2NENCeEdNT1pWNGxLK1p3cXNIV3puTWkyZy9ua0l4QnpoUHU5QWVneHV5WlJTZmQxekxkMHBSc3B5djhqM240cFVMRjJISWNUMFpJTmJQT3hDenBYMjZ6ZnRnL1plSThwRFVkc2JJcnVRTmI1NXlsLytMdDNJTEsrSXpnMlBLKzNxWHJwVEgvbGk2ZlM1RjhwZlZxM1FTdVNURitRTHlIWTdFVT0ifQ  //Framework
                        novo = baixaPedido.pegaProdutos(cnpj, id_plataforma, login, senha)
                    Else
                      
                    End If
                    
            End Select
            peds.baixados = novo.atualizados + novo.novo
            peds.novo = novo.novo
            peds.atualizados = novo.atualizados
            peds.sincronizados = sincronizados.sincronizados
            peds.erro = sincronizados.erro + novo.erro
                    
            JSON = "({""peds_novo"":""" + CStr(peds.novo) + """,""peds_atualizados"":""" + CStr(peds.atualizados) +
                  """,""peds_baixados"":""" + CStr(peds.baixados) + """,""peds_sincronizados"":""" + CStr(peds.sincronizados) + """,""erro"":""" + peds.erro + """})"

        Else
            JSON = "({""peds_novo"":""0"",""peds_atualizados"":""0"",""peds_baixados"":""0"",""peds_sincronizados"":""0"",""erro"":""Falha ao validar empresa!""})"
        End If
        
        Response.Write(CStr(Request.QueryString("callback")) + CStr(JSON))
        
    Catch
        
    End Try
       
    %>
