'
' Created by SharpDevelop.
' User: bergb
' Date: 2009/07/31
' Time: 11:08
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class MainForm
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.panel1 = New System.Windows.Forms.Panel()
		Me.panel3 = New System.Windows.Forms.Panel()
		Me.label1 = New System.Windows.Forms.Label()
		Me.annotateBox1 = New AnnotateScreenArea.AnnotateBox()
		Me.button1 = New System.Windows.Forms.Button()
		Me.panel2 = New System.Windows.Forms.Panel()
		Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.selectDestinationFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.transparantToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.resetAreaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.resetCounterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.autoSaveOnUpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.autoUpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
		Me.autoNumberToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.textBox1 = New System.Windows.Forms.TextBox()
		Me.button2 = New System.Windows.Forms.Button()
		Me.timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.contextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.panel1.SuspendLayout
		Me.panel2.SuspendLayout
		Me.contextMenuStrip1.SuspendLayout
		Me.SuspendLayout
		'
		'panel1
		'
		Me.panel1.BackColor = System.Drawing.Color.White
		Me.panel1.Controls.Add(Me.panel3)
		Me.panel1.Controls.Add(Me.label1)
		Me.panel1.Controls.Add(Me.annotateBox1)
		Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel1.Location = New System.Drawing.Point(0, 0)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(299, 346)
		Me.panel1.TabIndex = 0
		'
		'panel3
		'
		Me.panel3.Location = New System.Drawing.Point(129, 0)
		Me.panel3.Name = "panel3"
		Me.panel3.Size = New System.Drawing.Size(31, 346)
		Me.panel3.TabIndex = 2
		Me.panel3.Visible = false
		'
		'label1
		'
		Me.label1.BackColor = System.Drawing.Color.White
		Me.label1.Font = New System.Drawing.Font("Verdana", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.label1.ForeColor = System.Drawing.Color.Red
		Me.label1.Location = New System.Drawing.Point(3, 3)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(145, 25)
		Me.label1.TabIndex = 1
		'
		'annotateBox1
		'
		Me.annotateBox1.AllowDrop = true
		Me.annotateBox1.AllowPaste = true
		Me.annotateBox1.BackColor = System.Drawing.Color.White
		Me.annotateBox1.Cursor = System.Windows.Forms.Cursors.Cross
		Me.annotateBox1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.annotateBox1.Location = New System.Drawing.Point(0, 0)
		Me.annotateBox1.Name = "annotateBox1"
		Me.annotateBox1.openDroppedFile = true
		Me.annotateBox1.ShowColor = true
		Me.annotateBox1.showIcon = false
		Me.annotateBox1.ShowOpen = false
		Me.annotateBox1.showQuit = false
		Me.annotateBox1.ShowRoundedRectangle = false
		Me.annotateBox1.showSave = false
		Me.annotateBox1.Size = New System.Drawing.Size(299, 346)
		Me.annotateBox1.TabIndex = 0
		Me.annotateBox1.Visible = false
		'
		'button1
		'
		Me.button1.BackColor = System.Drawing.SystemColors.Control
		Me.button1.Location = New System.Drawing.Point(3, 3)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(66, 25)
		Me.button1.TabIndex = 1
		Me.button1.Text = "Set"
		Me.button1.UseVisualStyleBackColor = false
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'panel2
		'
		Me.panel2.ContextMenuStrip = Me.contextMenuStrip1
		Me.panel2.Controls.Add(Me.textBox1)
		Me.panel2.Controls.Add(Me.button2)
		Me.panel2.Controls.Add(Me.button1)
		Me.panel2.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.panel2.Location = New System.Drawing.Point(0, 346)
		Me.panel2.Name = "panel2"
		Me.panel2.Size = New System.Drawing.Size(299, 31)
		Me.panel2.TabIndex = 2
		'
		'contextMenuStrip1
		'
		Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.selectDestinationFolderToolStripMenuItem, Me.transparantToolStripMenuItem, Me.resetAreaToolStripMenuItem, Me.resetCounterToolStripMenuItem, Me.toolStripSeparator1, Me.autoSaveOnUpdateToolStripMenuItem, Me.autoUpdateToolStripMenuItem, Me.toolStripSeparator2, Me.autoNumberToolStripMenuItem})
		Me.contextMenuStrip1.Name = "contextMenuStrip1"
		Me.contextMenuStrip1.Size = New System.Drawing.Size(225, 170)
		Me.contextMenuStrip1.Tag = ""
		'
		'selectDestinationFolderToolStripMenuItem
		'
		Me.selectDestinationFolderToolStripMenuItem.Name = "selectDestinationFolderToolStripMenuItem"
		Me.selectDestinationFolderToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
		Me.selectDestinationFolderToolStripMenuItem.Text = "Select Destination Folder..."
		AddHandler Me.selectDestinationFolderToolStripMenuItem.Click, AddressOf Me.SelectDestinationFolderToolStripMenuItemClick
		'
		'transparantToolStripMenuItem
		'
		Me.transparantToolStripMenuItem.Name = "transparantToolStripMenuItem"
		Me.transparantToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
		Me.transparantToolStripMenuItem.Text = "Toggle Transparant Window"
		AddHandler Me.transparantToolStripMenuItem.Click, AddressOf Me.TransparantToolStripMenuItemClick
		'
		'resetAreaToolStripMenuItem
		'
		Me.resetAreaToolStripMenuItem.Name = "resetAreaToolStripMenuItem"
		Me.resetAreaToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
		Me.resetAreaToolStripMenuItem.Text = "Reset Area"
		AddHandler Me.resetAreaToolStripMenuItem.Click, AddressOf Me.ResetAreaToolStripMenuItemClick
		'
		'resetCounterToolStripMenuItem
		'
		Me.resetCounterToolStripMenuItem.Name = "resetCounterToolStripMenuItem"
		Me.resetCounterToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
		Me.resetCounterToolStripMenuItem.Text = "Reset Counter"
		AddHandler Me.resetCounterToolStripMenuItem.Click, AddressOf Me.ResetCounterToolStripMenuItemClick
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(221, 6)
		'
		'autoSaveOnUpdateToolStripMenuItem
		'
		Me.autoSaveOnUpdateToolStripMenuItem.CheckOnClick = true
		Me.autoSaveOnUpdateToolStripMenuItem.Name = "autoSaveOnUpdateToolStripMenuItem"
		Me.autoSaveOnUpdateToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
		Me.autoSaveOnUpdateToolStripMenuItem.Text = "Auto Save on Update"
		AddHandler Me.autoSaveOnUpdateToolStripMenuItem.Click, AddressOf Me.AutoSaveOnUpdateToolStripMenuItemClick
		'
		'autoUpdateToolStripMenuItem
		'
		Me.autoUpdateToolStripMenuItem.CheckOnClick = true
		Me.autoUpdateToolStripMenuItem.Name = "autoUpdateToolStripMenuItem"
		Me.autoUpdateToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
		Me.autoUpdateToolStripMenuItem.Text = "Auto Update on Save"
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(221, 6)
		'
		'autoNumberToolStripMenuItem
		'
		Me.autoNumberToolStripMenuItem.CheckOnClick = true
		Me.autoNumberToolStripMenuItem.Name = "autoNumberToolStripMenuItem"
		Me.autoNumberToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
		Me.autoNumberToolStripMenuItem.Text = "Auto Number"
		'
		'textBox1
		'
		Me.textBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox1.ContextMenuStrip = Me.contextMenuStrip1
		Me.textBox1.Location = New System.Drawing.Point(77, 5)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(144, 21)
		Me.textBox1.TabIndex = 3
		Me.textBox1.Text = "screen_001"
		Me.textBox1.Visible = false
		'
		'button2
		'
		Me.button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button2.BackColor = System.Drawing.SystemColors.Control
		Me.button2.Location = New System.Drawing.Point(229, 3)
		Me.button2.Name = "button2"
		Me.button2.Size = New System.Drawing.Size(66, 25)
		Me.button2.TabIndex = 2
		Me.button2.Text = "Save"
		Me.button2.UseVisualStyleBackColor = false
		Me.button2.Visible = false
		AddHandler Me.button2.Click, AddressOf Me.Button2Click
		'
		'timer1
		'
		Me.timer1.Enabled = true
		AddHandler Me.timer1.Tick, AddressOf Me.Timer1Tick
		'
		'contextMenuStrip2
		'
		Me.contextMenuStrip2.Name = "contextMenuStrip2"
		Me.contextMenuStrip2.Size = New System.Drawing.Size(61, 4)
		'
		'MainForm
		'
		Me.AllowDrop = true
		Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(299, 377)
		Me.Controls.Add(Me.panel1)
		Me.Controls.Add(Me.panel2)
		Me.DoubleBuffered = true
		Me.Font = New System.Drawing.Font("Verdana", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.KeyPreview = true
		Me.Name = "MainForm"
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.Text = "Annotate Screen Area"
		Me.TopMost = true
		Me.TransparencyKey = System.Drawing.Color.Magenta
		AddHandler DragDrop, AddressOf Me.MainFormDragDrop
		AddHandler DragOver, AddressOf Me.MainFormDragOver
		AddHandler Resize, AddressOf Me.MainFormResize
		Me.panel1.ResumeLayout(false)
		Me.panel2.ResumeLayout(false)
		Me.panel2.PerformLayout
		Me.contextMenuStrip1.ResumeLayout(false)
		Me.ResumeLayout(false)
	End Sub
	Private contextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
	Private timer1 As System.Windows.Forms.Timer
	Private resetAreaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private resetCounterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private autoSaveOnUpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private panel3 As System.Windows.Forms.Panel
	Private transparantToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private autoUpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private autoNumberToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private selectDestinationFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
	Private label1 As System.Windows.Forms.Label
	Private button2 As System.Windows.Forms.Button
	Private textBox1 As System.Windows.Forms.TextBox
	Private panel2 As System.Windows.Forms.Panel
	Private button1 As System.Windows.Forms.Button
	Private annotateBox1 As AnnotateScreenArea.AnnotateBox
	Private panel1 As System.Windows.Forms.Panel
End Class
