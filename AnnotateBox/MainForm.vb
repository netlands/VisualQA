
Public Partial Class MainForm
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
	End Sub
	

	Dim Private dlgSavePicture As New SaveFileDialog
	Dim Private dlgOpenPicture As New OpenFileDialog	
	
	Sub openToolStripMenuItemClick(sender As Object, e As EventArgs)
        dlgOpenPicture.DefaultExt = "*.png"
        If dlgOpenPicture.ShowDialog() = DialogResult.OK Then
            Try
                AnnotateBox1.Open(dlgOpenPicture.FileName)
                dlgSavePicture.InitialDirectory = dlgOpenPicture.FileName
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If		
	End Sub		
	
	Sub SaveAsToolStripMenuItemClick(sender As Object, e As EventArgs)
        If dlgSavePicture.ShowDialog() = DialogResult.OK Then
            Try
                AnnotateBox1.Save(dlgSavePicture.FileName)
                dlgOpenPicture.InitialDirectory = dlgSavePicture.FileName
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If			
	End Sub
	
	Sub FileToolStripMenuItemDropDownOpening(sender As Object, e As EventArgs)
		If Not AnnotateBox1.file Is Nothing Then
			SaveToolStripMenuItem.Enabled = True
			ClearToolStripMenuItem.Enabled = True
		Else
			SaveToolStripMenuItem.Enabled = False
			ClearToolStripMenuItem.Enabled = False
		End If
	End Sub
	
	Sub SaveToolStripMenuItemClick(sender As Object, e As EventArgs)
		AnnotateBox1.Save(AnnotateBox1.file)
	End Sub
	
	Sub ClearToolStripMenuItemClick(sender As Object, e As EventArgs)
		AnnotateBox1.Clear
	End Sub
	
	Sub AnnotateBox1ImageSaved(fileName As String)
		Me.Text = System.IO.Path.GetFileName(fileName)
	End Sub
End Class
