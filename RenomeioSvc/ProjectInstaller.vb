

Public Class ProjectInstaller

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent
        Dim name As String = New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).Name.Replace(".exe", "").ToUpper
        Me.svcInstaller.ServiceName += name
        Me.svcInstaller.DisplayName += " (" & name & ")"
    End Sub

End Class
