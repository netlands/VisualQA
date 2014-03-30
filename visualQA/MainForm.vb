Option Explicit On

' visual QA tool

' CABBAGE ROLL v0
' Combined (syncro) scroll for IE based documents
' support for html, xml, jpg, gif ...

' Cinamon scRoll

' TODO: Get current position in file using IHTMLCaret (IMarkupPointer for element)

Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.XPath
Imports System.Text
' Imports System.Web.HttpUtility

Imports System.Runtime.InteropServices
' Imports mshtml

Imports System.Drawing.Image

Imports Microsoft.Win32 ' registry access

Public Partial Class MainForm
	
	Dim debug As Boolean = False ' True
	
	Dim appFolder As String
	Dim appName As String
	Dim xcfgFileName As String = "project.xcfg"
	
	Dim sourceFile, targetFile As String
	
	Dim sourceDoc, targetDoc As HtmlDocument
	Dim sourceDom, targetDom As mshtml.IHTMLDocument2 ' Object
	
	Dim sourceWindow, targetWindow As mshtml.IHTMLWindow2
	
	Dim logDoc As HtmlDocument
	Dim logDom As mshtml.IHTMLDocument2
	Dim logWindow As mshtml.IHTMLWindow2
	
	' default sync type
	Dim panelMode as Boolean = False
	Dim browserMode As Boolean = True
	Dim imageMode As Boolean = False
	
	Dim annotateImageMode As Boolean = False
	
	' default control type
	Dim useSendKeys As Boolean = False
	Dim useMouseGestures As Boolean = True
	
	' control vars
	Dim scrollbarWidth As Integer = 17
	
	Dim line As Integer = 10
	
	Dim defaultFilter As String = "*.htm;*.html"
	Dim defaultSourceFolder As String = "EN,EN-US,English,Source"
	
	Dim defaultEditor As String = "notepad.exe"
	Dim fileExplorer As String = "explorer.exe" ' "C:\WINDOWS\explorer.exe"
	Dim fileCompare As String = Nothing
	Dim imageEditor As String = "mspaint.exe"
	
	' search box formatting
	Private textBox1Icon As vbAccelerator.Components.Controls.TextBoxMarginCustomise
	
	Dim autoUpdateCheckStatus As Boolean = True
	
	Dim parseXMLonLoad As Boolean = True
	
	Dim FOLDER_CLOSED_ICON As Integer = 0
	Dim FOLDER_OPEN_ICON As Integer = 1
	Dim FILES_ICON As Integer = 2
	Dim FILES_UNCHECKED_ICON As Integer = 3
	Dim FILES_PROBLEM_ICON As Integer = 4
	Dim FILES_IDENTICAL_ICON As Integer = 5
	Dim FILES_MISSING_ICON As Integer = 6
	Dim FILES_ORPHAN_ICON As Integer = 7
	Dim FILE_ICON As Integer = 8
	Dim FILE_EDIT_ICON As Integer = 9
	Dim FILE_FLAGGED_ICON As Integer = 10
	Dim FILE_OK_ICON As Integer = 11
	Dim FILE_NG_ICON As Integer = 12
	
	Private WithEvents mouseGestureFilter As vbAccelerator.Components.Win32.MouseGestureFilter

	Private WithEvents WebBrowser1 As New ExWebBrowser
	Private WithEvents WebBrowser2 As New ExWebBrowser

	
	' plug-in interface code
	Public Interface VisualQAPlugIn

	    ReadOnly Property Extensions() As String
		
		ReadOnly Property Name() As String
	
		ReadOnly Property Description() As String
		
		ReadOnly Property canHighlight() As Boolean
	    Function Highlight(browser As System.Windows.Forms.WebBrowser) As Boolean

		ReadOnly Property canDiff() As Boolean
	    Function diff(browser1 As System.Windows.Forms.WebBrowser, browser2 As System.Windows.Forms.WebBrowser) As Boolean

		ReadOnly Property canCheck() As Boolean
	    Function check(browser As System.Windows.Forms.WebBrowser) As Boolean
	    
		ReadOnly Property canCompare() As Boolean
	    Function compare(browser1 As System.Windows.Forms.WebBrowser, browser2 As System.Windows.Forms.WebBrowser) As Boolean

		ReadOnly Property hasTool1() As Boolean
	    Function tool1(Optional browser As System.Windows.Forms.WebBrowser = Nothing, Optional textIn As String = Nothing) As String
	    
		ReadOnly Property hasTool2() As Boolean
	    Function tool2(Optional browser As System.Windows.Forms.WebBrowser = Nothing, Optional textIn As String = Nothing) As String

		ReadOnly Property isFilter() As Boolean
	    Function filter(browser As System.Windows.Forms.WebBrowser, filePath As String) As Boolean
	    
	End Interface
	

	Private Function LoadPlugIn(ByVal LoadPath As String) As VisualQAPlugIn
	    Dim NewPlugIn As VisualQAPlugIn = Nothing
	    Try
	        Dim PlugInAssembly As Reflection.Assembly = Reflection.Assembly.LoadFrom(LoadPath)
	        Dim Types() As Type
	        Dim FoundInterface As Type
	        Types = PlugInAssembly.GetTypes
	        For Each PlugInType As Type In Types
	            FoundInterface = PlugInType.GetInterface("VisualQAPlugIn")
	            If FoundInterface IsNot Nothing Then
	                NewPlugIn = DirectCast(PlugInAssembly.CreateInstance(PlugInType.FullName), VisualQAPlugIn)
	                Exit For
	            End If
	        Next
	    Catch ex As Exception
	        'handle exceptions here
	    End Try
	    Return NewPlugIn
	End Function
	
	
	Public Sub New()
	
		diagnostics.debug.writeline("Reading defaults and command line arguments")
		
		If applicationActive Then End

		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		' set environment to default values
		appFolder = Mid(System.Windows.Forms.Application.ExecutablePath.ToString, 1,InstrRev(System.Windows.Forms.Application.ExecutablePath.ToString,"\"))
		appName = Mid(System.Windows.Forms.Application.ExecutablePath.ToString, InstrRev(System.Windows.Forms.Application.ExecutablePath.ToString,"\") + 1)
		
		' initialize interface components
		Dim Screens() As System.Windows.Forms.Screen = System.Windows.Forms.Screen.AllScreens
		If Screens.GetUpperBound(0)	>= 1 then DualscreenToolStripMenuItem.Visible = True
		Screens = Nothing
		
		' initialize menu conetents
		initEncodingMenuContents()

		webBrowser1.DocumentText = "<html><head><title>Source</title>" & _
			"<style type=""text/css""><!-- body {background-color: #ADD8E6;} h3 {font-family: Tahoma, Arial; font-weight: normal; text-align: center;} --></style></head>" & _
			"<body><br /><br /><h3>Drop source document here</h3></body></html>"
			
		webBrowser2.DocumentText = "<html><head><title>Source</title>" & _
			"<style type=""text/css""><!-- body {background-color: #90EE90;} h3 {font-family: Tahoma, Arial; font-weight: normal; text-align: center;} --> </style></head>" & _
			"<body><br /><br /><h3>Drop target document here</h3></body></html>"

		' get installed plug-ins
		contextMenuStripHighlightPlugins.Items.Clear()
		Dim hlPluginCount As Integer = 0
		If Directory.Exists(path.Combine(application.StartupPath,"Plugins")) Then
			For Each plugin As String In Directory.GetFiles(path.Combine(application.StartupPath,"Plugins"),"*.dll")
				system.Diagnostics.Debug.WriteLine(plugin)
				Try
					Dim _PlugIn As VisualQAPlugIn = loadPlugin(plugin)
					If _PlugIn.canHighlight Then contextMenuStripHighlightPlugins.Items.Add(_PlugIn.Name)
					hlPluginCount = hlPluginCount + 1
					_plugin = Nothing
				Catch ex As Exception
				End Try
			Next
		End If
		If hlPluginCount > 0 Then
'			pluginsToolStripMenuItem.Visible = True
'			toolstripSeparator25.Visible = True
			highlightUITermsToolStripMenuItem.Visible = True
			For Each item As ToolStripMenuItem In contextMenuStripHighlightPlugins.Items
				item.CheckOnClick = True
			Next
		End If
		
		
		' set default files
		If file.Exists(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) & "\source.htm") Then _
				sourceFile = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) & "\source.htm"
		If file.Exists(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) & "\target.htm") then _
				targetFile = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) & "\target.htm"
				
		' load settings from default file (if any)
		If file.Exists(application.StartupPath & "\VisualQA.cfg") then readXcfg(application.StartupPath & "\VisualQA.cfg", True)
		fitImages = FitImagesToolStripMenuItem1.Checked
		
		' process dropped files and arguments
		getCommandLineArguments()
				
				
		' check for tools, set prefered tools, check for cmd file, check for lnk file (overrides saved settings)
		' If File.Exists("C:\Program Files\EmEditor\emeditor.exe") then defaultEditor = "C:\Program Files\EmEditor\emeditor.exe"
		If File.Exists(appFolder & "edit.cmd") Then defaultEditor = appFolder & "edit.cmd"
		If File.Exists(appFolder & "edit.lnk") Then defaultEditor = appFolder & "edit.lnk" ' path part added for commandline compatibility
		
		' If File.Exists("C:\Program Files\Beyond Compare 3\BCompare.exe") then fileCompare = "C:\Program Files\Beyond Compare 3\BCompare.exe"
		If File.Exists(appFolder & "compare.cmd") Then fileCompare = appFolder & "compare.cmd"
		If File.Exists(appFolder & "compare.lnk") Then fileCompare = appFolder & "compare.lnk" ' path part added for commandline compatibility
		
		' If File.Exists("C:\Program Files\Paint.NET\PaintDotNet.exe") then imageEditor = "C:\Program Files\Paint.NET\PaintDotNet.exe"
		If File.Exists(appFolder & "ImageEditor.cmd") Then imageEditor = appFolder & "ImageEditor.cmd"
		If File.Exists(appFolder & "ImageEditor.lnk") Then imageEditor = appFolder & "ImageEditor.lnk" ' path part added for commandline compatibility
		
		If File.Exists(appFolder & "explorer.cmd") Then fileExplorer = appFolder & "explorer.cmd"
		If File.Exists(appFolder & "explorer.lnk") Then fileExplorer = appFolder & "explorer.lnk" ' path part added for commandline compatibility
								
	End Sub
			
			
			Function applicationActive() As Boolean ' Function PrevInstance() As Boolean
				If Ubound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
					Return True
				Else
					Return False
				End If
			End Function
			
			Dim public shared usingDefaultFilter As Boolean = False
			
			Sub GetCommandLineArguments()
				' get command line arguments
				Dim folderCount As Integer = 0
				Dim fileCount As Integer = 0
				
				If Environment.GetCommandLineArgs.Length > 1 Then ' 0 is the app itself
					
					For n As Integer = 1 To Environment.GetCommandLineArgs.Length - 1
						
						Dim argument As String = Environment.GetCommandLineArgs(n).ToString
						
						' get folders (dropped)
						If Directory.Exists(argument) then
							If folderCount = 0 Then ' 1st folder
								folderCount = folderCount + 1
								sourceFolder = argument
							Else
								If folderCount = 1 Then ' get output folder (2nd folder)
									folderCount = folderCount + 1
									targetFolder = argument
								End If
							End If
						End If
						
						' check for preferred default source folder
						Try
						If Not targetFolder Is Nothing then
							Dim _defaultSourceFolder As String = "," & defaultSourceFolder.ToUpper() & ","
							Dim _targetFolder As String = "," & targetFolder.ToString.ToUpper().Trim("\").SubString(targetFolder.ToString.lastIndexOf("\")+1) & ","
							If _defaultSourceFolder.Contains(_targetFolder) Then
								_targetFolder = targetFolder
								targetFolder = sourceFolder
								sourceFolder = _targetFolder
							End If
						End If
						Catch E As Exception
						End Try
						
						' get filter
						If Mid(Lcase(argument),1,2) = "/f" Or _
								Mid(Lcase(argument),1,2) = "-f" Then
							usingDefaultFilter = False
							filter = Trim(Mid(argument,3))
							
'						Else ' set filter based on first file type in source folder
'							If folderCount >= 1 then
'								Dim _files As String() = directory.GetFiles(sourceFolder)
'								If _files.Length > 0 Then
'									For Each _file As String In _Files
'										If Not len(trim(path.GetExtension(_file))) = 0  Then
'											filter = "*" & path.GetExtension(_file)
'											Exit For
'										End If
'										_file = Nothing
'									Next
'								End If
'								_files = Nothing
'							End If
							
						Else ' use fixed filter
							usingDefaultFilter = True
							filter = defaultFilter ' "*.htm;*.html" ' *.htm;*.html;*.xml;*.gif;*.jpg;*.png"
						End If
						
						
						' process subfolders?
						If Mid(Lcase(argument),1,2) = "/s" Or _
								Mid(Lcase(argument),1,2) = "-s" Then
							subfolders = True
						End If
						
						' if a configuration file is dropped
						If File.Exists(argument) And lcase(path.GetExtension(argument)) = ".xcfg" Then
							readXcfg(argument)
							projectFile = argument
							projectName = Path.GetFileNameWithoutExtension(argument)
							saveStatus = True
							
							SaveProjectToolStripMenuItem.Text = "Save project as..."
							SaveProjectToolStripMenuItem1.Visible = True
										
							Exit Sub
						End If
						
						' if files are dropped
						If File.Exists(argument) then
							If fileCount = 0 Then ' source
								fileCount = fileCount + 1
								sourceFile = argument
							Else
								If fileCount = 1 Then ' target
									fileCount = fileCount + 1
									targetFile = argument
								End If
							End If
						End If
						
					Next
					
					' if only one folder is dropped asume target is source
					If folderCount = 1 then targetFolder = sourceFolder
					' sub-folders should be included for ease of use
					If folderCount >= 1 Then subfolders = True
					
					folderCount = Nothing
					fileCount = Nothing
					
				End If
			End Sub

    Private Sub WebBrowser1_MouseLeave(ByVal e As System.Windows.Forms.MouseEventArgs) Handles WebBrowser1.MouseLeave
        panel4.Focus()
    End Sub

    Private Sub WebBrowser2_MouseLeave(ByVal e As System.Windows.Forms.MouseEventArgs) Handles WebBrowser2.MouseLeave
        panel4.Focus()
    End Sub
			
			Sub MainFormLoad(sender As Object, e As System.EventArgs)

			diagnostics.debug.writeline("Initialize the dialog")

	Me.webBrowser1.Location = New System.Drawing.Point(0, 0)
	Me.webBrowser1.Margin = New System.Windows.Forms.Padding(0)
	Me.webBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
	Me.webBrowser1.Name = "webBrowser1"
	Me.webBrowser1.Size = New System.Drawing.Size(200, 300)
	Me.webBrowser1.TabIndex = 0
	AddHandler Me.webBrowser1.DocumentCompleted, AddressOf Me.WebBrowser1DocumentCompleted
	AddHandler Me.webBrowser1.Navigated, AddressOf Me.WebBrowser1Navigated
	Me.panel1Contents.Controls.Add(Me.webBrowser1)
	
	Me.webBrowser2.Location = New System.Drawing.Point(0, 0)
	Me.webBrowser2.Margin = New System.Windows.Forms.Padding(0)
	Me.webBrowser2.MinimumSize = New System.Drawing.Size(20, 20)
	Me.webBrowser2.Name = "webBrowser2"
	Me.webBrowser2.Size = New System.Drawing.Size(200, 300)
	Me.webBrowser2.TabIndex = 1
	AddHandler Me.webBrowser2.DocumentCompleted, AddressOf Me.WebBrowser2DocumentCompleted
	AddHandler Me.webBrowser2.Navigated, AddressOf Me.WebBrowser2Navigated
	Me.panel2Contents.Controls.Add(Me.webBrowser2)

	' to get selected text
	AddHandler Me.webBrowser1.MouseUp, AddressOf Me.WebBrowser1MouseUp
	AddHandler Me.webBrowser2.MouseUp, AddressOf Me.WebBrowser2MouseUp
	
				AddHandler webbrowser1.DocumentCompleted, New _
					WebBrowserDocumentCompletedEventHandler(AddressOf getSourceDocument)
				
				AddHandler webbrowser2.DocumentCompleted, New _
					WebBrowserDocumentCompletedEventHandler(AddressOf getTargetDocument)

				If splitContainer1.IsSplitterFixed = True AND splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical  Then _
					splitContainer1.SplitterDistance = (splitContainer1.Width/2)-(splitContainer1.SplitterWidth/2)
				If splitContainer1.IsSplitterFixed = True AND splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal  Then _
						splitContainer1.SplitterDistance = (splitContainer1.Height/2)-(splitContainer1.SplitterWidth/2)

				
				' comment box
				Me.panelContents.Controls.Add(Me.panelComments)
				Me.PanelComments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
				Me.panelComments.Controls.Add(Me.textBoxComments)
				Me.textBoxComments.Multiline = True
				Me.textboxComments.BorderStyle = System.Windows.Forms.BorderStyle.None
				Me.textBoxComments.BackColor = Color.LemonChiffon ' Color.LightYellow
				
				Me.panelComments.Height = textBoxComments.Height * 3
				Me.textBoxComments.Height = Me.panelComments.Height
				Me.textBoxComments.Dock = System.Windows.Forms.DockStyle.Top
				Me.panelComments.Dock = System.Windows.Forms.DockStyle.Bottom
				Me.panelComments.Visible = False
				
				Treeview1.FullRowSelect = False
				TreeView1.ShowRootLines = False
				Treeview1.ContextMenuStrip = contextMenuStripTreeFilter
				
				AddHandler Me.textBoxComments.TextChanged, AddressOf Me.textBoxCommentsTextChanged
				
				' to interact
				webbrowser1.ObjectForScripting = Me
				webbrowser2.ObjectForScripting = Me
				
				webbrowser1.Dock = Dockstyle.Fill
				webbrowser2.Dock = Dockstyle.Fill
				
				' do not popup script error dialogs
				webbrowser1.ScriptErrorsSuppressed = True
				webbrowser2.ScriptErrorsSuppressed = True
				
					If setSize > 0 Then
						setBrowserSize(setSize)
					End If
				
				' check and set IE mode
				getIEmode() ' deleteIEmode

				Try
				Dim maxBrowserVersion As Integer = webbrowser1.Version.Major.ToString
				Select Case maxBrowserVersion
					Case 6
						IE7ToolStripMenuItem.Visible = False
						IE8ToolStripMenuItem.Visible = False
						IE9ToolStripMenuItem.Visible = False
						IE10ToolStripMenuItem.Visible = False
						IE11ToolStripMenuItem.Visible = False
						toolStripSeparator14.Visible = False
					Case 7
						IE7ToolStripMenuItem.Visible = True
						IE8ToolStripMenuItem.Visible = False
						IE9ToolStripMenuItem.Visible = False
						IE10ToolStripMenuItem.Visible = False
						IE11ToolStripMenuItem.Visible = False
						toolStripSeparator14.Visible = True
					Case 8
						IE7ToolStripMenuItem.Visible = True
						IE8ToolStripMenuItem.Visible = True
						IE9ToolStripMenuItem.Visible = False
						IE10ToolStripMenuItem.Visible = False
						IE11ToolStripMenuItem.Visible = False
						toolStripSeparator14.Visible = True
					Case 9
						IE7ToolStripMenuItem.Visible = True
						IE8ToolStripMenuItem.Visible = True
						IE9ToolStripMenuItem.Visible = True
						IE10ToolStripMenuItem.Visible = False
						IE11ToolStripMenuItem.Visible = False
						toolStripSeparator14.Visible = True
					Case 10
						IE7ToolStripMenuItem.Visible = True
						IE8ToolStripMenuItem.Visible = True
						IE9ToolStripMenuItem.Visible = True
						IE10ToolStripMenuItem.Visible = True
						IE11ToolStripMenuItem.Visible = False
						toolStripSeparator14.Visible = True
					Case 11
						IE7ToolStripMenuItem.Visible = True
						IE8ToolStripMenuItem.Visible = True
						IE9ToolStripMenuItem.Visible = True
						IE10ToolStripMenuItem.Visible = True
						IE11ToolStripMenuItem.Visible = True
						toolStripSeparator14.Visible = True						
				End Select
				Catch ex As Exception
					System.Diagnostics.debug.WriteLine("Problem getting maximum browser version")
				End Try
				
				initLogWindow()
				
				' saved searches
				readSavedSearch(savedSearch)

				' load settings from default file (if any)
'				If file.Exists(application.StartupPath & "\VisualQA.cfg") then readXcfg(application.StartupPath & "\VisualQA.cfg", True)
				
				' start project?
				If len(sourceFolder) > 0 Then
					if len(trim(filter)) = 0 then filter = defaultFilter ' "*.htm;*.html;*.xml;*.bmp;*.gif;*.jpg;*.png"
					openProject()
				Else
					' only get type from source (target should match)
					fileType = Mid(Path.GetExtension(sourceFile),2)
					if Trim(fileType) = "" then fileType = "html"
					determineMode(fileType)
					' set initial mode
					setMode()
					
					' load "default" files
					If len(Trim(sourceFile)) > 0 And File.exists(Trim(sourceFile)) Then _
						webBrowser1.Url = New Uri(sourceFile)
					If len(Trim(sourceFile)) > 0 And File.exists(Trim(targetFile)) Then _
						webBrowser2.Url = New Uri(targetFile)
					End If
				
				
				' search textbox formatting (add icon from imagelist)
				textBox1Icon = New vbAccelerator.Components.Controls.TextBoxMarginCustomise()
        		textBox1Icon.ImageList = imageListIcons
        		textBox1Icon.Icon = 0
        		textBox1Icon.Attach(textBox1)
				
				' get default settings
				useMouseGestures = UseMouseGesturesToolStripMenuItem.Checked
				autoUpdateCheckStatus = autoUpdateStatusToolStripMenuItem.Checked
				parseXMLonLoad = parseXMLOnLoadToolStripMenuItem.Checked
				fitImages = FitImagesToolStripMenuItem1.Checked
				
				parseXMLonLoad = ParseXMLOnLoadToolStripMenuItem.Checked
				checkOrphans = CheckForOrphansToolStripMenuItem.Checked
				checkMD5hashes = CheckaddMD5FileHashToolStripMenuItem.Checked
				
				' mouse gestures related
				mouseGestureFilter = New vbAccelerator.Components.Win32.MouseGestureFilter()
				Application.AddMessageFilter(mouseGestureFilter)
											
			End Sub

	Dim selection1, selection2 As string

Function getIndexforDomElement(elem As htmlElement, browser As webbrowser) As Integer
	Dim page as mshtml.HTMLdocument
	Dim Elements as mshtml.IHTMLElementCollection
	Dim elemCount As Integer = 0
	page = browser.document.Domdocument
	elements = page.getElementsByTagName(elem.TagName)
	' walk through document until client rectangles match and return index
	For Each element As mshtml.IHTMLElement In elements
		elemCount = elemCount + 1
'		System.Diagnostics.debug.WriteLine(elem.TagName & " " & elem.OffsetRectangle.Top & "," & elem.OffsetRectangle.Left)
'		System.Diagnostics.debug.WriteLine(element.TagName & " " & element.offsetTop & "," & element.offsetLeft)
		If (elem.OffsetRectangle.Top = element.offsetTop) And (elem.OffsetRectangle.Left = element.offsetLeft) Then
			Exit For
		End If
	Next
	If elemCount > 0 Then
		Dim matches as MatchCollection = regex.Matches(browser.DocumentText,"<" & elem.TagName,regexoptions.IgnoreCase Or regexoptions.Multiline)
		Return matches(elemCount-1).Index + 1
	Else
		Return 0
	End If
End Function

	' get the selected text in webbrowser
	Sub WebBrowser1MouseUp(e As System.Windows.Forms.MouseEventArgs)
		Dim _doc As mshtml.IHTMLDocument2 =  Webbrowser1.document.DomDocument
		Dim _range As mshtml.IHTMLTxtRange
		_range =  _doc.selection.createRange()
		' _range.select()
		selection1 = _range.text ' _range.htmltext
'		 Msgbox(selection1)
'		Dim _ds As mshtml.IDisplayServices
'		_ds = _doc
'		Dim _caret As mshtml.IHTMLCaret = Nothing
'		_ds.GetCaret(_caret)
'		dim _pos As mshtml.tagPOINT
'		_caret.GetLocation(_pos, True)
'		msgbox(_pos.X & "," & _pos.Y & " : " & _range.text)
		Dim ScreenCoord As New Point(MousePosition.X, MousePosition.Y)
		Dim BrowserCoord As Point = webBrowser1.PointToClient(ScreenCoord)
		Dim elem As htmlElement = webbrowser1.Document.GetElementFromPoint(BrowserCoord)
		System.Diagnostics.debug.WriteLine(getLineNumber(webbrowser1.DocumentText,getIndexforDomElement(elem,webbrowser1)) & "," & _
			getColumnNumber(webbrowser1.DocumentText,getIndexforDomElement(elem,webbrowser1)))

		System.Diagnostics.debug.WriteLine(elem.OuterHtml)
		' System.Diagnostics.debug.WriteLine(elem.Id.ToString)
		System.Diagnostics.debug.WriteLine("parent " & elem.Parent.TagName) ' .OuterHtml)
	End Sub

	Sub WebBrowser2MouseUp(e As System.Windows.Forms.MouseEventArgs)
		Dim _doc As mshtml.IHTMLDocument2 =  Webbrowser2.document.DomDocument
		Dim _range As mshtml.IHTMLTxtRange
		_range =  _doc.selection.createRange()
		' _range.select()
		selection2 = _range.text ' _range.htmltext
		' Msgbox(selection2)
'		Dim _ds As mshtml.IDisplayServices
'		_ds = _doc
'		Dim _caret As mshtml.IHTMLCaret = Nothing
'		_ds.GetCaret(_caret)
'		dim _pos As mshtml.tagPOINT
'		_caret.GetLocation(_pos, True)
'		msgbox(_pos.X & "," & _pos.Y & " : " & _range.text)
		Dim ScreenCoord As New Point(MousePosition.X, MousePosition.Y)
		Dim BrowserCoord As Point = webBrowser2.PointToClient(ScreenCoord)
		Dim elem As htmlElement = webbrowser2.Document.GetElementFromPoint(BrowserCoord)
		System.Diagnostics.debug.WriteLine(getLineNumber(webbrowser2.DocumentText,getIndexforDomElement(elem,webbrowser2)) & "," & _
			getColumnNumber(webbrowser2.DocumentText,getIndexforDomElement(elem,webbrowser2)))

		System.Diagnostics.debug.WriteLine(elem.OuterHtml)
		' System.Diagnostics.debug.WriteLine(elem.Id.ToString)
		System.Diagnostics.debug.WriteLine("parent " & elem.Parent.TagName) ' .OuterHtml)
	End Sub
			
			
			Private Sub mouseGestureFilter_MouseGesture(ByVal sender As Object, ByVal args As vbAccelerator.Components.Win32.MouseGestureEventArgs) Handles mouseGestureFilter.MouseGesture
				If annotateImageMode Then Exit Sub
				
				If useMouseGestures then
					Dim gesture As vbAccelerator.Components.Win32.MouseGestureTypes = args.GestureType
					Select Case (gesture)
						Case vbAccelerator.Components.Win32.MouseGestureTypes.NorthGesture
							scrollPageUp()
						Case vbAccelerator.Components.Win32.MouseGestureTypes.SouthGesture
							scrollPageDown()
						Case vbAccelerator.Components.Win32.MouseGestureTypes.EastGesture
							nextFile()
						Case vbAccelerator.Components.Win32.MouseGestureTypes.WestGesture
							previousFile()
							
						Case vbAccelerator.Components.Win32.MouseGestureTypes.NorthThenWestGesture
							LeftScrollPageUp()
						Case vbAccelerator.Components.Win32.MouseGestureTypes.WestThenNorthGesture
							LeftScrollPageUp()
						Case vbAccelerator.Components.Win32.MouseGestureTypes.EastThenNorthGesture
							RightScrollPageUp()
						Case vbAccelerator.Components.Win32.MouseGestureTypes.NorthThenEastGesture
							RightScrollPageUp()
							
						Case vbAccelerator.Components.Win32.MouseGestureTypes.SouthThenWestGesture
							LeftScrollPageDown()
						Case vbAccelerator.Components.Win32.MouseGestureTypes.WestThenSouthGesture
							LeftScrollPageDown()
						Case vbAccelerator.Components.Win32.MouseGestureTypes.EastThenSouthGesture
							RightScrollPageDown()
						Case vbAccelerator.Components.Win32.MouseGestureTypes.SouthThenEastGesture
							RightScrollPageDown()
							
					End Select
					
					args.AcceptGesture = True
				End If
			End Sub
			
			Sub initLogWindow()
				
				' we need a blank document
				WebBrowserLog.Navigate("about:blank" )
				webBrowserLog.ObjectForScripting = Me ' to interact
				
				logDoc = webbrowserLog.Document
				logDom = webbrowserLog.Document.DomDocument
				logWindow = logDom.parentWindow
				
				logDoc.Write("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">" & _
					"<html xmlns=""http://www.w3.org/1999/xhtml"">" & _
					"<title>" & fileList(activeFile, 2) & "</title></head>" & _
					"<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />" & _
					"<style type=""text/css"">" & _
					"  body { font: 8pt Courier New; background-color: White; }" & _
					"  .external { cursor: hand; color: #5F9EA0; text-decoration: none; }" & _
					"  .time { color: gray; }" & _
					"</style>" & _
					"<body><span class=""time"">" & now() & "</span> opening log<br /><br />" & vbcrlf)
				logDoc.Write("<p class=""debug"">" & debugInfo & "</p>")
				logdoc.Write("----------<br /><br />")
				' logDoc.Write("<span class=""external"" onclick=""window.external.Open('" & fixPath("c:\windows") & "')"" >click me!</span><br />")
				
			End Sub
			
			
			' open location from web browser
			Sub Open(path As String, Optional application As String = Nothing)
				If Not application = Nothing Then
					Process.Start(application, path)
				Else
					If file.Exists(path) Then
						Process.Start(path)
						Else if Directory.Exists(path) then
						Process.Start(fileExplorer, path)
					Else
						Messagebox.Show("File or folder not found")
					End If
				End If
			End Sub
			
			Dim fileType As String
			
			Function fixPath(path As String) As String
				Return Replace(path, "\", "\\")
			End Function
			
			
			Sub determineMode(fileFormat As String)
				
				Select lcase(fileFormat)
			Case "html", "htm"
				fileType = "html"
				panelMode = False
				browserMode = True
				imageMode = False
				usesendkeys = False
				
				ImageToolStripMenuItem.Visible = False
				ChangeEncodingToolStripMenuItem.Visible = True
				toolStripSeparator7.Visible = True
				labelImgInfo1.Visible = False
				labelImgInfo2.Visible = False
				
				toolStripSeparator12.Visible = False
				originalSizetoolStripMenuItem.Visible = False
				fitImagesToolStripMenuItem.Visible = False
				SplitViewToolStripMenuItem.Visible = False
				backgroundColorToolStripMenuItem.Visible = False
				
				buttonLineUp.Visible = True
				buttonPageUp.Visible = True
				buttonLineDown.Visible = True
				buttonPageDown.Visible = True
				
			Case "xml"
				fileType = "xml"
				panelMode = False
				browserMode = True
				imageMode = False
				usesendkeys = False
				
				ImageToolStripMenuItem.Visible = False
				ChangeEncodingToolStripMenuItem.Visible = True
				toolStripSeparator7.Visible = True
				labelImgInfo1.Visible = False
				labelImgInfo2.Visible = False
				
				toolStripSeparator12.Visible = False
				originalSizetoolStripMenuItem.Visible = False
				fitImagesToolStripMenuItem.Visible = False
				SplitViewToolStripMenuItem.Visible = False
				backgroundColorToolStripMenuItem.Visible = False
				
				buttonLineUp.Visible = True
				buttonPageUp.Visible = True
				buttonLineDown.Visible = True
				buttonPageDown.Visible = True
				
			Case "image", "bmp", "png", "jpg", "gif"
				fileType = "bitmap"
				panelMode = False
				browserMode = False
				imageMode = True
				usesendkeys = False
				
				' IMAGE
				pictureBox1.Image = Nothing
				pictureBox2.Image = Nothing
				PictureBox1.Top = 0
				PictureBox1.Left = 0
				PictureBox2.Top = 0
				PictureBox2.Left = 0
				
				' clear in case of no file
				labelImgInfo1.Text = ""
				labelImgInfo2.Text = ""
				
				Try
					' pictureBox1.Image = System.Drawing.Image.FromFile(sourceFile)
					' if fitImages then pictureBox1.Image = ResizeImage(System.Drawing.Image.FromFile(sourceFile),panel1Contents.Width,panel1Contents.Height,True,True)
					' labelImgInfo1.Text = "" & System.Drawing.Image.FromFile(sourceFile).Width & " x " & System.Drawing.Image.FromFile(sourceFile).Height & " x " & Image.GetPixelFormatSize(System.Drawing.Image.FromFile(sourceFile).PixelFormat) & ""
						' 2011/11/28 image is locked when using FromFile, using filestream to release image
						Dim fs As System.IO.FileStream
						fs = New System.IO.FileStream(sourceFile, IO.FileMode.Open, IO.FileAccess.Read)
						pictureBox1.Image = System.Drawing.Image.FromStream(fs)
						if fitImages then pictureBox1.Image = ResizeImage(System.Drawing.Image.FromStream(fs),panel1Contents.Width,panel1Contents.Height,True,True)
						labelImgInfo1.Text = "" & System.Drawing.Image.FromStream(fs).Width & " x " & System.Drawing.Image.FromStream(fs).Height & " x " & Image.GetPixelFormatSize(System.Drawing.Image.FromStream(fs).PixelFormat) & ""
						fs.Close()
					sourceToolStripMenuItem.Enabled = True
				Catch e As Exception
					sourceToolStripMenuItem.Enabled = False
				End Try
				
				If Not File.GetLastWriteTime(sourceFile) = File.GetCreationTime(sourceFile) then
					labelFileInfo1.Text = "modified: " & File.GetLastWriteTime(sourceFile) & " (created: " & File.GetCreationTime(sourceFile) & ")"
				Else
					labelFileInfo1.Text = "created: " & File.GetCreationTime(sourceFile)
				End If
				
				Try
					' pictureBox2.Image = System.Drawing.Image.FromFile(targetFile)
					' if fitImages then pictureBox2.Image = ResizeImage(System.Drawing.Image.FromFile(targetFile),panel2Contents.Width,panel2Contents.Height,True,True)
					' labelImgInfo2.Text = "" & System.Drawing.Image.FromFile(targetFile).Width & " x " & System.Drawing.Image.FromFile(targetFile).Height & " x " & Image.GetPixelFormatSize(System.Drawing.Image.FromFile(targetFile).PixelFormat) & ""
						' 2011/11/28 image is locked when using FromFile, using filestream to release image
						Dim fs As System.IO.FileStream
						fs = New System.IO.FileStream(targetFile, IO.FileMode.Open, IO.FileAccess.Read)
						pictureBox2.Image = System.Drawing.Image.FromStream(fs)
						if fitImages then pictureBox2.Image = ResizeImage(System.Drawing.Image.FromStream(fs),panel1Contents.Width,panel1Contents.Height,True,True)
						labelImgInfo2.Text = "" & System.Drawing.Image.FromStream(fs).Width & " x " & System.Drawing.Image.FromStream(fs).Height & " x " & Image.GetPixelFormatSize(System.Drawing.Image.FromStream(fs).PixelFormat) & ""
						fs.Close()
					targetToolStripMenuItem.Enabled = True
				Catch e As Exception
					targetToolStripMenuItem.Enabled = False
				End Try
				
				If Not File.GetLastWriteTime(targetFile) = File.GetCreationTime(targetFile) then
					labelFileInfo2.Text = "modified: " & File.GetLastWriteTime(targetFile) & " (created: " & File.GetCreationTime(targetFile) & ")"
				Else
					labelFileInfo2.Text = "created: " & File.GetCreationTime(targetFile)
				End If

				ImageToolStripMenuItem.Visible = True
				
				ChangeEncodingToolStripMenuItem.Visible = False
				toolStripSeparator7.Visible = False
				labelImgInfo1.Visible = True
				labelImgInfo2.Visible = True
				
				toolStripSeparator12.Visible = True
				originalSizetoolStripMenuItem.Visible = True
				fitImagesToolStripMenuItem.Visible = True
				SplitViewToolStripMenuItem.Visible = True
				backgroundColorToolStripMenuItem.Visible = True
				
				buttonLineUp.Visible = False
				buttonPageUp.Visible = False
				buttonLineDown.Visible = False
				buttonPageDown.Visible = False
				
			Case Else ' default
				panelMode = True
				browserMode = False
				imageMode = False
				usesendkeys = False
				
				ImageToolStripMenuItem.Visible = False
				ChangeEncodingToolStripMenuItem.Visible = True
				toolStripSeparator7.Visible = True
				labelImgInfo1.Visible = False
				labelImgInfo2.Visible = False
				
				toolStripSeparator12.Visible = False
				originalSizetoolStripMenuItem.Visible = False
				fitImagesToolStripMenuItem.Visible = False
				SplitViewToolStripMenuItem.Visible = False
				backgroundColorToolStripMenuItem.Visible = False
				
				buttonLineUp.Visible = True
				buttonPageUp.Visible = True
				buttonLineDown.Visible = True
				buttonPageDown.Visible = True
				
		End Select
	End Sub
	
	Dim targetEncoding, sourceEncoding As String
	
	Private Sub getSourceDocument(ByVal sender As Object, _
			ByVal e As WebBrowserDocumentCompletedEventArgs)
		
		' refreshSourceInfo()
		
		if debug then sourceDoc.backcolor = color.LightBlue
	End Sub
	
	Sub refreshSourceInfo()
		
		sourceFile = webbrowser1.Url.LocalPath
		
		sourceDoc = webbrowser1.Document
		sourceDom = webbrowser1.Document.DomDocument
		sourceWindow = sourceDom.parentWindow
		sourceEncoding = sourceDoc.Encoding
		
		labelEncoding1.Text = ""
		labelEncoding2.Text = ""
		
		Dim _fileName As String = ""
		If len(sourceDoc.Title) > 0 then
			textboxTitle1.Text = sourceDoc.Title
			_fileName = "file: " & Path.GetFileName(sourceFile) & ", "
		Else
			textboxTitle1.Text = Path.GetFileName(sourceFile)
		End If
		If UCASE(Mid(Path.GetExtension(sourceFile),2,3)) = "XML" Or UCASE(Mid(Path.GetExtension(sourceFile),2,3)) = "HTM" then
			labelEncoding1.Visible = True
			labelEncoding1.Text = Trim(sourceDoc.Encoding)
		Else
			labelEncoding1.Visible = False
			labelEncoding1.Text = ""
		End If
		
		labelFileInfo1.Text = ""
		
		If Not File.GetLastWriteTime(sourceFile) = File.GetCreationTime(sourceFile) then
			labelFileInfo1.Text = _fileName & "modified: " & File.GetLastWriteTime(sourceFile) ' & " (created: " & File.GetCreationTime(sourceFile) & ")"
		Else
			labelFileInfo1.Text = _fileName & "created: " & File.GetCreationTime(sourceFile)
		End If
		
		' get dropped file type from source (target should match)
		' Mid(Path.GetExtension(webbrowser1.Document.Url.ToString),2) ' webbrowser1.DocumentType
		
	End Sub
	
	Private Sub getTargetDocument(ByVal sender As Object, _
			ByVal e As WebBrowserDocumentCompletedEventArgs)
		
		' refreshTargetInfo()
		
		if debug then targetDoc.backcolor = color.LightGreen
	End Sub
	
	Sub refreshTargetInfo()
		targetFile = webbrowser2.Url.LocalPath
		
		targetDoc = webbrowser2.Document
		targetDom = webbrowser2.Document.DomDocument
		
		targetWindow = targetDom.parentWindow
		targetEncoding = targetDoc.Encoding
		
		
		dim _fileName As String = ""
		If len(targetDoc.Title) > 0 then
			textboxTitle2.Text = targetDoc.Title
			_fileName = "file: " & Path.GetFileName(targetFile) & ", "
		Else
			textboxTitle2.Text = Path.GetFileName(targetFile)
		End If
		
		If UCASE(Mid(Path.GetExtension(targetFile),2,3)) = "XML" Or UCASE(Mid(Path.GetExtension(targetFile),2,3)) = "HTM" then
			labelEncoding2.Visible = True
			labelEncoding2.Text = Trim(targetDoc.Encoding)
		Else
			labelEncoding2.Visible = False
			labelEncoding2.Text = ""
		End If
		
		labelFileInfo2.Text = ""
		
		If Not File.GetLastWriteTime(targetFile) = File.GetCreationTime(targetFile) then
			labelFileInfo2.Text = _fileName &  "modified: " & File.GetLastWriteTime(targetFile) ' & " (created: " & File.GetCreationTime(targetFile) & ")"
		Else
			labelFileInfo2.Text = _fileName &  "created: " & File.GetCreationTime(targetFile)
		End If
		
	End Sub
	
	Sub MainFormResize(sender As Object, e As System.EventArgs)
		' adjust viewer width
		If Not Me.WindowState = Windows.Forms.FormWindowState.Minimized then
			If panelMode Then
				webBrowser1.Width = panel1Contents.Width - scrollbarWidth
				webBrowser2.Width = panel2Contents.Width - scrollbarWidth
				If webBrowser1.Document.Body.ScrollRectangle.Height < panel1Contents.Height then webBrowser1.Height = panel1Contents.Height
				If webBrowser2.Document.Body.ScrollRectangle.Height < panel2Contents.Height then webBrowser2.Height = panel2Contents.Height
			End If
			
			If splitContainer1.IsSplitterFixed = True AND splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical  Then _
					splitContainer1.SplitterDistance = (splitContainer1.Width/2)-(splitContainer1.SplitterWidth/2)
				If splitContainer1.IsSplitterFixed = True AND splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal  Then _
						splitContainer1.SplitterDistance = (splitContainer1.Height/2)-(splitContainer1.SplitterWidth/2)
				End If
				
				if labelSize1.Visible Then labelSize1.Text = (WebBrowser1.Width - scrollBarCorrection).ToString() & " pixels"
				if labelSize2.Visible Then labelSize2.Text = (WebBrowser2.Width - scrollBarCorrection).ToString() & " pixels"
				
				If Me.Width < 720 And searchPanel.Visible Then
					searchPanel.Width = 147
				Else
					searchPanel.Width = 177
				End If
				
			End Sub
			
			Dim xOffset, yOffset As Integer
			
			Sub Panel1Scroll(sender As Object, e As System.Windows.Forms.ScrollEventArgs)
				If checkBox1.Checked then panel2Contents.AutoScrollPosition = New Point( _
						-panel1Contents.AutoScrollPosition.X+xOffset, _
						-panel1Contents.AutoScrollPosition.Y+yOffset)
			End Sub
				
			Sub Panel2Scroll(sender As Object, e As System.Windows.Forms.ScrollEventArgs)
				If checkBox1.Checked then panel1Contents.AutoScrollPosition = New Point( _
						-panel2Contents.AutoScrollPosition.X-xOffset, _
						-panel2Contents.AutoScrollPosition.Y-yOffset)
			End Sub
					
			Sub CheckBox1CheckedChanged(sender As Object, e As System.EventArgs)
				if checkbox1.Checked then
					xOffset = panel1Contents.AutoScrollPosition.X - panel2Contents.AutoScrollPosition.X
					yOffset = panel1Contents.AutoScrollPosition.Y - panel2Contents.AutoScrollPosition.Y
				else
					xOffset = 0
					yOffset = 0
				End If
			End Sub
					
					Sub scrollPageUp()
						If panelMode Then
							leftUp(panel1Contents.Height)
							rightUp(panel1Contents.Height)
							Else If imageMode Then
							PictureBox1.Top = 0
							If panel1Contents.Width < pictureBox1.Width then
								PictureBox1.Left =  panel1Contents.Width - pictureBox1.Width
							Else
								PictureBox1.Left = 0
							End If
							PictureBox2.top = 0
							If panel2Contents.Width < pictureBox2.Width then
								PictureBox2.Left =  panel2Contents.Width - pictureBox2.Width
							Else
								PictureBox1.Left = 0
							End If
						Else
							Randomize()
							If CInt(Int((10 * Rnd()) + 1)) > 5 then
								leftScrollPageUp()
								rightScrollPageUp()
							else
								rightScrollPageUp()
								leftScrollPageUp()
							end if
						End If
					End Sub
					
					Sub scrollPageDown()
						If panelMode Then
							leftDown(panel1Contents.Height)
							rightDown(panel1Contents.Height)
							Else If imageMode Then
							If panel1Contents.Height < pictureBox1.Height then
								PictureBox1.Top =  panel1Contents.Height - pictureBox1.Height
							Else
								PictureBox1.Top = 0
							End If
							If panel1Contents.Width < pictureBox1.Width then
								PictureBox1.Left =  panel1Contents.Width - pictureBox1.Width
							Else
								PictureBox1.Left = 0
							End If
							If panel2Contents.Height < pictureBox2.Height then
								PictureBox2.Top =  panel2Contents.Height - pictureBox2.Height
							Else
								PictureBox2.Top = 0
							End If
							If panel2Contents.Width < pictureBox2.Width then
								PictureBox2.Left =  panel2Contents.Width - pictureBox2.Width
							Else
								PictureBox1.Left = 0
							End If
						Else
							Randomize()
							If CInt(Int((10 * Rnd()) + 1)) > 5 then
								leftScrollPageDown()
								rightScrollPageDown()
							else
								rightScrollPageDown()
								leftScrollPageDown()
							end if
						End If
					End Sub
					
					Sub scrollUp()
						
						If ImageMode Then
							movePicture(0,-1,0) ' one step up
							Exit Sub
						End If
						
						If panelMode Then
							leftUp(line)
							rightUp(line)
						Else
							Randomize()
							If CInt(Int((10 * Rnd()) + 1)) > 5 then
								leftScrollUp()
								rightScrollUp()
							else
								rightScrollUp()
								leftScrollUp()
							end if
						End If
					End Sub
					
					Sub scrollDown()
						
						If ImageMode Then
							movePicture(0,+1,0) ' one step down
							Exit Sub
						End If
						
						If panelMode Then
							leftDown(line)
							rightDown(line)
						Else
							Randomize()
							If CInt(Int((10 * Rnd()) + 1)) > 5 then
								leftScrollDown()
								rightScrollDown()
							else
								rightScrollDown()
								leftScrollDown()
							end if
						End If
					End Sub
					
					
					Sub scrollToLeft()
						
						If ImageMode Then
							movePicture(-1,0,0) ' one step left
							Exit Sub
						End If
						
						If NOT useSendKeys then
							sourceWindow.scrollBy(-line,0)
							targetWindow.scrollBy(-line,0)
						End If
					End Sub
					
					Sub scrollToRight()
						
						If ImageMode Then
							movePicture(+1,0,0) ' one step right
							Exit Sub
						End If
						
						If NOT useSendKeys then
							sourceWindow.scrollBy(line,0)
							targetWindow.scrollBy(line,0)
						End If
					End Sub
					
					
					Sub scrollToTop()
						If panelMode Then
							panel2Contents.AutoScrollPosition = New Point(0,0)
							panel1Contents.AutoScrollPosition = New Point(0,0)
							Else If imageMode Then
							PictureBox1.Top = 0
							PictureBox1.Left = 0
							PictureBox2.Top = 0
							PictureBox2.Left = 0
						Else
							rightGotoStart()
							leftGotoStart()
						End If
						
					End Sub
					
					Sub scrollToBottom()
						If panelMode Then
							Dim y2 As Object = panel2Contents.AutoScrollPosition.Y
							Dim y1 As Object = panel1Contents.AutoScrollPosition.Y
							panel2Contents.AutoScrollPosition = New Point(0, y2.MinValue)
							panel1Contents.AutoScrollPosition = New Point(0, y1.MinValue)
							y2 = Nothing
							y1 = Nothing
							Else If imageMode Then
							If panel1Contents.Height < pictureBox1.Height then
								PictureBox1.Top =  panel1Contents.Height - pictureBox1.Height
							Else
								PictureBox1.Top = 0
							End If
							PictureBox1.Left = 0
							If panel2Contents.Height < pictureBox2.Height then
								PictureBox2.Top = panel2Contents.Height - pictureBox2.Height
							Else
								PictureBox2.Top = 0
							End If
							PictureBox2.Left = 0
						Else
							rightGotoEnd()
							leftGotoEnd()
						End If
					End Sub
					
					Sub leftUp(stepSize As Integer)
						if panelMode then panel1Contents.AutoScrollPosition = New Point( _
								-panel1Contents.AutoScrollPosition.X, _
								-panel1Contents.AutoScrollPosition.Y-stepSize)
						End Sub
						
						Sub leftDown(stepSize As Integer)
							if panelMode then panel1Contents.AutoScrollPosition = New Point( _
									-panel1Contents.AutoScrollPosition.X, _
									-panel1Contents.AutoScrollPosition.Y+stepSize)
							End Sub
							
							Sub rightUp(stepSize As Integer)
								if panelMode then panel2Contents.AutoScrollPosition = New Point( _
										-panel2Contents.AutoScrollPosition.X, _
										-panel2Contents.AutoScrollPosition.Y-stepSize)
								End Sub
								
								Sub rightDown(stepSize As Integer)
									if panelMode then panel2Contents.AutoScrollPosition = New Point( _
											-panel2Contents.AutoScrollPosition.X, _
											-panel2Contents.AutoScrollPosition.Y+stepSize)
									End Sub
									
									const KEY_SCROLL_LINEDOWN As Integer = 40 ' down
									const KEY_SCROLL_LINEUP As Integer = 38 ' up
									const KEY_SCROLL_PAGEDOWN As Integer = 34 ' Page Down
									const KEY_SCROLL_PAGEUP As Integer = 33 ' Page Up
									Const KEY_SCROLL_RIGHT As Integer = 39 ' Right
									const KEY_SCROLL_LEFT As Integer = 37 ' Left
									
									
									' catch keyboard input on entire form
									Protected Overrides Function ProcessDialogKey(ByVal keyData As System.Windows.Forms.Keys) As Boolean
										
										' if debug then Me.Text = KeyData
										If Not annotateImageMode Then
										Select Case keyData
										Case KEY_SCROLL_LINEDOWN ' line down
											scrollDown()
										Case KEY_SCROLL_LINEUP ' line up
											scrollUp()
										Case KEY_SCROLL_PAGEDOWN ' Page Down
											scrollPageDown()
										Case KEY_SCROLL_PAGEUP ' Page Up
											scrollPageUp()
										Case 131112 ' Ctrl Down
											If panelMode Then
												leftDown(10)
											Else
												leftScrollDown()
											End If
										Case 131110 ' Ctrl Up
											If PanelMode
												leftUp(10)
											Else
												leftScrollUp()
											End If
										Case 262184 ' Alt Down
											If panelMode
											rightDown(10)
											Else If imageMode Then
											movePicture(0,+1,2)
											Else
												rightScrollDown()
											End If
										Case 262182 ' Alt Up
											If panelMode
											rightUp(10)
											Else If imageMode Then
											movePicture(0,-1,2)
											Else
											rightScrollUp()
											End If
											
											
		Case 131106 ' Ctrl Page Down
			If panelMode Then
				leftDown(100)
			Else
				leftScrollPageDown()
			End If
		Case 131105 ' Ctrl Page Up
			If PanelMode
				leftUp(100)
			Else
				leftScrollPageUp()
			End If
		Case 262178 ' Alt Page Down
			If panelMode
			rightDown(100)
			Else
				rightScrollPageDown()
			End If
		Case 262177 ' Alt Page Up
			If panelMode
			rightUp(100)
			Else
			rightScrollPageUp()
			End If
			
								Case 36 ' Home
									scrollToTop()
									
								Case 35 ' End
									scrollToBottom()
									
								Case 131109 ' Ctrl Left
									previousFile()
									
								Case 131111 ' Ctrl Right
									nextFile()
	
									Case 131108 ' Ctrl Home
										leftGotoStart()
									Case 131107 ' Ctrl End
										leftGotoEnd()
									Case 262180 ' Alt Home
										rightGotoStart()
									Case 262179 ' Alt End
										rightGotoEnd()
									
								Case KEY_SCROLL_LEFT ' Left
									scrollToLeft()
									
								Case KEY_SCROLL_RIGHT ' Right
									scrollToRight()
									
									
									' for image file navigation
								Case 65574 ' Shift Up
									if imageMode then movePicture(0,-1,0)
									
								Case 65576 ' Shift Down
									If imageMode Then movePicture(0,+1,0)
									
								Case 65573 ' Shift Leftp
									If imageMode Then movePicture(-1,0,0)
									
								Case 65575 ' Shift Right
									If imageMode Then movePicture(+1,0,0)
									
								Case 393256 ' Ctrl Alt Down
									If panelMode Then
										leftDown(10)
										Else If imageMode Then
										movePicture(0,+1,1)
									Else
										leftScrollDown()
									End If
								Case 393254 ' Ctrl Alt Up
									If PanelMode
									leftUp(10)
									Else If imageMode Then
									movePicture(0,-1,1)
								Else
									leftScrollUp()
								End If
							Case 393253 ' Ctrl Alt Left
								If imageMode Then
									movePicture(-1,0,1)
								End If
							Case 393255 ' Ctrl Alt Right
								If imageMode Then
									movePicture(+1,0,1)
								End If
							Case 262181 ' Alt Left
								If imageMode Then
									movePicture(-1,0,2)
								End If
							Case 262183 ' Alt Right
								If imageMode Then
									movePicture(+1,0,2)
								End If
							Case 12 ' 5
								If imageMode Then
									If Not fitImages Then
										fitImagesToPictureBox()
										fitImages = True
									Else
										restoreImagesToNormalSize()
										fitImages = False
									End If
									FitImagesToolStripMenuItem1.Checked = fitImages
								End If
							Case 111 ' /
								If imageMode Then
									SplitViewToolStripMenuItemClick(Nothing, Nothing)
								End If
							Case 106 ' *
								If imageMode Then
									restoreImagesToNormalSize()
								End If
							Case 109 ' -
								If imageMode Then
									zoom = zoom - 10
									If zoom < 10 then zoom = 10
									zoomImages(zoom)
								End If
							Case 107 ' +
								If imageMode Then
									zoom = zoom + 10
									if zoom > 2000 then zoom = 2000
									zoomImages(zoom)
								End If
								
								
							Case 131153 ' Ctrl-Q
								toggleAnnotateMode()
							Case  131142 ' Ctrl-F
								ShowSearchToolStripMenuItemClick(Nothing, Nothing)
								textbox1.Focus()
								
								
							' additional shortcuts for responsive design	
							Case 112, 262193 ' F1, Alt-1	
								setBrowserSize(CInt(toolStripMenuItemSize1.Text))
							Case 113, 262194 ' F2, Alt-2	
								setBrowserSize(CInt(toolStripMenuItemSize2.Text))
							Case 114, 262195 ' F3, Alt-3	
								setBrowserSize(CInt(toolStripMenuItemSize3.Text))
							Case 115, 262196 ' F4, Alt-4
								setBrowserSize(CInt(toolStripMenuItemSize4.Text))
							End Select
						Else ' annotate mode
							
							Select Case keyData
	
							Case 27, 131153 ' Esc ' Ctrl-Q
								toggleAnnotateMode()
							Case 131155 ' Ctrl-S
								If annotateImageMode Then
									Dim SaveFileDialog1 As New SaveFileDialog()

							        SaveFileDialog1.DefaultExt = "png"
							        SaveFileDialog1.AddExtension = True
							        If Not len(Trim(projectFile)) = 0 Then SaveFileDialog1.InitialDirectory = Path.GetDirectoryName(projectFile)
									SaveFileDialog1.FileName = path.GetFileNameWithoutExtension(sourceFile) & ".png"
							        SaveFileDialog1.Filter = "Image Files (*.png;*.jpg;*.gif)|*.png;*.jpg;*.gif|All Files (*.*)|*.*"
							        SaveFileDialog1.Title = "Save Image File"

									If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
										If Not PictureBoxAnnotate.file Is Nothing Then
											PictureBoxAnnotate.Save(SaveFileDialog1.FileName)
										End If
									End If
									
								End If
							Case 131162 ' Undo Ctrl-Z
								If annotateImageMode Then PictureBoxAnnotate.undo()
							Case 131139 ' Copy Ctrl-C
								If annotateImageMode Then PictureBoxAnnotate.Copy()
							Case 131158 ' Paste Ctrl-V
								' If annotateImageMode Then PictureBoxAnnotate.Paste()
							End Select
						End If
							
						
					End Function
	
					Dim WithEvents PictureBoxAnnotate As New AnnotateBox
					
					Sub PictureBoxAnnotateImageSaved(fileName As String) Handles PictureBoxAnnotate.ImageSaved
						If Len(Trim(textBoxComments.Text)) > 0 Then textBoxComments.Text = textBoxComments.Text & VbCrlF
						textBoxComments.Text = textBoxComments.Text & "See " & System.IO.Path.GetFileName(fileName)
						toggleAnnotateMode()
					End Sub
	
					Sub PictureBoxAnnotateQuit() Handles PictureBoxAnnotate.Quit
						toggleAnnotateMode()
					End Sub
	
					Sub setMode()
						
						If panelMode Then
							
							' WEB
							webbrowser1.Visible = True
							picturebox1.Visible = False
							webbrowser2.Visible = True
							picturebox2.Visible = False
							
							pictureBackGroundColor(systemColors.control)
							
							' initialize webbrowser controls
							webBrowser1.ScrollBarsEnabled = False
							panel1Contents.AutoScroll = True
							webBrowser1.Dock = DockStyle.None
							
							webBrowser1.Height = webBrowser1.Document.Body.ScrollRectangle.Height
							If webBrowser1.Document.Body.ScrollRectangle.Height < panel1Contents.Height then webBrowser1.Height = panel1Contents.Height
							
							' adjust initial viewer width
							webBrowser1.Width = panel1Contents.Width - scrollbarWidth
							
							webBrowser2.ScrollBarsEnabled = False
							panel2Contents.AutoScroll = True
							webBrowser2.Dock = DockStyle.None
							
							webBrowser2.Height = webBrowser2.Document.Body.ScrollRectangle.Height
							If webBrowser2.Document.Body.ScrollRectangle.Height < panel2Contents.Height then webBrowser2.Height = panel2Contents.Height
							
							
							' adjust initial viewer width
							webBrowser2.Width = panel2Contents.Width - scrollbarWidth
							
							
						Else If browserMode Then
							
							' WEB
							webbrowser1.Visible = True
							picturebox1.Visible = False
							webbrowser2.Visible = True
							picturebox2.Visible = False
							
							' initialize webbrowser controls
							webBrowser1.ScrollBarsEnabled = True
							panel1Contents.AutoScroll = False
							webBrowser1.Dock = DockStyle.Fill
							
							webBrowser2.ScrollBarsEnabled = True
							panel2Contents.AutoScroll = False
							webBrowser2.Dock = DockStyle.Fill
							
							pictureBackGroundColor(systemColors.control)
							
						Else If imageMode Then
							
							' IMAGE
							picturebox1.Visible = True
							webbrowser1.Visible = False
							panel1Contents.AutoScroll = True
							picturebox2.Visible = True
							webbrowser2.Visible = False
							panel2Contents.AutoScroll = True
							
							ImageToolStripMenuItem.Checked = True
							
							' initialize webbrowser controls
							webBrowser1.ScrollBarsEnabled = True
							webBrowser1.AutoSize = True
							panel1Contents.AutoScroll = False
							webBrowser1.Dock = DockStyle.Fill
							
							webBrowser2.ScrollBarsEnabled = True
							webBrowser2.AutoSize = True
							panel2Contents.AutoScroll = False
							webBrowser2.Dock = DockStyle.Fill
							
							' set menu items
							ImageToolStripMenuItem.Checked = True
							DefaultToolStripMenuItem.Checked = False
							AlternativeToolStripMenuItem.Checked = False
							Alternative2ToolStripMenuItem.Checked = False
							
							pictureBackGroundColor(imageBackGroundColor)
							
						End If
					End Sub
					
					' default image background color
					Dim public imageBackGroundColor As Color = color.FromArgb(202,212,227) ' systemColors.Control
							
					Function leftScrollUp(Optional stepSize As Integer = 1) As Boolean
						If _autoScroll then
							scrollPos1 = scrollPos1 - (3*10)
							if scrollPos1 < 0 Then scrollPos1 = 0
							if Not t.Enabled then webbrowser1.Document.Window.ScrollTo(0,scrollPos1)
						Else

						If useSendKeys then
							webbrowser1.Focus()
							sendkeys.Flush()
							For i As Integer = 1 To stepSize
								sendkeys.SendWait("{UP}")
								sendkeys.Flush()
							Next
							panel3.Focus()
							Return True
						Else
							For i As Integer = 1 To stepSize
								sourceWindow.scrollBy(0,-line)
							Next
						End If
					
						End If
					End Function
					
					Function leftScrollDown(Optional stepSize As Integer = 1) As Boolean
						If _autoScroll then
							scrollPos1 = scrollPos1 + 10
							if scrollPos1 > WebBrowser1.Document.Body.ScrollRectangle.Height Then scrollPos1 = WebBrowser1.Document.Body.ScrollRectangle.Height + 10
							if Not t.Enabled then webbrowser1.Document.Window.ScrollTo(0,scrollPos1)
						Else


						If useSendKeys then
							webbrowser1.Focus()
							sendkeys.Flush()
							For i As Integer = 1 To stepSize
								sendkeys.SendWait("{DOWN}")
								sendkeys.Flush()
							Next
							panel3.Focus()
							Return True
						Else
							For i As Integer = 1 To stepSize
								sourceWindow.scrollBy(0,line)
							Next
						End If
						
						End If
					End Function
					
					Function rightScrollUp(Optional stepSize As Integer = 1) As Boolean
						If _autoScroll then
							scrollPos2 = scrollPos2 - (3*10)
							if scrollPos2 < 0 Then scrollPos2 = 0
							if Not t.Enabled then webbrowser2.Document.Window.ScrollTo(0,scrollPos2)
						Else

						If useSendKeys then
							webbrowser2.Focus()
							sendkeys.Flush()
							For i As Integer = 1 To stepSize
								sendkeys.SendWait("{UP}")
								sendkeys.Flush()
							Next
							panel3.Focus()
							Return True
						Else
							For i As Integer = 1 To stepSize
								targetWindow.scrollBy(0,-line)
							Next
						End If
						
						End If
					End Function
					
					Function rightScrollDown(Optional stepSize As Integer = 1) As Boolean
						If _autoScroll then
							scrollPos2 = scrollPos2 + 10
							if scrollPos2 > WebBrowser2.Document.Body.ScrollRectangle.Height Then scrollPos2 = WebBrowser2.Document.Body.ScrollRectangle.Height + 10
							if Not t.Enabled then webbrowser2.Document.Window.ScrollTo(0,scrollPos2)
						Else

						If useSendKeys then
							webbrowser2.Focus()
							sendkeys.Flush()
							For i As Integer = 1 To stepSize
								sendkeys.SendWait("{DOWN}")
								sendkeys.Flush()
							Next
							panel3.Focus()
							Return True
						Else
							For i As Integer = 1 To stepSize
								targetWindow.scrollBy(0,line)
							Next
						End If
						
						End if
					End Function
					
					Function leftScrollPageUp(Optional stepSize As Integer = 1) As Boolean
						If _autoScroll then
							scrollPos1 = scrollPos1 - (250)
							if scrollPos1 < 0 Then scrollPos1 = 0
							if Not t.Enabled then webbrowser1.Document.Window.ScrollTo(0,scrollPos1)
						Else

						If useSendKeys then
							webbrowser1.Focus()
							sendkeys.Flush()
							For i As Integer = 1 To stepSize
								sendkeys.SendWait("{PGUP}")
								sendkeys.Flush()
							Next
							panel3.Focus()
							Return True
						Else
							For i As Integer = 1 To stepSize
								sourceWindow.scrollBy(0,-(panel2Contents.Height-10))
							Next
						End If
						
						End If
					End Function
					
					Function leftScrollPageDown(Optional stepSize As Integer = 1) As Boolean
						If _autoScroll then
							scrollPos1 = scrollPos1 + (250)
							if scrollPos1 > WebBrowser1.Document.Body.ScrollRectangle.Height Then scrollPos1 = WebBrowser1.Document.Body.ScrollRectangle.Height + 10
							if Not t.Enabled then webbrowser1.Document.Window.ScrollTo(0,scrollPos1)
						Else

						If useSendKeys then
							webbrowser1.Focus()
							sendkeys.Flush()
							For i As Integer = 1 To stepSize
								sendkeys.SendWait("{PGDN}")
								sendkeys.Flush()
							Next
							panel3.Focus()
							Return True
						Else
							For i As Integer = 1 To stepSize
								sourceWindow.scrollBy(0,(panel2Contents.Height-10))
							Next
						End If
						
						End If
					End Function
					
					Function rightScrollPageUp(Optional stepSize As Integer = 1) As Boolean
						If _autoScroll then
							scrollPos2 = scrollPos2 - (250)
							if scrollPos2 < 0 Then scrollPos2 = 0
							if Not t.Enabled then webbrowser2.Document.Window.ScrollTo(0,scrollPos2)
						Else

						If useSendKeys then
							webbrowser2.Focus()
							sendkeys.Flush()
							For i As Integer = 1 To stepSize
								sendkeys.SendWait("{PGUP}")
								sendkeys.Flush()
							Next
							panel3.Focus()
							Return True
						Else
							For i As Integer = 1 To stepSize
								targetWindow.scrollBy(0,-(panel2Contents.Height-10))
							Next
						End If
						
						End If
					End Function
					
					Function rightScrollPageDown(Optional stepSize As Integer = 1) As Boolean
						If _autoScroll then
							scrollPos2 = scrollPos2 + (250)
							if scrollPos2 > WebBrowser2.Document.Body.ScrollRectangle.Height Then scrollPos2 = WebBrowser2.Document.Body.ScrollRectangle.Height + 10
							if Not t.Enabled then webbrowser2.Document.Window.ScrollTo(0,scrollPos2)
						Else

						If useSendKeys then
							webbrowser2.Focus()
							sendkeys.Flush()
							For i As Integer = 1 To stepSize
								sendkeys.SendWait("{PGDN}")
								sendkeys.Flush()
							Next
							panel3.Focus()
							Return True
						Else
							For i As Integer = 1 To stepSize
								targetWindow.scrollBy(0,(panel2Contents.Height-10))
							Next
						End If
						
						End If
					End Function
					
					
					Function rightGotoStart() As Boolean
						If _autoScroll then
							scrollPos2 = 0
							if Not t.Enabled then webbrowser2.Document.Window.ScrollTo(0,scrollPos2)
						Else
						
						If useSendKeys then
							webbrowser1.Focus()
							sendkeys.Flush()
							sendkeys.SendWait("^{HOME}")
							sendkeys.Flush()
							panel3.Focus()
							Return True
						Else
							targetWindow.scrollTo(0,0)
						End If
						
						End If
					End Function
					
					Function rightGotoEnd() As Boolean
						If _autoScroll then
							scrollPos2 = WebBrowser2.Document.Body.ScrollRectangle.Height + 10 ' 1000000000
							if Not t.Enabled then webbrowser2.Document.Window.ScrollTo(0,scrollPos2)
						Else
					
						If useSendKeys then
							webbrowser2.Focus()
							sendkeys.Flush()
							sendkeys.SendWait("^{END}")
							sendkeys.Flush()
							panel3.Focus()
							Return True
						Else
							targetWindow.scrollTo(1000000000,1000000000)
						End If
						
						End If
					End Function
					
					Function leftGotoStart() As Boolean
						If _autoScroll then
							scrollPos1 = 0
							if Not t.Enabled then webbrowser1.Document.Window.ScrollTo(0,scrollPos1) ' 1000000000
						Else
					
						If useSendKeys then
							webbrowser1.Focus()
							sendkeys.Flush()
							sendkeys.SendWait("^{HOME}")
							sendkeys.Flush()
							panel3.Focus()
							Return True
						Else
							sourceWindow.scrollTo(0,0)
						End If
						
						End If
					End Function
					
					Function leftGotoEnd() As Boolean
						If _autoScroll then
							scrollPos1 = WebBrowser1.Document.Body.ScrollRectangle.Height + 10
							if Not t.Enabled then webbrowser1.Document.Window.ScrollTo(0,scrollPos1)
						Else
					
						If useSendKeys then
							webbrowser1.Focus()
							sendkeys.Flush()
							sendkeys.SendWait("^{END}")
							sendkeys.Flush()
							panel3.Focus()
							Return True
						Else
							sourceWindow.scrollTo(1000000000,1000000000)
						End If
						
						End If
					End Function
					
					
					
					
					
					
					Sub ButtonPageDownClick(sender As Object, e As System.EventArgs)
						scrollPageDown()
					End Sub
					
					Sub ButtonPageUpClick(sender As Object, e As System.EventArgs)
						scrollPageUp()
					End Sub
					
					Sub ButtonLineDownClick(sender As Object, e As System.EventArgs)
						scrollDown()
					End Sub
					
					Sub ButtonLineUpClick(sender As Object, e As System.EventArgs)
						scrollUp()
					End Sub


 
					Dim fileList(1,4) As String ' HACK:  Dim fileList(1,4) As String
					Dim activeFile As Integer
					dim projectActive As Boolean = False
					Dim sourceFolder, targetFolder As Object
					
					
					Sub writeErrorToBrowser()
						
'						textboxTitle2.Text = Path.GetFileName(fileList(activeFile,0))
'						labelFileInfo2.Text = fileList(activeFile, 2)
'						labelImgInfo2.Text = fileList(activeFile, 2)
'						labelEncoding2.Text = ""
'						webbrowser2.DocumentText = _
'							"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">" & _
'							"<html xmlns=""http://www.w3.org/1999/xhtml"">" & _
'							"<title>" & fileList(activeFile, 2) & "</title></head>" & _
'							"<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />" & _
'							"<style type=""text/css"">" & _
'							"  body { font: 8pt verdana; background-color: #EBE9ED; }" & _
'							"  h1 { font: 12pt verdana }" & _
'							"  .external { cursor: hand; color: red; text-decoration: none; }" & _
'							"</style>" & _
'							"<body><h1>" & fileList(activeFile, 2) & "</h1>" & _
'							fileList(activeFile, 3) & "<br />" & _
'							"<p><strong>" & Path.GetFileName(fileList(activeFile,0)) & "</strong></p>" & _
'							"<p>Check folder: <span class=""external"" onclick=""window.external.Open('" & fixPath(targetFolder) & "')"" >" & targetFolder & "</span></p>" & _
'						"<hr /></body></html>"
						
						' 		targetWindow.document.clear()
						'		targetWindow.document.Write _
						'		("...")
	
					End Sub
					
					Sub ButtonNextClick(sender As Object, e As System.EventArgs)
						nextFile()
					End Sub
					
					Sub ButtonPreviousClick(sender As Object, e As System.EventArgs)
						previousFile()
					End Sub
					
					Dim sourcePinned As Boolean = False
					Dim targetPinned As Boolean = False
					
					Sub nextFile()
						' 2011/11/28 show only filtered items
						If filterActive Then
							While Not checkFilter(activeFile + 1) And activeFile < fileList.GetUpperBound(0) - 1
								activeFile = activeFile + 1
							End While
						End If
						
						If projectActive And activeFile < fileList.GetUpperBound(0) - 1 then
							updateTreeViewInteractive = True
							if autoUpdateCheckStatus then updateStatus()
							showFile(activeFile + 1)
							updateTreeViewInteractive = False
						End If
					End Sub
					
					
					Sub previousFile()
						' 2011/11/28 show only filtered items
						If filterActive Then
							While Not checkFilter(activeFile - 1) And activeFile > 0
								activeFile = activeFile - 1
							End While
						End If
											
						If projectActive And activeFile > 0 Then
							updateTreeViewInteractive = True
							if autoUpdateCheckStatus then updateStatus()
							showFile(activeFile - 1)
							updateTreeViewInteractive = False
						End If
					End Sub
					
					Sub openFile(fileID As String)
						showFile(Cint(fileID))
					End Sub
					
					Sub showFile(fileName As String)
						showFile(getFileID(fileName))
					End Sub
					
					Dim filterMismatchesOnly As Boolean = False
					Dim filterNoOrphans As Boolean = False
					Dim filterMatchesOnly As Boolean = False
					
					Sub showFile(fileID As Integer)
					
						System.Diagnostics.Debug.WriteLine(fileID)

						' do not update view if already viewing
						If fileID = activeFile And Not refreshFileList then Exit Sub
				
						' check filter
						If filterNoOrphans And Not nodeClicked Then ' update fileID
							Do While fileList(fileID,2) = "Orphan" Or _
									Not File.Exists(fileList(fileID,1))
								
								If fileID < activeFile Then fileID = fileID - 1
								If fileID > activeFile Then fileID = fileID + 1
								If fileID < 0 or fileID > fileList.GetUpperBound(0) - 1 then Exit Sub
							Loop
						End If
		
						' set new active file ID
						activeFile = fileID
						
						If not sourcePinned Then sourceFile = fileList(activeFile,0)
						If not targetPinned Then targetFile = fileList(activeFile,1)
						
					' TODO: separate source target
						
					' get file type from source
					If Not len(Trim(sourceFile)) = 0 Then
						fileType = Mid(Path.GetExtension(sourceFile),2) ' copy of target so no need to get extension from target
					Else
						fileType = Mid(Path.GetExtension(targetFile),2)
					End if
					determineMode(fileType)
					' set mode
					setMode()
			
						
					Select lcase(fileType)
					Case "bmp", "png", "jpg", "gif"
						If Not Lcase(fileList(activeFile,2)) = "error" Then
							if projectActive then pictureBox5.Visible = True
							panelTargetInfo.BackColor = panelInfoColor ' Me.BackColor ' Color.LightYellow
							textBoxTitle2.BackColor = panelInfoColor ' Me.BackColor ' Color.LightYellow
						Else If file.Exists(SourceFile) Then
							pictureBox5.Visible = False
							panelTargetInfo.BackColor = Color.LightPink
							textBoxTitle2.BackColor = Color.LightPink
							labelFileInfo2.Text = ""
							labelImgInfo2.Text = ""
							labelEncoding2.Text = ""
						End If
					Case Else
						webBrowser1.Url = New Uri(sourceFile)
						if projectActive then pictureBox4.Visible = True
						panelSourceInfo.BackColor = panelInfoColor ' Me.BackColor ' Color.LightYellow
						textBoxTitle1.BackColor = panelInfoColor ' Me.BackColor ' Color.LightYellow
						' If Not sourcePinned Then
						If Not Lcase(fileList(activeFile,2)) = "error" And Not Lcase(fileList(activeFile,2)) = "problem" And Not Lcase(fileList(activeFile,2)) = "missing" Then
							If file.Exists(targetFile) Then
								webBrowser2.Url = New Uri(targetFile)
								targetToolStripMenuItem.Enabled = True
								if projectActive then pictureBox5.Visible = True
								panelTargetInfo.BackColor = panelInfoColor ' Me.BackColor ' Color.LightYellow
								textBoxTitle2.BackColor = panelInfoColor ' Me.BackColor ' Color.LightYellow
							Else
								' writeErrorToBrowser() ' NEED SOME MORE DETAILS
								webBrowser2.Url = New Uri(targetFile) ' Just show 'broken' document as-is
								targetToolStripMenuItem.Enabled = True ' False
								pictureBox5.Visible = False
								panelTargetInfo.BackColor = Color.LightPink
								textBoxTitle2.BackColor = Color.LightPink ' LightCoral
							End If
						Else
							' writeErrorToBrowser
							webBrowser2.Url = New Uri(targetFile)
							pictureBox5.Visible = False
							panelTargetInfo.BackColor = Color.LightPink
							textBoxTitle2.BackColor = Color.LightPink ' LightCoral
						End If
						' End If
				
				End Select
				
				If Lcase(fileList(activeFile,2)) = "orphan" And Not file.Exists(sourceFile) Then
					pictureBox4.Visible = False
					panelSourceInfo.BackColor = Color.LightBlue
					textBoxTitle1.BackColor = Color.LightBlue

					textboxTitle1.Text = Path.GetFileName(fileList(activeFile,0))
					labelFileInfo1.Text = "Extra file in target folder"
					labelImgInfo1.Text = "Extra file in target folder"
					labelEncoding1.Text = ""
					
					webbrowser1.DocumentText = _
						"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">" & _
						"<html xmlns=""http://www.w3.org/1999/xhtml"">" & _
						"<title>" & fileList(activeFile, 2) & "</title></head>" & _
						"<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />" & _
						"<style type=""text/css"">" & _
						"  body { font: 8pt verdana; background-color: #EBE9ED; }" & _
						"  h1 { font: 12pt verdana }" & _
						"  .external { cursor: hand; color: red; text-decoration: none; }" & _
						"</style>" & _
						"<body><h1>" & "Extra file in target folder" & "</h1>" & _
						"<br />" & _
						"<p><strong>" & Path.GetFileName(fileList(activeFile,0)) & "</strong></p>" & _
						"<p>Check folder: <span class=""external"" onclick=""window.external.Open('" & fixPath(targetFolder) & "')"" >" & targetFolder & "</span></p>" & _
					"<hr /></body></html>"

				Else
					if projectActive then pictureBox4.Visible = True
					panelSourceInfo.BackColor = panelInfoColor ' Me.BackColor ' Color.LightYellow
					textBoxTitle1.BackColor = panelInfoColor ' Me.BackColor ' Color.LightYellow
				End If
				
				Me.Text = projectName & " - " & "Viewing " & Path.GetFileName(sourceFile) ' Path.GetFileNameWithoutExtension(sourceFile)
				If len(fileList(activeFile,2)) = 0 Then
					labelStatus.Text = "unchecked"
				Else
					labelStatus.Text = fileList(activeFile,2)
				End If
				
				
				' activate buttons
				If activeFile < fileList.GetUpperBound(0) - 1 Then
					buttonNext.Enabled = True
					buttonNext.Visible = True
					buttonNextOff.visible = False
				End If
				If activeFile = fileList.GetUpperBound(0) - 1 Then
					buttonNext.Enabled = False
					buttonNext.Visible = False
					buttonNextOff.visible = True
				End If
				If activeFile = 0 Then
					buttonPrevious.Enabled = False
					buttonPrevious.Visible = False
					buttonPreviousOff.visible = True
				End If
				If activeFile > 0 Then
					buttonPrevious.Enabled = True
					buttonPrevious.Visible = True
					buttonPreviousOff.visible = False
				End If
				
				labelCounter.Text = activeFile + 1 & "/" & fileList.GetUpperBound(0)
				activateCurrentNode(sourceFile)
				
				' set to default
				updateTreeViewInteractive = False
				
				If projectToolStripMenuItem.Visible then _
						textBoxComments.Text = fileList(activeFile,3)
					
				' reset click source
				nodeClicked = False
			End Sub
	
	Sub refreshTarget()
		FileSystemWatcher1.Filter = Path.GetFileName(targetFile)
		FileSystemWatcher1.Path = Path.GetDirectoryName(targetFile)
		FileSystemWatcher1.EnableRaisingEvents = True

		If fileType = "bitmap" Then
			Dim fs As System.IO.FileStream
			fs = New System.IO.FileStream(targetFile, IO.FileMode.Open, IO.FileAccess.Read)
			pictureBox2.Image = System.Drawing.Image.FromStream(fs)
			if fitImages then pictureBox2.Image = ResizeImage(System.Drawing.Image.FromStream(fs),panel1Contents.Width,panel1Contents.Height,True,True)
			labelImgInfo2.Text = "" & System.Drawing.Image.FromStream(fs).Width & " x " & System.Drawing.Image.FromStream(fs).Height & " x " & Image.GetPixelFormatSize(System.Drawing.Image.FromStream(fs).PixelFormat) & ""
			fs.Close()
		Else
			webBrowser2.Url = New Uri(targetFile)
		End If
	End Sub
	
				dim updateTreeViewInteractive As Boolean = False
				
				Dim projectName As String = "New Project"
				Dim projectFile As String = ""
				Dim saveStatus As Boolean = False
				
				function getFileID(fileName As String) As Integer
					getFileID = activeFile
					For i As Integer = 0 To filelist.Length
						If lcase(filelist(i,0)) = lcase(fileName) Then
							getFileID = i
							Exit For
							Else If lcase(filelist(i,1)) = lcase(fileName) Then
							getFileID = i
							Exit For
						End If
					Next
				End function
				
				
				' mouse wheel support
				Private Sub Wheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseWheel
					If annotateImageMode Then Exit Sub
					' "Spin :" & e.Delta
					If e.Delta < 0 Then

						If imageMode Then
							If IsKeyDown(Keys.ControlKey) Then
								zoom = zoom - 10
								If zoom < 10 then zoom = 10
								zoomImages(zoom)
							Else
								scrollDown()
							End If
						Else
							If Control.ModifierKeys = Keys.Shift Then ' IsKeyDown(Keys.ShiftKey) Then ' IsKeyDown(Keys.LShiftKey) Or IsKeyDown(Keys.RShiftKey)
								scrollPageDown
							ElseIf Control.ModifierKeys = Keys.Control Then ' IsKeyDown(Keys.ControlKey) Then
								LeftscrollDown()
							ElseIf Control.ModifierKeys = Keys.Alt Then ' IsKeyDown(Keys.Alt) Then
								RightscrollDown()
							Else
								scrollDown()
							End If
						End If
					Else If e.Delta > 0

						If imageMode Then
							If IsKeyDown(Keys.ControlKey) Then
								zoom = zoom + 10
								if zoom > 1000 then zoom = 1000
								zoomImages(zoom)
							Else
								scrollUp()
							End If
						Else
							If Control.ModifierKeys = Keys.Shift Then ' IsKeyDown(Keys.ShiftKey) Then
								scrollPageUp
							ElseIf Control.ModifierKeys = Keys.Control Then ' IsKeyDown(Keys.ControlKey) Then
								LeftscrollUp()
							ElseIf Control.ModifierKeys = Keys.Alt Then ' IsKeyDown(Keys.Alt) Then
								RightscrollUp()
							Else
								scrollUp()
							End If
						End If
					End if
					
				End Sub
				
				Private Declare Function GetKeyState Lib "user32" (ByVal nVirtKey As Long) As Integer
				
				Private Function IsKeyDown(ByVal key As Long) As Boolean
					Const KeyDownMask As Long = 32768
					Dim state As Long
					state = GetKeyState(key)
					IsKeyDown = ((state And KeyDownMask) > 0)
				End Function
				
				
				'Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Integer) As Integer
				'
				'Public Shared Function IsKeyDown(ByVal Key As Keys) As Boolean
				'    If Not GetAsyncKeyState(Convert.ToInt32(Key)) = 0 Then
				'        Return True
				'    Else
				'        Return False
				'    End If
				'End Function
				
				Sub zoomImages(zoomfactor As Integer)
					dim AA As Boolean = False
					if zoomFactor < 100 then AA = True
					pictureBox1.Image = ResizeImage(System.Drawing.Image.FromFile(sourceFile),zoomFactor,-1,True,AA)
					pictureBox2.Image = ResizeImage(System.Drawing.Image.FromFile(targetFile),zoomFactor,-1,True,AA)
				End Sub
				
				Sub DefaultToolStripMenuItemClick(sender As Object, e As System.EventArgs)
					panelMode = False
					browserMode = True
					checkBox1.Visible = False
					useSendKeys = False
					DefaultToolStripMenuItem.Checked = True
					DefaultToolStripMenuItem.Image = Imagelistsmallicons.Images.Item(13)
					AlternativeToolStripMenuItem.Checked = False
					AlternativeToolStripMenuItem.Image = Nothing
					Alternative2ToolStripMenuItem.Checked = False
					Alternative2ToolStripMenuItem.Image = Nothing
					ImageToolStripMenuItem.Checked = False
					ImageToolStripMenuItem.Image = Nothing
					setMode()
				End Sub
				
				Sub AlternativeToolStripMenuItemClick(sender As Object, e As System.EventArgs)
					panelMode = False
					browserMode = True
					checkBox1.Visible = False
					useSendKeys = True
					DefaultToolStripMenuItem.Checked = False
					DefaultToolStripMenuItem.Image = Nothing
					AlternativeToolStripMenuItem.Checked = True
					AlternativeToolStripMenuItem.Image = Imagelistsmallicons.Images.Item(13)
					Alternative2ToolStripMenuItem.Checked = False
					Alternative2ToolStripMenuItem.Image = Nothing
					ImageToolStripMenuItem.Checked = False
					ImageToolStripMenuItem.Image = Nothing
					setMode()
				End Sub
				
				Sub Alternative2ToolStripMenuItemClick(sender As Object, e As System.EventArgs)
					panelMode = True
					browserMode = False
					checkBox1.Visible = True
					DefaultToolStripMenuItem.Checked = False
					DefaultToolStripMenuItem.Image = Nothing
					AlternativeToolStripMenuItem.Checked = False
					AlternativeToolStripMenuItem.Image = Nothing
					Alternative2ToolStripMenuItem.Checked = True
					Alternative2ToolStripMenuItem.Image = Imagelistsmallicons.Images.Item(13)
					ImageToolStripMenuItem.Checked = False
					ImageToolStripMenuItem.Image = Nothing
					setMode()
				End Sub
				
				Sub ImageToolStripMenuItemClick(sender As Object, e As System.EventArgs)
					panelMode = False
					browserMode = False
					imageMode = True
					checkBox1.Visible = False
					useSendKeys = False
					DefaultToolStripMenuItem.Checked = False
					DefaultToolStripMenuItem.Image = Nothing
					AlternativeToolStripMenuItem.Checked = False
					AlternativeToolStripMenuItem.Image = Nothing
					Alternative2ToolStripMenuItem.Checked = False
					Alternative2ToolStripMenuItem.Image = Nothing
					ImageToolStripMenuItem.Checked = True
					ImageToolStripMenuItem.Image = Imagelistsmallicons.Images.Item(13)
					setMode()
				End Sub
				
				Sub CloseProjectToolStripMenuItemClick(sender As Object, e As System.EventArgs)
					activeFile = -1
					projectActive = False
					labelStatus.Visible = False
					panelComments.Visible = False
					' buttonNext.Enabled = False
					' buttonPrevious.Enabled = False
					buttonNext.Visible = False
					buttonPrevious.Visible = False
					buttonNextOff.visible = False
					buttonPreviousOff.visible = False
					CloseProjectToolStripMenuItem.Enabled = False
					labelCounter.Visible = False
					
					newProjectToolStripMenuItem.Text = "New project..."
					SaveProjectToolStripMenuItem.Enabled = False
					
					showFileListToolStripMenuItem.Visible = False
					showCommentsToolStripMenuItem.Visible = False
					openFoldersInCompareToolToolStripMenuItem.Visible = False
					
					projectToolStripMenuItem.Visible = False
					ProjectFilterToolStripMenuItem.Visible = False
					Me.Text = "Visual QA"
					
					pictureBox4.Visible = False
					pictureBox5.Visible = False
					
					' hide filelist
					SplitContainerContents.Panel1Collapsed = True
					ShowFileListToolStripMenuItem.Checked = False
					
					SaveProjectToolStripMenuItem1.Visible = False
					SaveProjectToolStripMenuItem.Text = "Save project..."
					projectName = "New Project"
					projectFile = ""
					saveStatus = False
					
					
					' Clear project filelist
					Redim fileList(0,0)
				End Sub
				
				Sub ExitToolStripMenuItemClick(sender As Object, e As System.EventArgs)
					Me.Close()
				End Sub
				
				Sub changeOrientationHorizontal(sender As Object, e As System.EventArgs)
					splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
					verticalToolStripMenuItem.Checked = False
					verticalToolStripMenuItem.Image = Nothing
					horizontalToolStripMenuItem.Checked = True
					horizontalToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
					
					If splitContainer1.IsSplitterFixed = True AND splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical  Then _
							splitContainer1.SplitterDistance = (splitContainer1.Width/2)-(splitContainer1.SplitterWidth/2)
						If splitContainer1.IsSplitterFixed = True AND splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal  Then _
								splitContainer1.SplitterDistance = (splitContainer1.Height/2)-(splitContainer1.SplitterWidth/2)
							
						End Sub
						
						Sub changeOrientationVertical(sender As Object, e As System.EventArgs)
							splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical
							verticalToolStripMenuItem.Checked = True
							verticalToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
							horizontalToolStripMenuItem.Checked = False
							horizontalToolStripMenuItem.Image = Nothing
							
							If splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical  Then _
									splitContainer1.SplitterDistance = (splitContainer1.Width/2)-(splitContainer1.SplitterWidth/2)
								If splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal  Then _
										splitContainer1.SplitterDistance = (splitContainer1.Height/2)-(splitContainer1.SplitterWidth/2)
									
								End Sub
								
								Sub SplitToolStripMenuItemClick(sender As Object, e As System.EventArgs)
									splitContainer1.Panel1Collapsed = False
									splitContainer1.Panel2Collapsed = False
									
									' reset splitter
									If splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical  Then _
											splitContainer1.SplitterDistance = (splitContainer1.Width/2)-(splitContainer1.SplitterWidth/2)
										If splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal  Then _
												splitContainer1.SplitterDistance = (splitContainer1.Height/2)-(splitContainer1.SplitterWidth/2)
											
											SplitToolStripMenuItem.Checked = True
											SplitToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
											SourceOnlyToolStripMenuItem.Checked = False
											SourceOnlyToolStripMenuItem.Image = Nothing
											TargetOnlyToolStripMenuItem.Checked = False
											TargetOnlyToolStripMenuItem.Image = Nothing
										End Sub
										
										Sub SourceOnlyToolStripMenuItemClick(sender As Object, e As System.EventArgs)
											splitContainer1.Panel2Collapsed = True
											If splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal Then _
													splitContainer1.Splitterdistance = splitContainer1.panel1.Width
												If splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical Then _
														splitContainer1.Splitterdistance = splitContainer1.panel1.Height
													
													SplitToolStripMenuItem.Checked = False
													SplitToolStripMenuItem.Image = Nothing
													SourceOnlyToolStripMenuItem.Checked = True
													SourceOnlyToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
													TargetOnlyToolStripMenuItem.Checked = False
													TargetOnlyToolStripMenuItem.Image = Nothing
												End Sub
												
												Sub TargetOnlyToolStripMenuItemClick(sender As Object, e As System.EventArgs)
													splitContainer1.Panel1Collapsed = True
													If splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal Then _
															splitContainer1.Splitterdistance = splitContainer1.Panel2.Width
														If splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical Then _
																splitContainer1.Splitterdistance = splitContainer1.Panel2.Height
															
															SplitToolStripMenuItem.Checked = False
															SplitToolStripMenuItem.Image = Nothing
															SourceOnlyToolStripMenuItem.Checked = False
															SourceOnlyToolStripMenuItem.Image = Nothing
															TargetOnlyToolStripMenuItem.Checked = True
															TargetOnlyToolStripMenuItem.Image =imagelistsmallicons.Images.Item(13)
														End Sub
														
														Sub SplitContainer1SplitterMoved(sender As Object, e As System.Windows.Forms.SplitterEventArgs)
															panel3.Focus()
														End Sub
														
														Dim panelInfoColor As Color = Color.FromArgb(254, 254, 254)
														
														Sub SplitSizeFixedToolStripMenuItemClick(sender As Object, e As System.EventArgs)
															If SplitSizeFixedToolStripMenuItem.Checked then
																splitContainer1.IsSplitterFixed = False
																SplitSizeFixedToolStripMenuItem.Checked = False
															Else
																splitContainer1.IsSplitterFixed = True
																SplitSizeFixedToolStripMenuItem.Checked = True
																
																' reset splitter
																If splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical  Then _
																		splitContainer1.SplitterDistance = (splitContainer1.Width/2)-(splitContainer1.SplitterWidth/2)
																	If splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal  Then _
																			splitContainer1.SplitterDistance = (splitContainer1.Height/2)-(splitContainer1.SplitterWidth/2)
																		
																	End If
																End Sub
																
																Const dResult_OK As DialogResult = DialogResult.OK
																
																Sub OpenSourceFileToolStripMenuItemClick(sender As Object, e As System.EventArgs)
																	Dim openFileDialog1 As New OpenFileDialog()
																	
																	openFileDialog1.Title = "Open source file"
																	If Directory.Exists(Path.GetDirectoryName(Webbrowser1.Url.ToString)) Then _
																			openFileDialog1.InitialDirectory = Path.GetDirectoryName(Webbrowser1.Url.ToString)
																		openFileDialog1.Filter = "HTML files (*.htm; *.html)|*.htm;*.html|XML files (*.xml)|*.xml|Image files (*.bmp; *.GIF; *.JPG; *.JPEG; *.PNG; *.ART)|*.bmp;*.GIF;*.JPG;*.JPEG;*.PNG;*.ART|All files (*.*)|*.*"
																		openFileDialog1.FilterIndex = 1
																		openFileDialog1.RestoreDirectory = true
																		
																		If openFileDialog1.ShowDialog() = dResult_OK Then ' DialogResult.OK
																			
																			sourceFile = openFileDialog1.FileName
																			' get file type
																			fileType = Mid(Path.GetExtension(sourceFile),2)
																			determineMode(fileType)
																			' set mode
																			setMode()
																			
																			' load file
																			Webbrowser1.Url = New Uri(sourceFile)
																			
																		End If
																		openFileDialog1 =Nothing
																	End Sub
																	
																	Sub OpenTargetFileToolStripMenuItemClick(sender As Object, e As System.EventArgs)
																		Dim openFileDialog1 As New OpenFileDialog()
																		
																		openFileDialog1.Title = "Open target file"
																		If Directory.Exists(Path.GetDirectoryName(Webbrowser2.Url.ToString)) Then _
																				openFileDialog1.InitialDirectory = Path.GetDirectoryName(Webbrowser2.Url.ToString)
																			openFileDialog1.Filter = "HTML files (*.htm; *.html)|*.htm;*.html|XML files (*.xml)|*.xml|Image files (*.bmp; *.GIF; *.JPG; *.JPEG; *.PNG; *.ART)|*.bmp;*.GIF;*.JPG;*.JPEG;*.PNG;*.ART|All files (*.*)|*.*"
																			openFileDialog1.FilterIndex = 1
																			openFileDialog1.RestoreDirectory = True
																			
																			If openFileDialog1.ShowDialog() = dResult_OK Then ' DialogResult.OK
																				
																				targetFile = openFileDialog1.FileName
																				' get file type
																				fileType = Mid(Path.GetExtension(targetFile),2)
																				determineMode(fileType)
																				' set mode
																				setMode()
																				
																				' load file
																				Webbrowser2.Url = New Uri(targetFile)
																				
																			End If
																			openFileDialog1 =Nothing
																		End Sub
																		
																		Dim tmpWidth, tmpHeight, tmpX, tmpY As Integer
																		Sub FullScreenToolStripMenuItemClick(sender As Object, e As System.EventArgs)
																			If FullScreenToolStripMenuItem.Checked Then
																				' Me.WindowState = System.Windows.Forms.FormWindowState.Normal
																				Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
																				
																				Me.Width = tmpWidth
																				Me.Height = tmpHeight
																				Me.Top = tmpX
																				Me.Left = tmpY
																				
																				FullScreenToolStripMenuItem.Checked = False
																			Else
																				' Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
																				Me.WindowState = System.Windows.Forms.FormWindowState.Normal
																				
																				tmpWidth = Me.Width
																				tmpHeight = Me.Height
																				tmpX = Me.Top
																				tmpY = Me.Left
																				
																				Me.FormBorderStyle = System.Windows.Forms.BorderStyle.None
																				Dim activeScreen as System.Windows.Forms.Screen = Screen.FromControl(Me)
																				Me.StartPosition = FormStartPosition.Manual
																				Me.Location = New point(activeScreen.Bounds.X, activeScreen.Bounds.Y)
																				Me.Height = activeScreen.Bounds.Height
																				Me.Width = activeScreen.Bounds.Width
																				
																				FullScreenToolStripMenuItem.Checked = True
																				activeScreen = Nothing
																			End If
																		End Sub
																		
																		
																		
																		Sub DualscreenToolStripMenuItemClick(sender As Object, e As System.EventArgs)
																			Dim Screens() As System.Windows.Forms.Screen = System.Windows.Forms.Screen.AllScreens
																			
																			If DualscreenToolStripMenuItem.Checked Then
																				
																				splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical
																				splitContainer1.IsSplitterFixed = True
																				splitContainer1.SplitterWidth = 10
																				
																				' Dim sourceScreen as Rectangle = Screens(0).WorkingArea ' Screen.PrimaryScreen.WorkingArea
																				
																				Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
																				'			Me.StartPosition = FormStartPosition.Manual
																				'			Me.Location = New point(sourceScreen.X, sourceScreen.Y)
																				'			Me.Width = Screens(0).WorkingArea.Width
																				'			Me.Height = Screens(0).WorkingArea.Height
																				
																				Me.Width = tmpWidth
																				Me.Height = tmpHeight
																				Me.Top = tmpX
																				Me.Left = tmpY
																				
																				
																				splitContainer1.SplitterDistance = (splitContainer1.Width / 2) - (splitContainer1.SplitterWidth / 2)
																				
																				
																				DualscreenToolStripMenuItem.Checked = False
																			Else
																				
																				tmpWidth = Me.Width
																				tmpHeight = Me.Height
																				tmpX = Me.Top
																				tmpY = Me.Left
																				
																				splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical
																				splitContainer1.IsSplitterFixed = True
																				splitContainer1.SplitterWidth = 1
																				
																				If Screens.GetUpperBound(0)	>= 1 Then
																					Dim sourceScreen as Rectangle = Screens(0).WorkingArea ' Screen.PrimaryScreen.WorkingArea
																					Dim targetScreen as Rectangle = Screens(1).WorkingArea
																					
																					Me.FormBorderStyle = System.Windows.Forms.BorderStyle.None
																					Me.StartPosition = FormStartPosition.Manual
																					Me.Location = New point(sourceScreen.X, sourceScreen.Y)
																					Me.Width = Screens(0).WorkingArea.Width + Screens(1).WorkingArea.Width
																					Me.Height = Screens(0).WorkingArea.Height
																					
																					
																					splitContainer1.SplitterDistance = sourceScreen.Width
																					
																					
																					'		panel2Contents.Dock = DockStyle.Top ' None
																					'		panel2Contents.Width = splitContainer1.panel2.Width
																					'		panel2Contents.Height = Screens(1).WorkingArea.Height - menuStrip1.Size.Height
																					
																					DualscreenToolStripMenuItem.Checked = True
																				End If
																				
																				
																			End If
																			
																			screens = Nothing
																		End Sub
																		
																		
																		Sub ShowInfoToolStripMenuItemClick(sender As Object, e As System.EventArgs)
																			If showInfoToolStripMenuItem.Checked Then
																				showInfoToolStripMenuItem.Checked = False
																				panel1Info.Visible = False
																				panel2Info.Visible = False
																				panelSourceInfo.Visible = False
																				panelTargetInfo.Visible = False
																			Else
																				refreshSourceInfo()
																				refreshTargetInfo()
																				showInfoToolStripMenuItem.Checked = True
																				panelSourceInfo.Visible = True
																				panelTargetInfo.Visible = True
																				panel1Info.Visible = True
																				panel2Info.Visible = True
																				
																			End If
																		End Sub
																		
																		Sub HideControlsToolStripMenuItemClick(sender As Object, e As System.EventArgs)
																			If HideControlsToolStripMenuItem.Text = "Hide controls" Then
																				panel3.Visible = False
																				HideControlsToolStripMenuItem.Text = "Show controls"
																			Else
																				panel3.Visible = True
																				HideControlsToolStripMenuItem.Text = "Hide controls"
																			End If
																		End Sub
																		
																		Dim filter As String
																		Dim ignorelist As String = ""
																		Dim includelist As String = ""
																		Dim subFolders As Boolean
																		
																		Sub ProjectSettingsToolStripMenuItemClick(sender As Object, e As System.EventArgs)
																			' keep status when changing settings
																			refreshFileList = True
																			NewProjectToolStripMenuItemClick(Nothing, Nothing)
																		End Sub
																		
																		Sub NewProjectToolStripMenuItemClick(sender As Object, e As System.EventArgs)
																			usingDefaultFilter = False
																			Dim newproject As New ProjectSettingForm
																			me.Enabled = False
																			
																			' get existing values
																			newproject.sourceFolder = sourceFolder
																			newproject.targetFolder = targetFolder
																			newproject.subfolders = subfolders
																			if len(trim(filter)) = 0 then filter = defaultFilter ' "*.htm;*.html"
																			newproject.Filter = filter
																			
																			newproject.ignore = ignorelist
																			newproject.include = includelist
																			
																			If Not highlightClassNames is Nothing Then newproject.highlightSpanTags = string.Join(",",highlightClassNames)
																			
																			' get the basic project settings
																			newproject.ShowDialog()
																			
																			sourceFolder = newproject.sourceFolder
																			targetFolder = newproject.targetFolder
																			filter = newproject.Filter
																			subfolders = newproject.subfolders
																			
																			ignorelist = newproject.ignore
																			ignorelist = Replace(ignorelist, " ,",",")
																			ignorelist = Replace(ignorelist, " ,",",")
																			ignorelist = Replace(ignorelist, ", ",",")
																			ignorelist = Replace(ignorelist, ", ",",")
																			
																			includelist = newproject.include
																			includelist = Replace(includelist, " ,",",")
																			includelist = Replace(includelist, " ,",",")
																			includelist = Replace(includelist, ", ",",")
																			includelist = Replace(includelist, ", ",",")
																			
																			newproject.highlightSpanTags = Replace(newproject.highlightSpanTags, " ,",",")
																			newproject.highlightSpanTags = Replace(newproject.highlightSpanTags, " ,",",")
																			newproject.highlightSpanTags = Replace(newproject.highlightSpanTags, ", ",",")
																			newproject.highlightSpanTags = Replace(newproject.highlightSpanTags, ", ",",")
																			highlightClassNames = splitLine(newproject.highlightSpanTags)
																			
																			Me.Enabled = True
																			
																			' now process information
																			If newProject.OK Then
																				If Not refreshFileList Then
																					Me.Text = "Visual QA"
																					projectName = "New Project"
																					projectFile = ""
																				End If
																				saveStatus = False
																				openProject
																			End If
																		End Sub
																		
																		Dim fileListRead As Boolean = False
																		
																		Dim performanceTimer As TimeSpan
																		Dim startTime As DateTime
																		Dim stopTime As DateTime
																		Dim readingProgress As Integer = 0
																		
																		Private WithEvents readFilesWorker As System.ComponentModel.BackgroundWorker
																		
																		Sub openProject()
																			
																			Picturebox4.Image = imagelist2.Images.Item(0)
																			sourcePinned = False
																			Picturebox5.Image = imagelist2.Images.Item(0)
																			targetPinned = False
																			
																			' clean up
																			sourceFolder = Trim(sourceFolder)
																			if Microsoft.VisualBasic.Right(sourceFolder,1) = "\" then sourceFolder = Microsoft.VisualBasic.Left(sourceFolder,len(sourceFolder)-1)
																			targetFolder = Trim(targetFolder)
																			if Microsoft.VisualBasic.Right(targetFolder,1) = "\" then targetFolder = Microsoft.VisualBasic.Left(targetFolder,len(targetFolder)-1)
																			
																			startTime = now()
																			logWindow.document.Write("<span class=""time"">" & startTime & "</span> start reading files<br /><br />" & vbcrlf)
																			label1.Text = "Reading files ..."
																			If refreshFileList Then
																				logWindow.document.Write("----------<br /><br />" & vbcrlf)
																				logWindow.document.Write("<span class=""time"">" & now() & "</span> refreshing file list<br /><br />" & vbcrlf)
																				label1.Text = "Refreshing files ..."
																			End If
																			
																			If Not fileListRead then
																			
																				
																				If Not refreshFileList Then
																					' clear webbrowser controls
																					webBrowser1.DocumentText = "<html><head><title>Source</title>" & _
																						"<style type=""text/css""><!-- body {background-color: #FEFEFE;} h3 {font-family: Tahoma, Arial; font-weight: normal; text-align: center;} --></style></head>" & _
																						"<body><br /><br /><h3></h3></body></html>"
																						
																					webBrowser2.DocumentText = "<html><head><title>Source</title>" & _
																						"<style type=""text/css""><!-- body {background-color: #FEFEFE;} h3 {font-family: Tahoma, Arial; font-weight: normal; text-align: center;} --> </style></head>" & _
																						"<body><br /><br /><h3></h3></body></html>"
													
																				
																					' show source side only
																					If sourceFolder = targetFolder Then
																						splitContainer1.Panel2Collapsed = True
																						If splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal Then _
																								splitContainer1.Splitterdistance = splitContainer1.panel1.Width
																						If splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical Then _
																								splitContainer1.Splitterdistance = splitContainer1.panel1.Height
																								
																						SplitToolStripMenuItem.Checked = False
																						SourceOnlyToolStripMenuItem.Checked = True
																						TargetOnlyToolStripMenuItem.Checked = False
																						SourceOnlyToolStripMenuItemClick(Nothing, Nothing)
																					End If
																				End If
																				
																				' readFileList()
																				splitContainerContents.Enabled = False
																				menuStrip1.Enabled = False
																				panel5.Dock = dockstyle.Fill
																				panel5.BringToFront()
																				panel5.Visible = True
																				
																				readingProgress = 0
																			  	readFilesWorker = New System.ComponentModel.BackgroundWorker
																			    readFilesWorker.WorkerReportsProgress = True
																			    readFilesWorker.WorkerSupportsCancellation = True
																			    readFilesWorker.RunWorkerAsync()
																			Else
																				showFirstFile()
																			End If
																			' showFirstFile()
						
																		End Sub
																		
																		Sub showFirstFile()
													
																			stopTime = Now()
																			performanceTimer = stopTime.Subtract(startTime)
																			logWindow.document.Write("<span class=""time"">" & stopTime & "</span> finished reading files (" & performanceTimer.ToString & ")<br /><br />" & vbcrlf)
																			logWindow.document.Write("----------<br /><br />" & vbcrlf)
																		
																			fileListRead = False

																			' load first file
																			projectActive = True
																			labelStatus.Visible = True
																			labelCounter.Visible = True
																			
																			SaveProjectToolStripMenuItem.Enabled = True
																			' newProjectToolStripMenuItem.Text = "Project settings..."
																			showFileListToolStripMenuItem.Visible = True
																			showCommentsToolStripMenuItem.Visible = True
																			projectToolStripMenuItem.Visible = True
																			ProjectFilterToolStripMenuItem.Visible = True
					
																			pictureBox4.Visible = True
																			pictureBox5.Visible = True
														
																			CloseProjectToolStripMenuItem.Enabled = True
																			openFoldersInCompareToolToolStripMenuItem.Visible = True
																			
																			fillFileListTree()
																			
																			splitContainerContents.Enabled = True
																			panel5.Visible = False
																			menuStrip1.Enabled = True
																			
'																			' show source side only
'																			If sourceFolder = targetFolder Then
'																				splitContainer1.Panel2Collapsed = True
'																				If splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal Then _
'																						splitContainer1.Splitterdistance = splitContainer1.panel1.Width
'																				If splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical Then _
'																						splitContainer1.Splitterdistance = splitContainer1.panel1.Height
'
'																				SplitToolStripMenuItem.Checked = False
'																				SourceOnlyToolStripMenuItem.Checked = True
'																				TargetOnlyToolStripMenuItem.Checked = False
'																				SourceOnlyToolStripMenuItemClick(Nothing, Nothing)
'																			End If
																					
																			activeFile = -1
																			showFile(0)
																			
'																			buttonNext.Enabled = True
'																			buttonPrevious.Enabled = True
'																			buttonNextOff.visible = True
'																			buttonPreviousOff.visible = True
											
																		End Sub
																		
																		Private Sub readFilesWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles readFilesWorker.ProgressChanged
																		    readingProgress = e.ProgressPercentage
																		    ' e.UserState
																		End Sub
																		
'																		Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
'																		    readFilesWorker.CancelAsync()
'																		End Sub

																		Private Sub readFilesWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles readFilesWorker.RunWorkerCompleted
																		    readingProgress = 100
																		    ' should we display first file?
																		    If Not nofilesFound Then
																		    	showFirstFile()
																		    Else
																				splitContainerContents.Enabled = True
																				panel5.Visible = False
																				menuStrip1.Enabled = True
																		    	CloseProjectToolStripMenuItemClick(nothing,nothing)
																		    End If
																		End Sub
																			
																		Dim noFilesFound As Boolean = False
																			
																		Sub readFileList(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles readFilesWorker.DoWork ' readFileList()
																			
																			noFilesFound = False
																		
																			Dim tmpArray(filelist.GetUpperBound(0),4) As String
																			If refreshFileList Then

																				' make temporary copy of the existing file list
																				
																				' Create a clone of the filelist array.
																				tmpArray = fileList.Clone()
																				
																				'				For i As Integer = 0 To filelist.GetUpperBound(0) -1
																				'					For n As Integer = 0 To filelist.GetUpperBound(1) -1
																				'						tmpArray(i,n) = fileList(i,n)
																				'					Next
																				'				Next
																				
																			End If
																		
																		
																			' Dim sourceFiles As String() = Directory.GetFiles(sourceFolder,"*.htm?") ' .Length
																			
																			Dim getFiles As New ParseFolder
																			getFiles.Input = sourceFolder
																			getFiles.Output = sourceFolder ' targetFolder
																			getFiles.Filter = filter
																			getfiles.subfolders = subfolders
																			getfiles.ignore = ignorelist
																			getfiles.include = includelist
																			
																			' get the actual files
																			getfiles.run()
																			
																				If getFiles.count = 0 And usingDefaultFilter Then
																					filter = "*.bmp;*.gif;*.jpg;*.png"
																					getFiles.Filter = filter
																					getfiles.run()
																				End If
																			
																			If getFiles.outputFiles Is Nothing Then
																		
																				MsgBox("No files to process found, check folders")
																				' logWindow.document.Write("no files to process found<br /><br />" & vbcrlf)
																				noFilesFound = True
																				Exit Sub
																		
																			End If
																			
																			Dim sourceFiles As String() = getFiles.outputFiles
																			
																			'For i As Integer = 0 To sourceFiles.GetUpperBound(0)
																			'  System.Diagnostics.Debug.Print(i+1 & " : " & sourceFiles(i))
																			'Next
																			
																			If sourceFiles.Length = 0 Then
																				MsgBox("No files to process found, check folders")
																				' logWindow.document.Write("no files to process found<br /><br />" & vbcrlf)
																				noFilesFound = True
																				Exit Sub
																			End If
																			
																			Dim targetFiles As String()
																			If checkOrphans Or checkMD5Hashes Then
																				
																				getFiles.Input = targetFolder
																				getFiles.Output = targetFolder
																				getFiles.Filter = filter
																				getfiles.subfolders = subfolders
																				getfiles.ignore = ignorelist
																				getfiles.include = includelist
																				
																				' get the actual files
																				getfiles.run()
																				
																				targetFiles = getFiles.outputFiles
																				
																				'For i As Integer = 0 To targetFiles.GetUpperBound(0)
																				'  System.Diagnostics.Debug.Print(i+1 & " : " & targetFiles(i))
																				'Next
																				
																				
																				If checkOrphans And targetFiles.GetUpperBound(0) >= 0 Then
																					' add the orphaned files to the end of array
																					For j As Integer = 0 To targetFiles.GetUpperBound(0)
																						WordToFind = Replace(targetFiles(j),targetFolder,sourceFolder)
																						' If Array.Find(sourceFiles, AddressOf FindWord) Is Nothing Then ' not found
																						If NOT Array.IndexOf(sourceFiles, WordToFind) >= 0 Then
																							ReDim Preserve sourceFiles(sourceFiles.GetUpperBound(0)+1)
																							sourceFiles(sourceFiles.GetUpperBound(0)) = Replace(targetFiles(j),targetFolder,sourceFolder)
																							System.Diagnostics.Debug.Print("+ " & targetFiles(j))
																						End If
																					Next
																					' sort the array
																					array.Sort(sourceFiles)
																				End If
																				
																			End If
																			
																			'For i As Integer = 0 To sourceFiles.GetUpperBound(0)
																			'  System.Diagnostics.Debug.Print(i+1 & " : " & sourceFiles(i))
																			'Next
																			
																			' create file list
																			ReDim fileList(sourceFiles.GetUpperBound(0)+1,5)
																			System.Diagnostics.Debug.Print(sourceFiles.GetLength(0))
																			
																			' 0: source file
																			' 1: target file
																			' 2: state
																			' 3: comment
																			' 4: checksum
																			
																			Dim i As Integer = 0
																			For i = 0 To sourceFiles.GetUpperBound(0)
																				System.Diagnostics.Debug.Print(i+1 & " > " & sourceFiles(i))
																				If sourceFolder = targetFolder Then
																					
																					fileList(i,0) = sourceFiles(i)
																					fileList(i,1) = sourceFiles(i)
																					fileList(i,2) = "unchecked"
																					
																					checkFiles(i)
																					
																				Else If Not File.Exists(sourceFiles(i)) Then
																					fileList(i,0) = sourceFiles(i)
																					fileList(i,1) = Path.Combine(Replace(Path.GetDirectoryName(sourceFiles(i)),sourceFolder,targetFolder),Path.GetFileName(sourceFiles(i))).ToString
																					fileList(i,2) = "Orphan"
																					fileList(i,3) = "Missing source file"
																					
																					checkFiles(i)
																					
																				Else If File.Exists(Path.Combine(Replace(Path.GetDirectoryName(sourceFiles(i)),sourceFolder,targetFolder), Path.GetFileName(sourceFiles(i)))) Then
																					fileList(i,0) = sourceFiles(i)
																					fileList(i,1) = Path.Combine(Replace(Path.GetDirectoryName(sourceFiles(i)),sourceFolder,targetFolder),Path.GetFileName(sourceFiles(i))).ToString
																					fileList(i,2) = "unchecked"
																					
																					checkFiles(i)
																					
																					If checkMD5Hashes Then
																						If getMD5Hash(sourceFiles(i)) = getMD5Hash(fileList(i,1)) Then _
																								fileList(i,2) = "Identical"
																						End If
																						
																					Else
																						fileList(i,0) = sourceFiles(i)
																						fileList(i,1) = Path.Combine(Replace(Path.GetDirectoryName(sourceFiles(i)),sourceFolder,targetFolder),Path.GetFileName(sourceFiles(i))).ToString
																						fileList(i,2) = "Missing"
																						fileList(i,3) = "Missing target file"
																						
'																						logWindow.document.Write( fileList(i,2) & ": " & fileList(i,3) & " " & _
'																							"<span class=""external"" onclick=""window.external.openFile('" & i & "')"">[" & (i + 1) & "]</span><br />" & _
'																						"<span class=""external"" onclick=""window.external.Open('" & fixPath(Replace(Path.GetDirectoryName(fileList(i,0)),sourceFolder,targetFolder)) & "')"" >" & Replace(Path.GetDirectoryName(fileList(i,0)),sourceFolder,targetFolder) & _
'																						"</span>\" & Path.GetFileName(fileList(i,0)) & "<br /><br />" & vbcrlf)
																						
																						checkFiles(i)
																						
																					End If
																					
																					' add checksum
																					fileList(i,4) = getChecksum(ucase(Replace(fileList(i,0).ToString,sourceFolder,"") & Replace(fileList(i,1).ToString,targetFolder,"")))
																					' Note that checksum is based on the combined relative paths from the source and target folders
																					
																				Next
																				
																				If refreshFileList Then
'				Dim checkSumArray(fileList.GetUpperBound(0)) As String
'				For i As Integer = 0 To fileList.GetUpperBound(0) - 1
'					checksumArray(i) = fileList(i,4)
'				Next
			
																		
																					' walk through all items in tmpArray (previous list)
																					For k As Integer = 0 To tmpArray.GetUpperBound(0) - 1
																						' compare checksum against the new list
																						
'				wordtofind = tmpArray(i, 4)
'				Dim n As Integer = Array.FindIndex(checksumArray, AddressOf findword)
'				If n >= 0
'					If keepComments Then fileList(n,3) = tmpArray(i, 3)
'					If keepStatus And tmpArray(i, 2) = "OK" Or tmpArray(i, 2) = "NG" Or tmpArray(i, 2) = "Flagged" Then _
'							fileList(n,2) = tmpArray(i, 2)
'				End If
'				checksumArray = Nothing
																						' System.Diagnostics.Debug.Print("old " & tmpArray(i, 4))
																						For n As Integer = 0 To fileList.GetUpperBound(0) - 1
																							' System.Diagnostics.Debug.Print("new " & fileList(n, 4))
																							if fileList(n, 4) = tmpArray(k, 4) then
																								If keepComments Then fileList(n,3) = tmpArray(i, 3)
																								If keepStatus And Ucase(tmpArray(k, 2)) = "OK" Or Ucase(tmpArray(k, 2)) = "NG" Or Ucase(tmpArray(i, 2)) = "FLAGGED" Then _
																										fileList(n,2) = tmpArray(k, 2)
																								Exit For
																							End If
																						Next
																					Next
																						
																					End If
																					refreshFileList = False
																					Redim tmpArray(0,0)
																		
																		End Sub
																		

																				
																						Dim WordToFind As String
																						
																						Private Function FindWord(ByVal w As String) As Boolean
																							
																							System.Diagnostics.Debug.Print(WordToFind & " - " & w)
																							If Not w is Nothing then
																								If w.Contains(WordToFind) Then
																									Return True
																								Else
																									Return False
																								End If
																							Else
																								Return False
																							End If
																						End Function
																						
																						
																						Sub checkFiles(i As Integer)
																							
																							Dim srcFile As String = fileList(i,0)
																							Dim tgtFile As String =	fileList(i,1)
																							
																							If Not file.Exists(tgtFile) then Exit Sub
																							
'																								Dim _tmpReader As New StreamReader(tgtFile)
'																								Dim _tmpHeader As String = ""
'																								Dim isXML As Boolean = False
'																								For n As Integer = 1 To 3
'																									If Not _tmpreader.EndOfStream then _tmpHeader = _tmpHeader & _tmpReader.ReadLine()
'																								Next
'																								_tmpReader = Nothing
'																								If InStr(_tmpHeader, "<!DOCTYPE ") > 0 Then isXML = True
																								
																							
																							If ucase(Path.GetExtension(srcFile)) = ".XML" Or ucase(Path.GetExtension(srcFile)).StartsWith(".HTM") and parseXMLOnLoad Then

																								Dim validateFile As New ValidateXMLClass
																								Try
																								validateFile.fileIn = tgtFile
																								
																								If Not validateFile.wellFormed Then ' validateFile.validate() Then
																									
																									fileList(i,2) = "Problem"
																									fileList(i,3) = "Document is invalid - " & validateFile.report
'																									logWindow.document.Write( fileList(i,2) & ": " & "Document is invalid" & " " & _
'																										"<span class=""external"" onclick=""window.external.openFile('" & i & "')"">[" & (i + 1) & "]</span><br />" & _
'																									"<span class=""external"" onclick=""window.external.Open('" & fixPath(tgtFile) & "','" & fixPath(defaultEditor) & "')"" >" & tgtFile & "</span><br />" & _
'																									validateFile.report & "<br /><br />" & vbcrlf)
																								End If
																								Catch e As Exception
																									fileList(i,2) = "Problem"
																									fileList(i,3) = "Document is not XML based"
																								End Try
																								validateFile = Nothing
																							End If
																						End Sub
																						
																						
																						Sub initEncodingMenuContents()
																							Dim ei As System.Text.EncodingInfo
																							For Each ei In  System.Text.Encoding.GetEncodings()
																								Dim e As System.Text.Encoding = ei.GetEncoding()
																								If e.IsBrowserDisplay Then
																									contextMenuStripEncoding.Items.Add(ei.Name)
																								End If
																								
																							Next ei
																							
																						End Sub
																						
																						
																						Sub ContextMenuStripEncodingItemClicked(sender As Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs)
																							Dim c As Control = ContextMenuStripEncoding.SourceControl
																							' c.Name.ToString
																							If activePanel = 1 Then
																								LabelEncoding1.Text = e.ClickedItem.ToString
																								activePanel = 0
																								Exit Sub
																								Else If activePanel = 2 Then
																								LabelEncoding2.Text = e.ClickedItem.ToString
																								activePanel = 0
																								exit sub
																							End If
																							If c Is Nothing then Exit Sub
																							If c.Name = labelEncoding1.Name OR c.Name = labelEncoding2.Name then c.text = e.ClickedItem.ToString
																							
																						End Sub
																						
																						Sub LabelEncoding1TextChanged(sender As Object, e As System.EventArgs)
																							Try
																							If Not lcase(LabelEncoding1.Text) = lcase(sourceEncoding) Then
																								sourceDoc.Encoding = LabelEncoding1.Text
																								sourceEncoding = LabelEncoding1.Text
																							End If
																							Catch ex As Exception
																							End Try
																						End Sub
																						
																						Sub LabelEncoding2TextChanged(sender As Object, e As System.EventArgs)
																							Try
																							If Not lcase(LabelEncoding2.Text) = lcase(targetEncoding) Then
																								targetDoc.Encoding = LabelEncoding2.Text
																								targetEncoding = LabelEncoding2.Text
																							End If
																							Catch ex As Exception
																							End Try
																						End Sub
																						
																						
																						
																						
																				Dim activePanel As Integer = 0
																						
																				Sub ContextMenuStripInfoOpening(sender As Object, e As System.ComponentModel.CancelEventArgs)
																					
																					Dim c As Control = ContextMenuStripInfo.SourceControl
																					If c Is Nothing Then Exit Sub
																					
																					Select c.Name
																					Case panelSourceInfo.Name, textBoxTitle1.Name
																						activePanel = 1
																						historyBackToolstripMenuItem.Enabled = webbrowser1.CanGoBack
																						
																						If projectActive Then
																							pinToolStripMenuItem.Visible = True
																							If sourcePinned Then
																								pinToolStripMenuItem.Text = "Unpin"
																							Else
																								pinToolStripMenuItem.Text = "Pin"
																							End If
																						Else
																							pinToolStripMenuItem.Visible = False
																						End If
																					Case panelTargetInfo.Name, textBoxTitle2.Name
																						activePanel = 2
																						historyBackToolstripMenuItem.Enabled = webbrowser2.CanGoBack
																						
																						If projectActive Then
																							pinToolStripMenuItem.Visible = True
																							If targetPinned Then
																								pinToolStripMenuItem.Text = "Unpin"
																							Else
																								pinToolStripMenuItem.Text = "Pin"
																							End If
																						Else
																							pinToolStripMenuItem.Visible = False
																						End If
																					End Select
																				
																					If imageMode Then
																						copyTitleToClipboardToolStripMenuItem.Visible = False
																						historyBackToolStripMenuItem.Visible = False
																					Else
																						copyTitleToClipboardToolStripMenuItem.Visible = True
																						historyBackToolStripMenuItem.Visible = True
																					End If
																				
																				End Sub
																				
																				
																				Private Function URLDecode(ByVal txt As String) As String
																					Dim txt_len As Integer
																					Dim i As Integer
																					Dim ch As String
																					Dim digits As String
																					Dim result As String
																					
																					result = ""
																					txt_len = Len(txt)
																					i = 1
																					Do While i <= txt_len
																						' Examine the next character.
																						ch = Mid$(txt, i, 1)
																						If ch = "+" Then
																							' Convert to space character.
																							result = result & " "
																						ElseIf ch <> "%" Then
																							' Normal character.
																							result = result & ch
																						ElseIf i > txt_len - 2 Then
																							' No room for two following digits.
																							result = result & ch
																						Else
																							' Get the next two hex digits.
																							digits = Mid$(txt, i + 1, 2)
																							result = result & Chr(CInt("&H" & digits))
																							i = i + 2
																						End If
																						i = i + 1
																					Loop
																					
																					result = Replace(result,"/","\")
																					
																					URLDecode = result
																				End Function
																				
															
																	Function getLineNumber(textIn As String, index As Integer) As Integer
																		textIn = textIn.Replace(VbCrLf,VbLf)
																		Try
																			Dim line as Integer = regex.Matches(textIn.Substring(0,index),"\n",RegexOptions.Multiline).Count + 1
																			If line < 1 Then line = 1
																			Return line
																		Catch
																			Return 0
																		End Try
																	End Function

																	Function getColumnNumber(textIn As String, index As Integer) As Integer
																		textIn = textIn.Replace(VbCrLf,VbLf)
																		Try
																			Dim line As Integer = regex.Matches(textIn.Substring(0,index),"\n",RegexOptions.Multiline).Count + 1
																			If line <= 1 Then
																				line = 1
																				Return index
																			Else
																				Dim column As Integer = index - regex.Matches(textIn.Substring(0,index),"\n",RegexOptions.Multiline)(line-2).index - (line-1) - 1
																				Return column
																			End If
																		Catch
																			Return 0
																		End Try
																	End Function
																																			
																Sub EditorToolStripMenuItemClick(sender As Object, e As System.EventArgs)
																	' determine editor
																	dim editor As string
																		Select lcase(mid(path.GetExtension(sourceDoc.Url.AbsolutePath),2,3))
																			Case "bmp", "gif", "png", "jpe", "jpg"
																				editor = imageEditor
																			Case Else ' "htm", "xml", "txt"
																				editor = defaultEditor
																		End Select
																		
																		If Not sender Is Nothing Then
																			If sender.text.tolower.contains("source file") Then
																				activePanel = 1
																			ElseIf sender.text.tolower.contains("target file") Then
																				activePanel = 2
																			End If
																		Else
																			activePanel = 2
																		End If
																		
																		Try
																		Select activePanel
																		Case 1
																			If editor.Contains("%") Then
																				Dim _app As String = editor.Substring(0, editor.IndexOf(".exe") + 4)
																				Dim _args As String = editor.Substring(editor.IndexOf(".exe") + 4).Trim()
																				_args = _args.Replace("%source", sourceFile)
																				_args = _args.Replace("%target", targetFile)
'																				_args = _args.Replace("%line", line)
'																				_args = _args.Replace("%column", column)
																				System.Diagnostics.Debug.WriteLine(_app & VbCrLf & VbCrLf & _args)
																				Process.Start(_app,_args)
																			Else
																				Process.Start(editor, """" & UrlDecode(sourceDoc.Url.AbsolutePath) & """")
																			End If
																			activePanel = 0
																		Case 2
																			If editor.Contains("%") Then
																				Dim _app As String = editor.Substring(0, editor.IndexOf(".exe") + 4)
																				Dim _args As String = editor.Substring(editor.IndexOf(".exe") + 4).Trim()
																				_args = _args.Replace("%source", sourceFile)
																				_args = _args.Replace("%target", targetFile)
'																				_args = _args.Replace("%line", line)
'																				_args = _args.Replace("%column", column
																				System.Diagnostics.Debug.WriteLine(_app & VbCrLf & VbCrLf & _args)
																				Process.Start(_app,_args)
																			Else
																				Process.Start(editor, """" & UrlDecode(targetDoc.Url.AbsolutePath) & """")
																			End IF
																			activePanel = 0
																		Case Else
																		End Select
																		Catch ex As Exception
																			Msgbox("Check editor settings.")
																		End Try
																End Sub
																
																Sub BrowserToolStripMenuItemClick(sender As Object, e As System.EventArgs)
																	Select activePanel
																Case 1
																	Process.Start(UrlDecode(sourceDoc.Url.AbsolutePath))
																	activePanel = 0
																Case 2
																	Process.Start(UrlDecode(targetDoc.Url.AbsolutePath))
																	activePanel = 0
																Case Else
															End Select
														End Sub
														
														Sub CopyLocationToClipboardToolStripMenuItemClick(sender As Object, e As System.EventArgs)
															Select activePanel
														Case 1
															Clipboard.SetText(UrlDecode(sourceDoc.Url.AbsolutePath), TextDataFormat.Text)
															activePanel = 0
														Case 2
															Clipboard.SetText(UrlDecode(targetDoc.Url.AbsolutePath), TextDataFormat.Text)
															activePanel = 0
														Case Else
													End Select
												End Sub
												
										Sub CopyFileNameToClipboardToolStripMenuItemClick(sender As Object, e As System.EventArgs)
												Select activePanel
												Case 1
													Clipboard.SetText(path.GetFileName(UrlDecode(sourceDoc.Url.AbsolutePath)), TextDataFormat.Text)
													activePanel = 0
												Case 2
													Clipboard.SetText(path.GetFileName(UrlDecode(targetDoc.Url.AbsolutePath)), TextDataFormat.Text)
													activePanel = 0
												Case Else
											End Select
										End Sub
										
								Sub RefreshToolStripMenuItemClick(sender As Object, e As System.EventArgs)
										Select activePanel
										Case 1
											If imageMode Then
												loadPicturebox1(sourceFile)
											Else
												Webbrowser1.Url = New Uri(sourceFile) ' webbrowser1.Refresh()
											End If
											activePanel = 0
											refreshSourceInfo()
											panelSourceInfo.Refresh()
										Case 2
											If imageMode Then
												loadPicturebox2(targetFile)
											Else
												Webbrowser2.Url = New Uri(targetFile)  ' webbrowser2.Refresh()
											End If
											activePanel = 0
											refreshTargetInfo()
											panelTargetInfo.Refresh()
										Case Else
									End Select
								End Sub

								Sub HistoryBackToolStripMenuItemClick(sender As Object, e As System.EventArgs)
										Select activePanel
										Case 1
											Try
												webbrowser1.GoBack()
											Catch ex As Exception
											End Try
											activePanel = 0
											refreshSourceInfo()
											panelSourceInfo.Refresh()
										Case 2
											Try
												webbrowser2.GoBack()
											Catch ex As Exception
											End Try
											activePanel = 0
											refreshTargetInfo()
											panelTargetInfo.Refresh()
										Case Else
									End Select
								End Sub
								
								Sub OpenContainingFoldertoolStripMenuItemClick(sender As Object, e As System.EventArgs)
									Select activePanel
								Case 1
									Process.Start(fileExplorer, Path.GetDirectoryName(UrlDecode(sourceDoc.Url.AbsolutePath)))
									activePanel = 0
								Case 2
									Process.Start(fileExplorer, Path.GetDirectoryName(UrlDecode(targetDoc.Url.AbsolutePath)))
									activePanel = 0
								Case Else
							End Select
						End Sub
						
						Sub TargetToolStripMenuItemClick(sender As Object, e As System.EventArgs)
							activePanel = 2
							historyBackToolStripMenuItem.Enabled = webbrowser2.CanGoBack
							
							If projectActive Then
								pinToolStripMenuItem.Visible = True
								If targetPinned Then
									pinToolStripMenuItem.Text = "Unpin"
								Else
									pinToolStripMenuItem.Text = "Pin"
								End If
							Else
								pinToolStripMenuItem.Visible = False
							End If
						End Sub
						
						Sub SourceToolStripMenuItemClick(sender As Object, e As System.EventArgs)
							activePanel = 1
							historyBackToolStripMenuItem.Enabled = webbrowser1.CanGoBack
							
							If projectActive Then
								pinToolStripMenuItem.Visible = True
								If sourcePinned Then
									pinToolStripMenuItem.Text = "Unpin"
								Else
									pinToolStripMenuItem.Text = "Pin"
								End If
							Else
								pinToolStripMenuItem.Visible = False
							End If
						End Sub
						
						Sub PreferedEditorToolStripMenuItemClick(sender As Object, e As System.EventArgs)
							
							Dim result As String = InputBox("Enter the path for your default editor", "Editor", defaultEditor)
							If result Is "" Then
								' nothing to do
							Else
								If file.Exists(result) then defaultEditor = result
							End If
							
						End Sub
						
						#Region " Configuration "
						' read/write project settings
						
						' xcfg structured configuration file
						Sub writeXcfg(xcfgFile As String, optional settingsOnly As Boolean = False)
						  Try
							If Not File.Exists(xcfgFile) Then ' create an empty document
								
								Dim Enc As System.Text.Encoding	= System.Text.Encoding.UTF8 ' Unicode ' set to Nothing defaults to environment default
								Dim xcfg As New XmlTextWriter(xcfgFile, Enc)
								xcfg.Formatting = Formatting.Indented
								xcfg.WriteStartDocument ' start document
								
								xcfg.WriteStartElement("job") ' write root element
								xcfg.WriteEndElement ' close root
								
								xcfg.WriteEndDocument 'end Document
								xcfg.Flush 'write to file
								xcfg.Close
								
							End If
							
							Dim xcfgdoc As XmlDocument = new XmlDocument()
							xcfgdoc.Load(xcfgFile)
							Dim root as XmlElement = xcfgdoc.DocumentElement ' ("//job/")
							
							' check if the current application already exists
							Dim appNode As XmlNode = root.SelectSingleNode("task[@tool='" & appName & "']")
							
							If Not appNode is Nothing then ' update current settings
								
								Dim activeNode As XmlNode = appNode.SelectSingleNode("source")
								activeNode.InnerText =sourceFolder ' .InnerXml
								
								' Dim attrCollection as XmlAttributeCollection = activeNode.Attributes
								Dim attr As XMLAttribute = activeNode.Attributes("sub-folders")
								attr.Value = subFolders.ToString
								
								activeNode = appNode.SelectSingleNode("target")
								activeNode.InnerText = targetFolder ' .InnerXml
								
								activeNode = appNode.SelectSingleNode("filter")
								activeNode.InnerText = filter ' .InnerXml
																
								activeNode = appNode.SelectSingleNode("includelist")
								activeNode.InnerText = includelist ' .InnerXml
								
								activeNode = appNode.SelectSingleNode("ignorelist")
								activeNode.InnerText = ignorelist ' .InnerXml
								
								activeNode = appNode.SelectSingleNode("highlightSpanTags")
								IF Not highlightClassNames Is Nothing Then activeNode.InnerText = string.Join(",",highlightClassNames) ' .InnerXml
							
									' environment
									activeNode = appNode.SelectSingleNode("environment/showFileList")
									activeNode.InnerText = ShowFileListToolStripMenuItem.Checked.ToString ' .InnerXml
	
									activeNode = appNode.SelectSingleNode("environment/fileListPaneWidth")
									activeNode.InnerText = SplitContainerContents.SplitterDistance ' .Inne
						
									activeNode = appNode.SelectSingleNode("environment/showFileInfo")
									activeNode.InnerText = showInfoToolStripMenuItem.Checked.ToString ' .InnerXml
						
									activeNode = appNode.SelectSingleNode("environment/showComments")
									activeNode.InnerText = ShowCommentsToolStripMenuItem.Checked.ToString ' .InnerXml
								
									Try
										activeNode = appNode.SelectSingleNode("environment/browserWidth")
										activeNode.InnerText = WebBrowser1.Width
										attr = activeNode.Attributes("exclude-scrollbar")
										attr.Value = excludeScrollbarToolStripMenuItem.Checked.ToString()
										attr = activeNode.Attributes("show-size")
										attr.Value = showBrowserWidthToolStripMenuItem.Checked.ToString()
										
										activeNode = appNode.SelectSingleNode("environment/browserSizePreset1")
										activeNode.InnerText = toolStripMenuItemSize1.Text
										activeNode = appNode.SelectSingleNode("environment/browserSizePreset2")
										activeNode.InnerText = toolStripMenuItemSize2.Text						
										activeNode = appNode.SelectSingleNode("environment/browserSizePreset3")
										activeNode.InnerText = toolStripMenuItemSize3.Text										
										activeNode = appNode.SelectSingleNode("environment/browserSizePreset4")
										activeNode.InnerText = toolStripMenuItemSize4.Text	
										
									Catch x As Exception
									End Try
									
									
									Try
										activeNode = appNode.SelectSingleNode("environment/highlightLocalLinks")
										activeNode.InnerText = highlightLocalLinksToolStripMenuItem.Checked.ToString ' .InnerXml

'										activeNode = appNode.SelectSingleNode("environment/highlightTags")
'										activeNode.InnerText = highlightUITermsToolStripMenuItem.Checked.ToString ' .InnerXml

										activeNode = appNode.SelectSingleNode("environment/imageBackGroundColor")
										activeNode.InnerText = colortranslator.ToHtml(imageBackGroundColor) ' .InnerXml

										activeNode = appNode.SelectSingleNode("environment/fitImages")
										activeNode.InnerText = fitImagesToolStripMenuItem1.Checked.ToString ' .InnerXml

										activeNode = appNode.SelectSingleNode("environment/autoRefreshAfterEdit")
										activeNode.InnerText = autoRefreshAfterEditToolStripMenuItem.Checked.ToString ' .InnerXml

										activeNode = appNode.SelectSingleNode("environment/useMouseGestures")
										activeNode.InnerText = useMouseGesturesToolStripMenuItem.Checked.ToString ' .InnerXml

										activeNode = appNode.SelectSingleNode("environment/followLinks")
										activeNode.InnerText = followLinksToolStripMenuItem.Checked.ToString ' .InnerXml

										activeNode = appNode.SelectSingleNode("environment/autoScroll")
										activeNode.InnerText = autoScrollToolStripMenuItem.Checked.ToString ' .InnerXml
										
										activeNode = appNode.SelectSingleNode("environment/defaultFilter")
										activeNode.InnerText = defaultFilter ' .InnerXml

										activeNode = appNode.SelectSingleNode("environment/defaultSourceFolder")
										activeNode.InnerText = defaultSourceFolder ' .InnerXml
										
										activeNode = appNode.SelectSingleNode("environment/editor")
										activeNode.InnerText = defaultEditor ' .InnerXml

										activeNode = appNode.SelectSingleNode("environment/imageEditor")
										activeNode.InnerText = imageEditor ' .InnerXml
										
										activeNode = appNode.SelectSingleNode("environment/compareTool")
										if Not fileCompare Is Nothing Then activeNode.InnerText = fileCompare ' .InnerXml
														
									Catch ex As Exception
									End Try
								
								
								Dim name As String = "fileList"
								Dim xmlData As String = "<" & name & "></" & name & ">"
								Dim xmlSnippet as XmlDocument = new XmlDocument()
								xmlSnippet.Load(New StringReader(xmlData))
								
								For row As Integer = 0 to filelist.GetUpperBound(0) - 1
									Dim record As XmlElement = xmlSnippet.CreateElement("file")
									attr = xmlSnippet.CreateAttribute("id")
									attr.value = row + 1
									record.attributes.append(attr)
									xmlSnippet.DocumentElement.AppendChild(record)
									
									For cell As Integer = 0 to filelist.GetUpperBound(1) - 1
										Dim field As XmlElement = xmlSnippet.CreateElement("field" & cell)
										field.InnerText = filelist.GetValue(row, cell)
										record.AppendChild(field)
									Next
								Next
								
								activeNode = appNode.SelectSingleNode(name)
								appNode.RemoveChild(activeNode)
								Dim tmpNode As xmlNode = xcfgdoc.ImportNode(xmlSnippet.DocumentElement, True)
								appNode.AppendChild(tmpNode)
								
								
								' task specific settings go here
								
								xcfgdoc.Save(xcfgFile)
								attr = nothing
								activeNode = Nothing
								
								
							Else ' write new item
								
								
								Dim tmpNode As XmlNode = xcfgdoc.CreateElement("task")
								Dim attr As XmlAttribute = xcfgdoc.CreateAttribute("tool")
								attr.value = appName
								tmpNode.attributes.append(attr)
								root.appendChild(tmpNode)
								
								appNode = root.SelectSingleNode("task[@tool='" & appName & "']")
								
								If Not settingsOnly Then
									tmpNode = xcfgdoc.CreateElement("source")
									If Not len(trim(sourceFolder)) = 0 then tmpNode.InnerText = sourceFolder
									attr  = xcfgdoc.CreateAttribute("sub-folders")
									attr.value = subFolders.ToString
									tmpNode.attributes.append(attr)
									appNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("target")
									If Not len(trim(targetFolder)) = 0 then tmpNode.InnerText = targetFolder
									appNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("filter")
									If Not len(trim(filter)) = 0 then tmpNode.InnerText = filter
									appNode.appendChild(tmpNode)
																		
									tmpNode = xcfgdoc.CreateElement("includelist")
									If Not len(trim(includelist)) = 0 then tmpNode.InnerText = includelist
									appNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("ignorelist")
									If Not len(trim(ignorelist)) = 0 then tmpNode.InnerText = ignorelist
									appNode.appendChild(tmpNode)
	
									tmpNode = xcfgdoc.CreateElement("highlightSpanTags")
									If not highlightClassNames Is Nothing then tmpNode.InnerText = string.Join(",",highlightClassNames)
									appNode.appendChild(tmpNode)
								End If
								
									' environment
									tmpNode = xcfgdoc.CreateElement("environment")
									appNode.appendChild(tmpNode)
									
									Dim envNode As XmlNode = appNode.SelectSingleNode("environment")
									
									tmpNode = xcfgdoc.CreateElement("showFileList")
									tmpNode.InnerText = ShowFileListToolStripMenuItem.Checked.ToString
									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("fileListPaneWidth")
									tmpNode.InnerText = SplitContainerContents.SplitterDistance
									envNode.appendChild(tmpNode)
						
									tmpNode = xcfgdoc.CreateElement("showFileInfo")
									tmpNode.InnerText = showInfoToolStripMenuItem.Checked.ToString
									envNode.appendChild(tmpNode)
						
									tmpNode = xcfgdoc.CreateElement("showComments")
									tmpNode.InnerText = ShowCommentsToolStripMenuItem.Checked.ToString
									envNode.appendChild(tmpNode)

										tmpNode = xcfgdoc.CreateElement("browserWidth")
										tmpNode.InnerText = WebBrowser1.Width
										attr  = xcfgdoc.CreateAttribute("exclude-scrollbar")
										attr.value = excludeScrollbarToolStripMenuItem.Checked.ToString()
										tmpNode.attributes.append(attr)
										envNode.appendChild(tmpNode)
										attr  = xcfgdoc.CreateAttribute("show-size")
										attr.value = showBrowserWidthToolStripMenuItem.Checked.ToString()
										tmpNode.attributes.append(attr)
										
										tmpNode = xcfgdoc.CreateElement("browserSizePreset1")
										tmpNode.InnerText = toolStripMenuItemSize1.Text
										envNode.appendChild(tmpNode)
										tmpNode = xcfgdoc.CreateElement("browserSizePreset2")
										tmpNode.InnerText = toolStripMenuItemSize2.Text
										envNode.appendChild(tmpNode)
										tmpNode = xcfgdoc.CreateElement("browserSizePreset3")
										tmpNode.InnerText = toolStripMenuItemSize3.Text										
										envNode.appendChild(tmpNode)
										tmpNode = xcfgdoc.CreateElement("browserSizePreset4")
										tmpNode.InnerText = toolStripMenuItemSize4.Text										
										envNode.appendChild(tmpNode)
																				
										
									tmpNode = xcfgdoc.CreateElement("highlightLocalLinks")
									tmpNode.InnerText = highlightLocalLinksToolStripMenuItem.Checked.ToString
									envNode.appendChild(tmpNode)
									
'									tmpNode = xcfgdoc.CreateElement("highlightTags")
'									tmpNode.InnerText = highlightUITermsToolStripMenuItem.Checked.ToString
'									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("fitImages")
									tmpNode.InnerText = fitImagesToolStripMenuItem1.Checked.ToString
									envNode.appendChild(tmpNode)
						
									tmpNode = xcfgdoc.CreateElement("imageBackGroundColor")
									tmpNode.InnerText = colortranslator.ToHtml(imageBackGroundColor)
									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("autoRefreshAfterEdit")
									tmpNode.InnerText = autoRefreshAfterEditToolStripMenuItem.Checked.ToString
									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("useMouseGestures")
									tmpNode.InnerText = useMouseGesturesToolStripMenuItem.Checked.ToString
									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("followLinks")
									tmpNode.InnerText = followLinksToolStripMenuItem.Checked.ToString
									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("autoScroll")
									tmpNode.InnerText = autoScrollToolStripMenuItem.Checked.ToString
									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("defaultFilter")
									tmpNode.InnerText = defaultFilter
									envNode.appendChild(tmpNode)

									tmpNode = xcfgdoc.CreateElement("defaultSourceFolder")
									tmpNode.InnerText = defaultSourceFolder
									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("editor")
									tmpNode.InnerText = defaultEditor
									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("imageEditor")
									tmpNode.InnerText = imageEditor
									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("compareTool")
									If not fileCompare Is Nothing then tmpNode.InnerText = fileCompare
									envNode.appendChild(tmpNode)
									
									tmpNode = xcfgdoc.CreateElement("plugins")
									envNode.appendChild(tmpNode)
									If settingsOnly And contextMenuStripHighlightPlugins.Items.Count > 0 Then
										Dim pluginNode As XmlNode = envNode.SelectSingleNode("plugins")
										For Each item As ToolStripMenuItem In contextMenuStripHighlightPlugins.Items
											If item.Checked Then
												tmpNode = xcfgdoc.CreateElement("name")
												tmpNode.InnerText = item.Text
												pluginNode.appendChild(tmpNode)
											End If
										Next
										pluginNode = Nothing
									End If
									
								
								If Not settingsOnly Then
									Dim name As String = "fileList"
									Dim xmlData As String = "<" & name & "></" & name & ">"
									Dim xmlSnippet as XmlDocument = new XmlDocument()
									xmlSnippet.Load(New StringReader(xmlData))
									
									For row As Integer = 0 to filelist.GetUpperBound(0) - 1
										Dim record As XmlElement = xmlSnippet.CreateElement("file")
										attr = xmlSnippet.CreateAttribute("id")
										attr.value = row + 1
										record.attributes.append(attr)
										xmlSnippet.DocumentElement.AppendChild(record)
										
										For cell As Integer = 0 to filelist.GetUpperBound(1) - 1
											Dim field As XmlElement = xmlSnippet.CreateElement("field" & cell)
											field.InnerText = filelist.GetValue(row, cell)
											record.AppendChild(field)
										Next
									Next
									
									tmpNode = xcfgdoc.ImportNode(xmlSnippet.DocumentElement, True)
									appNode.AppendChild(tmpNode)
								End If
											
								xcfgdoc.Save(xcfgFile)
								tmpNode = Nothing
								
								attr = Nothing
								
							End If
								
							Me.Text = Path.GetFileNameWithoutExtension(xcfgFile)
							
							appNode = Nothing
							root = Nothing
							xcfgdoc = Nothing
							
						 Catch ex As Exception
							Msgbox("Problem saving settings. Check file or try to save as a new file")
						 End Try
						End Sub
						
						Dim private setSize As Integer = 0
						
						Sub readXcfg(xcfgFile As String, Optional settingsOnly As Boolean = False)
							
							Try
								
								Dim xcfgdoc As XPathDocument
								Dim xcfgNav As XPathNavigator
								xcfgdoc = New XPathDocument(xcfgFile)
								xcfgNav = xcfgdoc.CreateNavigator()
								Dim xmlNode As XPathNodeIterator
								
								if Not settingsOnly then
									
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/source") ' ("//job/task[name='" & taskName & "']/source")
									If xmlNode.MoveNext()  Then
										' xmlNode.Current.Name & " : " & xmlNode.Current.Value
										sourceFolder = Trim(xmlNode.Current.Value)
										If Not sourceFolder = Nothing Then
											If Mid(sourceFolder, len(sourceFolder),1) = "\" AND len(sourceFolder) > 3 then sourceFolder = Mid(sourceFolder, 1, len(sourceFolder)-1)
										End If
										
										If UCase(xmlNode.Current.GetAttribute("sub-folders",String.Empty)) = "TRUE" then subFolders = True
										If UCase(xmlNode.Current.GetAttribute("sub-folders",String.Empty)) = "FALSE" then subFolders = False
									Else
										' nothing to do, keep defaults
									End If
									
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/target")
									If xmlNode.MoveNext()
										' xmlNode.Current.Name & " : " & xmlNode.Current.Value
										targetFolder = Trim(xmlNode.Current.Value)
										If Not targetFolder = Nothing Then
											If Mid(targetFolder, len(targetFolder),1) = "\" AND len(targetFolder) > 3 then targetFolder = Mid(targetFolder, 1, len(targetFolder)-1)
										End if
									Else
										' nothing to do, keep defaults
									End If
								
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/filter")
									If xmlNode.MoveNext()
										' xmlNode.Current.Name & " : " & xmlNode.Current.Value
										filter =  xmlNode.Current.Value
									Else
										' nothing to do, keep defaults
									End If
								
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/includelist")
									If xmlNode.MoveNext()
										' xmlNode.Current.Name & " : " & xmlNode.Current.Value
										includelist =  xmlNode.Current.Value
									Else
										' nothing to do, keep defaults
									End If
								
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/ignorelist")
									If xmlNode.MoveNext()
										' xmlNode.Current.Name & " : " & xmlNode.Current.Value
										ignorelist =  xmlNode.Current.Value
									Else
										' nothing to do, keep defaults
									End If

									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/highlightSpanTags")
									If xmlNode.MoveNext()
										' xmlNode.Current.Name & " : " & xmlNode.Current.Value
										highlightClassNames =  splitLine(xmlNode.Current.Value)
									Else
										' nothing to do, keep defaults
									End If

									' read file list
									Dim row As XPathNodeIterator = xcfgNav.Select("//job/task[@tool='" & appName & "']/fileList/file")
									If row.Count > 0 then
										ReDim filelist(row.Count, 5)
										While row.MoveNext
											Dim i as Integer = 0
											Dim cell As XPathNodeIterator = row.Current.SelectDescendants(XPathNodeType.Element, False)
											Dim arrayId as Integer = CInt(row.Current.GetAttribute("id",""))-1
											'Loop through the child nodes
											While cell.MoveNext
												' MsgBox(cell.Current.Name & " : " & cell.Current.Value)
												fileList(arrayId,i) = cell.Current.Value
												i = i + 1
											End While
										End While
										
										fileListRead = True
									End If

								End If
				
				
								' read environment
								If Not settingsOnly Then ' project specific settings
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/showFileList")
									If xmlNode.MoveNext()
										ShowFileListToolStripMenuItem.Checked = convertBool(xmlNode.Current.Value)
										SplitContainerContents.Panel1Collapsed = Not ShowFileListToolStripMenuItem.Checked
									End If

									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/showComments")
									If xmlNode.MoveNext()
										ShowCommentsToolStripMenuItem.Checked = convertBool(xmlNode.Current.Value)
										panelComments.Visible = ShowCommentsToolStripMenuItem.Checked
									End If
								End If
'
								xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/fileListPaneWidth")
								If xmlNode.MoveNext()
									SplitContainerContents.SplitterDistance = CInt(xmlNode.Current.Value)
								End If
								
								xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/showFileInfo")
								If xmlNode.MoveNext()
									showInfoToolStripMenuItem.Checked = convertBool(xmlNode.Current.Value)
									panel1Info.Visible = showInfoToolStripMenuItem.Checked
									panel2Info.Visible = showInfoToolStripMenuItem.Checked
									panelSourceInfo.Visible = showInfoToolStripMenuItem.Checked
									panelTargetInfo.Visible = showInfoToolStripMenuItem.Checked
								End If
								
								Try
								xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/browserWidth")
								If xmlNode.MoveNext()
									If UCase(xmlNode.Current.GetAttribute("exclude-scrollbar",String.Empty)) = "TRUE" Then
										excludeScrollbarToolStripMenuItem.Checked = True
									Else
										excludeScrollbarToolStripMenuItem.Checked = False
									End If
									If Cint(xmlNode.Current.Value) > 0 Then setSize = Cint(xmlNode.Current.Value) - scrollbarcorrection
									If UCase(xmlNode.Current.GetAttribute("show-size",String.Empty)) = "TRUE" Then
										showBrowserWidthToolStripMenuItem.Checked = True
									End If
								End If
								
								' read presets
								xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/browserSizePreset1")
								If xmlNode.MoveNext()
									toolStripMenuItemSize1.Text = xmlNode.Current.Value.Trim()
								End If								
								xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/browserSizePreset2")
								If xmlNode.MoveNext()
									toolStripMenuItemSize2.Text = xmlNode.Current.Value.Trim()
								End If	
								xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/browserSizePreset3")
								If xmlNode.MoveNext()
									toolStripMenuItemSize3.Text = xmlNode.Current.Value.Trim()
								End If									
								xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/browserSizePreset4")
								If xmlNode.MoveNext()
									toolStripMenuItemSize4.Text = xmlNode.Current.Value.Trim()
								End If									
								
								Catch x As Exception
								End Try
								
								' additional settings (added Try-Catch for backward compatibility)
								Try
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/highlightLocalLinks")
									If xmlNode.MoveNext()
										highlightLocalLinksToolStripMenuItem.Checked = convertBool(xmlNode.Current.Value)
									End If
'									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/highlightTags")
'									If xmlNode.MoveNext()
'										highlightUITermsToolStripMenuItem.Checked = convertBool(xmlNode.Current.Value)
'									End If
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/fitImages")
									If xmlNode.MoveNext()
										fitImagesToolStripMenuItem1.Checked = convertBool(xmlNode.Current.Value)
									End If
									
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/imageBackGroundColor")
									If xmlNode.MoveNext()
										imageBackGroundColor = ColorTranslator.FromHtml(xmlNode.Current.Value)
									End If
									
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/autoRefreshAfterEdit")
									If xmlNode.MoveNext()
										autoRefreshAfterEditToolStripMenuItem.Checked = convertBool(xmlNode.Current.Value)
									End If
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/useMouseGestures")
									If xmlNode.MoveNext()
										useMouseGesturesToolStripMenuItem.Checked = convertBool(xmlNode.Current.Value)
									End If
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/followLinks")
									If xmlNode.MoveNext()
										followLinksToolStripMenuItem.Checked = convertBool(xmlNode.Current.Value)
									End If
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/autoScroll")
									If xmlNode.MoveNext()
										autoScrollToolStripMenuItem.Checked = convertBool(xmlNode.Current.Value)
										_autoScroll = True
										scrollPanel.Visible = AutoScrollToolStripMenuItem.Checked
										t.Enabled = False
										btnToggleScroll.Image = imageList1.Images.Item(0)
									End If

									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/defaultFilter")
									If xmlNode.MoveNext()
										defaultFilter =  xmlNode.Current.Value
									End If

									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/defaultSourceFolder")
									If xmlNode.MoveNext()
										defaultSourceFolder =  xmlNode.Current.Value
									End If

									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/editor")
									If xmlNode.MoveNext()
										If xmlNode.Current.Value.Trim().Length > 0 Then defaultEditor = xmlNode.Current.Value
									End If
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/imageEditor")
									If xmlNode.MoveNext()
										If xmlNode.Current.Value.Trim().Length > 0 Then imageEditor = xmlNode.Current.Value
									End If
									xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/compareTool")
									If xmlNode.MoveNext()
										If xmlNode.Current.Value.Trim().Length > 0 Then fileCompare = xmlNode.Current.Value
									End If
									
									If settingsOnly And contextMenuStripHighlightPlugins.Items.Count > 0 Then
										xmlNode = xcfgNav.Select("//job/task[@tool='" & appName & "']/environment/plugins/name")
										While xmlNode.MoveNext()
											For Each item As ToolStripMenuItem In contextMenuStripHighlightPlugins.Items
												If xmlNode.Current.Value = item.Text Then
													item.Checked = True
													Exit For
												End If
											Next
										End While
									End If
									
								Catch ex As Exception
								End Try
								
								xmlNode = Nothing
								
							Catch e As Exception
								' problem with xcfg file
								Messagebox.Show("Problem reading local configuration file.")
							End Try
		
						End Sub
	
	
	' support functions
	Function convertBool(ByVal stringIn As String) As Boolean
		
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
	#End Region
	
	
	
	
	Sub OpenProjectToolStripMenuItemClick(sender As Object, e As System.EventArgs)
		usingDefaultFilter = False
		Dim openFileDialog1 As New OpenFileDialog()
		
		openFileDialog1.Title = "Open project"
		If Directory.Exists(appFolder) Then _
				openFileDialog1.InitialDirectory = appFolder
			openFileDialog1.Filter = "project files (*.xcfg)|*.xcfg|All files (*.*)|*.*"
			openFileDialog1.FilterIndex = 1
			openFileDialog1.RestoreDirectory = true
			
			If openFileDialog1.ShowDialog() = dResult_OK Then ' DialogResult.OK
				
				' read settings
				readXcfg(openFileDialog1.FileName)
				
				projectFile = openFileDialog1.FileName
				projectName = Path.GetFileNameWithoutExtension(projectFile)
				saveStatus = True
				
				SaveProjectToolStripMenuItem.Text = "Save project as..."
				SaveProjectToolStripMenuItem1.Visible = True
				
				' activate the new settings
				openProject()
				
			End If
			openFileDialog1 =Nothing
		End Sub
		
		Sub SaveProjectToolStripMenuItemClick(sender As Object, e As System.EventArgs)
			Dim saveFileDialog1 As New SaveFileDialog()
			
			saveFileDialog1.Title = "Save project"
			If Directory.Exists(appFolder) Then _
				saveFileDialog1.InitialDirectory = appFolder
				
				If Not len(projectFile) = 0 Then
					saveFileDialog1.Title = "Save project as"
					saveFileDialog1.InitialDirectory = Path.GetDirectoryName(projectFile)
					saveFileDialog1.FileName = path.GetFileName(projectFile)
				End If
		
				saveFileDialog1.Filter = "project files (*.xcfg)|*.xcfg|All files (*.*)|*.*"
				saveFileDialog1.OverwritePrompt = True
				saveFileDialog1.FilterIndex = 1
				saveFileDialog1.RestoreDirectory = true
				
				If saveFileDialog1.ShowDialog() = dResult_OK Then ' DialogResult.OK
					
					' write settings
					writeXcfg(saveFileDialog1.FileName)
					saveStatus = True
					projectFile = saveFileDialog1.FileName
					projectName = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)
					SaveProjectToolStripMenuItem.Text = "Save project as..."
					SaveProjectToolStripMenuItem1.Visible = True
					
				End If
				saveFileDialog1 =Nothing
				
				
			End Sub
			
			Sub ShowFileListToolStripMenuItemClick(sender As Object, e As System.EventArgs)
				If ShowFileListToolStripMenuItem.Checked Then
					
					SplitContainerContents.Panel1Collapsed = True
					ShowFileListToolStripMenuItem.Checked = False
				Else
		
					SplitContainerContents.Panel1Collapsed = False
					ShowFileListToolStripMenuItem.Checked = True
				End If
				
			End Sub
			
			
			Sub fillFileListTree()
				'		' add some basic info
				'		Dim newNode As New TreeNode
				'	    newNode.Text = "project1"
				'	   	TreeView1.Nodes.Add(newNode)
				'    	' newNode.Nodes.Add("file list")
				'
				'		Dim newNode As TreeNode = New TreeNode("Text for new node")
				'		TreeView1.SelectedNode.Nodes.Add(newNode)
				
				Treeview1.BeginUpdate()
				
				' now lets fill the project tree
				TreeView1.Nodes.Clear()
				
				Dim new_fields() As String
				For i As Integer = 0 To fileList.GetUpperBound(0) - 1
					
					' determine if file should be displayed based on filter settings
					If checkFilter(i) then
						' split folders
						If not len(fileList(i,0)) = 0
							new_fields = Replace(fileList(i,0),sourceFolder & "\","").Trim.Split("\"c)
						Else
							new_fields = Replace(fileList(i,1),targetFolder & "\","").Trim.Split("\"c)
						End If
						' create branch
						MakeTreeViewPath(TreeView1.Nodes, new_fields, 0)
					End if
				Next i
				
				If TreeView1.Nodes.Count = 0 Then
					Dim new_node As TreeNode = _
					TreeView1.Nodes.Add("No Files Found, Right-click to change filter")
					new_node.EnsureVisible()
					new_node.ForeColor = Color.DarkGray
					new_node.ContextMenuStrip = contextMenuStripTreeFilter ' Filter
					
				End If
				
				Treeview1.EndUpdate()
				
				TreeView1.ExpandAll()
				
			End Sub
	


	
	
		Private Sub MakeTreeViewPath(ByVal parent_nodes As _
				TreeNodeCollection, ByVal fields() As String, ByVal _
				field_num As Integer)
			' Do nothing if we've used all of the fields.
			If field_num > fields.GetUpperBound(0) Then Exit Sub
			
			' Search the parent_nodes for a child with this
			' field as its name.
			Dim found_field As Boolean
			For Each child_node As TreeNode In parent_nodes
				If child_node.Text = fields(field_num) Then
					' We found this field. Follow it.
					MakeTreeViewPath(child_node.Nodes, fields, _
						field_num + 1)
					found_field = True
				End If
			Next child_node
			
			' See if we found the field.
			If Not found_field Then
				' We didn't find the field. Make it here.
				Dim new_node As TreeNode = _
					parent_nodes.Add(fields(field_num))
				new_node.EnsureVisible()
				
				
				' do some formatting
				If Directory.Exists(sourceFolder & "\" & new_node.FullPath) Then ' folder
					new_node.ForeColor = Color.Black
					new_node.ContextMenuStrip = contextMenuStripTreeFilter ' Filter
				Else ' file
					Select Ucase(getFileStatus(sourceFolder & "\" & new_node.FullPath))
				Case "OK"
					new_node.ForeColor = Color.Black
					new_node.ImageIndex = 12
				Case "NG"
					new_node.ForeColor = Color.Red
					new_node.ImageIndex = 11
				Case "FLAGGED"
					new_node.ForeColor = Color.SeaGreen
					new_node.ImageIndex = 10
				Case "ERROR", "PROBLEM", "ATTN"
					new_node.ForeColor = Color.Red
					new_node.ImageIndex = 4
				Case "MISSING"
					new_node.ForeColor = Color.Red
					new_node.ImageIndex = 6
				Case "CHECKED"
					new_node.ForeColor = Color.DarkGray
					new_node.ImageIndex = 2
				Case "WARNING"
					new_node.ForeColor = Color.Orange
					new_node.ImageIndex = 2
				Case "ORPHAN"
					new_node.ForeColor = Color.CornFlowerBlue
					new_node.ImageIndex = 7
				Case "IDENTICAL"
					new_node.ForeColor = Color.Black
					new_node.ImageIndex = 5
				Case "UNCHECKED"
					new_node.ForeColor = Color.DarkGray
					new_node.ImageIndex = 3
				Case Else
					new_node.ForeColor = Color.DarkGray
					new_node.ImageIndex = 2
			End Select
					
			new_node.ContextMenuStrip = contextMenuStripFileItem ' Status
		
		End If
		' Make the rest of the path.
		MakeTreeViewPath(new_node.Nodes, fields, field_num _
			+ 1)
	End If
End Sub


Function getFileStatus(file As String) As String
	getFileStatus = "unknown"
	_commented = False
	For i As Integer = 0 To filelist.GetLength(0) - 1
		If lcase(filelist(i,0)) = lcase(file) Then
			if Trim(len(filelist(i,3))) > 0 then _commented = True
			getFileStatus = filelist(i,2)
			Exit For
			Else If lcase(filelist(i,1)) = lcase(file) Then
			if Trim(len(filelist(i,3))) > 0 then _commented = True
			getFileStatus = filelist(i,2)
			Exit For
		End If
	Next
End Function

Dim Private _commented As Boolean = False

Sub updateTreeViewFileStatus(file As String, optional treeNode as TreeNodeCollection = Nothing)
	If treeNode Is Nothing then treeNode = TreeView1.Nodes
	Dim currentNode As TreeNode
	For Each currentNode In treeNode
		
		If currentNode.GetNodeCount(False) > 0 Then _
				updateTreeViewFileStatus(file, currentNode.Nodes)
			
			If lcase(sourceFolder & "\" & currentNode.fullPath) = lcase(file) Then
				Select Ucase(getFileStatus(sourceFolder & "\" & currentNode.fullPath))
			Case "OK"
				currentNode.ForeColor = Color.Black
				currentNode.ImageIndex = 12
			Case "NG"
				currentNode.ForeColor = Color.Red
				currentNode.ImageIndex = 11
			Case "FLAGGED"
				currentNode.ForeColor = Color.SeaGreen
				currentNode.ImageIndex = 10
			Case "CHECKED"
				currentNode.ForeColor = Color.Black
				currentNode.ImageIndex = 2
			Case "UNCHECKED"
				currentNode.ForeColor = Color.DarkGray
				currentNode.ImageIndex = 3
			Case "ERROR", "PROBLEM", "ATTN"
				currentNode.ForeColor = Color.Red
				currentNode.ImageIndex = 4
			Case "MISSING"
				currentNode.ForeColor = Color.Red
				currentNode.ImageIndex = 6
			Case "WARNING"
				currentNode.ForeColor = Color.Orange
				currentNode.ImageIndex = 4
			Case "ORPHAN"
				currentNode.ForeColor = Color.CornFlowerBlue
				currentNode.ImageIndex = 7
			Case "IDENTICAL"
				currentNode.ForeColor = Color.Black
				currentNode.ImageIndex = 5
			Case Else
				currentNode.ForeColor = Color.Black
				currentNode.ImageIndex = 2
		End Select
			
		' also set selected image
		currentNode.SelectedImageIndex = currentNode.ImageIndex
		
	End If
Next
End Sub

Sub activateCurrentNode(file As String, Optional treeNode As TreeNodeCollection = Nothing)
	If treeNode Is Nothing then treeNode = TreeView1.Nodes
	Dim currentNode As TreeNode
	For Each currentNode In treeNode
		
		If currentNode.GetNodeCount(False) > 0 Then _
				activateCurrentNode(file, currentNode.Nodes)
			
			if file.StartsWith("file://") then file = normalizeUrlPath(file)
			
			If lcase(sourceFolder & "\" & currentNode.fullPath) = lcase(file) Then _
					treeView1.SelectedNode = currentNode
	Next
End Sub

Function normalizeUrlPath(urlIn As String) As String
	System.Diagnostics.Debug.WriteLine(Replace(regex.Match(System.Web.HttpUtility.UrlDecode(urlIn),"(?<=file:///)[A-Za-z]:.+$").Value,"/","\"))
	return Replace(regex.Match(System.Web.HttpUtility.UrlDecode(urlIn),"(?<=file:///)[A-Za-z]:.+$").Value,"/","\") ' (?<=file://[^/]+/)[A-Za-z]:.+$"
End Function
		
		Sub updateStatus()
			If fileList(activeFile,2) = "unchecked" And activeFile > -1 Then
				fileList(activeFile,2) = "checked"
				updateTreeViewFileStatus(fileList(activeFile,0))
			End If
		End Sub
		
		Sub TreeView1NodeMouseDoubleClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
			nodeClicked = True
			if autoUpdateCheckStatus then updateStatus()
			
			If file.Exists(sourceFolder & "\" & e.Node.FullPath) then showFile(sourceFolder & "\" & e.Node.FullPath)
			' If file.Exists(sourceFolder & "\" & e.Node.FullPath) Then webbrowser1.Url = New  Uri(sourceFolder & "\" & e.Node.FullPath )
			' If file.Exists(targetFolder & "\" & e.Node.FullPath) then webbrowser2.Url = New  Uri(targetFolder & "\" & e.Node.FullPath )
			nodeClicked = False
		End Sub
		
		
		
		Sub LabelStatusTextChanged(sender As Object, e As System.EventArgs)
			fileList(activeFile,2) = labelStatus.Text
			Select ucase(labelStatus.Text)
		Case "OK"
			labelStatus.BackColor = Color.lightGreen
			labelStatus.ForeColor = Color.Green
		Case "UNCHECKED"
			labelStatus.BackColor = Color.White
			labelStatus.ForeColor = Color.DarkGray
		Case "CHECKED"
			labelStatus.BackColor = Color.White
			labelStatus.ForeColor = Color.Black
		Case "ERROR", "PROBLEM", "MISSING", "NG"
			labelStatus.BackColor = Color.LightPink
			labelStatus.ForeColor = Color.Red
		Case "WARNING"
			labelStatus.BackColor = Color.White
			labelStatus.ForeColor = Color.Orange
		Case "ORPHAN"
			labelStatus.BackColor = Color.White
			labelStatus.ForeColor = Color.CornFlowerBlue
		Case "IDENTICAL"
			labelStatus.BackColor = Color.White
			labelStatus.ForeColor = Color.Black
		Case "FLAGGED"
			labelStatus.BackColor = Color.White
			labelStatus.ForeColor = Color.Black
		Case Else
			labelStatus.BackColor = Color.White
			labelStatus.ForeColor = Color.Black
	End Select
	updateTreeViewFileStatus(fileList(activeFile,0))
	panel3.Focus()
End Sub


Sub LabelStatusMouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs)
	' quick status switch
	If Not e.Button = Windows.Forms.MouseButtons.Right Then
	
		' cycle through basic status options
		Select labelStatus.Text
		Case "OK"
			labelStatus.Text = "NG"
		Case "NG"
'			labelStatus.Text = "unchecked" ' clear status
'		Case "unchecked"
			labelStatus.Text = "checked"
		Case Else
			labelStatus.Text = "OK"
		End Select
	
	Else
	
		' show context menu to change status
		ContextMenuStripStatus.RenderMode = ToolStripRenderMode.System
		For Each item As toolStripMenuItem In ContextMenuStripStatus.Items
			If lcase(item.Text) = lcase(labelStatus.Text) Then
				item.Checked = True
			Else
				item.Checked = False
			End If
		Next
		ContextMenuStripStatus.Show(LabelStatus, e.Location)
	
	End If
End Sub

Sub ContextMenuStripStatusItemClicked(sender As Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs)
	fileList(activeFile,2) = e.ClickedItem.Text
	labelStatus.Text = fileList(activeFile,2)
End Sub


Sub AutoUpdateStatusToolStripMenuItemClick(sender As Object, e As System.EventArgs)
	If AutoUpdateStatusToolStripMenuItem.Checked Then
		autoUpdateCheckStatus = False
		AutoUpdateStatusToolStripMenuItem.Checked = False
	Else
		autoUpdateCheckStatus = True
		AutoUpdateStatusToolStripMenuItem.Checked = True
	End If
End Sub

Sub OpenFilesInCompareToolToolStripMenuItemClick(sender As Object, e As System.EventArgs)
	If fileCompare  = Nothing Then
		Messagebox.Show("No compare tool selected.")
	Else
		Try
		If fileCompare.Contains("%") Then
			Dim _app As String = fileCompare.Substring(0, fileCompare.IndexOf(".exe") + 4)
			Dim _args As String = fileCompare.Substring(fileCompare.IndexOf(".exe") + 4).Trim()
			_args = _args.Replace("%source", sourceFile)
			_args = _args.Replace("%target", targetFile)
			System.Diagnostics.Debug.WriteLine(_app & VbCrLf & VbCrLf & _args)
			Process.Start(_app,_args)
		Else
			Process.Start(fileCompare, """" & sourceFile & """ """ & targetFile & """")
		End If
		Catch ex As Exception
			Messagebox.Show("Check compare tool settings.")
		End Try
	End If
End Sub

Sub OpenFoldersInCompareToolToolStripMenuItemClick(sender As Object, e As System.EventArgs)
	If fileCompare  = Nothing Then
		Messagebox.Show("No compare tool selected")
	Else
		Process.Start(fileCompare, """" & sourceFolder & """ """ & targetFolder & """")
	End If
End Sub

Sub PreferedCompareToolToolStripMenuItemClick(sender As Object, e As System.EventArgs)
	Dim result As String = InputBox("Enter the path for the default Compare tool", "Compare tool", fileCompare)
	If result Is "" Then
		' nothing to do
	Else
		If file.Exists(result) then fileCompare = result
	End If
End Sub




Sub ParseXMLOnLoadToolStripMenuItemClick(sender As Object, e As System.EventArgs)
	If parseXMLOnLoadToolStripMenuItem.Checked Then
		ParseXMLOnLoadToolStripMenuItem.Checked = False
		parseXMLonLoad = False
	Else
		ParseXMLOnLoadToolStripMenuItem.Checked = True
		parseXMLonLoad = True
	End If
End Sub




Sub TabControl1SizeChanged(sender As Object, e As System.EventArgs)
	tabControl1.Refresh()
End Sub




Sub DockToTopToolStripMenuItemClick(sender As Object, e As System.EventArgs)
	If splitContainerContents.Orientation = System.Windows.Forms.Orientation.Horizontal Then
		splitContainerContents.Orientation = System.Windows.Forms.Orientation.Vertical
		DockToTopToolStripMenuItem.Text = "Dock to top"
		tabControl1.Refresh()
	Else
		splitContainerContents.Orientation = System.Windows.Forms.Orientation.Horizontal
		DockToTopToolStripMenuItem.Text = "Dock to left"
		tabControl1.Refresh()
	End If
End Sub

Dim nodeClicked As Boolean = False
Sub TreeView1NodeMouseClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
	If autoUpdateCheckStatus Then updateStatus()
	nodeClicked = True
	system.Diagnostics.debug.WriteLine(sourceFolder & "\" & e.Node.FullPath)
	If file.Exists(sourceFolder & "\" & e.Node.FullPath) Then _
			showFile(sourceFolder & "\" & e.Node.FullPath)
		' MsgBox(targetFolder & "\" & e.Node.FullPath)
		If Not file.Exists(sourceFolder & "\" & e.Node.FullPath) And file.Exists(targetFolder & "\" & e.Node.FullPath) then _
				showFile(targetFolder & "\" & e.Node.FullPath)
			' If file.Exists(sourceFolder & "\" & e.Node.FullPath) Then webbrowser1.Url = New  Uri(sourceFolder & "\" & e.Node.FullPath )
			' If file.Exists
	nodeClicked = False
End Sub
		
		Sub LabelEncoding1Click(sender As Object, e As System.EventArgs)
			contextMenuStripEncoding.Show(sender, sender.PointToClient(Control.MousePosition))
		End Sub
		
		Sub LabelEncoding2Click(sender As Object, e As System.EventArgs)
			contextMenuStripEncoding.Show(sender, sender.PointToClient(Control.MousePosition))
		End Sub
		
		Const LEFTMOUSEBUTTON As System.Windows.Forms.MouseButtons = MouseButtons.Left
		Private startPoint As Point
		
		Private Sub PictureBox1MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
			If e.Button = LEFTMOUSEBUTTON Then
				startPoint = PictureBox1.PointToScreen(New Point(e.X, e.Y))
			End If
		End Sub
		
		Private Sub PictureBox1MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
			If e.Button =  LEFTMOUSEBUTTON Then
				Dim currentPoint As Point = PictureBox1.PointToScreen(New Point(e.X, e.Y))
				PictureBox1.Location = New Point(PictureBox1.Location.X - (startPoint.X - currentPoint.X), PictureBox1.Location.Y - (startPoint.Y - currentPoint.Y))
				If not Control.ModifierKeys = Keys.Control Then PictureBox2.Location = New Point(PictureBox2.Location.X - (startPoint.X - currentPoint.X), PictureBox2.Location.Y - (startPoint.Y - currentPoint.Y))
				startPoint = currentPoint
			End If
		End Sub
		
		Private Sub PictureBox2MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
			If e.Button = LEFTMOUSEBUTTON Then
				startPoint = PictureBox2.PointToScreen(New Point(e.X, e.Y))
			End If
		End Sub
		
		Private Sub PictureBox2MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
			If e.Button =  LEFTMOUSEBUTTON Then
				Dim currentPoint As Point = PictureBox2.PointToScreen(New Point(e.X, e.Y))
				PictureBox2.Location = New Point(PictureBox2.Location.X - (startPoint.X - currentPoint.X), PictureBox2.Location.Y - (startPoint.Y - currentPoint.Y))
				If not Control.ModifierKeys = Keys.Alt Then PictureBox1.Location = New Point(PictureBox1.Location.X - (startPoint.X - currentPoint.X), PictureBox1.Location.Y - (startPoint.Y - currentPoint.Y))
				startPoint = currentPoint
			End If
		End Sub
		
		
		Dim stepValue As Integer = 5
		
		Sub movePicture(xShift As Integer, yShift As Integer, optional picID as Integer = 0)
			
			If IsKeyDown(Keys.ShiftKey) Then
				stepValue = 25
			Else
				stepValue = 5
			End If
			
			If picID = 1 Or picID = 0 then
				PictureBox1.Location = New Point(PictureBox1.Location.X + (xShift * stepValue), PictureBox1.Location.Y + (yShift * stepValue))
			End If
			If picID = 2 Or picID = 0 then
				PictureBox2.Location = New Point(PictureBox2.Location.X + (xShift * stepValue), PictureBox2.Location.Y + (yShift * stepValue))
			End If
			
		End Sub
		
		
		Sub UseMouseGesturesToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
			useMouseGestures = UseMouseGesturesToolStripMenuItem.Checked
		End Sub
		
		Private Sub TextBoxJump_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
			If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
				Try ' in case of empty string or wrong input
					If textBoxJump.Text > 0 AND textBoxJump.Text <= fileList.GetUpperBound(0) then
						updateTreeViewInteractive = True
						showFile(Cint(textBoxJump.Text - 1))
						updateTreeViewInteractive = False
						labelCounter.Visible = True
						textboxJump.Visible = False
						e.Handled = True
					Else
						textboxJump.ForeColor = Color.red
					End If
				Catch ex As Exception
					textboxJump.ForeColor = Color.red
				End Try
			End If
		End Sub
		
		
		Sub TextBoxJumpLeave(ByVal sender As Object, ByVal e As EventArgs)
			labelCounter.Visible = True
			textboxJump.Visible = False
			textBoxJump.Text = activeFile + 1
		End Sub
		
		Sub TextBoxJumpTextChanged(ByVal sender As Object, ByVal e As EventArgs)
			Try ' in case of empty string
				If textBoxJump.Text > 0 AND textBoxJump.Text <= fileList.GetUpperBound(0) then
					textboxJump.ForeColor = Color.Black
				Else
					textboxJump.ForeColor = Color.Red
				End If
			Catch ex As Exception
				textboxJump.ForeColor = Color.Red
			End Try
		End Sub
		
		Sub LabelCounterDoubleClick(ByVal sender As Object, ByVal e As EventArgs)
			textBoxJump.Text = activeFile + 1
			textBoxJump.SelectAll
			labelCounter.Visible = False
			textboxJump.ForeColor = Color.Black
			textBoxJump.Visible = True
			textboxJump.Focus
			textBoxJump.SelectAll
		End Sub
		
		
		' imaging functions
		
		Dim zoom As Integer = 100
		Dim rotation As Integer
		
		Public Function ResizeImage(ByVal imageFile As Bitmap, ByVal Width As Integer, Optional ByVal Height As Integer = -1, Optional ByVal Scale As Boolean = False, Optional Antialias As Boolean = True) As System.Drawing.Bitmap
			
			If Height = -1 And width = 100 Then ' nothing to do
				zoom = 100
				Return imageFile
			Else ' Resize the image
				
				Dim newSize As Size
				Dim originalImage As Bitmap = imageFile ' Dim originalImage As New Bitmap(imageFile)
				
				If Height = -1 Then	' width is a percentage
					zoom = Width
					Scale = False ' no need to calculate size
					height = (originalImage.Height/100) * width
					width = (originalImage.Width/100) * width
				End If
				
				
				If Scale Then ' scale the image to fit the given size
					
					Dim originalWidth As Integer = originalImage.Width
					Dim originalHeight As Integer = originalImage.Height
					Dim scaledWidth As Integer = originalWidth
					Dim scaledHeight As Integer = originalHeight
					
					Do while scaledWidth > Width Or scaledHeight > Height
						If scaledWidth > Width Then
							scaledHeight = Math.Round((Width/scaledWidth)*scaledHeight)
							scaledWidth = Width
						End If
						If scaledHeight > Height Then
							scaledWidth = Math.Round((Height/scaledHeight)*scaledWidth)
							scaledHeight = Height
						End If
					Loop
					
					newSize = New Size(scaledWidth, scaledHeight)
					' zoom = (scaledWidth/originalWidth) * 100
					
				Else ' stretch the image to fit the given size
					
					newSize = New Size(Width, Height)
					' zoom = 100
					
				End If
				
				
				Dim resizedImage As New Bitmap(originalImage, newSize)
				
				If antialias Then
					Dim tempBitmap As New Bitmap(newSize.Width, newSize.Height)
					Dim graphicsObject As Graphics = Graphics.FromImage(tempBitmap)
					graphicsObject.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic ' .HighQualityBilinear .NearestNeighbour
					graphicsObject.DrawImage(originalImage, 0, 0, newSize.Width, newSize.Height)
					resizedImage = tempBitmap
					
					graphicsObject = Nothing
					tempBitmap = Nothing
				Else
					Dim tempBitmap As New Bitmap(newSize.Width, newSize.Height)
					Dim graphicsObject As Graphics = Graphics.FromImage(tempBitmap)
					graphicsObject.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
					graphicsObject.DrawImage(originalImage, 0, 0, newSize.Width, newSize.Height)
					resizedImage = tempBitmap
					
					graphicsObject = Nothing
					tempBitmap = Nothing
				End If
				
				originalImage.Dispose()
				
				newSize = Nothing
				originalImage = Nothing
				
				Return resizedImage
				
			End If
			
		End Function
		
		Public Function RotateImage(ByVal imageFile As Bitmap, ByVal Angle As Integer) As System.Drawing.Bitmap
			
			
			If Angle = 0 Then ' nothing to do
				Return imageFile
				rotation = 0
			Else
				
				Dim originalImage As Bitmap  = imageFile ' Dim originalImage As New Bitmap(imageFile)
				
				' Make an array of points defining the
				' image's corners.
				Dim wid As Single = originalImage.Width
				Dim hgt As Single = originalImage.Height
				Dim corners As Point() = { _
					New Point(0, 0), _
					New Point(wid, 0), _
					New Point(0, hgt), _
				New Point(wid, hgt)}
				
				' Translate to center the bounding box at the origin.
				Dim cx As Single = wid / 2
				Dim cy As Single = hgt / 2
				Dim i As Long
				For i = 0 To 3
					corners(i).X -= cx
					corners(i).Y -= cy
				Next i
				
				' Rotate.
				Dim theta As Single = Single.Parse(angle) * System.Math.PI _
					/ 180.0
				Dim sin_theta As Single = System.Math.Sin(theta)
				Dim cos_theta As Single = System.Math.Cos(theta)
				Dim X As Single
				Dim Y As Single
				For i = 0 To 3
					X = corners(i).X
					Y = corners(i).Y
					corners(i).X = X * cos_theta + Y * sin_theta
					corners(i).Y = -X * sin_theta + Y * cos_theta
				Next i
				
				' Translate so X >= 0 and Y >=0 for all corners.
				Dim xmin As Single = corners(0).X
				Dim ymin As Single = corners(0).Y
				For i = 1 To 3
					If xmin > corners(i).X Then xmin = corners(i).X
					If ymin > corners(i).Y Then ymin = corners(i).Y
				Next i
				For i = 0 To 3
					corners(i).X -= xmin
					corners(i).Y -= ymin
				Next i
				
				' Create an output Bitmap and Graphics object.
				Dim rotatedImage As New Bitmap(CInt(-2 * xmin), CInt(-2 * _
					ymin))
				Dim tempGraphics As Graphics = Graphics.FromImage(rotatedImage)
				
				' Drop the last corner lest we confuse DrawImage,
				' which expects an array of three corners.
				ReDim Preserve corners(2)
				
				' Draw the result onto the output Bitmap.
				' If antialias then tempGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic ' .HighQualityBilinear .NearestNeighbour
				tempGraphics.DrawImage(originalImage, corners)
				
				tempGraphics = Nothing
				originalImage.Dispose()
				
				originalImage = Nothing
				
				rotation = Angle
				
				Return rotatedImage
				
			End If
			
		End Function
		
		
		Sub SplitViewToolStripMenuItemClick(sender As Object, e As EventArgs)
			PictureBox1.Top = 0
			PictureBox1.Left = panel1Contents.Width + (splitContainer1.SplitterWidth/2) - (picturebox1.Width/2)
			PictureBox2.Top = 0
			PictureBox2.Left = (picturebox2.Width/2) * -1 - (splitContainer1.SplitterWidth/2)
		End Sub
		
		
		Sub OriginalSizetoolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
			restoreImagesToNormalSize()
		End Sub
		
		Sub FitImagesToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
			fitImagesToPictureBox()
			' fitImages = True
		End Sub
		
		Dim fitImages As Boolean = False
		Sub FitImagesToolStripMenuItem1Click(ByVal sender As Object, ByVal e As EventArgs)
			fitImages = FitImagesToolStripMenuItem1.Checked
			If FitImagesToolStripMenuItem1.Checked Then
				fitImagesToPictureBox()
			Else
				restoreImagesToNormalSize()
			End If
		End Sub
		
		Sub fitImagesToPictureBox()
			PictureBox1.Top = 0
			PictureBox1.Left = 0
			PictureBox2.Top = 0
			PictureBox2.Left = 0
			Try
				pictureBox1.Image = ResizeImage(System.Drawing.Image.FromFile(sourceFile),panel1Contents.Width,panel1Contents.Height,True,True)
			Catch e As Exception
			End Try
			Try
				pictureBox2.Image = ResizeImage(System.Drawing.Image.FromFile(targetFile),panel2Contents.Width,panel2Contents.Height,True,True)
			Catch e As Exception
			End Try
			
		End Sub
		
		Sub restoreImagesToNormalSize()
			PictureBox1.Top = 0
			PictureBox1.Left = 0
			PictureBox2.Top = 0
			PictureBox2.Left = 0
			Try
				pictureBox1.Image = ResizeImage(System.Drawing.Image.FromFile(sourceFile),100,-1,True,False)
			Catch e As Exception
			End Try
			Try
				pictureBox2.Image = ResizeImage(System.Drawing.Image.FromFile(targetFile),100,-1,True,False)
			Catch e As Exception
			End Try
		End Sub
		
		
		Sub SaveReportToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
			saveReport()
		End Sub
		
		Sub SaveLogToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
			saveLog()
		End Sub
		
		Sub ClearLogToolStripMenuItem1Click(ByVal sender As Object, ByVal e As EventArgs)
			clearLog()
		End Sub
		
		Sub clearLog()
			logDom.body.innerHTML = ""
			logDoc.Write("<span class=""time"">" & now() & "</span> opening log<br /><br />" & vbcrlf)
			logDoc.Write("<p class=""debug"">" & debugInfo & "</p>")
			logdoc.Write("----------<br /><br />")
		End Sub

		' get build date/time from application
		Function RetrieveLinkerTimestamp(ByVal filePath As String) As DateTime
		  Const PeHeaderOffset As Integer = 60
		  Const LinkerTimestampOffset As Integer = 8
		
		  Dim b(2047) As Byte
		  Dim s As Stream = Nothing
		  Try
		  s = New FileStream(filePath, FileMode.Open, FileAccess.Read)
		  s.Read(b, 0, 2048)
		  Finally
		  If Not s Is Nothing Then s.Close()
		  End Try
		  
		  Dim i As Integer = BitConverter.ToInt32(b, PeHeaderOffset)
		  
		  Dim SecondsSince1970 As Integer = BitConverter.ToInt32(b, i + LinkerTimestampOffset)
		  Dim dt As New DateTime(1970, 1, 1, 0, 0, 0)
		  dt = dt.AddSeconds(SecondsSince1970)
		  ' dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours)
		  Return dt
		End Function
		
		Dim debugInfo As String = "Visual QA " & application.ProductVersion.Substring(0,8) & " (" & RetrieveLinkerTimestamp(application.ExecutablePath).ToString("yy/MM/dd HH:mm") & ")<br /><br />" & VbCrLf & _
						"<script type=""text/javascript"">" & VbCrLf & _
						"  document.write(""You are viewing content in <b>IE"" + document.documentMode + ""</b> mode."")" & VbCrLf & _
						" " & VbCrLf & _
						"  // document.write(navigator.appName + "" "" + navigator.appVersion + ""<br /><br />"")" & VbCrLf & _
						"  document.write(""<br /><br />DocumentMode: "" + document.documentMode)" & VbCrLf & _
						"  document.write(""<br />UserAgent: "" + navigator.userAgent)" & VbCrLf & _
						"</script>"
						
'						"<script type=""text/javascript"">" & VbCrLf & _
'						"if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)){ // test for MSIE x.x;" & VbCrLf & _
'						" var ieversion=new Number(RegExp.$1) // capture x.x portion and store as a number" & VbCrLf & _
'						" if (ieversion>=10)" & VbCrLf & _
'						"  document.write(""You are viewing content in <b>IE10</b> mode."")" & VbCrLf & _
'						" else if (ieversion>=9)" & VbCrLf & _
'						"  document.write(""You are viewing content in <b>IE9</b> mode."")" & VbCrLf & _
'						" else if (ieversion>=8)" & VbCrLf & _
'						"  document.write(""You are viewing content in <b>IE8</b> mode."")" & VbCrLf & _
'						" else if (ieversion>=7)" & VbCrLf & _
'						"  document.write(""You are viewing content in <b>IE7</b> mode."")" & VbCrLf & _
'						" else if (ieversion>=6)" & VbCrLf & _
'  						" document.write(""IE mode."")" & VbCrLf & _
'						" } else {" & VbCrLf & _
'						"  document.write(""Cannot detect browser mode."")" & VbCrLf & _
'						"}  " & VbCrLf & _
'						"  // document.write(navigator.appName + "" "" + navigator.appVersion + ""<br /><br />"")" & VbCrLf & _
'						"  document.write(""<br /><br />DocumentMode: "" + document.documentMode)" & VbCrLf & _
'						"  document.write(""<br />UserAgent: "" + navigator.userAgent)" & VbCrLf & _
'						"</script>"
		
		Dim saveFile As New SaveFileDialog
		Const DIALOG_OK As System.Windows.Forms.DialogResult = DialogResult.OK
		
		Sub saveLog()
			Dim logFile As String = Nothing
			saveFile.Title = "Save log file"
			saveFile.FileName = "visualqa.log"
			saveFile.Filter = "Log files (*.log)|*.log|HTML files (*.htm)|*.htm|Text files (*.txt)|*.txt|All files (*.*)|*.*"
			If saveFile.ShowDialog() = DIALOG_OK Then
				logFile = saveFile.FileName
				Dim logFileWriter As StreamWriter
				If Not File.Exists(logFile) then
					logFileWriter = New StreamWriter(logFile)
				Else
					logFileWriter = File.AppendText(logFile)
				End If
				
				If lcase(Path.GetExtension(logFile)) = ".htm" then
					logFileWriter.Write(logDom.body.outerHTML)
				Else
					logFileWriter.Write(logDom.body.innerText)
				End If
				logFileWriter.Flush()
				logFileWriter.Close()
				logFileWriter = Nothing
				logFile = Nothing
			End If
		End Sub
		
		Sub saveReport()
			Dim reportFile As String = Nothing
			saveFile.Title = "Save report file"
			saveFile.FileName = "report.htm"
			saveFile.Filter = "HTML files (*.htm)|*.htm|All files (*.*)|*.*"
			If saveFile.ShowDialog() = DIALOG_OK Then
				reportFile = saveFile.FileName
				Dim reportFileWriter As StreamWriter
				If Not File.Exists(reportFile) then
					reportFileWriter = New StreamWriter(reportFile)
				Else
					reportFileWriter = File.AppendText(reportFile)
				End If
				
				reportFileWriter.WriteLine("<table>")
				
				' write header
				reportFileWriter.WriteLine("<tr>" & _
					"<th scope='col'>#</th>" & _
				"<th scope='col'>Filename</th>" & _
				"<th scope='col'>Source</th>" & _
				"<th scope='col'>Target</th>" & _
				"<th scope='col'>State</th>" & _
				"<th scope='col'>Comment</th>" & _
				"</tr>")
				
				For x As Integer = 0 To fileList.GetUpperBound(0) - 1
					reportFileWriter.Write("<tr><td>" & x+1 & "</td>")
					reportFileWriter.Write("<td>" & path.GetFileName(fileList(x,0)) & "</td>")
					For y As Integer = 0 To fileList.GetUpperBound(1) - 2 ' - 1 ' do not include checksum
						reportFileWriter.Write("<td>" & fileList(x,y) & "</td>")
					Next
					reportFileWriter.Write("</tr>" & vbCrLf)
				Next
				
				reportFileWriter.WriteLine("</table>")
				
				reportFileWriter.Flush()
				reportFileWriter.Close()
				reportFileWriter = Nothing
				reportFile = Nothing
			End If
		End Sub
		
		Dim refreshFileList As Boolean = False
		Dim keepComments As Boolean = True
		Dim keepStatus As Boolean = True
		Sub RefreshToolStripMenuItem1Click(ByVal sender As Object, ByVal e As EventArgs)
			refreshFileList = True
			Dim actFile As Integer = activeFile
			openProject()
			showFile(actFile)
			actFile = Nothing
		End Sub
		
		Dim checkOrphans As Boolean = False
		Dim checkMD5hashes As Boolean = False
		Sub CheckForOrphansToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
			checkOrphans = CheckForOrphansToolStripMenuItem.Checked
		End Sub
		
		Sub CheckaddMD5FileHashToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
			checkMD5hashes = CheckaddMD5FileHashToolStripMenuItem.Checked
		End Sub
		
		
		Function getMd5Hash(fileIn As String) As String
			
			Dim f As FileStream = New FileStream(fileIn, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
			Dim md5 As System.Security.Cryptography.MD5CryptoServiceProvider = New System.Security.Cryptography.MD5CryptoServiceProvider()
			md5.ComputeHash(f)
			f.Close()
			
			Dim hash As Byte() = md5.Hash
			Dim buff As StringBuilder = New StringBuilder()
			Dim hashByte As Byte
			For Each hashByte In hash
				buff.Append(String.Format("{0:X2}", hashByte)) ' String.Format("{0:X1}", hashByte)
			Next
			
			Return buff.ToString()
			
			hashByte = Nothing
			buff = Nothing
			Hash = Nothing
			md5 = Nothing
			f = Nothing
			
		End Function
		
		Public Shared Function getChecksum(textIn As String) As String
			
			Dim s As String = textIn
			Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
			
			Dim bs As Byte() = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(s))
			
			'Dim result as String = BitConverter.ToString(bs).ToLower().Replace("-","")
			Dim b As Byte
			Dim result As New System.Text.StringBuilder()
			For Each b In bs
				result.Append(b.ToString("x2"))
			Next
			
			getChecksum = result.ToString
			' Console.WriteLine(result)
			
		End Function
		
		Dim panelComments As New System.Windows.Forms.Panel
		Dim textBoxComments As New System.Windows.Forms.TextBox
		
		Sub TextBoxCommentsTextChanged(ByVal sender As Object, ByVal e As EventArgs)
			fileList(activeFile,3) = textBoxComments.Text
		End Sub
		
		Sub ShowCommentsToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
			panelComments.Visible = ShowCommentsToolStripMenuItem.Checked
			if activeFile < 0 then activeFile = 0
			textBoxComments.Text = fileList(activeFile,3)
		End Sub
		
		Sub NoOrphansToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
			NoOrphansToolStripMenuItem.Checked = Not NoOrphansToolStripMenuItem.Checked
			filterNoOrphans = NoOrphansToolStripMenuItem.Checked
		End Sub
		
		
		Public Sub HandleCommentUpdated(commentText As String, commentId As Integer)
			' things to do when event is executed
			filelist(commentId,3) = commentText
		End Sub
		
		
		Sub showCommentEditor()
			Dim editor As New SimpleEditorForm
			editor.id = activeFile
			' editor.title = Path.GetFileName(filelist(activeFile,1))
			editor.comment = filelist(activeFile,3)
			editor.Show ' editor.ShowDialog() ' modal form
		End Sub
		
		Sub TreeView1AfterCollapse(ByVal sender As Object, ByVal e As TreeViewEventArgs)
			If Not len(Path.GetExtension(sourceFolder & "\" & e.Node.FullPath).ToString) > 0 Then _
				e.Node.SelectedImageIndex = 0
		End Sub
		
		Sub TreeView1AfterExpand(ByVal sender As Object, ByVal e As TreeViewEventArgs)
			If Not len(Path.GetExtension(sourceFolder & "\" & e.Node.FullPath).ToString) > 0 Then _
				e.Node.SelectedImageIndex = 1
		End Sub
	
	
		Sub TreeView1AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs)
			If len(Path.GetExtension(sourceFolder & "\" & e.Node.FullPath).ToString) > 0 Then
				e.Node.SelectedImageIndex = e.Node.ImageIndex ' 9
			Else ' nothing to do with structural folder node
'				if e.Node.IsExpanded then
'					e.Node.SelectedImageIndex = 1
'				Else
'					e.Node.SelectedImageIndex = 0
'				End If
			End If
			
			if autoUpdateCheckStatus then updateStatus()
			If file.Exists(sourceFolder & "\" & e.Node.FullPath) Then _
				if Not updateTreeViewInteractive And Not sourcePinned then showFile(sourceFolder & "\" & e.Node.FullPath)
				' MsgBox(targetFolder & "\" & e.Node.FullPath)
				If Not file.Exists(sourceFolder & "\" & e.Node.FullPath) And file.Exists(targetFolder & "\" & e.Node.FullPath) then _
						showFile(targetFolder & "\" & e.Node.FullPath)
					' If file.Exists(sourceFolder & "\" & e.Node.FullPath) Then webbrowser1.Url = New  Uri(sourceFolder & "\" & e.Node.FullPath )
					' If file.Exists
		End Sub
				
' temporarily deactivated
				Sub LabelStatusMouseHover(ByVal sender As Object, ByVal e As EventArgs)
'					ContextMenuStripStatus.RenderMode = ToolStripRenderMode.System
'					For Each item As toolStripMenuItem In ContextMenuStripStatus.Items
'						If lcase(item.Text) = lcase(labelStatus.Text) Then
'							item.Checked = True
'						Else
'							item.Checked = False
'						End If
'					Next
'					' adjust location
'					Dim tmpPos As System.Drawing.Point
'					tmpPos.y = LabelStatus.Location.Y - ContextMenuStripStatus.Height + 10
'					tmpPos.X = LabelStatus.Location.X - LabelStatus.Left
'					ContextMenuStripStatus.Show(LabelStatus, tmpPos)
				End Sub
				
				Sub ContextMenuStripStatusMouseLeave(ByVal sender As Object, ByVal e As EventArgs)
					ContextMenuStripStatus.Hide
				End Sub

	
				Sub SaveProjectToolStripMenuItem1Click(ByVal sender As Object, ByVal e As EventArgs)
					If saveStatus = True then writeXcfg(projectFile)
				End Sub

	
	Function checkFilter(fileID As Integer) As Boolean
		Dim displayFile As Boolean = True
		
		' 2011/11/28 check added
		If fileID < 0 Then Return False
		
		If filterActive Then
			If Not filterOrphans.Checked And _
				(fileList(fileID,2) = "Orphan" Or fileList(fileID,2) = "Missing") _
				then displayFile = False
	
			If Not filterUnchecked.Checked And _
				fileList(fileID,2) = "unchecked" _
				Then displayFile = False
				
			If Not filterNg.Checked And _
				fileList(fileID,2) = "NG" _
				then displayFile = False
	
			If Not filterOk.Checked And _
				fileList(fileID,2) = "OK" _
				then displayFile = False
			
			If Not filterChecked.Checked And _
				fileList(fileID,2) = "checked" _
				Then displayFile = False
	
			If Not filterProblem.Checked And _
				fileList(fileID,2) = "Problem" _
				Then displayFile = False
				
				
				' second level filters
				If filterComments.Checked Then
					If DisplayFile And Not Len(Trim(fileList(fileID,3))) = 0 Then
						displayFile = True
					Else
						displayFile = False
					End If
				End If
		End If
			
		Return displayFile
	End Function

		
	Sub ContextMenuStripTreeFilterItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)
		If Not Instr(e.ClickedItem.text,"Filter") > 0 Then filterItemClicked = True
	End Sub
	
	
	Sub filterClicked(sender As Object, e As EventArgs)
		' contextMenuStripTreeFilter.Show
		If filterActive Then fillFileListTree
	End Sub


	Dim filterItemClicked As Boolean = False

	Sub ContextMenuStripTreeFilterClosing(sender As Object, e As ToolStripDropDownClosingEventArgs)
		If filterItemClicked Then
			filterItemClicked = False
			e.Cancel = True
		End If
	End Sub
	
	Sub ClearFilterShowAllToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
		contextMenuStripTreeFilter.Close()
		filterChecked.Checked = True
		filterUnchecked.Checked = True
		filterOk.Checked = True
		filterNg.Checked = True
		filterOrphans.Checked = True
		filterProblem.Checked = True
		filterComments.Checked = False
		If filterActive then fillFileListTree
	End Sub
	

	
	Sub FilterCommentsCheckedChanged(sender As Object, e As EventArgs)
		If filterActive then fillFileListTree
	End Sub

	Sub LabelEncoding1DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
		If panelComments.Visible then textBoxComments.Text = textBoxComments.Text & LabelEncoding1.Text
	End Sub
	
	Sub LabelEncoding2DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
		If panelComments.Visible then textBoxComments.Text = textBoxComments.Text & LabelEncoding2.Text
	End Sub
	
	Sub TextBoxTitle1DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
		If panelComments.Visible then textBoxComments.Text = textBoxComments.Text & TextBoxTitle1.Text
	End Sub
	
	Sub TextBoxTitle2DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
		If panelComments.Visible then textBoxComments.Text = textBoxComments.Text & TextBoxTitle2.Text
	End Sub
	
	Sub LabelImgInfo1DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
		If panelComments.Visible then textBoxComments.Text = textBoxComments.Text & LabelImgInfo1.Text
	End Sub
	
	Sub LabelImgInfo2DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
		If panelComments.Visible then textBoxComments.Text = textBoxComments.Text & LabelImgInfo2.Text
	End Sub
	
	Sub WebBrowser2DocumentCompleted(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
		highlight(webbrowser2)
		if searchPanel.Visible And len(trim(textbox1.Text)) > 0 then search(webbrowser2, textbox1.Text)
		
		' monitor edits on target
		FileSystemWatcher1.Filter = Path.GetFileName(targetFile)
		FileSystemWatcher1.Path = Path.GetDirectoryName(targetFile)
		If autoRefreshAfterEditToolStripMenuItem.Checked then FileSystemWatcher1.EnableRaisingEvents = True
	
		refreshTargetInfo()
		
		' follow links
		If followLinksToolStripMenuItem.Checked then
			Dim olink As HtmlElement
			Dim olinks As HtmlElementCollection = WebBrowser2.Document.Links
			
			For Each olink In olinks
			    olink.AttachEventHandler("onclick", AddressOf LinkClicked2)
			Next
		End If

		If pinningToTarget Then
			pinningToTarget = False
			pictureBox5Click(Nothing,Nothing)
		End If
		
		' for autoscroll
		_timer = 0
		scrollPos2 = 0

		panel4.Focus()

	End Sub
	
	Sub WebBrowser1DocumentCompleted(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
		highlight(webbrowser1)
		If searchPanel.Visible And len(trim(textbox1.Text)) > 0 then search(webbrowser1, textbox1.Text)
		refreshSourceInfo()
		
		' follow links
		If followLinksToolStripMenuItem.Checked then
			Dim olink As HtmlElement
			Dim olinks As HtmlElementCollection = WebBrowser1.Document.Links
			
			For Each olink In olinks
			    olink.AttachEventHandler("onclick", AddressOf LinkClicked1)
			Next
		End If

		If pinningToSource Then
			pinningToSource = False
			pictureBox4Click(Nothing,Nothing)
		End If
	
		' for autoscroll
		scrollPos1 = 0
	
		panel4.Focus()

	End Sub

    Private Shared Function splitLine(ByVal oneLine As String) As String()
      If Not oneLine is Nothing And Not Len(Trim(oneLine)) = 0 Then
		Dim delimiter As String = ","
		Dim textQualifier As String = """"
		Dim inputString As String = oneLine
		Dim pattern As String = String.Format("{0}(?=(?:[^{1}]*{1}[^{1}]*{1})*(?![^{1}]*{1}))", Regex.Escape(delimiter), Regex.Escape(textQualifier))
		Dim r As System.Text.RegularExpressions.Regex = _
		                New System.Text.RegularExpressions.Regex(Pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled Or RegexOptions.Multiline)
		' Return r.Split(inputString)
		' clean up a little
		Dim _tmp() As string = r.Split(inputString)
		For i As Integer = 0 To _tmp.Length - 1
			_tmp(i) = Trim(_tmp(i))
			If len(_tmp(i)) > 0 then
			If _tmp(i).Substring(0,1) = """" And _tmp(i).Substring(_tmp(i).Length -1 ,1) = """" Then _
				_tmp(i) = _tmp(i).Substring(1,_tmp(i).Length - 2)
			_tmp(i) = Replace(_tmp(i), """""", """")
			End If
			system.Diagnostics.debug.WriteLine(i & ": " & _tmp(i) & "")
		Next
		Return _tmp
	  Else
      	Return Nothing
      End If
    End Function


	Sub search1Click(ByVal sender As Object, ByVal e As EventArgs)
		Highlight(webbrowser1)
		Highlight(webbrowser2)
		search(webbrowser1, textbox1.Text)
		search(webbrowser2, textbox1.Text)
	End Sub
    
    Dim Public searchColor As String = "cyan"
    
    ' example string: #01DF74: Down, #01DF74: Up, #orange: PageUp, #orange: PageDown, Ctrl, Alt
    Sub search(browser As System.Windows.Forms.WebBrowser, searchString As String)
 		clearSearchResults(browser)
        If searchString.Length = 0 Then Exit Sub
        
        Dim MyRange As mshtml.IHTMLTxtRange = Nothing
        
        ' 2011/12/05 added for IE9 support
        Dim htmlDoc As mshtml.IHTMLDocument2 = Browser.Document.DomDocument
        Dim body As mshtml.IHTMLBodyElement = htmlDoc.body
        ' MyRange = Body.createTextRange()
        
        For each _searchString As String in splitLine(searchString)
	        MyRange = body.createTextRange() ' browser.Document.DomDocument.selection().createRange ' 2011/12/05
	        If not len(Trim(_searchString)) = 0 Then
		        
	        	Dim _searchColor As String = searchColor
	        	If _searchString.StartsWith("#") And _searchString.Contains(": ") Then
	        		_searchColor = _searchString.Substring(0,_searchString.IndexOf(":")).Trim()
	        		If Not regex.IsMatch(_searchColor,"^[A-F0-9]{6}$") Then _searchColor = _searchColor.Substring(1)
	        		_searchString = _searchString.Substring(_searchString.indexOf(":") + 2)
	        	End If
		        
		        Do While MyRange.findText(_searchString)
		                
		        	system.Diagnostics.debug.WriteLine(myrange.parentElement.tagName)
		        	System.Diagnostics.Debug.WriteLine("Found """ & myRange.text & """")
		        	' leading spaces are removed using pasteHTML
		        	Dim _tmpResult As String = MyRange.text
					if _tmpResult.StartsWith(" ") then _tmpResult = "&nbsp;" & _tmpResult.Substring(1)
		        	Myrange.pasteHTML("<span class=""searchresult"" style=""BACKGROUND-COLOR: " & _searchColor & """>" & _tmpResult & "</span>") ' magenta, yellow
		           	Myrange.collapse(False)
		        Loop
	        End If
        Next
        myrange = Nothing

        ' browser.DocumentText = regex.Replace(browser.DocumentText,searchString, "<span class=""searchresult"" style=""BACKGROUND-COLOR: cyan"">" & searchstring & "</span>") ' magenta, yellow
    
    End Sub
    
	
	Dim highlightClassNames() As String
	' HACK: Dim highlightClassNames() As String = {"ui"}
    
    sub Highlight(browser As System.Windows.Forms.WebBrowser)
        If sourcePinned And browser.Name = "webBrowser1" Then Exit Sub
        If targetPinned And browser.Name = "webBrowser2" Then Exit Sub
        
        
			' highlight files using plug-ins
			If Not contextMenuStripHighlightPlugins.Items.Count = 0 Then
				
' Msgbox(browser.Document.Body.Parent.OuterHtml)
' Encoding.GetEncoding(browser.Document.Encoding)
				
				' get html contents
'				Msgbox(browser.DocumentText)
				' get actual html
'				Dim html As String = Browser.DocumentText

				If Directory.Exists(path.Combine(application.StartupPath,"Plugins")) Then
					For Each plugin As String In Directory.GetFiles(path.Combine(application.StartupPath,"Plugins"),"*.dll")
						system.Diagnostics.Debug.WriteLine(plugin)
						Try
						Dim _PlugIn As VisualQAPlugIn = loadPlugin(plugin)
						If pluginIsActive(_plugin.Name) And _plugin.canHighlight Then
							_PlugIn.Highlight(browser)
						End If
						Catch ex As Exception
						End Try
					Next
				End If
						
'				' set html contents
'				Browser.DocumentText = html
			End If        
        
     
        ' system.Diagnostics.debug.WriteLine(browser.DocumentText)
		Dim page as mshtml.HTMLdocument
		Dim Elements as mshtml.IHTMLElementCollection
			
		
		  If Not highlightClassNames Is Nothing Then
			  For each className As String in highlightClassNames
				page = browser.document.Domdocument
				Elements = page.getElementsByTagName("span")
				system.Diagnostics.debug.WriteLine(elements.length)
				For Each obj As mshtml.IHTMLElement In Elements
					' to get class attributes use className, for attribute uses htmlFor
					If Not obj.getAttribute("className") Is Nothing Then
						Dim attrValue As object = obj.getAttribute("className",2)
						If attrValue.ToString = className Then
							obj.style.backgroundColor = "yellow" ' "lime" "magenta" "cyan" '
							' obj.value = "TEST"
						End If
						attrValue = Nothing
					End If
				Next
			  Next
			End If


			If highlightLocalLinksToolStripMenuItem.Checked Then
				page = browser.document.Domdocument
				Elements = page.getElementsByTagName("a")
				system.Diagnostics.debug.WriteLine(elements.length)
				For Each obj As mshtml.IHTMLElement In Elements
					If Not obj.getAttribute("href") Is Nothing Then
						Dim attrValue As Object = obj.getAttribute("href",2)
						' system.Diagnostics.Debug.WriteLine(attrValue.ToString)
						If Not Mid(attrValue.ToString,1, 2) = ".." And Not Mid(attrValue.ToString,1,7) = "http://" And Not Mid(attrValue.ToString,1,5) = "exec:" Then
							if len(trim(obj.innerText)) > 0 then obj.style.backgroundColor = "lime" ' "yellow" "magenta" "cyan" '
						End If
						attrValue = Nothing
					End If
				Next
			End if

			elements = nothing
			page = nothing

	End Sub


	Function pluginIsActive(pluginName As String) As Boolean
		If contextMenuStripHighlightPlugins.Items.Count = 0 Then Return False
		Dim _result As Boolean = False
		For Each item As ToolStripMenuItem In contextMenuStripHighlightPlugins.Items
			If item.Text = pluginName And item.Checked Then
				_result = True
				Exit For
			End If
		Next
		Return _result
	End Function
	

	Sub clearSearchResults(browser As System.Windows.Forms.WebBrowser)
		Dim page as mshtml.HTMLdocument
		Dim Elements as mshtml.IHTMLElementCollection
	
		page = browser.document.Domdocument
		Elements = page.getElementsByTagName("span")
		' system.Diagnostics.debug.WriteLine(elements.length)
		For Each obj As mshtml.IHTMLElement In Elements
		 	system.Diagnostics.debug.WriteLine(obj.outerHTML)
			' to get class attributes use className, for attribute uses htmlFor
			If Not obj.getAttribute("className") Is Nothing Then
				Dim attrValue As object = obj.getAttribute("className",2)
				If attrValue.ToString = "searchresult" Then
					obj.outerHTML = obj.innerHTML
				End If
				attrValue = Nothing
			End If
		Next
		elements = nothing
		page = nothing
	End Sub

	
	Sub ContextMenuStripFileItemItemClicked(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)
			If e.ClickedItem.Text = "checked" OR e.ClickedItem.Text = "OK" OR e.ClickedItem.Text = "NG" OR e.ClickedItem.Text = "Flagged" then
				fileList(activeFile,2) = e.ClickedItem.Text
				labelStatus.Text = fileList(activeFile,2)
			End If
	End Sub
	
	Sub OpenInEditorToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)

		activePanel = 2
		EditorToolStripMenuItemClick(Nothing, Nothing)
		
	End Sub


	Sub FileSystemWatcher1Changed(ByVal sender As Object, ByVal e As FileSystemEventArgs)
		FileSystemWatcher1.EnableRaisingEvents = False
		If autoRefreshAfterEditToolStripMenuItem.Checked then refreshTarget()
		System.Diagnostics.Debug.WriteLine("Target edited!")
	End Sub

	
	Sub TextBox1KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then 'Chr(13) is the Enter Key
            search1Click(Nothing,Nothing)
		End If
	End Sub
	
	Sub ShowSearchToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
		If searchPanel.visible Then
			searchPanel.Visible = False
			ShowSearchToolStripMenuItem.Text = "Show search"
			' textbox1.Text = ""
		Else
			searchPanel.Visible = True
			ShowSearchToolStripMenuItem.Text = "Hide search"
			textbox1.Focus()
		End If
	End Sub
	
	Sub highlightContents(ByVal sender As Object, ByVal e As EventArgs)
		Highlight(webbrowser2)
		Highlight(webbrowser1)
	End Sub
	
 
 
 	' follow links
    Private Sub LinkClicked1(ByVal sender As Object, ByVal e As EventArgs)
        Dim link As HtmlElement = WebBrowser1.Document.ActiveElement
        Dim url As String = link.GetAttribute("href")
 
        System.Diagnostics.Debug.WriteLine("Link Clicked: " & link.InnerText & vbCrLf & "Destination: " & url)
        
        If InStr(lcase(url),lcase(simpleURLencode(sourceFolder))) > 0 OR (sourcePinned AND InStr(lcase(url),lcase(simpleURLencode(targetFolder))) > 0) Then
        	webbrowser2.Navigate(Replace(lcase(url),lcase(simpleURLencode(sourceFolder)),lcase(simpleURLencode(targetFolder))))
        End If
       
        activateCurrentNode(url)
    End Sub
	
    Private Sub LinkClicked2(ByVal sender As Object, ByVal e As EventArgs)
        Dim link As HtmlElement = WebBrowser2.Document.ActiveElement
        Dim url As String = link.GetAttribute("href")
 
        System.Diagnostics.Debug.WriteLine("Link Clicked: " & link.InnerText & vbCrLf & "Destination: " & url)

        If InStr(lcase(url),lcase(simpleURLencode(targetFolder))) > 0  OR (targetPinned AND InStr(lcase(url),lcase(simpleURLencode(sourceFolder))) > 0) Then
        	webbrowser1.Navigate(Replace(lcase(url),lcase(simpleURLencode(targetFolder)),lcase(simpleURLencode(sourceFolder))))
        End If
    End Sub

	Dim pinningToSource As Boolean = False
	Dim pinningToTarget As Boolean = False

	' allow to cancel navigation
	Private Sub webBrowser1_Navigating(ByVal sender As Object, ByVal e As WebBrowserNavigatingEventArgs) Handles webBrowser1.Navigating
		If sourcePinned Then
			e.Cancel = True
			' webBrowser2.Navigate(e.Url)
		End If
	End Sub
	
	Private Sub webBrowser2_Navigating(ByVal sender As Object, ByVal e As WebBrowserNavigatingEventArgs) Handles webBrowser2.Navigating
		If targetPinned Then
			e.Cancel = True
			' webBrowser1.Navigate(e.Url)
		End If
	End Sub

	Function simpleURLencode(urlIn As String) As String
		urlIn = Replace(urlIn, "\", "/")
		urlIn = Replace(urlIn, " ", "%20")
		return urlIn
	End Function
	
	
	
	Dim _autoScroll As Boolean = False
	Dim scrollPos1 As Integer = 0
	Dim scrollPos2 As Integer = 0
	Dim _timer As Integer = 100
	
	Sub TTick(ByVal sender As Object, ByVal e As EventArgs)
		_timer = _timer + t.Interval
		If scrollPos1 < 0 Then scrollPos1 = 0
		If scrollPos2 < 0 Then scrollPos2 = 0
		
		If scrollPos1 > WebBrowser1.Document.Body.ScrollRectangle.Height then scrollPos1 = WebBrowser1.Document.Body.ScrollRectangle.Height
		If scrollPos2 > WebBrowser2.Document.Body.ScrollRectangle.Height then scrollPos2 = WebBrowser2.Document.Body.ScrollRectangle.Height
				
		if _autoScroll then
			if _timer >= 1000 then
				scrollPos1 = scrollPos1 + 10 ' pixels?
				scrollPos2 = scrollPos2 + 10
				if Not sourcePinned Then webbrowser1.Document.Window.ScrollTo(0,scrollPos1)
				if Not targetPinned Then webbrowser2.Document.Window.ScrollTo(0,scrollPos2)
			End If
			If scrollPos1 >= WebBrowser1.Document.Body.ScrollRectangle.Height - webbrowser1.Height + 30 And scrollPos2 >= WebBrowser2.Document.Body.ScrollRectangle.Height - webbrowser2.Height + 30 And _timer >= 2500 Then
				nextFile()
				' t.Enabled = False
			End If
		End If
	End Sub
	
	Sub Button1Click(ByVal sender As Object, ByVal e As EventArgs)
		t.Enabled = Not t.Enabled
		If t.Enabled then
			btnToggleScroll.Image = imageList1.Images.Item(2)
		Else
			btnToggleScroll.Image = imageList1.Images.Item(0)
		End if
	End Sub
	
	Sub AutoScrollToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
		_autoScroll = True
		scrollPanel.Visible = AutoScrollToolStripMenuItem.Checked
		t.Enabled = False
		btnToggleScroll.Image = imageList1.Images.Item(0)
	End Sub

	
	Sub WebBrowser1Navigated(ByVal sender As Object, ByVal e As WebBrowserNavigatedEventArgs)
		' to get filename from drag-and-drop operation
		sourceFile = WebBrowser1.Url.LocalPath
	End Sub
	
	Sub WebBrowser2Navigated(ByVal sender As Object, ByVal e As WebBrowserNavigatedEventArgs)
		' to get filename from drag-and-drop operation
		targetFile = WebBrowser2.Url.LocalPath
	End Sub

'				self closing message box
'				' msgboxautoclose("Entering annotate mode. Click OK to just save a screenshot", MsgBoxStyle.Information, "Annotate screen", 2)
'
'
'				Private Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, _
'			                       ByVal bScan As Byte, _
'			                       ByVal dwFlags As Byte, _
'			                       ByVal dwExtraInfo As Byte)
'
'			    Private Const VK_RETURN As Byte = &HD
'			    Private Const KEYEVENTF_KEYDOWN As Byte = &H0
'			    Private Const KEYEVENTF_KEYUP As Byte = &H2
'
'			    Public Sub msgboxautoclose(ByVal Message As String, Optional ByVal Style As MsgBoxStyle = Nothing, Optional ByVal title As String = Nothing, Optional ByVal delay As Integer = 5)
'			        Dim t As New Threading.Thread(AddressOf closeMsgbox)
'			        t.Start(delay) '5 second default delay
'			        MsgBox(Message, Style, title)
'			    End Sub
'
'			    Private Sub closeMsgbox(ByVal delay As Object)
'			        Threading.Thread.Sleep(CInt(delay) * 1000)
'			        AppActivate(Me.Text)
'			        keybd_event(VK_RETURN, 0, KEYEVENTF_KEYDOWN, 0)
'			        keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0)
'			    End Sub

	Sub toggleAnnotateMode()
		If Not annotateImageMode Then

			webbrowser1.Refresh()
			webbrowser2.Refresh()

			annotateImageMode = True
			panel3.Enabled = False
			treeview1.Enabled = False

				annotateModeToolStripMenuItem.Visible = True
				fileToolStripMenuItem.Visible = False
				projectToolStripMenuItem.Visible = False
				ProjectFilterToolStripMenuItem.Visible = False
				toolsToolStripMenuItem.Visible = False
				sourceToolStripMenuItem.Visible = False
				targetToolStripMenuItem.Visible = False
				settingsToolStripMenuItem.Visible = False
				viewToolStripMenuItem.Visible = False
	
''				Dim annotateBitmap As new Bitmap(panelBrowser.Width, panelBrowser.Height)
''				panelBrowser.DrawToBitmap(annotateBitmap, new Rectangle(0, 0, annotateBitmap.Width, annotateBitmap.Height))
'				' alternative "Utilities.CaptureWindow" PictureBoxAnnotate.SetCanvas(Utilities.CaptureWindow(panelBrowser.Handle))
''				PictureBoxAnnotate.SetCanvas(annotateBitmap)
''				annotateBitmap.Dispose()
''				annotateBitmap = Nothing

			' Enter annotation mode
			panelBrowser.Controls.Add(PictureBoxAnnotate) ' panelContents.Controls.Add(PictureBoxAnnotate)
			PictureBoxAnnotate.Visible = True
			PictureBoxAnnotate.AllowPaste = False
			PictureBoxAnnotate.AllowDrop = False
			PictureBoxAnnotate.showQuit = True
			PictureBoxAnnotate.showSave = False
			PictureBoxAnnotate.clearSelection()
				If Not len(Trim(projectFile)) = 0 Then PictureBoxAnnotate.workfolder = Path.GetDirectoryName(projectFile)
				PictureBoxAnnotate.SetName = path.GetFileNameWithoutExtension(sourceFile) & ".png"
			PictureBoxAnnotate.Dock = Dockstyle.Fill
			PictureBoxAnnotate.BringToFront
			Dim bounds As Rectangle
			Dim screenshot As System.Drawing.Bitmap
			Dim graph As Graphics
	
			bounds = panelBrowser.Bounds ' toolStripContainerMainForm.Bounds ' Screen.PrimaryScreen.Bounds
			Dim startPoint As Point = panelBrowser.PointToScreen(New Point(bounds.X, bounds.Y)) ' toolStripContainerMainForm.PointToScreen(New Point(bounds.X, bounds.Y))
			screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
			graph = Graphics.FromImage(screenshot)
			graph.CopyFromScreen(startPoint.X, startPoint.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
			PictureBoxAnnotate.SetCanvas(screenshot)
			screenshot = Nothing
			graph.Dispose()
			splitContainer1.Panel2.Visible = False ' toolStripContainerMainForm.Visible = False
			AnnotateToolStripMenuItem.Checked = annotateImageMode
		Else If annotateImageMode Then
			annotateImageMode = False
			'Exit annotation mode
			PictureBoxAnnotate.Dock = Dockstyle.None
			PictureBoxAnnotate.Visible = False
			splitContainer1.Panel2.Visible = True ' toolStripContainerMainForm.Visible = True
			AnnotateToolStripMenuItem.Checked = annotateImageMode
			panel3.Enabled = True
			treeview1.Enabled = True
				
				annotateModeToolStripMenuItem.Visible = False
				fileToolStripMenuItem.Visible = True
				projectToolStripMenuItem.Visible = projectActive
				ProjectFilterToolStripMenuItem.Visible = projectActive
				toolsToolStripMenuItem.Visible = True
				sourceToolStripMenuItem.Visible = True
				targetToolStripMenuItem.Visible = True
				settingsToolStripMenuItem.Visible = True
				viewToolStripMenuItem.Visible = True
			
		End If
	End Sub
	
	
	Sub AnnotateToolStripMenuItemClick(sender As Object, e As EventArgs)
		Me.Refresh
		startAnnotate.Start()
		' toggleAnnotateMode()
	End Sub

	
	Sub ResetToolStripMenuItemClick(sender As Object, e As EventArgs)
		refreshFileList = False
		Dim actFile As Integer = 0
		openProject()
		showFile(actFile)
		actFile = Nothing
	End Sub

	
	Dim private filterActive As Boolean = False
	
	Sub ActivateFilterToolStripMenuItemClick(sender As Object, e As EventArgs)
		contextMenuStripTreeFilter.Close()
		If ActivateFilterToolStripMenuItem.Text = "Activate Filter" Then
			ActivateFilterToolStripMenuItem.Text = "Deactivate Filter"
			filterActive = True
		Else
			ActivateFilterToolStripMenuItem.Text = "Activate Filter"
			filterActive = false
		End If
		fillFileListTree
	End Sub
	

	
	Sub CopyTitleToClipboardToolStripMenuItemClick(sender As Object, e As EventArgs)
		Dim tmp As String = " "
		Select activePanel
		Case 1
			tmp = textboxTitle1.text
		Case 2
			tmp = textboxTitle2.text
		Case Else
		End Select
		Clipboard.SetText(tmp, TextDataFormat.Text)
		activePanel = 0
	End Sub
	
	Sub ContextMenuStripSearchColorItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)
		For Each item As toolstripItem In contextMenuStripSearchColor.Items
			item.Image = Nothing
		Next
		e.ClickedItem.Image = imagelistSmallicons.Images.Item(13)
		searchColor = e.ClickedItem.Text.ToLower
		
		search1Click(Nothing, Nothing)

	End Sub
	
	
	' IE mode related

' 	IE7 Standards Mode (default)
'	[(HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE)\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION]
'	"VisualQA.exe" = dword 7000 (Hex: 0x1B58)
	
'	IE8 Standards Mode
'	[(HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE)\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION]
'	"VisualQA.exe" = dword 8000 (Hex: 0x1F40)

' 	*Forced* IE8 Standards Mode
'	[(HKEY_CURRENT_USER or HKEY_LOCAL_MACHINE)\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION]
'	"MyApplication.exe" = dword 8888 (Hex: 0x22B8)
	
	Dim shared ieMode As String = "IE7"
	Sub changeIEmode(sender As Object, e As EventArgs)
		getIEmode()
		Dim _sender As ToolStripItem = sender
		If _sender.text = "IE7" Then
			IE7ToolStripMenuItem.Checked = True
			IE7ToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
			IE8ToolStripMenuItem.Checked = False
			IE8ToolStripMenuItem.Image = Nothing
			IE9ToolStripMenuItem.Checked = False
			IE9ToolStripMenuItem.Image = Nothing
			IE10ToolStripMenuItem.Checked = False
			IE10ToolStripMenuItem.Image = Nothing
			IE11ToolStripMenuItem.Checked = False
			IE11ToolStripMenuItem.Image = Nothing
			setIEmode(7)
		Else if _sender.Text = "IE8" Then
			IE8ToolStripMenuItem.Checked = True
			IE8ToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
			IE7ToolStripMenuItem.Checked = False
			IE7ToolStripMenuItem.Image = Nothing
			IE9ToolStripMenuItem.Checked = False
			IE9ToolStripMenuItem.Image = Nothing
			IE10ToolStripMenuItem.Checked = False
			IE10ToolStripMenuItem.Image = Nothing
			IE11ToolStripMenuItem.Checked = False			
			IE11ToolStripMenuItem.Image = Nothing
			setIEmode(8)
		Else if _sender.Text = "IE9" Then
			IE9ToolStripMenuItem.Checked = True
			IE9ToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
			IE7ToolStripMenuItem.Checked = False
			IE7ToolStripMenuItem.Image = Nothing
			IE8ToolStripMenuItem.Checked = False
			IE8ToolStripMenuItem.Image = Nothing
			IE10ToolStripMenuItem.Checked = False
			IE10ToolStripMenuItem.Image = Nothing
			IE11ToolStripMenuItem.Checked = False			
			IE11ToolStripMenuItem.Image = Nothing
			setIEmode(9)
		Else if _sender.Text = "IE10" Then
			IE10ToolStripMenuItem.Checked = True
			IE10ToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
			IE7ToolStripMenuItem.Checked = False
			IE7ToolStripMenuItem.Image = Nothing
			IE8ToolStripMenuItem.Checked = False
			IE8ToolStripMenuItem.Image = Nothing
			IE9ToolStripMenuItem.Checked = False
			IE9ToolStripMenuItem.Image = Nothing
			IE11ToolStripMenuItem.Checked = False			
			IE11ToolStripMenuItem.Image = Nothing			
			setIEmode(10)
		Else if _sender.Text = "IE11" Then
			IE11ToolStripMenuItem.Checked = True
			IE11ToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
			IE7ToolStripMenuItem.Checked = False
			IE7ToolStripMenuItem.Image = Nothing
			IE8ToolStripMenuItem.Checked = False
			IE8ToolStripMenuItem.Image = Nothing
			IE9ToolStripMenuItem.Checked = False
			IE9ToolStripMenuItem.Image = Nothing
			IE10ToolStripMenuItem.Checked = False			
			IE10ToolStripMenuItem.Image = Nothing			
			setIEmode(11)			
		End If
	
	End Sub
	
'	document rendering mode
'	FEATURE_DOCUMENT_COMPATIBLE_MODE
'
'	Name:
'	EXE file
'
'	Value: DWORD (decimal)
'	50000 (Quirks Mode)
'	70000 (IE7 Standards mode)
'	80000 (IE8 Standards mode)

'	browser emulation
' 	FEATURE_BROWSER_EMULATION

'	Name:
'	EXE file
'
'	Value: DWORD decimal type.
'	8000 (IE8)
'	lower (IE7)

	' http://cathval.com/csharp/672
	Dim _firstStart As Boolean = False
	' 2011/07/08 ' Moved Registry.LocalMachine.OpenSubKey to Registry.CurrentUser.OpenSubKey to avoid issues on machines with security restrictions
	Sub getIEmode()
		Try
			Dim pRegKey As RegistryKey
			pRegKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION")
			Dim val As Object = pRegKey.GetValue("VisualQA.exe")
			ieMode = "IE" & val.ToString.Substring(0,1)
			if ieMode = "IE1" then ieMode = "IE10"
			If val.ToString.Substring(0,2) = "11" Then ieMode = "IE11"
			System.Diagnostics.Debug.WriteLine(ieMode & " Mode")
		
			If ieMode = "IE7" Then
				IE7ToolStripMenuItem.Checked = True
				IE7ToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
				IE8ToolStripMenuItem.Checked = False
				IE8ToolStripMenuItem.Image = Nothing
				IE9ToolStripMenuItem.Checked = False
				IE9ToolStripMenuItem.Image = Nothing
				IE10ToolStripMenuItem.Checked = False
				IE10ToolStripMenuItem.Image = Nothing
				IE11ToolStripMenuItem.Checked = False
				IE11ToolStripMenuItem.Image = Nothing				
			Else if ieMode = "IE8" Then
				IE8ToolStripMenuItem.Checked = True
				IE8ToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
				IE7ToolStripMenuItem.Checked = False
				IE7ToolStripMenuItem.Image = Nothing
				IE9ToolStripMenuItem.Checked = False
				IE9ToolStripMenuItem.Image = Nothing
				IE10ToolStripMenuItem.Checked = False
				IE10ToolStripMenuItem.Image = Nothing
				IE11ToolStripMenuItem.Checked = False
				IE11ToolStripMenuItem.Image = Nothing				
			Else if ieMode = "IE9" Then
				IE9ToolStripMenuItem.Checked = True
				IE9ToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
				IE7ToolStripMenuItem.Checked = False
				IE7ToolStripMenuItem.Image = Nothing
				IE8ToolStripMenuItem.Checked = False
				IE8ToolStripMenuItem.Image = Nothing
				IE10ToolStripMenuItem.Checked = False
				IE10ToolStripMenuItem.Image = Nothing
				IE11ToolStripMenuItem.Checked = False
				IE11ToolStripMenuItem.Image = Nothing				
			Else if ieMode = "IE10" Then
				IE10ToolStripMenuItem.Checked = True
				IE10ToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
				IE7ToolStripMenuItem.Checked = False
				IE7ToolStripMenuItem.Image = Nothing
				IE8ToolStripMenuItem.Checked = False
				IE8ToolStripMenuItem.Image = Nothing
				IE9ToolStripMenuItem.Checked = False
				IE9ToolStripMenuItem.Image = Nothing
				IE11ToolStripMenuItem.Checked = False
				IE11ToolStripMenuItem.Image = Nothing				
			Else if ieMode = "IE11" Then
				IE11ToolStripMenuItem.Checked = True
				IE11ToolStripMenuItem.Image = imagelistsmallicons.Images.Item(13)
				IE7ToolStripMenuItem.Checked = False
				IE7ToolStripMenuItem.Image = Nothing
				IE8ToolStripMenuItem.Checked = False
				IE8ToolStripMenuItem.Image = Nothing
				IE9ToolStripMenuItem.Checked = False
				IE9ToolStripMenuItem.Image = Nothing
				IE10ToolStripMenuItem.Checked = False
				IE10ToolStripMenuItem.Image = Nothing	
			End If

		Catch e As Exception
			_firstStart = True
			System.Diagnostics.Debug.WriteLine("Registry key not found")
			setIEmode(7)
		End Try
	End Sub
	
	Sub setIEmode(version As Integer)
	
		' check for FeatureControl key
		Dim _key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl", True)
		If _key Is Nothing Then
			' Add one more sub key
			_key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main", True)
			Dim newkey As RegistryKey = _key.CreateSubKey("FeatureControl")
			_key = newKey
		End If
		_key = Nothing
		
		' Documents without DTD are always displayed in DocumentMode: 5
		' Create a new key under HKEY_LOCAL_MACHINE\Software as MCBInc
		Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_DOCUMENT_COMPATIBLE_MODE", True)
		
		If key Is Nothing Then
			' Add one more sub key
			key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl", True)
			Dim newkey As RegistryKey = key.CreateSubKey("FEATURE_DOCUMENT_COMPATIBLE_MODE")
			key = newKey
		End If
		' Set value of key
		key.SetValue("VisualQA.exe", version * 10000) ' DWORD no string
		
		key  = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", True)
		If key Is Nothing Then
			' Add one more sub key
			key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl", True)
			Dim newkey As RegistryKey = key.CreateSubKey("FEATURE_BROWSER_EMULATION")
			key = newKey
		End If
		
		key.SetValue("VisualQA.exe", version * 1000) ' DWORD no string
		
		getIEmode() ' ieMode = "IE" & version.ToString

'		Dim regKey As RegistryKey
'		regKey = Registry.LocalMachine.OpenSubKey("Software", True)
'		regKey.DeleteSubKey("MyApp", True)
'		regKey.Close()
        
        If Not _firstStart Then Msgbox("Mode changed to " & ieMode & ", restart to activate.")
        _firstStart = False

	End Sub

	Sub deleteIEmode()
		Dim regKey As RegistryKey
		regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION",True)
		Try
		regKey.DeleteValue("VisualQA.exe")
		Catch e As Exception
		End Try
		Try
		regKey.DeleteValue("visualQA.exe")
		Catch e As Exception
		End Try
		' regKey.DeleteSubKey("MyApp", True)
		
		regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_DOCUMENT_COMPATIBLE_MODE",True)
		Try
		regKey.DeleteValue("VisualQA.exe")
		Catch e As Exception
		End Try
		Try
		regKey.DeleteValue("visualQA.exe")
		Catch e As Exception
		End Try

		regKey.Close()
	End Sub

	Dim savedSearch As String = Path.Combine(path.GetDirectoryName(application.ExecutablePath),"search.txt")
	
	Sub readSavedSearch(fileIn As String)
		If file.Exists(fileIn) Then
			textbox1.Text = ""
			Try
			Dim sr As StreamReader = New StreamReader(fileIn, system.Text.Encoding.UTF8)
			Do While Not sr.EndOfStream
				
				Dim _tmpArray As String() = sr.ReadLine.split(vbTab)
				Dim _searchTerm As String = _tmpArray(0)
				Dim _searchColor As String = _tmpArray(1)
				If Not _searchColor.Trim().Length = 0 Then
					if Not _searchColor.StartsWith("#") Then _searchColor = "#" & _searchColor
					textbox1.Text = textbox1.Text & "," &  _searchColor & ": " & _searchTerm
				Else
					textbox1.Text = textbox1.Text & "," & _searchTerm
				End If
				textbox1.Text = textbox1.Text.TrimStart(",")
			Loop
			sr.Close()
			sr.Dispose()
			sr = Nothing
			
			searchPanel.Visible = True
			ShowSearchToolStripMenuItem.Text = "Hide search"
			
			Catch e As Exception
			End Try
		End If
	End Sub
	
	
	Sub SaveSearchToolStripMenuItemClick(sender As Object, e As EventArgs)
		Dim sw As StreamWriter = New StreamWriter(savedSearch, False,  system.Text.Encoding.UTF8)
		For each _searchString As String in splitLine(textbox1.Text)
	        If not len(Trim(_searchString)) = 0 Then
	        	Dim _searchColor As String = ""
	        	If _searchString.StartsWith("#") And _searchString.Contains(": ") Then
	        		_searchColor = _searchString.Substring(0,_searchString.IndexOf(":")).Trim()
	        		If Not regex.IsMatch(_searchColor,"^[A-F0-9]{6}$") Then _searchColor = _searchColor.Substring(1)
	        		_searchString = _searchString.Substring(_searchString.indexOf(":") + 2)
	        	End If
				sw.WriteLine(_searchString & VbTab & _searchColor)
			End If
		Next
		sw.Flush()
		sw.Close()
		sw.Dispose()
		sw = Nothing
	End Sub
	
	Sub ClearSearchToolStripMenuItemClick(sender As Object, e As EventArgs)
		textbox1.Text = ""
		search1Click(Nothing,Nothing)
	End Sub
	
	Sub DeleteSavedSearchToolStripMenuItemClick(sender As Object, e As EventArgs)
		If file.Exists(savedSearch) Then
			file.Delete(savedSearch)
		End If
	End Sub
	
	Sub ReadSavedSearchToolStripMenuItemClick(sender As Object, e As EventArgs)
		readSavedSearch(savedSearch)
		search1Click(Nothing,Nothing)
	End Sub
	
	Sub SaveSettingsToolStripMenuItemClick(sender As Object, e As EventArgs)
		If file.Exists(application.StartupPath & "\VisualQA.cfg") Then File.Delete(application.StartupPath & "\VisualQA.cfg")
		writeXcfg(application.StartupPath & "\VisualQA.cfg", True)
	End Sub

	
	Sub DEBUGToClipboardToolStripMenuItemClick(sender As Object, e As EventArgs)
		CLipboard.SetText(webbrowser2.Document.Body.Parent.OuterHtml)
	End Sub
	
	Sub PictureBox4Click(sender As Object, e As EventArgs)
		If Not sourcePinned And targetPinned Then
			MessageBox.Show("Target document is already pinned." & VbCrLf & VbCrLf & _
							"You can only pin one document at a time." & VbCrLf & _
							"Unpin Target and try again.", _
							"Note", _
							MessageBoxButtons.OK, _
						    MessageBoxIcon.Information)
			Exit Sub
		End If
		If Not sourcePinned Then
			Picturebox4.Image = imagelist2.Images.Item(1)
			sourcePinned = True
		Else
			Picturebox4.Image = imagelist2.Images.Item(0)
			sourcePinned = False
		End If
	End Sub
	
	Sub PictureBox5Click(sender As Object, e As EventArgs)
		If Not targetPinned And sourcePinned Then
			MessageBox.Show("Source document is already pinned." & VbCrLf & VbCrLf & _
							"You can only pin one document at a time." & VbCrLf & _
							"Unpin Source and try again.", _
							"Note", _
							MessageBoxButtons.OK, _
						    MessageBoxIcon.Information)
			Exit Sub
		End If
		If Not targetPinned Then
			Picturebox5.Image = imagelist2.Images.Item(1)
			targetPinned = True
		Else
			Picturebox5.Image = imagelist2.Images.Item(0)
			targetPinned = False
		End If
	End Sub
	
	Sub PinToolStripMenuItemClick(sender As Object, e As EventArgs)
		If activePanel = 1 Then
			picturebox4Click(Nothing,Nothing)
		Else If activePanel = 2 Then
			picturebox5Click(Nothing,Nothing)
		End If
	End Sub
	
	Sub LoadPicturebox1(imageFile As String)
		Dim fs As System.IO.FileStream
		fs = New System.IO.FileStream(imageFile, IO.FileMode.Open, IO.FileAccess.Read)
		pictureBox1.Image = System.Drawing.Image.FromStream(fs)
		If fitImages then pictureBox1.Image = ResizeImage(System.Drawing.Image.FromStream(fs),panel1Contents.Width,panel1Contents.Height,True,True)
		labelImgInfo1.Text = "" & System.Drawing.Image.FromStream(fs).Width & " x " & System.Drawing.Image.FromStream(fs).Height & " x " & Image.GetPixelFormatSize(System.Drawing.Image.FromStream(fs).PixelFormat) & ""
		fs.Close()
	End Sub

	Sub LoadPicturebox2(imageFile As String)
		Dim fs As System.IO.FileStream
		fs = New System.IO.FileStream(imageFile, IO.FileMode.Open, IO.FileAccess.Read)
		pictureBox2.Image = System.Drawing.Image.FromStream(fs)
		If fitImages then pictureBox1.Image = ResizeImage(System.Drawing.Image.FromStream(fs),panel1Contents.Width,panel1Contents.Height,True,True)
		labelImgInfo2.Text = "" & System.Drawing.Image.FromStream(fs).Width & " x " & System.Drawing.Image.FromStream(fs).Height & " x " & Image.GetPixelFormatSize(System.Drawing.Image.FromStream(fs).PixelFormat) & ""
		fs.Close()
	End Sub
	
	Sub PinToOtherSideToolStripMenuItemClick(sender As Object, e As EventArgs)
		pinningToSource = True
		If sourcePinned Then picturebox4Click(Nothing,Nothing)
		sourceFile = webbrowser2.Url.LocalPath
		
		Select Case Mid(Path.GetExtension(sourceFile),2).ToLower
		Case "bmp", "png", "jpg", "gif"
			Try
				Dim fs As System.IO.FileStream
				fs = New System.IO.FileStream(sourceFile, IO.FileMode.Open, IO.FileAccess.Read)
				pictureBox1.Image = System.Drawing.Image.FromStream(fs)
				if fitImages then pictureBox1.Image = ResizeImage(System.Drawing.Image.FromStream(fs),panel1Contents.Width,panel1Contents.Height,True,True)
				labelImgInfo1.Text = "" & System.Drawing.Image.FromStream(fs).Width & " x " & System.Drawing.Image.FromStream(fs).Height & " x " & Image.GetPixelFormatSize(System.Drawing.Image.FromStream(fs).PixelFormat) & ""
				fs.Close()
				picturebox4Click(Nothing,Nothing)
			Catch ex As Exception
			End Try
		Case Else
			webBrowser1.Navigate(New Uri(sourceFile))
		End Select
	End Sub
	
	
	
	Sub ToolStripMenuItem3Click(sender As Object, e As EventArgs)
		pinningToTarget = True
		If targetPinned Then picturebox5Click(Nothing,Nothing)
		targetFile = webbrowser1.Url.LocalPath
		
		Select Case Mid(Path.GetExtension(targetFile),2).ToLower
		Case "bmp", "png", "jpg", "gif"
			Try
				Dim fs As System.IO.FileStream
				fs = New System.IO.FileStream(targetFile, IO.FileMode.Open, IO.FileAccess.Read)
				pictureBox2.Image = System.Drawing.Image.FromStream(fs)
				if fitImages then pictureBox2.Image = ResizeImage(System.Drawing.Image.FromStream(fs),panel2Contents.Width,panel2Contents.Height,True,True)
				labelImgInfo2.Text = "" & System.Drawing.Image.FromStream(fs).Width & " x " & System.Drawing.Image.FromStream(fs).Height & " x " & Image.GetPixelFormatSize(System.Drawing.Image.FromStream(fs).PixelFormat) & ""
				fs.Close()
				picturebox5Click(Nothing,Nothing)
			Catch ex As Exception
			End Try
		Case Else
			webBrowser2.Navigate(New Uri(targetFile))
		End Select
	End Sub
	
	Sub ContextMenuStripPinToTargetOpening(sender As Object, e As System.ComponentModel.CancelEventArgs)
		toolStripMenuItem3.Enabled = Not sourcePinned
	End Sub
	
	Sub ContextMenuStripPinToSourceOpening(sender As Object, e As System.ComponentModel.CancelEventArgs)
		pinToOtherSideToolStripMenuItem.Enabled = Not targetPinned
	End Sub
	
	Public Sub pictureBackGroundColor(colorValue As System.Drawing.Color)
        picturebox1.BackColor = colorValue
        panel1Contents.BackColor = colorValue
        picturebox2.BackColor = colorValue
        panel2Contents.BackColor = colorValue
	End Sub
	
		' helpers for colorDialog
		' The ColorDialog's CustomColors property identifies colors via BGR (Blue, green, red)
		Private Function ToNativeColor(argbColor As Integer) As Integer
			Dim nativeColor As Integer = argbColor And &Hffffff
			'strip out alpha
			Dim r As Integer = (nativeColor And &Hff0000) >> 16
			Dim g As Integer = (nativeColor And &Hff00) >> 8
			Dim b As Integer = nativeColor And &Hff
			nativeColor = (b << 16) + (g << 8) + r
			Return nativeColor
		End Function
		Private Function ToNativeColor(color As Color) As Integer
			Return ToNativeColor(color.ToArgb())
		End Function
	
	
	Sub BackgroundColorToolStripMenuItemClick(sender As Object, e As EventArgs)
		' set default dialog color(s) to the custom color area
		colorDialog1.CustomColors = New Integer() {ToNativeColor(color.FromArgb(202,212,227)),ToNativeColor(MainForm.DefaultBackColor)}
		colorDialog1.Color = picturebox1.BackColor
	    If colorDialog1.ShowDialog() = DialogResult.OK Then
	       imageBackGroundColor = colorDialog1.Color
	       pictureBackGroundColor(imageBackGroundColor)
	    End If
	End Sub

	Dim _splitterDistance As Integer = 230
	Sub SplitContainerContentsMouseDoubleClick(sender As Object, e As MouseEventArgs)
		If Not SplitContainerContents.SplitterDistance < 4 Then
			_splitterDistance = SplitContainerContents.SplitterDistance
			SplitContainerContents.SplitterDistance = 0
			CollapseToolStripMenuItem.Text = "Expand"
		Else
			SplitContainerContents.SplitterDistance = _splitterDistance
			CollapseToolStripMenuItem.Text = "Collapse"
		End If
		
	End Sub
	
	Sub CollapseToolStripMenuItemClick(sender As Object, e As EventArgs)
		If Not SplitContainerContents.SplitterDistance < 4 Then
			_splitterDistance = SplitContainerContents.SplitterDistance
			SplitContainerContents.SplitterDistance = 0
			CollapseToolStripMenuItem.Text = "Expand"
		Else
			SplitContainerContents.SplitterDistance = _splitterDistance
			CollapseToolStripMenuItem.Text = "Collapse"
		End If
	End Sub
	
		' add handle to splitcontainer
		Sub SplitContainerContentsPaint(sender As Object, e As PaintEventArgs)
			Dim control As Object = TryCast(sender, SplitContainer)
			'paint the three dots'
			Dim points As Point() = New Point(2) {}
			Dim pointRect As Rectangle = Rectangle.Empty
			Dim w As Integer = control.Width
			Dim h As Integer = control.Height
			Dim d As Integer = control.SplitterDistance
			Dim sW As Integer = control.SplitterWidth

			'calculate the position of the points'
			If control.Orientation = Orientation.Horizontal Then
				points(0) = New Point((w \ 2), d + (sW \ 2))
				points(1) = New Point(points(0).X - 10, points(0).Y)
				points(2) = New Point(points(0).X + 10, points(0).Y)
				pointRect = New Rectangle(points(1).X - 2, points(1).Y - 2, 25, 5)
			Else
				points(0) = New Point(d + (sW \ 2), (h \ 2))
				points(1) = New Point(points(0).X, points(0).Y - 10)
				points(2) = New Point(points(0).X, points(0).Y + 10)
				pointRect = New Rectangle(points(1).X - 2, points(1).Y - 2, 5, 25)
			End If

			For Each p As Point In points
				p.Offset(-2, -2)
				e.Graphics.FillEllipse(SystemBrushes.ControlDark, New Rectangle(p, New Size(3, 3)))

				p.Offset(1, 1)
				e.Graphics.FillEllipse(SystemBrushes.ControlLight, New Rectangle(p, New Size(3, 3)))
			Next

		End Sub

	Dim annotateDelay As Integer = 0
	Sub TimerAnnotateTick(sender As Object, e As EventArgs)
		annotateDelay = annotateDelay + 100
		If annotateDelay >= 500 Then
			startAnnotate.Stop()
			annotateDelay = 0
			toggleAnnotateMode()
		End If
	End Sub
	
	Dim scrollBarCorrection As Integer = 0
	
	
	Sub setBrowserSize(Optional widthInt As Integer = 0, Optional heightInt As Integer = 0, Optional showSize As Boolean = False)
		if widthInt = 0 Then widthInt = WebBrowser1.Width

		Dim scrollBarWidth As Integer = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth
		
		scrollBarCorrection = 0
		If excludeScrollbarToolStripMenuItem.Checked Then _
			scrollBarCorrection = scrollBarWidth
			
		If WebBrowser1.Width > widthInt
			Dim sizeDiff As Integer = WebBrowser1.Width - widthInt
			Me.Width = Me.Width - (sizeDiff * 2) + (scrollBarCorrection * 2)
		Else
			Dim sizeDiff As Integer = widthInt - WebBrowser1.Width
			me.Width = me.Width + (sizeDiff * 2) + (scrollBarCorrection * 2)
		End IF

		labelSize1.Text = (WebBrowser1.Width - scrollBarCorrection).ToString() & " pixels"
		labelSize2.Text = (WebBrowser2.Width - scrollBarCorrection).ToString() & " pixels"
		
		If showSize Then
			ShowBrowserWidthToolStripMenuItem.Checked = True
			labelSize1.Visible = True
			labelSize2.Visible = True
		End If

	End Sub
	
	Sub ToolStripMenuItem10Click(sender As ToolStripMenuItem, e As EventArgs)
'		If sender.Name = "toolStripMenuItemSize1" Then
'			toolStripMenuItemSize1.Image = imagelistsmallicons.Images.Item(13)
'			toolStripMenuItemSize2.Image = Nothing
'			toolStripMenuItemSize3.Image = Nothing
'			toolStripMenuItemSize4.Image = Nothing			
'		Else If sender.Name = "toolStripMenuItemSize2"	
'			toolStripMenuItemSize2.Image = imagelistsmallicons.Images.Item(13)
'			toolStripMenuItemSize1.Image = Nothing
'			toolStripMenuItemSize3.Image = Nothing
'			toolStripMenuItemSize4.Image = Nothing				
'		Else If sender.Name = "toolStripMenuItemSize3"
'			toolStripMenuItemSize3.Image = imagelistsmallicons.Images.Item(13)
'			toolStripMenuItemSize2.Image = Nothing
'			toolStripMenuItemSize1.Image = Nothing
'			toolStripMenuItemSize4.Image = Nothing				
'		Else If sender.Name = "toolStripMenuItemSize4"	
'			toolStripMenuItemSize4.Image = imagelistsmallicons.Images.Item(13)
'			toolStripMenuItemSize2.Image = Nothing
'			toolStripMenuItemSize3.Image = Nothing
'			toolStripMenuItemSize1.Image = Nothing				
'		End If
		setBrowserSize(Cint(sender.Text),True)
	End Sub
	
	Sub ExcludeScrollbarToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		setBrowserSize()
	End Sub
	
	
	Sub ShowBrowserWidthToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		If ShowBrowserWidthToolStripMenuItem.Checked Then
			If excludeScrollbarToolStripMenuItem.Checked Then scrollBarCorrection = scrollBarWidth
			labelSize1.Text = (WebBrowser1.Width - scrollBarCorrection).ToString() & " pixels"
			labelSize2.Text = (WebBrowser2.Width - scrollBarCorrection).ToString() & " pixels"
			labelSize1.Visible = True
			labelSize2.Visible = True
		Else
			labelSize1.Visible = False
			labelSize2.Visible = False
		End If
	End Sub

			End Class
			
			Public Class ParseFolder
				
				' define local variables
				Private folderIn, folderOut As String
				Private processSubfolders As Boolean
				
				Private fileFilter, filterType As String
				Private allFiles As String  = "All files (*.*)"
				
				Private totalFiles, fileCount As Integer
				Private countTotal As Boolean = False ' not being used
				
				Dim extensionsArray as String() = Nothing
				Dim includeArray As String() = Nothing
				Dim ignoreArray As String() = Nothing
				
				Private ignorelist, includelist As String
				
				' the array with files
				Public outputFiles As String()
				
				
				' define properties of class
				Property Input() As String
					Get
						Return folderIn
					End Get
					Set (ByVal textIn As String)
						folderIn = textIn
					End Set
				End Property
				
				Property Output() As String
					Get
						Return folderOut
					End Get
					Set (ByVal textIn As String)
						folderOut = textIn
					End Set
				End Property
				
				Property Subfolders() As Boolean
					Get
						Return processSubfolders
					End Get
					Set (ByVal boolIn As Boolean)
						processSubfolders = boolIn
					End Set
				End Property
				
				
				
				Property Filter() As String
					Get
						Return fileFilter
					End Get
					Set (ByVal textIn As String)
						
						fileFilter = textIn
						
						' check type of filter
						If NOT Mid(fileFilter,1,1) = """" AND NOT Mid(fileFilter,Len(fileFilter),1) = """" Then
							' create extensions array
							filterType = "EXTENSION"
							Dim separatorChar As Char = ";"C
							Dim extList As String = lcase(fileFilter)
							
							' remove descriptive file comments
							extList = RegEx.Replace(extList,".*?(\()(.*?)(\))","${2}")
							
							' clean up extension list
							extList = Replace(extList,lcase(allFiles),"")
							extList = Replace(extList," ","")
							extList = Replace(extList,".","")
							extList = Replace(extList,"*","")
							extList = Replace(extList,"?","")
							fileFilter = "*." & Replace(extList,";",";*.")
							
							If len(extList) = 0 Then fileFilter = allFiles
							If len(extList) = 0 Then extList = allFiles
							
							extensionsArray = extList.Split(separatorChar)
							
						Else
							filterType = "REGULAR EXPRESSION"
							fileFilter = Mid(fileFilter,2,Len(fileFilter)-2)
						End If
						
						totalFiles = 0
						If countTotal Then countFiles(folderIn)
						
					End Set
				End Property
				
				Private separatorChar As Char = ","C
				
				Property ignore() As String
					Get
						Return ignorelist
					End Get
					Set (ByVal textIn As String)
						ignorelist = textIn
						if len(ignorelist) > 0 then ignoreArray = ignorelist.Split(separatorChar)
					End Set
				End Property
				
				
				Property include() As String
					Get
						Return includelist
					End Get
					Set (ByVal textIn As String)
						includelist = textIn
						if len(includelist) > 0 then includeArray = includelist.Split(separatorChar)
					End Set
				End Property
				
				ReadOnly Property total() As Integer
					Get
						Return totalFiles
					End Get
				End Property
				
				ReadOnly Property count() As Integer
					Get
						Return fileCount
					End Get
				End Property
				
				' actions for class
				Public Sub run()
					
					' Redim outputFiles(0)
					
					fileCount = 0
					itterateFolders(folderIn)
					
				End Sub
				
				
				#Region " Process folders "
				' process folders
				Private Sub itterateFolders(folderIn As String)
					
					' check input
					If Directory.Exists(folderIn) then
						
						' itterate through files
						itterateFiles(folderIn)
						
						If processSubfolders = True then
							' itterate through subfolders in folder
							Dim activedir As String
							Dim subdirs() As String = Directory.GetDirectories(folderIn)
							If Not subdirs.Length = 0 then
								For Each activedir In subdirs
									
									itterateFolders(activedir)
									
								Next
							End if
						End If ' process sub-folders
						
					End If
					
				End Sub
				# End Region
				
				#Region " Process contents of folder "
				Private Sub itterateFiles(folderIn As String)
					
					' check input
					If Directory.Exists(folderIn) Then
						
						' itterate through files in folder
						Dim activefile As String
						Dim allfiles() As String = Directory.GetFiles(folderIn)
						If Not allfiles.Length = 0 then
							For Each activefile In allfiles
								processFile(activefile)
							Next
						End If
					End If
					
				End Sub
				# End Region
				
				#Region " Process file "
				Private Sub processFile(fileIn As String)
					
					' get file information
					Dim fileExtension As String = Mid(Path.GetExtension(fileIn),2)
					Dim fileBasename as String = Path.GetFileNameWithoutExtension(fileIn)
					Dim filePath as string = Path.GetDirectoryName(fileIn)
					Dim fileName as String = Path.GetFileName(fileIn)
					
					Dim fileOk As Boolean = False
					If Mid(UCase(filterType),1,3) = "EXT" then
						' process only accepted extensions
						If Array.IndexOf(extensionsArray, lcase(fileExtension)) >= 0 Or extensionsArray(0) = allFiles then fileOk = True
					Else ' "REG"
						Dim r As New Regex(fileFilter)
						Dim m As Match = r.Match(FileName)
						If m.Success Then fileOk = True
					End If
					
					' check include and ignore list
					If fileOK Then ' file type accepted
						If len(includelist) > 0 Then ' check include
							fileOK = False
							For n As Integer = 0 to includeArray.GetUpperBound(0)
								If Instr(lcase(fileIn), lcase(includeArray(n))) > 0 Then
									fileOK = True ' include
									Exit For
								End If
							Next
						End If
						
						If len(ignorelist) > 0 Then ' check ignore
							For n As Integer = 0 to ignoreArray.GetUpperBound(0)
								If Instr(lcase(fileIn), lcase(ignoreArray(n))) > 0 Then
									fileOK = False ' ignore
									Exit For
								End If
							Next
						End If
					End If
					
					If fileOk Then
						
						fileCount = fileCount + 1
						
						
						'				' target file
						'				Dim targetFile As String
						'				If Not Lcase(Trim(folderIn)) = Lcase(Trim(folderOut)) then
						'				  	targetFile = Replace(fileIn, folderIn, folderOut)
						'				Else
						'					targetFile = fileIn
						'				End If
						
						' ----------------------------------------
						
						ReDim Preserve outputFiles(fileCount - 1)
						outPutFiles(fileCount - 1) = fileIn
						System.Diagnostics.Debug.Print(fileCount & " : " & fileIn)
						' ----------------------------------------
						
					End If
					
				End Sub
				# End Region
				
				#Region  " Count all files in folder "
				Private Sub countFiles(folderIn As String)
					
					' check input
					If Directory.Exists(folderIn) then
						
						' count files
						' totalFiles = totalFiles + Directory.GetFiles(folderIn).Length
						' itterate through files in folder
						Dim currentfile As String
						Dim folderInFiles() As String = Directory.GetFiles(folderIn)
						Dim fileExt As String
						
						For Each currentfile In folderInFiles
							fileExt= Mid(Path.GetExtension(currentfile),2)
							If Mid(Ucase(filterType),1,3) = "EXT" then
								If Array.IndexOf(extensionsArray, lcase(fileExt)) >= 0 Or extensionsArray(0) = allFiles Then _
										totalFiles = totalFiles + 1
								Else ' "REG"
									Dim r As New Regex(fileFilter)
									Dim m As Match = r.Match(Path.GetFileName(currentfile))
									If m.Success Then totalFiles = totalFiles + 1
								End If
							Next
							
							If processSubfolders = True then
								' itterate through subfolders in folder
								Dim activedir As String
								Dim subdirs() As String = Directory.GetDirectories(folderIn)
								For Each activedir In subdirs
									
									countFiles(activedir)
									
								Next
							End If ' process sub-folders
							
							
						End If
						
					End Sub
					
					# End Region
					
				End Class

				
		Public NotInheritable Class Utilities
			
			' Dim SRCCOPY As Integer  = 13369376
			
			Private Sub New()
			End Sub
			Public Shared Function CaptureScreen() As Image
				Return CaptureWindow(User32.GetDesktopWindow())
			End Function

			Public Shared Function CaptureWindow(handle As IntPtr) As Image

				Dim hdcSrc As IntPtr = User32.GetWindowDC(handle)

				Dim windowRect As New RECT()
				User32.GetWindowRect(handle, windowRect)

				Dim width As Integer = windowRect.right - windowRect.left
				Dim height As Integer = windowRect.bottom - windowRect.top

				Dim hdcDest As IntPtr = Gdi32.CreateCompatibleDC(hdcSrc)
				Dim hBitmap As IntPtr = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height)

				Dim hOld As IntPtr = Gdi32.SelectObject(hdcDest, hBitmap)
				Gdi32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, _
					0, 0, 13369376)
				Gdi32.SelectObject(hdcDest, hOld)
				Gdi32.DeleteDC(hdcDest)
				User32.ReleaseDC(handle, hdcSrc)

				Dim image__1 As Image = Image.FromHbitmap(hBitmap)
				Gdi32.DeleteObject(hBitmap)

				Return image__1
			End Function

		End Class

		Public Class Gdi32
			<DllImport("gdi32.dll")> _
			Public Shared Function BitBlt(hObject As IntPtr, nXDest As Integer, nYDest As Integer, nWidth As Integer, nHeight As Integer, hObjectSource As IntPtr, _
			nXSrc As Integer, nYSrc As Integer, dwRop As Integer) As Boolean
			End Function
			<DllImport("gdi32.dll")> _
			Public Shared Function CreateCompatibleBitmap(hDC As IntPtr, nWidth As Integer, nHeight As Integer) As IntPtr
			End Function
			<DllImport("gdi32.dll")> _
			Public Shared Function CreateCompatibleDC(hDC As IntPtr) As IntPtr
			End Function
			<DllImport("gdi32.dll")> _
			Public Shared Function DeleteDC(hDC As IntPtr) As Boolean
			End Function
			<DllImport("gdi32.dll")> _
			Public Shared Function DeleteObject(hObject As IntPtr) As Boolean
			End Function
			<DllImport("gdi32.dll")> _
			Public Shared Function SelectObject(hDC As IntPtr, hObject As IntPtr) As IntPtr
			End Function
		End Class

		Public NotInheritable Class User32
			Private Sub New()
			End Sub
			<DllImport("user32.dll")> _
			Public Shared Function GetDesktopWindow() As IntPtr
			End Function
			<DllImport("user32.dll")> _
			Public Shared Function GetWindowDC(hWnd As IntPtr) As IntPtr
			End Function
			<DllImport("user32.dll")> _
			Public Shared Function GetWindowRect(hWnd As IntPtr, ByRef rect As RECT) As IntPtr
			End Function
			<DllImport("user32.dll")> _
			Public Shared Function ReleaseDC(hWnd As IntPtr, hDC As IntPtr) As IntPtr
			End Function
		End Class

		<StructLayout(LayoutKind.Sequential)> _
		Public Structure RECT
			Public left As Integer
			Public top As Integer
			Public right As Integer
			Public bottom As Integer
		End Structure

			
				
				
Public Class ExWebBrowser
    Inherits WebBrowser

    Private WithEvents Body As HtmlElement

    Public Shadows Event MouseDown(ByVal e As MouseEventArgs)
    Public Shadows Event MouseUp(ByVal e As MouseEventArgs)
    Public Shadows Event MouseEnter(ByVal e As MouseEventArgs)
    Public Shadows Event MouseLeave(ByVal e As MouseEventArgs)
    Public Shadows Event MouseMove(ByVal e As MouseEventArgs)
    Public Shadows Event MouseOver(ByVal e As MouseEventArgs)

    Protected Overrides Sub OnDocumentCompleted(ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs)
        Me.Body = Me.Document.Body
        MyBase.OnDocumentCompleted(e)
    End Sub

    Private Sub Body_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs) Handles Body.MouseDown
        RaiseEvent MouseDown(New MouseEventArgs(e.MouseButtonsPressed, 1, e.ClientMousePosition.X, e.ClientMousePosition.Y, 0))
    End Sub

    Private Sub Body_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs) Handles Body.MouseUp
        RaiseEvent MouseUp(New MouseEventArgs(e.MouseButtonsPressed, 1, e.ClientMousePosition.X, e.ClientMousePosition.Y, 0))
    End Sub

    Private Sub Body_MouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs) Handles Body.MouseEnter
        RaiseEvent MouseEnter(New MouseEventArgs(Windows.Forms.MouseButtons.None, 0, e.ClientMousePosition.X, e.ClientMousePosition.Y, 0))
    End Sub

    Private Sub Body_MouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs) Handles Body.MouseLeave
        RaiseEvent MouseLeave(New MouseEventArgs(Windows.Forms.MouseButtons.None, 0, e.ClientMousePosition.X, e.ClientMousePosition.Y, 0))
    End Sub

    Private Sub Body_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs) Handles Body.MouseMove
        RaiseEvent MouseMove(New MouseEventArgs(Windows.Forms.MouseButtons.None, 0, e.ClientMousePosition.X, e.ClientMousePosition.Y, 0))
    End Sub

    Private Sub Body_MouseOver(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs) Handles Body.MouseOver
        RaiseEvent MouseOver(New MouseEventArgs(Windows.Forms.MouseButtons.None, 0, e.ClientMousePosition.X, e.ClientMousePosition.Y, 0))
    End Sub

End Class

'about.html
'<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
'<html>
'<head><title>About Visual QA</title></head>
'<body>
'<h3>About Visual QA</h3>
'<p>
'<script type="text/javascript"><!-- 
'if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)){ // test for MSIE x.x;
' var ieversion=new Number(RegExp.$1) // capture x.x portion and store as a number
' if (ieversion>=10)
'  document.write("You are viewing content in <b>IE10</b> mode.")
' else if (ieversion>=9)
'  document.write("You are viewing content in <b>IE9</b> mode.")
' else if (ieversion>=8)
'  document.write("You are viewing content in <b>IE8</b> mode.")
' else if (ieversion>=7)
'  document.write("You are viewing content in <b>IE7</b> mode.")
' else if (ieversion>=6)
'  document.write("IE mode.")
'} else {
'  document.write("Cannot detect browser mode.")
'}  
'  // document.write(navigator.appName + " " + navigator.appVersion + "<br /><br />")
'  document.write("<br /><br />DocumentMode: " + document.documentMode)
'  document.write("<br />UserAgent: " + navigator.userAgent)
'//--></script>
'</p>
'<p align="left"><a href="javascript: history.go(-1)">Back</a></p>
'</body>
'</html>
				
				
