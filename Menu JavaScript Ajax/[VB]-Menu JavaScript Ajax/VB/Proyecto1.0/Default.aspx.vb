Imports System.Threading
Imports System.Globalization
Imports System.Web.Services
Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    <WebMethod(EnableSession:=True)> _
    Public Shared Function CargarMenu() As List(Of usp_ConsultarMenuResult)
        Try
            Dim dcl As New dclProyectoDataContext
            Dim qryMenu = dcl.usp_ConsultarMenu.ToList
            Return qryMenu
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class