Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Reflection
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace ImageFunctions.Forms
	Partial Friend Class FrmAboutBox
		Inherits Form

		Public Sub New()
			InitializeComponent()
			Me.Text = String.Format("About {0}", AssemblyTitle)
			Me.labelProductName.Text = AssemblyProduct
			Me.labelVersion.Text = String.Format("Version {0}", AssemblyVersion)
			Me.labelCopyright.Text = AssemblyCopyright
			Me.labelCompanyName.Text = AssemblyCompany
			Me.textBoxDescription.Text = AssemblyDescription
		End Sub

		#Region "Assembly Attribute Accessors"

		Public ReadOnly Property AssemblyTitle() As String
			Get
				Dim attributes() As Object = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyTitleAttribute), False)
				If attributes.Length > 0 Then
					Dim titleAttribute As AssemblyTitleAttribute = DirectCast(attributes(0), AssemblyTitleAttribute)
					If titleAttribute.Title <> "" Then
						Return titleAttribute.Title
					End If
				End If
				Return System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
			End Get
		End Property

		Public ReadOnly Property AssemblyVersion() As String
			Get
				Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
			End Get
		End Property

		Public ReadOnly Property AssemblyDescription() As String
			Get
				Dim attributes() As Object = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyDescriptionAttribute), False)
				If attributes.Length = 0 Then
					Return ""
				End If
				Return DirectCast(attributes(0), AssemblyDescriptionAttribute).Description
			End Get
		End Property

		Public ReadOnly Property AssemblyProduct() As String
			Get
				Dim attributes() As Object = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyProductAttribute), False)
				If attributes.Length = 0 Then
					Return ""
				End If
				Return DirectCast(attributes(0), AssemblyProductAttribute).Product
			End Get
		End Property

		Public ReadOnly Property AssemblyCopyright() As String
			Get
				Dim attributes() As Object = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCopyrightAttribute), False)
				If attributes.Length = 0 Then
					Return ""
				End If
				Return DirectCast(attributes(0), AssemblyCopyrightAttribute).Copyright
			End Get
		End Property

		Public ReadOnly Property AssemblyCompany() As String
			Get
				Dim attributes() As Object = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCompanyAttribute), False)
				If attributes.Length = 0 Then
					Return ""
				End If
				Return DirectCast(attributes(0), AssemblyCompanyAttribute).Company
			End Get
		End Property
		#End Region
	End Class
End Namespace
