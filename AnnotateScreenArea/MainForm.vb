Imports System.Runtime.InteropServices
Imports System.Drawing.Image
Imports System.IO

Public Partial Class MainForm
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		' activate double buffering
		Me.SetStyle( _
		 ControlStyles.AllPaintingInWmPaint Or _
		 ControlStyles.UserPaint Or _
		 ControlStyles.DoubleBuffer, _
		 True)
		Me.UpdateStyles()
		
		Me.SetStyle(ControlStyles.ResizeRedraw, True)
		Me.SetStyle(ControlStyles.DoubleBuffer, True)
		Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
		
	
		AddHandler annotatebox1.Quit, AddressOf Quit
		resetArea()
		saveFolder = Application.StartupPath
		
		If Environment.GetCommandLineArgs.Length > 1 Then ' 0 is the app itself
		
			For n As Integer = 1 To Environment.GetCommandLineArgs.Length - 1
				Dim argument As String = Environment.GetCommandLineArgs(n).ToString
			
				If File.Exists(argument) Then
					If "png|gif|jpg".Contains(Path.GetExtension(argument).Trim("."c).tolower()) Then
						
						' transformation automation
						If Path.GetFileNameWithoutExtension(Application.ExecutablePath).ToLower().Contains("area") Then
							
							Dim fs As System.IO.FileStream
							fs = New System.IO.FileStream(argument, IO.FileMode.Open, IO.FileAccess.Read)
							imageFile = System.Drawing.Bitmap.FromStream(fs) ' Image.FromFile(fileName)				
							fs.Dispose()
							fs = Nothing							
							Dim transform As New BasicImaging.transform
							Dim tmp As Bitmap = ImageFile
							
							tmp = transform.Create1bppImageWithErrorDiffusion(tmp)
							If tmp.Width > tmp.Height Then tmp = transform.Rotate(tmp,90)
							
							tmp = transform.Resize(tmp,144,168,True)
							loadImage(tmp)
							tmp.Dispose()
							tmp = Nothing
							
						Else						
							loadImage(argument)
						End If
					End If
				End If
				Exit For
			Next
		End If
				
	End Sub


	Sub Quit()
		Me.Close()
	End Sub

		Private Const vbKeyPrtScr As Integer = 44
		Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Long) As Integer
		
		Sub Timer1Tick(sender As Object, e As EventArgs)
		    If (GetAsyncKeyState(vbKeyPrtScr)) Then
		        Debug.WriteLine("PrtScr Pressed")
				If	button1.Text = "Update" Then
					getAnnotateArea()
					If AutoSaveOnUpdateToolStripMenuItem.Checked Then
						saveArea()
					End If
				End If
			 End If
		End Sub

		' catch keyboard input on entire form
	    Protected Overrides Function ProcessDialogKey(ByVal keyData As System.Windows.Forms.Keys) As Boolean
			Debug.WriteLine(keyData.ToString)
			If button1.Text = "Set" Then
				Select keyData
					Case 38
						me.Top = me.Top - 1
					Case 39
						Me.Left = Me.Left + 1
					Case 40
						Me.Top = Me.Top + 1
					Case 37
						Me.Left = Me.Left - 1
					Case 65574
						me.Top = me.Top - 10
					Case 65575
						Me.Left = Me.Left + 10
					Case 65576
						Me.Top = Me.Top + 10
					Case 65573
						Me.Left = Me.Left - 10
					Case 131110
						Me.Height = Me.Height - 1
					Case 131111
						Me.Width = Me.Width + 1
					Case 131112
						Me.Height = Me.Height + 1
					Case 131109
						Me.Width = Me.Width - 1
					Case 196646
						Me.Height = Me.Height - 10
					Case 196647
						Me.Width = Me.Width + 10
					Case 196648
						Me.Height = Me.Height + 10
					Case 196645
						Me.Width = Me.Width - 10
				End Select
			Else
				Select keyData
					Case 131162 ' Ctrl+Z, Undo
						annotatebox1.Undo()
					Case 131139 ' Ctrl+C, Copy
						annotatebox1.Copy()
					Case 131155 ' Ctrl+S, Save (As)
						annotatebox1.Save()
					Case 131153 ' Ctrl+Q, Quit
						annotatebox1.CloseApp()
					Case 131137 ' Ctrl+A, Select All
						annotatebox1.selectAll()
					' image editing mode specific
					Case 131142 ' Flip content Ctrl+F
						annotatebox1.FlipImage()
				End Select
			End If
		
		End Function

	
	Dim annotateArea As Rectangle
	Dim startPoint As Point

	
	Sub resetArea()
		debug.WriteLine("RESET")
		annotateArea.Width = 0
		annotateArea.Height = 0
		annotatebox1.Visible = False
		Me.Opacity = 1
		me.TransparencyKey = Color.Magenta
		Panel1.BackColor = Color.Magenta
		label1.BackColor = Color.Magenta
		label1.Visible = True
		Me.MaximumSize = New Size(0,0)
		Me.MinimumSize = New Size(0,0)
		Button1.Text = "Set"
		button2.Visible = False
		textbox1.Visible = False
		label1.Text = panel1.Size.Width & " x " & panel1.Size.Height
	End Sub

	
	Sub getAnnotateArea()
		debug.WriteLine("SET")
		
		label1.Visible = False
		panel1.BackColor = color.Magenta
		AnnotateBox1.Visible = True
		
		Dim bounds As Rectangle
		Dim screenshot As System.Drawing.Bitmap
		Dim graph As Graphics
			
		If Not annotateArea.Width = 0 Then
			bounds = annotateArea
		Else
			bounds = panel1.Bounds ' Screen.PrimaryScreen.Bounds
			startPoint = panel1.PointToScreen(New Point(bounds.X, bounds.Y))
		End If
		
		screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
		graph = Graphics.FromImage(screenshot)
		graph.CopyFromScreen(startPoint.X, startPoint.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
		AnnotateBox1.SetCanvas(screenshot)
		screenshot = Nothing
		graph.Dispose()
		panel1.BackColor = color.White
		me.TransparencyKey = Nothing
		Me.MaximumSize = Me.Size
		Me.MinimumSize = Me.Size
		Me.Opacity = 1
		annotateArea = bounds
	End Sub

	
	Sub Button1Click(sender As Object, e As EventArgs)
		
		If button1.Text = "Reset" Then
			Debug.WriteLine("RESET")
			loadImage(imageFile)
			Exit Sub
		End If
		
		If button1.Text = "Set" Then
			button1.Text = "Update"
			button2.Visible = True
			textbox1.Visible = True
		End If
		
		getAnnotateArea()
		
		If AutoSaveOnUpdateToolStripMenuItem.Checked Then
			saveArea()
		End If
		
	End Sub

	
	Sub MainFormResize(sender As Object, e As EventArgs)
		label1.Text = panel1.Size.Width & " x " & panel1.Size.Height
	End Sub
	
	Dim saveFolder As String
	
	Sub SelectDestinationFolderToolStripMenuItemClick(sender As Object, e As EventArgs)
		
		Dim folderBrowserDialog1 As New FolderBrowserDialog

		folderBrowserDialog1.Description = "Select folder to store images."
		folderBrowserDialog1.SelectedPath = saveFolder
		folderBrowserDialog1.ShowNewFolderButton = True

        If folderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            saveFolder= folderBrowserDialog1.SelectedPath
            annotatebox1.workfolder = saveFolder
        End If
		
		
	End Sub
	
	Sub TransparantToolStripMenuItemClick(sender As Object, e As EventArgs)
		If Me.Opacity = 1 Then
			Me.Opacity = 0.5
		Else
			Me.Opacity = 1
		End If
	End Sub
	
	Sub Button2Click(sender As Object, e As EventArgs)
		saveArea()

	End Sub
	
	Dim counter As Integer = 1
	
	Sub saveArea()
		debug.WriteLine("SAVE")
		Dim saveFile As String = textbox1.Text
		If saveFile.Length = 0 then saveFile = "screen_001"
		
		If Not button1.Text = "Reset" Then _
			If not Lcase(saveFile).EndsWith(".png") then saveFile = saveFile & ".png"
		If autoNumberToolStripMenuItem.Checked Or AutoSaveOnUpdateToolStripMenuItem.Checked Then
			While file.Exists(Path.Combine(saveFolder,saveFile))
				counter = counter + 1
				saveFile = Replace(saveFile, Format(counter-1,"000") & ".png",Format(counter,"000") & ".png")
			debug.WriteLine(saveFile)
			End While
		End If
		textBox1.Text = saveFile
		
     	Dim result As DialogResult = Windows.Forms.DialogResult.OK
        
        If File.Exists(Path.Combine(saveFolder,saveFile)) then
        	result = MessageBox.Show("File already exists.  Click OK to overwrite.", "File Exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
        End If
        
        If Not result = Windows.Forms.DialogResult.OK Then
           Exit Sub
        End If
		
		annotatebox1.Save(Path.Combine(saveFolder,saveFile))
	
		If autoUpdateToolStripMenuItem.Checked And autoUpdateToolStripMenuItem.Enabled Then
			getAnnotateArea()
		End If
	
	End Sub
	
	Sub AutoSaveOnUpdateToolStripMenuItemClick(sender As Object, e As EventArgs)
		autoUpdateToolStripMenuItem.Enabled = Not AutoSaveOnUpdateToolStripMenuItem.Checked
		autoNumberToolStripMenuItem.Enabled = Not AutoSaveOnUpdateToolStripMenuItem.Checked
		button2.Enabled = Not AutoSaveOnUpdateToolStripMenuItem.Checked
	End Sub
	
	Sub ResetCounterToolStripMenuItemClick(sender As Object, e As EventArgs)
		counter = 1
	End Sub
	
	Sub ResetAreaToolStripMenuItemClick(sender As Object, e As EventArgs)
		resetArea()
	End Sub

	
	Sub MainFormDragDrop(sender As Object, e As DragEventArgs)
		If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
			Dim droppedFiles() As String
			droppedFiles = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
			For Each droppedFile As String In droppedFiles
				If File.Exists(droppedFile) And "png|gif|jpg".Contains(Path.GetExtension(droppedFiles(0)).ToLower().Trim("."c)) Then
					Debug.WriteLine(droppedFile)
					loadImage(droppedFile)
					Exit For
				End If
			Next
		End If
	End Sub
	
	Dim imageFile As Bitmap = Nothing
	
	Sub loadImage(fileName As String)
		If not File.Exists(fileName) Then Exit Sub
		
		Dim fs As System.IO.FileStream
		fs = New System.IO.FileStream(fileName, IO.FileMode.Open, IO.FileAccess.Read)
		imageFile = System.Drawing.Bitmap.FromStream(fs) ' Image.FromFile(fileName)				
		loadImage(imageFile)
		fs.Dispose()
		fs = Nothing
		Me.text = "Editing " & path.GetFileName(fileName)
		textBox1.Text = path.GetFileName(fileName)
		saveFolder = path.GetDirectoryName(fileName)
		annotatebox1.workfolder = saveFolder
		annotatebox1.imageEditingMode = True
		button1.Text = "Reset"
		button2.Visible = True
		textbox1.Visible = True
		Panel2.ContextMenuStrip = Nothing
		textbox1.ContextMenuStrip = Nothing
		label1.Visible = False
	End Sub
	
	Sub loadImage(image As Bitmap)

		Me.MaximumSize = New Size(0,0)
		Me.MinimumSize = New Size(0,0)

		Dim BorderWidth as Integer = (Me.Width - Me.ClientSize.Width)/2
		Dim TitlebarHeight as Integer = Me.Height - Me.ClientSize.Height - (BorderWidth)
		
'		If imageFile.Width < 300 Then
'			Me.Width = 300 + (BorderWidth * 2)
'		Else
			Me.Width = image.Width + (BorderWidth * 2)
'		End If
		
'		If imageFile.Height < 50 Then
'			Me.Height = 50 + BorderWidth + TitlebarHeight + Panel2.Height
'		Else
			Me.Height = image.Height + BorderWidth + TitlebarHeight + Panel2.Height
'		End If
		
		panel1.BackColor = color.LightGray
		Me.MaximumSize = Me.Size
		Me.MinimumSize = Me.Size
		
		Try
			annotatebox1.Location = New Point(0, 0)
			annotatebox1.Width = image.Width
			annotatebox1.Height = image.Height
			AnnotateBox1.SetCanvas(image)
			annotatebox1.Visible = True
		Catch ex As Exception
			MessageBox.Show(ex.Message, "Error Loading Image", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

	End Sub
	
	
	
	Sub MainFormDragOver(sender As Object, e As DragEventArgs)
		e.Effect = DragDropEffects.None
		
		If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
			Dim droppedFiles() As String
			droppedFiles = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
			If File.Exists(droppedFiles(0)) And "png|gif|jpg".Contains(Path.GetExtension(droppedFiles(0)).ToLower().Trim("."c)) Then
				e.Effect = DragDropEffects.Copy
			End If
		Else
			e.Effect = DragDropEffects.None
		End If
	End Sub
End Class
