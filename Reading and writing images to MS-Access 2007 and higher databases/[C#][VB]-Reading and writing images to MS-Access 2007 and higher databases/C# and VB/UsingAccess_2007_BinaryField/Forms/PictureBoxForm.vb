Imports DataAccess
Public Class frmPictureBoxForm
    WithEvents _bsData As New BindingSource
    Private Sub PictureBoxForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim da As New DataAccess.Operations
        _bsData.DataSource = da.PictureDataTable
        BindingNavigator1.BindingSource = _bsData
    End Sub
    Private Sub bsData_PositionChanged(sender As Object, e As EventArgs) Handles _bsData.PositionChanged
        If _bsData.Current IsNot Nothing Then
            PictureBox1.Image = Image.FromStream(New IO.MemoryStream(CType(_bsData.Current, DataRowView).Row.Field(Of Byte())("Picture")))
        End If
    End Sub
End Class