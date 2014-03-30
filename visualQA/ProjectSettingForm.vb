
Imports System.IO

Public Partial Class ProjectSettingForm
	
	Public sourceFolder As String
	Public targetFolder As String
	Public subfolders As Boolean = False
	Public filter As String
	
	Public ignore As String
	Public include As String
	
	Public highlightSpanTags As String 
	
	public OK as Boolean = False
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()

		updateFilter
		textFilter.Text = standardFilter
		
	End Sub
	
	Sub ButtonOkClick(sender As Object, e As System.EventArgs)

		' if empty dialog is OKed
		If Len(Trim(textTargetFolder.text)) = 0 And _
			Len(Trim(textSourceFolder.text)) = 0 Then
			Me.Close
		End If		

		' verify minimum requirements
		If Not Directory.Exists(textTargetFolder.text) Or _
			Not Directory.Exists(textSourceFolder.text) Then
			
			LabelWarning.Text = "Problem or empty input folders detected. Check and correct the items shown in red or click Cancel."
			Exit Sub
		End If
		
		sourceFolder = Trim(textSourceFolder.Text)
		if Microsoft.VisualBasic.Right(sourceFolder,1) = "\" then sourceFolder = Microsoft.VisualBasic.Left(sourceFolder,len(sourceFolder)-1)
		targetFolder = Trim(textTargetFolder.Text)
		if Microsoft.VisualBasic.Right(targetFolder,1) = "\" then targetFolder = Microsoft.VisualBasic.Left(targetFolder,len(targetFolder)-1)
		subfolders = checkbox1.checked
		filter = Trim(textFilter.Text)
		OK = True
		Me.close
	End Sub
	
	Sub CheckBox1CheckedChanged(sender As Object, e As System.EventArgs)
		checkBox2.Checked = checkbox1.checked
	End Sub
	
	Sub TextSourceFolderTextChanged(sender As Object, e As System.EventArgs)
		If Directory.Exists(TextSourceFolder.Text) Then
			TextSourceFolder.ForeColor = Color.Black
		Else
			TextSourceFolder.ForeColor = Color.Gray
		End If
		
		If CheckBoxSourceOnly.Checked then TextTargetFolder.Text = TextSourceFolder.Text
		
	End Sub
	
	Sub ButtonBrowseSourceClick(sender As Object, e As System.EventArgs)
		Dim folderBrowserDialog1 As New FolderBrowserDialog()

	    folderBrowserDialog1.Description = "Select folder containing source files" 
	    folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
	    folderBrowserDialog1.ShowNewFolderButton = False
	    
		if Directory.Exists(textSourceFolder.Text) then _
			folderBrowserDialog1.SelectedPath = textSourceFolder.Text
	       
	    If folderBrowserDialog1.ShowDialog() = dResult_OK Then _ 
			textSourceFolder.text = folderBrowserDialog1.SelectedPath ' DialogResult.OK
	
		folderBrowserDialog1 = Nothing


		If Directory.Exists(textSourceFolder.text) And Len(Trim(textTargetFolder.Text)) = 0 then
			If Not Directory.GetParent(textSourceFolder.text) Is Nothing then
				textTargetFolder.text = Directory.GetParent(textSourceFolder.text).ToString
				textTargetFolder.ForeColor = Color.Gray
			End If
		End if	
	
	End Sub
	
	
	Const dResult_OK As DialogResult = DialogResult.OK
	
	Sub ButtonBrowseTargetClick(sender As Object, e As System.EventArgs)
		Dim folderBrowserDialog1 As New FolderBrowserDialog()
		
		
	    folderBrowserDialog1.Description = "Select folder containing target files" 
	    folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
	    folderBrowserDialog1.ShowNewFolderButton = False
	    
		if Directory.Exists(textTargetFolder.Text) then _
			folderBrowserDialog1.SelectedPath = textTargetFolder.Text
	       
	    If folderBrowserDialog1.ShowDialog() = dResult_OK Then _ 
			textTargetFolder.text = folderBrowserDialog1.SelectedPath ' DialogResult.OK
	
		folderBrowserDialog1 = Nothing

	End Sub
	
	
	Sub TextSourceFolderLeave(sender As Object, e As System.EventArgs)
		If Directory.Exists(textSourceFolder.text) And Len(Trim(textTargetFolder.Text)) = 0 And Not CheckBoxSourceOnly.Checked then
			If Not Directory.GetParent(textSourceFolder.text) Is Nothing then
				textTargetFolder.text = Directory.GetParent(textSourceFolder.text).ToString
				textTargetFolder.ForeColor = Color.Gray
			End If
		End If	
		
		If Not Directory.Exists(textSourceFolder.text) then textSourceFolder.ForeColor = Color.Red
	End Sub
	
	Sub TextSourceFolderEnter(sender As Object, e As System.EventArgs)
		textSourceFolder.ForeColor = Color.Black
	End Sub
	
	Sub TextTargetFolderEnter(sender As Object, e As System.EventArgs)
		textTargetFolder.ForeColor = Color.Black
	End Sub
	
	Sub TextTargetFolderLeave(sender As Object, e As System.EventArgs)
		If Not Directory.Exists(textTargetFolder.text) then textTargetFolder.ForeColor = Color.Red
		
	End Sub
	
	dim standardFilter As String
	
	Sub updateFilter()
		standardFilter = ""
		If CheckBoxHTML.Checked Then standardFilter = "*.htm;*.html"
		If CheckBoxImages.Checked Then standardFilter = standardFilter & ";*.bmp;*.gif;*.jpg;*.png"
		If CheckBoxXml.Checked Then standardFilter = standardFilter & ";*.xml"
		If Microsoft.VisualBasic.Left(standardFilter,1) = ";" then standardFilter = Mid(standardFilter,2)
	End Sub
	
	Sub CheckBoxHTMLCheckedChanged(sender As Object, e As System.EventArgs)
		updateFilter
		If Not TextFilter.Enabled then TextFilter.Text = standardFilter 
		
	End Sub
	
	Sub CheckBoxImagesCheckedChanged(sender As Object, e As System.EventArgs)
		updateFilter
		If Not TextFilter.Enabled then TextFilter.Text = standardFilter 
		
	End Sub
	
	Sub CheckBoxXmlCheckedChanged(sender As Object, e As System.EventArgs)
		updateFilter
		If Not TextFilter.Enabled Then TextFilter.Text = standardFilter 
		
	End Sub
	
	Sub CheckBox3CheckedChanged(sender As Object, e As System.EventArgs)
		If Checkbox3.Checked Then
			CheckBoxHTML.Enabled = False
			CheckBoxImages.Enabled = False
			CheckBoxXML.Enabled = False
			textFilter.Enabled = True
			textFilter.ForeColor = Color.DodgerBlue
		Else
			CheckBoxHTML.Enabled = True
			CheckBoxImages.Enabled = True
			CheckBoxXML.Enabled = True
			textFilter.Enabled = False
			updateFilter
			TextFilter.Text = standardFilter			
			textFilter.ForeColor = Color.Gray
		End If
	End Sub
	

	
	Sub CheckBoxSourceOnlyCheckedChanged(sender As Object, e As System.EventArgs)
		groupBox2.Enabled = Not CheckBoxSourceOnly.Checked
		If CheckBoxSourceOnly.Checked then TextTargetFolder.Text = TextSourceFolder.Text
	End Sub
	
	Sub ProjectSettingFormLoad(sender As Object, e As System.EventArgs)
		' fill form with initial values
		If Len(Trim(sourceFolder)) > 0 Then textSourceFolder.text = sourceFolder
		If Len(Trim(targetFolder)) > 0 Then textTargetFolder.text = targetFolder
		If subFolders then checkBox1.Checked = True
		If Len(Trim(sourceFolder)) > 0 AND lcase(textSourceFolder.text) = lcase(textTargetFolder.text) then checkBoxSourceOnly.Checked = True
		
		If InStr(lcase(filter),"*.htm;*.html") then CheckBoxHTML.Checked = True
		If InStr(lcase(filter),"*.bmp;*.gif;*.jpg;*.png") Then CheckBoxImages.Checked = True
		If InStr(lcase(filter),"*.xml") Then CheckBoxXml.Checked = True	
		If Len(Trim(filter)) > 0 Then textFilter.Text = Filter
		
		If Not len(Trim(filter)) = 0 AND Not lcase(Trim(standardFilter)) = lcase(Trim(filter)) then
			Checkbox3.Checked = True
			CheckBoxHTML.Enabled = False
			CheckBoxHTML.Checked = False
			CheckBoxImages.Enabled = False
			CheckBoxImages.Checked = False
			CheckBoxXML.Enabled = False
			CheckBoxXML.Checked = False
		End If	
		
		If Len(Trim(ignore)) > 0 Then textbox1.text = ignore
		If Len(Trim(include)) > 0 Then textbox2.text = include		
		
	End Sub
	
	Sub TextBox1TextChanged(sender As Object, e As System.EventArgs)		
		ignore = Trim(textbox1.text)
	End Sub
	
	Sub TextBox2TextChanged(sender As Object, e As System.EventArgs)	
		include = Trim(textbox2.text)
	End Sub
	
	Sub GroupBox1DragDrop(sender As Object, e As DragEventArgs)
		If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
			Dim droppedFiles() As String
			droppedFiles = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
			If Directory.Exists(droppedFiles(0)) Then
				textSourceFolder.Text = droppedFiles(0)
				If Directory.GetFiles(droppedFiles(0)).Length = 0 And Directory.GetDirectories(droppedFiles(0)).Length > 0 then checkBox1.Checked = True
			End If
		End If	
	End Sub
	
	Sub GroupBox1DragOver(sender As Object, e As DragEventArgs)
		e.Effect = DragDropEffects.None
		
		If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
			Dim droppedFiles() As String
			droppedFiles = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
			If Directory.Exists(droppedFiles(0)) then e.Effect = DragDropEffects.Copy
		Else
			e.Effect = DragDropEffects.None
		End If			
	End Sub
	
	Sub GroupBox2DragDrop(sender As Object, e As DragEventArgs)
		If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
			Dim droppedFiles() As String
			droppedFiles = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
			If Directory.Exists(droppedFiles(0)) Then
				textTargetFolder.Text = droppedFiles(0)
				' If Directory.GetFiles(droppedFiles(0)).Length = 0 And Directory.GetDirectories(droppedFiles(0)).Length > 0 then checkBox2.Checked = True
			End If
		End If			
	End Sub
	
	Sub GroupBox2DragOver(sender As Object, e As DragEventArgs)
		e.Effect = DragDropEffects.None
		
		If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
			Dim droppedFiles() As String
			droppedFiles = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
			If Directory.Exists(droppedFiles(0)) then e.Effect = DragDropEffects.Copy
		Else
			e.Effect = DragDropEffects.None
		End If			
	End Sub
	
	Sub TextBox3TextChanged(sender As Object, e As EventArgs)
		highlightSpanTags = Trim(textbox3.text)		
	End Sub
	
	Dim private _height As Integer = 320
	Sub AdvancedToolStripMenuItemClick(sender As Object, e As EventArgs)
		If Not AdvancedToolStripMenuItem.Checked Then
			AdvancedToolStripMenuItem.Checked = True
			_height = Me.height
			panelAdvanced.Visible = True
			Me.Height = _height + panelAdvanced.Height
		Else
			AdvancedToolStripMenuItem.Checked = False
			panelAdvanced.Visible = False
			Me.Height = _height
		End If
	End Sub
	
	Sub TextFilterLeave(sender As Object, e As EventArgs)
		sender.Text = Trim(sender.text)
		sender.text = Replace(sender.text, ",",";")
		sender.text = Replace(sender.text, " ;",";")
		sender.text = Replace(sender.text, " ;",";")
		sender.text = Replace(sender.text, "; ",";")
		sender.text = Replace(sender.text, "; ",";")
	End Sub
	
	Sub TextLeave(sender As Object, e As EventArgs)
		sender.Text = Trim(sender.text)
		sender.text = Replace(sender.text, " ,",",")
		sender.text = Replace(sender.text, " ,",",")
		sender.text = Replace(sender.text, ", ",",")
		sender.text = Replace(sender.text, ", ",",")		
	End Sub
End Class


