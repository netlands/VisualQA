'
' Created by SharpDevelop.
' User: bergb
' Date: 2006/05/29
' Time: 12:26
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
		Me.panel2 = New System.Windows.Forms.Panel()
		Me.panel2Contents = New System.Windows.Forms.Panel()
		Me.pictureBox2 = New System.Windows.Forms.PictureBox()
		Me.panel2Info = New System.Windows.Forms.Panel()
		Me.panelTargetInfo = New System.Windows.Forms.Panel()
		Me.contextMenuStripInfo = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.refreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.historyBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.pinToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
		Me.openFileInToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.browserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.editorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.OpenContainingFoldertoolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
		Me.copyLocationToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.copyFileNameToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.copyTitleToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
		Me.ChangeEncodingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.contextMenuStripEncoding = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.encodingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.sourceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.labelSize2 = New System.Windows.Forms.Label()
		Me.pictureBox5 = New System.Windows.Forms.PictureBox()
		Me.contextMenuStripPinToSource = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.pinToOtherSideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.labelFileInfo2 = New System.Windows.Forms.Label()
		Me.textBoxTitle2 = New System.Windows.Forms.TextBox()
		Me.labelEncoding2 = New System.Windows.Forms.Label()
		Me.labelImgInfo2 = New System.Windows.Forms.Label()
		Me.targetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.panelBrowser = New System.Windows.Forms.Panel()
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.contextMenuStripCompare = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.openFilesInCompareToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.openFoldersInCompareToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
		Me.originalSizetoolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.fitImagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.splitViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.backgroundColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
		Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.panel1 = New System.Windows.Forms.Panel()
		Me.panel1Contents = New System.Windows.Forms.Panel()
		Me.pictureBox1 = New System.Windows.Forms.PictureBox()
		Me.panel1Info = New System.Windows.Forms.Panel()
		Me.panelSourceInfo = New System.Windows.Forms.Panel()
		Me.labelSize1 = New System.Windows.Forms.Label()
		Me.pictureBox4 = New System.Windows.Forms.PictureBox()
		Me.contextMenuStripPinToTarget = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.toolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
		Me.labelFileInfo1 = New System.Windows.Forms.Label()
		Me.textBoxTitle1 = New System.Windows.Forms.TextBox()
		Me.labelEncoding1 = New System.Windows.Forms.Label()
		Me.labelImgInfo1 = New System.Windows.Forms.Label()
		Me.panelContents = New System.Windows.Forms.Panel()
		Me.toolStripContainerMainForm = New System.Windows.Forms.ToolStripContainer()
		Me.splitContainerContents = New System.Windows.Forms.SplitContainer()
		Me.contextMenuStripFileList = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.dockToTopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.collapseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.tabControl1 = New System.Windows.Forms.TabControl()
		Me.tabPage1 = New System.Windows.Forms.TabPage()
		Me.treeView1 = New System.Windows.Forms.TreeView()
		Me.imageListSmallIcons = New System.Windows.Forms.ImageList(Me.components)
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.tabPage2 = New System.Windows.Forms.TabPage()
		Me.webBrowserLog = New System.Windows.Forms.WebBrowser()
		Me.contextMenuLog = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.saveLogToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.clearLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.button2 = New System.Windows.Forms.Button()
		Me.searchPanel = New System.Windows.Forms.Panel()
		Me.textBox1 = New System.Windows.Forms.TextBox()
		Me.contextMenuStripSearch = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.clearSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator()
		Me.saveSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.readSavedSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.deleteSavedSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator23 = New System.Windows.Forms.ToolStripSeparator()
		Me.searchColorToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.contextMenuStripSearchColor = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.magentaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.yellowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.cyanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.limeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.searchColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.contextMenuStripTreeFilter = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.activateFilterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.clearFilterShowAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
		Me.filterChecked = New System.Windows.Forms.ToolStripMenuItem()
		Me.filterUnchecked = New System.Windows.Forms.ToolStripMenuItem()
		Me.filterOk = New System.Windows.Forms.ToolStripMenuItem()
		Me.filterNg = New System.Windows.Forms.ToolStripMenuItem()
		Me.filterOrphans = New System.Windows.Forms.ToolStripMenuItem()
		Me.filterProblem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
		Me.filterComments = New System.Windows.Forms.ToolStripMenuItem()
		Me.ProjectFilterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.contextMenuStripFilter = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.noOrphansToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.panel3 = New System.Windows.Forms.Panel()
		Me.panel5 = New System.Windows.Forms.Panel()
		Me.pictureBox3 = New System.Windows.Forms.PictureBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.textBoxJump = New System.Windows.Forms.TextBox()
		Me.labelCounter = New System.Windows.Forms.Label()
		Me.buttonNextOff = New System.Windows.Forms.Button()
		Me.buttonNext = New System.Windows.Forms.Button()
		Me.buttonPreviousOff = New System.Windows.Forms.Button()
		Me.buttonPrevious = New System.Windows.Forms.Button()
		Me.panel4 = New System.Windows.Forms.Panel()
		Me.buttonPageDown = New System.Windows.Forms.Button()
		Me.buttonPageUp = New System.Windows.Forms.Button()
		Me.buttonLineDown = New System.Windows.Forms.Button()
		Me.buttonLineUp = New System.Windows.Forms.Button()
		Me.scrollPanel = New System.Windows.Forms.Panel()
		Me.btnToggleScroll = New System.Windows.Forms.Button()
		Me.labelStatus = New System.Windows.Forms.Label()
		Me.checkBox1 = New System.Windows.Forms.CheckBox()
		Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
		Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.openSourceFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.openTargetFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
		Me.saveProjectToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.openProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.newProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.saveProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.closeProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.projectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.settingsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.refreshToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
		Me.saveReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
		Me.saveLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.clearLogToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
		Me.resetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.contextMenuStripTools = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.annotateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.openFilesInCompareToolToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.openSourceFileInEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.openTargetFileInEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator25 = New System.Windows.Forms.ToolStripSeparator()
		Me.pluginsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.dEBUGToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.viewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.orientationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.verticalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.horizontalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.fullScreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.dualscreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator27 = New System.Windows.Forms.ToolStripSeparator()
		Me.browserSizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemSize1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemSize2 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemSize3 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemSize4 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator26 = New System.Windows.Forms.ToolStripSeparator()
		Me.excludeScrollbarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.showBrowserWidthToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
		Me.splitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.sourceOnlyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.targetOnlyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
		Me.splitSizeFixedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
		Me.showFileListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.showInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.showCommentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
		Me.hideControlsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.showSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.settingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.modeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.IE7ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.IE8ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.IE9ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.IE10ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.IE11ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.IE12ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
		Me.synchronizedScrollToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.defaultToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.alternativeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.alternative2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.imageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
		Me.preferedEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.preferedCompareToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
		Me.useMouseGesturesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.autoRefreshAfterEditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.fitImagesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.autoScrollToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
		Me.highlightLocalLinksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.highlightUITermsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.contextMenuStripHighlightPlugins = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.toolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
		Me.followLinksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
		Me.saveSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator24 = New System.Windows.Forms.ToolStripSeparator()
		Me.projectToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.autoUpdateStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.checkForOrphansToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.parseXMLOnLoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.checkaddMD5FileHashToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.yadaYadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.annotateModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.contextMenuStripStatus = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.uncheckedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.checkedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.oKToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.nGToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.flaggedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.problemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.warningToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.contextMenuStripFileItem = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.openInEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator()
		Me.toolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
		Me.uncheckedToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.fileSystemWatcher1 = New System.IO.FileSystemWatcher()
		Me.t = New System.Windows.Forms.Timer(Me.components)
		Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
		Me.imageListIcons = New System.Windows.Forms.ImageList(Me.components)
		Me.imageList2 = New System.Windows.Forms.ImageList(Me.components)
		Me.colorDialog1 = New System.Windows.Forms.ColorDialog()
		Me.startAnnotate = New System.Windows.Forms.Timer(Me.components)
		Me.panel2.SuspendLayout
		Me.panel2Contents.SuspendLayout
		CType(Me.pictureBox2,System.ComponentModel.ISupportInitialize).BeginInit
		Me.panel2Info.SuspendLayout
		Me.panelTargetInfo.SuspendLayout
		Me.contextMenuStripInfo.SuspendLayout
		Me.contextMenuStripEncoding.SuspendLayout
		CType(Me.pictureBox5,System.ComponentModel.ISupportInitialize).BeginInit
		Me.contextMenuStripPinToSource.SuspendLayout
		Me.panelBrowser.SuspendLayout
		Me.splitContainer1.Panel1.SuspendLayout
		Me.splitContainer1.Panel2.SuspendLayout
		Me.splitContainer1.SuspendLayout
		Me.contextMenuStripCompare.SuspendLayout
		Me.panel1.SuspendLayout
		Me.panel1Contents.SuspendLayout
		CType(Me.pictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
		Me.panel1Info.SuspendLayout
		Me.panelSourceInfo.SuspendLayout
		CType(Me.pictureBox4,System.ComponentModel.ISupportInitialize).BeginInit
		Me.contextMenuStripPinToTarget.SuspendLayout
		Me.panelContents.SuspendLayout
		Me.toolStripContainerMainForm.ContentPanel.SuspendLayout
		Me.toolStripContainerMainForm.SuspendLayout
		Me.splitContainerContents.Panel1.SuspendLayout
		Me.splitContainerContents.Panel2.SuspendLayout
		Me.splitContainerContents.SuspendLayout
		Me.contextMenuStripFileList.SuspendLayout
		Me.tabControl1.SuspendLayout
		Me.tabPage1.SuspendLayout
		Me.tabPage2.SuspendLayout
		Me.contextMenuLog.SuspendLayout
		Me.searchPanel.SuspendLayout
		Me.contextMenuStripSearch.SuspendLayout
		Me.contextMenuStripSearchColor.SuspendLayout
		Me.contextMenuStripTreeFilter.SuspendLayout
		Me.contextMenuStripFilter.SuspendLayout
		Me.panel3.SuspendLayout
		Me.panel5.SuspendLayout
		CType(Me.pictureBox3,System.ComponentModel.ISupportInitialize).BeginInit
		Me.panel4.SuspendLayout
		Me.scrollPanel.SuspendLayout
		Me.menuStrip1.SuspendLayout
		Me.contextMenuStripTools.SuspendLayout
		Me.contextMenuStripStatus.SuspendLayout
		Me.contextMenuStripFileItem.SuspendLayout
		CType(Me.fileSystemWatcher1,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'panel2
		'
		Me.panel2.AutoScroll = true
		Me.panel2.BackColor = System.Drawing.SystemColors.Control
		Me.panel2.Controls.Add(Me.panel2Contents)
		Me.panel2.Controls.Add(Me.panel2Info)
		Me.panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel2.Location = New System.Drawing.Point(0, 0)
		Me.panel2.Margin = New System.Windows.Forms.Padding(0)
		Me.panel2.Name = "panel2"
		Me.panel2.Size = New System.Drawing.Size(343, 399)
		Me.panel2.TabIndex = 0
		AddHandler Me.panel2.Scroll, AddressOf Me.Panel2Scroll
		'
		'panel2Contents
		'
		Me.panel2Contents.Controls.Add(Me.pictureBox2)
		Me.panel2Contents.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel2Contents.Location = New System.Drawing.Point(0, 44)
		Me.panel2Contents.Margin = New System.Windows.Forms.Padding(0)
		Me.panel2Contents.Name = "panel2Contents"
		Me.panel2Contents.Size = New System.Drawing.Size(343, 355)
		Me.panel2Contents.TabIndex = 3
		'
		'pictureBox2
		'
		Me.pictureBox2.Location = New System.Drawing.Point(0, 0)
		Me.pictureBox2.Margin = New System.Windows.Forms.Padding(0)
		Me.pictureBox2.Name = "pictureBox2"
		Me.pictureBox2.Size = New System.Drawing.Size(200, 300)
		Me.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
		Me.pictureBox2.TabIndex = 2
		Me.pictureBox2.TabStop = false
		Me.pictureBox2.Visible = false
		AddHandler Me.pictureBox2.MouseDown, AddressOf Me.PictureBox2MouseDown
		AddHandler Me.pictureBox2.MouseMove, AddressOf Me.PictureBox2MouseMove
		'
		'panel2Info
		'
		Me.panel2Info.BackColor = System.Drawing.SystemColors.Control
		Me.panel2Info.Controls.Add(Me.panelTargetInfo)
		Me.panel2Info.Dock = System.Windows.Forms.DockStyle.Top
		Me.panel2Info.Location = New System.Drawing.Point(0, 0)
		Me.panel2Info.Name = "panel2Info"
		Me.panel2Info.Size = New System.Drawing.Size(343, 44)
		Me.panel2Info.TabIndex = 4
		Me.panel2Info.Visible = false
		'
		'panelTargetInfo
		'
		Me.panelTargetInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(254,Byte),Integer), CType(CType(254,Byte),Integer), CType(CType(254,Byte),Integer))
		Me.panelTargetInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.panelTargetInfo.ContextMenuStrip = Me.contextMenuStripInfo
		Me.panelTargetInfo.Controls.Add(Me.labelSize2)
		Me.panelTargetInfo.Controls.Add(Me.pictureBox5)
		Me.panelTargetInfo.Controls.Add(Me.labelFileInfo2)
		Me.panelTargetInfo.Controls.Add(Me.textBoxTitle2)
		Me.panelTargetInfo.Controls.Add(Me.labelEncoding2)
		Me.panelTargetInfo.Controls.Add(Me.labelImgInfo2)
		Me.panelTargetInfo.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panelTargetInfo.Location = New System.Drawing.Point(0, 0)
		Me.panelTargetInfo.Name = "panelTargetInfo"
		Me.panelTargetInfo.Size = New System.Drawing.Size(343, 44)
		Me.panelTargetInfo.TabIndex = 4
		Me.panelTargetInfo.Visible = false
		'
		'contextMenuStripInfo
		'
		Me.contextMenuStripInfo.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.refreshToolStripMenuItem, Me.historyBackToolStripMenuItem, Me.pinToolStripMenuItem, Me.toolStripSeparator8, Me.openFileInToolStripMenuItem, Me.OpenContainingFoldertoolStripMenuItem, Me.toolStripSeparator6, Me.copyLocationToClipboardToolStripMenuItem, Me.copyFileNameToClipboardToolStripMenuItem, Me.copyTitleToClipboardToolStripMenuItem, Me.toolStripSeparator7, Me.ChangeEncodingToolStripMenuItem})
		Me.contextMenuStripInfo.Name = "contextMenuStripInfo"
		Me.contextMenuStripInfo.OwnerItem = Me.targetToolStripMenuItem
		Me.contextMenuStripInfo.ShowImageMargin = false
		Me.contextMenuStripInfo.Size = New System.Drawing.Size(194, 220)
		AddHandler Me.contextMenuStripInfo.Opening, AddressOf Me.ContextMenuStripInfoOpening
		'
		'refreshToolStripMenuItem
		'
		Me.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem"
		Me.refreshToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
		Me.refreshToolStripMenuItem.Text = "Refresh"
		AddHandler Me.refreshToolStripMenuItem.Click, AddressOf Me.RefreshToolStripMenuItemClick
		'
		'historyBackToolStripMenuItem
		'
		Me.historyBackToolStripMenuItem.Enabled = false
		Me.historyBackToolStripMenuItem.Name = "historyBackToolStripMenuItem"
		Me.historyBackToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
		Me.historyBackToolStripMenuItem.Text = "Back"
		AddHandler Me.historyBackToolStripMenuItem.Click, AddressOf Me.HistoryBackToolStripMenuItemClick
		'
		'pinToolStripMenuItem
		'
		Me.pinToolStripMenuItem.Name = "pinToolStripMenuItem"
		Me.pinToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
		Me.pinToolStripMenuItem.Text = "Pin"
		Me.pinToolStripMenuItem.Visible = false
		AddHandler Me.pinToolStripMenuItem.Click, AddressOf Me.PinToolStripMenuItemClick
		'
		'toolStripSeparator8
		'
		Me.toolStripSeparator8.Name = "toolStripSeparator8"
		Me.toolStripSeparator8.Size = New System.Drawing.Size(190, 6)
		'
		'openFileInToolStripMenuItem
		'
		Me.openFileInToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.browserToolStripMenuItem, Me.editorToolStripMenuItem})
		Me.openFileInToolStripMenuItem.Name = "openFileInToolStripMenuItem"
		Me.openFileInToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
		Me.openFileInToolStripMenuItem.Text = "Open in..."
		'
		'browserToolStripMenuItem
		'
		Me.browserToolStripMenuItem.Name = "browserToolStripMenuItem"
		Me.browserToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
		Me.browserToolStripMenuItem.Text = "Browser"
		AddHandler Me.browserToolStripMenuItem.Click, AddressOf Me.BrowserToolStripMenuItemClick
		'
		'editorToolStripMenuItem
		'
		Me.editorToolStripMenuItem.Name = "editorToolStripMenuItem"
		Me.editorToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
		Me.editorToolStripMenuItem.Text = "Editor"
		AddHandler Me.editorToolStripMenuItem.Click, AddressOf Me.EditorToolStripMenuItemClick
		'
		'OpenContainingFoldertoolStripMenuItem
		'
		Me.OpenContainingFoldertoolStripMenuItem.Name = "OpenContainingFoldertoolStripMenuItem"
		Me.OpenContainingFoldertoolStripMenuItem.Size = New System.Drawing.Size(193, 22)
		Me.OpenContainingFoldertoolStripMenuItem.Text = "Open containing folder"
		AddHandler Me.OpenContainingFoldertoolStripMenuItem.Click, AddressOf Me.OpenContainingFoldertoolStripMenuItemClick
		'
		'toolStripSeparator6
		'
		Me.toolStripSeparator6.Name = "toolStripSeparator6"
		Me.toolStripSeparator6.Size = New System.Drawing.Size(190, 6)
		'
		'copyLocationToClipboardToolStripMenuItem
		'
		Me.copyLocationToClipboardToolStripMenuItem.Name = "copyLocationToClipboardToolStripMenuItem"
		Me.copyLocationToClipboardToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
		Me.copyLocationToClipboardToolStripMenuItem.Text = "Copy location to clipboard"
		AddHandler Me.copyLocationToClipboardToolStripMenuItem.Click, AddressOf Me.CopyLocationToClipboardToolStripMenuItemClick
		'
		'copyFileNameToClipboardToolStripMenuItem
		'
		Me.copyFileNameToClipboardToolStripMenuItem.Name = "copyFileNameToClipboardToolStripMenuItem"
		Me.copyFileNameToClipboardToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
		Me.copyFileNameToClipboardToolStripMenuItem.Text = "Copy filename to clipboard"
		AddHandler Me.copyFileNameToClipboardToolStripMenuItem.Click, AddressOf Me.CopyFileNameToClipboardToolStripMenuItemClick
		'
		'copyTitleToClipboardToolStripMenuItem
		'
		Me.copyTitleToClipboardToolStripMenuItem.Name = "copyTitleToClipboardToolStripMenuItem"
		Me.copyTitleToClipboardToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
		Me.copyTitleToClipboardToolStripMenuItem.Text = "Copy title to clipboard"
		AddHandler Me.copyTitleToClipboardToolStripMenuItem.Click, AddressOf Me.CopyTitleToClipboardToolStripMenuItemClick
		'
		'toolStripSeparator7
		'
		Me.toolStripSeparator7.Name = "toolStripSeparator7"
		Me.toolStripSeparator7.Size = New System.Drawing.Size(190, 6)
		'
		'ChangeEncodingToolStripMenuItem
		'
		Me.ChangeEncodingToolStripMenuItem.DropDown = Me.contextMenuStripEncoding
		Me.ChangeEncodingToolStripMenuItem.Name = "ChangeEncodingToolStripMenuItem"
		Me.ChangeEncodingToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
		Me.ChangeEncodingToolStripMenuItem.Text = "Change encoding..."
		'
		'contextMenuStripEncoding
		'
		Me.contextMenuStripEncoding.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.encodingToolStripMenuItem})
		Me.contextMenuStripEncoding.Name = "contextMenuStrip1"
		Me.contextMenuStripEncoding.OwnerItem = Me.ChangeEncodingToolStripMenuItem
		Me.contextMenuStripEncoding.ShowImageMargin = false
		Me.contextMenuStripEncoding.Size = New System.Drawing.Size(133, 26)
		AddHandler Me.contextMenuStripEncoding.ItemClicked, AddressOf Me.ContextMenuStripEncodingItemClicked
		'
		'encodingToolStripMenuItem
		'
		Me.encodingToolStripMenuItem.Enabled = false
		Me.encodingToolStripMenuItem.Name = "encodingToolStripMenuItem"
		Me.encodingToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.encodingToolStripMenuItem.Text = "select encoding"
		'
		'sourceToolStripMenuItem
		'
		Me.sourceToolStripMenuItem.DropDown = Me.contextMenuStripInfo
		Me.sourceToolStripMenuItem.Name = "sourceToolStripMenuItem"
		Me.sourceToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
		Me.sourceToolStripMenuItem.Text = "Source"
		AddHandler Me.sourceToolStripMenuItem.DropDownOpening, AddressOf Me.SourceToolStripMenuItemClick
		'
		'labelSize2
		'
		Me.labelSize2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.labelSize2.BackColor = System.Drawing.Color.Transparent
		Me.labelSize2.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.labelSize2.ForeColor = System.Drawing.Color.Orange
		Me.labelSize2.Location = New System.Drawing.Point(254, 1)
		Me.labelSize2.Name = "labelSize2"
		Me.labelSize2.Size = New System.Drawing.Size(85, 13)
		Me.labelSize2.TabIndex = 7
		Me.labelSize2.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.labelSize2.Visible = false
		'
		'pictureBox5
		'
		Me.pictureBox5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.pictureBox5.ContextMenuStrip = Me.contextMenuStripPinToSource
		Me.pictureBox5.Image = CType(resources.GetObject("pictureBox5.Image"),System.Drawing.Image)
		Me.pictureBox5.Location = New System.Drawing.Point(322, 23)
		Me.pictureBox5.Name = "pictureBox5"
		Me.pictureBox5.Size = New System.Drawing.Size(16, 16)
		Me.pictureBox5.TabIndex = 6
		Me.pictureBox5.TabStop = false
		Me.pictureBox5.Visible = false
		AddHandler Me.pictureBox5.Click, AddressOf Me.PictureBox5Click
		'
		'contextMenuStripPinToSource
		'
		Me.contextMenuStripPinToSource.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pinToOtherSideToolStripMenuItem})
		Me.contextMenuStripPinToSource.Name = "contextMenuStripPinToSource"
		Me.contextMenuStripPinToSource.ShowImageMargin = false
		Me.contextMenuStripPinToSource.Size = New System.Drawing.Size(136, 26)
		AddHandler Me.contextMenuStripPinToSource.Opening, AddressOf Me.ContextMenuStripPinToSourceOpening
		'
		'pinToOtherSideToolStripMenuItem
		'
		Me.pinToOtherSideToolStripMenuItem.Name = "pinToOtherSideToolStripMenuItem"
		Me.pinToOtherSideToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
		Me.pinToOtherSideToolStripMenuItem.Text = "Pin to other side"
		AddHandler Me.pinToOtherSideToolStripMenuItem.Click, AddressOf Me.PinToOtherSideToolStripMenuItemClick
		'
		'labelFileInfo2
		'
		Me.labelFileInfo2.AutoSize = true
		Me.labelFileInfo2.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.labelFileInfo2.ForeColor = System.Drawing.Color.DimGray
		Me.labelFileInfo2.Location = New System.Drawing.Point(3, 22)
		Me.labelFileInfo2.Name = "labelFileInfo2"
		Me.labelFileInfo2.Size = New System.Drawing.Size(0, 13)
		Me.labelFileInfo2.TabIndex = 3
		'
		'textBoxTitle2
		'
		Me.textBoxTitle2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBoxTitle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(254,Byte),Integer), CType(CType(254,Byte),Integer), CType(CType(254,Byte),Integer))
		Me.textBoxTitle2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.textBoxTitle2.ContextMenuStrip = Me.contextMenuStripInfo
		Me.textBoxTitle2.Font = New System.Drawing.Font("Tahoma", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World)
		Me.textBoxTitle2.Location = New System.Drawing.Point(3, 1)
		Me.textBoxTitle2.Name = "textBoxTitle2"
		Me.textBoxTitle2.ReadOnly = true
		Me.textBoxTitle2.Size = New System.Drawing.Size(243, 15)
		Me.textBoxTitle2.TabIndex = 4
		AddHandler Me.textBoxTitle2.DoubleClick, AddressOf Me.TextBoxTitle2DoubleClick
		'
		'labelEncoding2
		'
		Me.labelEncoding2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.labelEncoding2.BackColor = System.Drawing.Color.Transparent
		Me.labelEncoding2.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.labelEncoding2.ForeColor = System.Drawing.Color.Orange
		Me.labelEncoding2.Location = New System.Drawing.Point(254, 1)
		Me.labelEncoding2.Name = "labelEncoding2"
		Me.labelEncoding2.Size = New System.Drawing.Size(85, 13)
		Me.labelEncoding2.TabIndex = 2
		Me.labelEncoding2.TextAlign = System.Drawing.ContentAlignment.TopRight
		AddHandler Me.labelEncoding2.TextChanged, AddressOf Me.LabelEncoding2TextChanged
		AddHandler Me.labelEncoding2.DoubleClick, AddressOf Me.LabelEncoding2DoubleClick
		'
		'labelImgInfo2
		'
		Me.labelImgInfo2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.labelImgInfo2.BackColor = System.Drawing.Color.Transparent
		Me.labelImgInfo2.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.labelImgInfo2.ForeColor = System.Drawing.Color.Orange
		Me.labelImgInfo2.Location = New System.Drawing.Point(165, 1)
		Me.labelImgInfo2.Name = "labelImgInfo2"
		Me.labelImgInfo2.Size = New System.Drawing.Size(175, 14)
		Me.labelImgInfo2.TabIndex = 5
		Me.labelImgInfo2.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.labelImgInfo2.Visible = false
		'
		'targetToolStripMenuItem
		'
		Me.targetToolStripMenuItem.DropDown = Me.contextMenuStripInfo
		Me.targetToolStripMenuItem.Name = "targetToolStripMenuItem"
		Me.targetToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
		Me.targetToolStripMenuItem.Text = "Target"
		AddHandler Me.targetToolStripMenuItem.DropDownOpening, AddressOf Me.TargetToolStripMenuItemClick
		'
		'panelBrowser
		'
		Me.panelBrowser.Controls.Add(Me.splitContainer1)
		Me.panelBrowser.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panelBrowser.Location = New System.Drawing.Point(0, 0)
		Me.panelBrowser.Margin = New System.Windows.Forms.Padding(0)
		Me.panelBrowser.Name = "panelBrowser"
		Me.panelBrowser.Size = New System.Drawing.Size(704, 403)
		Me.panelBrowser.TabIndex = 0
		'
		'splitContainer1
		'
		Me.splitContainer1.BackColor = System.Drawing.SystemColors.Control
		Me.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.splitContainer1.ContextMenuStrip = Me.contextMenuStripCompare
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.IsSplitterFixed = true
		Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.panel1)
		Me.splitContainer1.Panel1MinSize = 1
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.panel2)
		Me.splitContainer1.Panel2MinSize = 1
		Me.splitContainer1.Size = New System.Drawing.Size(704, 403)
		Me.splitContainer1.SplitterDistance = 347
		Me.splitContainer1.SplitterWidth = 10
		Me.splitContainer1.TabIndex = 2
		Me.splitContainer1.TabStop = false
		AddHandler Me.splitContainer1.SplitterMoved, AddressOf Me.SplitContainer1SplitterMoved
		'
		'contextMenuStripCompare
		'
		Me.contextMenuStripCompare.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.openFilesInCompareToolToolStripMenuItem, Me.openFoldersInCompareToolToolStripMenuItem, Me.toolStripSeparator12, Me.originalSizetoolStripMenuItem, Me.fitImagesToolStripMenuItem, Me.splitViewToolStripMenuItem, Me.backgroundColorToolStripMenuItem, Me.toolStripSeparator19, Me.toolStripMenuItem1})
		Me.contextMenuStripCompare.Name = "contextMenuStripCompare"
		Me.contextMenuStripCompare.ShowImageMargin = false
		Me.contextMenuStripCompare.Size = New System.Drawing.Size(205, 170)
		'
		'openFilesInCompareToolToolStripMenuItem
		'
		Me.openFilesInCompareToolToolStripMenuItem.Name = "openFilesInCompareToolToolStripMenuItem"
		Me.openFilesInCompareToolToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
		Me.openFilesInCompareToolToolStripMenuItem.Text = "Open files in compare tool"
		AddHandler Me.openFilesInCompareToolToolStripMenuItem.Click, AddressOf Me.OpenFilesInCompareToolToolStripMenuItemClick
		'
		'openFoldersInCompareToolToolStripMenuItem
		'
		Me.openFoldersInCompareToolToolStripMenuItem.Name = "openFoldersInCompareToolToolStripMenuItem"
		Me.openFoldersInCompareToolToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
		Me.openFoldersInCompareToolToolStripMenuItem.Text = "Open folders in compare tool"
		Me.openFoldersInCompareToolToolStripMenuItem.Visible = false
		AddHandler Me.openFoldersInCompareToolToolStripMenuItem.Click, AddressOf Me.OpenFoldersInCompareToolToolStripMenuItemClick
		'
		'toolStripSeparator12
		'
		Me.toolStripSeparator12.Name = "toolStripSeparator12"
		Me.toolStripSeparator12.Size = New System.Drawing.Size(201, 6)
		Me.toolStripSeparator12.Visible = false
		'
		'originalSizetoolStripMenuItem
		'
		Me.originalSizetoolStripMenuItem.Name = "originalSizetoolStripMenuItem"
		Me.originalSizetoolStripMenuItem.Size = New System.Drawing.Size(204, 22)
		Me.originalSizetoolStripMenuItem.Text = "100%"
		Me.originalSizetoolStripMenuItem.Visible = false
		AddHandler Me.originalSizetoolStripMenuItem.Click, AddressOf Me.OriginalSizetoolStripMenuItemClick
		'
		'fitImagesToolStripMenuItem
		'
		Me.fitImagesToolStripMenuItem.Name = "fitImagesToolStripMenuItem"
		Me.fitImagesToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
		Me.fitImagesToolStripMenuItem.Text = "Fit visible"
		Me.fitImagesToolStripMenuItem.Visible = false
		AddHandler Me.fitImagesToolStripMenuItem.Click, AddressOf Me.FitImagesToolStripMenuItemClick
		'
		'splitViewToolStripMenuItem
		'
		Me.splitViewToolStripMenuItem.Name = "splitViewToolStripMenuItem"
		Me.splitViewToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
		Me.splitViewToolStripMenuItem.Text = "50/50 split"
		Me.splitViewToolStripMenuItem.Visible = false
		AddHandler Me.splitViewToolStripMenuItem.Click, AddressOf Me.SplitViewToolStripMenuItemClick
		'
		'backgroundColorToolStripMenuItem
		'
		Me.backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem"
		Me.backgroundColorToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
		Me.backgroundColorToolStripMenuItem.Text = "Background color"
		Me.backgroundColorToolStripMenuItem.Visible = false
		AddHandler Me.backgroundColorToolStripMenuItem.Click, AddressOf Me.BackgroundColorToolStripMenuItemClick
		'
		'toolStripSeparator19
		'
		Me.toolStripSeparator19.Name = "toolStripSeparator19"
		Me.toolStripSeparator19.Size = New System.Drawing.Size(201, 6)
		'
		'toolStripMenuItem1
		'
		Me.toolStripMenuItem1.CheckOnClick = true
		Me.toolStripMenuItem1.Image = CType(resources.GetObject("toolStripMenuItem1.Image"),System.Drawing.Image)
		Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
		Me.toolStripMenuItem1.Size = New System.Drawing.Size(204, 22)
		Me.toolStripMenuItem1.Text = "Annotate"
		AddHandler Me.toolStripMenuItem1.Click, AddressOf Me.AnnotateToolStripMenuItemClick
		'
		'panel1
		'
		Me.panel1.AutoScroll = true
		Me.panel1.BackColor = System.Drawing.SystemColors.Control
		Me.panel1.Controls.Add(Me.panel1Contents)
		Me.panel1.Controls.Add(Me.panel1Info)
		Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel1.Location = New System.Drawing.Point(0, 0)
		Me.panel1.Margin = New System.Windows.Forms.Padding(0)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(343, 399)
		Me.panel1.TabIndex = 0
		AddHandler Me.panel1.Scroll, AddressOf Me.Panel1Scroll
		'
		'panel1Contents
		'
		Me.panel1Contents.Controls.Add(Me.pictureBox1)
		Me.panel1Contents.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel1Contents.Location = New System.Drawing.Point(0, 44)
		Me.panel1Contents.Margin = New System.Windows.Forms.Padding(0)
		Me.panel1Contents.Name = "panel1Contents"
		Me.panel1Contents.Size = New System.Drawing.Size(343, 355)
		Me.panel1Contents.TabIndex = 2
		'
		'pictureBox1
		'
		Me.pictureBox1.Location = New System.Drawing.Point(0, 0)
		Me.pictureBox1.Margin = New System.Windows.Forms.Padding(0)
		Me.pictureBox1.Name = "pictureBox1"
		Me.pictureBox1.Size = New System.Drawing.Size(200, 300)
		Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
		Me.pictureBox1.TabIndex = 1
		Me.pictureBox1.TabStop = false
		Me.pictureBox1.Visible = false
		AddHandler Me.pictureBox1.MouseDown, AddressOf Me.PictureBox1MouseDown
		AddHandler Me.pictureBox1.MouseMove, AddressOf Me.PictureBox1MouseMove
		'
		'panel1Info
		'
		Me.panel1Info.BackColor = System.Drawing.SystemColors.Control
		Me.panel1Info.Controls.Add(Me.panelSourceInfo)
		Me.panel1Info.Dock = System.Windows.Forms.DockStyle.Top
		Me.panel1Info.Location = New System.Drawing.Point(0, 0)
		Me.panel1Info.Name = "panel1Info"
		Me.panel1Info.Size = New System.Drawing.Size(343, 44)
		Me.panel1Info.TabIndex = 3
		Me.panel1Info.Visible = false
		'
		'panelSourceInfo
		'
		Me.panelSourceInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(254,Byte),Integer), CType(CType(254,Byte),Integer), CType(CType(254,Byte),Integer))
		Me.panelSourceInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.panelSourceInfo.ContextMenuStrip = Me.contextMenuStripInfo
		Me.panelSourceInfo.Controls.Add(Me.labelSize1)
		Me.panelSourceInfo.Controls.Add(Me.pictureBox4)
		Me.panelSourceInfo.Controls.Add(Me.labelFileInfo1)
		Me.panelSourceInfo.Controls.Add(Me.textBoxTitle1)
		Me.panelSourceInfo.Controls.Add(Me.labelEncoding1)
		Me.panelSourceInfo.Controls.Add(Me.labelImgInfo1)
		Me.panelSourceInfo.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panelSourceInfo.Location = New System.Drawing.Point(0, 0)
		Me.panelSourceInfo.Name = "panelSourceInfo"
		Me.panelSourceInfo.Size = New System.Drawing.Size(343, 44)
		Me.panelSourceInfo.TabIndex = 3
		Me.panelSourceInfo.Visible = false
		'
		'labelSize1
		'
		Me.labelSize1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.labelSize1.BackColor = System.Drawing.Color.Transparent
		Me.labelSize1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.labelSize1.ForeColor = System.Drawing.Color.Orange
		Me.labelSize1.Location = New System.Drawing.Point(255, 1)
		Me.labelSize1.Name = "labelSize1"
		Me.labelSize1.Size = New System.Drawing.Size(85, 13)
		Me.labelSize1.TabIndex = 6
		Me.labelSize1.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.labelSize1.Visible = false
		'
		'pictureBox4
		'
		Me.pictureBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.pictureBox4.ContextMenuStrip = Me.contextMenuStripPinToTarget
		Me.pictureBox4.Image = CType(resources.GetObject("pictureBox4.Image"),System.Drawing.Image)
		Me.pictureBox4.Location = New System.Drawing.Point(322, 23)
		Me.pictureBox4.Name = "pictureBox4"
		Me.pictureBox4.Size = New System.Drawing.Size(16, 16)
		Me.pictureBox4.TabIndex = 5
		Me.pictureBox4.TabStop = false
		Me.pictureBox4.Tag = ""
		Me.pictureBox4.Visible = false
		AddHandler Me.pictureBox4.Click, AddressOf Me.PictureBox4Click
		'
		'contextMenuStripPinToTarget
		'
		Me.contextMenuStripPinToTarget.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItem3})
		Me.contextMenuStripPinToTarget.Name = "contextMenuStripPinToSource"
		Me.contextMenuStripPinToTarget.ShowImageMargin = false
		Me.contextMenuStripPinToTarget.Size = New System.Drawing.Size(136, 26)
		AddHandler Me.contextMenuStripPinToTarget.Opening, AddressOf Me.ContextMenuStripPinToTargetOpening
		'
		'toolStripMenuItem3
		'
		Me.toolStripMenuItem3.Name = "toolStripMenuItem3"
		Me.toolStripMenuItem3.Size = New System.Drawing.Size(135, 22)
		Me.toolStripMenuItem3.Text = "Pin to other side"
		AddHandler Me.toolStripMenuItem3.Click, AddressOf Me.ToolStripMenuItem3Click
		'
		'labelFileInfo1
		'
		Me.labelFileInfo1.AutoSize = true
		Me.labelFileInfo1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.labelFileInfo1.ForeColor = System.Drawing.Color.DimGray
		Me.labelFileInfo1.Location = New System.Drawing.Point(3, 22)
		Me.labelFileInfo1.Name = "labelFileInfo1"
		Me.labelFileInfo1.Size = New System.Drawing.Size(0, 13)
		Me.labelFileInfo1.TabIndex = 2
		'
		'textBoxTitle1
		'
		Me.textBoxTitle1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBoxTitle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(254,Byte),Integer), CType(CType(254,Byte),Integer), CType(CType(254,Byte),Integer))
		Me.textBoxTitle1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.textBoxTitle1.ContextMenuStrip = Me.contextMenuStripInfo
		Me.textBoxTitle1.Font = New System.Drawing.Font("Tahoma", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World)
		Me.textBoxTitle1.Location = New System.Drawing.Point(4, 1)
		Me.textBoxTitle1.Name = "textBoxTitle1"
		Me.textBoxTitle1.ReadOnly = true
		Me.textBoxTitle1.Size = New System.Drawing.Size(245, 15)
		Me.textBoxTitle1.TabIndex = 3
		AddHandler Me.textBoxTitle1.DoubleClick, AddressOf Me.TextBoxTitle1DoubleClick
		'
		'labelEncoding1
		'
		Me.labelEncoding1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.labelEncoding1.BackColor = System.Drawing.Color.Transparent
		Me.labelEncoding1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.labelEncoding1.ForeColor = System.Drawing.Color.Orange
		Me.labelEncoding1.Location = New System.Drawing.Point(255, 1)
		Me.labelEncoding1.Name = "labelEncoding1"
		Me.labelEncoding1.Size = New System.Drawing.Size(85, 13)
		Me.labelEncoding1.TabIndex = 1
		Me.labelEncoding1.TextAlign = System.Drawing.ContentAlignment.TopRight
		AddHandler Me.labelEncoding1.TextChanged, AddressOf Me.LabelEncoding1TextChanged
		AddHandler Me.labelEncoding1.DoubleClick, AddressOf Me.LabelEncoding1DoubleClick
		'
		'labelImgInfo1
		'
		Me.labelImgInfo1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.labelImgInfo1.BackColor = System.Drawing.Color.Transparent
		Me.labelImgInfo1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.labelImgInfo1.ForeColor = System.Drawing.Color.Orange
		Me.labelImgInfo1.Location = New System.Drawing.Point(164, 1)
		Me.labelImgInfo1.Name = "labelImgInfo1"
		Me.labelImgInfo1.Size = New System.Drawing.Size(175, 14)
		Me.labelImgInfo1.TabIndex = 4
		Me.labelImgInfo1.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.labelImgInfo1.Visible = false
		AddHandler Me.labelImgInfo1.DoubleClick, AddressOf Me.LabelImgInfo1DoubleClick
		'
		'panelContents
		'
		Me.panelContents.Controls.Add(Me.toolStripContainerMainForm)
		Me.panelContents.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panelContents.Location = New System.Drawing.Point(0, 24)
		Me.panelContents.Name = "panelContents"
		Me.panelContents.Size = New System.Drawing.Size(704, 403)
		Me.panelContents.TabIndex = 6
		'
		'toolStripContainerMainForm
		'
		Me.toolStripContainerMainForm.BottomToolStripPanelVisible = false
		'
		'toolStripContainerMainForm.ContentPanel
		'
		Me.toolStripContainerMainForm.ContentPanel.Controls.Add(Me.splitContainerContents)
		Me.toolStripContainerMainForm.ContentPanel.Size = New System.Drawing.Size(704, 403)
		Me.toolStripContainerMainForm.Dock = System.Windows.Forms.DockStyle.Fill
		Me.toolStripContainerMainForm.LeftToolStripPanelVisible = false
		Me.toolStripContainerMainForm.Location = New System.Drawing.Point(0, 0)
		Me.toolStripContainerMainForm.Name = "toolStripContainerMainForm"
		Me.toolStripContainerMainForm.RightToolStripPanelVisible = false
		Me.toolStripContainerMainForm.Size = New System.Drawing.Size(704, 403)
		Me.toolStripContainerMainForm.TabIndex = 3
		Me.toolStripContainerMainForm.TopToolStripPanelVisible = false
		'
		'splitContainerContents
		'
		Me.splitContainerContents.ContextMenuStrip = Me.contextMenuStripFileList
		Me.splitContainerContents.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainerContents.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainerContents.Location = New System.Drawing.Point(0, 0)
		Me.splitContainerContents.Name = "splitContainerContents"
		'
		'splitContainerContents.Panel1
		'
		Me.splitContainerContents.Panel1.Controls.Add(Me.tabControl1)
		Me.splitContainerContents.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.splitContainerContents.Panel1Collapsed = true
		Me.splitContainerContents.Panel1MinSize = 2
		'
		'splitContainerContents.Panel2
		'
		Me.splitContainerContents.Panel2.Controls.Add(Me.panelBrowser)
		Me.splitContainerContents.Size = New System.Drawing.Size(704, 403)
		Me.splitContainerContents.SplitterDistance = 230
		Me.splitContainerContents.SplitterWidth = 2
		Me.splitContainerContents.TabIndex = 0
		AddHandler Me.splitContainerContents.MouseDoubleClick, AddressOf Me.SplitContainerContentsMouseDoubleClick
		'
		'contextMenuStripFileList
		'
		Me.contextMenuStripFileList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.dockToTopToolStripMenuItem, Me.collapseToolStripMenuItem})
		Me.contextMenuStripFileList.Name = "contextMenuStripFileList"
		Me.contextMenuStripFileList.ShowImageMargin = false
		Me.contextMenuStripFileList.Size = New System.Drawing.Size(112, 48)
		'
		'dockToTopToolStripMenuItem
		'
		Me.dockToTopToolStripMenuItem.Name = "dockToTopToolStripMenuItem"
		Me.dockToTopToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
		Me.dockToTopToolStripMenuItem.Text = "Dock to top"
		AddHandler Me.dockToTopToolStripMenuItem.Click, AddressOf Me.DockToTopToolStripMenuItemClick
		'
		'collapseToolStripMenuItem
		'
		Me.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem"
		Me.collapseToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
		Me.collapseToolStripMenuItem.Text = "Collapse"
		AddHandler Me.collapseToolStripMenuItem.Click, AddressOf Me.CollapseToolStripMenuItemClick
		'
		'tabControl1
		'
		Me.tabControl1.Controls.Add(Me.tabPage1)
		Me.tabControl1.Controls.Add(Me.tabPage2)
		Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tabControl1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.tabControl1.HotTrack = true
		Me.tabControl1.Location = New System.Drawing.Point(3, 0)
		Me.tabControl1.Multiline = true
		Me.tabControl1.Name = "tabControl1"
		Me.tabControl1.SelectedIndex = 0
		Me.tabControl1.Size = New System.Drawing.Size(227, 100)
		Me.tabControl1.TabIndex = 1
		AddHandler Me.tabControl1.SizeChanged, AddressOf Me.TabControl1SizeChanged
		'
		'tabPage1
		'
		Me.tabPage1.Controls.Add(Me.treeView1)
		Me.tabPage1.Controls.Add(Me.toolStrip1)
		Me.tabPage1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.tabPage1.Location = New System.Drawing.Point(4, 22)
		Me.tabPage1.Name = "tabPage1"
		Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.tabPage1.Size = New System.Drawing.Size(219, 74)
		Me.tabPage1.TabIndex = 0
		Me.tabPage1.Text = "File List"
		Me.tabPage1.UseVisualStyleBackColor = true
		'
		'treeView1
		'
		Me.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.treeView1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.treeView1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.treeView1.HideSelection = false
		Me.treeView1.ImageIndex = 1
		Me.treeView1.ImageList = Me.imageListSmallIcons
		Me.treeView1.Location = New System.Drawing.Point(3, 3)
		Me.treeView1.Name = "treeView1"
		Me.treeView1.SelectedImageIndex = 1
		Me.treeView1.ShowPlusMinus = false
		Me.treeView1.Size = New System.Drawing.Size(213, 68)
		Me.treeView1.TabIndex = 0
		AddHandler Me.treeView1.AfterCollapse, AddressOf Me.TreeView1AfterCollapse
		AddHandler Me.treeView1.AfterExpand, AddressOf Me.TreeView1AfterExpand
		AddHandler Me.treeView1.AfterSelect, AddressOf Me.TreeView1AfterSelect
		AddHandler Me.treeView1.NodeMouseClick, AddressOf Me.TreeView1NodeMouseClick
		AddHandler Me.treeView1.NodeMouseDoubleClick, AddressOf Me.TreeView1NodeMouseDoubleClick
		'
		'imageListSmallIcons
		'
		Me.imageListSmallIcons.ImageStream = CType(resources.GetObject("imageListSmallIcons.ImageStream"),System.Windows.Forms.ImageListStreamer)
		Me.imageListSmallIcons.TransparentColor = System.Drawing.Color.Transparent
		Me.imageListSmallIcons.Images.SetKeyName(0, "folder_closed.png")
		Me.imageListSmallIcons.Images.SetKeyName(1, "folder_open.png")
		Me.imageListSmallIcons.Images.SetKeyName(2, "files.png")
		Me.imageListSmallIcons.Images.SetKeyName(3, "files_unchecked.png")
		Me.imageListSmallIcons.Images.SetKeyName(4, "files_problem.png")
		Me.imageListSmallIcons.Images.SetKeyName(5, "files_identical.png")
		Me.imageListSmallIcons.Images.SetKeyName(6, "files_missing.png")
		Me.imageListSmallIcons.Images.SetKeyName(7, "files_orphan.png")
		Me.imageListSmallIcons.Images.SetKeyName(8, "file.png")
		Me.imageListSmallIcons.Images.SetKeyName(9, "file_edit.png")
		Me.imageListSmallIcons.Images.SetKeyName(10, "file_flagged.png")
		Me.imageListSmallIcons.Images.SetKeyName(11, "file_NG.png")
		Me.imageListSmallIcons.Images.SetKeyName(12, "file_OK.png")
		Me.imageListSmallIcons.Images.SetKeyName(13, "selected.png")
		'
		'toolStrip1
		'
		Me.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.toolStrip1.Location = New System.Drawing.Point(3, 3)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(213, 25)
		Me.toolStrip1.TabIndex = 1
		Me.toolStrip1.Text = "toolStrip1"
		Me.toolStrip1.Visible = false
		'
		'tabPage2
		'
		Me.tabPage2.Controls.Add(Me.webBrowserLog)
		Me.tabPage2.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.tabPage2.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.tabPage2.Location = New System.Drawing.Point(4, 22)
		Me.tabPage2.Name = "tabPage2"
		Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.tabPage2.Size = New System.Drawing.Size(219, 74)
		Me.tabPage2.TabIndex = 1
		Me.tabPage2.Text = "Message Log"
		Me.tabPage2.UseVisualStyleBackColor = true
		'
		'webBrowserLog
		'
		Me.webBrowserLog.ContextMenuStrip = Me.contextMenuLog
		Me.webBrowserLog.Dock = System.Windows.Forms.DockStyle.Fill
		Me.webBrowserLog.IsWebBrowserContextMenuEnabled = false
		Me.webBrowserLog.Location = New System.Drawing.Point(3, 3)
		Me.webBrowserLog.MinimumSize = New System.Drawing.Size(20, 20)
		Me.webBrowserLog.Name = "webBrowserLog"
		Me.webBrowserLog.Size = New System.Drawing.Size(213, 68)
		Me.webBrowserLog.TabIndex = 0
		'
		'contextMenuLog
		'
		Me.contextMenuLog.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.saveLogToolStripMenuItem1, Me.clearLogToolStripMenuItem})
		Me.contextMenuLog.Name = "contextMenuLog"
		Me.contextMenuLog.ShowImageMargin = false
		Me.contextMenuLog.Size = New System.Drawing.Size(97, 48)
		'
		'saveLogToolStripMenuItem1
		'
		Me.saveLogToolStripMenuItem1.Name = "saveLogToolStripMenuItem1"
		Me.saveLogToolStripMenuItem1.Size = New System.Drawing.Size(96, 22)
		Me.saveLogToolStripMenuItem1.Text = "Save log"
		AddHandler Me.saveLogToolStripMenuItem1.Click, AddressOf Me.SaveLogToolStripMenuItemClick
		'
		'clearLogToolStripMenuItem
		'
		Me.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem"
		Me.clearLogToolStripMenuItem.Size = New System.Drawing.Size(96, 22)
		Me.clearLogToolStripMenuItem.Text = "Clear log"
		AddHandler Me.clearLogToolStripMenuItem.Click, AddressOf Me.ClearLogToolStripMenuItem1Click
		'
		'button2
		'
		Me.button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.button2.BackColor = System.Drawing.SystemColors.Control
		Me.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.button2.Cursor = System.Windows.Forms.Cursors.Default
		Me.button2.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.button2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.button2.Location = New System.Drawing.Point(122, 3)
		Me.button2.Name = "button2"
		Me.button2.Size = New System.Drawing.Size(50, 26)
		Me.button2.TabIndex = 6
		Me.button2.Text = "Search"
		Me.button2.UseVisualStyleBackColor = false
		Me.button2.Visible = false
		AddHandler Me.button2.Click, AddressOf Me.search1Click
		'
		'searchPanel
		'
		Me.searchPanel.Controls.Add(Me.button2)
		Me.searchPanel.Controls.Add(Me.textBox1)
		Me.searchPanel.Location = New System.Drawing.Point(64, 4)
		Me.searchPanel.Name = "searchPanel"
		Me.searchPanel.Size = New System.Drawing.Size(177, 30)
		Me.searchPanel.TabIndex = 13
		Me.searchPanel.Visible = false
		'
		'textBox1
		'
		Me.textBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox1.BackColor = System.Drawing.SystemColors.Window
		Me.textBox1.ContextMenuStrip = Me.contextMenuStripSearch
		Me.textBox1.Location = New System.Drawing.Point(3, 5)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(169, 21)
		Me.textBox1.TabIndex = 13
		AddHandler Me.textBox1.KeyPress, AddressOf Me.TextBox1KeyPress
		'
		'contextMenuStripSearch
		'
		Me.contextMenuStripSearch.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.clearSearchToolStripMenuItem, Me.toolStripSeparator22, Me.saveSearchToolStripMenuItem, Me.readSavedSearchToolStripMenuItem, Me.deleteSavedSearchToolStripMenuItem, Me.toolStripSeparator23, Me.searchColorToolStripMenuItem1})
		Me.contextMenuStripSearch.Name = "contextMenuStripSearch"
		Me.contextMenuStripSearch.Size = New System.Drawing.Size(178, 126)
		'
		'clearSearchToolStripMenuItem
		'
		Me.clearSearchToolStripMenuItem.Name = "clearSearchToolStripMenuItem"
		Me.clearSearchToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.clearSearchToolStripMenuItem.Text = "Clear search"
		AddHandler Me.clearSearchToolStripMenuItem.Click, AddressOf Me.ClearSearchToolStripMenuItemClick
		'
		'toolStripSeparator22
		'
		Me.toolStripSeparator22.Name = "toolStripSeparator22"
		Me.toolStripSeparator22.Size = New System.Drawing.Size(174, 6)
		'
		'saveSearchToolStripMenuItem
		'
		Me.saveSearchToolStripMenuItem.Name = "saveSearchToolStripMenuItem"
		Me.saveSearchToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.saveSearchToolStripMenuItem.Text = "Save search"
		AddHandler Me.saveSearchToolStripMenuItem.Click, AddressOf Me.SaveSearchToolStripMenuItemClick
		'
		'readSavedSearchToolStripMenuItem
		'
		Me.readSavedSearchToolStripMenuItem.Name = "readSavedSearchToolStripMenuItem"
		Me.readSavedSearchToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.readSavedSearchToolStripMenuItem.Text = "Read saved search"
		Me.readSavedSearchToolStripMenuItem.Visible = false
		AddHandler Me.readSavedSearchToolStripMenuItem.Click, AddressOf Me.ReadSavedSearchToolStripMenuItemClick
		'
		'deleteSavedSearchToolStripMenuItem
		'
		Me.deleteSavedSearchToolStripMenuItem.Name = "deleteSavedSearchToolStripMenuItem"
		Me.deleteSavedSearchToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
		Me.deleteSavedSearchToolStripMenuItem.Text = "Delete saved search"
		AddHandler Me.deleteSavedSearchToolStripMenuItem.Click, AddressOf Me.DeleteSavedSearchToolStripMenuItemClick
		'
		'toolStripSeparator23
		'
		Me.toolStripSeparator23.Name = "toolStripSeparator23"
		Me.toolStripSeparator23.Size = New System.Drawing.Size(174, 6)
		'
		'searchColorToolStripMenuItem1
		'
		Me.searchColorToolStripMenuItem1.DropDown = Me.contextMenuStripSearchColor
		Me.searchColorToolStripMenuItem1.Name = "searchColorToolStripMenuItem1"
		Me.searchColorToolStripMenuItem1.Size = New System.Drawing.Size(177, 22)
		Me.searchColorToolStripMenuItem1.Text = "Search color"
		'
		'contextMenuStripSearchColor
		'
		Me.contextMenuStripSearchColor.BackColor = System.Drawing.SystemColors.Control
		Me.contextMenuStripSearchColor.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.magentaToolStripMenuItem, Me.yellowToolStripMenuItem, Me.cyanToolStripMenuItem, Me.limeToolStripMenuItem})
		Me.contextMenuStripSearchColor.Name = "contextMenuStripSearchColor"
		Me.contextMenuStripSearchColor.OwnerItem = Me.searchColorToolStripMenuItem
		Me.contextMenuStripSearchColor.Size = New System.Drawing.Size(122, 92)
		AddHandler Me.contextMenuStripSearchColor.ItemClicked, AddressOf Me.ContextMenuStripSearchColorItemClicked
		'
		'magentaToolStripMenuItem
		'
		Me.magentaToolStripMenuItem.BackColor = System.Drawing.Color.Magenta
		Me.magentaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText
		Me.magentaToolStripMenuItem.Name = "magentaToolStripMenuItem"
		Me.magentaToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
		Me.magentaToolStripMenuItem.Text = "Magenta"
		'
		'yellowToolStripMenuItem
		'
		Me.yellowToolStripMenuItem.BackColor = System.Drawing.Color.Yellow
		Me.yellowToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText
		Me.yellowToolStripMenuItem.Name = "yellowToolStripMenuItem"
		Me.yellowToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
		Me.yellowToolStripMenuItem.Text = "Yellow"
		'
		'cyanToolStripMenuItem
		'
		Me.cyanToolStripMenuItem.BackColor = System.Drawing.Color.Cyan
		Me.cyanToolStripMenuItem.Image = CType(resources.GetObject("cyanToolStripMenuItem.Image"),System.Drawing.Image)
		Me.cyanToolStripMenuItem.Name = "cyanToolStripMenuItem"
		Me.cyanToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
		Me.cyanToolStripMenuItem.Text = "Cyan"
		'
		'limeToolStripMenuItem
		'
		Me.limeToolStripMenuItem.BackColor = System.Drawing.Color.Lime
		Me.limeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText
		Me.limeToolStripMenuItem.Name = "limeToolStripMenuItem"
		Me.limeToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
		Me.limeToolStripMenuItem.Text = "Lime"
		'
		'searchColorToolStripMenuItem
		'
		Me.searchColorToolStripMenuItem.DropDown = Me.contextMenuStripSearchColor
		Me.searchColorToolStripMenuItem.Name = "searchColorToolStripMenuItem"
		Me.searchColorToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.searchColorToolStripMenuItem.Text = "Search color"
		'
		'contextMenuStripTreeFilter
		'
		Me.contextMenuStripTreeFilter.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.activateFilterToolStripMenuItem, Me.clearFilterShowAllToolStripMenuItem, Me.toolStripSeparator15, Me.filterChecked, Me.filterUnchecked, Me.filterOk, Me.filterNg, Me.filterOrphans, Me.filterProblem, Me.toolStripSeparator20, Me.filterComments})
		Me.contextMenuStripTreeFilter.Name = "contextMenuStripTreeFilter"
		Me.contextMenuStripTreeFilter.OwnerItem = Me.ProjectFilterToolStripMenuItem
		Me.contextMenuStripTreeFilter.Size = New System.Drawing.Size(196, 214)
		AddHandler Me.contextMenuStripTreeFilter.Closing, AddressOf Me.ContextMenuStripTreeFilterClosing
		AddHandler Me.contextMenuStripTreeFilter.ItemClicked, AddressOf Me.ContextMenuStripTreeFilterItemClicked
		'
		'activateFilterToolStripMenuItem
		'
		Me.activateFilterToolStripMenuItem.Image = CType(resources.GetObject("activateFilterToolStripMenuItem.Image"),System.Drawing.Image)
		Me.activateFilterToolStripMenuItem.Name = "activateFilterToolStripMenuItem"
		Me.activateFilterToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
		Me.activateFilterToolStripMenuItem.Text = "Activate Filter"
		AddHandler Me.activateFilterToolStripMenuItem.Click, AddressOf Me.ActivateFilterToolStripMenuItemClick
		'
		'clearFilterShowAllToolStripMenuItem
		'
		Me.clearFilterShowAllToolStripMenuItem.Image = CType(resources.GetObject("clearFilterShowAllToolStripMenuItem.Image"),System.Drawing.Image)
		Me.clearFilterShowAllToolStripMenuItem.Name = "clearFilterShowAllToolStripMenuItem"
		Me.clearFilterShowAllToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
		Me.clearFilterShowAllToolStripMenuItem.Text = "Reset Filter (Show All)"
		AddHandler Me.clearFilterShowAllToolStripMenuItem.Click, AddressOf Me.ClearFilterShowAllToolStripMenuItemClick
		'
		'toolStripSeparator15
		'
		Me.toolStripSeparator15.Name = "toolStripSeparator15"
		Me.toolStripSeparator15.Size = New System.Drawing.Size(192, 6)
		'
		'filterChecked
		'
		Me.filterChecked.Checked = true
		Me.filterChecked.CheckOnClick = true
		Me.filterChecked.CheckState = System.Windows.Forms.CheckState.Checked
		Me.filterChecked.Name = "filterChecked"
		Me.filterChecked.Size = New System.Drawing.Size(195, 22)
		Me.filterChecked.Text = "Checked"
		AddHandler Me.filterChecked.Click, AddressOf Me.filterClicked
		'
		'filterUnchecked
		'
		Me.filterUnchecked.Checked = true
		Me.filterUnchecked.CheckOnClick = true
		Me.filterUnchecked.CheckState = System.Windows.Forms.CheckState.Checked
		Me.filterUnchecked.Name = "filterUnchecked"
		Me.filterUnchecked.Size = New System.Drawing.Size(195, 22)
		Me.filterUnchecked.Text = "Unchecked"
		AddHandler Me.filterUnchecked.Click, AddressOf Me.filterClicked
		'
		'filterOk
		'
		Me.filterOk.Checked = true
		Me.filterOk.CheckOnClick = true
		Me.filterOk.CheckState = System.Windows.Forms.CheckState.Checked
		Me.filterOk.Name = "filterOk"
		Me.filterOk.Size = New System.Drawing.Size(195, 22)
		Me.filterOk.Text = "OK"
		AddHandler Me.filterOk.Click, AddressOf Me.filterClicked
		'
		'filterNg
		'
		Me.filterNg.Checked = true
		Me.filterNg.CheckOnClick = true
		Me.filterNg.CheckState = System.Windows.Forms.CheckState.Checked
		Me.filterNg.Name = "filterNg"
		Me.filterNg.Size = New System.Drawing.Size(195, 22)
		Me.filterNg.Text = "NG"
		AddHandler Me.filterNg.Click, AddressOf Me.filterClicked
		'
		'filterOrphans
		'
		Me.filterOrphans.Checked = true
		Me.filterOrphans.CheckOnClick = true
		Me.filterOrphans.CheckState = System.Windows.Forms.CheckState.Checked
		Me.filterOrphans.Name = "filterOrphans"
		Me.filterOrphans.Size = New System.Drawing.Size(195, 22)
		Me.filterOrphans.Text = "Orphans"
		AddHandler Me.filterOrphans.Click, AddressOf Me.filterClicked
		'
		'filterProblem
		'
		Me.filterProblem.Checked = true
		Me.filterProblem.CheckOnClick = true
		Me.filterProblem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.filterProblem.Name = "filterProblem"
		Me.filterProblem.Size = New System.Drawing.Size(195, 22)
		Me.filterProblem.Text = "Problem"
		AddHandler Me.filterProblem.Click, AddressOf Me.filterClicked
		'
		'toolStripSeparator20
		'
		Me.toolStripSeparator20.Name = "toolStripSeparator20"
		Me.toolStripSeparator20.Size = New System.Drawing.Size(192, 6)
		'
		'filterComments
		'
		Me.filterComments.CheckOnClick = true
		Me.filterComments.Name = "filterComments"
		Me.filterComments.Size = New System.Drawing.Size(195, 22)
		Me.filterComments.Text = "Commented Files Only"
		AddHandler Me.filterComments.CheckedChanged, AddressOf Me.FilterCommentsCheckedChanged
		AddHandler Me.filterComments.Click, AddressOf Me.filterClicked
		'
		'ProjectFilterToolStripMenuItem
		'
		Me.ProjectFilterToolStripMenuItem.DropDown = Me.contextMenuStripTreeFilter
		Me.ProjectFilterToolStripMenuItem.Name = "ProjectFilterToolStripMenuItem"
		Me.ProjectFilterToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
		Me.ProjectFilterToolStripMenuItem.Text = "Filter"
		Me.ProjectFilterToolStripMenuItem.Visible = false
		'
		'contextMenuStripFilter
		'
		Me.contextMenuStripFilter.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.noOrphansToolStripMenuItem})
		Me.contextMenuStripFilter.Name = "contextMenuStripFilter"
		Me.contextMenuStripFilter.Size = New System.Drawing.Size(145, 26)
		'
		'noOrphansToolStripMenuItem
		'
		Me.noOrphansToolStripMenuItem.Name = "noOrphansToolStripMenuItem"
		Me.noOrphansToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
		Me.noOrphansToolStripMenuItem.Text = "Skip Orphans"
		AddHandler Me.noOrphansToolStripMenuItem.Click, AddressOf Me.NoOrphansToolStripMenuItemClick
		'
		'panel3
		'
		Me.panel3.Controls.Add(Me.panel5)
		Me.panel3.Controls.Add(Me.searchPanel)
		Me.panel3.Controls.Add(Me.textBoxJump)
		Me.panel3.Controls.Add(Me.labelCounter)
		Me.panel3.Controls.Add(Me.buttonNextOff)
		Me.panel3.Controls.Add(Me.buttonNext)
		Me.panel3.Controls.Add(Me.buttonPreviousOff)
		Me.panel3.Controls.Add(Me.buttonPrevious)
		Me.panel3.Controls.Add(Me.panel4)
		Me.panel3.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.panel3.Location = New System.Drawing.Point(0, 427)
		Me.panel3.Name = "panel3"
		Me.panel3.Size = New System.Drawing.Size(704, 41)
		Me.panel3.TabIndex = 5
		'
		'panel5
		'
		Me.panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer))
		Me.panel5.Controls.Add(Me.pictureBox3)
		Me.panel5.Controls.Add(Me.label1)
		Me.panel5.Location = New System.Drawing.Point(3, 3)
		Me.panel5.Name = "panel5"
		Me.panel5.Size = New System.Drawing.Size(153, 34)
		Me.panel5.TabIndex = 15
		Me.panel5.Visible = false
		'
		'pictureBox3
		'
		Me.pictureBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer))
		Me.pictureBox3.Image = CType(resources.GetObject("pictureBox3.Image"),System.Drawing.Image)
		Me.pictureBox3.Location = New System.Drawing.Point(8, 10)
		Me.pictureBox3.Name = "pictureBox3"
		Me.pictureBox3.Size = New System.Drawing.Size(19, 18)
		Me.pictureBox3.TabIndex = 15
		Me.pictureBox3.TabStop = false
		'
		'label1
		'
		Me.label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer))
		Me.label1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer))
		Me.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.label1.Location = New System.Drawing.Point(23, 6)
		Me.label1.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
		Me.label1.Name = "label1"
		Me.label1.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
		Me.label1.Size = New System.Drawing.Size(117, 23)
		Me.label1.TabIndex = 14
		Me.label1.Text = "Reading files ..."
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'textBoxJump
		'
		Me.textBoxJump.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBoxJump.Location = New System.Drawing.Point(572, 11)
		Me.textBoxJump.Name = "textBoxJump"
		Me.textBoxJump.Size = New System.Drawing.Size(67, 21)
		Me.textBoxJump.TabIndex = 3
		Me.textBoxJump.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		Me.textBoxJump.Visible = false
		AddHandler Me.textBoxJump.TextChanged, AddressOf Me.TextBoxJumpTextChanged
		AddHandler Me.textBoxJump.KeyPress, AddressOf Me.TextBoxJump_KeyPress
		AddHandler Me.textBoxJump.Leave, AddressOf Me.TextBoxJumpLeave
		'
		'labelCounter
		'
		Me.labelCounter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.labelCounter.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.labelCounter.Location = New System.Drawing.Point(572, 13)
		Me.labelCounter.Name = "labelCounter"
		Me.labelCounter.Size = New System.Drawing.Size(67, 23)
		Me.labelCounter.TabIndex = 11
		Me.labelCounter.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.labelCounter.Visible = false
		AddHandler Me.labelCounter.DoubleClick, AddressOf Me.LabelCounterDoubleClick
		'
		'buttonNextOff
		'
		Me.buttonNextOff.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.buttonNextOff.BackColor = System.Drawing.SystemColors.Control
		Me.buttonNextOff.BackgroundImage = CType(resources.GetObject("buttonNextOff.BackgroundImage"),System.Drawing.Image)
		Me.buttonNextOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.buttonNextOff.Enabled = false
		Me.buttonNextOff.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonNextOff.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.buttonNextOff.Location = New System.Drawing.Point(645, 8)
		Me.buttonNextOff.Name = "buttonNextOff"
		Me.buttonNextOff.Size = New System.Drawing.Size(47, 26)
		Me.buttonNextOff.TabIndex = 10
		Me.buttonNextOff.UseVisualStyleBackColor = false
		Me.buttonNextOff.Visible = false
		'
		'buttonNext
		'
		Me.buttonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.buttonNext.BackColor = System.Drawing.SystemColors.Control
		Me.buttonNext.BackgroundImage = CType(resources.GetObject("buttonNext.BackgroundImage"),System.Drawing.Image)
		Me.buttonNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.buttonNext.Enabled = false
		Me.buttonNext.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonNext.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.buttonNext.Location = New System.Drawing.Point(645, 7)
		Me.buttonNext.Name = "buttonNext"
		Me.buttonNext.Size = New System.Drawing.Size(47, 26)
		Me.buttonNext.TabIndex = 8
		Me.buttonNext.UseVisualStyleBackColor = false
		Me.buttonNext.Visible = false
		AddHandler Me.buttonNext.Click, AddressOf Me.ButtonNextClick
		'
		'buttonPreviousOff
		'
		Me.buttonPreviousOff.BackColor = System.Drawing.SystemColors.Control
		Me.buttonPreviousOff.BackgroundImage = CType(resources.GetObject("buttonPreviousOff.BackgroundImage"),System.Drawing.Image)
		Me.buttonPreviousOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.buttonPreviousOff.Cursor = System.Windows.Forms.Cursors.Default
		Me.buttonPreviousOff.Enabled = false
		Me.buttonPreviousOff.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonPreviousOff.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.buttonPreviousOff.Location = New System.Drawing.Point(12, 8)
		Me.buttonPreviousOff.Name = "buttonPreviousOff"
		Me.buttonPreviousOff.Size = New System.Drawing.Size(47, 26)
		Me.buttonPreviousOff.TabIndex = 9
		Me.buttonPreviousOff.UseVisualStyleBackColor = false
		Me.buttonPreviousOff.Visible = false
		'
		'buttonPrevious
		'
		Me.buttonPrevious.BackColor = System.Drawing.SystemColors.Control
		Me.buttonPrevious.BackgroundImage = CType(resources.GetObject("buttonPrevious.BackgroundImage"),System.Drawing.Image)
		Me.buttonPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.buttonPrevious.Cursor = System.Windows.Forms.Cursors.Default
		Me.buttonPrevious.Enabled = false
		Me.buttonPrevious.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonPrevious.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.buttonPrevious.Location = New System.Drawing.Point(12, 7)
		Me.buttonPrevious.Name = "buttonPrevious"
		Me.buttonPrevious.Size = New System.Drawing.Size(47, 26)
		Me.buttonPrevious.TabIndex = 7
		Me.buttonPrevious.UseVisualStyleBackColor = false
		Me.buttonPrevious.Visible = false
		AddHandler Me.buttonPrevious.Click, AddressOf Me.ButtonPreviousClick
		'
		'panel4
		'
		Me.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top
		Me.panel4.Controls.Add(Me.buttonPageDown)
		Me.panel4.Controls.Add(Me.buttonPageUp)
		Me.panel4.Controls.Add(Me.buttonLineDown)
		Me.panel4.Controls.Add(Me.buttonLineUp)
		Me.panel4.Controls.Add(Me.scrollPanel)
		Me.panel4.Controls.Add(Me.labelStatus)
		Me.panel4.Controls.Add(Me.checkBox1)
		Me.panel4.Location = New System.Drawing.Point(145, 5)
		Me.panel4.Name = "panel4"
		Me.panel4.Size = New System.Drawing.Size(415, 32)
		Me.panel4.TabIndex = 7
		'
		'buttonPageDown
		'
		Me.buttonPageDown.BackColor = System.Drawing.SystemColors.Control
		Me.buttonPageDown.BackgroundImage = CType(resources.GetObject("buttonPageDown.BackgroundImage"),System.Drawing.Image)
		Me.buttonPageDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.buttonPageDown.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonPageDown.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.buttonPageDown.Location = New System.Drawing.Point(213, 2)
		Me.buttonPageDown.Name = "buttonPageDown"
		Me.buttonPageDown.Size = New System.Drawing.Size(47, 26)
		Me.buttonPageDown.TabIndex = 2
		Me.buttonPageDown.UseVisualStyleBackColor = false
		AddHandler Me.buttonPageDown.Click, AddressOf Me.ButtonPageDownClick
		'
		'buttonPageUp
		'
		Me.buttonPageUp.BackColor = System.Drawing.SystemColors.Control
		Me.buttonPageUp.BackgroundImage = CType(resources.GetObject("buttonPageUp.BackgroundImage"),System.Drawing.Image)
		Me.buttonPageUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.buttonPageUp.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonPageUp.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.buttonPageUp.Location = New System.Drawing.Point(152, 2)
		Me.buttonPageUp.Name = "buttonPageUp"
		Me.buttonPageUp.Size = New System.Drawing.Size(47, 26)
		Me.buttonPageUp.TabIndex = 3
		Me.buttonPageUp.UseVisualStyleBackColor = false
		AddHandler Me.buttonPageUp.Click, AddressOf Me.ButtonPageUpClick
		'
		'buttonLineDown
		'
		Me.buttonLineDown.BackColor = System.Drawing.SystemColors.Control
		Me.buttonLineDown.BackgroundImage = CType(resources.GetObject("buttonLineDown.BackgroundImage"),System.Drawing.Image)
		Me.buttonLineDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.buttonLineDown.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonLineDown.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.buttonLineDown.Location = New System.Drawing.Point(266, 2)
		Me.buttonLineDown.Name = "buttonLineDown"
		Me.buttonLineDown.Size = New System.Drawing.Size(47, 26)
		Me.buttonLineDown.TabIndex = 4
		Me.buttonLineDown.UseVisualStyleBackColor = false
		AddHandler Me.buttonLineDown.Click, AddressOf Me.ButtonLineDownClick
		'
		'buttonLineUp
		'
		Me.buttonLineUp.BackColor = System.Drawing.SystemColors.Control
		Me.buttonLineUp.BackgroundImage = CType(resources.GetObject("buttonLineUp.BackgroundImage"),System.Drawing.Image)
		Me.buttonLineUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.buttonLineUp.Cursor = System.Windows.Forms.Cursors.Default
		Me.buttonLineUp.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.buttonLineUp.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.buttonLineUp.Location = New System.Drawing.Point(99, 2)
		Me.buttonLineUp.Name = "buttonLineUp"
		Me.buttonLineUp.Size = New System.Drawing.Size(47, 26)
		Me.buttonLineUp.TabIndex = 5
		Me.buttonLineUp.UseVisualStyleBackColor = false
		AddHandler Me.buttonLineUp.Click, AddressOf Me.ButtonLineUpClick
		'
		'scrollPanel
		'
		Me.scrollPanel.Controls.Add(Me.btnToggleScroll)
		Me.scrollPanel.Location = New System.Drawing.Point(315, 1)
		Me.scrollPanel.Name = "scrollPanel"
		Me.scrollPanel.Size = New System.Drawing.Size(28, 30)
		Me.scrollPanel.TabIndex = 14
		Me.scrollPanel.Visible = false
		'
		'btnToggleScroll
		'
		Me.btnToggleScroll.BackColor = System.Drawing.SystemColors.Control
		Me.btnToggleScroll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
		Me.btnToggleScroll.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.btnToggleScroll.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.btnToggleScroll.Image = CType(resources.GetObject("btnToggleScroll.Image"),System.Drawing.Image)
		Me.btnToggleScroll.Location = New System.Drawing.Point(1, 1)
		Me.btnToggleScroll.Name = "btnToggleScroll"
		Me.btnToggleScroll.Size = New System.Drawing.Size(26, 26)
		Me.btnToggleScroll.TabIndex = 13
		Me.btnToggleScroll.UseVisualStyleBackColor = false
		AddHandler Me.btnToggleScroll.Click, AddressOf Me.Button1Click
		'
		'labelStatus
		'
		Me.labelStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.labelStatus.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.labelStatus.ForeColor = System.Drawing.Color.DarkGray
		Me.labelStatus.Location = New System.Drawing.Point(347, 7)
		Me.labelStatus.Name = "labelStatus"
		Me.labelStatus.Size = New System.Drawing.Size(64, 18)
		Me.labelStatus.TabIndex = 12
		Me.labelStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.labelStatus.Visible = false
		AddHandler Me.labelStatus.TextChanged, AddressOf Me.LabelStatusTextChanged
		AddHandler Me.labelStatus.MouseClick, AddressOf Me.LabelStatusMouseClick
		AddHandler Me.labelStatus.MouseHover, AddressOf Me.LabelStatusMouseHover
		'
		'checkBox1
		'
		Me.checkBox1.AutoSize = true
		Me.checkBox1.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.checkBox1.Location = New System.Drawing.Point(3, 8)
		Me.checkBox1.Name = "checkBox1"
		Me.checkBox1.Size = New System.Drawing.Size(91, 17)
		Me.checkBox1.TabIndex = 0
		Me.checkBox1.Text = "sync scrollbar"
		Me.checkBox1.UseVisualStyleBackColor = true
		Me.checkBox1.Visible = false
		AddHandler Me.checkBox1.CheckedChanged, AddressOf Me.CheckBox1CheckedChanged
		'
		'menuStrip1
		'
		Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.projectToolStripMenuItem, Me.ProjectFilterToolStripMenuItem, Me.toolsToolStripMenuItem, Me.sourceToolStripMenuItem, Me.viewToolStripMenuItem, Me.targetToolStripMenuItem, Me.settingsToolStripMenuItem, Me.helpToolStripMenuItem, Me.annotateModeToolStripMenuItem})
		Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.menuStrip1.Name = "menuStrip1"
		Me.menuStrip1.Size = New System.Drawing.Size(704, 24)
		Me.menuStrip1.TabIndex = 7
		'
		'fileToolStripMenuItem
		'
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.openSourceFileToolStripMenuItem, Me.openTargetFileToolStripMenuItem, Me.toolStripSeparator4, Me.saveProjectToolStripMenuItem1, Me.openProjectToolStripMenuItem, Me.newProjectToolStripMenuItem, Me.saveProjectToolStripMenuItem, Me.closeProjectToolStripMenuItem, Me.toolStripSeparator1, Me.exitToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.fileToolStripMenuItem.Text = "File"
		'
		'openSourceFileToolStripMenuItem
		'
		Me.openSourceFileToolStripMenuItem.Name = "openSourceFileToolStripMenuItem"
		Me.openSourceFileToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.openSourceFileToolStripMenuItem.Text = "Open source..."
		AddHandler Me.openSourceFileToolStripMenuItem.Click, AddressOf Me.OpenSourceFileToolStripMenuItemClick
		'
		'openTargetFileToolStripMenuItem
		'
		Me.openTargetFileToolStripMenuItem.Name = "openTargetFileToolStripMenuItem"
		Me.openTargetFileToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.openTargetFileToolStripMenuItem.Text = "Open target..."
		AddHandler Me.openTargetFileToolStripMenuItem.Click, AddressOf Me.OpenTargetFileToolStripMenuItemClick
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		Me.toolStripSeparator4.Size = New System.Drawing.Size(149, 6)
		'
		'saveProjectToolStripMenuItem1
		'
		Me.saveProjectToolStripMenuItem1.Image = CType(resources.GetObject("saveProjectToolStripMenuItem1.Image"),System.Drawing.Image)
		Me.saveProjectToolStripMenuItem1.Name = "saveProjectToolStripMenuItem1"
		Me.saveProjectToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
		Me.saveProjectToolStripMenuItem1.Text = "Save project"
		Me.saveProjectToolStripMenuItem1.Visible = false
		AddHandler Me.saveProjectToolStripMenuItem1.Click, AddressOf Me.SaveProjectToolStripMenuItem1Click
		'
		'openProjectToolStripMenuItem
		'
		Me.openProjectToolStripMenuItem.Image = CType(resources.GetObject("openProjectToolStripMenuItem.Image"),System.Drawing.Image)
		Me.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem"
		Me.openProjectToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.openProjectToolStripMenuItem.Text = "Open project..."
		AddHandler Me.openProjectToolStripMenuItem.Click, AddressOf Me.OpenProjectToolStripMenuItemClick
		'
		'newProjectToolStripMenuItem
		'
		Me.newProjectToolStripMenuItem.Image = CType(resources.GetObject("newProjectToolStripMenuItem.Image"),System.Drawing.Image)
		Me.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem"
		Me.newProjectToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.newProjectToolStripMenuItem.Text = "New project..."
		AddHandler Me.newProjectToolStripMenuItem.Click, AddressOf Me.NewProjectToolStripMenuItemClick
		'
		'saveProjectToolStripMenuItem
		'
		Me.saveProjectToolStripMenuItem.Enabled = false
		Me.saveProjectToolStripMenuItem.Image = CType(resources.GetObject("saveProjectToolStripMenuItem.Image"),System.Drawing.Image)
		Me.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem"
		Me.saveProjectToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.saveProjectToolStripMenuItem.Text = "Save project..."
		AddHandler Me.saveProjectToolStripMenuItem.Click, AddressOf Me.SaveProjectToolStripMenuItemClick
		'
		'closeProjectToolStripMenuItem
		'
		Me.closeProjectToolStripMenuItem.Enabled = false
		Me.closeProjectToolStripMenuItem.Image = CType(resources.GetObject("closeProjectToolStripMenuItem.Image"),System.Drawing.Image)
		Me.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem"
		Me.closeProjectToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.closeProjectToolStripMenuItem.Text = "Close project"
		AddHandler Me.closeProjectToolStripMenuItem.Click, AddressOf Me.CloseProjectToolStripMenuItemClick
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(149, 6)
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		Me.exitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.exitToolStripMenuItem.Text = "Exit"
		AddHandler Me.exitToolStripMenuItem.Click, AddressOf Me.ExitToolStripMenuItemClick
		'
		'projectToolStripMenuItem
		'
		Me.projectToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.settingsToolStripMenuItem1, Me.refreshToolStripMenuItem1, Me.toolStripSeparator13, Me.saveReportToolStripMenuItem, Me.toolStripMenuItem2, Me.saveLogToolStripMenuItem, Me.clearLogToolStripMenuItem1, Me.toolStripSeparator21, Me.resetToolStripMenuItem})
		Me.projectToolStripMenuItem.Name = "projectToolStripMenuItem"
		Me.projectToolStripMenuItem.Size = New System.Drawing.Size(56, 20)
		Me.projectToolStripMenuItem.Text = "Project"
		Me.projectToolStripMenuItem.Visible = false
		'
		'settingsToolStripMenuItem1
		'
		Me.settingsToolStripMenuItem1.Image = CType(resources.GetObject("settingsToolStripMenuItem1.Image"),System.Drawing.Image)
		Me.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1"
		Me.settingsToolStripMenuItem1.Size = New System.Drawing.Size(133, 22)
		Me.settingsToolStripMenuItem1.Text = "Settings..."
		AddHandler Me.settingsToolStripMenuItem1.Click, AddressOf Me.ProjectSettingsToolStripMenuItemClick
		'
		'refreshToolStripMenuItem1
		'
		Me.refreshToolStripMenuItem1.Image = CType(resources.GetObject("refreshToolStripMenuItem1.Image"),System.Drawing.Image)
		Me.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1"
		Me.refreshToolStripMenuItem1.Size = New System.Drawing.Size(133, 22)
		Me.refreshToolStripMenuItem1.Text = "Refresh"
		AddHandler Me.refreshToolStripMenuItem1.Click, AddressOf Me.RefreshToolStripMenuItem1Click
		'
		'toolStripSeparator13
		'
		Me.toolStripSeparator13.Name = "toolStripSeparator13"
		Me.toolStripSeparator13.Size = New System.Drawing.Size(130, 6)
		'
		'saveReportToolStripMenuItem
		'
		Me.saveReportToolStripMenuItem.Name = "saveReportToolStripMenuItem"
		Me.saveReportToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
		Me.saveReportToolStripMenuItem.Text = "Save report"
		AddHandler Me.saveReportToolStripMenuItem.Click, AddressOf Me.SaveReportToolStripMenuItemClick
		'
		'toolStripMenuItem2
		'
		Me.toolStripMenuItem2.Name = "toolStripMenuItem2"
		Me.toolStripMenuItem2.Size = New System.Drawing.Size(130, 6)
		'
		'saveLogToolStripMenuItem
		'
		Me.saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem"
		Me.saveLogToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
		Me.saveLogToolStripMenuItem.Text = "Save log"
		Me.saveLogToolStripMenuItem.Visible = false
		AddHandler Me.saveLogToolStripMenuItem.Click, AddressOf Me.SaveLogToolStripMenuItemClick
		'
		'clearLogToolStripMenuItem1
		'
		Me.clearLogToolStripMenuItem1.Name = "clearLogToolStripMenuItem1"
		Me.clearLogToolStripMenuItem1.Size = New System.Drawing.Size(133, 22)
		Me.clearLogToolStripMenuItem1.Text = "Clear log"
		Me.clearLogToolStripMenuItem1.Visible = false
		AddHandler Me.clearLogToolStripMenuItem1.Click, AddressOf Me.ClearLogToolStripMenuItem1Click
		'
		'toolStripSeparator21
		'
		Me.toolStripSeparator21.Name = "toolStripSeparator21"
		Me.toolStripSeparator21.Size = New System.Drawing.Size(130, 6)
		Me.toolStripSeparator21.Visible = false
		'
		'resetToolStripMenuItem
		'
		Me.resetToolStripMenuItem.Name = "resetToolStripMenuItem"
		Me.resetToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
		Me.resetToolStripMenuItem.Text = "Reset"
		AddHandler Me.resetToolStripMenuItem.Click, AddressOf Me.ResetToolStripMenuItemClick
		'
		'toolsToolStripMenuItem
		'
		Me.toolsToolStripMenuItem.DropDown = Me.contextMenuStripTools
		Me.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem"
		Me.toolsToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
		Me.toolsToolStripMenuItem.Text = "Tools"
		'
		'contextMenuStripTools
		'
		Me.contextMenuStripTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.annotateToolStripMenuItem, Me.openFilesInCompareToolToolStripMenuItem1, Me.openSourceFileInEditorToolStripMenuItem, Me.openTargetFileInEditorToolStripMenuItem, Me.toolStripSeparator25, Me.pluginsToolStripMenuItem, Me.dEBUGToClipboardToolStripMenuItem})
		Me.contextMenuStripTools.Name = "contextMenuStripTools"
		Me.contextMenuStripTools.OwnerItem = Me.toolsToolStripMenuItem
		Me.contextMenuStripTools.Size = New System.Drawing.Size(215, 142)
		'
		'annotateToolStripMenuItem
		'
		Me.annotateToolStripMenuItem.CheckOnClick = true
		Me.annotateToolStripMenuItem.Image = CType(resources.GetObject("annotateToolStripMenuItem.Image"),System.Drawing.Image)
		Me.annotateToolStripMenuItem.Name = "annotateToolStripMenuItem"
		Me.annotateToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
		Me.annotateToolStripMenuItem.Text = "Annotate"
		AddHandler Me.annotateToolStripMenuItem.Click, AddressOf Me.AnnotateToolStripMenuItemClick
		'
		'openFilesInCompareToolToolStripMenuItem1
		'
		Me.openFilesInCompareToolToolStripMenuItem1.Name = "openFilesInCompareToolToolStripMenuItem1"
		Me.openFilesInCompareToolToolStripMenuItem1.Size = New System.Drawing.Size(214, 22)
		Me.openFilesInCompareToolToolStripMenuItem1.Text = "Open files in compare tool"
		AddHandler Me.openFilesInCompareToolToolStripMenuItem1.Click, AddressOf Me.OpenFilesInCompareToolToolStripMenuItemClick
		'
		'openSourceFileInEditorToolStripMenuItem
		'
		Me.openSourceFileInEditorToolStripMenuItem.Name = "openSourceFileInEditorToolStripMenuItem"
		Me.openSourceFileInEditorToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
		Me.openSourceFileInEditorToolStripMenuItem.Text = "Open source file in editor"
		AddHandler Me.openSourceFileInEditorToolStripMenuItem.Click, AddressOf Me.EditorToolStripMenuItemClick
		'
		'openTargetFileInEditorToolStripMenuItem
		'
		Me.openTargetFileInEditorToolStripMenuItem.Name = "openTargetFileInEditorToolStripMenuItem"
		Me.openTargetFileInEditorToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
		Me.openTargetFileInEditorToolStripMenuItem.Text = "Open target file in editor"
		AddHandler Me.openTargetFileInEditorToolStripMenuItem.Click, AddressOf Me.EditorToolStripMenuItemClick
		'
		'toolStripSeparator25
		'
		Me.toolStripSeparator25.Name = "toolStripSeparator25"
		Me.toolStripSeparator25.Size = New System.Drawing.Size(211, 6)
		Me.toolStripSeparator25.Visible = false
		'
		'pluginsToolStripMenuItem
		'
		Me.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem"
		Me.pluginsToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
		Me.pluginsToolStripMenuItem.Text = "Plug-ins"
		Me.pluginsToolStripMenuItem.Visible = false
		'
		'dEBUGToClipboardToolStripMenuItem
		'
		Me.dEBUGToClipboardToolStripMenuItem.ForeColor = System.Drawing.Color.Red
		Me.dEBUGToClipboardToolStripMenuItem.Name = "dEBUGToClipboardToolStripMenuItem"
		Me.dEBUGToClipboardToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
		Me.dEBUGToClipboardToolStripMenuItem.Text = "*DEBUG* To clipboard"
		Me.dEBUGToClipboardToolStripMenuItem.Visible = false
		AddHandler Me.dEBUGToClipboardToolStripMenuItem.Click, AddressOf Me.DEBUGToClipboardToolStripMenuItemClick
		'
		'viewToolStripMenuItem
		'
		Me.viewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.orientationToolStripMenuItem, Me.fullScreenToolStripMenuItem, Me.dualscreenToolStripMenuItem, Me.toolStripSeparator27, Me.browserSizeToolStripMenuItem, Me.toolStripSeparator2, Me.splitToolStripMenuItem, Me.sourceOnlyToolStripMenuItem, Me.targetOnlyToolStripMenuItem, Me.toolStripSeparator3, Me.splitSizeFixedToolStripMenuItem, Me.toolStripSeparator5, Me.showFileListToolStripMenuItem, Me.showInfoToolStripMenuItem, Me.showCommentsToolStripMenuItem, Me.toolStripMenuItem4, Me.hideControlsToolStripMenuItem, Me.showSearchToolStripMenuItem})
		Me.viewToolStripMenuItem.Name = "viewToolStripMenuItem"
		Me.viewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
		Me.viewToolStripMenuItem.Text = "View"
		'
		'orientationToolStripMenuItem
		'
		Me.orientationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.verticalToolStripMenuItem, Me.horizontalToolStripMenuItem})
		Me.orientationToolStripMenuItem.Image = CType(resources.GetObject("orientationToolStripMenuItem.Image"),System.Drawing.Image)
		Me.orientationToolStripMenuItem.Name = "orientationToolStripMenuItem"
		Me.orientationToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.orientationToolStripMenuItem.Text = "Orientation"
		'
		'verticalToolStripMenuItem
		'
		Me.verticalToolStripMenuItem.Checked = true
		Me.verticalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.verticalToolStripMenuItem.Image = CType(resources.GetObject("verticalToolStripMenuItem.Image"),System.Drawing.Image)
		Me.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem"
		Me.verticalToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
		Me.verticalToolStripMenuItem.Text = "Vertical"
		AddHandler Me.verticalToolStripMenuItem.Click, AddressOf Me.changeOrientationVertical
		'
		'horizontalToolStripMenuItem
		'
		Me.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem"
		Me.horizontalToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
		Me.horizontalToolStripMenuItem.Text = "Horizontal"
		AddHandler Me.horizontalToolStripMenuItem.Click, AddressOf Me.changeOrientationHorizontal
		'
		'fullScreenToolStripMenuItem
		'
		Me.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem"
		Me.fullScreenToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.fullScreenToolStripMenuItem.Text = "Full screen"
		AddHandler Me.fullScreenToolStripMenuItem.Click, AddressOf Me.FullScreenToolStripMenuItemClick
		'
		'dualscreenToolStripMenuItem
		'
		Me.dualscreenToolStripMenuItem.Name = "dualscreenToolStripMenuItem"
		Me.dualscreenToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.dualscreenToolStripMenuItem.Text = "Dual-screen"
		Me.dualscreenToolStripMenuItem.Visible = false
		AddHandler Me.dualscreenToolStripMenuItem.Click, AddressOf Me.DualscreenToolStripMenuItemClick
		'
		'toolStripSeparator27
		'
		Me.toolStripSeparator27.Name = "toolStripSeparator27"
		Me.toolStripSeparator27.Size = New System.Drawing.Size(179, 6)
		'
		'browserSizeToolStripMenuItem
		'
		Me.browserSizeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItemSize1, Me.toolStripMenuItemSize2, Me.toolStripMenuItemSize3, Me.toolStripMenuItemSize4, Me.toolStripSeparator26, Me.excludeScrollbarToolStripMenuItem, Me.showBrowserWidthToolStripMenuItem})
		Me.browserSizeToolStripMenuItem.Name = "browserSizeToolStripMenuItem"
		Me.browserSizeToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.browserSizeToolStripMenuItem.Text = "Browser size presets"
		'
		'toolStripMenuItemSize1
		'
		Me.toolStripMenuItemSize1.Name = "toolStripMenuItemSize1"
		Me.toolStripMenuItemSize1.Size = New System.Drawing.Size(162, 22)
		Me.toolStripMenuItemSize1.Text = "320"
		AddHandler Me.toolStripMenuItemSize1.Click, AddressOf Me.ToolStripMenuItem10Click
		'
		'toolStripMenuItemSize2
		'
		Me.toolStripMenuItemSize2.Name = "toolStripMenuItemSize2"
		Me.toolStripMenuItemSize2.Size = New System.Drawing.Size(162, 22)
		Me.toolStripMenuItemSize2.Text = "480"
		AddHandler Me.toolStripMenuItemSize2.Click, AddressOf Me.ToolStripMenuItem10Click
		'
		'toolStripMenuItemSize3
		'
		Me.toolStripMenuItemSize3.Name = "toolStripMenuItemSize3"
		Me.toolStripMenuItemSize3.Size = New System.Drawing.Size(162, 22)
		Me.toolStripMenuItemSize3.Text = "800"
		AddHandler Me.toolStripMenuItemSize3.Click, AddressOf Me.ToolStripMenuItem10Click
		'
		'toolStripMenuItemSize4
		'
		Me.toolStripMenuItemSize4.Name = "toolStripMenuItemSize4"
		Me.toolStripMenuItemSize4.Size = New System.Drawing.Size(162, 22)
		Me.toolStripMenuItemSize4.Text = "1024"
		AddHandler Me.toolStripMenuItemSize4.Click, AddressOf Me.ToolStripMenuItem10Click
		'
		'toolStripSeparator26
		'
		Me.toolStripSeparator26.Name = "toolStripSeparator26"
		Me.toolStripSeparator26.Size = New System.Drawing.Size(159, 6)
		'
		'excludeScrollbarToolStripMenuItem
		'
		Me.excludeScrollbarToolStripMenuItem.CheckOnClick = true
		Me.excludeScrollbarToolStripMenuItem.Name = "excludeScrollbarToolStripMenuItem"
		Me.excludeScrollbarToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
		Me.excludeScrollbarToolStripMenuItem.Text = "Exclude scrollbar"
		AddHandler Me.excludeScrollbarToolStripMenuItem.CheckedChanged, AddressOf Me.ExcludeScrollbarToolStripMenuItemCheckedChanged
		'
		'showBrowserWidthToolStripMenuItem
		'
		Me.showBrowserWidthToolStripMenuItem.CheckOnClick = true
		Me.showBrowserWidthToolStripMenuItem.Name = "showBrowserWidthToolStripMenuItem"
		Me.showBrowserWidthToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
		Me.showBrowserWidthToolStripMenuItem.Text = "Show width"
		AddHandler Me.showBrowserWidthToolStripMenuItem.CheckedChanged, AddressOf Me.ShowBrowserWidthToolStripMenuItemCheckedChanged
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(179, 6)
		'
		'splitToolStripMenuItem
		'
		Me.splitToolStripMenuItem.Checked = true
		Me.splitToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.splitToolStripMenuItem.Image = CType(resources.GetObject("splitToolStripMenuItem.Image"),System.Drawing.Image)
		Me.splitToolStripMenuItem.Name = "splitToolStripMenuItem"
		Me.splitToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.splitToolStripMenuItem.Text = "Source and target"
		AddHandler Me.splitToolStripMenuItem.Click, AddressOf Me.SplitToolStripMenuItemClick
		'
		'sourceOnlyToolStripMenuItem
		'
		Me.sourceOnlyToolStripMenuItem.Name = "sourceOnlyToolStripMenuItem"
		Me.sourceOnlyToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.sourceOnlyToolStripMenuItem.Text = "Source only"
		AddHandler Me.sourceOnlyToolStripMenuItem.Click, AddressOf Me.SourceOnlyToolStripMenuItemClick
		'
		'targetOnlyToolStripMenuItem
		'
		Me.targetOnlyToolStripMenuItem.Name = "targetOnlyToolStripMenuItem"
		Me.targetOnlyToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.targetOnlyToolStripMenuItem.Text = "Target only"
		AddHandler Me.targetOnlyToolStripMenuItem.Click, AddressOf Me.TargetOnlyToolStripMenuItemClick
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(179, 6)
		'
		'splitSizeFixedToolStripMenuItem
		'
		Me.splitSizeFixedToolStripMenuItem.Checked = true
		Me.splitSizeFixedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.splitSizeFixedToolStripMenuItem.Name = "splitSizeFixedToolStripMenuItem"
		Me.splitSizeFixedToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.splitSizeFixedToolStripMenuItem.Text = "Split size fixed"
		AddHandler Me.splitSizeFixedToolStripMenuItem.Click, AddressOf Me.SplitSizeFixedToolStripMenuItemClick
		'
		'toolStripSeparator5
		'
		Me.toolStripSeparator5.Name = "toolStripSeparator5"
		Me.toolStripSeparator5.Size = New System.Drawing.Size(179, 6)
		'
		'showFileListToolStripMenuItem
		'
		Me.showFileListToolStripMenuItem.Image = CType(resources.GetObject("showFileListToolStripMenuItem.Image"),System.Drawing.Image)
		Me.showFileListToolStripMenuItem.Name = "showFileListToolStripMenuItem"
		Me.showFileListToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.showFileListToolStripMenuItem.Text = "Show file list"
		Me.showFileListToolStripMenuItem.Visible = false
		AddHandler Me.showFileListToolStripMenuItem.Click, AddressOf Me.ShowFileListToolStripMenuItemClick
		'
		'showInfoToolStripMenuItem
		'
		Me.showInfoToolStripMenuItem.Name = "showInfoToolStripMenuItem"
		Me.showInfoToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.showInfoToolStripMenuItem.Text = "Show info"
		AddHandler Me.showInfoToolStripMenuItem.Click, AddressOf Me.ShowInfoToolStripMenuItemClick
		'
		'showCommentsToolStripMenuItem
		'
		Me.showCommentsToolStripMenuItem.CheckOnClick = true
		Me.showCommentsToolStripMenuItem.Image = CType(resources.GetObject("showCommentsToolStripMenuItem.Image"),System.Drawing.Image)
		Me.showCommentsToolStripMenuItem.Name = "showCommentsToolStripMenuItem"
		Me.showCommentsToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.showCommentsToolStripMenuItem.Text = "Show file comments"
		Me.showCommentsToolStripMenuItem.Visible = false
		AddHandler Me.showCommentsToolStripMenuItem.Click, AddressOf Me.ShowCommentsToolStripMenuItemClick
		'
		'toolStripMenuItem4
		'
		Me.toolStripMenuItem4.Name = "toolStripMenuItem4"
		Me.toolStripMenuItem4.Size = New System.Drawing.Size(179, 6)
		'
		'hideControlsToolStripMenuItem
		'
		Me.hideControlsToolStripMenuItem.Name = "hideControlsToolStripMenuItem"
		Me.hideControlsToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.hideControlsToolStripMenuItem.Text = "Hide controls"
		AddHandler Me.hideControlsToolStripMenuItem.Click, AddressOf Me.HideControlsToolStripMenuItemClick
		'
		'showSearchToolStripMenuItem
		'
		Me.showSearchToolStripMenuItem.Image = CType(resources.GetObject("showSearchToolStripMenuItem.Image"),System.Drawing.Image)
		Me.showSearchToolStripMenuItem.Name = "showSearchToolStripMenuItem"
		Me.showSearchToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
		Me.showSearchToolStripMenuItem.Text = "Show search"
		AddHandler Me.showSearchToolStripMenuItem.Click, AddressOf Me.ShowSearchToolStripMenuItemClick
		'
		'settingsToolStripMenuItem
		'
		Me.settingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.modeToolStripMenuItem, Me.toolStripSeparator10, Me.preferedEditorToolStripMenuItem, Me.preferedCompareToolToolStripMenuItem, Me.toolStripSeparator9, Me.useMouseGesturesToolStripMenuItem, Me.autoRefreshAfterEditToolStripMenuItem, Me.fitImagesToolStripMenuItem1, Me.autoScrollToolStripMenuItem, Me.toolStripSeparator11, Me.highlightLocalLinksToolStripMenuItem, Me.highlightUITermsToolStripMenuItem, Me.searchColorToolStripMenuItem, Me.toolStripSeparator18, Me.followLinksToolStripMenuItem, Me.toolStripSeparator17, Me.saveSettingsToolStripMenuItem, Me.toolStripSeparator24, Me.projectToolStripMenuItem1})
		Me.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem"
		Me.settingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
		Me.settingsToolStripMenuItem.Text = "Settings"
		'
		'modeToolStripMenuItem
		'
		Me.modeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IE7ToolStripMenuItem, Me.IE8ToolStripMenuItem, Me.IE9ToolStripMenuItem, Me.IE10ToolStripMenuItem, Me.IE11ToolStripMenuItem, Me.IE12ToolStripMenuItem, Me.toolStripSeparator14, Me.synchronizedScrollToolStripMenuItem})
		Me.modeToolStripMenuItem.Name = "modeToolStripMenuItem"
		Me.modeToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.modeToolStripMenuItem.Text = "Mode"
		'
		'IE7ToolStripMenuItem
		'
		Me.IE7ToolStripMenuItem.Checked = true
		Me.IE7ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.IE7ToolStripMenuItem.Image = CType(resources.GetObject("IE7ToolStripMenuItem.Image"),System.Drawing.Image)
		Me.IE7ToolStripMenuItem.Name = "IE7ToolStripMenuItem"
		Me.IE7ToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.IE7ToolStripMenuItem.Text = "IE7"
		AddHandler Me.IE7ToolStripMenuItem.Click, AddressOf Me.changeIEmode
		'
		'IE8ToolStripMenuItem
		'
		Me.IE8ToolStripMenuItem.Name = "IE8ToolStripMenuItem"
		Me.IE8ToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.IE8ToolStripMenuItem.Text = "IE8"
		AddHandler Me.IE8ToolStripMenuItem.Click, AddressOf Me.changeIEmode
		'
		'IE9ToolStripMenuItem
		'
		Me.IE9ToolStripMenuItem.Name = "IE9ToolStripMenuItem"
		Me.IE9ToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.IE9ToolStripMenuItem.Text = "IE9"
		AddHandler Me.IE9ToolStripMenuItem.Click, AddressOf Me.changeIEmode
		'
		'IE10ToolStripMenuItem
		'
		Me.IE10ToolStripMenuItem.Name = "IE10ToolStripMenuItem"
		Me.IE10ToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.IE10ToolStripMenuItem.Text = "IE10"
		AddHandler Me.IE10ToolStripMenuItem.Click, AddressOf Me.changeIEmode
		'
		'IE11ToolStripMenuItem
		'
		Me.IE11ToolStripMenuItem.Name = "IE11ToolStripMenuItem"
		Me.IE11ToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.IE11ToolStripMenuItem.Text = "IE11"
		AddHandler Me.IE11ToolStripMenuItem.Click, AddressOf Me.changeIEmode
		'
		'IE12ToolStripMenuItem
		'
		Me.IE12ToolStripMenuItem.Name = "IE12ToolStripMenuItem"
		Me.IE12ToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.IE12ToolStripMenuItem.Text = "IE12 (Edge)"
		AddHandler Me.IE12ToolStripMenuItem.Click, AddressOf Me.changeIEmode
		'
		'toolStripSeparator14
		'
		Me.toolStripSeparator14.Name = "toolStripSeparator14"
		Me.toolStripSeparator14.Size = New System.Drawing.Size(173, 6)
		'
		'synchronizedScrollToolStripMenuItem
		'
		Me.synchronizedScrollToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.defaultToolStripMenuItem, Me.alternativeToolStripMenuItem, Me.alternative2ToolStripMenuItem, Me.imageToolStripMenuItem})
		Me.synchronizedScrollToolStripMenuItem.Name = "synchronizedScrollToolStripMenuItem"
		Me.synchronizedScrollToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.synchronizedScrollToolStripMenuItem.Text = "Synchronized scroll"
		'
		'defaultToolStripMenuItem
		'
		Me.defaultToolStripMenuItem.Checked = true
		Me.defaultToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.defaultToolStripMenuItem.Image = CType(resources.GetObject("defaultToolStripMenuItem.Image"),System.Drawing.Image)
		Me.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem"
		Me.defaultToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
		Me.defaultToolStripMenuItem.Text = "Default (IE)"
		AddHandler Me.defaultToolStripMenuItem.Click, AddressOf Me.DefaultToolStripMenuItemClick
		'
		'alternativeToolStripMenuItem
		'
		Me.alternativeToolStripMenuItem.Name = "alternativeToolStripMenuItem"
		Me.alternativeToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
		Me.alternativeToolStripMenuItem.Text = "Alternative 1 (IE)"
		AddHandler Me.alternativeToolStripMenuItem.Click, AddressOf Me.AlternativeToolStripMenuItemClick
		'
		'alternative2ToolStripMenuItem
		'
		Me.alternative2ToolStripMenuItem.Name = "alternative2ToolStripMenuItem"
		Me.alternative2ToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
		Me.alternative2ToolStripMenuItem.Text = "Alternative 2 (Form)"
		AddHandler Me.alternative2ToolStripMenuItem.Click, AddressOf Me.Alternative2ToolStripMenuItemClick
		'
		'imageToolStripMenuItem
		'
		Me.imageToolStripMenuItem.Name = "imageToolStripMenuItem"
		Me.imageToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
		Me.imageToolStripMenuItem.Text = "Image"
		Me.imageToolStripMenuItem.Visible = false
		AddHandler Me.imageToolStripMenuItem.Click, AddressOf Me.ImageToolStripMenuItemClick
		'
		'toolStripSeparator10
		'
		Me.toolStripSeparator10.Name = "toolStripSeparator10"
		Me.toolStripSeparator10.Size = New System.Drawing.Size(202, 6)
		'
		'preferedEditorToolStripMenuItem
		'
		Me.preferedEditorToolStripMenuItem.Name = "preferedEditorToolStripMenuItem"
		Me.preferedEditorToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.preferedEditorToolStripMenuItem.Text = "Preferred editor..."
		AddHandler Me.preferedEditorToolStripMenuItem.Click, AddressOf Me.PreferedEditorToolStripMenuItemClick
		'
		'preferedCompareToolToolStripMenuItem
		'
		Me.preferedCompareToolToolStripMenuItem.Name = "preferedCompareToolToolStripMenuItem"
		Me.preferedCompareToolToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.preferedCompareToolToolStripMenuItem.Text = "Preferred compare tool..."
		AddHandler Me.preferedCompareToolToolStripMenuItem.Click, AddressOf Me.PreferedCompareToolToolStripMenuItemClick
		'
		'toolStripSeparator9
		'
		Me.toolStripSeparator9.Name = "toolStripSeparator9"
		Me.toolStripSeparator9.Size = New System.Drawing.Size(202, 6)
		'
		'useMouseGesturesToolStripMenuItem
		'
		Me.useMouseGesturesToolStripMenuItem.Checked = true
		Me.useMouseGesturesToolStripMenuItem.CheckOnClick = true
		Me.useMouseGesturesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.useMouseGesturesToolStripMenuItem.Name = "useMouseGesturesToolStripMenuItem"
		Me.useMouseGesturesToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.useMouseGesturesToolStripMenuItem.Text = "Use mouse gestures"
		AddHandler Me.useMouseGesturesToolStripMenuItem.Click, AddressOf Me.UseMouseGesturesToolStripMenuItemClick
		'
		'autoRefreshAfterEditToolStripMenuItem
		'
		Me.autoRefreshAfterEditToolStripMenuItem.Checked = true
		Me.autoRefreshAfterEditToolStripMenuItem.CheckOnClick = true
		Me.autoRefreshAfterEditToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.autoRefreshAfterEditToolStripMenuItem.Name = "autoRefreshAfterEditToolStripMenuItem"
		Me.autoRefreshAfterEditToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.autoRefreshAfterEditToolStripMenuItem.Text = "Auto refresh after edit"
		'
		'fitImagesToolStripMenuItem1
		'
		Me.fitImagesToolStripMenuItem1.CheckOnClick = true
		Me.fitImagesToolStripMenuItem1.Name = "fitImagesToolStripMenuItem1"
		Me.fitImagesToolStripMenuItem1.Size = New System.Drawing.Size(205, 22)
		Me.fitImagesToolStripMenuItem1.Text = "Fit images"
		AddHandler Me.fitImagesToolStripMenuItem1.Click, AddressOf Me.FitImagesToolStripMenuItem1Click
		'
		'autoScrollToolStripMenuItem
		'
		Me.autoScrollToolStripMenuItem.CheckOnClick = true
		Me.autoScrollToolStripMenuItem.Name = "autoScrollToolStripMenuItem"
		Me.autoScrollToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.autoScrollToolStripMenuItem.Text = "Auto scroll"
		AddHandler Me.autoScrollToolStripMenuItem.Click, AddressOf Me.AutoScrollToolStripMenuItemClick
		'
		'toolStripSeparator11
		'
		Me.toolStripSeparator11.Name = "toolStripSeparator11"
		Me.toolStripSeparator11.Size = New System.Drawing.Size(202, 6)
		'
		'highlightLocalLinksToolStripMenuItem
		'
		Me.highlightLocalLinksToolStripMenuItem.CheckOnClick = true
		Me.highlightLocalLinksToolStripMenuItem.Name = "highlightLocalLinksToolStripMenuItem"
		Me.highlightLocalLinksToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.highlightLocalLinksToolStripMenuItem.Text = "Highlight local links"
		'
		'highlightUITermsToolStripMenuItem
		'
		Me.highlightUITermsToolStripMenuItem.DropDown = Me.contextMenuStripHighlightPlugins
		Me.highlightUITermsToolStripMenuItem.Name = "highlightUITermsToolStripMenuItem"
		Me.highlightUITermsToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.highlightUITermsToolStripMenuItem.Text = "Highlight contents"
		Me.highlightUITermsToolStripMenuItem.Visible = false
		'
		'contextMenuStripHighlightPlugins
		'
		Me.contextMenuStripHighlightPlugins.Name = "contextMenuStripPlugins"
		Me.contextMenuStripHighlightPlugins.OwnerItem = Me.highlightUITermsToolStripMenuItem
		Me.contextMenuStripHighlightPlugins.Size = New System.Drawing.Size(61, 4)
		'
		'toolStripSeparator18
		'
		Me.toolStripSeparator18.Name = "toolStripSeparator18"
		Me.toolStripSeparator18.Size = New System.Drawing.Size(202, 6)
		'
		'followLinksToolStripMenuItem
		'
		Me.followLinksToolStripMenuItem.Checked = true
		Me.followLinksToolStripMenuItem.CheckOnClick = true
		Me.followLinksToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.followLinksToolStripMenuItem.Name = "followLinksToolStripMenuItem"
		Me.followLinksToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.followLinksToolStripMenuItem.Text = "Follow links"
		'
		'toolStripSeparator17
		'
		Me.toolStripSeparator17.Name = "toolStripSeparator17"
		Me.toolStripSeparator17.Size = New System.Drawing.Size(202, 6)
		'
		'saveSettingsToolStripMenuItem
		'
		Me.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem"
		Me.saveSettingsToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
		Me.saveSettingsToolStripMenuItem.Text = "Save settings"
		AddHandler Me.saveSettingsToolStripMenuItem.Click, AddressOf Me.SaveSettingsToolStripMenuItemClick
		'
		'toolStripSeparator24
		'
		Me.toolStripSeparator24.Name = "toolStripSeparator24"
		Me.toolStripSeparator24.Size = New System.Drawing.Size(202, 6)
		'
		'projectToolStripMenuItem1
		'
		Me.projectToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.autoUpdateStatusToolStripMenuItem, Me.checkForOrphansToolStripMenuItem, Me.parseXMLOnLoadToolStripMenuItem, Me.checkaddMD5FileHashToolStripMenuItem})
		Me.projectToolStripMenuItem1.Image = CType(resources.GetObject("projectToolStripMenuItem1.Image"),System.Drawing.Image)
		Me.projectToolStripMenuItem1.Name = "projectToolStripMenuItem1"
		Me.projectToolStripMenuItem1.Size = New System.Drawing.Size(205, 22)
		Me.projectToolStripMenuItem1.Text = "Project"
		'
		'autoUpdateStatusToolStripMenuItem
		'
		Me.autoUpdateStatusToolStripMenuItem.Checked = true
		Me.autoUpdateStatusToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.autoUpdateStatusToolStripMenuItem.Name = "autoUpdateStatusToolStripMenuItem"
		Me.autoUpdateStatusToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
		Me.autoUpdateStatusToolStripMenuItem.Text = "Auto update status"
		AddHandler Me.autoUpdateStatusToolStripMenuItem.Click, AddressOf Me.AutoUpdateStatusToolStripMenuItemClick
		'
		'checkForOrphansToolStripMenuItem
		'
		Me.checkForOrphansToolStripMenuItem.Checked = true
		Me.checkForOrphansToolStripMenuItem.CheckOnClick = true
		Me.checkForOrphansToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
		Me.checkForOrphansToolStripMenuItem.Name = "checkForOrphansToolStripMenuItem"
		Me.checkForOrphansToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
		Me.checkForOrphansToolStripMenuItem.Text = "Check for orphans"
		AddHandler Me.checkForOrphansToolStripMenuItem.Click, AddressOf Me.CheckForOrphansToolStripMenuItemClick
		'
		'parseXMLOnLoadToolStripMenuItem
		'
		Me.parseXMLOnLoadToolStripMenuItem.Name = "parseXMLOnLoadToolStripMenuItem"
		Me.parseXMLOnLoadToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
		Me.parseXMLOnLoadToolStripMenuItem.Text = "Parse X(HT)ML on load"
		AddHandler Me.parseXMLOnLoadToolStripMenuItem.Click, AddressOf Me.ParseXMLOnLoadToolStripMenuItemClick
		'
		'checkaddMD5FileHashToolStripMenuItem
		'
		Me.checkaddMD5FileHashToolStripMenuItem.CheckOnClick = true
		Me.checkaddMD5FileHashToolStripMenuItem.Name = "checkaddMD5FileHashToolStripMenuItem"
		Me.checkaddMD5FileHashToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
		Me.checkaddMD5FileHashToolStripMenuItem.Text = "Compare files"
		AddHandler Me.checkaddMD5FileHashToolStripMenuItem.Click, AddressOf Me.CheckaddMD5FileHashToolStripMenuItemClick
		'
		'helpToolStripMenuItem
		'
		Me.helpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.yadaYadaToolStripMenuItem})
		Me.helpToolStripMenuItem.Name = "helpToolStripMenuItem"
		Me.helpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
		Me.helpToolStripMenuItem.Text = "Help"
		Me.helpToolStripMenuItem.Visible = false
		'
		'yadaYadaToolStripMenuItem
		'
		Me.yadaYadaToolStripMenuItem.Name = "yadaYadaToolStripMenuItem"
		Me.yadaYadaToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
		Me.yadaYadaToolStripMenuItem.Text = "yada yada ..."
		'
		'annotateModeToolStripMenuItem
		'
		Me.annotateModeToolStripMenuItem.ForeColor = System.Drawing.Color.DimGray
		Me.annotateModeToolStripMenuItem.Name = "annotateModeToolStripMenuItem"
		Me.annotateModeToolStripMenuItem.Size = New System.Drawing.Size(472, 20)
		Me.annotateModeToolStripMenuItem.Text = "Annotation Mode    Press Ctrl-S to Save,  Ctrl-Q to Quit,  Right-click screen for"& _ 
		" options"
		Me.annotateModeToolStripMenuItem.Visible = false
		'
		'contextMenuStripStatus
		'
		Me.contextMenuStripStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uncheckedToolStripMenuItem, Me.checkedToolStripMenuItem, Me.oKToolStripMenuItem, Me.nGToolStripMenuItem, Me.flaggedToolStripMenuItem, Me.problemToolStripMenuItem, Me.warningToolStripMenuItem})
		Me.contextMenuStripStatus.Name = "contextMenuStripStatus"
		Me.contextMenuStripStatus.ShowCheckMargin = true
		Me.contextMenuStripStatus.ShowImageMargin = false
		Me.contextMenuStripStatus.Size = New System.Drawing.Size(133, 158)
		AddHandler Me.contextMenuStripStatus.ItemClicked, AddressOf Me.ContextMenuStripStatusItemClicked
		AddHandler Me.contextMenuStripStatus.MouseLeave, AddressOf Me.ContextMenuStripStatusMouseLeave
		'
		'uncheckedToolStripMenuItem
		'
		Me.uncheckedToolStripMenuItem.ForeColor = System.Drawing.Color.DarkGray
		Me.uncheckedToolStripMenuItem.Name = "uncheckedToolStripMenuItem"
		Me.uncheckedToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.uncheckedToolStripMenuItem.Text = "unchecked"
		'
		'checkedToolStripMenuItem
		'
		Me.checkedToolStripMenuItem.Name = "checkedToolStripMenuItem"
		Me.checkedToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.checkedToolStripMenuItem.Text = "checked"
		'
		'oKToolStripMenuItem
		'
		Me.oKToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
		Me.oKToolStripMenuItem.ForeColor = System.Drawing.Color.Green
		Me.oKToolStripMenuItem.Name = "oKToolStripMenuItem"
		Me.oKToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.oKToolStripMenuItem.Text = "OK"
		'
		'nGToolStripMenuItem
		'
		Me.nGToolStripMenuItem.ForeColor = System.Drawing.Color.Red
		Me.nGToolStripMenuItem.Name = "nGToolStripMenuItem"
		Me.nGToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.nGToolStripMenuItem.Text = "NG"
		'
		'flaggedToolStripMenuItem
		'
		Me.flaggedToolStripMenuItem.ForeColor = System.Drawing.Color.CadetBlue
		Me.flaggedToolStripMenuItem.Name = "flaggedToolStripMenuItem"
		Me.flaggedToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.flaggedToolStripMenuItem.Text = "Flagged"
		Me.flaggedToolStripMenuItem.Visible = false
		'
		'problemToolStripMenuItem
		'
		Me.problemToolStripMenuItem.ForeColor = System.Drawing.Color.Red
		Me.problemToolStripMenuItem.Name = "problemToolStripMenuItem"
		Me.problemToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.problemToolStripMenuItem.Text = "Problem"
		Me.problemToolStripMenuItem.Visible = false
		'
		'warningToolStripMenuItem
		'
		Me.warningToolStripMenuItem.ForeColor = System.Drawing.Color.Orange
		Me.warningToolStripMenuItem.Name = "warningToolStripMenuItem"
		Me.warningToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
		Me.warningToolStripMenuItem.Text = "Warning"
		Me.warningToolStripMenuItem.Visible = false
		'
		'contextMenuStripFileItem
		'
		Me.contextMenuStripFileItem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.openInEditorToolStripMenuItem, Me.toolStripSeparator16, Me.toolStripMenuItem5, Me.toolStripMenuItem6, Me.toolStripMenuItem7, Me.toolStripMenuItem8, Me.uncheckedToolStripMenuItem1})
		Me.contextMenuStripFileItem.Name = "contextMenuStripStatus"
		Me.contextMenuStripFileItem.ShowCheckMargin = true
		Me.contextMenuStripFileItem.ShowImageMargin = false
		Me.contextMenuStripFileItem.Size = New System.Drawing.Size(151, 142)
		AddHandler Me.contextMenuStripFileItem.ItemClicked, AddressOf Me.ContextMenuStripFileItemItemClicked
		'
		'openInEditorToolStripMenuItem
		'
		Me.openInEditorToolStripMenuItem.Name = "openInEditorToolStripMenuItem"
		Me.openInEditorToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
		Me.openInEditorToolStripMenuItem.Text = "Open in editor"
		AddHandler Me.openInEditorToolStripMenuItem.Click, AddressOf Me.OpenInEditorToolStripMenuItemClick
		'
		'toolStripSeparator16
		'
		Me.toolStripSeparator16.Name = "toolStripSeparator16"
		Me.toolStripSeparator16.Size = New System.Drawing.Size(147, 6)
		'
		'toolStripMenuItem5
		'
		Me.toolStripMenuItem5.Name = "toolStripMenuItem5"
		Me.toolStripMenuItem5.Size = New System.Drawing.Size(150, 22)
		Me.toolStripMenuItem5.Text = "checked"
		'
		'toolStripMenuItem6
		'
		Me.toolStripMenuItem6.BackColor = System.Drawing.SystemColors.Control
		Me.toolStripMenuItem6.ForeColor = System.Drawing.Color.Green
		Me.toolStripMenuItem6.Name = "toolStripMenuItem6"
		Me.toolStripMenuItem6.Size = New System.Drawing.Size(150, 22)
		Me.toolStripMenuItem6.Text = "OK"
		'
		'toolStripMenuItem7
		'
		Me.toolStripMenuItem7.ForeColor = System.Drawing.Color.Red
		Me.toolStripMenuItem7.Name = "toolStripMenuItem7"
		Me.toolStripMenuItem7.Size = New System.Drawing.Size(150, 22)
		Me.toolStripMenuItem7.Text = "NG"
		'
		'toolStripMenuItem8
		'
		Me.toolStripMenuItem8.ForeColor = System.Drawing.Color.CadetBlue
		Me.toolStripMenuItem8.Name = "toolStripMenuItem8"
		Me.toolStripMenuItem8.Size = New System.Drawing.Size(150, 22)
		Me.toolStripMenuItem8.Text = "Flagged"
		Me.toolStripMenuItem8.Visible = false
		'
		'uncheckedToolStripMenuItem1
		'
		Me.uncheckedToolStripMenuItem1.ForeColor = System.Drawing.Color.DarkGray
		Me.uncheckedToolStripMenuItem1.Name = "uncheckedToolStripMenuItem1"
		Me.uncheckedToolStripMenuItem1.Size = New System.Drawing.Size(150, 22)
		Me.uncheckedToolStripMenuItem1.Text = "unchecked"
		'
		'fileSystemWatcher1
		'
		Me.fileSystemWatcher1.EnableRaisingEvents = true
		Me.fileSystemWatcher1.NotifyFilter = System.IO.NotifyFilters.LastWrite
		Me.fileSystemWatcher1.SynchronizingObject = Me
		AddHandler Me.fileSystemWatcher1.Changed, AddressOf Me.FileSystemWatcher1Changed
		'
		't
		'
		Me.t.Interval = 250
		AddHandler Me.t.Tick, AddressOf Me.TTick
		'
		'imageList1
		'
		Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"),System.Windows.Forms.ImageListStreamer)
		Me.imageList1.TransparentColor = System.Drawing.Color.Transparent
		Me.imageList1.Images.SetKeyName(0, "play.png")
		Me.imageList1.Images.SetKeyName(1, "stop.png")
		Me.imageList1.Images.SetKeyName(2, "pause.png")
		'
		'imageListIcons
		'
		Me.imageListIcons.ImageStream = CType(resources.GetObject("imageListIcons.ImageStream"),System.Windows.Forms.ImageListStreamer)
		Me.imageListIcons.TransparentColor = System.Drawing.Color.Transparent
		Me.imageListIcons.Images.SetKeyName(0, "search.png")
		'
		'imageList2
		'
		Me.imageList2.ImageStream = CType(resources.GetObject("imageList2.ImageStream"),System.Windows.Forms.ImageListStreamer)
		Me.imageList2.TransparentColor = System.Drawing.Color.Transparent
		Me.imageList2.Images.SetKeyName(0, "unpinned.png")
		Me.imageList2.Images.SetKeyName(1, "pinned.png")
		'
		'startAnnotate
		'
		AddHandler Me.startAnnotate.Tick, AddressOf Me.TimerAnnotateTick
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(704, 468)
		Me.Controls.Add(Me.panelContents)
		Me.Controls.Add(Me.panel3)
		Me.Controls.Add(Me.menuStrip1)
		Me.DoubleBuffered = true
		Me.Font = New System.Drawing.Font("Tahoma", 11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.MainMenuStrip = Me.menuStrip1
		Me.MinimumSize = New System.Drawing.Size(654, 506)
		Me.Name = "MainForm"
		Me.Text = "Visual QA"
		AddHandler Load, AddressOf Me.MainFormLoad
		AddHandler Resize, AddressOf Me.MainFormResize
		Me.panel2.ResumeLayout(false)
		Me.panel2Contents.ResumeLayout(false)
		Me.panel2Contents.PerformLayout
		CType(Me.pictureBox2,System.ComponentModel.ISupportInitialize).EndInit
		Me.panel2Info.ResumeLayout(false)
		Me.panelTargetInfo.ResumeLayout(false)
		Me.panelTargetInfo.PerformLayout
		Me.contextMenuStripInfo.ResumeLayout(false)
		Me.contextMenuStripEncoding.ResumeLayout(false)
		CType(Me.pictureBox5,System.ComponentModel.ISupportInitialize).EndInit
		Me.contextMenuStripPinToSource.ResumeLayout(false)
		Me.panelBrowser.ResumeLayout(false)
		Me.splitContainer1.Panel1.ResumeLayout(false)
		Me.splitContainer1.Panel2.ResumeLayout(false)
		Me.splitContainer1.ResumeLayout(false)
		Me.contextMenuStripCompare.ResumeLayout(false)
		Me.panel1.ResumeLayout(false)
		Me.panel1Contents.ResumeLayout(false)
		Me.panel1Contents.PerformLayout
		CType(Me.pictureBox1,System.ComponentModel.ISupportInitialize).EndInit
		Me.panel1Info.ResumeLayout(false)
		Me.panelSourceInfo.ResumeLayout(false)
		Me.panelSourceInfo.PerformLayout
		CType(Me.pictureBox4,System.ComponentModel.ISupportInitialize).EndInit
		Me.contextMenuStripPinToTarget.ResumeLayout(false)
		Me.panelContents.ResumeLayout(false)
		Me.toolStripContainerMainForm.ContentPanel.ResumeLayout(false)
		Me.toolStripContainerMainForm.ResumeLayout(false)
		Me.toolStripContainerMainForm.PerformLayout
		Me.splitContainerContents.Panel1.ResumeLayout(false)
		Me.splitContainerContents.Panel2.ResumeLayout(false)
		Me.splitContainerContents.ResumeLayout(false)
		Me.contextMenuStripFileList.ResumeLayout(false)
		Me.tabControl1.ResumeLayout(false)
		Me.tabPage1.ResumeLayout(false)
		Me.tabPage1.PerformLayout
		Me.tabPage2.ResumeLayout(false)
		Me.contextMenuLog.ResumeLayout(false)
		Me.searchPanel.ResumeLayout(false)
		Me.searchPanel.PerformLayout
		Me.contextMenuStripSearch.ResumeLayout(false)
		Me.contextMenuStripSearchColor.ResumeLayout(false)
		Me.contextMenuStripTreeFilter.ResumeLayout(false)
		Me.contextMenuStripFilter.ResumeLayout(false)
		Me.panel3.ResumeLayout(false)
		Me.panel3.PerformLayout
		Me.panel5.ResumeLayout(false)
		CType(Me.pictureBox3,System.ComponentModel.ISupportInitialize).EndInit
		Me.panel4.ResumeLayout(false)
		Me.panel4.PerformLayout
		Me.scrollPanel.ResumeLayout(false)
		Me.menuStrip1.ResumeLayout(false)
		Me.menuStrip1.PerformLayout
		Me.contextMenuStripTools.ResumeLayout(false)
		Me.contextMenuStripStatus.ResumeLayout(false)
		Me.contextMenuStripFileItem.ResumeLayout(false)
		CType(Me.fileSystemWatcher1,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private IE12ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemSize4 As System.Windows.Forms.ToolStripMenuItem
	Private IE11ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private showBrowserWidthToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator26 As System.Windows.Forms.ToolStripSeparator
	Private labelSize1 As System.Windows.Forms.Label
	Private labelSize2 As System.Windows.Forms.Label
	Private excludeScrollbarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemSize3 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemSize2 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemSize1 As System.Windows.Forms.ToolStripMenuItem
	Private browserSizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator27 As System.Windows.Forms.ToolStripSeparator
	Private startAnnotate As System.Windows.Forms.Timer
	Private IE10ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private splitViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private collapseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private colorDialog1 As System.Windows.Forms.ColorDialog
	Private backgroundColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripPinToTarget As System.Windows.Forms.ContextMenuStrip
	Private pinToOtherSideToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripPinToSource As System.Windows.Forms.ContextMenuStrip
	Private pinToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private pictureBox5 As System.Windows.Forms.PictureBox
	Private imageList2 As System.Windows.Forms.ImageList
	Private pictureBox4 As System.Windows.Forms.PictureBox
	Private pictureBox3 As System.Windows.Forms.PictureBox
	Private panel5 As System.Windows.Forms.Panel
	Private label1 As System.Windows.Forms.Label
	Private IE9ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private clearLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private saveLogToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuLog As System.Windows.Forms.ContextMenuStrip
	Private dEBUGToClipboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripHighlightPlugins As System.Windows.Forms.ContextMenuStrip
	Private pluginsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator25 As System.Windows.Forms.ToolStripSeparator
	Private openTargetFileInEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private openSourceFileInEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private openFilesInCompareToolToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator19 As System.Windows.Forms.ToolStripSeparator
	Private contextMenuStripTools As System.Windows.Forms.ContextMenuStrip
	Private toolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator24 As System.Windows.Forms.ToolStripSeparator
	Private saveSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private readSavedSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator23 As System.Windows.Forms.ToolStripSeparator
	Private deleteSavedSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private searchColorToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator22 As System.Windows.Forms.ToolStripSeparator
	Private clearSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private saveSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripSearch As System.Windows.Forms.ContextMenuStrip
	Private IE7ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private IE8ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private synchronizedScrollToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
	Private searchColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private limeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private cyanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private yellowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private magentaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripSearchColor As System.Windows.Forms.ContextMenuStrip
	Private copyTitleToClipboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private annotateModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private ProjectFilterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private activateFilterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private imageListIcons As System.Windows.Forms.ImageList
	Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private resetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
	Private filterComments As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
	Private filterProblem As System.Windows.Forms.ToolStripMenuItem
	Private filterOrphans As System.Windows.Forms.ToolStripMenuItem
	Private filterNg As System.Windows.Forms.ToolStripMenuItem
	Private filterOk As System.Windows.Forms.ToolStripMenuItem
	Private filterUnchecked As System.Windows.Forms.ToolStripMenuItem
	Private filterChecked As System.Windows.Forms.ToolStripMenuItem
	Private annotateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private autoScrollToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private imageList1 As System.Windows.Forms.ImageList
	Private scrollPanel As System.Windows.Forms.Panel
	Private btnToggleScroll As System.Windows.Forms.Button
	Private t As System.Windows.Forms.Timer
	Private followLinksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator18 As System.Windows.Forms.ToolStripSeparator
	Private highlightLocalLinksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private highlightUITermsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
	Private showSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private searchPanel As System.Windows.Forms.Panel
	Private button2 As System.Windows.Forms.Button
	Private textBox1 As System.Windows.Forms.TextBox
	Private uncheckedToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private autoRefreshAfterEditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fileSystemWatcher1 As System.IO.FileSystemWatcher
	Private contextMenuStripFileItem As System.Windows.Forms.ContextMenuStrip
	Private toolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
	Private openInEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
	Private clearFilterShowAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
	Private contextMenuStripTreeFilter As System.Windows.Forms.ContextMenuStrip
	Private saveProjectToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private flaggedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private nGToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private noOrphansToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripFilter As System.Windows.Forms.ContextMenuStrip
	Private toolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
	Private toolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
	Private showCommentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private checkaddMD5FileHashToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private checkForOrphansToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private settingsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private refreshToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private clearLogToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private saveLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
	Private saveReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fitImagesToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private fitImagesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private originalSizetoolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
	Private textBoxJump As System.Windows.Forms.TextBox
	Private labelImgInfo1 As System.Windows.Forms.Label
	Private labelImgInfo2 As System.Windows.Forms.Label
	Private useMouseGesturesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private imageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private warningToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private dockToTopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripFileList As System.Windows.Forms.ContextMenuStrip
	Private webBrowserLog As System.Windows.Forms.WebBrowser
	Private parseXMLOnLoadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private projectToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
	Private imageListSmallIcons As System.Windows.Forms.ImageList
	Private toolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
	Private preferedCompareToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
	Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private openFoldersInCompareToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private openFilesInCompareToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private yadaYadaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripCompare As System.Windows.Forms.ContextMenuStrip
	Private autoUpdateStatusToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private labelStatus As System.Windows.Forms.Label
	Private problemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private oKToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private checkedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private uncheckedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripStatus As System.Windows.Forms.ContextMenuStrip
	Private tabPage2 As System.Windows.Forms.TabPage
	Private tabPage1 As System.Windows.Forms.TabPage
	Private tabControl1 As System.Windows.Forms.TabControl
	Private OpenContainingFoldertoolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private showFileListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private treeView1 As System.Windows.Forms.TreeView
	Private splitContainerContents As System.Windows.Forms.SplitContainer
	Private saveProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private openProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripContainerMainForm As System.Windows.Forms.ToolStripContainer
	Private labelCounter As System.Windows.Forms.Label
	Private sourceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private targetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
	Private refreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private historyBackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
	Private ChangeEncodingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private preferedEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private copyFileNameToClipboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private copyLocationToClipboardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
	Private editorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private browserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private openFileInToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStripInfo As System.Windows.Forms.ContextMenuStrip
	Private contextMenuStripEncoding As System.Windows.Forms.ContextMenuStrip
	Private encodingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private newProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private textBoxTitle2 As System.Windows.Forms.TextBox
	Private textBoxTitle1 As System.Windows.Forms.TextBox
	Private labelFileInfo1 As System.Windows.Forms.Label
	Private labelFileInfo2 As System.Windows.Forms.Label
	Private labelEncoding1 As System.Windows.Forms.Label
	Private labelEncoding2 As System.Windows.Forms.Label
	Private panelTargetInfo As System.Windows.Forms.Panel
	Private panelSourceInfo As System.Windows.Forms.Panel
	Private hideControlsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private showInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
	Private panel1Info As System.Windows.Forms.Panel
	Private panel2Info As System.Windows.Forms.Panel
	Private panel2Contents As System.Windows.Forms.Panel
	Private panel1Contents As System.Windows.Forms.Panel
	Private dualscreenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fullScreenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private pictureBox1 As System.Windows.Forms.PictureBox
	Private pictureBox2 As System.Windows.Forms.PictureBox
	Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private openTargetFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private openSourceFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private splitSizeFixedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private targetOnlyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private sourceOnlyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private splitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private horizontalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private verticalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private orientationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private viewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private buttonPreviousOff As System.Windows.Forms.Button
	Private buttonNextOff As System.Windows.Forms.Button
	Private exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private closeProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private alternative2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private alternativeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private defaultToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private modeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private settingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private projectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private menuStrip1 As System.Windows.Forms.MenuStrip
	Private buttonPrevious As System.Windows.Forms.Button
	Private buttonNext As System.Windows.Forms.Button
	Private panel4 As System.Windows.Forms.Panel
	Private buttonLineUp As System.Windows.Forms.Button
	Private buttonLineDown As System.Windows.Forms.Button
	Private buttonPageDown As System.Windows.Forms.Button
	Private buttonPageUp As System.Windows.Forms.Button
	Private panel3 As System.Windows.Forms.Panel
	Private panelBrowser As System.Windows.Forms.Panel
	Private checkBox1 As System.Windows.Forms.CheckBox
	' Private webBrowser1 As System.Windows.Forms.WebBrowser
	Private panel1 As System.Windows.Forms.Panel
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private panelContents As System.Windows.Forms.Panel
	Private panel2 As System.Windows.Forms.Panel
	' Private webBrowser2 As System.Windows.Forms.WebBrowser

End Class
