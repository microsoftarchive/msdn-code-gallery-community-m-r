
' Copyright (c) <2013> Hirendra Sisodiya

'Permission is hereby granted, free of charge, to any person obtaining a copy
'of this software and associated documentation files (the "Software"), to deal
'in the Software without restriction, including without limitation the rights
'to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
'copies of the Software, and to permit persons to whom the Software is
'furnished to do so, subject to the following conditions:

'The above copyright notice and this permission notice shall be included in
'all copies or substantial portions of the Software.

'THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
'IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
'FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
'AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
'LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
'OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
'THE SOFTWARE.
'----------------------------------------------------------------------
' Originally created by hirendra sisodiya
' website: https:www.defercode.com
' Date: 1/2/2013
' Subject: Muitl page Tif viewer
' License: MIT
'Version : 1.01
'----------------------------------------------------------------------

Imports System.ComponentModel
Imports System.Drawing.Imaging

Public Class TiffViewer

    Private _Image As Image
    Private _count As Integer = 0

    <Category("Appearance")> _
   <Description("Set and Get the image object to the control")> _
   <DefaultValue("Nothing")> _
   Public Overloads Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            If value IsNot Nothing Then
                _Image = value
                PictureBox1.Image = _Image
                _pagecount = _Image.GetFrameCount(FrameDimension.Page)
                If _pagecount > 0 Then
                    _count = _DefaultPageNumber
                End If
                txtTotalPage.Text = _pagecount
                txtCurrentpage.Text = 1
                RefreshAll()
            End If
        End Set
    End Property
    <Category("Custom")> _
 <Description("Get the total pages of the image of the control")> _
 <DefaultValue("Nothing")> _
  Private _pagecount As Integer = 0
    Public ReadOnly Property Pagecount() As Integer
        Get
            Return _pagecount
        End Get
    End Property
    <Category("Custom")> _
<Description("Get and set the default page number of the image to show in the control at the time of first load")> _
<DefaultValue("0")> _
  Private _DefaultPageNumber As Integer = 0
    Public Property defaultPageNumber() As Integer
        Get
            Return _DefaultPageNumber
        End Get
        Set(ByVal value As Integer)
            _DefaultPageNumber = value
        End Set
    End Property

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If _count < _pagecount Then
            _count = _count + 1
            txtCurrentpage.Text = _count + 1
        End If
        If _count = Pagecount - 1 Then
            btnNext.Enabled = False
            btnPrev.Enabled = True
        End If
        RefreshAll()
    End Sub

    Private Sub btnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        If _count > 0 Then
            _count = _count - 1
            txtCurrentpage.Text = _count + 1
        End If
        If _count = 0 Then
            btnNext.Enabled = True
            btnPrev.Enabled = False
        End If
        RefreshAll()
    End Sub
    Private Sub RefreshAll()
        If _count < _pagecount Then
            Dim tempimage As Image
            _Image.SelectActiveFrame(FrameDimension.Page, _count)
            tempimage = New Bitmap(_Image, PictureBox1.Width, PictureBox1.Height)
            PictureBox1.Image = tempimage
        End If
    End Sub
    Public Sub Save(ByVal filepath As String, ByVal ImageFormat As ImageFormat)
        _Image.Save(filepath, ImageFormat)
    End Sub
End Class

