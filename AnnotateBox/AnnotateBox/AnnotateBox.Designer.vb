'
' Created by SharpDevelop.
' User: bergb
' Date: 2009/04/27
' Time: 15:28
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class AnnotateBox
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the control.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnnotateBox))
		Me.picCanvas = New System.Windows.Forms.PictureBox()
		Me.contextMenuStripCanvas = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.rectangleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.freehandToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.highlightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ellipseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.roundedRectangleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.lineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.arrowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.pixelateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.textToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.colorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.redToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.greenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.blueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.customToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.highlightColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.highlightColorYellowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.highlightColorGreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.highlightColorBlueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.HighlightColorMagentaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.lineWidthToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.contextMenuStripLine = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.toolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem14 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
		Me.insertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.cursorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.handToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
		Me.insertClipboardContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
		Me.undoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.resetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.imageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.contextMenuStripSelection = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.flipHorizontalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.flipVerticalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.fillToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.bitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.grayscaleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.invertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.selectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.selectAreaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.copyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.pasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
		Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.saveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.saveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.clearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
		Me.quitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.colorDialog1 = New System.Windows.Forms.ColorDialog()
		Me.textBox1 = New System.Windows.Forms.TextBox()
		Me.panelTextbox = New System.Windows.Forms.Panel()
		Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
		CType(Me.picCanvas,System.ComponentModel.ISupportInitialize).BeginInit
		Me.contextMenuStripCanvas.SuspendLayout
		Me.contextMenuStripLine.SuspendLayout
		Me.contextMenuStripSelection.SuspendLayout
		Me.panelTextbox.SuspendLayout
		Me.SuspendLayout
		'
		'picCanvas
		'
		Me.picCanvas.ContextMenuStrip = Me.contextMenuStripCanvas
		Me.picCanvas.Dock = System.Windows.Forms.DockStyle.Fill
		Me.picCanvas.Location = New System.Drawing.Point(0, 0)
		Me.picCanvas.Name = "picCanvas"
		Me.picCanvas.Size = New System.Drawing.Size(150, 163)
		Me.picCanvas.TabIndex = 0
		Me.picCanvas.TabStop = false
		AddHandler Me.picCanvas.Paint, AddressOf Me.PicCanvasPaint
		AddHandler Me.picCanvas.MouseDown, AddressOf Me.picCanvasMouseDown
		AddHandler Me.picCanvas.MouseMove, AddressOf Me.picCanvasMouseMove
		AddHandler Me.picCanvas.MouseUp, AddressOf Me.picCanvasMouseUp
		'
		'contextMenuStripCanvas
		'
		Me.contextMenuStripCanvas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.rectangleToolStripMenuItem, Me.freehandToolStripMenuItem, Me.highlightToolStripMenuItem, Me.ellipseToolStripMenuItem, Me.roundedRectangleToolStripMenuItem, Me.lineToolStripMenuItem, Me.arrowToolStripMenuItem, Me.pixelateToolStripMenuItem, Me.textToolStripMenuItem, Me.toolStripSeparator7, Me.colorToolStripMenuItem, Me.highlightColorToolStripMenuItem, Me.lineWidthToolStripMenuItem, Me.toolStripSeparator2, Me.insertToolStripMenuItem, Me.toolStripSeparator5, Me.undoToolStripMenuItem, Me.resetToolStripMenuItem, Me.imageToolStripMenuItem, Me.selectionToolStripMenuItem, Me.toolStripSeparator1, Me.selectAreaToolStripMenuItem, Me.copyToolStripMenuItem, Me.pasteToolStripMenuItem, Me.toolStripSeparator3, Me.OpenToolStripMenuItem, Me.saveToolStripMenuItem, Me.saveAsToolStripMenuItem, Me.clearToolStripMenuItem, Me.toolStripSeparator4, Me.quitToolStripMenuItem})
		Me.contextMenuStripCanvas.Name = "contextMenuStripCanvas"
		Me.contextMenuStripCanvas.Size = New System.Drawing.Size(178, 612)
		AddHandler Me.contextMenuStripCanvas.Opening, AddressOf Me.ContextMenuStripCanvasOpening
		'
		'rectangleToolStripMenuItem
		'
		Me.rectangleToolStripMenuItem.Checked = true
		Me.rectangleToolStripMenuItem.CheckOnClick = true
		Me.rectangleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.rectangleToolStripMenuItem.Image = CType(resources.GetObject("rectangleToolStripMenuItem.Image"),System.Drawing.Image)
		Me.rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem"
		Me.rectangleToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.rectangleToolStripMenuItem.Text = "Rectangle"
		AddHandler Me.rectangleToolStripMenuItem.CheckedChanged, AddressOf Me.RectangleToolStripMenuItemCheckedChanged
		AddHandler Me.rectangleToolStripMenuItem.Click, AddressOf Me.toolToolStripMenuItemClick
		'
		'freehandToolStripMenuItem
		'
		Me.freehandToolStripMenuItem.CheckOnClick = true
		Me.freehandToolStripMenuItem.Image = CType(resources.GetObject("freehandToolStripMenuItem.Image"),System.Drawing.Image)
		Me.freehandToolStripMenuItem.Name = "freehandToolStripMenuItem"
		Me.freehandToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.freehandToolStripMenuItem.Text = "Freehand"
		AddHandler Me.freehandToolStripMenuItem.CheckedChanged, AddressOf Me.FreehandToolStripMenuItemCheckedChanged
		AddHandler Me.freehandToolStripMenuItem.Click, AddressOf Me.toolToolStripMenuItemClick
		'
		'highlightToolStripMenuItem
		'
		Me.highlightToolStripMenuItem.CheckOnClick = true
		Me.highlightToolStripMenuItem.Image = CType(resources.GetObject("highlightToolStripMenuItem.Image"),System.Drawing.Image)
		Me.highlightToolStripMenuItem.Name = "highlightToolStripMenuItem"
		Me.highlightToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.highlightToolStripMenuItem.Text = "Highlight"
		AddHandler Me.highlightToolStripMenuItem.CheckedChanged, AddressOf Me.HighlightToolStripMenuItemCheckedChanged
		AddHandler Me.highlightToolStripMenuItem.Click, AddressOf Me.toolToolStripMenuItemClick
		'
		'ellipseToolStripMenuItem
		'
		Me.ellipseToolStripMenuItem.CheckOnClick = true
		Me.ellipseToolStripMenuItem.Image = CType(resources.GetObject("ellipseToolStripMenuItem.Image"),System.Drawing.Image)
		Me.ellipseToolStripMenuItem.Name = "ellipseToolStripMenuItem"
		Me.ellipseToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.ellipseToolStripMenuItem.Text = "Ellipse"
		AddHandler Me.ellipseToolStripMenuItem.CheckedChanged, AddressOf Me.EllipseToolStripMenuItemCheckedChanged
		AddHandler Me.ellipseToolStripMenuItem.Click, AddressOf Me.toolToolStripMenuItemClick
		'
		'roundedRectangleToolStripMenuItem
		'
		Me.roundedRectangleToolStripMenuItem.CheckOnClick = true
		Me.roundedRectangleToolStripMenuItem.Image = CType(resources.GetObject("roundedRectangleToolStripMenuItem.Image"),System.Drawing.Image)
		Me.roundedRectangleToolStripMenuItem.Name = "roundedRectangleToolStripMenuItem"
		Me.roundedRectangleToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.roundedRectangleToolStripMenuItem.Text = "Rounded Rectangle"
		Me.roundedRectangleToolStripMenuItem.Visible = false
		AddHandler Me.roundedRectangleToolStripMenuItem.CheckedChanged, AddressOf Me.RoundedRectangleToolStripMenuItemCheckedChanged
		AddHandler Me.roundedRectangleToolStripMenuItem.Click, AddressOf Me.toolToolStripMenuItemClick
		'
		'lineToolStripMenuItem
		'
		Me.lineToolStripMenuItem.CheckOnClick = true
		Me.lineToolStripMenuItem.Image = CType(resources.GetObject("lineToolStripMenuItem.Image"),System.Drawing.Image)
		Me.lineToolStripMenuItem.Name = "lineToolStripMenuItem"
		Me.lineToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.lineToolStripMenuItem.Text = "Line"
		AddHandler Me.lineToolStripMenuItem.CheckedChanged, AddressOf Me.LineToolStripMenuItemCheckedChanged
		AddHandler Me.lineToolStripMenuItem.Click, AddressOf Me.toolToolStripMenuItemClick
		'
		'arrowToolStripMenuItem
		'
		Me.arrowToolStripMenuItem.CheckOnClick = true
		Me.arrowToolStripMenuItem.Image = CType(resources.GetObject("arrowToolStripMenuItem.Image"),System.Drawing.Image)
		Me.arrowToolStripMenuItem.Name = "arrowToolStripMenuItem"
		Me.arrowToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.arrowToolStripMenuItem.Text = "Arrow"
		AddHandler Me.arrowToolStripMenuItem.CheckedChanged, AddressOf Me.arrowToolStripMenuItemCheckedChanged
		AddHandler Me.arrowToolStripMenuItem.Click, AddressOf Me.toolToolStripMenuItemClick
		'
		'pixelateToolStripMenuItem
		'
		Me.pixelateToolStripMenuItem.CheckOnClick = true
		Me.pixelateToolStripMenuItem.Image = CType(resources.GetObject("pixelateToolStripMenuItem.Image"),System.Drawing.Image)
		Me.pixelateToolStripMenuItem.Name = "pixelateToolStripMenuItem"
		Me.pixelateToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.pixelateToolStripMenuItem.Text = "Pixelate"
		AddHandler Me.pixelateToolStripMenuItem.CheckedChanged, AddressOf Me.PixelateToolStripMenuItemCheckedChanged
		AddHandler Me.pixelateToolStripMenuItem.Click, AddressOf Me.toolToolStripMenuItemClick
		'
		'textToolStripMenuItem
		'
		Me.textToolStripMenuItem.CheckOnClick = true
		Me.textToolStripMenuItem.Image = CType(resources.GetObject("textToolStripMenuItem.Image"),System.Drawing.Image)
		Me.textToolStripMenuItem.Name = "textToolStripMenuItem"
		Me.textToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.textToolStripMenuItem.Text = "Text"
		AddHandler Me.textToolStripMenuItem.CheckedChanged, AddressOf Me.TextToolStripMenuItemCheckedChanged
		AddHandler Me.textToolStripMenuItem.Click, AddressOf Me.toolToolStripMenuItemClick
		'
		'colorToolStripMenuItem
		'
		Me.colorToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.redToolStripMenuItem, Me.greenToolStripMenuItem, Me.blueToolStripMenuItem, Me.customToolStripMenuItem})
		Me.colorToolStripMenuItem.Image = CType(resources.GetObject("colorToolStripMenuItem.Image"),System.Drawing.Image)
		Me.colorToolStripMenuItem.Name = "colorToolStripMenuItem"
		Me.colorToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.colorToolStripMenuItem.Text = "Color"
		'
		'redToolStripMenuItem
		'
		Me.redToolStripMenuItem.BackColor = System.Drawing.Color.Red
		Me.redToolStripMenuItem.Name = "redToolStripMenuItem"
		Me.redToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.redToolStripMenuItem.Text = "Red"
		AddHandler Me.redToolStripMenuItem.Click, AddressOf Me.SetColorToolStripMenuItemClick
		'
		'greenToolStripMenuItem
		'
		Me.greenToolStripMenuItem.BackColor = System.Drawing.Color.LawnGreen
		Me.greenToolStripMenuItem.Name = "greenToolStripMenuItem"
		Me.greenToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.greenToolStripMenuItem.Text = "Green"
		AddHandler Me.greenToolStripMenuItem.Click, AddressOf Me.SetColorToolStripMenuItemClick
		'
		'blueToolStripMenuItem
		'
		Me.blueToolStripMenuItem.BackColor = System.Drawing.Color.DodgerBlue
		Me.blueToolStripMenuItem.Name = "blueToolStripMenuItem"
		Me.blueToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.blueToolStripMenuItem.Text = "Blue"
		AddHandler Me.blueToolStripMenuItem.Click, AddressOf Me.SetColorToolStripMenuItemClick
		'
		'customToolStripMenuItem
		'
		Me.customToolStripMenuItem.Name = "customToolStripMenuItem"
		Me.customToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.customToolStripMenuItem.Text = "Custom"
		AddHandler Me.customToolStripMenuItem.Click, AddressOf Me.ColorToolStripMenuItemClick
		'
		'highlightColorToolStripMenuItem
		'
		Me.highlightColorToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.highlightColorYellowToolStripMenuItem, Me.highlightColorGreenToolStripMenuItem, Me.highlightColorBlueToolStripMenuItem, Me.HighlightColorMagentaToolStripMenuItem})
		Me.highlightColorToolStripMenuItem.Name = "highlightColorToolStripMenuItem"
		Me.highlightColorToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.highlightColorToolStripMenuItem.Text = "Highlight Color"
		'
		'highlightColorYellowToolStripMenuItem
		'
		Me.highlightColorYellowToolStripMenuItem.BackColor = System.Drawing.Color.Yellow
		Me.highlightColorYellowToolStripMenuItem.Checked = true
		Me.highlightColorYellowToolStripMenuItem.CheckOnClick = true
		Me.highlightColorYellowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.highlightColorYellowToolStripMenuItem.Name = "highlightColorYellowToolStripMenuItem"
		Me.highlightColorYellowToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
		Me.highlightColorYellowToolStripMenuItem.Text = "Yellow"
		AddHandler Me.highlightColorYellowToolStripMenuItem.Click, AddressOf Me.HighlightColorToolStripMenuItemClick
		'
		'highlightColorGreenToolStripMenuItem
		'
		Me.highlightColorGreenToolStripMenuItem.BackColor = System.Drawing.Color.Lime
		Me.highlightColorGreenToolStripMenuItem.CheckOnClick = true
		Me.highlightColorGreenToolStripMenuItem.Name = "highlightColorGreenToolStripMenuItem"
		Me.highlightColorGreenToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
		Me.highlightColorGreenToolStripMenuItem.Text = "Green"
		AddHandler Me.highlightColorGreenToolStripMenuItem.Click, AddressOf Me.HighlightColorToolStripMenuItemClick
		'
		'highlightColorBlueToolStripMenuItem
		'
		Me.highlightColorBlueToolStripMenuItem.BackColor = System.Drawing.Color.Cyan
		Me.highlightColorBlueToolStripMenuItem.CheckOnClick = true
		Me.highlightColorBlueToolStripMenuItem.Name = "highlightColorBlueToolStripMenuItem"
		Me.highlightColorBlueToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
		Me.highlightColorBlueToolStripMenuItem.Text = "Blue"
		AddHandler Me.highlightColorBlueToolStripMenuItem.Click, AddressOf Me.HighlightColorToolStripMenuItemClick
		'
		'HighlightColorMagentaToolStripMenuItem
		'
		Me.HighlightColorMagentaToolStripMenuItem.BackColor = System.Drawing.Color.Fuchsia
		Me.HighlightColorMagentaToolStripMenuItem.CheckOnClick = true
		Me.HighlightColorMagentaToolStripMenuItem.Name = "HighlightColorMagentaToolStripMenuItem"
		Me.HighlightColorMagentaToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
		Me.HighlightColorMagentaToolStripMenuItem.Text = "Magenta"
		AddHandler Me.HighlightColorMagentaToolStripMenuItem.Click, AddressOf Me.HighlightColorToolStripMenuItemClick
		'
		'lineWidthToolStripMenuItem
		'
		Me.lineWidthToolStripMenuItem.DropDown = Me.contextMenuStripLine
		Me.lineWidthToolStripMenuItem.Image = CType(resources.GetObject("lineWidthToolStripMenuItem.Image"),System.Drawing.Image)
		Me.lineWidthToolStripMenuItem.Name = "lineWidthToolStripMenuItem"
		Me.lineWidthToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.lineWidthToolStripMenuItem.Text = "Line width"
		'
		'contextMenuStripLine
		'
		Me.contextMenuStripLine.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItem11, Me.toolStripMenuItem12, Me.toolStripMenuItem13, Me.toolStripMenuItem14, Me.toolStripMenuItem15})
		Me.contextMenuStripLine.Name = "contextMenuStripLine"
		Me.contextMenuStripLine.Size = New System.Drawing.Size(87, 114)
		'
		'toolStripMenuItem11
		'
		Me.toolStripMenuItem11.Name = "toolStripMenuItem11"
		Me.toolStripMenuItem11.Size = New System.Drawing.Size(86, 22)
		Me.toolStripMenuItem11.Text = "1"
		AddHandler Me.toolStripMenuItem11.Click, AddressOf Me.setLineWidth
		'
		'toolStripMenuItem12
		'
		Me.toolStripMenuItem12.Name = "toolStripMenuItem12"
		Me.toolStripMenuItem12.Size = New System.Drawing.Size(86, 22)
		Me.toolStripMenuItem12.Text = "2"
		AddHandler Me.toolStripMenuItem12.Click, AddressOf Me.setLineWidth
		'
		'toolStripMenuItem13
		'
		Me.toolStripMenuItem13.Checked = true
		Me.toolStripMenuItem13.CheckState = System.Windows.Forms.CheckState.Checked
		Me.toolStripMenuItem13.Name = "toolStripMenuItem13"
		Me.toolStripMenuItem13.Size = New System.Drawing.Size(86, 22)
		Me.toolStripMenuItem13.Text = "4"
		AddHandler Me.toolStripMenuItem13.Click, AddressOf Me.setLineWidth
		'
		'toolStripMenuItem14
		'
		Me.toolStripMenuItem14.Name = "toolStripMenuItem14"
		Me.toolStripMenuItem14.Size = New System.Drawing.Size(86, 22)
		Me.toolStripMenuItem14.Text = "8"
		AddHandler Me.toolStripMenuItem14.Click, AddressOf Me.setLineWidth
		'
		'toolStripMenuItem15
		'
		Me.toolStripMenuItem15.Name = "toolStripMenuItem15"
		Me.toolStripMenuItem15.Size = New System.Drawing.Size(86, 22)
		Me.toolStripMenuItem15.Text = "16"
		AddHandler Me.toolStripMenuItem15.Click, AddressOf Me.setLineWidth
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(174, 6)
		'
		'insertToolStripMenuItem
		'
		Me.insertToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cursorToolStripMenuItem, Me.handToolStripMenuItem, Me.toolStripMenuItem2, Me.toolStripMenuItem3, Me.toolStripMenuItem4, Me.toolStripMenuItem5, Me.toolStripMenuItem6, Me.toolStripMenuItem7, Me.toolStripMenuItem8, Me.toolStripMenuItem9, Me.toolStripMenuItem10, Me.toolStripSeparator6, Me.insertClipboardContentsToolStripMenuItem})
		Me.insertToolStripMenuItem.Name = "insertToolStripMenuItem"
		Me.insertToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.insertToolStripMenuItem.Text = "Insert"
		'
		'cursorToolStripMenuItem
		'
		Me.cursorToolStripMenuItem.Image = CType(resources.GetObject("cursorToolStripMenuItem.Image"),System.Drawing.Image)
		Me.cursorToolStripMenuItem.Name = "cursorToolStripMenuItem"
		Me.cursorToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
		Me.cursorToolStripMenuItem.Text = "Cursor"
		AddHandler Me.cursorToolStripMenuItem.Click, AddressOf Me.CursorToolStripMenuItemClick
		'
		'handToolStripMenuItem
		'
		Me.handToolStripMenuItem.Name = "handToolStripMenuItem"
		Me.handToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
		Me.handToolStripMenuItem.Text = "Hand"
		AddHandler Me.handToolStripMenuItem.Click, AddressOf Me.CursorToolStripMenuItemClick
		'
		'toolStripMenuItem2
		'
		Me.toolStripMenuItem2.Name = "toolStripMenuItem2"
		Me.toolStripMenuItem2.Size = New System.Drawing.Size(126, 22)
		Me.toolStripMenuItem2.Text = "1"
		AddHandler Me.toolStripMenuItem2.Click, AddressOf Me.ToolStripMenuItemNumber
		'
		'toolStripMenuItem3
		'
		Me.toolStripMenuItem3.Name = "toolStripMenuItem3"
		Me.toolStripMenuItem3.Size = New System.Drawing.Size(126, 22)
		Me.toolStripMenuItem3.Text = "2"
		AddHandler Me.toolStripMenuItem3.Click, AddressOf Me.ToolStripMenuItemNumber
		'
		'toolStripMenuItem4
		'
		Me.toolStripMenuItem4.Name = "toolStripMenuItem4"
		Me.toolStripMenuItem4.Size = New System.Drawing.Size(126, 22)
		Me.toolStripMenuItem4.Text = "3"
		AddHandler Me.toolStripMenuItem4.Click, AddressOf Me.ToolStripMenuItemNumber
		'
		'toolStripMenuItem5
		'
		Me.toolStripMenuItem5.Name = "toolStripMenuItem5"
		Me.toolStripMenuItem5.Size = New System.Drawing.Size(126, 22)
		Me.toolStripMenuItem5.Text = "4"
		AddHandler Me.toolStripMenuItem5.Click, AddressOf Me.ToolStripMenuItemNumber
		'
		'toolStripMenuItem6
		'
		Me.toolStripMenuItem6.Name = "toolStripMenuItem6"
		Me.toolStripMenuItem6.Size = New System.Drawing.Size(126, 22)
		Me.toolStripMenuItem6.Text = "5"
		AddHandler Me.toolStripMenuItem6.Click, AddressOf Me.ToolStripMenuItemNumber
		'
		'toolStripMenuItem7
		'
		Me.toolStripMenuItem7.Name = "toolStripMenuItem7"
		Me.toolStripMenuItem7.Size = New System.Drawing.Size(126, 22)
		Me.toolStripMenuItem7.Text = "6"
		AddHandler Me.toolStripMenuItem7.Click, AddressOf Me.ToolStripMenuItemNumber
		'
		'toolStripMenuItem8
		'
		Me.toolStripMenuItem8.Name = "toolStripMenuItem8"
		Me.toolStripMenuItem8.Size = New System.Drawing.Size(126, 22)
		Me.toolStripMenuItem8.Text = "7"
		AddHandler Me.toolStripMenuItem8.Click, AddressOf Me.ToolStripMenuItemNumber
		'
		'toolStripMenuItem9
		'
		Me.toolStripMenuItem9.Name = "toolStripMenuItem9"
		Me.toolStripMenuItem9.Size = New System.Drawing.Size(126, 22)
		Me.toolStripMenuItem9.Text = "8"
		AddHandler Me.toolStripMenuItem9.Click, AddressOf Me.ToolStripMenuItemNumber
		'
		'toolStripMenuItem10
		'
		Me.toolStripMenuItem10.Name = "toolStripMenuItem10"
		Me.toolStripMenuItem10.Size = New System.Drawing.Size(126, 22)
		Me.toolStripMenuItem10.Text = "9"
		AddHandler Me.toolStripMenuItem10.Click, AddressOf Me.ToolStripMenuItemNumber
		'
		'toolStripSeparator6
		'
		Me.toolStripSeparator6.Name = "toolStripSeparator6"
		Me.toolStripSeparator6.Size = New System.Drawing.Size(123, 6)
		Me.toolStripSeparator6.Visible = false
		'
		'insertClipboardContentsToolStripMenuItem
		'
		Me.insertClipboardContentsToolStripMenuItem.Name = "insertClipboardContentsToolStripMenuItem"
		Me.insertClipboardContentsToolStripMenuItem.Size = New System.Drawing.Size(126, 22)
		Me.insertClipboardContentsToolStripMenuItem.Text = "Clipboard"
		Me.insertClipboardContentsToolStripMenuItem.Visible = false
		AddHandler Me.insertClipboardContentsToolStripMenuItem.Click, AddressOf Me.InsertClipboardContentsToolStripMenuItemClick
		'
		'toolStripSeparator5
		'
		Me.toolStripSeparator5.Name = "toolStripSeparator5"
		Me.toolStripSeparator5.Size = New System.Drawing.Size(174, 6)
		'
		'undoToolStripMenuItem
		'
		Me.undoToolStripMenuItem.Enabled = false
		Me.undoToolStripMenuItem.Image = CType(resources.GetObject("undoToolStripMenuItem.Image"),System.Drawing.Image)
		Me.undoToolStripMenuItem.Name = "undoToolStripMenuItem"
		Me.undoToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.undoToolStripMenuItem.Text = "Undo"
		AddHandler Me.undoToolStripMenuItem.Click, AddressOf Me.UndoToolStripMenuItemClick
		'
		'resetToolStripMenuItem
		'
		Me.resetToolStripMenuItem.Enabled = false
		Me.resetToolStripMenuItem.Name = "resetToolStripMenuItem"
		Me.resetToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.resetToolStripMenuItem.Text = "Reset"
		AddHandler Me.resetToolStripMenuItem.Click, AddressOf Me.BackToolStripMenuItemClick
		'
		'imageToolStripMenuItem
		'
		Me.imageToolStripMenuItem.DropDown = Me.contextMenuStripSelection
		Me.imageToolStripMenuItem.Name = "imageToolStripMenuItem"
		Me.imageToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.imageToolStripMenuItem.Text = "Image"
		Me.imageToolStripMenuItem.Visible = false
		AddHandler Me.imageToolStripMenuItem.DropDownItemClicked, AddressOf Me.ImageToolStripMenuItemDropDownItemClicked
		'
		'contextMenuStripSelection
		'
		Me.contextMenuStripSelection.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.flipHorizontalToolStripMenuItem, Me.flipVerticalToolStripMenuItem, Me.fillToolStripMenuItem, Me.toolStripMenuItem1})
		Me.contextMenuStripSelection.Name = "contextMenuStripSelection"
		Me.contextMenuStripSelection.Size = New System.Drawing.Size(152, 92)
		'
		'flipHorizontalToolStripMenuItem
		'
		Me.flipHorizontalToolStripMenuItem.Image = CType(resources.GetObject("flipHorizontalToolStripMenuItem.Image"),System.Drawing.Image)
		Me.flipHorizontalToolStripMenuItem.Name = "flipHorizontalToolStripMenuItem"
		Me.flipHorizontalToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
		Me.flipHorizontalToolStripMenuItem.Text = "Flip Horizontal"
		AddHandler Me.flipHorizontalToolStripMenuItem.Click, AddressOf Me.FlipToolStripMenuItemClick
		'
		'flipVerticalToolStripMenuItem
		'
		Me.flipVerticalToolStripMenuItem.Image = CType(resources.GetObject("flipVerticalToolStripMenuItem.Image"),System.Drawing.Image)
		Me.flipVerticalToolStripMenuItem.Name = "flipVerticalToolStripMenuItem"
		Me.flipVerticalToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
		Me.flipVerticalToolStripMenuItem.Text = "Flip Vertical"
		AddHandler Me.flipVerticalToolStripMenuItem.Click, AddressOf Me.FlipToolStripMenuItemClick
		'
		'fillToolStripMenuItem
		'
		Me.fillToolStripMenuItem.Name = "fillToolStripMenuItem"
		Me.fillToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
		Me.fillToolStripMenuItem.Text = "Fill"
		AddHandler Me.fillToolStripMenuItem.Click, AddressOf Me.FillToolStripMenuItemClick
		'
		'toolStripMenuItem1
		'
		Me.toolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.bitToolStripMenuItem, Me.grayscaleToolStripMenuItem, Me.invertToolStripMenuItem})
		Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
		Me.toolStripMenuItem1.Size = New System.Drawing.Size(151, 22)
		Me.toolStripMenuItem1.Text = "Transform"
		Me.toolStripMenuItem1.Visible = false
		'
		'bitToolStripMenuItem
		'
		Me.bitToolStripMenuItem.Name = "bitToolStripMenuItem"
		Me.bitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.bitToolStripMenuItem.Text = "1 bit"
		AddHandler Me.bitToolStripMenuItem.Click, AddressOf Me.transformToolStripMenuItemClick
		'
		'grayscaleToolStripMenuItem
		'
		Me.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem"
		Me.grayscaleToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.grayscaleToolStripMenuItem.Text = "Grayscale"
		AddHandler Me.grayscaleToolStripMenuItem.Click, AddressOf Me.transformToolStripMenuItemClick
		'
		'invertToolStripMenuItem
		'
		Me.invertToolStripMenuItem.Name = "invertToolStripMenuItem"
		Me.invertToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.invertToolStripMenuItem.Text = "Invert"
		AddHandler Me.invertToolStripMenuItem.Click, AddressOf Me.transformToolStripMenuItemClick
		'
		'selectionToolStripMenuItem
		'
		Me.selectionToolStripMenuItem.DropDown = Me.contextMenuStripSelection
		Me.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem"
		Me.selectionToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.selectionToolStripMenuItem.Text = "Selection"
		Me.selectionToolStripMenuItem.Visible = false
		AddHandler Me.selectionToolStripMenuItem.DropDownItemClicked, AddressOf Me.SelectionToolStripMenuItemDropDownItemClicked
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(174, 6)
		'
		'selectAreaToolStripMenuItem
		'
		Me.selectAreaToolStripMenuItem.CheckOnClick = true
		Me.selectAreaToolStripMenuItem.Image = CType(resources.GetObject("selectAreaToolStripMenuItem.Image"),System.Drawing.Image)
		Me.selectAreaToolStripMenuItem.Name = "selectAreaToolStripMenuItem"
		Me.selectAreaToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.selectAreaToolStripMenuItem.Text = "Select area"
		AddHandler Me.selectAreaToolStripMenuItem.CheckedChanged, AddressOf Me.SelectAreaToolStripMenuItemCheckedChanged
		AddHandler Me.selectAreaToolStripMenuItem.Click, AddressOf Me.toolToolStripMenuItemClick
		'
		'copyToolStripMenuItem
		'
		Me.copyToolStripMenuItem.Image = CType(resources.GetObject("copyToolStripMenuItem.Image"),System.Drawing.Image)
		Me.copyToolStripMenuItem.Name = "copyToolStripMenuItem"
		Me.copyToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.copyToolStripMenuItem.Text = "Copy"
		AddHandler Me.copyToolStripMenuItem.Click, AddressOf Me.CopyToolStripMenuItemClick
		'
		'pasteToolStripMenuItem
		'
		Me.pasteToolStripMenuItem.Image = CType(resources.GetObject("pasteToolStripMenuItem.Image"),System.Drawing.Image)
		Me.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem"
		Me.pasteToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.pasteToolStripMenuItem.Text = "Paste"
		AddHandler Me.pasteToolStripMenuItem.Click, AddressOf Me.PasteToolStripMenuItemClick
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(174, 6)
		'
		'OpenToolStripMenuItem
		'
		Me.OpenToolStripMenuItem.Image = CType(resources.GetObject("OpenToolStripMenuItem.Image"),System.Drawing.Image)
		Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
		Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.OpenToolStripMenuItem.Text = "Open..."
		Me.OpenToolStripMenuItem.Visible = false
		AddHandler Me.OpenToolStripMenuItem.Click, AddressOf Me.openToolStripMenuItemClick
		'
		'saveToolStripMenuItem
		'
		Me.saveToolStripMenuItem.Enabled = false
		Me.saveToolStripMenuItem.Image = CType(resources.GetObject("saveToolStripMenuItem.Image"),System.Drawing.Image)
		Me.saveToolStripMenuItem.Name = "saveToolStripMenuItem"
		Me.saveToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.saveToolStripMenuItem.Text = "Save"
		AddHandler Me.saveToolStripMenuItem.Click, AddressOf Me.SaveToolStripMenuItemClick
		'
		'saveAsToolStripMenuItem
		'
		Me.saveAsToolStripMenuItem.Image = CType(resources.GetObject("saveAsToolStripMenuItem.Image"),System.Drawing.Image)
		Me.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
		Me.saveAsToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.saveAsToolStripMenuItem.Text = "Save As..."
		AddHandler Me.saveAsToolStripMenuItem.Click, AddressOf Me.SaveAsToolStripMenuItemClick
		'
		'clearToolStripMenuItem
		'
		Me.clearToolStripMenuItem.Name = "clearToolStripMenuItem"
		Me.clearToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.clearToolStripMenuItem.Text = "Clear"
		Me.clearToolStripMenuItem.Visible = false
		AddHandler Me.clearToolStripMenuItem.Click, AddressOf Me.ClearToolStripMenuItemClick
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		Me.toolStripSeparator4.Size = New System.Drawing.Size(174, 6)
		Me.toolStripSeparator4.Visible = false
		'
		'quitToolStripMenuItem
		'
		Me.quitToolStripMenuItem.Name = "quitToolStripMenuItem"
		Me.quitToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.quitToolStripMenuItem.Text = "Quit"
		Me.quitToolStripMenuItem.Visible = false
		AddHandler Me.quitToolStripMenuItem.Click, AddressOf Me.QuitToolStripMenuItemClick
		'
		'colorDialog1
		'
		Me.colorDialog1.AnyColor = true
		Me.colorDialog1.Color = System.Drawing.Color.Red
		Me.colorDialog1.SolidColorOnly = true
		'
		'textBox1
		'
		Me.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.textBox1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.textBox1.Location = New System.Drawing.Point(0, 0)
		Me.textBox1.Margin = New System.Windows.Forms.Padding(0)
		Me.textBox1.Multiline = true
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(1, 1)
		Me.textBox1.TabIndex = 2
		AddHandler Me.textBox1.TextChanged, AddressOf Me.TextBox1TextChanged
		AddHandler Me.textBox1.KeyPress, AddressOf Me.TextBox1KeyPress
		AddHandler Me.textBox1.KeyUp, AddressOf Me.textBox1_KeyUp
		'
		'panelTextbox
		'
		Me.panelTextbox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.panelTextbox.BackColor = System.Drawing.SystemColors.Control
		Me.panelTextbox.Controls.Add(Me.textBox1)
		Me.panelTextbox.Location = New System.Drawing.Point(133, 146)
		Me.panelTextbox.Name = "panelTextbox"
		Me.panelTextbox.Size = New System.Drawing.Size(1, 1)
		Me.panelTextbox.TabIndex = 3
		Me.panelTextbox.Visible = false
		'
		'toolStripSeparator7
		'
		Me.toolStripSeparator7.Name = "toolStripSeparator7"
		Me.toolStripSeparator7.Size = New System.Drawing.Size(174, 6)
		'
		'AnnotateBox
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.picCanvas)
		Me.Controls.Add(Me.panelTextbox)
		Me.DoubleBuffered = true
		Me.Name = "AnnotateBox"
		Me.Size = New System.Drawing.Size(150, 163)
		AddHandler Load, AddressOf Me.AnnotateBoxLoad
		AddHandler DragDrop, AddressOf Me.annotateBoxDragDrop
		AddHandler DragOver, AddressOf Me.annotateBoxDragOver
		CType(Me.picCanvas,System.ComponentModel.ISupportInitialize).EndInit
		Me.contextMenuStripCanvas.ResumeLayout(false)
		Me.contextMenuStripLine.ResumeLayout(false)
		Me.contextMenuStripSelection.ResumeLayout(false)
		Me.panelTextbox.ResumeLayout(false)
		Me.panelTextbox.PerformLayout
		Me.ResumeLayout(false)
	End Sub
	Private toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
	Private HighlightColorMagentaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private bitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private invertToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private grayscaleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private insertClipboardContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
	Private highlightColorGreenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private highlightColorBlueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private highlightColorYellowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private highlightColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private highlightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem15 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem14 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripLine As System.Windows.Forms.ContextMenuStrip
	Private lineWidthToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private handToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private textToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private panelTextbox As System.Windows.Forms.Panel
	Private textBox1 As System.Windows.Forms.TextBox
	Private imageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fillToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private flipVerticalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private flipHorizontalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripSelection As System.Windows.Forms.ContextMenuStrip
	Private selectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private customToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private greenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private blueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private redToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
	Private cursorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private insertToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private colorDialog1 As System.Windows.Forms.ColorDialog
	Private colorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private roundedRectangleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private quitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private arrowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private lineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private saveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private saveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private clearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private selectAreaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private pasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private copyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private undoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private resetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private ellipseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private pixelateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private freehandToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private rectangleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripCanvas As System.Windows.Forms.ContextMenuStrip
	Private picCanvas As System.Windows.Forms.PictureBox
End Class
