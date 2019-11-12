Public Module ConversionModule
    ''' <summary>
    ''' Saves bytes to a new image file
    ''' </summary>
    ''' <param name="pImageData"></param>
    ''' <param name="pFilePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConvertBytesToImageFile(pImageData As Byte(), pFilePath As String) As Boolean
        Try
            Dim fileStream = New IO.FileStream(pFilePath, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
            Dim bw = New IO.BinaryWriter(fileStream)
            bw.Write(pImageData)
            bw.Flush()
            bw.Close()
            fileStream.Close()
            bw = Nothing
            fileStream.Dispose()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Return a byte array for a file, in this demo a image file
    ''' used in the DataAccess.vb file to add new records to the database
    ''' </summary>
    ''' <param name="pFileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FileImageBytes(pFileName As String) As Byte()
        Dim fileStream = New IO.FileStream(pFileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
        Dim imageStream As IO.StreamReader = New IO.StreamReader(fileStream)
        Dim byteArray(CInt(fileStream.Length - 1)) As Byte
        fileStream.Read(byteArray, 0, CInt(fileStream.Length))

        Return byteArray

    End Function
End Module
