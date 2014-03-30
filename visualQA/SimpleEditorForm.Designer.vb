'
' Created by SharpDevelop.
' User: bergb
' Date: 2008/01/25
' Time: 14:46
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class SimpleEditorForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SimpleEditorForm))
		Me.richTextBox1 = New System.Windows.Forms.RichTextBox
		Me.statusStrip1 = New System.Windows.Forms.StatusStrip
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip
		Me.toolStripButton1 = New System.Windows.Forms.ToolStripButton
		Me.toolStripButton2 = New System.Windows.Forms.ToolStripButton
		Me.toolStripButton3 = New System.Windows.Forms.ToolStripButton
		Me.toolStrip1.SuspendLayout
		Me.SuspendLayout
		'
		'richTextBox1
		'
		Me.richTextBox1.BackColor = System.Drawing.Color.LemonChiffon
		Me.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.richTextBox1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.richTextBox1.Location = New System.Drawing.Point(0, 25)
		Me.richTextBox1.Name = "richTextBox1"
		Me.richTextBox1.Size = New System.Drawing.Size(481, 219)
		Me.richTextBox1.TabIndex = 0
		Me.richTextBox1.Text = ""
		'
		'statusStrip1
		'
		Me.statusStrip1.Location = New System.Drawing.Point(0, 244)
		Me.statusStrip1.Name = "statusStrip1"
		Me.statusStrip1.Size = New System.Drawing.Size(481, 22)
		Me.statusStrip1.TabIndex = 1
		Me.statusStrip1.Text = "statusStrip1"
		'
		'toolStrip1
		'
		Me.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripButton1, Me.toolStripButton2, Me.toolStripButton3})
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(481, 25)
		Me.toolStrip1.TabIndex = 2
		Me.toolStrip1.Text = "toolStrip1"
		'
		'toolStripButton1
		'
		Me.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.toolStripButton1.Image = CType(resources.GetObject("toolStripButton1.Image"),System.Drawing.Image)
		Me.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.toolStripButton1.Name = "toolStripButton1"
		Me.toolStripButton1.Size = New System.Drawing.Size(23, 22)
		Me.toolStripButton1.Text = "Cancel"
		AddHandler Me.toolStripButton1.Click, AddressOf Me.ToolStripButton1Click
		'
		'toolStripButton2
		'
		Me.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.toolStripButton2.Image = CType(resources.GetObject("toolStripButton2.Image"),System.Drawing.Image)
		Me.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.toolStripButton2.Name = "toolStripButton2"
		Me.toolStripButton2.Size = New System.Drawing.Size(23, 22)
		Me.toolStripButton2.Text = "Save and Close"
		AddHandler Me.toolStripButton2.Click, AddressOf Me.ToolStripButton2Click
		'
		'toolStripButton3
		'
		Me.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.toolStripButton3.Image = CType(resources.GetObject("toolStripButton3.Image"),System.Drawing.Image)
		Me.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.toolStripButton3.Name = "toolStripButton3"
		Me.toolStripButton3.Size = New System.Drawing.Size(23, 22)
		Me.toolStripButton3.Text = "toolStripButton3"
		'
		'SimpleEditorForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 12!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(481, 266)
		Me.ControlBox = false
		Me.Controls.Add(Me.richTextBox1)
		Me.Controls.Add(Me.toolStrip1)
		Me.Controls.Add(Me.statusStrip1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
		Me.Name = "SimpleEditorForm"
		Me.ShowInTaskbar = false
		Me.Text = "Edit Comment"
		Me.TopMost = true
		AddHandler Load, AddressOf Me.SimpleEditorFormLoad
		Me.toolStrip1.ResumeLayout(false)
		Me.toolStrip1.PerformLayout
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private toolStripButton3 As System.Windows.Forms.ToolStripButton
	Private toolStripButton2 As System.Windows.Forms.ToolStripButton
	Private toolStripButton1 As System.Windows.Forms.ToolStripButton
	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private statusStrip1 As System.Windows.Forms.StatusStrip
	Private richTextBox1 As System.Windows.Forms.RichTextBox
End Class
