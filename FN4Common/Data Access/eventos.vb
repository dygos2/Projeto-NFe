Namespace DataAccess
    Public Class eventos
        Public Shared Function obterEvento(ByVal idEvento As Integer, ByVal chave As String, ByVal tipo As Integer) As FN4Common.eventoVO

            Dim evento As New eventoVO

            evento.infEvento_nSeqEvento = idEvento
            evento.NFe_infNFe_id = chave
            evento.infEvento_tpEvento = tipo

            Return IBatisNETHelper.Instance.QueryForObject("obterEvento", evento)

        End Function

        Public Shared Function obterQuantidadeDeEventos(ByVal cnpj As String) As Integer
            Dim ht As New Hashtable
            'ht.Add("infEvento_tpEvento", tipoDeEvento)
            ht.Add("NFe_emit_CNPJ", cnpj)

            Return IBatisNETHelper.Instance.QueryForObject("obterQuantidadeDeEventos", ht)

        End Function

        Public Shared Function obterCartasCorrecaoParaEnvio() As List(Of FN4Common.eventoVO)
            'Dim ht As New Hashtable
            Return IBatisNETHelper.Instance.QueryForList(Of eventoVO)("obterCartasCorrecaoParaEnvio", Nothing) 'ht)

        End Function
        Public Shared Function obterCancelamentoParaEnvio() As List(Of FN4Common.eventoVO)
            'Dim ht As New Hashtable
            Return IBatisNETHelper.Instance.QueryForList(Of eventoVO)("obterCancelamentosParaEnvio", Nothing) 'ht)

        End Function

        Public Shared Function obterListaDeEventos(ByVal cnpj As String, ByVal NFe_infNFe_id As String) As List(Of FN4Common.eventoVO)
            Dim ht As New Hashtable
            'ht.Add("registroInicial", registroInicial)
            'ht.Add("registrosPorPagina", registrosPorPagina)
            'ht.Add("infEvento_tpEvento", tipoDeEvento)
            ht.Add("NFe_infNFe_id", NFe_infNFe_id)
            ht.Add("NFe_emit_CNPJ", cnpj)

            Return IBatisNETHelper.Instance.QueryForList(Of eventoVO)("obterListaDeEventos", ht)
        End Function

        Public Shared Function inserirEvento(ByVal evento As eventoVO) As Integer
            Dim seq As Integer = IBatisNETHelper.Instance.QueryForObject("obterIdProximoEvento", evento)

            Dim ultimoEvento As eventoVO
            ultimoEvento = Nothing

            If seq = -1 Then
                seq = 1
            Else
                'obter o ultimo evento inserido
                ultimoEvento = obterEvento(seq - 1, evento.NFe_infNFe_id, evento.infEvento_tpEvento)
            End If

            If Not ultimoEvento Is Nothing Then
                'se for carta de correcao
                Select (ultimoEvento.infEvento_tpEvento)
                    Case 110110 'cce
                        'se status for 21 (aprovado) - inserir o proximo
                        If ultimoEvento.statusEvento = 21 Then
                            evento.infEvento_nSeqEvento = seq
                            Log.registrarInfo(String.Concat("Nova CCe adicionada para a empresa - ", evento.NFe_infNFe_id.Substring(6, 14)), "EnvioEventos")
                            IBatisNETHelper.Instance.Insert("inserirEvento", evento)
                            Return seq

                            'se for 3 - excluir e substituir o anterior   
                        ElseIf ultimoEvento.statusEvento = 3 Then
                            Log.registrarInfo(String.Concat("CCe substituindo a ultima CCe devido a erro - ", evento.NFe_infNFe_id.Substring(6, 14)), "EnvioEventos")
                            IBatisNETHelper.Instance.Delete("excluirEventos", ultimoEvento)

                            evento.infEvento_nSeqEvento = seq - 1
                            IBatisNETHelper.Instance.Insert("inserirEvento", evento)
                            Return evento.infEvento_nSeqEvento
                        ElseIf ultimoEvento.statusEvento = 0 Then
                            'se for 0
                            'Throw New Exception("Ja existe uma carta de correcao em processamento. Nao e possivel processar uma nova carta de correcao")
                            Log.registrarInfo(String.Concat("CCe não pode ser enviada pois antiga ainda está em processamento - ", evento.NFe_infNFe_id.Substring(6, 14)), "EnvioEventos")
                            Return -1
                        End If
                    Case 110111 'cancelamento
                        'se status for 21 (aprovado) não deixa inserir o proximo evento
                        If ultimoEvento.statusEvento = 21 Then
                            Log.registrarInfo("NFe já cancelada por evento anterior. Solicitar a Consulta da NF-e pelo monitor.", "EnvioEventos")
                            Return 0

                            'se for 3 - excluir e substituir o anterior   
                        ElseIf ultimoEvento.statusEvento = 3 Then
                            Log.registrarInfo(String.Concat("Cancelamento substituindo a ultima enviada devido a erro - ", evento.NFe_infNFe_id.Substring(6, 14)), "EnvioEventos")
                            IBatisNETHelper.Instance.Delete("excluirEventos", ultimoEvento)

                            evento.infEvento_nSeqEvento = 1
                            IBatisNETHelper.Instance.Insert("inserirEvento", evento)
                            Return evento.infEvento_nSeqEvento
                        ElseIf ultimoEvento.statusEvento = 0 Then
                            'se for 0
                            'Throw New Exception("Ja existe uma carta de correcao em processamento. Nao e possivel processar uma nova carta de correcao")
                            Log.registrarInfo(String.Concat("Cancelamento não pode ser enviado pois antigo ainda está em processamento - ", evento.NFe_infNFe_id.Substring(6, 14)), "EnvioEventos")
                            Return -1
                        End If
                End Select

            Else
                'primeiro novo evento
                evento.infEvento_nSeqEvento = seq
                IBatisNETHelper.Instance.Insert("inserirEvento", evento)
                Return seq
            End If



        End Function

        Public Shared Sub alterarEvento(ByVal evento As eventoVO)
            IBatisNETHelper.Instance.Update("alterarEvento", evento)
        End Sub



    End Class
End Namespace

