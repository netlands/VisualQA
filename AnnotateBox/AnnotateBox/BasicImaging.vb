Imports System.Drawing.Imaging

NameSpace BasicImaging

Public Class transform
	
	' imaging functions

	Public zoom As Integer
	Public rotation As Integer
	
    Public Function Resize(ByVal imageFile As Bitmap, ByVal Width As Integer, Optional ByVal Height As Integer = -1, Optional ByVal Scale As Boolean = False, Optional Antialias As Boolean = True) As System.Drawing.Bitmap
					' ResizeImage(ByVal imageFile As String, ByVal Width As Integer, Optional ByVal Height As Integer = -1, Optional ByVal Scale As Boolean = False, Optional Antialias As Boolean = True) As System.Drawing.Bitmap
     
	  
	If Height = -1 And width = 100 Then ' nothing to do
		Return imageFile
		zoom = 100
	Else ' Resize the image
	   
	Dim newSize As Size
	Dim originalImage As Bitmap = imageFile ' Dim originalImage As New Bitmap(imageFile) 
 
		If Height = -1 Then	' width is a percentage
			Scale = False ' no need to calculate size
			height = (originalImage.Height/100) * width
			width = (originalImage.Width/100) * width
			zoom = Width
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
	   	zoom = (scaledWidth/originalWidth) * 100
	   	
	Else ' stretch the image to fit the given size
	   	
	   	newSize = New Size(Width, Height)
			zoom = 100	

		End If
		
		
		Dim resizedImage As New Bitmap(originalImage, newSize)
		
		' make smaller use .HighQualityBicubic
		' make larger use .HighQualityBilinear
		
	If antialias Then
			Dim tempBitmap As New Bitmap(newSize.Width, newSize.Height)
		Dim graphicsObject As Graphics = Graphics.FromImage(tempBitmap)
			If (originalImage.Width * originalImage.Height) < (newSize.Width * newSize.Height) then ' make larger
				graphicsObject.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear ' .HighQualityBilinear .NearestNeighbour
			Else ' make smaller
				graphicsObject.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
			End If
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

	Public Function Rotate(ByVal imageFile As Bitmap, ByVal Angle As Integer) As System.Drawing.Bitmap 
					' RotateImage(ByVal imageFile As String, ByVal Angle As Integer) As System.Drawing.Bitmap 
	
		
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
	
	Public Function filter(ByVal imageFile As Bitmap, ByVal cm As ColorMatrix) As System.Drawing.Bitmap
	
		' Dim originalImage As New Bitmap(imageFile) ' (ByVal imageFile As String)
		Dim originalImage As Bitmap = imageFile
		
		Dim imgattr As New ImageAttributes()
	
	    ' Create an output Bitmap and Graphics object.
	    Dim newImage As New Bitmap(originalImage.Width, originalImage.Height)
	    Dim g As Graphics = Graphics.FromImage(newImage)
	    
	    Dim rc As New Rectangle(0, 0, originalImage.Width, originalImage.Height)
	    
	
	    ' associate the ColorMatrix object with an ImageAttributes object
	    imgattr.SetColorMatrix(cm) 
	
	    ' draw the copy of the source image back over the original image, 
	    'applying the ColorMatrix
	    g.DrawImage(originalImage, rc, 0, 0, originalImage.Width, originalImage.Height, _
				   GraphicsUnit.Pixel, imgattr)
	
	    
		g.Dispose()
		originalImage = Nothing
		g = Nothing
		imgattr = Nothing
		rc = Nothing
	    
	    Return newImage
	    
	End Function
	
	Public Function brightness(ByVal imageFile As Bitmap, ByVal brightnessValue As Single) As System.Drawing.Bitmap
	
		' allowed values -1 and 1, 0 is default 
		If (brightnessValue < -1 And brightnessValue > 1) Then 
			brightnessValue = 0
			Return imageFile
		Else
	
			' brightness
			Dim cm As New Imaging.ColorMatrix(New Single()() { _
			New Single() {1.0F, 0.0F, 0.0F, 0.0F, 0.0F}, _
			New Single() {0.0F, 1.0F, 0.0F, 0.0F, 0.0F}, _
			New Single() {0.0F, 0.0F, 1.0F, 0.0F, 0.0F}, _
			New Single() {0.0F, 0.0F, 0.0F, 1.0F, 0.0F}, _
			New Single() {brightnessValue, brightnessValue, brightnessValue, 1.0F, 1.0F}}) 
	   
	    	Return filter(imageFile, cm)
	    
	    End If
	    
	End Function
	
	Public Function contrast(ByVal imageFile As Bitmap, ByVal contrastValue As Single) As System.Drawing.Bitmap
	
		' allowed values between 0 and 3
		If (contrastValue < 0 And contrastValue > 3) Then 
			Return imageFile
		Else
		
			If contrastValue = 1 Then ' no need to do anything
				Return imageFile
				Exit Function
			End If
		
			Dim Trans As Single = (1 - ContrastValue) / 2
	
			' contrast
			Dim cm As New Imaging.ColorMatrix(New Single()() { _
			New Single() {contrastValue, 0.0F, 0.0F, 0.0F, 0.0F}, _
			New Single() {0.0F, contrastValue, 0.0F, 0.0F, 0.0F}, _
			New Single() {0.0F, 0.0F, contrastValue, 0.0F, 0.0F}, _
			New Single() {0.0F, 0.0F, 0.0F, 1.0F, 0.0F}, _
			New Single() {Trans, Trans, Trans, 0.0F, 1.0F}}) 
	   
	    	Return filter(imageFile, cm)
	    
	    End If
	    
	End Function
	
	Public Function translate(ByVal imageFile As Bitmap, ByVal red As Single, _
				ByVal green As Single, ByVal blue As Single, _
				Optional ByVal alpha As Single = 0) As System.Drawing.Bitmap
	    
	    Dim sr, sg, sb, sa As Single
	    
	    ' noramlize the color components to 1
	    sr = red / 255
	    sg = green / 255
	    sb = blue / 255
	    sa = alpha / 255
	 
	    ' create the color matrix
	    dim cm As New ColorMatrix(New Single()() _
				{New Single() {1, 0, 0, 0, 0}, _
				New Single() {0, 1, 0, 0, 0}, _
				New Single() {0, 0, 1, 0, 0}, _
				New Single() {0, 0, 0, 1, 0}, _
				New Single() {sr, sg, sb, sa, 1}})
	
	    ' apply the matrix to the image
		Return filter(imageFile, cm)
	
	End Function
	
	Public Function grayscale(ByVal imageFile As Bitmap) As System.Drawing.Bitmap
		
		' grayscale
'		Dim cm As ColorMatrix = New ColorMatrix(New Single()() _
'			{New Single() {0.299, 0.299, 0.299, 0, 0}, _
'			New Single() {0.587, 0.587, 0.587, 0, 0}, _
'			New Single() {0.114, 0.114, 0.114, 0, 0}, _
'			New Single() {0, 0, 0, 1, 0}, _
'			New Single() {0, 0, 0, 0, 1}})			
	    
	    
		    Dim cm As ColorMatrix = New ColorMatrix(New Single()() _
		    	{new Single(){0.3086F,0.3086F,0.3086F,0,0}, _
		    new Single(){0.6094F,0.6094F,0.6094F,0,0}, _
		    new Single(){0.082F,0.082F,0.082F,0,0}, _
		    new Single(){0,0,0,1,0,0}, _
		    new Single(){0,0,0,0,1,0}, _
		    new Single(){0,0,0,0,0,1}})	    
	    
	    
		'different matrices for grayscale conversion
		'CCIR Rec 709
		' Dim cm As ColorMatrix =  New ColorMatrix(New Single()() _
		'	{New Single() {0.213, 0.213, 0.213, 0, 0}, _
		'	New Single() {0.715, 0.715, 0.715, 0, 0}, _
		'	New Single() {0.072, 0.072, 0.072, 0, 0}, _
		'	New Single() {0, 0, 0, 1, 0}, _
		'	New Single() {0, 0, 0, 0, 1}})
		'NTSC/Pal
		' Dim cm As ColorMatrix = New ColorMatrix(New Single()() _
		'	{New Single() {0.299, 0.299, 0.299, 0, 0}, _
		'	New Single() {0.587, 0.587, 0.587, 0, 0}, _
		'	New Single() {0.114, 0.114, 0.114, 0, 0}, _
		'	New Single() {0, 0, 0, 1, 0}, _
		'	New Single() {0, 0, 0, 0, 1}})
		'Simple Average
		' Dim cm As ColorMatrix = New ColorMatrix(New Single()() _
		'	{New Single() {0.333, 0.333, 0.333, 0, 0}, _
		'	New Single() {0.333, 0.333, 0.333, 0, 0}, _
		'	New Single() {0.333, 0.333, 0.333, 0, 0}, _
		'	New Single() {0, 0, 0, 1, 0}, _
		'	New Single() {0, 0, 0, 0, 1}})
		'Weighted Average
		' Dim cm As ColorMatrix = New ColorMatrix(New Single()() _
		'	{New Single() {0.333, 0.333, 0.333, 0, 0}, _
		'	New Single() {0.444, 0.444, 0.444, 0, 0}, _
		'	New Single() {0.222, 0.222, 0.222, 0, 0}, _
		'	New Single() {0, 0, 0, 1, 0}, _
		'	New Single() {0, 0, 0, 0, 1}})
	  
	    Return filter(imageFile, cm)
	    
	End Function	
	
	Public Function negative(ByVal imageFile As Bitmap) As System.Drawing.Bitmap
	
		' negative
	    Dim cm As ColorMatrix = New ColorMatrix(New Single()() _
				   {New Single() {-1, 0, 0, 0, 0}, _
				    New Single() {0, -1, 0, 0, 0}, _
				    New Single() {0, 0, -1, 0, 0}, _
				    New Single() {0, 0, 0, 1, 0}, _
				    New Single() {1, 1, 1, 0, 1}}) ' corrected for pure black New Single() {0, 0, 0, 0, 1}})
	   
	    Return filter(imageFile, cm)
	    
	End Function
	
	Public Function sepia(ByVal imageFile As Bitmap) As System.Drawing.Bitmap
	
		' sepia
		Dim cm As New Imaging.ColorMatrix(New Single()() _
							{New Single() {0.393, 0.349, 0.272, 0, 0}, _
						 New Single() {0.769, 0.686, 0.534, 0, 0}, _
						 New Single() {0.189, 0.168, 0.131, 0, 0}, _
				 New Single() {0, 0, 0, 1, 0}, _
				 New Single() {0, 0, 0, 0, 1}})					 
	
	   
	    Return filter(imageFile, cm)
	    
	End Function
	
	Public Function opacityX(ByVal imageFile As Bitmap, ByVal opacityValue As Single) As System.Drawing.Bitmap
	
		If (opacityValue < 0 And opacityValue > 1) Then
			Return imageFile
			
		Else
	
			Dim Trans As Single = (1 - opacityValue)
			
			' opacity
			Dim cm As New Imaging.ColorMatrix(New Single()() { _
			New Single() {1.0F, 0.0F, 0.0F, 0.0F, 0.0F}, _
			New Single() {0.0F, 1.0F, 0.0F, 0.0F, 0.0F}, _
			New Single() {0.0F, 0.0F, 1.0F, 0.0F, 0.0F}, _
			New Single() {0.0F, 0.0F, 0.0F, Trans, 0.0F}, _
			New Single() {0.0F, 0.0F, 0.0F, 0.0F, 1.0F}}) 
		   
		    Return filter(imageFile, cm)
	    
	    End If
	    
	End Function
	
	Public Function translucent(ByVal imageFile As Bitmap) As System.Drawing.Bitmap
	
		' translucent
		Dim cm As New Imaging.ColorMatrix(New Single()() { _
		New Single() {1.0F, 0.0F, 0.0F, 0.0F, 0.0F}, _
		New Single() {0.0F, 1.0F, 0.0F, 0.0F, 0.0F}, _
		New Single() {0.0F, 0.0F, 1.0F, 0.0F, 0.0F}, _
		New Single() {0.0F, 0.0F, 0.0F, 0.0F, 0.0F}, _
		New Single() {0.0F, 0.0F, 0.0F, 0.4F, 1.0F}}) 
	   
	    Return filter(imageFile, cm)
	    
	End Function
	
	' black-and-white image functions from http://dobon.net/vb/dotnet/graphics/1bpp.html
	
	''' <summary>
	''' 指定された画像から1bppのイメージを作成する
	''' </summary>
	''' <param name="img">基になる画像</param>
	''' <returns>1bppに変換されたイメージ</returns>
	Public Function Create1bppImage(ByVal img As Bitmap) As Bitmap
	    '1bppイメージを作成する
	    Dim newImg As New Bitmap(img.Width, img.Height, _
				     PixelFormat.Format1bppIndexed)
	
	    'Bitmapをロックする
	    Dim bmpDate As BitmapData = newImg.LockBits( _
		New Rectangle(0, 0, newImg.Width, newImg.Height), _
		ImageLockMode.WriteOnly, newImg.PixelFormat)
	
	    '新しい画像のピクセルデータを作成する
	    Dim pixels As Byte() = New Byte(bmpDate.Stride * bmpDate.Height - 1) {}
	    For y As Integer = 0 To bmpDate.Height - 1
		For x As Integer = 0 To bmpDate.Width - 1
		    '明るさが0.5以上の時は白くする
		    If 0.5F <= img.GetPixel(x, y).GetBrightness() Then
			'ピクセルデータの位置
			Dim pos As Integer = (x >> 3) + bmpDate.Stride * y
			'白くする
			pixels(pos) = pixels(pos) Or CByte(&H80 >> (x And &H7))
		    End If
		Next
	    Next
	    '作成したピクセルデータをコピーする
	    Dim ptr As IntPtr = bmpDate.Scan0
	    System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, pixels.Length)
	
	    'ロックを解除する
	    newImg.UnlockBits(bmpDate)
	
	    Return newImg
	End Function

	''' <summary>
	''' ランダムディザリングを使用して、
	''' 指定された画像から1bppのイメージを作成する
	''' </summary>
	''' <param name="img">基になる画像</param>
	''' <returns>1bppに変換されたイメージ</returns>
	Public Function Create1bppImageWithRandomDithering( _
		ByVal img As Bitmap) As Bitmap
	    '乱数を生成するために、Randomオブジェクトを作成する
	    Dim rnd As New Random()
	
	    '1bppイメージを作成する
	    Dim newImg As New Bitmap(img.Width, img.Height, _
				     PixelFormat.Format1bppIndexed)
	
	    'Bitmapをロックする
	    Dim bmpDate As BitmapData = newImg.LockBits( _
		New Rectangle(0, 0, newImg.Width, newImg.Height), _
		ImageLockMode.WriteOnly, newImg.PixelFormat)
	
	    '新しい画像のピクセルデータを作成する
	    Dim pixels As Byte() = New Byte(bmpDate.Stride * bmpDate.Height - 1) {}
	    For y As Integer = 0 To bmpDate.Height - 1
		For x As Integer = 0 To bmpDate.Width - 1
		    '明るさがランダムな数値以上の時は白くする
		    If rnd.NextDouble() <= img.GetPixel(x, y).GetBrightness() Then
			'ピクセルデータの位置
			Dim pos As Integer = (x >> 3) + bmpDate.Stride * y
			'白くする
			pixels(pos) = pixels(pos) Or CByte(&H80 >> (x And &H7))
		    End If
		Next
	    Next
	    '作成したピクセルデータをコピーする
	    Dim ptr As IntPtr = bmpDate.Scan0
	    System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, pixels.Length)
	
	    'ロックを解除する
	    newImg.UnlockBits(bmpDate)
	
	    Return newImg
	End Function
	
	''' <summary>
	''' 配列ディザリングを使用して、
	''' 指定された画像から1bppのイメージを作成する
	''' </summary>
	''' <param name="img">基になる画像</param>
	''' <returns>1bppに変換されたイメージ</returns>
	Public Function Create1bppImageWithOrderedDithering( _
		ByVal img As Bitmap) As Bitmap
	    'しきい値マップを作成する
	    Dim thresholdMap As Single()() = New Single(3)() _
		{New Single(3) _
		    {1.0F / 17.0F, 9.0F / 17.0F, 3.0F / 17.0F, 11.0F / 17.0F}, _
		 New Single(3) _
		    {13.0F / 17.0F, 5.0F / 17.0F, 15.0F / 17.0F, 7.0F / 17.0F}, _
		 New Single(3) _
		    {4.0F / 17.0F, 12.0F / 17.0F, 2.0F / 17.0F, 10.0F / 17.0F}, _
		 New Single(3) _
		    {16.0F / 17.0F, 8.0F / 17.0F, 14.0F / 17.0F, 6.0F / 17.0F}}
	
	    '1bppイメージを作成する
	    Dim newImg As New Bitmap(img.Width, img.Height, _
				     PixelFormat.Format1bppIndexed)
	
	    'Bitmapをロックする
	    Dim bmpDate As BitmapData = newImg.LockBits( _
		New Rectangle(0, 0, newImg.Width, newImg.Height), _
		ImageLockMode.WriteOnly, newImg.PixelFormat)
	
	    '新しい画像のピクセルデータを作成する
	    Dim pixels As Byte() = New Byte(bmpDate.Stride * bmpDate.Height - 1) {}
	    For y As Integer = 0 To bmpDate.Height - 1
		For x As Integer = 0 To bmpDate.Width - 1
		    'しきい値マップの値と比較する
		    If thresholdMap(x Mod 4)(y Mod 4) <= _
			    img.GetPixel(x, y).GetBrightness() Then
			'ピクセルデータの位置
			Dim pos As Integer = (x >> 3) + bmpDate.Stride * y
			'白くする
			pixels(pos) = pixels(pos) Or CByte(&H80 >> (x And &H7))
		    End If
		Next
	    Next
	    '作成したピクセルデータをコピーする
	    Dim ptr As IntPtr = bmpDate.Scan0
	    System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, pixels.Length)
	
	    'ロックを解除する
	    newImg.UnlockBits(bmpDate)
	
	    Return newImg
	End Function	
	
	''' <summary>
	''' 誤差拡散法（Floyd-Steinbergディザリング）を使用して、
	''' 指定された画像から1bppのイメージを作成する
	''' </summary>
	''' <param name="img">基になる画像</param>
	''' <returns>1bppに変換されたイメージ</returns>
	Public Function Create1bppImageWithErrorDiffusion( _
		ByVal img As Bitmap) As Bitmap
	    '1bppイメージを作成する
	    Dim newImg As New Bitmap(img.Width, img.Height, _
				     PixelFormat.Format1bppIndexed)
	
	    'Bitmapをロックする
	    Dim bmpDate As BitmapData = newImg.LockBits( _
		New Rectangle(0, 0, newImg.Width, newImg.Height), _
		ImageLockMode.[WriteOnly], newImg.PixelFormat)
	
	    '現在の行と次の行の誤差を記憶する配列
	    Dim errors As Single()() = New Single(1)() _
		{New Single(bmpDate.Width) {}, _
		 New Single(bmpDate.Width) {}}
	
	    '新しい画像のピクセルデータを作成する
	    Dim pixels As Byte() = New Byte(bmpDate.Stride * bmpDate.Height - 1) {}
	    For y As Integer = 0 To bmpDate.Height - 1
		For x As Integer = 0 To bmpDate.Width - 1
		    'ピクセルの明るさに、誤差を加える
		    Dim err As Single = _
			img.GetPixel(x, y).GetBrightness() + errors(0)(x)
		    '明るさが0.5以上の時は白くする
		    If 0.5F <= err Then
			'ピクセルデータの位置
			Dim pos As Integer = (x >> 3) + bmpDate.Stride * y
			'白くする
			pixels(pos) = pixels(pos) Or CByte(&H80 >> (x And &H7))
			'誤差を計算（黒くした時の誤差はerr-0なので、そのまま）
			err -= 1.0F
		    End If
	
		    '誤差を振り分ける
		    errors(0)(x + 1) += err * 7.0F / 16.0F
		    If x > 0 Then
			errors(1)(x - 1) += err * 3.0F / 16.0F
		    End If
		    errors(1)(x) += err * 5.0F / 16.0F
		    errors(1)(x + 1) += err * 1.0F / 16.0F
		Next
		'誤差を記憶した配列を入れ替える
		errors(0) = errors(1)
		errors(1) = New Single(errors(0).Length - 1) {}
	    Next
	    '作成したピクセルデータをコピーする
	    Dim ptr As IntPtr = bmpDate.Scan0
	    System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, pixels.Length)
	
	    'ロックを解除する
	    newImg.UnlockBits(bmpDate)
	
	    Return newImg
	End Function
End Class


End NameSpace
