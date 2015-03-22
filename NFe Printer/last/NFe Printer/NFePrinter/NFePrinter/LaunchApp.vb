
'Use only ONE of these Main methods.
Public Module LaunchApp

    Public Sub Main()
        'Turn visual styles back on
        Application.EnableVisualStyles()

        'Run the application using AppContext
        Application.Run(New AppContext)

        ''You can also run the application using a main form
        'Application.Run(New MainForm)

        ''Or in a default context with no user interface at all
        'Application.Run()
    End Sub

    'Public Sub Main(ByVal cmdArgs() As String)
    '    Application.EnableVisualStyles()

    '    Dim UseTray As Boolean = False

    '    For Each Cmd As String In cmdArgs
    '        If Cmd.ToLower = "-tray" Then
    '            UseTray = True
    '            Exit For
    '        End If
    '    Next

    '    If UseTray Then
    '        Application.Run(New AppContext)
    '    Else
    '        Application.Run(New MainForm)
    '    End If
    'End Sub

    'Public Function Main() As Integer
    'End Function

    'Public Function Main(ByVal cmdArgs() As String) As Integer
    'End Function

End Module
