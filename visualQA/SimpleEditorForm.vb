' contains some code for using events to send data to the main form
' main form contains the following code:
'
'	Public Sub HandleCommentUpdated(commentText As String, commentId As Integer)
'    	' things to do when event is executed
'    	MsgBox(commentId & " : " & commentText)
'	End Sub

Public Partial Class SimpleEditorForm
	Dim private commentText As String
	Dim Private commentId As Integer
	Dim private commentTitle As String = "Edit Comment"
		
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		' add handler for commenting	
 		AddHandler commentUpdated, AddressOf MainForm.HandleCommentUpdated
	End Sub
	
	Public Event commentUpdated(commentText As String, commentId As Integer)
	
	Public Property comment() As String	
		Get 
			Return commentText
		End Get
		Set (ByVal textIn As String)
			commentText = textIn
		End Set
	End Property

	Public Property title() As String	
		Get 
			Return commentTitle
		End Get
		Set (ByVal textIn As String)
			commentTitle = textIn
		End Set
	End Property
	
	Public Property id() As Integer	
		Get 
			Return commentId
		End Get
		Set (ByVal valueIn As Integer)
			commentID = valueIn
		End Set
	End Property	
	
	Sub ToolStripButton1Click(ByVal sender As Object, ByVal e As EventArgs)
		Me.Close
	End Sub
	
	Sub ToolStripButton2Click(ByVal sender As Object, ByVal e As EventArgs)
		commentText = richTextbox1.Text
		RaiseEvent commentUpdated(commentText , commentId)
		Me.Close
	End Sub
	
	Sub SimpleEditorFormLoad(ByVal sender As Object, ByVal e As EventArgs)
		richTextbox1.Text = commentText
		me.Text = commentTitle
	End Sub
End Class
