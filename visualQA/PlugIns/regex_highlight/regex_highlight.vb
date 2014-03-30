' plug-in component for regular expression based highlight functionallity
'
' name of the file indicates the pluginname (no spaces or underscores)
' followed by a single underscore and the extension or filetype

Imports visualqa.MainForm
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class regex_highlight
	Implements VisualQAPlugIn
	
	Dim private _extensions As String = "html"

	Dim Public Shared searchTerms(,) As String = Nothing

 	Public Sub New()
 		' read expressions
 		If searchTerms Is Nothing Then
	 		
	 		If file.Exists(path.combine(path.GetDirectoryName(System.Windows.Forms.application.ExecutablePath),"PlugIns\regex_highlight.txt")) Then
	 			Debug.WriteLine("EXISTS")
	 			searchTerms = helpers.FileToArray(path.combine(path.GetDirectoryName(System.Windows.Forms.application.ExecutablePath),"PlugIns\regex_highlight.txt"))
	 		Else
	 			Debug.WriteLine("")
	 			searchTerms = Nothing
	 		End If
	 		
 		End If
 	End Sub

 	Public ReadOnly Property Name() As String Implements VisualQAPlugIn.Name
        Get
			Return "Regex Highlighter"
        End Get
    End Property	
	
 	Public ReadOnly Property Description() As String Implements VisualQAPlugIn.Description
        Get
			Return "Regex Based Highlighter for Visual QA"
        End Get
    End Property
	
 	Public ReadOnly Property Extensions() As String Implements VisualQAPlugIn.Extensions
        Get
			Return _extensions
        End Get
    End Property
    
  
    Public ReadOnly Property CanHighlight() As Boolean Implements VisualQAPlugIn.CanHighlight
        Get
			Return True
        End Get
    End Property
	 
	 
	 
	 
  	Public Function Highlight(browser As System.Windows.Forms.WebBrowser) As Boolean Implements VisualQAPlugIn.Highlight

		Dim fileIn As String = browser.Document.Url.LocalPath ' browser.Url.AbsolutePath
		
		' Dim searchTerms() As String = {"Ctrl", "EX", "Easy"}
		Dim highlightColor As String = "cyan"
		
		Dim page as mshtml.HTMLdocument = browser.document.Domdocument ' Dim page as mshtml.IHTMLdocument2 = browser.document.Domdocument
		' /// Dim html as String = page.body.outerHTML ' StringBuilder = new StringBuilder(page.body.outerHTML)
		

Dim html As String = browser.DocumentText
	
	html = Regex.Match(html,"\<body[^>]*?\>.+?\</body\>",RegexOptions.Singleline).Value
		Dim Elements as mshtml.IHTMLElementCollection = page.getElementsByTagName("body")
		For Each obj As mshtml.IHTMLElement In Elements
			Debug.WriteLine(searchTerms.GetUpperBound(1).ToString)

'			Dim html As String = obj.innerText
'			If html Is Nothing Then Exit For
				
			For n As Integer = 1 To searchTerms.GetUpperBound(1)
				Debug.WriteLine(n)
				Dim	searchTerm As String = searchTerms(0,n)
				highlightColor = searchTerms(2,n)
				Dim options As RegexOptions = regexoptions.IgnoreCase
				If searchTerms(1,n).ToUpper = "FALSE" Then 
					options = Nothing ' Not regexoptions.IgnoreCase
				End If

				Try
				Dim i As Integer = 0
				Dim matches As MatchCollection = regex.Matches(html,searchTerm,options)
				Debug.WriteLine(searchTerm & " (" & matches.Count.ToString & ")")				
				For Each match As Match In matches
					
					Dim matchStart As Integer = match.Index + i
					Dim matchEnd As Integer = match.Index + match.Length + i
					Dim matchValue As String = html.Substring(matchStart, match.Length) ' match.Value
					
					If html.LastIndexOf(">",matchStart) < html.LastIndexOf("<",matchStart) Then
						' start inside tag
						matchStart = html.LastIndexOf("<",matchStart) 
					End If
					
					Dim endInTag As Boolean = False
					If html.IndexOf(">",matchEnd) < html.IndexOf("<",matchEnd) Then
						' end inside tag
						matchEnd = html.IndexOf(">",matchEnd) + 1
						i = i + 1
						endInTag = True
					End If
					
					matchValue = html.Substring(matchStart,matchEnd - matchStart)
					
					' check if match is completely inside a tag
					If matchValue.StartsWith("<") And matchValue.EndsWith(">") And Not matchValue.Substring(1).Contains("<") Then
						if endInTag Then i = i - 1
					Else
						i = i + ("<span style=""background-color: " & highlightColor & ";""></span>").Length
						html = html.Substring(0,matchStart) & "<span style=""background-color: " & highlightColor & ";"">" & matchValue & "</span>" & html.Substring(matchEnd)		
					End If
					
				Next
				Catch ex As Exception
					Debug.WriteLine(ex.Message)
				End Try	
							
			Next
			
' browser.DocumentText = html	

			obj.innerHTML = html
		Next


		'   IHTMLDocument2 doc2 = webBrowser1.Document.DomDocument as IHTMLDocument2;
		'   StringBuilder html = new StringBuilder(doc2.body.outerHTML);
		
		'	   var words = new[] { "laureati", "passione" };
		'	   foreach (String key in words)
		'	   {
		'	      String substitution = "<span style='background-color: rgb(255, 255, 0);'>" + key + "</span>";
		'	      html.Replace(key, substitution);
		'	   }
		
		   		' /// page.body.innerHTML = html.ToString()
				' /// html = nothing
				
				
'		elements = Nothing
		page = Nothing
		Return True

    End Function

    
   Public ReadOnly Property CanDiff() As Boolean Implements VisualQAPlugIn.CanDiff
        Get
			Return False
        End Get
    End Property

  	Public Function diff(browser1 As System.Windows.Forms.WebBrowser, browser2 As System.Windows.Forms.WebBrowser) As Boolean Implements VisualQAPlugIn.Diff
  		Return False
  	End Function

    Public ReadOnly Property CanCheck() As Boolean Implements VisualQAPlugIn.CanCheck
        Get
			Return False
        End Get
    End Property

  	Public Function check(browser As System.Windows.Forms.WebBrowser) As Boolean Implements VisualQAPlugIn.Check
  		Return False
  	End Function 
 
    Public ReadOnly Property CanCompare() As Boolean Implements VisualQAPlugIn.CanCompare
        Get
			Return False
        End Get
    End Property

  	Public Function compare(browser1 As System.Windows.Forms.WebBrowser, browser2 As System.Windows.Forms.WebBrowser) As Boolean Implements VisualQAPlugIn.Compare
  		Return False
  	End Function

    
    Public ReadOnly Property hasTool1() As Boolean Implements VisualQAPlugIn.hasTool1
        Get
			Return False
        End Get
    End Property

  	Public Function tool1(Optional browser As System.Windows.Forms.WebBrowser = Nothing, Optional textIn As String = Nothing) As String Implements VisualQAPlugIn.tool1
  		If browser Is Nothing Then
  			Return "Tool 1 Name"
  		Else
  			Return "done"
  		End If
  	End Function 
 
    Public ReadOnly Property hasTool2() As Boolean Implements VisualQAPlugIn.hasTool2
        Get
			Return False
        End Get
    End Property

  	Public Function tool2(Optional browser As System.Windows.Forms.WebBrowser = Nothing, Optional textIn As String = Nothing) As String Implements VisualQAPlugIn.tool2
  		If browser Is Nothing Then
  			Return "Tool 2 Name"
  		Else
  			Return "done"
  		End If
  	End Function  
 
     Public ReadOnly Property isFilter() As Boolean Implements VisualQAPlugIn.isFilter
        Get
			Return False
        End Get
    End Property

  	Public Function filter(browser As System.Windows.Forms.WebBrowser, filePath As String) As boolean Implements VisualQAPlugIn.filter
  		return False
  	End Function  
 
End Class

Class helpers
	
	' read separated text file into multi-dimensional array
	Public Shared Function FileToArray(fileIn As String) As String(,)
		Debug.WriteLine("Reading " & fileIn)
		Dim arrayOut(,) As String = Nothing
		Dim tempArray() As String
		Dim n As Integer = 0
		Dim line As String = ""
		Dim sr As New IO.StreamReader(fileIn)
		While Not sr.EndOfStream
			line = sr.ReadLine
			If Not line.startsWith("#") Or Trim(line).Length = 0 Then
				tempArray = Split(line, vbTab)
				n = n + 1
				Redim Preserve arrayOut(2,n)
				
				arrayOut(0,n) = Trim(tempArray(0))
				arrayOut(1,n) = convertBool(Trim(tempArray(1))).ToString.ToUpper
				arrayOut(2,n) = tempArray(2)
				
			End If
		End While
		Debug.WriteLine("Read " & arrayOut.GetUpperBound(1) & " items")
		sr.Close()
		return arrayOut
	End Function

	Public Shared Function convertBool(ByVal stringIn As String) As Boolean
		Select Case UCase(stringIn)
			Case "TRUE"
				Return True
			Case "FALSE"
				Return False
			Case "T"
				Return True
			Case "F"
				Return False
			Case "1"
				Return True
			Case "0"
				Return False
			Case Else
				Return False
		End Select
	End Function

End Class