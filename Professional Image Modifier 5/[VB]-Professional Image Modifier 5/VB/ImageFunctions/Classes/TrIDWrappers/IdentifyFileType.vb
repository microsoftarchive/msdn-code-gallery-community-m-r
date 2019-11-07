Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace ImageFunctions.Classes.TrIDWrappers
	Friend Class IdentifyFileType
		'TODO: Allow this to be edited by the End User
		Private alImageFileTypes As New ArrayList() 'TODO: This should be a dictionary<string Extension,string Description>

		Public Enum FileType
			Unknown
			Image
		End Enum

		Private p As New Process()

		Private CurrentImage As String


		Public Delegate Sub IdentifyFileTypeCompleteHandler(ByVal ft As FileType)
		Public Event IdentifyFileTypeComplete As IdentifyFileTypeCompleteHandler
		Public Delegate Sub IdentifyFileTypeErrorHandler(ByVal Message As String)
		Public Event IdentifyFileTypeError As IdentifyFileTypeErrorHandler
		Public Delegate Sub IdentifyFileTypeLogHandler(ByVal Message As String)
		Public Event IdentifyFileTypeLog As IdentifyFileTypeLogHandler

		Public Sub New(ByVal CurrentImage As String)
			Me.CurrentImage = CurrentImage
			LoadImageFileTypes()
		End Sub

		'TODO: Load these from an external (Non Hard-Coded) source
		''' <summary>
		''' Load the Image File Types
		''' </summary>
		Private Sub LoadImageFileTypes()
			alImageFileTypes.Add(".thm") ' Apparently a Thumbnail file
			alImageFileTypes.Add(".yuv") ' YUV Encoded Image File
			alImageFileTypes.Add(".ASE") ' Adobe Swatch
			alImageFileTypes.Add(".art") ' America Online proprietary format
			alImageFileTypes.Add(".bmp") ' BMP – Microsoft Windows Bitmap formatted image
			alImageFileTypes.Add(".blp") ' BLP – Blizzard Entertainment proprietary texture format
			alImageFileTypes.Add(".cd5") ' CD5 – Chasys Draw IES image
			alImageFileTypes.Add(".cit") ' CIT – Intergraph is a monochrome bitmap format
			alImageFileTypes.Add(".cpt") ' //CPT – Corel PHOTO - PAINT image
			alImageFileTypes.Add(".cr2") ' CR2 – Canon camera raw format. Photos will have this format on some Canon cameras if the quality "RAW" is selected in camera settings.
			alImageFileTypes.Add(".cut") 'CUT – Dr. Halo image file
			alImageFileTypes.Add(".dds") 'DDS – DirectX texture file
			alImageFileTypes.Add(".dib") ' DIB – Device - Independent Bitmap graphic
			alImageFileTypes.Add(".djvu") ' DjVu – DjVu for scanned documents
			alImageFileTypes.Add(".egt") ' EGT – EGT Universal Document, used in EGT SmartSense to compress PNG files to yet a smaller file
			alImageFileTypes.Add(".exif") 'Exif – Exchangeable image file format (Exif) is a specification for the image file format used by digital cameras
			alImageFileTypes.Add(".gif") 'GIF – CompuServe's Graphics Interchange Format
			alImageFileTypes.Add(".gpl") ' GPL – GIMP Palette, using a textual representation of color names and RGB values
			alImageFileTypes.Add(".grf") 'GRF – Zebra Technologies proprietary format
			alImageFileTypes.Add(".icns") ' ICNS – file format use for icons in Mac OS X. Contains bitmap images at multiple resolutions and bitdepths with alpha channel.
			alImageFileTypes.Add(".icon")
			alImageFileTypes.Add(".ico") 'ICO – a file format used for icons in Microsoft Windows. Contains small bitmap images at multiple resolutions and sizes.
			alImageFileTypes.Add(".iff") 'IFF (.iff, .ilbm, .lbm) – ILBM (Seen on Atari & Amiga)
			alImageFileTypes.Add(".ilbm") 'IFF (.iff, .ilbm, .lbm) – ILBM (Seen on Atari & Amiga)
			alImageFileTypes.Add(".jng") 'JNG – a single   -frame MNG using JPEG compression and possibly an alpha channel.
			alImageFileTypes.Add(".jpg") ' All file Extensions should be in Lowercase
			alImageFileTypes.Add(".jpe") 'JPEG, JFIF (.jpg or .jpeg) – Joint Photographic Experts Group – a lossy image format widely used to display photographic images.
			alImageFileTypes.Add(".jp2") 'JP2 – JPEG2000
			alImageFileTypes.Add(".jps") 'JPS – JPEG Stereo
			alImageFileTypes.Add(".lbm") 'LBM – Deluxe Paint image file
			alImageFileTypes.Add(".max") 'MAX – ScanSoft PaperPort document
			alImageFileTypes.Add(".miff") 'MIFF – ImageMagick's native file format
			alImageFileTypes.Add(".mng") 'MNG – Multiple Network Graphics, the animated version of PNG
			alImageFileTypes.Add(".msp") 'MSP – a file format used by old versions of Microsoft Paint. Replaced with BMP in Microsoft Windows 3.0
			alImageFileTypes.Add(".nitf") 'NITF – A U.S. Government standard commonly used in Intelligence systems
			alImageFileTypes.Add(".ota") 'OTA bitmap (Over The Air bitmap) – a specification designed by Nokia for black and white images for mobile phones
			alImageFileTypes.Add(".pbm") 'PBM – Portable bitmap
			alImageFileTypes.Add(".pc1") 'PC1 – Low resolution, compressed Degas picture file
			alImageFileTypes.Add(".pc2") 'PC2 – Medium resolution, compressed Degas picture file
			alImageFileTypes.Add(".pc3") 'PC3 – High resolution, compressed Degas picture file
			alImageFileTypes.Add(".pcf") 'PCF – Pixel Coordination Format
			alImageFileTypes.Add(".pcx") 'PCX – a lossless format used by ZSoft's PC Paint, popular at one time on DOS systems.
			alImageFileTypes.Add(".pdn") 'PDN – Paint.NET image file
			alImageFileTypes.Add(".pgm") 'PGM – Portable graymap
			alImageFileTypes.Add(".pi1") 'PI1 – Low resolution, uncompressed Degas picture file
			alImageFileTypes.Add(".pi2") 'PI2 – Medium resolution, uncompressed Degas picture file. Also Portrait Innovations encrypted image format.
			alImageFileTypes.Add(".pi3") 'PI3 – High resolution, uncompressed Degas picture file
			alImageFileTypes.Add(".pict") 'PICT, PCT – Apple Macintosh PICT image
			alImageFileTypes.Add(".pct") 'PICT, PCT – Apple Macintosh PICT image
			alImageFileTypes.Add(".png") 'PNG – Portable Network Graphic (lossless, recommended for display and edition of graphic images)
			alImageFileTypes.Add(".pnm") 'PNM – Portable anymap graphic bitmap image
			alImageFileTypes.Add(".pns") 'PNS – PNG Stereo
			alImageFileTypes.Add(".ppm") 'PPM – Portable Pixmap (Pixel Map) image
			alImageFileTypes.Add(".psb") 'PSB – Adobe Photoshop Big image file (for large files)
			alImageFileTypes.Add(".psd") 'PSD, PDD – Adobe Photoshop Drawing
			alImageFileTypes.Add(".pdd") 'PSD, PDD – Adobe Photoshop Drawing
			alImageFileTypes.Add(".psp") 'PSP – Paint Shop Pro image
			alImageFileTypes.Add(".px") 'PX – Pixel image editor image file
			alImageFileTypes.Add(".pxm") 'PXM - Pixelmator image file
			alImageFileTypes.Add(".pxr") 'PXR – Pixar Image Computer image file
			alImageFileTypes.Add(".qfx") 'QFX – QuickLink Fax image
			alImageFileTypes.Add(".raw") 'RAW – General term for minimally processed image data (acquired by a digital camera)
			alImageFileTypes.Add(".rle") 'RLE – a run -length encoded image
			alImageFileTypes.Add(".sct") 'SCT – Scitex Continuous Tone image file
			alImageFileTypes.Add(".sgi") 'SGI, RGB, INT, BW – Silicon Graphics Image
			alImageFileTypes.Add(".rgb") 'SGI, RGB, INT, BW – Silicon Graphics Image
			alImageFileTypes.Add(".int") 'SGI, RGB, INT, BW – Silicon Graphics Image
			alImageFileTypes.Add(".bw") 'SGI, RGB, INT, BW – Silicon Graphics Image
			alImageFileTypes.Add(".tga") 'TGA (.tga, .targa, .icb, .vda, .vst, .pix) – Truevision TGA (Targa) image
			alImageFileTypes.Add(".targa") 'TGA (.tga, .targa, .icb, .vda, .vst, .pix) – Truevision TGA (Targa) image
			alImageFileTypes.Add(".icb") 'TGA (.tga, .targa, .icb, .vda, .vst, .pix) – Truevision TGA (Targa) image
			alImageFileTypes.Add(".vda") 'TGA (.tga, .targa, .icb, .vda, .vst, .pix) – Truevision TGA (Targa) image
			alImageFileTypes.Add(".vst") 'TGA (.tga, .targa, .icb, .vda, .vst, .pix) – Truevision TGA (Targa) image
			alImageFileTypes.Add(".pix") 'TGA (.tga, .targa, .icb, .vda, .vst, .pix) – Truevision TGA (Targa) image
			alImageFileTypes.Add(".tif") 'TIFF (.tif or .tiff) – Tagged Image File Format (usually lossless, but many variants exist, including lossy ones)
			alImageFileTypes.Add(".tiff") 'TIFF (.tif or .tiff) – Tagged Image File Format (usually lossless, but many variants exist, including lossy ones)
			alImageFileTypes.Add(".vtf") 'VTF – Valve Texture Format
			alImageFileTypes.Add(".xbm") 'XBM – X Window System Bitmap
			alImageFileTypes.Add(".xcf") 'XCF – GIMP image (from Gimp's origin at the eXperimental Computing Facility of the University of California)
			alImageFileTypes.Add(".xpm") 'XPM – X Window System Pixmap
		End Sub

		' Run Gm.exe and read its results saving them into output.
		Public Sub GetFileType()
			RaiseEvent IdentifyFileTypeLog("Identifying File Type")
			Dim path As String = System.IO.Path.Combine(Application.StartupPath, "Binn\TrID")
			path = System.IO.Path.Combine(path, "trid.exe")

			Dim arguments As String = String.Format(" """ & Me.CurrentImage & """" & " -v -r:1 ")
			Dim startInfo = New ProcessStartInfo With {.Arguments = arguments, .FileName = path, .CreateNoWindow = True, .RedirectStandardError = True, .RedirectStandardOutput = True, .UseShellExecute = False}

			p.EnableRaisingEvents = True
			p.StartInfo = startInfo
			p.Start()

			AddHandler p.ErrorDataReceived, AddressOf p_ErrorDataReceived

			'	string error = p.StandardError.ReadToEnd();
			Dim output As String = p.StandardOutput.ReadToEnd()

			p.WaitForExit()

			If output.Length > 0 Then
				OutputDataReceived(output)
			End If
			'	if (error.Length > 0) StatsError(error);

			RaiseEvent IdentifyFileTypeLog("Identifying File Type Completed.")

		End Sub

		Private Sub p_ErrorDataReceived(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
			RaiseEvent IdentifyFileTypeError(e.Data)
			p.Dispose()

		End Sub

		' Process the results of GM.EXE
		Private Sub OutputDataReceived(ByVal output As String)
			' <Blank line at the beginning of the output>
			'TrID/32 - File Identifier v2.10 - (C) 2003-11 By M.Pontello
			'Definitions found:  5231
			'Analyzing...

			'Collecting data from file: C:\Users\Dave\SkyDrive\Pictures\..\...\1.jpg
			'32.2% (.JPG) JFIF-EXIF JPEG Bitmap (5000/1/1)
			'Definition   : bitmap-jfif-exif.trid.xml
			'Files      : 12
			'Author       : Marco Pontello
			'E-Mail     : marcopon@gmail.com
			'Home Page  : http://mark0.net

			Dim lines() As String = output.Split(ControlChars.Cr)
			Dim Result() As String = lines(6).Split("("c)
			Dim extension() As String = Result(1).Split(")"c)
			RaiseEvent IdentifyFileTypeLog(" Using: " & lines(1).ToString().TrimStart(ControlChars.Lf)) ' Sends   Using: File Identifier v2.10 - (C) 2003-11 By M.Pontello

			NowGetType(extension(0).ToString())

		End Sub

		Private Sub NowGetType(ByVal FileExtension As String)
			If alImageFileTypes.Contains(FileExtension.ToLowerInvariant()) Then
				RaiseEvent IdentifyFileTypeLog("Identified Image File") ' Just because we know the type of file does not mean we can load it.
				RaiseEvent IdentifyFileTypeComplete(FileType.Image)
			Else
				RaiseEvent IdentifyFileTypeLog("Unknown File Type")
				RaiseEvent IdentifyFileTypeComplete(FileType.Unknown)
			End If
		End Sub
	End Class
End Namespace
