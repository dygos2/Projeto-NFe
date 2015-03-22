Imports FN4Common
Imports FN4Common.Helpers
Imports System.Text
Imports System.Timers
Imports FN4SincroniaCtl.DataAccess
Imports System.IO
Imports System.Net
Imports System.Windows
Imports System
Imports System.Xml


Public Class ProcFTP

    Private WithEvents _tm As Timer

    Const Servico As String = "SincroniasFTP"

    Public Sub New()
        _tm = New Timer(Geral.Parametro("intervaloDeVarreduraFTP"))
        _tm.Enabled = False
        Log.registrarInfo("Servico de Sincronia Iniciado - Intervalos de " & (CInt(Geral.Parametro("intervaloDeVarreduraFTP")) / 1000) & " segundos", Servico)
    End Sub

    Public Sub run()
        _tm.Start()
    End Sub

    Public Sub pause()
        'Log.registrarInfo("Servico de Sincronia Pausado", Servico)
        _tm.Stop()
    End Sub

    Sub limpa_pastas()
        Dim caminhoApp = New System.IO.FileInfo(Application.StartupPath & "/ftp/").ToString

        'limpando pastas
        Dim strDirectory As String = caminhoApp & "xml/"
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(strDirectory)
            Kill(foundFile)
        Next
        strDirectory = caminhoApp & "pdf/"
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(strDirectory)
            Kill(foundFile)
        Next
        strDirectory = caminhoApp & "txt/"
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(strDirectory)
            Kill(foundFile)
        Next
    End Sub
    Public Sub executarEnvioDeArquivosFTP() Handles _tm.Elapsed
        Me.pause()
        Dim nfe_num As Integer

        Try
            'Log.registrarInfo("rodou", Servico)

            Dim empresas_proc = varreEmpresas()
            Dim caminhoApp = New System.IO.FileInfo(Application.StartupPath & "/ftp/").ToString
            Dim srvid As Integer = Geral.Parametro("srvid")
            Dim tp_amb As Integer = Geral.Parametro("tp_amb")
            Dim now As DateTime = DateTime.Now
            Dim ftps As String

            tp_amb += 1


            'TODO:teste, tirar
            'srvid = 2
            'tp_amb = 2

            If empresas_proc.Count > 0 Then
                Log.registrarInfo("Iniciando sincronia FTP em " & empresas_proc.Count & " empresa(s). Inicio em " & now, Servico)
                'limpando pastas
                limpa_pastas()
            End If

            Dim i = 0
            For i = 0 To empresas_proc.Count - 1
                ftps = ""

                'passando de 0 para a sincronizar
                FN4Common.DataAccess.notas.SincIniNotas(empresas_proc(i).NFe_emit_CNPJ, 1, 0)
                'MsgBox(1)

                Dim confiFTP As List(Of FN4Common.configuracaoVO) = FN4Common.configuracaoDAO.obterFTPConfig(empresas_proc(i).idEmpresa)
                'MsgBox(2)

                Dim str_servicos As String = ""
                Dim qtd As Integer
                Dim arr_ftp As New List(Of FtpVO)

                Dim f = 0
                For f = 0 To confiFTP.Count - 1
                    If (InStr(1, confiFTP(f).chave, "xml_path") > 0) Then
                        str_servicos = str_servicos & "_xml"
                    End If

                    If (confiFTP(f).chave = "ftp") Then
                        qtd = confiFTP(f).valor
                    End If

                    If (InStr(1, confiFTP(f).chave, "txt_path") > 0) Then
                        str_servicos = str_servicos & "_txt"
                    End If

                    If (InStr(1, confiFTP(f).chave, "pdf_path") > 0) Then
                        str_servicos = str_servicos & "_pdf"
                    End If
                Next f

                For f = 1 To qtd
                    Dim ftp As New FtpVO()

                    For z = 0 To confiFTP.Count - 1
                        If InStr(confiFTP(z).chave, "host_" & f) > 0 Then
                            ftp.ftp_host = confiFTP(z).valor
                            ftps = ftps + confiFTP(z).valor + " / "
                        End If
                        If InStr(confiFTP(z).chave, "user_" & f) > 0 Then
                            ftp.user = confiFTP(z).valor
                        End If
                        If InStr(confiFTP(z).chave, "pass_" & f) > 0 Then
                            ftp.pass = confiFTP(z).valor
                        End If
                        If InStr(confiFTP(z).chave, "pdf_path_" & f) > 0 Then
                            ftp.pdf_path = confiFTP(z).valor
                        End If
                        If InStr(confiFTP(z).chave, "txt_path_" & f) > 0 Then
                            ftp.txt_path = confiFTP(z).valor
                        End If
                        If InStr(confiFTP(z).chave, "xml_path_" & f) > 0 Then
                            ftp.xml_path = confiFTP(z).valor
                        End If
                    Next
                    arr_ftp.Add(ftp)
                Next
                ftps = Left(ftps, ftps.Length - 3)

                Dim notasSinc As List(Of FN4Common.notaVO) = FN4Common.DataAccess.notas.obterNotasSinc(empresas_proc(i).NFe_emit_CNPJ)
                Log.registrarInfo("Sincronizando (FTP) CNPJ - " & empresas_proc(i).NFe_emit_CNPJ & " em " & notasSinc.Count & " notas. De " & notasSinc(0).NFe_ide_nNF & " até " & notasSinc(notasSinc.Count - 1).NFe_ide_nNF, Servico)

                Dim n = 0
                For n = 0 To notasSinc.Count - 1
                    Dim xml1, xml2 As New XmlDocument()
                    Dim arr_pasta As Array = Split(notasSinc(n).pastaDeTrabalho, "\")
                    nfe_num = notasSinc(n).NFe_ide_nNF
                    Log.registrarInfo("== Sincronizando NOTA - " & notasSinc(n).NFe_ide_nNF, Servico)

                    'MsgBox("loading" & notasSinc(n).pastaDeTrabalho & notasSinc(n).NFe_ide_nNF & "_procNFe.xml")

                    xml1.Load(notasSinc(n).pastaDeTrabalho & notasSinc(n).NFe_ide_nNF & "_procNFe.xml")
                    'TODO: teste, tirar
                    'xml1.Load("C:\TFS\tmp\11_procNFe.xml")
                    xml2 = xml1
                    xml1.LoadXml(xml1.InnerXml.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))

                    If (InStr(1, str_servicos, "xml") > 0) Then
                        xml2.Save(caminhoApp & "xml/" & notasSinc(n).NFe_ide_nNF & "_procNFe.xml")
                        'envia por ftp
                        For xftp = 0 To qtd - 1
                            If Not IsNothing(arr_ftp(xftp).xml_path) Then
                                envia_ftp(caminhoApp & "xml/" & notasSinc(n).NFe_ide_nNF & "_procNFe.xml", arr_ftp(xftp).xml_path & notasSinc(n).NFe_ide_nNF & "_procNFe.xml", arr_ftp(xftp))
                            End If
                        Next

                    End If
                    If (InStr(1, str_servicos, "txt") > 0) Then
                        Dim caminho_txt As String = geraTxtNota(xml1, notasSinc(n).num_pedido, notasSinc(n).emailDestinatario, notasSinc(n))
                        'envia por ftp
                        For xftp = 0 To qtd - 1
                            If Not IsNothing(arr_ftp(xftp).txt_path) Then
                                envia_ftp(caminho_txt, arr_ftp(xftp).txt_path & "NFe" & notasSinc(n).NFe_infNFe_id & ".txt", arr_ftp(xftp))
                            End If
                        Next

                    End If
                    If (InStr(1, str_servicos, "pdf") > 0) Then
                        Dim caminho_pdf As String = grava_pdf("http://amazon-nfe4web.elasticbeanstalk.com/nfe4web/gera_pdfs/?cnpj=" & notasSinc(n).NFe_emit_CNPJ & "&ano=" & arr_pasta(5) & "&mes=" & arr_pasta(6) & "&dia=" & arr_pasta(7) & "&nnfe=" & arr_pasta(8) & "&arq=" & notasSinc(n).NFe_ide_nNF & "_procNFe&ch=" & notasSinc(n).NFe_infNFe_id & "&srvid=" & srvid & "&tp_amb=" & tp_amb & "&dest_saida=D", notasSinc(n))
                        For xftp = 0 To qtd - 1
                            If Not IsNothing(arr_ftp(xftp).pdf_path) Then
                                envia_ftp(caminho_pdf, arr_ftp(xftp).pdf_path & "NFe" & notasSinc(n).NFe_infNFe_id & ".pdf", arr_ftp(xftp))
                            End If
                        Next
                    End If

                    inserirHistorico(32, ftps, notasSinc(n))

                Next n

                'passando de processada para sincronizada
                FN4Common.DataAccess.notas.SincIniNotas(empresas_proc(i).NFe_emit_CNPJ, 2, 1)

            Next i

            now = DateTime.Now


            If empresas_proc.Count > 0 Then
                Log.registrarInfo("Finalizando sincronia FTP em " & empresas_proc.Count & " empresa(s). Final em " & now, Servico)
            End If
        Catch ex As Exception
            Log.registrarInfo("Erro de sincronia - Nota " & nfe_num & " erro: " & ex.Message, Servico)
        Finally
            Me.run()
        End Try

    End Sub
    Sub envia_ftp(ByVal file_path As String, ByVal file_server As String, ByVal ftp_obj As FtpVO)
        Try
            Dim miUri As String = "ftp://" & ftp_obj.ftp_host & file_server

            Dim ftp As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(miUri), System.Net.FtpWebRequest)
            'Dim miRequest As Net.FtpWebRequest = Net.WebRequest.Create(miUri)
            ftp.Credentials = New Net.NetworkCredential(ftp_obj.user, ftp_obj.pass)
            ftp.KeepAlive = True
            ftp.UseBinary = True
            ftp.Method = Net.WebRequestMethods.Ftp.UploadFile

            Dim bFile() As Byte = System.IO.File.ReadAllBytes(file_path)
            Dim miStream As System.IO.Stream = ftp.GetRequestStream()
            miStream.Write(bFile, 0, bFile.Length)

            bFile = Nothing
            miStream.Close()
            miStream.Dispose()
            'System.Threading.Thread.Sleep(3000)
        Catch ex As Exception
            Log.registrarInfo("Erro de envio pelo FTP - " & ex.Message, Servico)
        End Try

        'Dim clsRequest As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(miUri), System.Net.FtpWebRequest)

    End Sub
    Public Function grava_pdf(ByVal nota_url_path As String, ByVal nota As FN4Common.notaVO) As String
        Dim caminhoApp = New System.IO.FileInfo(Application.StartupPath & "/ftp/pdf/NFe" & nota.NFe_infNFe_id & ".pdf").ToString
        Try
            Dim _WebClient As New System.Net.WebClient()
            'AddHandler _WebClient.DownloadFileCompleted, AddressOf Downloaded
            '_WebClient.DownloadFile(nota_url_path, caminhoApp)
            _WebClient.DownloadFile(New Uri(nota_url_path), (caminhoApp))
            System.Threading.Thread.Sleep(3000)

        Catch ex As Exception
            Log.registrarInfo("Erro de gravaçao do PDF (url - " & nota_url_path & " / pdf - " & Application.StartupPath & "/ftp/pdf/NFe" & nota.NFe_infNFe_id & ".pdf" & ") - Erro: " & ex.Message, Servico)
        End Try
        Return caminhoApp

    End Function
    Private Sub Downloaded()
        'MsgBox("Foi!")
        'Console.Write("Foi")
    End Sub

    Private Function varreEmpresas() As List(Of notasProcVO)
        Return notasProcDAO.obterEmpresas()

    End Function


    Public Function geraTxtNota(ByVal xmlNota As XmlDocument, ByVal num_ped As String, ByVal emailDest As String, ByVal nota As FN4Common.notaVO) As String


        Dim caminhoApp = New System.IO.FileInfo(Application.StartupPath & "/ftp/txt/").ToString
        Dim escrever As System.IO.StreamWriter

        Dim nrNota = xmlNota.SelectSingleNode("//nNF").InnerXml
        Dim serieNota = xmlNota.SelectSingleNode("//serie").InnerXml

        Dim novaLinha As String
        Dim camposNota = camposNotaXml(xmlNota)
        Dim ct As Integer
        ct = 0

        Try
            escrever = File.CreateText(caminhoApp + "NFe" + nota.NFe_infNFe_id + ".txt")

            Dim detalhes = xmlNota.SelectNodes("//det")
            Dim det As XmlElement
            For Each det In detalhes
                ct = ct + 1
                Dim xml_tmp As New XmlDocument

                xml_tmp.LoadXml(det.OuterXml)

                camposNota.vlProduto = xml_tmp.SelectSingleNode("//vUnCom").InnerXml
                camposNota.qtd = Split(xml_tmp.SelectSingleNode("//qCom").InnerXml, ".")(0)
                camposNota.partNumber = xml_tmp.SelectSingleNode("//cProd").InnerXml
                camposNota.vlProdsTot = xml_tmp.SelectSingleNode("//vProd").InnerXml 'alterado em 23/06/2014 solicitado pelo Thiago

                Try
                    camposNota.EAN = xml_tmp.SelectSingleNode("//infAdProd").InnerXml
                Catch ex As Exception
                    camposNota.EAN = xml_tmp.SelectSingleNode("//cEAN").InnerXml
                End Try

                novaLinha = "FOB;" + num_ped.ToString + ";" + "venda;" + "0;" + camposNota.nomeCli.ToString + ";" +
                                    camposNota.CPFcli.ToString + ";" + camposNota.endCli.ToString + ";" + camposNota.nrEndCli.ToString + ";" + camposNota.complEnd.ToString + ";" + camposNota.bairroCli.ToString + ";" +
                                    camposNota.cidadeCli + ";" + camposNota.estCli + ";" + camposNota.cepCli.ToString + ";" + emailDest + ";" +
                                    camposNota.DDD.ToString + ";" + camposNota.telefone.ToString + ";" + camposNota.nrNota.ToString + ";" + camposNota.serieNota.ToString + ";" + camposNota.dtNota.ToString + ";" +
                                    camposNota.vlNFeTot.ToString + ";" + camposNota.vlProdsTot.ToString + ";;" + camposNota.NFeChave.ToString + ";" + camposNota.qtd.ToString + ";" +
                                    camposNota.EAN.ToString + ";" + camposNota.partNumber.ToString + ";" + camposNota.vlProduto.ToString + ";" + camposNota.serviceType.ToString + ";" + camposNota.giftPackage.ToString
                If (detalhes.Count > ct) Then
                    escrever.WriteLine(novaLinha & vbCr)
                Else
                    escrever.Write(novaLinha)
                End If

                'escrever.Write("(1)" & novaLinha & vbCr)
                'escrever.WriteLine("(2)" & novaLinha & vbCr)

                'escrever.Write("(3)" & novaLinha & vbCrLf)
                'escrever.WriteLine("(4)" & novaLinha & vbCrLf)

                'escrever.Write("(5)" & novaLinha & vbCr & vbCrLf)
                'escrever.WriteLine("(6)" & novaLinha)

                'escrever.Write("(7)" & novaLinha)

            Next

            escrever.Flush()
            escrever.Close()
        Catch ex As Exception
            Log.registrarInfo("Erro de gerar TXT da Nota ( " & caminhoApp + nrNota + "_" + serieNota + ".txt" & " ) - " & ex.Message, Servico)
        End Try

        Return caminhoApp + "NFe" + nota.NFe_infNFe_id + ".txt"

    End Function

    Private Function camposNotaXml(ByVal xmlNota As XmlDocument) As camposNotaXMLVO
        Dim campos As New camposNotaXMLVO

        campos.nomeCli = return_notnull("//dest/xNome", xmlNota)
        campos.endCli = return_notnull("//enderDest/xLgr", xmlNota)
        campos.nrEndCli = return_notnull("//enderDest/nro", xmlNota)
        campos.bairroCli = return_notnull("//enderDest/xBairro", xmlNota)
        campos.cidadeCli = return_notnull("//enderDest/xMun", xmlNota)
        campos.estCli = return_notnull("//enderDest/UF", xmlNota)
        campos.cepCli = Right(String.Concat("00000000", return_notnull("//enderDest/CEP", xmlNota)), 8)
        campos.complEnd = return_notnull("//enderDest/xCpl", xmlNota)
        campos.DDD = return_notnull("//enderDest/fone", xmlNota)
        campos.CPFcli = return_notnull("//dest/CPF", xmlNota)

        If campos.DDD.ToString.Length > 3 Then
            campos.DDD = campos.DDD.ToString.Substring(0, 2)
        End If
        campos.telefone = return_notnull("//enderDest/fone", xmlNota)
        If campos.telefone.ToString.Length > 7 Then
            campos.telefone = Right(campos.telefone, campos.telefone.ToString.Length - 2)
        End If

        campos.nrNota = return_notnull("//ide/nNF", xmlNota)
        campos.serieNota = return_notnull("//ide/serie", xmlNota)
        campos.dtNota = return_notnull("//infProt/dhRecbto", xmlNota)
        If campos.dtNota.ToString.Length > 2 Then
            campos.dtNota = campos.dtNota.Substring(0, 10)
            campos.dtNota = String.Concat(Split(campos.dtNota, "-")(2), "/", Split(campos.dtNota, "-")(1), "/", Right(Split(campos.dtNota, "-")(0), 2))
        End If

        campos.vlNFeTot = return_notnull("//total/ICMSTot/vNF", xmlNota)
        'campos.vlProdsTot = return_notnull("//total/ICMSTot/vProd", xmlNota)
        campos.NFeChave = return_notnull("//chNFe", xmlNota)

        campos.CFOP = return_notnull("//CFOP", xmlNota)
        campos.qtd = return_notnull("//qCom", xmlNota)
        'campos.vlProduto = return_notnull("//vProd", xmlNota)

        campos.serviceType = "E"
        campos.giftPackage = 0
        campos.EAN = ""
        campos.partNumber = ""

        Return campos

        'campos.endCli = xmlNota.GetElementsByTagName("enderDest").ItemOf(0).Item("xLgr").InnerXml '.Substring(0, 80)
        'campos.nrEndCli = xmlNota.GetElementsByTagName("enderDest").ItemOf(0).Item("nro").InnerXml
        'campos.bairroCli = xmlNota.GetElementsByTagName("enderDest").ItemOf(0).Item("xBairro").InnerXml
        'campos.cidadeCli = xmlNota.GetElementsByTagName("enderDest").ItemOf(0).Item("xMun").InnerXml
        'campos.estCli = xmlNota.GetElementsByTagName("enderDest").ItemOf(0).Item("UF").InnerXml
        'campos.cepCli = xmlNota.GetElementsByTagName("enderDest").ItemOf(0).Item("CEP").InnerXml
        'campos.DDD = xmlNota.GetElementsByTagName("enderDest").ItemOf(0).Item("fone").InnerXml.Substring(0, 2)
        'campos.telefone = xmlNota.GetElementsByTagName("enderDest").ItemOf(0).Item("fone").InnerXml.Substring(2, 8)
        'campos.nrNota = xmlNota.GetElementsByTagName("ide").ItemOf(0).Item("nNF").InnerXml
        'campos.serieNota = xmlNota.GetElementsByTagName("ide").ItemOf(0).Item("serie").InnerXml
        'campos.dtNota = Replace(xmlNota.GetElementsByTagName("infProt").ItemOf(0).Item("dhRecbto").InnerXml.Substring(0, 10), "-", "")
        'campos.vlNFeTot = xmlNota.GetElementsByTagName("total").ItemOf(0).Item("ICMSTot").Item("vNF").InnerXml
        'campos.vlProdsTot = xmlNota.GetElementsByTagName("total").ItemOf(0).Item("ICMSTot").Item("cProd").InnerXml
        'campos.NFeChave = xmlNota.SelectSingleNode("chNFe").InnerXml

    End Function

    Function return_notnull(ByVal xpath As String, ByVal xml_da_nota As XmlDocument) As String

        Dim teste_retorno As String
        Try
            teste_retorno = xml_da_nota.SelectSingleNode(xpath).InnerXml()
        Catch ex As Exception
            teste_retorno = ""
        End Try

        Return teste_retorno

    End Function

    Public Sub EnviarArquivoFTP(ByVal caminhoArquivoFtp As String, ByVal arquivo As String)
        'Informe o nome servidor ftp ou ip 
        Dim ftphost As String = "127.0.0.1"

        Dim caminhoFTP As String = "ftp://" & ftphost & caminhoArquivoFtp
        Dim ftp As FtpWebRequest = DirectCast(FtpWebRequest.Create(caminhoFTP), FtpWebRequest)

        'define as credenciais 
        '   ftp.Credentials = New NetworkCredential(txtUsuario.Text, txtSenha.Text)
        'define o tipo de ação 
        ftp.KeepAlive = True
        ftp.UseBinary = True
        ftp.Method = WebRequestMethods.Ftp.UploadFile
        'trata o retorno
        Dim fs As FileStream = File.OpenRead(arquivo)
        Dim buffer As Byte() = New Byte(fs.Length - 1) {}
        fs.Read(buffer, 0, buffer.Length)
        fs.Close()
        Dim ftpstream As Stream = ftp.GetRequestStream()
        ftpstream.Write(buffer, 0, buffer.Length)
        ftpstream.Close()
    End Sub




#Region "Acessorios"

    Private Sub inserirHistorico(ByVal tipo As String, ByVal descricao As String, ByVal nota As FN4Common.notaVO)
        Dim hist As New historicoVO(tipo, descricao, nota.NFe_ide_nNF, nota.NFe_emit_CNPJ, nota.serie)

        notaDAO.inserirHistorico(hist)
        'Retorno.escreverRetorno(nota, DateTime.Now, tipo, descricao)
    End Sub
#End Region

End Class