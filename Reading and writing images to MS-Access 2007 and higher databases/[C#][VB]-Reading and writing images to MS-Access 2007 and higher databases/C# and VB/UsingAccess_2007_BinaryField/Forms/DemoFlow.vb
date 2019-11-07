Public Class DemoFlowForm
    Private Sub DemoFlow_Load(sender As Object, e As EventArgs) _
        Handles MyBase.Load

        Dim da As New DataAccess.Operations
        da.LoadImages()

        Dim dt As DataTable = da.PictureDataTable

        For Each row As DataRow In dt.Rows

            Dim mPic = New PictureBox With {
                .Image = Image.FromStream(
                    New IO.MemoryStream(row.Field(Of Byte())("Picture"))),
                .Height = 200,
                .Width = 200,
                .SizeMode = PictureBoxSizeMode.Zoom
            }


            FlowLayoutPanel1.Controls.Add(mPic)

        Next
    End Sub
End Class