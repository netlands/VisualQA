' plug-in component for Canon IJ specific functionallity
'
' name of the file indicates the pluginname (no spaces or underscores)
' followed by a single underscore and the extension or filetype

' updated ui patterns to match the new UI Manager based format

Imports visualqa.MainForm
Imports System.IO

Public Class canon_html
	Implements VisualQAPlugIn
	
	Dim private _extensions As String = "html"

 	Public ReadOnly Property Name() As String Implements VisualQAPlugIn.Name
        Get
			Return "Canon IJ XHTML"
        End Get
    End Property	
	
 	Public ReadOnly Property Description() As String Implements VisualQAPlugIn.Description
        Get
			Return "Canon IJ XHTML Specific Functionality"
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
		Dim changeFile As String = Path.Combine(path.GetDirectoryName(fileIn),path.GetFileNameWithoutExtension(fileIn) & ".diff")
		Debug.WriteLine(VbCrLf & changeFile & VbCrLf)
		
		If File.exists(changeFile) then
			
			Dim newStcStyle As String = "yellow"
			Dim leveragedStcStyle As String = "LightGray"
			Dim referencedStcStyle As String = "cyan"
			Dim uiTermStyle As String = "Purple"
			Dim problemStcStyle As String = "Pink"
	
			Dim diff() As String = File.ReadAllText(changeFile).Split(VbLf)
			
			Dim page as mshtml.HTMLdocument = browser.document.Domdocument
			Dim Elements as mshtml.IHTMLElementCollection = page.getElementsByTagName("span")
			
			dim classCounter As integer = 0
			For Each obj As mshtml.IHTMLElement In Elements
				' to get class attributes use className, for attribute uses htmlFor
				If Not obj.getAttribute("className") Is Nothing Then
					Dim attrValue As object = obj.getAttribute("className",2)
					If attrValue.ToString = "stc" Then
						classCounter = classCounter + 1
						For Each newString As String In diff
							If newString.StartsWith("STC") Then
								Dim sentence() As String = newString.Split(",")
								If classCounter = CInt(sentence(1)) Then
					                Select CInt(sentence(2))
									Case 0, 1
										obj.style.backgroundColor = newStcStyle
									Case 100, 101
										obj.style.backgroundColor = leveragedStcStyle
									Case 200, 201
										obj.style.backgroundColor = referencedStcStyle
									End Select
							
										' verify if string is left in English
										If Not fileIn.Contains("\EN\") then ' fileIn.Contains("/EN/")
										If Trim(sentence(4)) = helpers.getMD5hash(helpers.simplifyXml(obj.innerHTML)) Then
											Dim leftInEnglish As Boolean = True
											If Len(Trim(system.Text.RegularExpressions.Regex.Replace(obj.innerHTML,"<span class=""ui"".*?>.+?</span>",""))) = 0 Then
												' UI
												leftInEnglish = False
											Else If Len(Trim(obj.InnerText)) = 0 Then
												' spaces only
												leftInEnglish = False
											Else If Len(Trim(system.Text.RegularExpressions.Regex.Replace(obj.InnerText,"\([A-Z]|[0-9]{1,2}\)",""))) = 0
												' numbering
												leftInEnglish = False
											Else If Len(Trim(system.Text.RegularExpressions.Regex.Replace(obj.InnerText,"[A-Z][0-9/]{1,}",""))) = 0
												' code like
												leftInEnglish = False
											Else If Len(Trim(system.Text.RegularExpressions.Regex.Replace(obj.InnerText,"[-+*\\/0-9#]{1,}",""))) = 0
												' numbers only
												leftInEnglish = False
		'									Else If Not ignoreList Is Nothing
		'										If numberOfMatches(ignoreList, Trim(obj.InnerText)) > 0 Then
		'								    		' ignore list
		'											leftInEnglish = False
		'										End If
											Else
												leftInEnglish = True
											End If
										
											If leftInEnglish Then obj.style.backgroundColor = problemStcStyle
											
										End If
										End If
									Exit For
								End If
							End If
						Next
					End If

				End If
			Next


			For Each obj In Elements
				If Not obj.getAttribute("className") Is Nothing Then
					Dim attrValue As object = obj.getAttribute("className",2)
					If attrValue.ToString = "ui" Then
						obj.style.Color = uiTermStyle
					End If
				End If
			Next
			
			elements = nothing
			page = Nothing
			Return True
		Else

			' highlight UI terms
			Dim page as mshtml.HTMLdocument = browser.document.Domdocument
			Dim Elements as mshtml.IHTMLElementCollection = page.getElementsByTagName("span")			
			
			system.Diagnostics.debug.WriteLine(elements.length)
			For Each obj As mshtml.IHTMLElement In Elements
				' to get class attributes use className, for attribute uses htmlFor
				If Not obj.getAttribute("className") Is Nothing Then
					Dim attrValue As object = obj.getAttribute("className",2)
					If System.Text.RegularExpressions.Regex.IsMatch(attrValue.ToString,"^ui .+?$") Then
						obj.style.backgroundColor =  "greenyellow" ' "lightgreen"
					Else If attrValue.ToString = "os_ui" OR attrValue.ToString = "other_ui" Or attrValue.ToString = "ui" Then
						obj.style.backgroundColor = "turquoise" ' "lightblue"
					End If
					' obj.value = "TEST"
					attrValue = Nothing
				End If
			Next			

			Return True
		End If

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
	
	Public Shared Function getMD5hash(textIn As String) As String
	
		Dim s As String = textIn
		Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
		
		Dim bs As Byte() = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(s))

		'Dim result as String = BitConverter.ToString(bs).ToLower().Replace("-","")
		Dim b As Byte
		Dim result As New System.Text.StringBuilder()
		For Each b In bs
		    result.Append(b.ToString("x2"))
		Next
		
		getMD5hash = result.ToString
		' Console.WriteLine(result)
	End Function
	
	Shared Function simplifyXML(xmlIn As String) As String
		' remove tags and spaces for Left-in-English check
		simplifyXML = system.Text.RegularExpressions.Regex.Replace(xmlIn,"<[^<]+?>","")
		simplifyXML = system.Web.httputility.HtmlDecode(simplifyXML)
		simplifyXML = system.Text.RegularExpressions.Regex.Replace(simplifyXML,"[\s]{1,}"," ")
		simplifyXML = Trim(simplifyXML)
		' Debug.WriteLine("%%%" & simplifyXML & "%%%")
		Return simplifyXML
	End Function
	
End Class