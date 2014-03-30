'
' Created by SharpDevelop.
' User: bergb
' Date: 2009/04/27
' Time: 15:27
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
		Me.annotateBox1 = New AnnotateTest.AnnotateBox
		Me.menuStrip1 = New System.Windows.Forms.MenuStrip
		Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.clearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.statusStrip1 = New System.Windows.Forms.StatusStrip
		Me.menuStrip1.SuspendLayout
		Me.SuspendLayout
		'
		'annotateBox1
		'
		Me.annotateBox1.AllowPaste = true
		Me.annotateBox1.BackColor = System.Drawing.Color.White
		Me.annotateBox1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.annotateBox1.Location = New System.Drawing.Point(0, 24)
		Me.annotateBox1.Name = "annotateBox1"
		Me.annotateBox1.ShowColor = true
		Me.annotateBox1.showIcon = false
		Me.annotateBox1.ShowOpen = false
		Me.annotateBox1.showQuit = false
		Me.annotateBox1.ShowRoundedRectangle = false
		Me.annotateBox1.showSave = true
		Me.annotateBox1.Size = New System.Drawing.Size(292, 242)
		Me.annotateBox1.TabIndex = 0
		AddHandler Me.annotateBox1.ImageSaved, AddressOf Me.AnnotateBox1ImageSaved
		'
		'menuStrip1
		'
		Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem})
		Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.menuStrip1.Name = "menuStrip1"
		Me.menuStrip1.Size = New System.Drawing.Size(292, 24)
		Me.menuStrip1.TabIndex = 1
		Me.menuStrip1.Text = "menuStrip1"
		'
		'fileToolStripMenuItem
		'
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.openToolStripMenuItem, Me.saveToolStripMenuItem, Me.saveAsToolStripMenuItem, Me.clearToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.fileToolStripMenuItem.Text = "File"
		AddHandler Me.fileToolStripMenuItem.DropDownOpening, AddressOf Me.FileToolStripMenuItemDropDownOpening
		'
		'openToolStripMenuItem
		'
		Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
		Me.openToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
		Me.openToolStripMenuItem.Text = "Open"
		AddHandler Me.openToolStripMenuItem.Click, AddressOf Me.OpenToolStripMenuItemClick
		'
		'saveToolStripMenuItem
		'
		Me.saveToolStripMenuItem.Name = "saveToolStripMenuItem"
		Me.saveToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
		Me.saveToolStripMenuItem.Text = "Save"
		AddHandler Me.saveToolStripMenuItem.Click, AddressOf Me.SaveToolStripMenuItemClick
		'
		'saveAsToolStripMenuItem
		'
		Me.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
		Me.saveAsToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
		Me.saveAsToolStripMenuItem.Text = "Save As..."
		AddHandler Me.saveAsToolStripMenuItem.Click, AddressOf Me.SaveAsToolStripMenuItemClick
		'
		'clearToolStripMenuItem
		'
		Me.clearToolStripMenuItem.Name = "clearToolStripMenuItem"
		Me.clearToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
		Me.clearToolStripMenuItem.Text = "Clear"
		AddHandler Me.clearToolStripMenuItem.Click, AddressOf Me.ClearToolStripMenuItemClick
		'
		'statusStrip1
		'
		Me.statusStrip1.Location = New System.Drawing.Point(0, 266)
		Me.statusStrip1.Name = "statusStrip1"
		Me.statusStrip1.Size = New System.Drawing.Size(292, 22)
		Me.statusStrip1.TabIndex = 2
		Me.statusStrip1.Text = "statusStrip1"
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(292, 288)
		Me.Controls.Add(Me.annotateBox1)
		Me.Controls.Add(Me.statusStrip1)
		Me.Controls.Add(Me.menuStrip1)
		Me.MainMenuStrip = Me.menuStrip1
		Me.Name = "MainForm"
		Me.ShowIcon = false
		Me.Text = "Annotate"
		Me.menuStrip1.ResumeLayout(false)
		Me.menuStrip1.PerformLayout
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private clearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private saveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private saveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private statusStrip1 As System.Windows.Forms.StatusStrip
	Private openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private menuStrip1 As System.Windows.Forms.MenuStrip
	Private annotateBox1 As AnnotateTest.AnnotateBox
End Class
