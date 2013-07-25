Imports System.Windows.Forms
Imports System.Net
Imports System.IO

Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Runtime.InteropServices
Imports System.Xml
Imports System.ComponentModel
Imports System.Threading




Public Class MDI

    Public m_AsyncWorker As BackgroundWorker = New BackgroundWorker()
    Public Sub New(ByVal pr As String)

        
        ' This call is required by the designer.
        InitializeComponent()
        Dim ptr As New frmprinter("1")
        ptr.Show()
        ptr.btnsave.Text = "Configurar parâmetros"
        Icon = My.Resources.TrayIcon
        Me.Close()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New()

        ' This call is required by the designer.

        InitializeComponent()
        Icon = My.Resources.TrayIcon
        m_AsyncWorker.WorkerReportsProgress = True
        m_AsyncWorker.WorkerSupportsCancellation = True
        AddHandler m_AsyncWorker.DoWork, AddressOf BackgroundWorker1_DoWork
        AddHandler m_AsyncWorker.ProgressChanged, AddressOf BackgroundWorker1_ProgressChanged
        AddHandler m_AsyncWorker.RunWorkerCompleted, AddressOf BackgroundWorker1_RunWorkerCompleted
            
        m_AsyncWorker.RunWorkerAsync()
        ' Add any initialization after the InitializeComponent() call.

       

    End Sub




    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer
    Public status_while As Boolean

    Private Sub MDI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AxAcroPDF1.LoadFile("D:\dutyslip\35130111051549000121550100000000031000000038.pdf")

       
        ' process_start()
    End Sub
    

    Private Sub btnselectprinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselectprinter.Click
        Dim ptr As New frmprinter()
        ptr.Show()
        Me.Hide()
    End Sub

    Private Function GetFileStream(ByVal p1 As Object) As MemoryStream
        Throw New NotImplementedException
    End Function

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

    End Sub
   
    Private Sub MDI_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If (Me.DialogResult = DialogResult.Abort) Then

            Icon = My.Resources.TrayIcon
            Me.Hide()
        End If
    End Sub

    Private Sub MDI_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If (Me.WindowState = FormWindowState.Minimized) Then
            Icon = My.Resources.TrayIcon
            Me.Hide()

        End If
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        status_while = True

    End Sub
    Dim st As Thread
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
        'st = New Threading.Thread(AddressOf process_start)
        'st.Start()

        process_start()




    End Sub
    Dim ds As New DataSet()
    Dim ds_login As New DataSet()

#Region " process for printing pdf "
    Public dt_check_url As DateTime
    Public stop_while As Boolean
    Dim doc As XmlDocument
    Dim m_node As XmlNode
    Dim url As String
    Dim remove() As Char
    Dim req As WebRequest
    Dim str As WebResponse
    Dim reader As StreamReader
    Dim Data As String
    Dim status() As String
    Dim newurl() As String
    Dim newurl_new() As String
    Dim data_arr() As String
    Dim data_arr_first_for_loop() As String
    Dim for_id_n_path() As String
    Dim pdf_id As String
    Dim url_new As String
    Dim series As String
    Dim pdfname1 As String
    Dim wc1 As New WebClient()
    Dim ip As String = System.Net.Dns.GetHostName()
    Dim result As String

    Public Sub process_start()


        dt_check_url = System.DateTime.Now
        While (True)
            doc = New XmlDocument()
            doc.Load(Application.StartupPath & "\XMLDOC.XML")
            'm_nodelist = doc.SelectNodes("/Detail/printer")
            m_node = doc.SelectSingleNode("/Detail/Last_pdf_print")
            If (m_node IsNot Nothing) Then
                m_node.ChildNodes(0).InnerText = System.DateTime.Now

                doc.Save(Application.StartupPath & "\XMLDOC.XML")

            End If
            Try
                If (status_while = False) Then
                    ds_login = New DataSet()
                    ds_login.ReadXml(Application.StartupPath & "\XMLDOC_login_detail.xml")
                    ds = New DataSet()
                    ds.ReadXml(Application.StartupPath & "\XMLDOC.xml")
                    If ((System.DateTime.Now.Subtract(Convert.ToDateTime(ds.Tables("Last_pdf_print").Rows(0)(0).ToString()))).TotalMinutes < 90) Then
                        ' Dim ds As New DataSet()
                        'ds.ReadXml(Application.StartupPath & "\XMLDOC.xml")
                        url = String.Empty
                        url = ds_login.Tables("Login").Rows(0)("url").ToString() + "?cnpj=" + ds_login.Tables("Login").Rows(0)("CNPJ").ToString() + "&token=" + ds_login.Tables("Login").Rows(0)("TOKEN").ToString()

                        req = WebRequest.Create(New Uri(url))

                        str = req.GetResponse()

                        reader = New StreamReader(str.GetResponseStream())
                        Data = String.Empty
                        Data = reader.ReadToEnd()
                        status = New String() {2}
                        status = Data.Split(",")
                        newurl = New String() {2}
                        newurl_new = New String() {2}

                        If (status(0).Contains("on")) Then
                            url = String.Empty
                            newurl = status(1).Split("""")
                            remove = newurl(3).ToCharArray(0, newurl(3).Length - 2)
                            url = newurl(3) + "?cnpj=" + ds_login.Tables("Login").Rows(0)("CNPJ").ToString() + "&token=" + ds_login.Tables("Login").Rows(0)("TOKEN").ToString()


                            req = WebRequest.Create(New Uri(url))

                            str = req.GetResponse()

                            reader = New StreamReader(str.GetResponseStream())
                            data_arr = New String() {10}
                            data_arr_first_for_loop = New String() {10}
                            Data = reader.ReadToEnd()

                            data_arr_first_for_loop = Data.Split("},")

                            pdf_id = String.Empty

                            For pdf_count As Integer = 0 To data_arr_first_for_loop.Count - 2
                                url_new = String.Empty

                                for_id_n_path = New String() {3}
                                pdf_id = String.Empty
                                pdfname1 = String.Empty
                                series = String.Empty
                                for_id_n_path = data_arr_first_for_loop(pdf_count).Split("""")
                                pdf_id = for_id_n_path(3)
                                series = for_id_n_path(7)
                                url = for_id_n_path(15)
                                req = WebRequest.Create(New Uri(url))

                                str = req.GetResponse()

                                reader = New StreamReader(str.GetResponseStream())

                                Data = reader.ReadToEnd()

                                data_arr = url.Split("=")
                                data_arr = data_arr(7).Split("&")

                                pdfname1 = data_arr(0).ToString()





                                If (File.Exists(ds.Tables("Pdf_location").Rows(0)("Path").ToString() + "\" + pdfname1 + ".pdf") = False) Then

                                    Try

                                        wc1 = New WebClient()
                                        wc1.DownloadFile(New Uri(url), ds.Tables("Pdf_location").Rows(0)("Path").ToString() + "\" + pdfname1 + ".pdf")
                                        Dim print_pdf() As String = New String() {5}
                                        print_pdf = Directory.GetFiles(ds.Tables("Pdf_location").Rows(0)("Path").ToString())

                                        Dim pdfProcess As Process = New Process()
                                        pdfProcess.StartInfo.FileName = Application.StartupPath & "\pdfreader.exe"  '"D:\Foxit Reader\Foxit Reader.exe"
                                        Dim fileNameToSave As String
                                        Dim printername As String
                                        fileNameToSave = ds.Tables("Pdf_location").Rows(0)("Path").ToString() + "\" + pdfname1 + ".pdf"
                                        If (ds.Tables("printer").Rows(0)("Printer_name").ToString().Contains("\") Or ds.Tables("System_ip").Rows(0)("Ip").ToString().ToUpper() = ip.ToUpper()) Then
                                            printername = ds.Tables("printer").Rows(0)("Printer_name").ToString()

                                        Else

                                            printername = "\\" + ds.Tables("System_ip").Rows(0)("Ip").ToString() + "\" + ds.Tables("printer").Rows(0)("Printer_name").ToString()
                                        End If
                                        'MsgBox(ip)
                                        'MsgBox(ds.Tables("System_ip").Rows(0)("Ip").ToString())
                                        'MsgBox(ds.Tables("printer").Rows(0)("Printer_name").ToString().Contains("\"))
                                        'MsgBox(printername)
                                        'pdfProcess.StartInfo.Arguments = "/p /t " + """" + fileNameToSave + """" + " " + """" + """" + printername + """"

                                        'final run arguments
                                        pdfProcess.StartInfo.Arguments = String.Format("/t ""{0}"" ""{1}""", fileNameToSave, printername) ' String.Format("/t {0}", fileNameToSave.ToString(),"192.168.1.3", printername)
                                        'for margin..
                                        ' '' '' ''Dim p As Printing.PrintDocument = New PrintDocument()
                                        ' '' '' ''p.PrinterSettings.PrinterName = printername
                                        ' '' '' ''p.PrinterSettings.PrintFileName = fileNameToSave

                                        '' '' '' ''p.Print()
                                        ' '' '' ''p.DefaultPageSettings.Margins.Left = 0
                                        ' '' '' ''p.DefaultPageSettings.Margins.Right = 0
                                        ' '' '' ''p.DefaultPageSettings.Margins.Top = 0
                                        ' '' '' ''p.DefaultPageSettings.Margins.Bottom = 0
                                       
                                        Try
                                            pdfProcess.Start()
                                        Catch ex As Exception
                                            MsgBox(ex.Message)
                                        End Try

                                        'pdfProcess.Start()


                                        doc = New XmlDocument()





                                        '' '' '' ''printLibrary = New PdfPrint("TestCompany", "")
                                        '' '' '' ''result = String.Empty
                                        '' '' '' ''result = printLibrary.Print(ds.Tables("Pdf_location").Rows(0)("Path").ToString() + "\" + pdfname1 + ".pdf", String.Empty)
                                        '' '' '' ''SetDefaultPrinter(ds.Tables("printer").Rows(0)("Printer_name").ToString())
                                        insert_pdf_name_n_print_time(pdfname1, pdf_id, series)

                                        doc.Load(Application.StartupPath & "\XMLDOC.XML")
                                        'm_nodelist = doc.SelectNodes("/Detail/printer")
                                        m_node = doc.SelectSingleNode("/Detail/Last_pdf_print")
                                        If (m_node IsNot Nothing) Then
                                            m_node.ChildNodes(0).InnerText = System.DateTime.Now

                                            doc.Save(Application.StartupPath & "\XMLDOC.XML")
                                        End If

                                        'Dim p As PrintAction
                                        'Dim pname As New PrinterSettings
                                        'pname.PrinterName = "aaa"
                                        'p = PrintAction.PrintToPrinter

                                    Catch ex As Exception

                                    End Try
                                Else
                                End If
                            Next



                        Else
                            MessageBox.Show("Custome is Deactivate")
                            Exit While
                        End If
                    Else
                        stop_while = True
                        Exit While
                    End If
                Else
                    Exit While
                End If

            Catch ex As Exception

            End Try
        End While
        If (stop_while = True) Then
            ExitApplication()
        End If
    

    End Sub
#End Region


    Dim pdfp As PDFPrinter = New PDFPrinter()
    

    <DllImport("winspool.drv", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function SetDefaultPrinter(ByVal hwnd As String) As Boolean
    End Function




#Region " Save Pdf Details and Time "


    Sub insert_pdf_name_n_print_time(ByVal pdf_name As String, ByVal pdf_id As String, ByVal pdf_series As String)
        Dim doc As New XmlDocument
        doc.Load(Application.StartupPath & "\XMLDOC.XML")
        'm_nodelist = doc.SelectNodes("/Detail/printer")
        Dim m_node As XmlNode = doc.SelectSingleNode("/Detail/Pdf_Print_Detail")

        Dim nameEl As XmlElement = doc.CreateElement("Pdf_Name")
        nameEl.InnerText = pdf_name
        m_node.AppendChild(nameEl)
        doc.DocumentElement.AppendChild(m_node)

        nameEl = doc.CreateElement("Pdf_Id")
        nameEl.InnerText = pdf_id
        m_node.AppendChild(nameEl)
        doc.DocumentElement.AppendChild(m_node)
        nameEl = doc.CreateElement("Pdf_Series")
        nameEl.InnerText = pdf_series
        m_node.AppendChild(nameEl)
        doc.DocumentElement.AppendChild(m_node)
        nameEl = doc.CreateElement("Pdf_Print_DateTime")
        nameEl.InnerText = System.DateTime.Now
        m_node.AppendChild(nameEl)
        doc.DocumentElement.AppendChild(m_node)
        doc.Save(Application.StartupPath & "\XMLDOC.XML")
    End Sub
#End Region

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        'If Not m_AsyncWorker.IsBusy = True Then
        '    insert_login_time()
        '    status_while = False
        '    m_AsyncWorker.RunWorkerAsync()

        'Else
        'End If
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        If m_AsyncWorker.WorkerSupportsCancellation = True Then
            Dim doc As New XmlDocument
            doc.Load(Application.StartupPath & "\XMLDOC.XML")
            'm_nodelist = doc.SelectNodes("/Detail/printer")
            Dim m_node As XmlNode = doc.SelectSingleNode("/Detail/Log_detail")

            Dim nameEl As XmlElement = doc.CreateElement("Log_out_Time")
            nameEl.InnerText = System.DateTime.Now
            m_node.AppendChild(nameEl)
            doc.DocumentElement.AppendChild(m_node)
            doc.Save(Application.StartupPath & "\XMLDOC.XML")

            m_AsyncWorker.CancelAsync()
            ExitApplication()

            'st.Abort()


        End If
    End Sub

    Sub insert_login_time()
        Dim doc As New XmlDocument
        doc.Load(Application.StartupPath & "\XMLDOC.XML")
        'm_nodelist = doc.SelectNodes("/Detail/printer")
        Dim m_node As XmlNode = doc.SelectSingleNode("/Detail/Log_detail")

        Dim nameEl As XmlElement = doc.CreateElement("Log_in_Time")
        nameEl.InnerText = System.DateTime.Now
        m_node.AppendChild(nameEl)
        doc.DocumentElement.AppendChild(m_node)
        doc.Save(Application.StartupPath & "\XMLDOC.XML")
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub StatusStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sh_log As frm_Show_logs = New frm_Show_logs()
        sh_log.Show()
        Me.Hide()

    End Sub
End Class
