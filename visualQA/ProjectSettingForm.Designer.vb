'
' Created by SharpDevelop.
' User: ${USER}
' Date: ${DATE}
' Time: ${TIME}
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class ProjectSettingForm
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
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProjectSettingForm))
		Me.buttonOk = New System.Windows.Forms.Button
		Me.buttonCancel = New System.Windows.Forms.Button
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.checkBoxSourceOnly = New System.Windows.Forms.CheckBox
		Me.checkBox1 = New System.Windows.Forms.CheckBox
		Me.label1 = New System.Windows.Forms.Label
		Me.buttonBrowseSource = New System.Windows.Forms.Button
		Me.textSourceFolder = New System.Windows.Forms.TextBox
		Me.groupBox2 = New System.Windows.Forms.GroupBox
		Me.checkBox2 = New System.Windows.Forms.CheckBox
		Me.label2 = New System.Windows.Forms.Label
		Me.buttonBrowseTarget = New System.Windows.Forms.Button
		Me.textTargetFolder = New System.Windows.Forms.TextBox
		Me.groupBox3 = New System.Windows.Forms.GroupBox
		Me.checkBox3 = New System.Windows.Forms.CheckBox
		Me.checkBoxXml = New System.Windows.Forms.CheckBox
		Me.checkBoxImages = New System.Windows.Forms.CheckBox
		Me.checkBoxHTML = New System.Windows.Forms.CheckBox
		Me.label3 = New System.Windows.Forms.Label
		Me.textFilter = New System.Windows.Forms.TextBox
		Me.panelButtons = New System.Windows.Forms.Panel
		Me.labelWarning = New System.Windows.Forms.Label
		Me.groupBox4 = New System.Windows.Forms.GroupBox
		Me.label5 = New System.Windows.Forms.Label
		Me.textBox2 = New System.Windows.Forms.TextBox
		Me.label4 = New System.Windows.Forms.Label
		Me.textBox1 = New System.Windows.Forms.TextBox
		Me.groupBox5 = New System.Windows.Forms.GroupBox
		Me.label6 = New System.Windows.Forms.Label
		Me.textBox3 = New System.Windows.Forms.TextBox
		Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.advancedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.panelAdvanced = New System.Windows.Forms.Panel
		Me.panelBasic = New System.Windows.Forms.Panel
		Me.groupBox1.SuspendLayout
		Me.groupBox2.SuspendLayout
		Me.groupBox3.SuspendLayout
		Me.panelButtons.SuspendLayout
		Me.groupBox4.SuspendLayout
		Me.groupBox5.SuspendLayout
		Me.contextMenuStrip1.SuspendLayout
		Me.panelAdvanced.SuspendLayout
		Me.panelBasic.SuspendLayout
		Me.SuspendLayout
		'
		'buttonOk
		'
		Me.buttonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.buttonOk.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonOk.Location = New System.Drawing.Point(407, 12)
		Me.buttonOk.Name = "buttonOk"
		Me.buttonOk.Size = New System.Drawing.Size(67, 23)
		Me.buttonOk.TabIndex = 2
		Me.buttonOk.Text = "OK"
		Me.buttonOk.UseVisualStyleBackColor = true
		AddHandler Me.buttonOk.Click, AddressOf Me.ButtonOkClick
		'
		'buttonCancel
		'
		Me.buttonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.buttonCancel.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonCancel.Location = New System.Drawing.Point(334, 12)
		Me.buttonCancel.Name = "buttonCancel"
		Me.buttonCancel.Size = New System.Drawing.Size(67, 23)
		Me.buttonCancel.TabIndex = 3
		Me.buttonCancel.Text = "Cancel"
		Me.buttonCancel.UseVisualStyleBackColor = true
		'
		'groupBox1
		'
		Me.groupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.groupBox1.Controls.Add(Me.checkBoxSourceOnly)
		Me.groupBox1.Controls.Add(Me.checkBox1)
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Controls.Add(Me.buttonBrowseSource)
		Me.groupBox1.Controls.Add(Me.textSourceFolder)
		Me.groupBox1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.groupBox1.Location = New System.Drawing.Point(9, 3)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(468, 79)
		Me.groupBox1.TabIndex = 4
		Me.groupBox1.TabStop = false
		Me.groupBox1.Text = "Source"
		'
		'checkBoxSourceOnly
		'
		Me.checkBoxSourceOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.checkBoxSourceOnly.AutoSize = true
		Me.checkBoxSourceOnly.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.checkBoxSourceOnly.Location = New System.Drawing.Point(250, 47)
		Me.checkBoxSourceOnly.Name = "checkBoxSourceOnly"
		Me.checkBoxSourceOnly.Size = New System.Drawing.Size(208, 17)
		Me.checkBoxSourceOnly.TabIndex = 3
		Me.checkBoxSourceOnly.Text = "use same folder for source and target"
		Me.checkBoxSourceOnly.UseVisualStyleBackColor = true
		AddHandler Me.checkBoxSourceOnly.CheckedChanged, AddressOf Me.CheckBoxSourceOnlyCheckedChanged
		'
		'checkBox1
		'
		Me.checkBox1.AutoSize = true
		Me.checkBox1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.checkBox1.Location = New System.Drawing.Point(16, 47)
		Me.checkBox1.Name = "checkBox1"
		Me.checkBox1.Size = New System.Drawing.Size(116, 17)
		Me.checkBox1.TabIndex = 2
		Me.checkBox1.Text = "include sub-folders"
		Me.checkBox1.UseVisualStyleBackColor = true
		AddHandler Me.checkBox1.CheckedChanged, AddressOf Me.CheckBox1CheckedChanged
		'
		'label1
		'
		Me.label1.AutoSize = true
		Me.label1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.label1.Location = New System.Drawing.Point(16, 23)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(35, 13)
		Me.label1.TabIndex = 1
		Me.label1.Text = "folder"
		'
		'buttonBrowseSource
		'
		Me.buttonBrowseSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.buttonBrowseSource.Font = New System.Drawing.Font("Tahoma", 10!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonBrowseSource.Location = New System.Drawing.Point(430, 18)
		Me.buttonBrowseSource.Name = "buttonBrowseSource"
		Me.buttonBrowseSource.Size = New System.Drawing.Size(23, 23)
		Me.buttonBrowseSource.TabIndex = 1
		Me.buttonBrowseSource.Text = "..."
		Me.buttonBrowseSource.UseVisualStyleBackColor = true
		AddHandler Me.buttonBrowseSource.Click, AddressOf Me.ButtonBrowseSourceClick
		'
		'textSourceFolder
		'
		Me.textSourceFolder.AllowDrop = true
		Me.textSourceFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textSourceFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
		Me.textSourceFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
		Me.textSourceFolder.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.textSourceFolder.Location = New System.Drawing.Point(57, 20)
		Me.textSourceFolder.Name = "textSourceFolder"
		Me.textSourceFolder.Size = New System.Drawing.Size(367, 21)
		Me.textSourceFolder.TabIndex = 0
		AddHandler Me.textSourceFolder.TextChanged, AddressOf Me.TextSourceFolderTextChanged
		AddHandler Me.textSourceFolder.DragDrop, AddressOf Me.GroupBox1DragDrop
		AddHandler Me.textSourceFolder.Leave, AddressOf Me.TextSourceFolderLeave
		AddHandler Me.textSourceFolder.Enter, AddressOf Me.TextSourceFolderEnter
		AddHandler Me.textSourceFolder.DragOver, AddressOf Me.GroupBox1DragOver
		'
		'groupBox2
		'
		Me.groupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.groupBox2.Controls.Add(Me.checkBox2)
		Me.groupBox2.Controls.Add(Me.label2)
		Me.groupBox2.Controls.Add(Me.buttonBrowseTarget)
		Me.groupBox2.Controls.Add(Me.textTargetFolder)
		Me.groupBox2.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.groupBox2.Location = New System.Drawing.Point(9, 89)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(468, 77)
		Me.groupBox2.TabIndex = 5
		Me.groupBox2.TabStop = false
		Me.groupBox2.Text = "Target"
		'
		'checkBox2
		'
		Me.checkBox2.AutoSize = true
		Me.checkBox2.Enabled = false
		Me.checkBox2.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.checkBox2.Location = New System.Drawing.Point(16, 47)
		Me.checkBox2.Name = "checkBox2"
		Me.checkBox2.Size = New System.Drawing.Size(116, 17)
		Me.checkBox2.TabIndex = 3
		Me.checkBox2.Text = "include sub-folders"
		Me.checkBox2.UseVisualStyleBackColor = true
		'
		'label2
		'
		Me.label2.AutoSize = true
		Me.label2.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.label2.Location = New System.Drawing.Point(16, 23)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(35, 13)
		Me.label2.TabIndex = 1
		Me.label2.Text = "folder"
		'
		'buttonBrowseTarget
		'
		Me.buttonBrowseTarget.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.buttonBrowseTarget.Font = New System.Drawing.Font("Tahoma", 10!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonBrowseTarget.Location = New System.Drawing.Point(430, 18)
		Me.buttonBrowseTarget.Name = "buttonBrowseTarget"
		Me.buttonBrowseTarget.Size = New System.Drawing.Size(23, 23)
		Me.buttonBrowseTarget.TabIndex = 1
		Me.buttonBrowseTarget.Text = "..."
		Me.buttonBrowseTarget.UseVisualStyleBackColor = true
		AddHandler Me.buttonBrowseTarget.Click, AddressOf Me.ButtonBrowseTargetClick
		'
		'textTargetFolder
		'
		Me.textTargetFolder.AllowDrop = true
		Me.textTargetFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textTargetFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
		Me.textTargetFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
		Me.textTargetFolder.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.textTargetFolder.Location = New System.Drawing.Point(57, 20)
		Me.textTargetFolder.Name = "textTargetFolder"
		Me.textTargetFolder.Size = New System.Drawing.Size(367, 21)
		Me.textTargetFolder.TabIndex = 0
		AddHandler Me.textTargetFolder.DragDrop, AddressOf Me.GroupBox2DragDrop
		AddHandler Me.textTargetFolder.Leave, AddressOf Me.TextTargetFolderLeave
		AddHandler Me.textTargetFolder.Enter, AddressOf Me.TextTargetFolderEnter
		AddHandler Me.textTargetFolder.DragOver, AddressOf Me.GroupBox2DragOver
		'
		'groupBox3
		'
		Me.groupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.groupBox3.Controls.Add(Me.checkBox3)
		Me.groupBox3.Controls.Add(Me.checkBoxXml)
		Me.groupBox3.Controls.Add(Me.checkBoxImages)
		Me.groupBox3.Controls.Add(Me.checkBoxHTML)
		Me.groupBox3.Controls.Add(Me.label3)
		Me.groupBox3.Controls.Add(Me.textFilter)
		Me.groupBox3.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.groupBox3.Location = New System.Drawing.Point(9, 171)
		Me.groupBox3.Name = "groupBox3"
		Me.groupBox3.Size = New System.Drawing.Size(468, 73)
		Me.groupBox3.TabIndex = 6
		Me.groupBox3.TabStop = false
		Me.groupBox3.Text = "Files"
		'
		'checkBox3
		'
		Me.checkBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.checkBox3.AutoSize = true
		Me.checkBox3.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.checkBox3.Location = New System.Drawing.Point(368, 20)
		Me.checkBox3.Name = "checkBox3"
		Me.checkBox3.Size = New System.Drawing.Size(85, 17)
		Me.checkBox3.TabIndex = 7
		Me.checkBox3.Text = "custom filter"
		Me.checkBox3.UseVisualStyleBackColor = true
		AddHandler Me.checkBox3.CheckedChanged, AddressOf Me.CheckBox3CheckedChanged
		'
		'checkBoxXml
		'
		Me.checkBoxXml.AutoSize = true
		Me.checkBoxXml.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.checkBoxXml.Location = New System.Drawing.Point(133, 20)
		Me.checkBoxXml.Name = "checkBoxXml"
		Me.checkBoxXml.Size = New System.Drawing.Size(42, 17)
		Me.checkBoxXml.TabIndex = 6
		Me.checkBoxXml.Text = "xml"
		Me.checkBoxXml.UseVisualStyleBackColor = true
		AddHandler Me.checkBoxXml.CheckedChanged, AddressOf Me.CheckBoxXmlCheckedChanged
		'
		'checkBoxImages
		'
		Me.checkBoxImages.AutoSize = true
		Me.checkBoxImages.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.checkBoxImages.Location = New System.Drawing.Point(68, 20)
		Me.checkBoxImages.Name = "checkBoxImages"
		Me.checkBoxImages.Size = New System.Drawing.Size(59, 17)
		Me.checkBoxImages.TabIndex = 5
		Me.checkBoxImages.Text = "images"
		Me.checkBoxImages.UseVisualStyleBackColor = true
		AddHandler Me.checkBoxImages.CheckedChanged, AddressOf Me.CheckBoxImagesCheckedChanged
		'
		'checkBoxHTML
		'
		Me.checkBoxHTML.AutoSize = true
		Me.checkBoxHTML.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.checkBoxHTML.Location = New System.Drawing.Point(16, 20)
		Me.checkBoxHTML.Name = "checkBoxHTML"
		Me.checkBoxHTML.Size = New System.Drawing.Size(46, 17)
		Me.checkBoxHTML.TabIndex = 4
		Me.checkBoxHTML.Text = "html"
		Me.checkBoxHTML.UseVisualStyleBackColor = true
		AddHandler Me.checkBoxHTML.CheckedChanged, AddressOf Me.CheckBoxHTMLCheckedChanged
		'
		'label3
		'
		Me.label3.AutoSize = true
		Me.label3.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.label3.Location = New System.Drawing.Point(16, 46)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(29, 13)
		Me.label3.TabIndex = 1
		Me.label3.Text = "filter"
		'
		'textFilter
		'
		Me.textFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textFilter.Enabled = false
		Me.textFilter.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.textFilter.ForeColor = System.Drawing.Color.Gray
		Me.textFilter.Location = New System.Drawing.Point(57, 43)
		Me.textFilter.Name = "textFilter"
		Me.textFilter.Size = New System.Drawing.Size(396, 21)
		Me.textFilter.TabIndex = 0
		AddHandler Me.textFilter.Leave, AddressOf Me.TextFilterLeave
		'
		'panelButtons
		'
		Me.panelButtons.Controls.Add(Me.labelWarning)
		Me.panelButtons.Controls.Add(Me.buttonOk)
		Me.panelButtons.Controls.Add(Me.buttonCancel)
		Me.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.panelButtons.Location = New System.Drawing.Point(0, 275)
		Me.panelButtons.Name = "panelButtons"
		Me.panelButtons.Size = New System.Drawing.Size(486, 46)
		Me.panelButtons.TabIndex = 7
		'
		'labelWarning
		'
		Me.labelWarning.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.labelWarning.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(128,Byte),Integer), CType(CType(0,Byte),Integer))
		Me.labelWarning.Location = New System.Drawing.Point(12, 7)
		Me.labelWarning.Name = "labelWarning"
		Me.labelWarning.Size = New System.Drawing.Size(316, 34)
		Me.labelWarning.TabIndex = 4
		'
		'groupBox4
		'
		Me.groupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.groupBox4.Controls.Add(Me.label5)
		Me.groupBox4.Controls.Add(Me.textBox2)
		Me.groupBox4.Controls.Add(Me.label4)
		Me.groupBox4.Controls.Add(Me.textBox1)
		Me.groupBox4.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.groupBox4.Location = New System.Drawing.Point(9, 3)
		Me.groupBox4.Name = "groupBox4"
		Me.groupBox4.Size = New System.Drawing.Size(468, 81)
		Me.groupBox4.TabIndex = 8
		Me.groupBox4.TabStop = false
		Me.groupBox4.Text = "Filters"
		'
		'label5
		'
		Me.label5.AutoSize = true
		Me.label5.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.label5.Location = New System.Drawing.Point(21, 23)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(56, 13)
		Me.label5.TabIndex = 3
		Me.label5.Text = "include list"
		'
		'textBox2
		'
		Me.textBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox2.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.textBox2.ForeColor = System.Drawing.SystemColors.WindowText
		Me.textBox2.Location = New System.Drawing.Point(80, 20)
		Me.textBox2.Name = "textBox2"
		Me.textBox2.Size = New System.Drawing.Size(373, 21)
		Me.textBox2.TabIndex = 2
		AddHandler Me.textBox2.TextChanged, AddressOf Me.TextBox2TextChanged
		AddHandler Me.textBox2.Leave, AddressOf Me.TextLeave
		'
		'label4
		'
		Me.label4.AutoSize = true
		Me.label4.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.label4.Location = New System.Drawing.Point(21, 50)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(53, 13)
		Me.label4.TabIndex = 1
		Me.label4.Text = "ignore list"
		'
		'textBox1
		'
		Me.textBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.textBox1.ForeColor = System.Drawing.SystemColors.WindowText
		Me.textBox1.Location = New System.Drawing.Point(80, 47)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(373, 21)
		Me.textBox1.TabIndex = 0
		AddHandler Me.textBox1.TextChanged, AddressOf Me.TextBox1TextChanged
		AddHandler Me.textBox1.Leave, AddressOf Me.TextLeave
		'
		'groupBox5
		'
		Me.groupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.groupBox5.Controls.Add(Me.label6)
		Me.groupBox5.Controls.Add(Me.textBox3)
		Me.groupBox5.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.groupBox5.Location = New System.Drawing.Point(9, 91)
		Me.groupBox5.Name = "groupBox5"
		Me.groupBox5.Size = New System.Drawing.Size(468, 54)
		Me.groupBox5.TabIndex = 9
		Me.groupBox5.TabStop = false
		Me.groupBox5.Text = "Highlight span tags"
		'
		'label6
		'
		Me.label6.AutoSize = true
		Me.label6.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.label6.Location = New System.Drawing.Point(24, 23)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(30, 13)
		Me.label6.TabIndex = 4
		Me.label6.Text = "class"
		'
		'textBox3
		'
		Me.textBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox3.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.textBox3.ForeColor = System.Drawing.SystemColors.WindowText
		Me.textBox3.Location = New System.Drawing.Point(57, 20)
		Me.textBox3.Name = "textBox3"
		Me.textBox3.Size = New System.Drawing.Size(396, 21)
		Me.textBox3.TabIndex = 2
		AddHandler Me.textBox3.TextChanged, AddressOf Me.TextBox3TextChanged
		AddHandler Me.textBox3.Leave, AddressOf Me.TextLeave
		'
		'contextMenuStrip1
		'
		Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.advancedToolStripMenuItem})
		Me.contextMenuStrip1.Name = "contextMenuStrip1"
		Me.contextMenuStrip1.Size = New System.Drawing.Size(128, 26)
		'
		'advancedToolStripMenuItem
		'
		Me.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem"
		Me.advancedToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
		Me.advancedToolStripMenuItem.Text = "Advanced"
		AddHandler Me.advancedToolStripMenuItem.Click, AddressOf Me.AdvancedToolStripMenuItemClick
		'
		'panelAdvanced
		'
		Me.panelAdvanced.Controls.Add(Me.groupBox4)
		Me.panelAdvanced.Controls.Add(Me.groupBox5)
		Me.panelAdvanced.Dock = System.Windows.Forms.DockStyle.Top
		Me.panelAdvanced.Location = New System.Drawing.Point(0, 247)
		Me.panelAdvanced.Name = "panelAdvanced"
		Me.panelAdvanced.Size = New System.Drawing.Size(486, 157)
		Me.panelAdvanced.TabIndex = 10
		Me.panelAdvanced.Visible = false
		'
		'panelBasic
		'
		Me.panelBasic.Controls.Add(Me.groupBox1)
		Me.panelBasic.Controls.Add(Me.groupBox2)
		Me.panelBasic.Controls.Add(Me.groupBox3)
		Me.panelBasic.Dock = System.Windows.Forms.DockStyle.Top
		Me.panelBasic.Location = New System.Drawing.Point(0, 0)
		Me.panelBasic.Name = "panelBasic"
		Me.panelBasic.Size = New System.Drawing.Size(486, 247)
		Me.panelBasic.TabIndex = 11
		'
		'ProjectSettingForm
		'
		Me.AcceptButton = Me.buttonOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.buttonCancel
		Me.ClientSize = New System.Drawing.Size(486, 321)
		Me.ContextMenuStrip = Me.contextMenuStrip1
		Me.ControlBox = false
		Me.Controls.Add(Me.panelAdvanced)
		Me.Controls.Add(Me.panelButtons)
		Me.Controls.Add(Me.panelBasic)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "ProjectSettingForm"
		Me.ShowInTaskbar = false
		Me.Text = "Project Settings"
		AddHandler Load, AddressOf Me.ProjectSettingFormLoad
		Me.groupBox1.ResumeLayout(false)
		Me.groupBox1.PerformLayout
		Me.groupBox2.ResumeLayout(false)
		Me.groupBox2.PerformLayout
		Me.groupBox3.ResumeLayout(false)
		Me.groupBox3.PerformLayout
		Me.panelButtons.ResumeLayout(false)
		Me.groupBox4.ResumeLayout(false)
		Me.groupBox4.PerformLayout
		Me.groupBox5.ResumeLayout(false)
		Me.groupBox5.PerformLayout
		Me.contextMenuStrip1.ResumeLayout(false)
		Me.panelAdvanced.ResumeLayout(false)
		Me.panelBasic.ResumeLayout(false)
		Me.ResumeLayout(false)
	End Sub
	Private panelBasic As System.Windows.Forms.Panel
	Private panelAdvanced As System.Windows.Forms.Panel
	Private advancedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
	Private textBox3 As System.Windows.Forms.TextBox
	Private label6 As System.Windows.Forms.Label
	Private groupBox5 As System.Windows.Forms.GroupBox
	Private textBox1 As System.Windows.Forms.TextBox
	Private label4 As System.Windows.Forms.Label
	Private textBox2 As System.Windows.Forms.TextBox
	Private label5 As System.Windows.Forms.Label
	Private groupBox4 As System.Windows.Forms.GroupBox
	Private checkBoxSourceOnly As System.Windows.Forms.CheckBox
	Private checkBox3 As System.Windows.Forms.CheckBox
	Private labelWarning As System.Windows.Forms.Label
	Private textFilter As System.Windows.Forms.TextBox
	Private buttonBrowseSource As System.Windows.Forms.Button
	Private buttonBrowseTarget As System.Windows.Forms.Button
	Private checkBoxHTML As System.Windows.Forms.CheckBox
	Private checkBoxImages As System.Windows.Forms.CheckBox
	Private checkBoxXml As System.Windows.Forms.CheckBox
	Private panelButtons As System.Windows.Forms.Panel
	Private checkBox2 As System.Windows.Forms.CheckBox
	Private checkBox1 As System.Windows.Forms.CheckBox
	Private buttonCancel As System.Windows.Forms.Button
	Private label3 As System.Windows.Forms.Label
	Private groupBox3 As System.Windows.Forms.GroupBox
	Private textTargetFolder As System.Windows.Forms.TextBox
	Private label2 As System.Windows.Forms.Label
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private textSourceFolder As System.Windows.Forms.TextBox
	Private label1 As System.Windows.Forms.Label
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private buttonOk As System.Windows.Forms.Button
	
End Class

