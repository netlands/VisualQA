Option Explicit On

Imports System.Math


Public Partial Class AnnotateBox
	Inherits System.Windows.Forms.UserControl

    Private m_OriginalBitmap As Bitmap
    Private m_CurrentBitmap As Bitmap
    Private m_TempBitmap As Bitmap

    Private m_Gr As Graphics
    Private m_Pen As Pen

    Private m_SelectingArea As Boolean
    Private m_X1 As Integer
    Private m_Y1 As Integer
	
	Private undoBuffer As Stack
	Private m_UndoBitmap As Bitmap
	
	Public Shared textSize As Integer = 16
	Public Shared textStyle As System.Drawing.FontStyle = fontstyle.Bold ' fontstyle.Regular
	Public Shared textFont As String = "Arial" ' "Segoe UI"
	
  	Public Shared highlighterWidth As Integer = 4
  	Public Shared highlighterHeight As Integer = 20	
	
	
	Public imageEditingMode As Boolean = False

	Public Shared numberedItemStyle As String = "circle" ' "box" "triangle"
	
' 	Dim transform As New BasicImaging.transform
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		me.Cursor = Cursors.Cross
				
		' Double-buffer
        Me.SetStyle( _
            ControlStyles.AllPaintingInWmPaint Or _
            ControlStyles.UserPaint Or _
            ControlStyles.DoubleBuffer, _
            True)
        Me.UpdateStyles()
        
	End Sub
	
	Public Sub selectAll()
		
		prevTool = tool
		tool = "select"
		me.Cursor = Cursors.Cross
		
		FreehandToolStripMenuItem.Checked = False
		lineToolStripMenuItem.Checked = False
		PixelateToolStripMenuItem.Checked = False
		RectangleToolStripMenuItem.Checked = False
		ellipseToolStripMenuItem.Checked = False
		arrowToolStripMenuItem.Checked = False
		SelectAreaToolStripMenuItem.Checked = True
		TextToolStripMenuItem.Checked = False
		RoundedRectangleToolStripMenuItem.Checked = False
		highlightToolStripMenuItem.Checked = False

        Dim x As Integer = m_OriginalBitmap.Width
        Dim y As Integer = m_OriginalBitmap.Height
	
		' get the rectangle
		_selectionRect = New Rectangle( _
            Min(0, x), _
            Min(0, y), _
            Abs(x), _
            Abs(y))
        ' m_Gr.DrawRectangle(m_Pen, _selectionRect)
		_selectionActive = True
		Me.Refresh
		
	End Sub
	
    ' Start selecting an area.
    Private Sub picCanvasMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
		If e.Button = MouseButtons.Right Then
	        m_X1 = e.X
	        m_Y1 = e.Y
			Exit Sub
		End If
		
		debug.WriteLine(">>> Mouse Down")
	
	activeRect = Nothing
	
				If _arrow Then
					If lineWidth = 1 Then
						arrowHead = New System.Drawing.Drawing2D.AdjustableArrowCap(8,8,False)
					Else
						arrowHead = New System.Drawing.Drawing2D.AdjustableArrowCap(4,4,False)
					End If
				End If
				
		' me.Cursor = Cursors.Cross
		
        ' Make sure we have a picture loaded.
        If m_OriginalBitmap Is Nothing Then Exit Sub

        resetToolStripMenuItem.Enabled = False
        
        m_X1 = e.X
        m_Y1 = e.Y
        
m_X2 = e.X
m_Y2 = e.Y
        
        
        ' set undo bitmap
		m_undoBitmap = m_CurrentBitmap
		' clipboard.SetImage(m_undoBitmap)

        ' Make a copy of the current bitmap
        'and prepare to draw.
        m_TempBitmap = New Bitmap(m_CurrentBitmap)
        m_Gr = Graphics.FromImage(m_TempBitmap)

		Select Case tool
		Case "text"
			t_X1 = m_X1
			t_Y1 = m_Y1
			Me.Refresh
			textbox1.Focus()
		Me.Refresh
		Case "pixelate"
			updateUndoBuffer()

        	m_SelectingArea = True
	        m_Pen = New Pen(Color.Gray, 2)
	        m_Pen.DashStyle = Drawing2D.DashStyle.Dot
		Case "select"
			_selectionrect = Nothing
        	m_SelectingArea = True
' REMOVE	        m_Pen = New Pen(Color.Gray, 1)
' REMOVE	        m_Pen.DashStyle = Drawing2D.DashStyle.Dot
		Case "rectangle", "ellipse", "line", "rounded"
			updateUndoBuffer()
	    	m_SelectingArea = True
	    	_selectionActive = False
 			m_Pen = New Pen(annotateColor, 1)
	        m_Pen.DashStyle = Drawing2D.DashStyle.Dash
			
			If tool = "line" Then
 				m_Pen.StartCap = System.Drawing.Drawing2D.LineCap.Flat
 				If _arrow Then
 					Dim _arrow as New System.Drawing.Drawing2D.AdjustableArrowCap(16,16,False) ' (8,16, False)
					Dim _customArrow As System.Drawing.Drawing2D.CustomLineCap = _arrow
					m_Pen.CustomEndCap = _customArrow
				Else
					m_Pen.EndCap = System.Drawing.Drawing2D.LineCap.Flat
				End If
			End If

	    Case "freehand", "highlight"
	    	m_Drawing = True
	    	UndoBuffer.Push(m_UndoBitmap)
	    	
'	    	If tool = "highlight" And highlightTexture Is Nothing Then ' copy only black pixels to patternbrush making all other colors transparent
'	    		highlightTexture = m_CurrentBitmap ' new Bitmap("file.jpg")
'	    		' highlightTexture.MakeTransparent(Color.FromArgb(255, 255, 255, 255))
'				For w As Integer = 0 To highlightTexture.Width - 1
'					For h As Integer = 0 To highlightTexture.Height - 1
'						Dim pixelColor As Color = highlightTexture.GetPixel(w, h)
'						If pixelColor.GetSaturation < 0.2 Then
'						' If pixelColor = Color.FromArgb(255, 255, 255, 255) Then ' If pixelColor <> Color.Black Then ' pixelColor <> Color.FromArgb(255, 0, 0, 0)
'							highlightTexture.SetPixel(w, h, Color.Transparent) ' Color.Transparent ' Color.FromArgb(0, 0, 0, 0)
'						End If
'					Next
'				Next
'		    	highlightTextureBrush = New TextureBrush(highlightTexture)
'		    	highlightTextureBrush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile ' .Clamp
'	    		highlightPen = New Pen(highlightTextureBrush, 20)
'	    	End If

	    End Select
	    
    End Sub

'	Dim highlightTexture As Bitmap = Nothing
'	Dim highlightPen As Pen
'	Dim highlightTextureBrush As TextureBrush
	Dim Public lineWidth As Integer = 4

	Sub updateUndoBuffer()
		Try
		' need to set initial undo bitmap pixelformat without writing to undo buffer
		Dim _image As BitMap
		_image = picCanvas.Image
		Dim _Bitmap As New Bitmap(picCanvas.image.Width, picCanvas.Image.Height,system.Drawing.Imaging.PixelFormat.Format32bppArgb)
		Dim _tmp As Graphics = Graphics.FromImage(_Bitmap)
		_tmp.DrawImage(_image,0,0)
		' now write to undo buffer
		' clipboard.SetImage(_Bitmap)
       	UndoBuffer.Push(_Bitmap)
		Catch
		End Try
	End Sub

	Private m_Drawing As Boolean

	Private m_X2 As Integer
    Private m_Y2 As Integer
    Private refreshRect As Rectangle
    

    ' Continue selecting the area.
    Private Sub picCanvasMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

 		' me.Cursor = Cursors.Cross
 
  		If m_Drawing Or m_SelectingArea Then
  			
  			debug.Write(".")
  			
  			Select Case tool
	  		Case "rectangle", "pixelate", "select"
		        ' Start with the current image.
		        ' m_Gr.DrawImage(m_CurrentBitmap, 0, 0)


				
' only refresh working area	(rectangle)
'If m_X2 = m_X1 Then m_X2 = e.X + 10
'If m_Y2 = m_Y2 Then m_Y2 = e.Y + 10
'If Not (m_X2 - m_X1) = 0 Or (m_Y2 - m_Y1) = 0 Then
'm_Gr.DrawImage(m_currentBitmap.Clone(New Rectangle(Min(m_X1, m_X2),Min(m_Y1, m_Y2),Abs(m_X2 - m_X1),Abs(m_Y2 - m_Y1)),m_CurrentBitmap.PixelFormat),Min(m_X1, m_X2),Min(m_Y1, m_Y2))
'End If
'if e.X > m_X2 Then m_X2 = e.X + 10
'If e.Y > m_Y2 Then m_Y2 = e.Y + 10



' Make sure the start point is on the picture.
Dim r_X1 As Integer
If m_X1 >= e.X Then
	r_X1 = m_X1 + 100
Else
	r_X1 = m_X1 - 100
End If
If r_X1 < 0 Then r_X1 = 0
If r_X1 > m_OriginalBitmap.Width - 1 Then r_X1 = m_OriginalBitmap.Width - 1

Dim r_Y1 As Integer
If m_Y1 >= e.Y Then
	r_Y1 = m_Y1 + 100
Else
	r_Y1 = m_Y1 - 100
End If
If r_Y1 < 0 Then r_Y1 = 0
If r_Y1 > m_OriginalBitmap.Height - 1 Then r_Y1 = m_OriginalBitmap.Height - 1
'r_X1 = m_X1
'r_Y1 = m_Y1

' Make sure the end point is on the picture.
Dim r_X2 As Integer
If e.X >= m_X1 Then
	r_X2 = e.X + 100
Else
	r_X2 = e.X - 100
End If
If r_X2 < 0 Then r_X2 = 0
If r_X2 > m_OriginalBitmap.Width - 1 Then r_X2 = m_OriginalBitmap.Width - 1

Dim r_Y2 As Integer
If e.Y >= m_Y1 Then
	r_Y2 = e.Y + 100
Else
	r_Y2 = e.Y - 100
End If
If r_Y2 < 0 Then r_Y2 = 0
If r_Y2 > m_OriginalBitmap.Height - 1 Then r_Y2 = m_OriginalBitmap.Height - 1

Try ' dirty fix for 0 width/height checks

If Not (r_X2 - r_X1) = 0 Or (r_Y2 - r_Y1) = 0 Then
	refreshRect = New Rectangle(Min(r_X1, r_X2),Min(r_Y1, r_Y2),Abs(r_X2 - r_X1),Abs(r_Y2 - r_Y1))
	m_Gr.DrawImage(m_currentBitmap.Clone(refreshRect,m_CurrentBitmap.PixelFormat),Min(r_X1, r_X2),Min(r_Y1, r_Y2))
End If

Catch x As Exception
End Try

		        	Dim m_X2 As Integer = e.X
					Dim m_Y2 As Integer = e.Y
		
				If tool = "select" Then
' REMOVE					m_Gr.DrawRectangle(pens.White, _
		            _selectionrect = New Rectangle( _
		            Min(m_X1, m_X2), _
		            Min(m_Y1, m_Y2), _
		            Abs(m_X2 - m_X1), _
		            Abs(m_Y2 - m_Y1))
				End If

		        ' Draw the new selection box
		        If Not tool = "select" Then _
		        m_Gr.DrawRectangle(m_Pen, _
		            Min(m_X1, m_X2), _
		            Min(m_Y1, m_Y2), _
		            Abs(m_X2 - m_X1), _
		            Abs(m_Y2 - m_Y1))
				
		        ' Display the result.
		   		picCanvas.Image = m_TempBitmap
	        Case "line"
		        ' Start with the current image.
		        m_Gr.DrawImage(m_CurrentBitmap, 0, 0)
		
		        ' Draw the new selection box.
		        Try
		        	Dim m_X2 As Integer = e.X
					Dim m_Y2 As Integer = e.Y
		    		' constrain direction
		        	If Control.ModifierKeys = Keys.Shift Then
		        		If Math.Abs(m_X1 - e.X) <= Math.Abs(m_Y1 - e.Y) Then
		        			m_X2 = m_X1
		        		Else
		        			m_Y2 = m_Y1
		        		End If
		        	End If
			        m_Gr.DrawLine(m_Pen, m_X1, m_Y1, m_X2, m_Y2) ' m_Gr.DrawLine(m_Pen, m_X1, m_Y1, e.X, e.Y)
				Catch
				End Try
		        ' Display the result.
		   		picCanvas.Image = m_TempBitmap
	        Case "freehand"
				' using custom pen
				Dim brush As New SolidBrush(annotateColor)
				Dim customPen As New Pen(brush, lineWidth)
				' Dim customPen As Pen
				' customPen = New Pen(Color.Red, 5)
				Dim linecap As System.Drawing.Drawing2D.LineCap = System.Drawing.Drawing2D.LineCap.Round
				customPen.StartCap = linecap
				customPen.EndCap = linecap
				
				m_Gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
				m_Gr.DrawLine(customPen, m_X1, m_Y1, e.X, e.Y)
				
				' Draw the new line. Pens are one pixel wide
				' m_Graphics.DrawLine(Pens.Black, m_LastX, m_LastY, e.X, e.Y)
				
		        ' Display the result.
		        picCanvas.Image = m_TempBitmap
				
				' Save the latest point.
				m_X1 = e.X
				m_Y1 = e.Y
	        Case "highlight"
	        	
	        	' using lockbits
	          	Dim pxf As System.Drawing.Imaging.PixelFormat = m_TempBitmap.PixelFormat ' System.Drawing.Imaging.Pixelformat.Format24bppRgb
	          	
	          	' define and lock the area to be highlighted
	          	Dim _highlighterWidth As Integer = highlighterWidth / 2
	          	Dim _highlighterHeight As Integer = highlighterHeight / 2
	          	Dim highlighterWidth_ As Integer = highlighterWidth
	          	Dim highlighterHeight_ As Integer = highlighterHeight	          	
	          	          	
	          	
				Try	          		
		          	Dim rect As Rectangle = New Rectangle(e.X-_highlighterWidth,e.Y-_highlighterHeight, highlighterWidth_, highlighterHeight_) ' m_X1, m_Y1
					Dim m_BitmapData As System.Drawing.Imaging.BitmapData = m_TempBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, pxf)
					
					' get first line of data
'					Dim ptr As IntPtr = m_BitmapData.Scan0
'				    ' Dim numBytes as Integer = rect.Width * rect.Height * 3 
'				    Dim numBytes as Integer = m_BitmapData.Stride * rect.Height
'				    Dim rgbValues(numBytes) As Byte 
'				
'				    ' Copy the RGB values into the array.
'				    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, numBytes)			    
'				    
'				     ' Manipulate the bitmap
'				     For counter As Integer = 0 To rgbValues.Length Step 3
'				     	rgbValues(counter) = 0 ' blue to 0 for a yellow highlighter
'				     Next counter					    
'				    
'				    ' Copy the RGB values back to the bitmap
'				    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, numBytes)
				    
' http://bobpowell.net/lockingbits.aspx				    
Dim x As Integer
Dim y As Integer
Dim PixelSize As Integer = 4
Select Case m_TempBitmap.PixelFormat
	Case System.Drawing.Imaging.Pixelformat.Format24bppRgb
		PixelSize = 3 ' BGR
	Case System.Drawing.Imaging.Pixelformat.Format32bppArgb
		PixelSize = 4 ' BGRA
	Case System.Drawing.Imaging.Pixelformat.Format8bppIndexed
		PixelSize = 1  
End Select
For y = 0 To m_BitmapData.Height - 1
	For x = 0 To m_BitmapData.Width - 1 
		    
	    Select Case highlightColorName.ToLower
	    Case "yellow"		
			System.Runtime.InteropServices.Marshal.WriteByte(m_BitmapData.Scan0, (m_BitmapData.Stride * y) + (PixelSize * x) , 0) ' blue to 0 for a yellow highlighter
	    Case "blue", "cyan"
	     	System.Runtime.InteropServices.Marshal.WriteByte(m_BitmapData.Scan0, (m_BitmapData.Stride * y) + (PixelSize * x) + 2 , 0) ' red to 0 for a blue highlighter
	    Case "magenta"
	     	System.Runtime.InteropServices.Marshal.WriteByte(m_BitmapData.Scan0, (m_BitmapData.Stride * y) + (PixelSize * x) + 1 , 0) ' green to 0 for a magenta highlighter
	    Case "green"
	    	System.Runtime.InteropServices.Marshal.WriteByte(m_BitmapData.Scan0, (m_BitmapData.Stride * y) + (PixelSize * x) , 0) 
	    	System.Runtime.InteropServices.Marshal.WriteByte(m_BitmapData.Scan0, (m_BitmapData.Stride * y) + (PixelSize * x) + 2 , 0)
	    End Select	
		
	Next
Next					    
				    
				    ' Unlock the bits.
				    m_TempBitmap.UnlockBits(m_BitmapData)			    
				Catch ex As Exception
					Debug.WriteLine(ex.Message)
				End Try
				
'				' using custom pen
'				Dim customPen As New Pen(highlightColor, 20) ' opaque Color.FromArgb(255, 0, 0, 255)
'				m_Gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected
'							
'				m_Gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
'				m_Gr.DrawLine(customPen, m_X1, m_Y1, e.X, e.Y)
'				
'				' draw foreground on top of background
'				m_Gr.DrawLine(highlightPen, m_X1, m_Y1, e.X, e.Y)
				 
				
		        ' Display the result.
		        picCanvas.Image = m_TempBitmap
		        
		        _highlighterWidth = highlighterWidth * 2
		        
				' Save the latest point.
				m_X1 = e.X
				m_Y1 = e.Y
	        Case "ellipse"
		        ' Start with the current image.
		        m_Gr.DrawImage(m_CurrentBitmap, 0, 0)
		
		        ' Draw the new selection box.
				Dim new_rect As New Rectangle( _
		            Min(m_X1, e.X), _
		            Min(m_Y1, e.Y), _
		            Abs(e.X - m_X1), _
		            Abs(e.Y - m_Y1))
		        m_Gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
				m_Gr.DrawEllipse(m_Pen, new_rect)
		        ' Display the result.
		   		picCanvas.Image = m_TempBitmap
  
  	        Case "rounded"
		        ' Start with the current image.
		        m_Gr.DrawImage(m_CurrentBitmap, 0, 0)

		        m_Gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
				' draw the rounded rectangle
				DrawRoundRect(	m_Gr, _
								m_Pen, _
								Min(m_X1, e.X), _
						        Min(m_Y1, e.Y), _
						        Abs(e.X - m_X1), _
						        Abs(e.Y - m_Y1), radius)
		        ' Display the result.
		   		picCanvas.Image = m_TempBitmap
  
	        End Select
		Else
			Exit Sub
		End if
    End Sub

	Dim private allowMove As Boolean = False
	Dim private allowResize As Boolean = False
	Dim Private activeRect As Rectangle = Nothing

	Dim highlightColor As Color = Color.FromArgb(100, 255, 255, 0)

	public radius As Integer = 6

    ' Finish selecting the area.
    Private Sub picCanvasMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
		If e.Button = MouseButtons.Right Then Exit Sub
		debug.WriteLine(VbCrLf & "<<< Mouse Up")
	
		' me.Cursor = Cursors.Default
		allowMove = False
		allowResize = False
	
		If m_SelectingArea Or m_drawing Then

'			If (Not m_X1 = e.X Or Not m_Y1 = e.Y) And Not _selectArea Then
'				' Dim undo As Bitmap = m_CurrentBitmap.Clone()
'				UndoBuffer.Push(m_UndoBitmap)
'			End If

			Select Case tool
				
			Case "pixelate"
				m_SelectingArea = False
		        ' Make sure this point is on the picture.
		        Dim x As Integer = e.X
		        If x < 0 Then x = 0
		        If x > m_OriginalBitmap.Width - 1 Then x = m_OriginalBitmap.Width - 1
		
		        Dim y As Integer = e.Y
		        If y < 0 Then y = 0
		        If y > m_OriginalBitmap.Height - 1 Then y = m_OriginalBitmap.Height - 1
	
	       		' Pixelate the selected area.
		        PixelateArea( _
		            Min(m_X1, x), _
		            Min(m_Y1, y), _
		            Abs(x - m_X1), _
		            Abs(y - m_Y1))
		        
		        ' We're done drawing for now.
		        m_Pen.Dispose()
		        m_Gr.Dispose()
		        m_TempBitmap.Dispose()
		
		        m_Pen = Nothing
		        m_Gr = Nothing
		        m_TempBitmap = Nothing
		            
			Case "rectangle"
				m_SelectingArea = False
				If Not (Not m_X1 = e.X Or Not m_Y1 = e.Y) Then
					UndoBuffer.Pop() ' UndoBuffer.Push(m_UndoBitmap)
					Exit Sub
				End If
	        	
	allowMove = True
	allowResize = True
	        	
		        ' Make sure this point is on the picture.
		        Dim x As Integer = e.X
		        If x < 0 Then x = 0
		        If x > m_OriginalBitmap.Width - 1 Then x = m_OriginalBitmap.Width - 1
		
		        Dim y As Integer = e.Y
		        If y < 0 Then y = 0
		        If y > m_OriginalBitmap.Height - 1 Then y = m_OriginalBitmap.Height - 1
		        
		        ' Dim brush As New SolidBrush(Color.FromArgb(90, 0, 50, 255))
		 		Dim brush As New SolidBrush(annotateColor)
				Dim customPen As New Pen(brush, lineWidth)
				
				m_Gr = Graphics.FromImage(m_UndoBitmap) ' clean
				
				' draw the rectangle
		        Dim new_rect As New Rectangle( _
		            Min(m_X1, x), _
		            Min(m_Y1, y), _
		            Abs(x - m_X1), _
		            Abs(y - m_Y1))
		        m_Gr.DrawRectangle(customPen, new_rect)
				
	activeRect = new_Rect
				
'    ' highlight the selected area
'	 Dim highlightColor As Color = Color.FromArgb(196, 255, 255, 50)
'    Dim hl_brush As New SolidBrush(highlightColor)
'    m_Gr.FillRectangle(hl_brush, new_rect)
'    hl_brush.Dispose()
				
				' Display the result.
		        ' Set the current bitmap to the result.
		        m_CurrentBitmap = New Bitmap(m_UndoBitmap)
	
		        ' Display the result.
		        picCanvas.Image = m_CurrentBitmap
	
		        ' We're done drawing for now.
		        m_Pen.Dispose()
		        m_Gr.Dispose()
		        m_TempBitmap.Dispose()
		
		        m_Pen = Nothing
		        m_Gr = Nothing
		        m_TempBitmap = Nothing

			Case "select"

	        	m_SelectingArea = False

				If m_X1 = e.X And m_Y1 = e.Y Then
		        	picCanvas.Image = m_CurrentBitmap
		        	m_TempBitmap = Nothing
		        	_selectionRect = Nothing
		        	_selectionActive = False
		       	 	Exit Sub
				End If
	        	
		        ' Make sure this point is on the picture.
		        Dim x As Integer = e.X
		        If x < 0 Then x = 0
		        If x > m_OriginalBitmap.Width - 1 Then x = m_OriginalBitmap.Width - 1
		
		        Dim y As Integer = e.Y
		        If y < 0 Then y = 0
		        If y > m_OriginalBitmap.Height - 1 Then y = m_OriginalBitmap.Height - 1
		        
			
				' get the rectangle
				_selectionRect = New Rectangle( _
		            Min(m_X1, x), _
		            Min(m_Y1, y), _
		            Abs(x - m_X1), _
		            Abs(y - m_Y1))
		        ' m_Gr.DrawRectangle(m_Pen, _selectionRect)
				_selectionActive = True
				
			Case "freehand", "highlight"

'				If tool = "highlight" Then
'					m_Gr.DrawImage(highlightTexture, new Point(0, 0)) ' overlay text layer
'				End If
				
				m_Drawing = False
	
				' Display the result.
		        ' Set the current bitmap to the result.
		        m_CurrentBitmap = New Bitmap(m_TempBitmap)
		        
		        ' Display the result.
		        picCanvas.Image = m_CurrentBitmap
	
		        ' We're done drawing for now.
		        m_Gr.Dispose()
		        m_TempBitmap.Dispose()
		
		        m_Gr = Nothing
		        m_TempBitmap = Nothing
						
			Case "ellipse"
				If Not (Not m_X1 = e.X Or Not m_Y1 = e.Y) Then UndoBuffer.Pop() ' UndoBuffer.Push(m_UndoBitmap)
	        	m_SelectingArea = False
		        
		 		Dim brush As New SolidBrush(annotateColor)
				Dim customPen As New Pen(brush, lineWidth)
				
				m_Gr = Graphics.FromImage(m_UndoBitmap) ' clean
				
				' draw the ellipse
		        Dim new_rect As New Rectangle( _
		            Min(m_X1, e.x), _
		            Min(m_Y1, e.y), _
		            Abs(e.x - m_X1), _
		            Abs(e.y - m_Y1))
		        m_Gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
		        m_Gr.DrawEllipse(customPen, new_rect)
				
				' Display the result.
		        ' Set the current bitmap to the result.
		        m_CurrentBitmap = New Bitmap(m_UndoBitmap)
				
		        ' Display the result.
		        picCanvas.Image = m_CurrentBitmap
			
	
		        ' We're done drawing for now.
		        m_Pen.Dispose()
		        m_Gr.Dispose()
		        m_TempBitmap.Dispose()
		
		        m_Pen = Nothing
		        m_Gr = Nothing
		        m_TempBitmap = Nothing
			
			Case "line"
				If Not (Not m_X1 = e.X Or Not m_Y1 = e.Y) Then UndoBuffer.Pop() ' UndoBuffer.Push(m_UndoBitmap)
	        	m_SelectingArea = False
		        
		        Try
		 		Dim brush As New SolidBrush(annotateColor)
				Dim customPen As New Pen(brush, lineWidth)
				if _arrow Then
					customPen.CustomEndCap = arrowHead ' .EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor
					If lineWidth = 1 Then
						arrowHead = New System.Drawing.Drawing2D.AdjustableArrowCap(8,8,False)
					Else
						arrowHead = New System.Drawing.Drawing2D.AdjustableArrowCap(4,4,False)
					End If
				Else
					customPen.EndCap = System.Drawing.Drawing2D.LineCap.Flat
				End If
				customPen.StartCap = System.Drawing.Drawing2D.LineCap.Flat
				
				m_Gr = Graphics.FromImage(m_UndoBitmap) ' clean
				
				' draw the line
		        m_Gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
	        	
	        	Dim m_X2 As Integer = e.X
				Dim m_Y2 As Integer = e.Y
	    		' constrain direction
	        	If Control.ModifierKeys = Keys.Shift Then
	        		If Math.Abs(m_X1 - e.X) <= Math.Abs(m_Y1 - e.Y) Then
	        			m_X2 = m_X1
	        		Else
	        			m_Y2 = m_Y1
	        		End If
	        	End If
			    m_Gr.DrawLine(customPen, m_X1, m_Y1, m_X2, m_Y2) ' m_Gr.DrawLine(customPen, m_X1, m_Y1, e.X, e.Y)
		        		
				' Display the result.
		        ' Set the current bitmap to the result.
		        m_CurrentBitmap = New Bitmap(m_UndoBitmap)
		
		
		        ' Display the result.
		        picCanvas.Image = m_CurrentBitmap
	
		        ' We're done drawing for now.
		        m_Pen.Dispose()
		        m_Gr.Dispose()
		        m_TempBitmap.Dispose()
		
		        m_Pen = Nothing
		        m_Gr = Nothing
		        m_TempBitmap = Nothing
				Catch
				End Try

			Case "rounded"
				If Not (Not m_X1 = e.X Or Not m_Y1 = e.Y) Then UndoBuffer.Pop() ' UndoBuffer.Push(m_UndoBitmap)
	        	m_SelectingArea = False
		        ' Make sure this point is on the picture.
		        Dim x As Integer = e.X
		        If x < 0 Then x = 0
		        If x > m_OriginalBitmap.Width - 1 Then x = m_OriginalBitmap.Width - 1
		
		        Dim y As Integer = e.Y
		        If y < 0 Then y = 0
		        If y > m_OriginalBitmap.Height - 1 Then y = m_OriginalBitmap.Height - 1
		        
		        ' Dim brush As New SolidBrush(Color.FromArgb(90, 0, 50, 255))
		 		Dim brush As New SolidBrush(annotateColor)
				Dim customPen As New Pen(brush, lineWidth)
				
				m_Gr = Graphics.FromImage(m_UndoBitmap) ' clean
				
				' draw the rounded rectangle
				DrawRoundRect(	m_Gr, _
								customPen, _
								Min(m_X1, x), _
						        Min(m_Y1, y), _
						        Abs(x - m_X1), _
						        Abs(y - m_Y1), radius)
				
				' Display the result.
		        ' Set the current bitmap to the result.
		        m_CurrentBitmap = New Bitmap(m_UndoBitmap)
	
		        ' Display the result.
		        picCanvas.Image = m_CurrentBitmap
	
		        ' We're done drawing for now.
		        m_Pen.Dispose()
		        m_Gr.Dispose()
		        m_TempBitmap.Dispose()
		
		        m_Pen = Nothing
		        m_Gr = Nothing
		        m_TempBitmap = Nothing

			
			End Select
			
			resetToolStripMenuItem.Enabled = True
		
		End If
        
    End Sub
	
	Dim arrowHead as New System.Drawing.Drawing2D.AdjustableArrowCap(4,4,False) ' (4,8,False)

	' catch keyboard input on entire form
	Protected Overrides Function ProcessDialogKey(ByVal keyData As System.Windows.Forms.Keys) As Boolean
		
		Debug.WriteLine(KeyData)
		Select Case keyData
		Case 131139 ' Ctrl+C, Copy
			Copy()
		Case 131158 ' Ctrl+V, Paste
			Paste()
		Case 131162 ' Ctrl+Z, Undo
			Undo()
		Case 131155 ' Ctrl+S, Save (As)
			If _file.Length > 0 Then
				Save(_file)
			Else
				Save()
			End If
		Case 131153 ' Ctrl+Q, Quit
				CloseApp()
    	End Select
    End Function
	
    ' Pixelate the area.
    Private Sub PixelateArea(ByVal x As Integer, ByVal y As Integer, ByVal wid As Integer, ByVal hgt As Integer)
        
        If wid = 0 Or hgt = 0 then Exit Sub
        
        Const cell_wid As Integer = 8 ' 4 20
        Const cell_hgt As Integer = 4 ' 4 10

        ' Start with the current image.
        m_Gr.DrawImage(m_CurrentBitmap, 0, 0)

        ' Make x and y multiples of cell_wid/cell_hgt
        ' from the origin.
        Dim new_x As Integer = cell_wid * Int(x \ cell_wid)
        Dim new_y As Integer = cell_hgt * Int(y \ cell_hgt)

        ' Pixelate the selected area.
        For x1 As Integer = new_x To x + wid Step cell_wid
            For y1 As Integer = new_y To y + hgt Step cell_hgt
                AverageRectangle(x1, y1, cell_wid, cell_hgt)
            Next y1
        Next x1

	    	' grab the selected area of the image
			Dim bm As Bitmap = m_TempBitmap.Clone(New Rectangle(x,y,wid,hgt),m_TempBitmap.PixelFormat)
			' clipboard.SetImage(bm)
	
			' only set selected area to the result
			Dim g As Graphics = Graphics.FromImage(m_CurrentBitmap)
			g.DrawImage(bm, New Rectangle(x, y, wid, hgt))
			g.Dispose()
			bm.Dispose()
		
        ' Set the current bitmap to the result.
        ' m_CurrentBitmap = New Bitmap(m_TempBitmap)

        ' Display the result.
        picCanvas.Image = m_CurrentBitmap

    End Sub

    ' Fill this rectangle with the average of its pixel values.
    Private Sub AverageRectangle(ByVal x As Integer, ByVal y As Integer, ByVal wid As Integer, ByVal hgt As Integer)
        ' Make sure we don't exceed the image's bounds.
        If x < 0 Then x = 0
        If x + wid >= m_OriginalBitmap.Width Then
            wid = m_OriginalBitmap.Width - x - 1
        End If
        If wid <= 0 Then Exit Sub

        If y < 0 Then y = 0
        If y + hgt >= m_OriginalBitmap.Height Then
            hgt = m_OriginalBitmap.Height - y - 1
        End If
        If hgt <= 0 Then Exit Sub

        ' Get the total red, green, and blue values.
        Dim clr As Color
        Dim r As Integer
        Dim g As Integer
        Dim b As Integer
        For i As Integer = 0 To hgt - 1
            For j As Integer = 0 To wid - 1
                clr = m_CurrentBitmap.GetPixel(x + j, y + i)
                r += clr.R
                g += clr.G
                b += clr.B
            Next j
        Next i

        ' Calculate the averages.
        r \= wid * hgt
        g \= wid * hgt
        b \= wid * hgt

        ' Set the pixel values.
        Dim ave_brush As New SolidBrush(Color.FromArgb(255, r, g, b))
        m_Gr.FillRectangle(ave_brush, x, y, wid, hgt)
        ave_brush.Dispose()
    End Sub

	Dim Private dlgSavePicture As New SaveFileDialog
	Dim Private dlgOpenPicture As New OpenFileDialog
	
	Dim Public workfolder As String = application.StartupPath
	Dim Public SetName As string = ""
	
	Sub SaveAsToolStripMenuItemClick(sender As Object, e As EventArgs)
        dlgSavePicture.DefaultExt = "png"
        dlgSavePicture.AddExtension = True
        dlgSavePicture.InitialDirectory = workfolder
        dlgSavePicture.Filter = "Image Files (*.png;*.jpg;*.gif)|*.png;*.jpg;*.gif|All Files (*.*)|*.*"
        dlgSavePicture.Title = "Save Image File"
        
        If len(Trim(setName)) > 0 Then
        	dlgSavePicture.FileName = setName
        Else If len(Trim(_file)) > 0 Then
        	dlgSavePicture.InitialDirectory = System.IO.Path.GetDirectoryName(_file)
        	dlgSavePicture.FileName = System.IO.Path.GetFileName(_file)
        End If
        
        If dlgSavePicture.ShowDialog() = DialogResult.OK Then
            Try
            	_file = dlgSavePicture.FileName
                m_CurrentBitmap.Save(dlgSavePicture.FileName)
                RaiseEvent ImageSaved(_file)
                dlgOpenPicture.InitialDirectory = dlgSavePicture.FileName
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
	End Sub
	
	Sub openToolStripMenuItemClick(sender As Object, e As EventArgs)
        dlgOpenPicture.DefaultExt = "png"
        dlgOpenPicture.InitialDirectory = workfolder
        dlgOpenPicture.Filter = "Image Files (*.png;*.jpg;*.gif)|*.png;*.jpg;*.gif|All Files (*.*)|*.*"
        dlgOpenPicture.Title = "Open Image File"
        
        If dlgOpenPicture.ShowDialog() = DialogResult.OK Then
            Try
                Dim fs As System.IO.FileStream
				fs = New System.IO.FileStream(dlgOpenPicture.FileName, IO.FileMode.Open, IO.FileAccess.Read)
				Dim bm As Bitmap = System.Drawing.Bitmap.FromStream(fs) ' Dim bm As New Bitmap(dlgOpenPicture.FileName)
				fs.Dispose()
                
                m_OriginalBitmap = New Bitmap(bm)
                m_CurrentBitmap = New Bitmap(bm)
                bm.Dispose()
                picCanvas.Image = m_CurrentBitmap
                dlgSavePicture.InitialDirectory = dlgOpenPicture.FileName
				_file = dlgOpenPicture.FileName
                ' Make the form big enough.
                Me.ClientSize = New Size(m_OriginalBitmap.Width, m_OriginalBitmap.Height)
                UndoBuffer.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
	End Sub
	
	Public Function Open(fileName As String) As Boolean
		Try
			
			Dim fs As System.IO.FileStream
			fs = New System.IO.FileStream(fileName, IO.FileMode.Open, IO.FileAccess.Read)
			Dim bm As Bitmap = System.Drawing.Bitmap.FromStream(fs) ' Dim bm As New Bitmap(fileName)
			fs.Dispose()
            
            m_OriginalBitmap = New Bitmap(bm)
            m_CurrentBitmap = New Bitmap(bm)
            bm.Dispose()
            picCanvas.Image = m_CurrentBitmap
            dlgSavePicture.InitialDirectory = dlgOpenPicture.FileName
			_file = dlgOpenPicture.FileName
            ' Make the form big enough.
            Me.ClientSize = New Size(m_OriginalBitmap.Width, m_OriginalBitmap.Height)
            UndoBuffer.Clear()
			Return True
		Catch
        	Return False
        End Try
	End Function
	
	Dim Private tool As String = "rectangle"
	Dim private _arrow As Boolean = false
	
	Sub BackToolStripMenuItemClick(sender As Object, e As EventArgs)
        If m_OriginalBitmap Is Nothing Then Exit Sub
        m_CurrentBitmap = New Bitmap(m_OriginalBitmap)
        picCanvas.Image = m_CurrentBitmap
        undoBuffer.Clear()
        updateUndoBuffer()
	End Sub
	
	Sub RectangleToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		' CheckedChanged raised before ItemClick
		prevTool = tool
		If RectangleToolStripMenuItem.Checked Then tool = "rectangle"
		Me.Refresh()
	End Sub
	
	Sub FreehandToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		 prevTool = tool
		 If FreehandToolStripMenuItem.Checked Then tool = "freehand"
		 Me.Refresh()
	End Sub

	Sub HighlightToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		 prevTool = tool
		 If HighlightToolStripMenuItem.Checked Then tool = "highlight"
		 Me.Refresh()
	End Sub
	
	Sub PixelateToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		prevTool = tool
		If PixelateToolStripMenuItem.Checked Then tool = "pixelate"
		Me.Refresh()
	End Sub

	Sub LineToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		prevTool = tool
		If LineToolStripMenuItem.Checked Then tool = "line"
		_arrow = Not LineToolStripMenuItem.Checked
		Me.Refresh()
	End Sub
	
	Sub toolToolStripMenuItemClick(sender As Object, e As EventArgs)
		
		' reset highlightTexture
'		highlightTexture = Nothing
		
		' Dim _sender As ToolStripMenuItem = sender
		
		FreehandToolStripMenuItem.Checked = False
		lineToolStripMenuItem.Checked = False
		PixelateToolStripMenuItem.Checked = False
		RectangleToolStripMenuItem.Checked = False
		ellipseToolStripMenuItem.Checked = False
		arrowToolStripMenuItem.Checked = False
		SelectAreaToolStripMenuItem.Checked = False
		TextToolStripMenuItem.Checked = False
		RoundedRectangleToolStripMenuItem.Checked = False
		highlightToolStripMenuItem.Checked = False
		
		Sender.Checked = True
		Select Case tool
			
			Case "rectangle"
				me.Cursor = Cursors.Cross
			Case "ellipse"
				Me.Cursor = Cursors.Cross
			Case "line"
				Me.Cursor = Cursors.Cross
			Case "select"
				Me.Cursor = Cursors.Cross
			Case "pixelate"
				Me.Cursor = Cursors.Cross
			Case "rectangle"
				Me.Cursor = Cursors.Cross
			Case "freehand"
				Me.Cursor = Cursors.Cross
			Case "highlight"
				me.Cursor = Cursors.IBeam ' should be a line (height of the marker)
			Case "text"
				me.Cursor = Cursors.IBeam
			
		End Select
	End Sub

	
	Sub EllipseToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		prevTool = tool
		If ellipseToolStripMenuItem.Checked Then tool = "ellipse"
		Me.Refresh()
	End Sub

	Sub RoundedRectangleToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		prevTool = tool
		If RoundedRectangleToolStripMenuItem.Checked Then tool = "rounded"
		Me.Refresh()
	End Sub
	
	Sub arrowToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		prevTool = tool
		If arrowToolStripMenuItem.Checked Then tool = "line"
		_arrow = arrowToolStripMenuItem.Checked
		Me.Refresh()
	End Sub

		Private prevTool As String
	
	Sub SelectAreaToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		prevTool = tool
		If SelectAreaToolStripMenuItem.Checked Then tool = "select"

		If Not _selectArea Then
			_selectionActive = False
			_selectionRect = Nothing
		End If
		' insertToolStripMenuItem.Enabled = Not SelectAreaToolStripMenuItem.Checked
		' restore the original screen
		
		If Not SelectAreaToolStripMenuItem.Checked Then tool = "Nothing"
	End Sub
	
	' clear selected area between annotations
	Public Sub clearSelection()
		_selectionActive = False
		_selectionRect = Nothing
	End Sub

	Private _selectArea As Boolean = False
	Dim _selectionActive As Boolean = False
	Dim _selectionRect As Rectangle = Nothing
	
	Sub AnnotateBoxLoad(sender As Object, e As EventArgs)
		undoBuffer = New Stack()
		' AllocateBitmap()
	End Sub
	

    ' Make a new Bitmap to draw on.
    Private Sub AllocateBitmap()

		' Set Bitmap from screenshot
		Dim bounds As Rectangle
		bounds = picCanvas.Bounds ' Screen.PrimaryScreen.Bounds
		Dim startPoint As Point = picCanvas.PointToScreen(New Point(bounds.X, bounds.Y))
		Dim screenshot As System.Drawing.Bitmap
		screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
		Dim g As Graphics = Graphics.FromImage(screenshot)
		g.CopyFromScreen(startPoint.X, startPoint.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
        m_OriginalBitmap = New Bitmap(screenshot)
        m_CurrentBitmap = New Bitmap(screenshot)
        screenshot.Dispose()
        picCanvas.Image = m_CurrentBitmap
     
    End Sub




	Public Function SetCanvas(canvas As Bitmap) As Boolean
'		highlightTexture = Nothing
		Try
			m_OriginalBitmap = New Bitmap(canvas)
	        m_CurrentBitmap = New Bitmap(canvas)
	        picCanvas.Image = m_CurrentBitmap
	
	        ' Make the form big enough.
	        Me.ClientSize = New Size(m_OriginalBitmap.Width, m_OriginalBitmap.Height)
	        
	        UndoBuffer.Clear()
			Return True
		Catch
        	Return False
        End Try
	End Function

	Public Function GetCanvas() As Bitmap
		If Not m_CurrentBitmap Is Nothing Then
			Return m_CurrentBitmap
		Else
			Return Nothing
		End If
	End Function
	
	Public Sub Clear()
		If Not m_CurrentBitmap Is Nothing Then
			m_OriginalBitmap = Nothing
			m_CurrentBitmap = Nothing
			picCanvas.Image = Nothing
			_file = Nothing
			UndoBuffer.Clear()
			updateUndoBuffer()
		End If
	End Sub
	
	Public Function Save() As Boolean
		SaveAsToolStripMenuItemClick(Nothing,Nothing)
	End Function

	Public Function Save(fileName As String) As boolean
		Try
			m_CurrentBitmap.Save(fileName)
			_file = fileName
			m_OriginalBitmap = picCanvas.Image
			UndoBuffer.Clear()
			updateUndoBuffer()
			RaiseEvent ImageSaved(_file)
			Return True
		Catch
			Return False
		End Try
	End Function

	
	Sub SaveToolStripMenuItemClick(sender As Object, e As EventArgs)
		m_CurrentBitmap.Save(_file)
		m_OriginalBitmap = picCanvas.Image
		UndoBuffer.Clear()
		updateUndoBuffer()
		RaiseEvent ImageSaved(_file)
	End Sub

	
	Sub UndoToolStripMenuItemClick(sender As Object, e As EventArgs)
        undo()
	End Sub
	
	Public Sub undo()
        If Not UndoBuffer.Count = 0 Then
	        m_CurrentBitmap = UndoBuffer.Pop()
    	    picCanvas.Image = m_CurrentBitmap
			Me.Refresh()
		End If
	End Sub
	
	Sub ContextMenuStripCanvasOpening(sender As Object, e As System.ComponentModel.CancelEventArgs)
		If UndoBuffer.Count > 0 Then
			UndoToolStripMenuItem.Enabled = True
		Else
			UndoToolStripMenuItem.Enabled = False
		End If

		OpenToolStripMenuItem.Visible = _showOpen
		SaveToolStripMenuItem.Visible = _showSave
		pasteToolStripMenuItem.Visible = _allowPaste
		colorToolStripMenuItem.Visible = _showColor
		roundedRectangleToolStripMenuItem.Visible = _showRoundedRectangle
		
		If Not m_CurrentBitmap Is Nothing Then
			saveAsToolStripMenuItem.Visible = True
			ClearToolStripMenuItem.Enabled = True
		Else
			saveAsToolStripMenuItem.Visible = False
			ClearToolStripMenuItem.Enabled = False
		End If

		If Not _file Is Nothing Then
			saveToolStripMenuItem.Enabled = True
		Else
			saveToolStripMenuItem.Enabled = False
		End If

		If _allowPaste Then 
			If clipboard.ContainsText Or clipboard.ContainsImage Then
				pasteToolstripMenuItem.Enabled = True
			Else
				pasteToolstripMenuItem.Enabled = False
			End If
		End If
		
		toolStripSeparator4.Visible = _showQuit
		quitToolStripMenuItem.Visible = _showQuit

		selectionToolStripMenuItem.Visible = _selectionActive

	End Sub


	Dim Private _file As String = Nothing
	
	Public ReadOnly Property file As String
		Get
			If Not _file Is Nothing Then
				Return _file
			Else
				Return Nothing
			End If
		End Get
	End Property

	Dim private _showOpen As Boolean = False

	Public Property ShowOpen() As Boolean
		Get
			Return _showOpen
		End Get
		Set (ByVal Value As Boolean)
			_showOpen = Value
		End Set
	End Property

	Dim private _showColor As Boolean = True

	Public Property ShowColor() As Boolean
		Get
			Return _showColor
		End Get
		Set (ByVal Value As Boolean)
			_showColor = Value
		End Set
	End Property
	
	Dim private _showRoundedRectangle As Boolean = False
	
	Public Property ShowRoundedRectangle() As Boolean
		Get
			Return _showRoundedRectangle
		End Get
		Set (ByVal Value As Boolean)
			_showRoundedRectangle = Value
		End Set
	End Property

	Dim private _allowPaste As Boolean = True

	Public Property AllowPaste() As Boolean
		Get
			Return _allowPaste
		End Get
		Set (ByVal Value As Boolean)
			_allowPaste = Value
		End Set
	End Property

	Dim private _showSave As Boolean = True

	Public Property showSave() As Boolean
		Get
			Return _showSave
		End Get
		Set (ByVal Value As Boolean)
			_showSave = Value
		End Set
	End Property
	
	Dim private _allowDrop As Boolean = False

	Public Property openDroppedFile() As Boolean
		Get
			Return _allowDrop
		End Get
		Set (ByVal Value As Boolean)
			_allowDrop = Value
			me.AllowDrop = _allowDrop
		End Set
	End Property

	Sub annotateBoxDragDrop(sender As Object, e As DragEventArgs)
		If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
			Dim droppedFiles() As String
			droppedFiles = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
			For Each droppedFile As String In droppedFiles
				If System.IO.File.Exists(droppedFile) And "png|gif|jpg".Contains(System.IO.Path.GetExtension(droppedFiles(0)).ToLower().Trim("."c)) Then
					Debug.WriteLine(droppedFile)
					open(droppedFile)
					Exit For
				End If
			Next
		End If
	End Sub

	Sub annotateBoxDragOver(sender As Object, e As DragEventArgs)
		e.Effect = DragDropEffects.None
		
		If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
			Dim droppedFiles() As String
			droppedFiles = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
			If System.IO.File.Exists(droppedFiles(0)) And "png|gif|jpg".Contains(System.IO.Path.GetExtension(droppedFiles(0)).ToLower().Trim("."c)) Then
				e.Effect = DragDropEffects.Copy
			End If
		Else
			e.Effect = DragDropEffects.None
		End If
	End Sub
	
	
	Public Sub Copy()
		If _selectionActive Then
			clipboard.SetImage(CropImage(m_currentBitmap,_selectionRect))
		Else ' copy all
			Clipboard.SetImage(picCanvas.Image)
		End If
	End Sub
	
	Public Sub Paste()
		If _allowPaste Then
			updateUndoBuffer()
			insertClipboard(m_X1,m_Y1)
		End If
		
'		if clipboard.ContainsImage And _allowPaste Then
'			m_OriginalBitmap = New Bitmap(clipboard.GetImage)
'	        m_CurrentBitmap = New Bitmap(clipboard.GetImage)
'	        picCanvas.Image = m_CurrentBitmap
'	        UndoBuffer.Clear()
'		End If
	End Sub
	
	Sub CopyToolStripMenuItemClick(sender As Object, e As EventArgs)
		Copy()
	End Sub
		
	Sub PasteToolStripMenuItemClick(sender As Object, e As EventArgs)
		Paste()
	End Sub

    
    Private Function CropImage(ByVal SrcBmp As Bitmap, ByVal SrcRect As Rectangle) As Bitmap
    ' Private Function CropImage(ByVal SrcBmp As Bitmap, ByVal NewSize As Size, ByVal StartPoint As Point) As Bitmap
   	    ' Set up the rectangle that shows the portion of image to be cropped
    '    Dim SrcRect As New Rectangle(StartPoint.X, StartPoint.Y, NewSize.Width, NewSize.Height)
        ' Now set up the destination rectangle(DestRect) and bitmap(DestBmp)
    '    Dim DestRect As New Rectangle(0, 0, NewSize.Width, NewSize.Height)
    '    Dim DestBmp As New Bitmap(NewSize.Width, NewSize.Height, Imaging.PixelFormat.Format32bppArgb)
    	Dim DestRect As New Rectangle(0, 0, SrcRect.Width, SrcRect.Height)
    	Dim DestBmp As New Bitmap(DestRect.Width, DestRect.Height, Imaging.PixelFormat.Format32bppArgb)
        ' Set the graphics to the destination bitmap(DestBmp)
        Dim g As Graphics = Graphics.FromImage(DestBmp)
        ' and draw the image from source bitmap (SrcRect) to the
        ' destination bitmap(DestBmp) using the destination and
        ' source rectangles.
        g.DrawImage(SrcBmp, DestRect, SrcRect, GraphicsUnit.Pixel)
        ' finally return the destBmp
        Return DestBmp
    End Function

	Dim private _showIcon As Boolean = False

	Property showIcon As Boolean
		Get
			Return _showIcon
		End Get
		Set(ByVal Value As Boolean)
			_showIcon = Value
		End Set
	End Property

	Dim private _showQuit As Boolean = False

	Property showQuit As Boolean
		Get
			Return _showQuit
		End Get
		Set(ByVal Value As Boolean)
			_showQuit = Value
		End Set
	End Property
	
	Sub PicCanvasPaint(sender As Object, e As PaintEventArgs)
		#If DEBUG Then
			e.Graphics.DrawString("Debug",me.Font,New Drawing.SolidBrush(annotateColor),10,10)
		#End If
		' draw the current tool icon
'        If _showIcon then
'	        Dim front_pen As New Pen(Color.Red, 1)
'			Dim back_pen As New Pen(Color.White, 3)
'
'	        Dim new_rect As New Rectangle(4,4,20,14)
'			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
'
'			Select Case tool
'			Case "rectangle"
'	            e.Graphics.DrawRectangle(back_pen, new_rect)
'	            e.Graphics.DrawRectangle(front_pen, new_rect)
'			Case "ellipse"
'	            e.Graphics.DrawEllipse(back_pen, new_rect)
'	            e.Graphics.DrawEllipse(front_pen, new_rect)
'			End Select
'			front_Pen.Dispose()
'			back_pen.Dispose()
'		End if

		' draw the current selection
		Select Case tool
			
		Case "select"
			If (_selectionActive Or m_SelectingArea) And Not _selectionrect = Nothing Then
		        Dim s_Pen As New Pen(Color.Gray, 1)
		        s_Pen.DashStyle = Drawing2D.DashStyle.Dot
				e.Graphics.DrawRectangle(pens.White, _selectionrect)
				e.Graphics.DrawRectangle(s_pen, _selectionrect)
			End If
		
		Case "text"
			Dim sf As New StringFormat
	    	sf.LineAlignment = StringAlignment.Near
	    	sf.Alignment = StringAlignment.Near
			Dim font As New Font(textFont, textSize, textStyle, graphicsunit.pixel)
			Dim brush As New SolidBrush(annotateColor)
			Dim _height As Integer = e.Graphics.MeasureString("x", font).Height
			If system.Text.RegularExpressions.Regex.Matches(_text,"\n",system.Text.RegularExpressions.RegexOptions.Multiline).Count > 0 Then
				_height = (system.Text.RegularExpressions.Regex.Matches(_text,"\n",system.Text.RegularExpressions.RegexOptions.Multiline).Count + 1) * _height
			End If
			Dim _width As Integer = e.Graphics.MeasureString(_text, font).Width
			if _text.Length = 0 then _width = -1
			e.Graphics.DrawRectangle(pens.LightGray, t_X1-1,t_Y1-10-1,_width+2,_height+2)
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit ' .AntiAliasGridFit
			e.Graphics.DrawString(_text, font, brush, t_X1, t_Y1-10,sf) ' Brushes.White
			Debug.WriteLine(_text)
		End Select
	End Sub

	
	Sub ClearToolStripMenuItemClick(sender As Object, e As EventArgs)
		Clear()
	End Sub
	
'   ' check if mouse is over selection
'	Private Function IsCollide(TargetX as integer TargetY as integer, _
'		TargetW as integer, TargetH as integer, _
'		ObjectX as integer, ObjectY as integer, _
'		ObjectW as integer, ObjectH as integer) As Boolean
'
'		If ObjectX > TargetX + TargetW or _
'		ObjectX + ObjectW < TargetX or _
'		ObjectY > TargetY + TargetH or _
'		ObjectY + ObjectH < TargetY then 'object not over target
'
'			'good
'			iscollide = false
'
'		else 'object is over target
'
'			'Collision
'			iscollide = true
'
'		end if
'	End Function


	Public Event ImageSaved(ByVal fileName As String)

'	Private WithEvents AnnotateBox1 As AnnotateBox
'
'	Sub AnnotateBox1ImageSaved(fileName As String) Handles AnnotateBox1.ImageSaved
'		Msgbox(System.IO.Path.GetFileName(fileName))
'	End Sub

	Public Function saved() As Boolean
		If UndoBuffer.Count > 0 Then
			Return False
		Else
			Return True
		End If
	End Function
	
	
	Public Event Quit()
	Sub QuitToolStripMenuItemClick(sender As Object, e As EventArgs)
		If Not saved Then
			If Msgbox("Are you sure you want to quit without saving annotations?", MsgBoxStyle.YesNo,"Not Saved") = Msgboxresult.Yes Then
				RaiseEvent Quit()
			End If
		Else
			RaiseEvent Quit()
		End If
	End Sub
	
	Public Sub CloseApp()
		QuitToolStripMenuItemClick(Nothing, Nothing)
	End Sub
	
	Private Sub DrawRoundRect(ByVal g As Graphics, ByVal p As Pen, ByVal x As Single, ByVal y As Single, ByVal width As Single, ByVal height As Single, ByVal radius As Single)
	    Dim gp As Drawing2D.GraphicsPath = New Drawing2D.GraphicsPath()
	
	    gp.AddLine(X + radius, Y, X + width - (radius*2), Y)
	    gp.AddArc(X + width - (radius*2), Y, radius*2, radius*2, 270, 90)
	    gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius*2))
	    gp.AddArc(X + width - (radius*2), Y + height - (radius*2), radius*2, radius*2,0,90)
	    gp.AddLine(X + width - (radius*2), Y + height, X + radius, Y + height)
	    gp.AddArc(X, Y + height - (radius*2), radius*2, radius*2, 90, 90)
	    gp.AddLine(X, Y + height - (radius*2), X, Y + radius)
	    gp.AddArc(X, Y, radius*2, radius*2, 180, 90)
	
	    gp.CloseFigure()
	
	    g.DrawPath(p, gp)
	
	    gp.Dispose()
	End Sub
		
	Public annotateColor As Color = Color.Red
	public textColor As Color = Color.Black
		
	Sub ColorToolStripMenuItemClick(sender As Object, e As EventArgs)
		Dim result As DialogResult
		
		result = colorDialog1.ShowDialog()
		
		If result = DialogResult.Cancel Then
		 Return
		End If
		
		annotateColor = colorDialog1.Color
	End Sub
	
	Sub CursorToolStripMenuItemClick(sender As Object, e As EventArgs)
		Dim _sender As ToolStripMenuItem = sender
		updateUndoBuffer()
		
		Dim imageComposite As BitMap
		imageComposite = picCanvas.Image
		Dim g As Graphics = Graphics.FromImage(imageComposite)
		Dim smallImage As Image = CreateImage(CreateIcon(Cursors.Default)) ' Image.FromFile("c:\temp\test.png")

		Select Case _sender.Text
			Case "Hand"
				smallImage = CreateImage(CreateIcon(Cursors.Hand))
		End Select
			
		g.DrawImage(smallImage, m_X1, m_Y1)
		' Display the result.
		picCanvas.Image = imageComposite
		g.Dispose
	End Sub
	
	Sub FlipToolStripMenuItemClick(sender As Object, e As EventArgs)
		' Dim _sender As ToolStripMenuItem = sender
		
		If _selectionActive Then picCanvas.Image = m_CurrentBitmap
		updateUndoBuffer()
		
		Dim imageComposite As BitMap
		imageComposite = picCanvas.Image
		
		Dim _workBitmap As Bitmap
		_workBitmap = imageComposite.Clone(_activeRect,imageComposite.PixelFormat)
		If sender.Text = "Flip Vertical" Then
			_workBitmap.RotateFlip(rotateFliptype.RotateNoneFlipY)
		Else
			_workBitmap.RotateFlip(rotateFliptype.RotateNoneFlipX)
		End If

		Dim g As Graphics = Graphics.FromImage(imageComposite)
		g.DrawImage(_workBitmap,_activeRect)
		
		picCanvas.Image = imageComposite
		g.Dispose

	End Sub

	Sub FlipImage()
		' If not imageEditingMode then Exit Sub
		' If Not _selectionActive Then Exit Sub
		
		If _selectionRect = Nothing Then
			_activeRect = New Rectangle(0, 0, picCanvas.Width, picCanvas.Height)
		Else
			_activeRect = _selectionRect
		End If

		picCanvas.Image = m_CurrentBitmap
		updateUndoBuffer()
		
		Dim imageComposite As BitMap
		imageComposite = picCanvas.Image
		
		Dim _workBitmap As Bitmap
		_workBitmap = imageComposite.Clone(_activeRect,imageComposite.PixelFormat)
		_workBitmap.RotateFlip(rotateFliptype.RotateNoneFlipX)

		Dim g As Graphics = Graphics.FromImage(imageComposite)
		g.DrawImage(_workBitmap,_activeRect)
		
		picCanvas.Image = imageComposite
		g.Dispose

	End Sub
	
	Sub FillToolStripMenuItemClick(sender As Object, e As EventArgs)
		If _selectionActive Then picCanvas.Image = m_CurrentBitmap
		updateUndoBuffer()
		
		Dim imageComposite As BitMap
		imageComposite = picCanvas.Image
		
		Dim g As Graphics = Graphics.FromImage(imageComposite)
		Dim brush As New Drawing.SolidBrush(annotateColor)
		' transparent fill
		' brush = Drawing.SolidBrush(Color.FromArgb(50, 255, 255, 0))
		' g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected
		g.FillRectangle(brush,_activeRect)
		
		picCanvas.Image = imageComposite
		g.Dispose

	End Sub

	Sub ToolStripMenuItemNumber(sender As Object, e As EventArgs)
		' Dim _sender As ToolStripMenuItem = sender
		updateUndoBuffer()
		' sender.Image
		NumberedItem(Sender.Text, m_X1, m_Y1)
	End Sub
	
	' insert numbered item
	Private Sub NumberedItem(ByVal txt As String, ByVal x As Single, ByVal y As Single)
	    
		Dim imageComposite As BitMap
		imageComposite = picCanvas.Image
		Dim gr As Graphics = Graphics.FromImage(imageComposite)
	    
	    ' Mark the center for debugging.
	'    gr.DrawLine(Pens.Red, x - 10, y, x + 10, y)
	'    gr.DrawLine(Pens.Red, x, y - 10, x, y + 10)
	
	    ' Make a StringFormat object that centers
	    Dim sf As New StringFormat
	    sf.LineAlignment = StringAlignment.Center
	    sf.Alignment = StringAlignment.Center
		' Me.Font.FontFamily 10
		Dim font As New Font(textFont, 11, fontStyle.Bold, graphicsunit.Point)
		Dim _width As integer = gr.MeasureString("W", font).Width + 2 ' 16
		If Not _width Mod 2 = 0 Then _width = _width + 1
		
		Dim _rect As New Rectangle(m_X1-(_width/2),m_Y1-(_width/2),_width,_width)

		' Draw box
		Dim brush As New Drawing.SolidBrush(annotateColor)
		Select numberedItemStyle.ToLower
		Case "box"
			gr.FillRectangle(brush,_rect)
		Case "circle"
			gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
			gr.FillEllipse(brush,_rect)
			' correct text position
	    	x = x + 1
	    	y = y + 1
		Case "triangle"
			gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
			Dim myPoints() As Point
			ReDim myPoints(2)
			myPoints(0) = New Point(m_X1, m_Y1-(_width/2)-2)
			myPoints(1) = New Point(m_X1-(_width/2)-2, m_Y1+(_width/2)+2)
			myPoints(2) = New Point(m_X1+(_width/2)+2, m_Y1+(_width/2)+2)
			gr.FillPolygon(brush,myPoints)
			' correct text position
	    	x = x + 1
	    	y = y + 5
		Case "none"
			
		End Select
		
	    ' Draw text
	    gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit ' ClearTypeGridFit
	    gr.DrawString(txt, font, Brushes.White, x, y, sf)
	    sf.Dispose()
	    
		picCanvas.Image = imageComposite
		gr.Dispose
	End Sub

	
	Sub InsertClipboardContentsToolStripMenuItemClick(sender As Object, e As EventArgs)
		updateUndoBuffer()
		insertClipboard(m_X1, m_Y1)
	End Sub

	Private Sub insertClipboard(ByVal x As Single, ByVal y As Single)
	    
		Dim imageComposite As BitMap
		imageComposite = picCanvas.Image
		Dim gr As Graphics = Graphics.FromImage(imageComposite)
	    
	    ' Mark the center for debugging.
	'    gr.DrawLine(Pens.Red, x - 10, y, x + 10, y)
	'    gr.DrawLine(Pens.Red, x, y - 10, x, y + 10)
	
		Try
		If clipboard.ContainsText Then
			
			dim txt As String = clipboard.GetText(textdataformat.UnicodeText)
			
		    ' Make a StringFormat object the align text at cursor
		    Dim sf As New StringFormat
		    sf.LineAlignment = StringAlignment.Near
		    sf.Alignment = StringAlignment.Near
			' Me.Font.FontFamily 10
			Dim font As New Font(textFont, 11, fontStyle.Bold, graphicsunit.Point)

			Dim brush As New Drawing.SolidBrush(annotateColor)
		    ' Draw text
		    gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit ' ClearTypeGridFit
		    gr.DrawString(txt, font, brush, x, y, sf)
		    sf.Dispose()
		Else If clipboard.ContainsImage Then

			Dim image As Image = clipboard.GetImage() ' Image.FromFile("c:\temp\test.png")
			gr.DrawImage(image, x, y)

		End If
		Catch e As Exception
			Debug.WriteLine("Problem with clipboard data" & vbcrlf & e.Message)
		End Try
		
	    ' Display the result.
		picCanvas.Image = imageComposite
		gr.Dispose
	End Sub
	
		#Region "Converting Cursors, Icons, Images, and Bitmaps"
		''' <summary>
		''' Converts an Image into a Cursor.
		''' </summary>
		''' <param name="image">The Image you want to Convert to a Cursor.</param>
		''' <returns>A Cursor</returns>
		Public Function CreateCursor(image As Image) As Cursor
			'Get the Bitmap form of the Image
			Dim bitmap As New Bitmap(image)
			'Have the Graphics Work from the Bitmap
			Dim graphics__1 As Graphics = Graphics.FromImage(bitmap)
			'Get the Icon Handle
			Dim handle As IntPtr = bitmap.GetHicon()
			'Create the new Cursor
			Dim myCursor As New Cursor(handle)
			'Return the cursor
			Return myCursor
		End Function

		''' <summary>
		''' Converts an Image into an Icon.
		''' </summary>
		''' <param name="image">The Image you want to Convert to an Icon.</param>
		''' <returns>An Icon</returns>
		Public Function CreateIcon(image As Image) As Icon
			'Get the Bitmap from the Image
			Dim bitmap As New Bitmap(image)
			'Get the Bitmap Handle
			Dim handle As IntPtr = bitmap.GetHicon()
			'Create the Icon from the Bitmap's Handle
			Dim icon__1 As Icon = Icon.FromHandle(handle)
			'Return the Icon
			Return icon__1
		End Function

		''' <summary>
		''' Converts an Icon into an Image.
		''' </summary>
		''' <param name="icon">The Icon you want to Convert to an Image.</param>
		''' <returns>An Image</returns>
		Public Function CreateImage(icon__1 As Icon) As Image
			'Get the Bitmap from the Icon
			Dim bitmap As New Bitmap(icon__1.ToBitmap())
			'Return the Bitmap
			Return bitmap
		End Function

		''' <summary>
		''' Converts an Image into a Bitmap.
		''' </summary>
		''' <param name="image">The Image you want to Convert to a Bitmap.</param>
		''' <returns>A Bitmap</returns>
		Public Function CreateBitmap(image As Image) As Bitmap
			'Get the Image
			Dim bitmap As New Bitmap(image)
			'Return the Image as a Bitmap
			Return bitmap
		End Function

		''' <summary>
		''' Converts a Bitmap into an Image.
		''' </summary>
		''' <param name="bitmap">The Bitmap you want to Convert to an Image.</param>
		''' <returns>An Image</returns>
		Public Function CreateImage(bitmap As Bitmap) As Image
			'Get the Bitmap
			Dim image As Image = bitmap
			'Return the Bitmap as an Image
			Return image
		End Function

		''' <summary>
		''' Converts a Cursor to an Icon.
		''' </summary>
		''' <param name="cursor">The Curosr you want to Convert to an Icon.</param>
		''' <returns>An Icon</returns>
		Public Function CreateIcon(cursor As Cursor) As Icon
			'Create an Icon from the Cursor's Handle
			Dim icon__1 As Icon = Icon.FromHandle(cursor.CopyHandle())
			'Return the Icon
			Return icon__1
		End Function
		#End Region
	
	Sub SetColorToolStripMenuItemClick(sender As Object, e As EventArgs)
		' Dim _sender As ToolStripMenuItem = sender
		annotateColor = sender.BackColor
	End Sub

	Dim _activeRect As Rectangle
	

	Sub ImageToolStripMenuItemDropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)
		' _activeRect is full image
		_activeRect = New Rectangle(0, 0, picCanvas.Width, picCanvas.Height)
	End Sub
	
	Sub SelectionToolStripMenuItemDropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)
		' _activeRect is selection
		_activeRect = _selectionRect
	End Sub

	Dim _text As String = ""
	
	Dim t_X1, t_Y1 As Integer
	Sub TextBox1TextChanged(sender As Object, e As EventArgs)
		_text = textbox1.Text
		Me.Refresh
	End Sub
	
	Sub TextToolStripMenuItemCheckedChanged(sender As Object, e As EventArgs)
		tool = "text"
		panelTextbox.Visible = TextToolStripMenuItem.Checked
		if Not TextToolStripMenuItem.Checked Then _text = ""
		t_X1 = m_X1
		t_Y1 = m_Y1
		Me.Refresh
	End Sub

	' Ignore arrows in textbox
	Private Sub textBox1_KeyUp(sender As Object, e As KeyEventArgs)
		If e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Home  Or e.KeyCode = Keys.End Then
			e.SuppressKeyPress = True
			textbox1.Select(textbox1.Text.Length,0)
		End If
	End Sub

	' detect Enter (Shift-Enter)key press in textbox
	Sub TextBox1KeyPress(sender As Object, e As KeyPressEventArgs)

		If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.[Return]) Then
			' use Shift-Enter for linebreaks
			If Not Control.ModifierKeys = Keys.Shift Then
				e.Handled = True
				insertText()
				Exit Sub
			End If
		End If
		
	End Sub

	Sub insertText()
		updateUndoBuffer()
		
		Dim imageComposite As BitMap
		imageComposite = picCanvas.Image
		
		Dim g As Graphics = Graphics.FromImage(imageComposite)
		
		Dim sf As New StringFormat
    	sf.LineAlignment = StringAlignment.Near
    	sf.Alignment = StringAlignment.Near
		Dim font As New Font(textFont, textSize, textStyle, graphicsunit.pixel)
		Dim brush As New SolidBrush(annotateColor)
		g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit
		g.DrawString(_text, font, brush, t_X1, t_Y1-10,sf)
		picCanvas.Image = imageComposite
		g.Dispose
		textbox1.Text = ""
		_text = ""
		
	End Sub

	Sub setLineWidth(sender As Object, e As EventArgs)
		For each item As ToolStripMenuItem in contextMenuStripLine.Items
			item.Checked = False
		Next
		sender.Checked = True
		lineWidth = CInt(sender.Text)
	End Sub

	Public Function moveRect() As boolean
		' $$$ $$$ $$$
 		Debug.WriteLine("move!")
 		' get latest image
		Dim imageComposite As BitMap
		imageComposite = UndoBuffer.Peek()
		Dim _g As Graphics = Graphics.FromImage(imageComposite)

		' now draw new position of latest rectangle
		Dim brush As New SolidBrush(annotateColor)
		Dim customPen As New Pen(brush, lineWidth)
					
		' draw the rectangle
        Dim new_rect As New Rectangle( _
            activeRect.X + 10, _
           	activeRect.Y + 10, _
            activeRect.Width, _
            activeRect.Height)
        _g.DrawRectangle(customPen, new_rect)
		
		activeRect = new_Rect

        m_CurrentBitmap = imageComposite

        ' Display the result.
        picCanvas.Image = imageComposite
		Return True
	End Function
	
	Dim highlightcolorname As String = "yellow"
	
	Sub HighlightColorToolStripMenuItemClick(sender As ToolStripMenuItem, e As EventArgs)
		
		HighlightColorYellowToolStripMenuItem.Checked = False
		HighlightColorGreenToolStripMenuItem.Checked = False
		HighlightColorBlueToolStripMenuItem.Checked = False
		HighlightColorMagentaToolStripMenuItem.Checked = False
		
		sender.Checked = True
		highlightcolorname = sender.Text.ToLower
		
'		Select Case sender.Text.ToLower()
'			Case "yellow"
'				 highlightColor = Color.FromArgb(100, 255, 255, 0)
'			Case "green"
'				 highlightColor = Color.FromArgb(100, 0, 255, 0)
'			Case "blue"
'				 highlightColor = Color.FromArgb(100, 0, 255, 255)
'			Case "magenta"
'				highlightColor = Color.FromArgb(100, 255, 255, 0)
'		End Select
	
	End Sub
	
	' get brightness of color
	Private Shared Function Brightness(c As Color) As Integer
		Return CInt(Math.Truncate(Math.Sqrt(c.R * c.R * 0.241 + c.G * c.G * 0.691 + c.B * c.B * 0.068)))
	End Function
	' http://www.nbdtech.com/Blog/archive/2008/04/27/Calculating-the-Perceived-Brightness-of-a-Color.aspx
	' Color textColor = Brightness(backgroundColor) < 130 ? Colors.White : Colors.Black;
	
	' transform
	Sub transformToolStripMenuItemClick(sender As ToolStripMenuItem, e As EventArgs)
		
		transformSelection(sender.text)
		
	End Sub	
	
	Sub transformSelection(transformation As String)
		If _selectionRect = Nothing Then
			_activeRect = New Rectangle(0, 0, picCanvas.Width, picCanvas.Height)
		Else
			_activeRect = _selectionRect
		End If

		picCanvas.Image = m_CurrentBitmap
		updateUndoBuffer()
		
		Dim imageComposite As BitMap
		imageComposite = picCanvas.Image
		
		Dim _workBitmap As Bitmap = Nothing
		Select Case transformation.ToLower().Replace(" ","")
'			Case "grayscale"
'				_workBitmap = transform.grayscale(imageComposite.Clone(_activeRect,imageComposite.PixelFormat))
			Case "invert"
'				_workBitmap = transform.negative(imageComposite.Clone(_activeRect,imageComposite.PixelFormat))
			Case "1bit"
'				_workBitmap = transform.Create1bppImageWithErrorDiffusion(imageComposite.Clone(_activeRect,imageComposite.PixelFormat))
			Case Else
				_workBitmap = imageComposite.Clone(_activeRect,imageComposite.PixelFormat)
		End Select
		

		Dim g As Graphics = Graphics.FromImage(imageComposite)
		g.DrawImage(_workBitmap,_activeRect)
		
		picCanvas.Image = imageComposite
		g.Dispose
	
	End Sub

End Class
