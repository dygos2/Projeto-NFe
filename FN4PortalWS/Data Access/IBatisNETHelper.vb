Imports IBatisNet.Common.Utilities
Imports IBatisNet.DataMapper
Imports IBatisNet.DataMapper.Configuration
Public Class IBatisNETHelper
    Private Shared _mapper As ISqlMapper = Nothing

    Private Sub New()

    End Sub

    Protected Shared Sub Configure(ByVal objeto As Object)
        _mapper = Nothing

    End Sub

    Protected Shared Sub InitMapper()
        Try
            Dim handler As ConfigureHandler = New ConfigureHandler(AddressOf Configure)
            Dim builder As New DomSqlMapBuilder
            _mapper = builder.ConfigureAndWatch(handler)
        Catch ex As Exception

        End Try
    End Sub

    Public Shared ReadOnly Property Instance() As ISqlMapper
        Get
            If _mapper Is Nothing Then
                InitMapper()
            End If
            Return _mapper
        End Get
    End Property

End Class
