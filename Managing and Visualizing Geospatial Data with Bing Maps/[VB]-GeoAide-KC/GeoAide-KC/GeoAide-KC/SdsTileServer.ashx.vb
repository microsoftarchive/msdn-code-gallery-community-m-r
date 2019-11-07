Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports System.Drawing
Imports Microsoft.SqlServer.Types
Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports Newtonsoft.Json
Imports System.Net

Public Class SdsTileServer
    Implements System.Web.IHttpHandler

    Public quadkey As String
    Public layer As String
    Public lvl As Integer
    Public tileX As Integer
    Public tileY As Integer
    Public nwX As Integer
    Public nwY As Integer
    Public nwLon As Double
    Public nwLat As Double
    Public seLon As Double
    Public seLat As Double
    Public myPixelX As Integer
    Public myPixelY As Integer
    Public searchResultsPage As Integer
    Public searchResultsPages As Integer

    Public bmKey As String = "YOUR_BING_MAPS_KEY"
    Public baseUrl As String = "http://spatial.virtualearth.net/REST/v1/data/"
    Public dsId As String = "8d0cefb0710044f0ab719a06cb5d6b57"
    Public dsName As String = "KingCountyParcel"
    Public dsEntityName As String = "KingCountyParcel"

    Public baseDir As String = "C:\Tiles\SdsParcels\"

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        'Fetch URL-Parameters
        quadkey = context.Request.Params("quadkey")

        'Determine Zoom-Level
        lvl = quadkey.Length

        'Is file in disk-cache?
        If File.Exists(baseDir + layer + "\" + quadkey + ".png") Then
            Dim myImage As New Bitmap(System.Drawing.Image.FromFile(baseDir + layer + "\" + quadkey + ".png"))
            WritePngToStream(myImage, context.Response.OutputStream)
        Else
            'Get TileXY-Coordinates
            QuadKeyToTileXY(quadkey, tileX, tileY, lvl)

            'Get PixelXY of the North-West Corner or the tile
            TileXYToPixelXY(tileX, tileY, nwX, nwY)

            'Get Latitude and Longitude of the North-West Corner
            PixelXYToLatLong(nwX, nwY, lvl, nwLat, nwLon)
            PixelXYToLatLong(nwX + 256, nwY + 256, lvl, seLat, seLon)

            'table-specific settings
            Dim myBrush As New SolidBrush(Color.Transparent)
            Dim myPen As New Pen(Brushes.LightGreen)
            myPen.Width = 1

            searchResultsPage = 0
            searchResultsPages = 0

            Dim wc As New WebClient
            Dim jsonStr As String = wc.DownloadString(baseUrl + "/" + dsId + "/" + dsName + "/" + dsEntityName + "?SpatialFilter=intersects('POLYGON ((" + nwLon.ToString + " " + nwLat.ToString + "," + nwLon.ToString + " " + seLat.ToString + "," + seLon.ToString + " " + seLat.ToString + "," + seLon.ToString + " " + nwLat.ToString + "," + nwLon.ToString + " " + nwLat.ToString + "))')&$format=json&$inlinecount=allpages&$top=250&$skip=" + (searchResultsPage * 250).ToString + "&key=" + bmKey)
            Dim parcels As Rootobject = JsonConvert.DeserializeObject(Of Rootobject)(jsonStr)
            searchResultsPages = Int(parcels.d.__count / 250)

            'Create a new image
            Dim myBitmap As New Bitmap(256, 256, Imaging.PixelFormat.Format32bppArgb)

            For j = 0 To searchResultsPages
                jsonStr = wc.DownloadString(baseUrl + "/" + dsId + "/" + dsName + "/" + dsEntityName + "?SpatialFilter=intersects('POLYGON ((" + nwLon.ToString + " " + nwLat.ToString + "," + nwLon.ToString + " " + seLat.ToString + "," + seLon.ToString + " " + seLat.ToString + "," + seLon.ToString + " " + nwLat.ToString + "," + nwLon.ToString + " " + nwLat.ToString + "))')&$format=json&$inlinecount=allpages&$top=250&$skip=" + (searchResultsPage * 250).ToString + "&key=" + bmKey)
                parcels = JsonConvert.DeserializeObject(Of Rootobject)(jsonStr)
                'Parse data and draw image
                For i = 0 To parcels.d.results.Count - 1
                    'Get the Geometry
                    Dim myGeom As SqlGeometry = SqlGeometry.Parse(parcels.d.results(i).Geom)
                    'Deteremine the number of Geometries in the object
                    Dim numGeom As Integer = myGeom.STNumGeometries
                    For loop1 = 1 To numGeom
                        Dim curGeom As SqlGeometry = myGeom.STGeometryN(loop1)
                        'Process only polygons. in this sample we don't want objects that have been rduced to points or lines
                        If curGeom.STNumPoints > 2 Then
                            'Process the exterior Ring
                            Dim numIntRing As Integer = curGeom.STNumInteriorRing
                            Dim extRing As SqlGeometry = curGeom.STExteriorRing
                            Dim numExtPoints As Integer = extRing.STNumPoints
                            Dim myExtPointArray(numExtPoints - 2) As Point
                            For loop2 = 1 To numExtPoints - 1
                                Dim myPoint As SqlGeometry = extRing.STPointN(loop2)
                                Dim myLon As Double = myPoint.STX
                                Dim myLat As Double = myPoint.STY

                                LatLongToPixelXY(myLat, myLon, lvl, myPixelX, myPixelY)
                                myExtPointArray(loop2 - 1) = New Point(myPixelX - nwX, myPixelY - nwY)
                            Next

                            Dim extRingRegion As New GraphicsPath
                            extRingRegion.AddPolygon(myExtPointArray)
                            Dim myRegion As New Region(extRingRegion)

                            'Process interior rings
                            Dim intRingRegion As New GraphicsPath
                            For loop3 = 1 To numIntRing
                                Dim intRing As SqlGeometry = curGeom.STInteriorRingN(loop3)
                                Dim numIntPoints As Integer = intRing.STNumPoints
                                If numIntPoints > 2 Then
                                    Dim myIntPointArray(numIntPoints - 2) As Point
                                    For loop4 = 1 To numIntPoints - 1
                                        Dim myPoint As SqlGeometry = intRing.STPointN(loop4)
                                        Dim myLon As Double = myPoint.STX
                                        Dim myLat As Double = myPoint.STY

                                        LatLongToPixelXY(myLat, myLon, lvl, myPixelX, myPixelY)
                                        myIntPointArray(loop4 - 1) = New Point(myPixelX - nwX, myPixelY - nwY)
                                    Next
                                    intRingRegion.AddPolygon(myIntPointArray)
                                End If
                            Next
                            myRegion.Exclude(intRingRegion)

                            'Draw the Graphics
                            Dim g As Graphics = Graphics.FromImage(myBitmap)
                            'set background-color to white
                            'g.Clear(Color.White)
                            'Anti-Aliasing?
                            'g.SmoothingMode = SmoothingMode.AntiAlias
                            'Start with the Polygons, here we apply the logic for the color-coding
                            g.FillRegion(myBrush, myRegion)
                            'Now draw the Boundaries
                            'Start again with the outer ring
                            g.DrawPolygon(myPen, myExtPointArray)
                            'Now the interior rings
                            For loop5 = 1 To numIntRing
                                Dim intRing As SqlGeometry = curGeom.STInteriorRingN(loop5)
                                Dim numIntPoints As Integer = intRing.STNumPoints
                                If numIntPoints > 2 Then
                                    Dim myIntPointArray(numIntPoints - 2) As Point
                                    For loop6 = 1 To numIntPoints - 1
                                        Dim myPoint As SqlGeometry = intRing.STPointN(loop6)
                                        Dim myLon As Double = myPoint.STX
                                        Dim myLat As Double = myPoint.STY

                                        LatLongToPixelXY(myLat, myLon, lvl, myPixelX, myPixelY)
                                        myIntPointArray(loop6 - 1) = New Point(myPixelX - nwX, myPixelY - nwY)
                                    Next
                                    g.DrawPolygon(myPen, myIntPointArray)
                                End If
                            Next
                        End If
                    Next
                Next

                searchResultsPage = searchResultsPage + 1
            Next

            WritePngToStream(myBitmap, context.Response.OutputStream)
        End If
    End Sub

    Private Sub WritePngToStream(ByVal image As Bitmap, ByVal outStream As Stream)
        Dim writeStream As New MemoryStream()
        image.Save(writeStream, Imaging.ImageFormat.Png)
        If Not File.Exists(baseDir + layer + "\" + quadkey + ".png") Then
            image.Save(baseDir + layer + "\" + quadkey + ".png", Imaging.ImageFormat.Png)
        End If
        writeStream.WriteTo(outStream)
        image.Dispose()
    End Sub

#Region "Helper Functions"

    Private Const EarthRadius As Double = 6378137
    Private Const MinLatitude As Double = -85.05112878
    Private Const MaxLatitude As Double = 85.05112878
    Private Const MinLongitude As Double = -180
    Private Const MaxLongitude As Double = 180

    ''' <summary>
    ''' Clips a number to the specified minimum and maximum values.
    ''' </summary>
    ''' <param name="n">The number to clip.</param>
    ''' <param name="minValue">Minimum allowable value.</param>
    ''' <param name="maxValue">Maximum allowable value.</param>
    ''' <returns>The clipped value.</returns>
    Private Function Clip(ByVal n As Double, ByVal minValue As Double, ByVal maxValue As Double) As Double
        Return Math.Min(Math.Max(n, minValue), maxValue)
    End Function

    ''' <summary>
    ''' Determines the map width and height (in pixels) at a specified level
    ''' of detail.
    ''' </summary>
    ''' <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    ''' to 23 (highest detail).</param>
    ''' <returns>The map width and height in pixels.</returns>
    Public Function MapSize(ByVal levelOfDetail As Integer) As UInteger
        Return CUInt(256) << levelOfDetail
    End Function

    ''' <summary>
    ''' Determines the ground resolution (in meters per pixel) at a specified
    ''' latitude and level of detail.
    ''' </summary>
    ''' <param name="latitude">Latitude (in degrees) at which to measure the
    ''' ground resolution.</param>
    ''' <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    ''' to 23 (highest detail).</param>
    ''' <returns>The ground resolution, in meters per pixel.</returns>
    Public Function GroundResolution(ByVal latitude As Double, ByVal levelOfDetail As Integer) As Double
        latitude = Clip(latitude, MinLatitude, MaxLatitude)
        Return Math.Cos(latitude * Math.PI / 180) * 2 * Math.PI * EarthRadius / MapSize(levelOfDetail)
    End Function

    ''' <summary>
    ''' Determines the map scale at a specified latitude, level of detail,
    ''' and screen resolution.
    ''' </summary>
    ''' <param name="latitude">Latitude (in degrees) at which to measure the
    ''' map scale.</param>
    ''' <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    ''' to 23 (highest detail).</param>
    ''' <param name="screenDpi">Resolution of the screen, in dots per inch.</param>
    ''' <returns>The map scale, expressed as the denominator N of the ratio 1 : N.</returns>
    Public Function MapScale(ByVal latitude As Double, ByVal levelOfDetail As Integer, ByVal screenDpi As Integer) As Double
        Return GroundResolution(latitude, levelOfDetail) * screenDpi / 0.0254
    End Function

    ''' <summary>
    ''' Converts a point from latitude/longitude WGS-84 coordinates (in degrees)
    ''' into pixel XY coordinates at a specified level of detail.
    ''' </summary>
    ''' <param name="latitude">Latitude of the point, in degrees.</param>
    ''' <param name="longitude">Longitude of the point, in degrees.</param>
    ''' <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    ''' to 23 (highest detail).</param>
    ''' <param name="pixelX">Output parameter receiving the X coordinate in pixels.</param>
    ''' <param name="pixelY">Output parameter receiving the Y coordinate in pixels.</param>
    Public Sub LatLongToPixelXY(ByVal latitude As Double, ByVal longitude As Double, ByVal levelOfDetail As Integer, ByRef pixelX As Integer, ByRef pixelY As Integer)
        latitude = Clip(latitude, MinLatitude, MaxLatitude)
        longitude = Clip(longitude, MinLongitude, MaxLongitude)

        Dim x As Double = (longitude + 180) / 360
        Dim sinLatitude As Double = Math.Sin(latitude * Math.PI / 180)
        Dim y As Double = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI)

        Dim mapSize__1 As UInteger = MapSize(levelOfDetail)
        pixelX = CInt(Clip(x * mapSize__1 + 0.5, 0, mapSize__1 - 1))
        pixelY = CInt(Clip(y * mapSize__1 + 0.5, 0, mapSize__1 - 1))
    End Sub

    ''' <summary>
    ''' Converts a pixel from pixel XY coordinates at a specified level of detail
    ''' into latitude/longitude WGS-84 coordinates (in degrees).
    ''' </summary>
    ''' <param name="pixelX">X coordinate of the point, in pixels.</param>
    ''' <param name="pixelY">Y coordinates of the point, in pixels.</param>
    ''' <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    ''' to 23 (highest detail).</param>
    ''' <param name="latitude">Output parameter receiving the latitude in degrees.</param>
    ''' <param name="longitude">Output parameter receiving the longitude in degrees.</param>
    Public Sub PixelXYToLatLong(ByVal pixelX As Integer, ByVal pixelY As Integer, ByVal levelOfDetail As Integer, ByRef latitude As Double, ByRef longitude As Double)
        Dim mapSize__1 As Double = MapSize(levelOfDetail)
        Dim x As Double = (Clip(pixelX, 0, mapSize__1 - 1) / mapSize__1) - 0.5
        Dim y As Double = 0.5 - (Clip(pixelY, 0, mapSize__1 - 1) / mapSize__1)

        latitude = 90 - 360 * Math.Atan(Math.Exp(-y * 2 * Math.PI)) / Math.PI
        longitude = 360 * x
    End Sub

    ''' <summary>
    ''' Converts pixel XY coordinates into tile XY coordinates of the tile containing
    ''' the specified pixel.
    ''' </summary>
    ''' <param name="pixelX">Pixel X coordinate.</param>
    ''' <param name="pixelY">Pixel Y coordinate.</param>
    ''' <param name="tileX">Output parameter receiving the tile X coordinate.</param>
    ''' <param name="tileY">Output parameter receiving the tile Y coordinate.</param>
    Public Sub PixelXYToTileXY(ByVal pixelX As Integer, ByVal pixelY As Integer, ByRef tileX As Integer, ByRef tileY As Integer)
        tileX = Math.Floor(pixelX / 256)
        tileY = Math.Floor(pixelY / 256)
    End Sub

    ''' <summary>
    ''' Converts tile XY coordinates into pixel XY coordinates of the upper-left pixel
    ''' of the specified tile.
    ''' </summary>
    ''' <param name="tileX">Tile X coordinate.</param>
    ''' <param name="tileY">Tile Y coordinate.</param>
    ''' <param name="pixelX">Output parameter receiving the pixel X coordinate.</param>
    ''' <param name="pixelY">Output parameter receiving the pixel Y coordinate.</param>
    Public Sub TileXYToPixelXY(ByVal tileX As Integer, ByVal tileY As Integer, ByRef pixelX As Integer, ByRef pixelY As Integer)
        pixelX = tileX * 256
        pixelY = tileY * 256
    End Sub

    ''' <summary>
    ''' Converts tile XY coordinates into a QuadKey at a specified level of detail.
    ''' </summary>
    ''' <param name="tileX">Tile X coordinate.</param>
    ''' <param name="tileY">Tile Y coordinate.</param>
    ''' <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
    ''' to 23 (highest detail).</param>
    ''' <returns>A string containing the QuadKey.</returns>
    Public Function TileXYToQuadKey(ByVal tileX As Integer, ByVal tileY As Integer, ByVal levelOfDetail As Integer) As String
        Dim quadKey As New StringBuilder()
        For i As Integer = levelOfDetail To 1 Step -1
            Dim digit As Integer = 0
            Dim mask As Integer = 1 << (i - 1)
            If (tileX And mask) <> 0 Then
                digit += 1
            End If
            If (tileY And mask) <> 0 Then
                digit += 1
                digit += 1
            End If
            quadKey.Append(digit.ToString)
        Next
        Return quadKey.ToString()
    End Function

    ''' <summary>
    ''' Converts a QuadKey into tile XY coordinates.
    ''' </summary>
    ''' <param name="quadKey">QuadKey of the tile.</param>
    ''' <param name="tileX">Output parameter receiving the tile X coordinate.</param>
    ''' <param name="tileY">Output parameter receiving the tile Y coordinate.</param>
    ''' <param name="levelOfDetail">Output parameter receiving the level of detail.</param>
    Public Sub QuadKeyToTileXY(ByVal quadKey As String, ByRef tileX As Integer, ByRef tileY As Integer, ByRef levelOfDetail As Integer)
        tileX = InlineAssignHelper(tileY, 0)
        levelOfDetail = quadKey.Length
        For i As Integer = levelOfDetail To 1 Step -1
            Dim mask As Integer = 1 << (i - 1)
            Select Case quadKey(levelOfDetail - i)
                Case "0"c
                    Exit Select

                Case "1"c
                    tileX = tileX Or mask
                    Exit Select

                Case "2"c
                    tileY = tileY Or mask
                    Exit Select

                Case "3"c
                    tileX = tileX Or mask
                    tileY = tileY Or mask
                    Exit Select
                Case Else

                    Throw New ArgumentException("Invalid QuadKey digit sequence.")
            End Select
        Next
    End Sub

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function

#End Region

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class


