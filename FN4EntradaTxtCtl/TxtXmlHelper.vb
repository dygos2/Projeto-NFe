Imports FN4Common
Imports FN4Common.DataAccess
Imports System.Xml
Imports System.Xml.Schema
Imports System.IO
Imports System.Xml.Xsl
Imports System.Text.RegularExpressions

Public Class TxtXmlHelper

    Private Shared resultadoValidacao As System.Text.StringBuilder

    Private Class arquivoVO
        Public cabecalho As New ArrayList
        Public detalhes As New ArrayList
        Public totais As New ArrayList
        Public observacoes As New ArrayList
        Public importacao As New ArrayList
        Public exportacao As New ArrayList
        Public txt As String
        Public linhas As New ArrayList
        Public informacoesAdicionais As New ArrayList

        Public Sub New(ByVal strEntrada As String, ByVal delimitador As String)
            Dim linha As String
            Dim valor As String
            Dim each_linha_arr As Array

            'txt
            Me.txt = strEntrada

            'linhas
            For Each linha In strEntrada.replace(vbCr, "").Split(vbLf)
                linha = linha.Trim.Replace(vbCr, "").Replace(vbLf, "")
                If linha <> "" Then
                    Me.linhas.Add(linha.Replace(vbCr, "").Replace(vbLf, ""))
                End If
            Next

            If linhas.Count < 4 Then Throw New Exception("Arquivo invalido. Total de linhas insuficiente")

            For Each each_linha In linhas
                each_linha_arr = each_linha.ToString.Split(delimitador)

                Select Case each_linha_arr(0)
                    Case "01" 'cabecalho
                        For Each valor In each_linha.Split(delimitador)
                            valor = valor.Trim.Replace(vbCr, "").Replace(vbLf, "")
                            cabecalho.Add(valor.Replace(vbCr, "").Replace(vbLf, ""))
                        Next
                    Case "02" 'item
                        Dim valores As New ArrayList
                        For Each valor In each_linha.Split(delimitador)
                            valor = valor.Trim.Replace(vbCr, "").Replace(vbLf, "")
                            valores.Add(valor.Replace(vbCr, "").Replace(vbLf, ""))
                        Next
                        detalhes.Add(valores)
                    Case "021" 'importacao
                        Dim valores As New ArrayList
                        For Each valor In each_linha.Split(delimitador)
                            valor = valor.Trim.Replace(vbCr, "").Replace(vbLf, "")
                            valores.Add(valor.Replace(vbCr, "").Replace(vbLf, ""))
                        Next
                        importacao.Add(valores)
                    Case "022" 'exportacao
                        Dim valores As New ArrayList
                        For Each valor In each_linha.Split(delimitador)
                            valor = valor.Trim.Replace(vbCr, "").Replace(vbLf, "")
                            valores.Add(valor.Replace(vbCr, "").Replace(vbLf, ""))
                        Next
                        exportacao.Add(valores)
                    Case "03" 'totais
                        For Each valor In each_linha.Split(delimitador)
                            valor = valor.Trim.Replace(vbCr, "").Replace(vbLf, "")
                            totais.Add(valor.Replace(vbCr, "").Replace(vbLf, ""))
                        Next
                    Case "04" 'observacoes
                        For Each valor In each_linha.Split(delimitador)
                            valor = valor.Trim.Replace(vbCr, "").Replace(vbLf, "")
                            observacoes.Add(valor.Replace(vbCr, "").Replace(vbLf, ""))
                        Next
                    Case "99" 'Informacoes adicionais
                        For Each valor In each_linha.Split(delimitador)
                            valor = valor.Trim.Replace(vbCr, String.Empty).Replace(vbLf, String.Empty)
                            informacoesAdicionais.Add(valor.Replace(vbCr, String.Empty).Replace(vbLf, String.Empty))
                        Next
                End Select

            Next

        End Sub
    End Class

    Private Class mapVO
        Public xPath As String
        Public tipo As Integer
        Public indice As Integer
        Public Sub New(ByVal xpath As String, ByVal tipo As Integer, ByVal indice As Integer)
            Me.xpath = xpath.Trim.Replace(vbCr, "").Replace(vbLf, "")
            Me.tipo = tipo
            Me.indice = indice
        End Sub
    End Class
    'TODO verificar essa função
#Region "Mappers"
    Public Shared Function gerarXML(ByVal strEntrada As String, ByVal empresa As empresaVO, ByVal nNF As Long) As XmlDocument

        Dim mappings As ArrayList
        Dim mappings_exp As ArrayList
        Dim mappings_imp As ArrayList
        Dim xmlCanonico As XmlDocument
        Dim dupMappings As ArrayList
        Dim dupXml As XmlNode
        Dim detMappings As ArrayList
        Dim detxml As XmlNode
        Dim impMappings As ArrayList
        Dim valoresEntrada As arquivoVO

        Try
            'Dim ct
            valoresEntrada = New arquivoVO(strEntrada, empresa.delimitador)
            'Dim map As mapVO

            'verificando delimitadores
            'campo 01 tem que ter 56 delimitadores exatamente
            If valoresEntrada.cabecalho.Count <> 56 Then
                Throw New Exception("Erro no cabeçalho (01) Delimitadores encontrados (" & valoresEntrada.cabecalho.Count & ") - Necessários exatamente  56")
            Else
                'concatena utc no campo data se for nfe 3.10
                Try
                    If empresa.versao_nfe <> "2.00" Then
                        'se a versao for 3.10, adiciona o utc da cidade
                        If valoresEntrada.cabecalho(6) <> "" Then
                            valoresEntrada.cabecalho(6) = String.Concat(valoresEntrada.cabecalho(6), empresa.utc)
                        End If
                        If valoresEntrada.cabecalho(7) <> "" Then
                            valoresEntrada.cabecalho(7) = String.Concat(valoresEntrada.cabecalho(7), empresa.utc)
                        End If
                    Else
                        'se a versao for 2.00, passar somente data em um campo e a hora em outro
                        If valoresEntrada.cabecalho(6) <> "" Then
                            valoresEntrada.cabecalho(6) = Split(valoresEntrada.cabecalho(6), "T")(0)
                        End If
                        If valoresEntrada.cabecalho(7) <> "" Then
                            valoresEntrada.cabecalho(7) = Split(valoresEntrada.cabecalho(7), "T")(1)
                        End If
                    End If

                Catch ex As Exception
                    Throw New Exception("Erro ao carregar o UTC do estado " & empresa.uf & " : " & ex.Message)
                End Try
            End If

            'loop nos itens
            Dim loop_ct
            loop_ct = 0
            For Each itemp_tmp In valoresEntrada.detalhes
                loop_ct += 1
                'campo 02 tem que ter 100 delimitadores exatamente
                If itemp_tmp.count <> 100 Then
                    Throw New Exception("Erro no Ítem nº " & loop_ct & ". Delimitadores encontrados (" & itemp_tmp.count & ") - Necessários  exatamente  100")
                End If
            Next
            'TODO verificar DI e Exportacao
            'loop nas importacoes
            loop_ct = 0
            For Each itemp_tmp In valoresEntrada.importacao
                loop_ct += 1
                'campo 021 tem que ter ao menos 28 delimitadores
                If itemp_tmp.count < 28 Then
                    Throw New Exception("Erro na DI nº " & loop_ct & ". Delimitadores encontrados (" & itemp_tmp.count & ") - Necessários 28 ou mais")
                End If
            Next
            'loop nas exportacoes
            loop_ct = 0
            For Each itemp_tmp In valoresEntrada.exportacao
                loop_ct += 1
                'campo 022 tem que ter ao menos 6 delimitadores
                If itemp_tmp.count < 6 Then
                    Throw New Exception("Erro na exportação (022) nº " & loop_ct & ". Delimitadores encontrados (" & itemp_tmp.count & ") - Necessários 6 ou mais")
                End If
            Next
            'se tiver importacao, terá que existir a mesma qtd de campos de itens
            If valoresEntrada.importacao.Count > 0 And valoresEntrada.importacao.Count <> valoresEntrada.detalhes.Count Then
                Throw New Exception("Importações divergem dos itens. Enviados (" & valoresEntrada.importacao.Count & ") campos de DI e encontrados " & valoresEntrada.detalhes.Count & " produtos.")
            End If

            'se tiver exportacao, terá que existir a mesma qtd de campos de itens
            If valoresEntrada.exportacao.Count > 0 And valoresEntrada.exportacao.Count <> valoresEntrada.detalhes.Count Then
                Throw New Exception("Exportações divergem dos itens. Enviados (" & valoresEntrada.exportacao.Count & ") campos de Ex e encontrados " & valoresEntrada.detalhes.Count & " produtos.")
            End If

            'campo 03 tem que ter 118 delimitadores no mínimo
            If valoresEntrada.totais.Count < 118 Then
                Throw New Exception("Erro nos Totais (03) Delimitadores encontrados (" & valoresEntrada.totais.Count & ") - Necessários  no mínimo  118")
            End If

            'campo 04 tem que ter >= 3 delimitadores (atende Fisconet e NFecommerce)
            If valoresEntrada.observacoes.Count < 3 Then
                Throw New Exception("Erro nos Campos adicionais (04) Delimitadores encontrados (" & valoresEntrada.informacoesAdicionais.Count & ") - Necessários > 2")
            End If

            'campo 99 tem que ter 9 delimitadores exatamente
            If valoresEntrada.informacoesAdicionais.Count <> 9 Then
                Throw New Exception("Erro nos Campos de parâmetros (99). Delimitadores encontrados (" & valoresEntrada.informacoesAdicionais.Count & ") - Necessários exatamente  9")
            End If

            'geral
            mappings = obterMappings("TXTmappingsGerais.txt")
            xmlCanonico = obterXMLCanonico()

            'importacao
            mappings_imp = obterMappings("TXTmappingsImportacao.txt")

            'exportacao
            mappings_exp = obterMappings("TXTmappingsExportacao.txt")

            'duplicatas
            dupMappings = obterMappings("TXTmappingsDuplicatas.txt")
            dupXml = xmlCanonico.SelectSingleNode("/NFe/infNFe[1]/cobr[1]/dup[1]")

            'detalhes
            detMappings = obterMappings("TXTmappingsProdutos.txt")
            detxml = xmlCanonico.SelectSingleNode("/NFe/infNFe[1]/det[1]").Clone

            'impostos
            impMappings = obterMappings("TXTmappingsImposto.txt")
        Catch ex As Exception
            Throw New Exception("Erro nas atribuições de mappings. " + ex.Message & vbCrLf + Geral.ObterStackTraceEmCascata(ex))
        End Try

        '------------------------------------------
        'processa os mapeamentos gerais
        For Each map In mappings
            If map.tipo = 1 Then
                Try
                    If map.indice <= valoresEntrada.cabecalho.Count Then
                        If valoresEntrada.cabecalho(map.indice - 1) <> "" Then
                            xmlCanonico.SelectSingleNode(map.xPath).InnerText = valoresEntrada.cabecalho(map.indice - 1) 'o arraylist é zero based
                        End If
                    End If

                Catch ex As Exception
                    Console.WriteLine(map.indice & ", " & map.tipo & ", " & map.xPath)
                    Throw New Exception("Erro em mappings. " + ex.Message)
                End Try
            End If

            If map.tipo = 3 Then
                Try
                    If valoresEntrada.totais(map.indice - 1) <> "" Then
                        xmlCanonico.SelectSingleNode(map.xPath).InnerText = valoresEntrada.totais(map.indice - 1) 'o arraylist é zero based
                    End If
                Catch ex As Exception
                    Console.WriteLine(map.indice & ", " & map.tipo & ", " & map.xPath)
                    Throw New Exception("Erro em map.tipo = 3 " + ex.Message)
                End Try
            End If

            If map.tipo = 4 Then
                Try
                    If valoresEntrada.observacoes(map.indice - 1) <> "" Then
                        xmlCanonico.SelectSingleNode(map.xPath).InnerText = valoresEntrada.observacoes(map.indice - 1) 'o arraylist é zero based
                    End If
                Catch ex As Exception
                    Console.WriteLine(map.indice & ", " & map.tipo & ", " & map.xPath)
                    Throw New Exception("Erro em map.tipo = 4 " + ex.Message)
                End Try
            End If
        Next

        '----------------------------------------------------------------
        'processa mapeamento com n repeticoes das duplicatas
        'quantidade de duplicatas = total de valores - o inicio das duplicatas contados de 3 em 3 (tamanho da tag de duplicatas)
        Dim qteDup As Integer = (valoresEntrada.totais.Count - 106) / 3 'TODO - checar esse valor
        'Dim qteDup As Integer = 1

        'para contador = 1 to quantidade de notas
        For ct = 1 To qteDup

            'o registro a ser processado é a contado a partir do ultimo valor trabalhado
            Dim atual As Integer = 106 + 3 * (ct - 1)

            'se for mais de uma duplicata tem q adicionar os nodes de duplicata
            If ct > 1 Then
                Try
                    'Se tiver algum valor nos campos, então clona`e também já popula os campos
                    If valoresEntrada.totais(atual - 1) <> "" Or valoresEntrada.totais(atual) <> "" Or valoresEntrada.totais(atual + 1) <> "" Then
                        'duplicando a tag cobrança
                        xmlCanonico.SelectSingleNode("/NFe/infNFe[1]/cobr[1]").AppendChild(dupXml.Clone)

                        'varrendo para adicionar valores
                        For Each map In dupMappings
                            If valoresEntrada.totais(atual - 1) <> "" Then 'zero based

                                'troca CONTADOR por ct para posicionar no item certo
                                xmlCanonico.SelectSingleNode(map.xPath.Replace("CONTADOR", ct)).InnerText = valoresEntrada.totais(atual - 1) 'o arraylist é zero based
                            Else 'se nao tiver valor, zera o campo
                                xmlCanonico.SelectSingleNode(map.xPath.Replace("CONTADOR", ct)).InnerText = String.Empty
                            End If
                            atual += 1
                        Next

                    End If

                Catch e As Exception
                    Throw New Exception("Erro ao clonar o Dup: " + e.Message)
                End Try

            Else

                Try
                    'Se tiver algum valor nos campos, então já popula os campos
                    If valoresEntrada.totais(atual - 1) <> "" Or valoresEntrada.totais(atual) <> "" Or valoresEntrada.totais(atual + 1) <> "" Then

                        'varrendo para adicionar valores
                        For Each map In dupMappings
                            If valoresEntrada.totais(atual - 1) <> "" Then 'zero based

                                'troca CONTADOR por ct para posicionar no item certo
                                xmlCanonico.SelectSingleNode(map.xPath.Replace("CONTADOR", ct)).InnerText = valoresEntrada.totais(atual - 1) 'o arraylist é zero based
                            End If
                            atual += 1
                        Next

                    End If

                Catch e As Exception
                    Throw New Exception("Erro em else ct = 1 To qteDup: " + e.Message)
                End Try

            End If

        Next

        'processa os produtos
        '--------------------------
        'quantidade de produtos
        Dim qtdeProdutos As Integer = valoresEntrada.detalhes.Count

        'para contador = 1 to quantidade de produtos
        For ct = 1 To qtdeProdutos

            'se for mais de uma PRODUTO tem q adicionar  
            If ct > 1 Then
                Try
                    xmlCanonico.SelectSingleNode("/NFe/infNFe[1]").InsertAfter(detxml.Clone, xmlCanonico.SelectSingleNode("/NFe/infNFe[1]/det[" & ct - 1 & "]"))
                Catch e As Exception
                    Console.WriteLine(e.Message)
                    Throw New Exception("Erro em qtdeProdutos: " + e.Message)
                End Try

            End If

            Try
                'imposto ICMS monta manualmente 
                'comentado em 20/02/2013, nao precisa buscar esse campo do banco pois todo cliente acaba sendo igual
                'Dim configuracao As configuracaoVO = configuracaoDAO.obterConfiguracao("formatoICMS", 0)
                'Dim padraoICMS As String = configuracao.valor
                Dim padraoICMS As String = "<ICMS99><orig/><CST/><CSOSN/><modBC/><pRedBC/><vBC/><pICMS/><vICMSOp/><pDif/><vICMSDif/><vICMS/><modBCST/><pMVAST/><pRedBCST/><vBCST/><pICMSST/><vICMSST/><vBCSTRet/><vICMSSTRet/><motDesICMS/><vICMSDeson/><pCredSN/><vCredICMSSN/><vBCSTDest/><vICMSSTDest/></ICMS99>"

                'padraoICMS = padraoICMS.Replace("ICMS99", icmsTag)
                Dim xmlICMS As New XmlDocument
                xmlICMS.LoadXml(padraoICMS)

                xmlCanonico.SelectSingleNode("/NFe/infNFe[1]/det[" & ct & "]/imposto[1]/ICMS[1]").AppendChild(xmlCanonico.ImportNode(xmlICMS.ChildNodes(0), True))
            Catch ex As Exception
                Throw New Exception("Erro ao obter configuracao, padraoICMS, etc. " + ex.Message + Geral.ObterStackTraceEmCascata(ex))
            End Try

            For Each map In impMappings
                Try
                    If valoresEntrada.detalhes(ct - 1)(map.indice - 1) <> "" Then 'zero based

                        'troca CONTADOR por ct para posicionar no item certo 
                        'imposto troca o imposto
                        xmlCanonico.SelectSingleNode(map.xPath.Replace("CONTADOR", ct)).InnerText = valoresEntrada.detalhes(ct - 1)(map.indice - 1) 'o arraylist é zero based

                        '     Log.registrarDebug(map.xPath & " - " & valoresEntrada.detalhes(ct - 1)(map.indice - 1))
                        If map.xPath = "/NFe/infNFe[1]/det[CONTADOR]/imposto[1]/PIS[1]/PISPadr[1]/CST[1]" Then
                            xmlCanonico.SelectSingleNode(map.xPath.Replace("CONTADOR", ct)).InnerText = valoresEntrada.detalhes(ct - 1)(map.indice - 1) 'o arraylist é zero based
                        End If
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                    Throw New Exception("Erro em impMappings, indice " & map.indice & ": " + ex.Message)
                End Try
            Next

            Try
                'envia os DEMAIS valores
                xmlCanonico.SelectSingleNode("/NFe/infNFe[1]/det[" & ct & "]/@nItem").InnerXml = "" & ct
            Catch ex As Exception
                Throw New Exception("Erro ao atribuir nItem. " + ex.Message + Geral.ObterStackTraceEmCascata(ex))
            End Try


            For Each map In detMappings
                Try
                    If valoresEntrada.detalhes(ct - 1)(map.indice - 1) <> "" Then 'zero based

                        'troca CONTADOR por ct para posicionar no item certo
                        xmlCanonico.SelectSingleNode(map.xPath.Replace("CONTADOR", ct)).InnerText = valoresEntrada.detalhes(ct - 1)(map.indice - 1) 'o arraylist é zero based
                        ' Log.registrarDebug(map.xPath & " - " & valoresEntrada.detalhes(ct - 1)(map.indice - 1))
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                    Throw New Exception("Erro em detMappings: " + ex.Message)
                End Try
            Next
            'se tiver mais campos de importação (nDraw), duplicar o xml
            'If valoresEntrada.exportacao(ct - 1) > 6 Then
            'Dim exp As New XmlDocument()
            'exp.LoadXml("<detExport><nDraw /><exportInd><nRE /><chNFe /><qExport /></exportInd></detExport>")
            'Dim dup_exp As XmlNode = exp.DocumentElement

            'Dim root As XmlNode = xmlCanonico.DocumentElement

            'Add the node to the document.
            'root.InsertAfter(dup_exp, xmlCanonico.SelectSingleNode("/NFe/infNFe[1]/det[CONTADOR]/prod[1]/detExport[1]/".Replace("CONTADOR", ct)))

            'End If
            'executando exportacao se tiver
            If valoresEntrada.exportacao.Count > 0 Then
                For Each map In mappings_exp
                    Try
                        If valoresEntrada.exportacao(ct - 1)(map.indice - 1) <> "" Then 'zero based
                            xmlCanonico.SelectSingleNode(map.xPath.Replace("CONTADOR", ct)).InnerText = valoresEntrada.exportacao(ct - 1)(map.indice - 1)
                        End If
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                        Throw New Exception("Erro em Mappings_exp: " + ex.Message)
                    End Try
                Next
            End If

            'executando importacao se tiver
            If valoresEntrada.importacao.Count > 0 Then
                For Each map In mappings_imp
                    Try
                        If valoresEntrada.importacao(ct - 1)(map.indice - 1) <> "" Then 'zero based
                            xmlCanonico.SelectSingleNode(map.xPath.Replace("CONTADOR", ct)).InnerText = valoresEntrada.importacao(ct - 1)(map.indice - 1)
                        End If
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                        Throw New Exception("Erro em Mappings_imp: " + ex.Message)
                    End Try
                Next
            End If
        Next

        Dim mensagem As String = String.Empty
        Try
            If empresa.cnpj <> Nothing Then
                mensagem = mensagem & "CNPJ: " & empresa.cnpj & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/CNPJ").InnerText = empresa.cnpj
            End If

            If nNF <> Nothing Then
                mensagem = mensagem & "nNF: " & nNF & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/ide/nNF").InnerText = nNF
            End If

            If Not xmlCanonico.SelectSingleNode("/NFe/infNFe/ide/nNF") Is Nothing Then
                mensagem = mensagem & "nNF XML: " & xmlCanonico.SelectSingleNode("/NFe/infNFe/ide/nNF").InnerText & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/ide/cNF").InnerText = xmlCanonico.SelectSingleNode("/NFe/infNFe/ide/nNF").InnerText
            End If

            mensagem = mensagem & "homologacao: " & (empresa.homologacao + 1).ToString() & vbCrLf
            xmlCanonico.SelectSingleNode("/NFe/infNFe/ide/tpAmb").InnerText = (empresa.homologacao + 1).ToString()

            mensagem = mensagem & "NFe" & gerarChaveDeAcesso(xmlCanonico) & vbCrLf
            xmlCanonico.SelectSingleNode("/NFe/infNFe").Attributes("Id").Value = "NFe" & gerarChaveDeAcesso(xmlCanonico)

            If Not xmlCanonico.SelectSingleNode("/NFe/infNFe").Attributes("Id").Value.Substring(46) Is Nothing Then
                mensagem = mensagem & "Id infNfe: " & xmlCanonico.SelectSingleNode("/NFe/infNFe").Attributes("Id").Value.Substring(46) & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/ide/cDV").InnerText = xmlCanonico.SelectSingleNode("/NFe/infNFe").Attributes("Id").Value.Substring(46)
            End If

            If Not xmlCanonico.SelectSingleNode("/NFe/infNFe/transp/veicTransp/placa").InnerText.Replace("-", String.Empty) Is Nothing Then
                mensagem = mensagem & "placa: " & xmlCanonico.SelectSingleNode("/NFe/infNFe/transp/veicTransp/placa").InnerText.Replace("-", String.Empty) & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/transp/veicTransp/placa").InnerText = xmlCanonico.SelectSingleNode("/NFe/infNFe/transp/veicTransp/placa").InnerText.Replace("-", String.Empty)
            End If

            If Not xmlCanonico.SelectSingleNode("/NFe/infNFe/transp/reboque/placa").InnerText.Replace("-", String.Empty) Is Nothing Then
                mensagem = mensagem & "placa reboque:" & xmlCanonico.SelectSingleNode("/NFe/infNFe/transp/reboque/placa").InnerText.Replace("-", String.Empty) & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/transp/reboque/placa").InnerText = xmlCanonico.SelectSingleNode("/NFe/infNFe/transp/reboque/placa").InnerText.Replace("-", String.Empty)
            End If

            If Not empresa.nome Is Nothing Then
                mensagem = mensagem & "empresa nome:" & empresa.nome & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/xNome").InnerText = empresa.nome
            End If

            If empresa.nomeFantasia <> Nothing Then
                mensagem = mensagem & "nome fantasia:" & empresa.nomeFantasia & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/xFant").InnerText = empresa.nomeFantasia
            End If

            If Not empresa.logradouro Is Nothing Then
                mensagem = mensagem & "logradouro: " & empresa.logradouro & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/enderEmit/xLgr").InnerText = empresa.logradouro
            End If

            If Not empresa.numero Is Nothing Then
                mensagem = mensagem & "empresa numero: " & empresa.numero & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/enderEmit/nro").InnerText = empresa.numero
            End If

            If empresa.complemento <> Nothing Then
                mensagem = mensagem & "complemento: " & empresa.complemento & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/enderEmit/xCpl").InnerText = empresa.complemento
                Log.registrarInfo(xmlCanonico.InnerText, "EntradaTxtService")
            End If

            mensagem = mensagem & "empresa bairro: "
            If Not empresa.bairro Is Nothing Then
                mensagem = mensagem & "BAIRRO NÃO ESTÁ NULO!!!"
                mensagem = mensagem & empresa.bairro & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/enderEmit/xBairro").InnerText = empresa.bairro
            End If

            mensagem = mensagem & "codigo municipio: "
            If empresa.codigoMunicipio <> Nothing Then
                mensagem = mensagem & empresa.codigoMunicipio & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/enderEmit/cMun").InnerText = empresa.codigoMunicipio
            End If

            mensagem = mensagem & "nomeMunicipio: "
            If Not empresa.nomeMunicipio Is Nothing Then
                mensagem = mensagem & empresa.nomeMunicipio & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/enderEmit/xMun").InnerText = empresa.nomeMunicipio
            End If

            mensagem = mensagem & "empresa uf: "
            If Not empresa.uf Is Nothing Then
                mensagem = mensagem & empresa.uf & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/enderEmit/UF").InnerText = empresa.uf
            End If

            mensagem = mensagem & "empresa cep: "
            If empresa.cep <> Nothing Then
                mensagem = mensagem & empresa.cep & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/enderEmit/CEP").InnerText = empresa.cep
            End If

            If empresa.fone <> Nothing Then
                mensagem = mensagem & "empresa fone:" & empresa.fone & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/enderEmit/fone").InnerText = empresa.fone
            End If

            If Not empresa.ie Is Nothing Then
                mensagem = mensagem & "empresa ie:" & empresa.ie & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/IE").InnerText = empresa.ie
            End If

            If empresa.iest <> Nothing Then
                mensagem = mensagem & "empresa iest:" & empresa.iest & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/IEST").InnerText = empresa.iest
            End If

            If empresa.im <> Nothing And empresa.cnae <> Nothing Then
                mensagem = mensagem & "empresa im: " & empresa.im & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/IM").InnerText = empresa.im
                mensagem = mensagem & "empresa cnae: " & empresa.cnae & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/CNAE").InnerText = empresa.cnae
            End If

            If empresa.crt <> Nothing Then
                mensagem = mensagem & "empresa crt: " & empresa.crt & vbCrLf
                xmlCanonico.SelectSingleNode("/NFe/infNFe/emit/CRT").InnerText = empresa.crt
            End If

        Catch ex As Exception
            Throw New Exception("Erro nas atribuições manuais de nós XML. " + ex.Message & vbCrLf + mensagem)
        End Try

        Return xmlCanonico
    End Function

    Public Shared Function processarTxt(ByVal text As String)
        Dim valor As String
        valor = text

        valor = Regex.Replace(valor, "¹", "1")
        valor = Regex.Replace(valor, "²", "2")
        valor = Regex.Replace(valor, "³", "3")
        valor = Regex.Replace(valor, "ª", "a")
        valor = Regex.Replace(valor, "º", "o")
        valor = valor.Replace(vbCr, "")
        valor = valor.Replace(vbLf, "carriageNFEreturn")
        valor = Regex.Replace(valor, "[^A-Za-z0-9\-/ÁÀÃÂÉÈÊÍÌÓÒÔÕÚÙÛÜÇáàâãéèêíìóòôõúùûüç#'!£$%^&*().,?|:@ ]", "")
        valor = valor.Replace("carriageNFEreturn", vbLf)

        Return valor
    End Function

    Private Shared Function obterMappings(ByVal arquivo As String) As ArrayList
        Dim retorno As New ArrayList

        Dim mapfile As String = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\" & arquivo
        Dim reader As New System.IO.StreamReader(mapfile)
        Dim strMappings As String = reader.ReadToEnd
        strMappings = strMappings.Replace(vbCrLf & vbCrLf, vbCrLf)
        Dim linha As String
        Try
            For Each linha In strMappings.Split(vbCrLf)
                Dim StrItens = linha.Split(";")
                Dim valor As New mapVO(StrItens(0), StrItens(1), StrItens(2))
                retorno.Add(valor)
            Next
        Catch ex As Exception
            Log.registrarErro(ex.Message & vbCrLf & ex.StackTrace, "EntradaTxtService")
        End Try

        Return retorno
    End Function

    Private Shared Function obterXMLCanonico() As XmlDocument
        Dim xmlfile As String = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\XMLCanonico.XML"
        Dim retorno As New XmlDocument
        retorno.Load(xmlfile)
        Return retorno
    End Function

    Public Shared Function obterXMLCanonico(ByVal xmlPath As String) As XmlDocument
        Dim xmlFile As String = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, xmlPath)
        Dim retorno As New XmlDocument
        retorno.Load(xmlFile)
        Return retorno
    End Function

    Public Shared Sub gerarXmlDeSaida(ByVal arquivoDeEntrada As String, ByVal pastadetrabalho As String, ByVal numerodanota As Integer, ByVal xslt As String)
        Try
            Log.registrarInfo("Aplicação de stylesheet no arquivo" & arquivoDeEntrada, "EntradaTxtService")

            'carrega o Stylesheet
            Dim xsl As New XslCompiledTransform
            xsl.Load(xslt)

            'aplica o stylesheet ao arquivo de entrada e gera o XML de Envio
            Dim pathXmlEnvio As String = pastadetrabalho & numerodanota & "_transformado.xml"
            xsl.Transform(arquivoDeEntrada, pathXmlEnvio)

            'Retorna o XML de envio
            '   Dim xmlEnvio As New XmlDocument
            'xmlEnvio.Load(pathXmlEnvio)
        Catch ex As Exception
            Log.registrarErro(ex.Message & vbCrLf & ex.StackTrace, "EntradaTxtService")
            Throw ex
        End Try
    End Sub

#End Region

    Public Shared Function validarXmlDeEnvio(ByVal PathXmlEnvio As String, ByVal empresa As empresaVO)
        resultadoValidacao = New System.Text.StringBuilder
        Dim myevent As ValidationEventHandler = New ValidationEventHandler(AddressOf ValidationEvent)

        'carrega o XSD
        Dim pathXSD As String
        If empresa.versao_nfe = "2.00" Then
            pathXSD = System.AppDomain.CurrentDomain.BaseDirectory() & "xsd/nfe_v2.00.xsd"
        Else
            pathXSD = System.AppDomain.CurrentDomain.BaseDirectory() & Geral.Parametro("arquivoSchemaNfe")
        End If

        Dim xschema As XmlSchema = XmlSchema.Read(New XmlTextReader(pathXSD), myevent)

        'configura a validação
        Dim xsettings As New XmlReaderSettings

        xsettings.ValidationType = ValidationType.Schema

        xsettings.Schemas.Add(xschema)

        'atribui o evento de validação
        AddHandler xsettings.ValidationEventHandler, myevent
        'faz a validação
        Using xreader As XmlReader = XmlReader.Create(PathXmlEnvio, xsettings)
            While xreader.Read
            End While
        End Using
        '"http://www.portalfiscal.inf.br/nfe", System.AppDomain.CurrentDomain.BaseDirectory() & "XSD\leiauteNFe_v1.10.xsd")

        Return resultadoValidacao.ToString
    End Function
    Public Shared Function validarXmlDPECDeEnvio(ByVal PathXmlEnvio As String)
        resultadoValidacao = New System.Text.StringBuilder
        Dim myevent As ValidationEventHandler = New ValidationEventHandler(AddressOf ValidationEvent)
        'carrega o XSD

        Dim xschema As XmlSchema = XmlSchema.Read(New XmlTextReader(System.AppDomain.CurrentDomain.BaseDirectory() & "XSD\envDPEC_v1.01.xsd"), myevent)

        'configura a validação
        Dim xsettings As New XmlReaderSettings

        xsettings.ValidationType = ValidationType.Schema

        xsettings.Schemas.Add(xschema)

        'atribui o evento de validação
        AddHandler xsettings.ValidationEventHandler, myevent
        'faz a validação
        Using xreader As XmlReader = XmlReader.Create(PathXmlEnvio, xsettings)
            While xreader.Read
            End While
        End Using
        '"http://www.portalfiscal.inf.br/nfe", System.AppDomain.CurrentDomain.BaseDirectory() & "XSD\leiauteNFe_v1.10.xsd")

        Return resultadoValidacao.ToString
    End Function

    Private Shared Sub ValidationEvent(ByVal sender As Object, ByVal e As ValidationEventArgs)
        'erros.Add(New erro(vargs.Severity, vargs.Message, vargs.Exception.SourceUri, vargs.Exception.LineNumber, vargs.Exception.LinePosition))
        'If vargs.Message.IndexOf("The element 'NFe' in namespace 'http://www.portalfiscal.inf.br/nfe' has incomplete content. List of possible elements expected: 'Signature' in namespace") = -1 Then
        '    Throw New Exception("Erro de validação de schema. Arquivo " & vargs.Exception.SourceUri & _
        '          " (" & vargs.Exception.LineNumber & "," & vargs.Exception.LinePosition & _
        '          "): severidade " & vargs.Severity & " - " & vargs.Message)
        'End If

        Try
            If Not e.Message.Contains("The element 'NFe' in namespace 'http://www.portalfiscal.inf.br/nfe' has incomplete content. List of possible elements expected: 'Signature' in namespace") _
            And Not e.Message.Contains("O elemento 'NFe' no espaço para nome 'http://www.portalfiscal.inf.br/nfe' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: 'Signature' no espaço para nome") Then
                Select Case e.Severity
                    Case XmlSeverityType.Error
                        resultadoValidacao.AppendLine("Erro(linha " & e.Exception.LineNumber & " posição " & e.Exception.LinePosition & "): " & e.Message)
                        resultadoValidacao.AppendLine("---")
                    Case XmlSeverityType.Warning
                        resultadoValidacao.AppendLine("Alerta: " & e.Message)
                        resultadoValidacao.AppendLine("---")
                End Select
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#Region "Acessorios"
    Public Shared Function gerarChaveDeAcesso(ByVal documento As XmlDocument) As String

        Dim chaveDeAcesso As String

        'obter o Código da UF          <cUF>35</cUF> (inteiro)
        chaveDeAcesso = documento.SelectSingleNode("/NFe/infNFe/ide/cUF").InnerText

        'obter AAMM da emissão     <dEmi>2008-01-22</dEmi> (formatar este campo em AAMM)
        chaveDeAcesso += documento.SelectSingleNode("/NFe/infNFe/ide/dhEmi").InnerText.Substring(2, 5).Replace("-", "")

        'obter CNPJ do emitente      <CNPJ>62462015000129</CNPJ> (inteiro)
        chaveDeAcesso += documento.SelectSingleNode("/NFe/infNFe/emit/CNPJ").InnerText

        'obter o Modelo                      <mod>55</mod> (inteiro)
        chaveDeAcesso += documento.SelectSingleNode("/NFe/infNFe/ide/mod").InnerText

        'obter a Série                         <serie>2</serie> (formatar máscara em 999)
        chaveDeAcesso += Format(CInt(documento.SelectSingleNode("/NFe/infNFe/ide/serie").InnerText), "000")

        'obter o Número da NFe         <nNF>871</nNF> (formatar máscara em 999999999)
        chaveDeAcesso += Format(CInt(documento.SelectSingleNode("/NFe/infNFe/ide/nNF").InnerText), "000000000")

        'obter a Forma de emissao
        chaveDeAcesso += documento.SelectSingleNode("/NFe/infNFe/ide/tpEmis").InnerText

        'obter o Código numérico       <cNF>871</cNF> (formatar máscara em 999999999)
        chaveDeAcesso += Format(CInt(documento.SelectSingleNode("/NFe/infNFe/ide/cNF").InnerText), "00000000")

        '--fim (07/04/2008)-----------------------

        'gerar o digito verificador
        chaveDeAcesso += gerarDigitoVerificador(chaveDeAcesso)

        Return chaveDeAcesso
    End Function

    Public Shared Function gerarChaveDeAcessoComNamespace(ByVal documento As XmlDocument) As String

        Dim chaveDeAcesso As String

        'obter o Código da UF          <cUF>35</cUF> (inteiro)
        chaveDeAcesso = documento.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ide' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cUF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText

        'obter AAMM da emissão     <dEmi>2008-01-22</dEmi> (formatar este campo em AAMM)
        chaveDeAcesso += documento.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ide' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='dEmi' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText.Substring(2, 5).Replace("-", "")

        'obter CNPJ do emitente      <CNPJ>62462015000129</CNPJ> (inteiro)
        chaveDeAcesso += documento.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='emit' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='CNPJ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText

        'obter o Modelo                      <mod>55</mod> (inteiro)
        chaveDeAcesso += documento.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ide' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='mod' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText

        'obter a Série                         <serie>2</serie> (formatar máscara em 999)
        chaveDeAcesso += Format(CInt(documento.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ide' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='serie' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText), "000")

        'obter o Número da NFe         <nNF>871</nNF> (formatar máscara em 999999999)
        chaveDeAcesso += Format(CInt(documento.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ide' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nNF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText), "000000000")

        'obter a Forma de emissao
        chaveDeAcesso += documento.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ide' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='tpEmis' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText.Trim()

        'obter o Código numérico       <cNF>871</cNF> (formatar máscara em 999999999)        
        chaveDeAcesso += Format(CInt(documento.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ide' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cNF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText), "00000000")

        '--fim (07/04/2008)-----------------------

        'gerar o digito verificador
        chaveDeAcesso += gerarDigitoVerificador(chaveDeAcesso)

        Return chaveDeAcesso
    End Function

    Private Shared Function gerarDigitoVerificador(ByVal idNFe As String) As String

        Dim numAtual As String
        Dim soma As Integer
        Dim numMult As Integer
        Dim DV As Integer
        Dim peso As Integer = 2
        Dim c As Integer

        For c = idNFe.Length - 1 To 0 Step -1
            numAtual = CInt(idNFe.Substring(c, 1))
            numMult = numAtual * peso
            soma += numMult
            If peso = 9 Then
                peso = 2
            Else
                peso = peso + 1
            End If
        Next
        DV = 11 - (soma Mod 11)
        If DV > 9 Then
            DV = 0
        End If

        Return DV

    End Function


    Private Shared Function somarvolumes(ByVal vol As String)
        Return CInt(vol.Trim(" "))
    End Function

#End Region

End Class
