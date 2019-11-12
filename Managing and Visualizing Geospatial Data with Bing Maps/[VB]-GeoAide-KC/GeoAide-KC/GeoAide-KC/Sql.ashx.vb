Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Types

Public Class Sql
    Implements System.Web.IHttpHandler

    Public nwLon As Double
    Public nwLat As Double
    Public seLon As Double
    Public seLat As Double
    Public callback As String

    Sub ProcessRequest(ByVal context As HttpContext) _
        Implements IHttpHandler.ProcessRequest
        'Fetch URL-Parameters
        nwLat = context.Request.Params("nwLat")
        nwLon = context.Request.Params("nwLon")
        seLat = context.Request.Params("seLat")
        seLon = context.Request.Params("seLon")
        seLon = context.Request.Params("seLon")
        callback = context.Request.Params("jsonp")

        'Retrieve Database Setting from web.config
        Dim settings As ConnectionStringSettings =
            ConfigurationManager.ConnectionStrings("kcgis")

        'Open a connection to the database
        Dim myConn As New SqlConnection(settings.ConnectionString)
        myConn.Open()
        Dim cmd As New SqlCommand()

        'Set SQL Parameters
        cmd.Connection = myConn
        cmd.CommandType = Data.CommandType.StoredProcedure
        cmd.Parameters.Add(New SqlParameter("nwLon", nwLon))
        cmd.Parameters.Add(New SqlParameter("nwLat", nwLat))
        cmd.Parameters.Add(New SqlParameter("seLon", seLon))
        cmd.Parameters.Add(New SqlParameter("seLat", seLat))

        'Specify the stored procedure name as the command text
        cmd.CommandText = "GetTrails"

        'Create a string builder
        Dim sb As StringBuilder = New StringBuilder
        sb.Append(callback + "({" + """" + "d" + """" + ":{")
        sb.Append("""" + "results" + """" + ":[")

        'Read Data
        Dim myReader As SqlDataReader = cmd.ExecuteReader()
        While myReader.Read()
            'Get the Geometry
            Dim myGeom As SqlGeometry = myReader(0)
            'Deteremine the number of Geometries in the object
            Dim numGeom As Integer = myGeom.STNumGeometries
            For j = 1 To numGeom
                Dim curGeom As SqlGeometry = myGeom.STGeometryN(j)
                sb.Append("{" + """" + "Geom" + """" + ":" + """" +
                          curGeom.ToString() + """" + "},")
            Next
        End While
        sb.Length = sb.Length - 1
        sb.Append("]}});")
        myReader.Close()
        myConn.Close()

        context.Response.ContentType = "application/json"
        context.Response.Write(sb.ToString())
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class