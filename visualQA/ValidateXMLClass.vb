Imports System.IO
Imports System.Xml
Imports System.Xml.XPath
Imports System.Xml.Schema
Imports System.Net

Imports System.Reflection
Imports System.Resources

Public Class ValidateXMLClass

Public fileIn As String
Public report As String = ""
Public isValid As Boolean = True
Public line As Integer 
public column As Integer

public validateWhat As String = "" ' schema or DTD

Public Function wellformed() As Boolean
	Dim settings As XmlReaderSettings = New XmlReaderSettings()
	settings.ProhibitDtd = False
	settings.ConformanceLevel = ConformanceLevel.Document
	' settings.XmlResolver = Nothing ' nothing is resolved but HTML entities (e.g., &nbsp;) will create problems
	
	Dim resolver As New XHTMLResolver()
'	settings.XmlResolver = resolver
	
	' Dim stream As FileStream = new FileStream(fileIn, FileMode.Open)
	dim result As Boolean 
	Dim watch As Stopwatch = Stopwatch.StartNew()
	
'	Using xmlReader As XmlReader = XmlReader.Create(fileIn, settings)
'	  Try
'	    While xmlReader.Read()
'	    End While
'	    result = True
'	  Catch e As XmlException
'	    report = e.Message
'	    debug.WriteLine(report)
'	    result = False
'	  End Try
'	End Using

	Dim doc As New XmlDocument()
	doc.XmlResolver = resolver
	Try
		doc.Load(fileIn)
		result = True	
	Catch e As XmlException
		line = e.LineNumber
		column = e.LinePosition
		report = e.Message
		result = False
	End Try
	
	
	watch.Stop
	debug.WriteLine(watch.ElapsedMilliseconds/1000)
	
	Return result
End Function


'  ' System.Xml.Schema.XmlSchemaValidationException
Public Function validate() As Boolean
	
	If Not File.Exists(fileIn) Then 
		report = "File is missing"
		return False
		Exit Function
	End If	
	
	Try
    	Dim settings as XmlReaderSettings = new XmlReaderSettings()
		
		Select UCASE(validateWhat)
		Case "SCHEMA"
		    settings.ValidationType = ValidationType.Schema
		    settings.ValidationFlags = settings.ValidationFlags Or XmlSchemaValidationFlags.ProcessInlineSchema
		    settings.ValidationFlags = settings.ValidationFlags Or XmlSchemaValidationFlags.ReportValidationWarnings
		Case "DTD"
			settings.ProhibitDtd = False
	    	settings.ValidationType = ValidationType.DTD
		Case Else ' check if XML is well-formed
			' default = check if XML is well-formed
			settings.ProhibitDtd = False ' to give "readable" feedback if a DTD problem is found
			settings.ValidationType = ValidationType.None
			settings.ConformanceLevel = ConformanceLevel.Document
		End Select
		
'		' ignore DTD
'		Dim resolver As New XmlUrlResolver()
'		' resolver.Credentials = Nothing ' not set equals Null
'		settings.XmlResolver = resolver
'		settings.ProhibitDtd = False
	
		AddHandler settings.ValidationEventHandler, AddressOf MyValidationEventHandler
	
	    ' Create the XmlReader object.
	    Dim reader as XmlReader = XmlReader.Create(fileIn, settings)
	
	    ' Parse the file. 
	    while reader.Read()
	    End While
	    
		reader.Close()
		reader = Nothing
		
		' process results
		If isValid Then ' "Document is valid"
			Return True
		Else ' "Document is invalid"
			return False
		End If
		
	Catch a As UnauthorizedAccessException
		'dont have access permission
		report = a.Message
		return False
	Catch a As Exception
		' anything else
		report = a.Message
		return False
	End Try
	
End Function


	Sub MyValidationEventHandler(ByVal sender As Object, ByVal args As ValidationEventArgs)
		isValid = False
		report = args.Message
		line = args.Exception.lineNumber
		column = args.Exception.LinePosition
	End Sub	
	
End Class


Public Class XHTMLResolver
	Inherits XmlResolver
	
	Public Overloads Overrides WriteOnly Property Credentials() As ICredentials
		Set
		End Set
	End Property

	Public Sub New()
	End Sub

	Public Overloads Overrides Function ResolveUri(baseUri As Uri, relativeUri As [String]) As Uri
		If [String].Compare(relativeUri, "-//W3C//DTD XHTML 1.0 Transitional//EN", True) = 0 Then
			Return New Uri("http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd")
		ElseIf [String].Compare(relativeUri, "-//W3C//DTD XHTML 1.0 Transitional//EN", True) = 0 Then
			Return New Uri("http://www.w3.org/tr/xhtml1/DTD/xhtml1-strict.dtd")
		ElseIf [String].Compare(relativeUri, "-//W3C//DTD XHTML 1.0 Transitional//EN", True) = 0 Then
			Return New Uri("http://www.w3.org/tr/xhtml1/DTD/xhtml1-frameset.dtd")
		ElseIf [String].Compare(relativeUri, "-//W3C//DTD XHTML 1.1//EN", True) = 0 Then
			Return New Uri("http://www.w3.org/tr/xhtml11/DTD/xhtml11.dtd")
		End If

		Return MyBase.ResolveUri(baseUri, relativeUri)
	End Function
	
	Public Overloads Overrides Function GetEntity(absoluteUri As Uri, role As String, ofObjectToReturn As Type) As Object

		Dim entityObj As [Object] = Nothing
		Dim strURI As [String] = absoluteUri.AbsoluteUri
		' debug.WriteLine(">>> " & strURI)

		Dim assembly As Assembly = Assembly.GetExecutingAssembly()
		' Return assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))	
		Select Case strURI.ToLower()
		Case "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd"
			' debug.WriteLine(Path.GetFileName(absoluteUri.AbsolutePath))
			' Return assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml1/dtd/xhtml-lat1.ent"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml1/dtd/xhtml-special.ent"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml1/dtd/xhtml-symbol.ent"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml1/dtd/xhtml1.dcl"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml1/dtd/xhtml1-strict.dtd"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml1/dtd/xhtml1-frameset.dtd"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml11/dtd/xhtml11.dtd"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-inlstyle-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-framework-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-datatypes-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-qname-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-events-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-attribs-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml11/dtd/xhtml11-model-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-charent-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-lat1.ent"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-symbol.ent"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-special.ent"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-text-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-inlstruct-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-inlphras-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/ruby/xhtml-ruby-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-blkstruct-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-blkphras-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-hypertext-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-list-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-edit-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-bdo-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-pres-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-inlpres-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-blkpres-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-link-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-meta-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-base-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-script-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-style-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-image-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-csismap-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-ssismap-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-param-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-object-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-table-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-form-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select
		Case "http://www.w3.org/tr/xhtml-modularization/dtd/xhtml-struct-1.mod"
			entityObj = assembly.GetManifestResourceStream(Me.[GetType](), Path.GetFileName(absoluteUri.AbsolutePath))
			Exit Select				
'			Case Else
'				Dim xur As New XmlUrlResolver()
'				Return xur.GetEntity(absoluteUri, role, ofObjectToReturn)
		End Select			

		If entityObj IsNot Nothing Then
			' return the local resource
		Else
			Dim xmlRes As New XmlUrlResolver()
			entityObj = xmlRes.GetEntity(absoluteUri, role, ofObjectToReturn)
		End If
		Return entityObj
	End Function
End Class
