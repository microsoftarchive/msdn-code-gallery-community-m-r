'Imports SWF = System.Windows.Forms
Imports SDS = System.Data.SqlClient
'Imports SE = System.Environment
Public Class AssConn

    ' The StartupPath is the pick the path of the project's Folder
    ' where the Database File "S38OOL.mdb" resides

    'Public Path As String = Application.ExecutablePath.Remove(Application.StartupPath.Length, 11)

    ''Public Path As String = SWF.Application.StartupPath '.Remove(Application.StartupPath.Length, 11)
    ''Public Conn As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source= " & Path & "\71RM5NTS.dat;")
    ''Public Cmd As New OleDb.OleDbCommand

    Public Conn, Conn2, Conn3 As New SDS.SqlConnection
    Public Cmd, Cmd2, Cmd3 As New SDS.SqlCommand

    'Private Function SysName() As String
    '    Return SE.MachineName
    'End Function

    Sub New()
        'Conn.ConnectionString = "workstation id=SERVER;packet size=32767;integrated security=SSPI;data source=SERVER;persist security info=False;Connect Timeout=30;initial catalog=Neuro_BS"
        Conn.ConnectionString = "Data Source=server;Initial Catalog=Neuro_BS;User ID=sa"
        Conn2.ConnectionString = "workstation id=SERVER;packet size=32767;integrated security=SSPI;data source=SERVER;persist security info=False;Connect Timeout=30;initial catalog=Neuro_BS"
        Conn3.ConnectionString = "Data Source=server;Initial Catalog=Neuro_BS;Integrated Security=True"
        'Conn2.ConnectionString = "workstation id=SERVER;packet size=8192;user id=NeuroSoft;password=Neuro7638;data source=server;persist security info=False;initial catalog=Neuro_BS"
    End Sub

End Class
