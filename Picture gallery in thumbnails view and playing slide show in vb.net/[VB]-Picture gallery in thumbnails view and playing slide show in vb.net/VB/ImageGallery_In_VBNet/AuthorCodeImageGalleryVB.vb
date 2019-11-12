Public Class AuthorCodeImageGalleryVB
    Dim CtrlWidth As Integer
    Dim CtrlHeight As Integer
    Dim PicWidth As Integer
    Dim PicHeight As Integer
    Dim XLocation As Integer
    Dim YLocation As Integer

    Private Sub AuthorCodeImageGalleryVB_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        CtrlHeight = Me.Height
        CtrlWidth = Me.Width
    End Sub
    Private _Directory_Path As String
    Public Property Directorypath() As String
        Get
            Return _Directory_Path
        End Get
        Set(ByVal value As String)
          
            _Directory_Path = value
            XLocation = 25
            YLocation = 25
            PicWidth = 117
            PicHeight = 109
            CreateGallery()
        End Set
    End Property
    Dim i As Integer = 0
    Private Sub DrawPictureBox(ByVal _filename As String, ByVal _displayname As String)
        Dim Pic1 As New PictureBox
        Pic1.Location = New System.Drawing.Point(XLocation, YLocation)
        XLocation = XLocation + PicWidth + 20
        If XLocation + PicWidth >= CtrlWidth Then
            XLocation = 25
            YLocation = YLocation + PicHeight + 20
        End If
        Pic1.Name = "PictureBox" & i
        i += 1
        Pic1.Size = New System.Drawing.Size(PicWidth, PicHeight)
        Pic1.TabIndex = 0
        Pic1.TabStop = False
        Pic1.BorderStyle = BorderStyle.Fixed3D
        Me.ToolTip1.SetToolTip(Pic1, _displayname)
        AddHandler Pic1.MouseEnter, AddressOf Pic1_MouseEnter
        AddHandler Pic1.MouseLeave, AddressOf Pic1_MouseLeave
        Me.Controls.Add(Pic1)
        Pic1.Image = Image.FromFile(_filename)
        Pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    End Sub
    Private Sub CreateGallery()
        i = 0
        RemoveControls()
        If Directorypath IsNot Nothing Then
            Dim di As New IO.DirectoryInfo(Directorypath)
            Dim diar1 As IO.FileInfo() = di.GetFiles("*.jpg").Concat(di.GetFiles("*.bmp")).Concat(di.GetFiles("*.png")).Concat(di.GetFiles("*.gif")).ToArray
            Dim dra As IO.FileInfo
            For Each dra In diar1
                DrawPictureBox(dra.FullName, dra.Name)
            Next
        End If
    End Sub

    Private Sub Pic1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Pic As PictureBox
        Pic = sender
        Pic.BorderStyle = BorderStyle.FixedSingle
    End Sub
    Private Sub Pic1_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Pic As PictureBox
        Pic = sender
        Pic.BorderStyle = BorderStyle.Fixed3D
    End Sub
    Private Sub RemoveControls()
Again:  For Each ctrl As Control In Me.Controls
            If TypeOf (ctrl) Is PictureBox Then
                Me.Controls.Remove(ctrl)
            End If
        Next
        If Me.Controls.Count > 0 Then
            GoTo Again
        End If
    End Sub
End Class
