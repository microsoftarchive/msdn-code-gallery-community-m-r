Imports DataAccess
Public Class DataBindingDemo

    WithEvents _bsData As New BindingSource
    Private CurrentImage As Image

    Private Sub DataBindingDemo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim da As New DataAccess.Operations
        _bsData.DataSource = da.PictureDataTable
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.DataSource = _bsData
    End Sub
    Private Sub bsData_PositionChanged(sender As Object, e As EventArgs) Handles _bsData.PositionChanged
        If _bsData.Current IsNot Nothing Then

            CurrentImage = Image.FromStream(
                New IO.MemoryStream(
                    CType(_bsData.Current, DataRowView).Row.Field(Of Byte())("Picture"))
            )

            PictureBox1.Image = CurrentImage
            CurrentImage = Nothing
        Else
            PictureBox1.Image = Nothing
        End If
    End Sub
End Class