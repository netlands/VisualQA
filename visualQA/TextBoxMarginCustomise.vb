Namespace vbAccelerator.Components.Controls


    ''' <summary>
    ''' A class for adding right and left margins to a TextBox
    ''' or the embedded TextBox within a DropDown ComboBox.
    ''' The margin can display an m_icon, a m_control or can
    ''' be drawn using a custom routine implemented by the
    ''' user.
    ''' </summary>
    Public Class TextBoxMarginCustomise
        Inherits NativeWindow


#Region "Unmanaged Code"
        <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)> _
        Private Structure RECT
            Public left As Integer
            Public top As Integer
            Public right As Integer
            Public bottom As Integer
        End Structure

        Private Declare Auto Function SendMessage Lib "user32" ( _
            ByVal hwnd As IntPtr, _
            ByVal wMsg As Integer, _
            ByVal wParam As Integer, _
            ByVal lParam As Integer _
            ) As Integer

        Private Declare Auto Function FindWindowEx Lib "user32" ( _
            ByVal hwndParent As IntPtr, _
            ByVal hwndChildAfter As IntPtr, _
            <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)> _
            ByVal lpszClass As String, _
            <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)> _
            ByVal lpszWindow As String _
            ) As IntPtr
        Private Declare Auto Function GetWindowLong Lib "user32" ( _
            ByVal hWnd As IntPtr, _
            ByVal dwStyle As Integer _
            ) As Integer
        Private Declare Function GetDC Lib "user32" ( _
            ByVal hwnd As IntPtr _
            ) As IntPtr

        Private Declare Function ReleaseDC Lib "user32" ( _
         ByVal hwnd As IntPtr, _
         ByVal hdc As IntPtr _
        ) As Integer

        Private Declare Function GetClientRect Lib "user32" ( _
         ByVal hwnd As IntPtr, _
         ByRef rc As RECT _
         ) As Integer
        Private Declare Function GetWindowRect Lib "user32" ( _
         ByVal hwnd As IntPtr, _
         ByRef rc As RECT _
         ) As Integer

        Private Const EC_LEFTMARGIN As Integer = &H1
        Private Const EC_RIGHTMARGIN As Integer = &H2
        Private Const EC_USEFONTINFO As Integer = &HFFFF
        Private Const EM_SETMARGINS As Integer = &HD3
        Private Const EM_GETMARGINS As Integer = &HD4

        Private Const WM_PAINT As Integer = &HF

        Private Const WM_SETFOCUS As Integer = &H7
        Private Const WM_KILLFOCUS As Integer = &H8

        Private Const WM_SETFONT As Integer = &H30

        Private Const WM_MOUSEMOVE As Integer = &H200
        Private Const WM_LBUTTONDOWN As Integer = &H201
        Private Const WM_RBUTTONDOWN As Integer = &H204
        Private Const WM_MBUTTONDOWN As Integer = &H207
        Private Const WM_LBUTTONUP As Integer = &H202
        Private Const WM_RBUTTONUP As Integer = &H205
        Private Const WM_MBUTTONUP As Integer = &H208
        Private Const WM_LBUTTONDBLCLK As Integer = &H203
        Private Const WM_RBUTTONDBLCLK As Integer = &H206
        Private Const WM_MBUTTONDBLCLK As Integer = &H209

        Private Const WM_KEYDOWN As Integer = &H100
        Private Const WM_KEYUP As Integer = &H101
        Private Const WM_CHAR As Integer = &H102

        Private Const GWL_EXSTYLE As Integer = (-20)
        Private Const WS_EX_RIGHT As Integer = &H1000
        Private Const WS_EX_LEFT As Integer = &H0
        Private Const WS_EX_RTLREADING As Integer = &H2000
        Private Const WS_EX_LTRREADING As Integer = &H0
        Private Const WS_EX_LEFTSCROLLBAR As Integer = &H4000
        Private Const WS_EX_RIGHTSCROLLBAR As Integer = &H0

#End Region

#Region "Member Variables"
        Private m_imageList As System.Windows.Forms.ImageList = Nothing
        Private m_icon As Integer = -1
        Private m_control As Control = Nothing
        Private m_customPainter As ITextBoxMarginCustomisePainter = Nothing
        Private customWidth As Integer = 0
#End Region

        ''' <summary>
        ''' Gets whether a Window is <c>RightToLeft.Yes</c> from
        ''' its <c>Handle</c>.
        ''' </summary>
        ''' <param name="theHandle">Handle of window to check.</param>
        ''' <returns><c>True</c> if Window is RightToLeft, <c>False</c> otherwise.</returns>
        Private Shared Function IsRightToLeft(ByVal theHandle As IntPtr) As Boolean
            Dim style As Integer = GetWindowLong(theHandle, GWL_EXSTYLE)
            IsRightToLeft = _
             ((style And WS_EX_RIGHT) = WS_EX_RIGHT) Or _
             ((style And WS_EX_RTLREADING) = WS_EX_RTLREADING) Or _
             ((style And WS_EX_LEFTSCROLLBAR) = WS_EX_LEFTSCROLLBAR)
        End Function

        ''' <summary>
        ''' Gets the far margin of a TextBox m_control or the
        ''' TextBox contained within a ComboBox.
        ''' </summary>
        ''' <param name="ctl">The Control to get the far margin
        ''' for</param>
        ''' <returns>Far margin, in pixels.</returns>
        Public Shared Function FarMargin(ByVal ctl As Control) As Integer

            Dim theHandle As IntPtr = ctl.Handle

            If TypeOf ctl Is System.Windows.Forms.ComboBox Then
                theHandle = ComboEdithWnd(theHandle)
            End If

            FarMargin = FarMargin(theHandle)

        End Function

        Private Shared Function FarMargin(ByVal theHandle As IntPtr) As Integer
            Dim theMargin As Integer = SendMessage(theHandle, EM_GETMARGINS, 0, 0)
            If (IsRightToLeft(theHandle)) Then
                theMargin = theMargin And &HFFFF
            Else
                theMargin = (theMargin / &H10000)
            End If
            FarMargin = theMargin
        End Function


        ''' <summary>
        ''' Sets the far margin of a TextBox m_control or the
        ''' TextBox contained within a ComboBox.
        ''' </summary>
        ''' <param name="ctl">The Control to set the far margin
        ''' for</param>
        ''' <param name="margin">New far margin in pixels, or -1
        ''' to use the default far margin.</param>
        Public Shared Sub FarMargin(ByVal ctl As Control, ByVal margin As Integer)
            Dim theHandle As IntPtr = ctl.Handle
            If TypeOf ctl Is System.Windows.Forms.ComboBox Then
                theHandle = ComboEdithWnd(theHandle)
            End If
            FarMargin(theHandle, margin)
        End Sub

        Private Shared Sub FarMargin(ByVal theHandle As IntPtr, ByVal margin As Integer)
            Dim message As Integer = IIf(IsRightToLeft(theHandle), EC_LEFTMARGIN, EC_RIGHTMARGIN)
            If (message = EC_LEFTMARGIN) Then
                margin = margin & &HFFFF
            Else
                margin = margin * &H10000
            End If
            SendMessage(theHandle, EM_SETMARGINS, message, margin)
        End Sub

        ''' <summary>
        ''' Gets the near margin of a TextBox m_control or the
        ''' TextBox contained within a ComboBox.
        ''' </summary>
        ''' <param name="ctl">The Control to get the near margin
        ''' for</param>
        ''' <returns>Near margin, in pixels.</returns>
        Public Shared Function NearMargin(ByVal ctl As Control) As Integer
            Dim theHandle As IntPtr = ctl.Handle
            If TypeOf ctl Is System.Windows.Forms.ComboBox Then
                theHandle = ComboEdithWnd(theHandle)
            End If
            NearMargin = NearMargin(theHandle)
        End Function

        Private Shared Function NearMargin(ByVal theHandle As IntPtr) As Integer
            Dim theMargin As Integer = SendMessage(theHandle, EM_GETMARGINS, 0, 0)
            If (IsRightToLeft(theHandle)) Then
                theMargin = theMargin / &H10000
            Else
                theMargin = theMargin And &HFFFF
            End If
            NearMargin = theMargin
        End Function

        ''' <summary>
        ''' Sets the near margin of a TextBox m_control or the
        ''' TextBox contained within a ComboBox.
        ''' </summary>
        ''' <param name="ctl">The Control to set the near margin
        ''' for</param>
        ''' <param name="margin">New near margin in pixels, or -1
        ''' to use the default near margin.</param>
        Public Shared Sub NearMargin(ByVal ctl As Control, ByVal margin As Integer)
            Dim theHandle As IntPtr = ctl.Handle
            If TypeOf ctl Is System.Windows.Forms.ComboBox Then
                theHandle = ComboEdithWnd(theHandle)
            End If
            NearMargin(theHandle, margin)
        End Sub

        Private Shared Sub NearMargin(ByVal theHandle As IntPtr, ByVal margin As Integer)
            Dim message As Integer = IIf(IsRightToLeft(theHandle), EC_RIGHTMARGIN, EC_LEFTMARGIN)
            If (message = EC_LEFTMARGIN) Then
                margin = margin And &HFFFF
            Else
                margin = margin * &H10000
            End If
            SendMessage(theHandle, EM_SETMARGINS, message, margin)
        End Sub

        ''' <summary>
        ''' Gets the handle of the TextBox contained within a
        ''' ComboBox m_control.
        ''' </summary>
        ''' <param name="handle">The ComboBox window handle.</param>
        ''' <returns>The handle of the contained text box.</returns>
        Public Shared Function ComboEdithWnd(ByVal theHandle As IntPtr) As IntPtr
            Dim findHandle As IntPtr = FindWindowEx(theHandle, IntPtr.Zero, "EDIT", vbNullString)
            ComboEdithWnd = findHandle
        End Function

        ''' <summary>
        ''' Attaches an instance of this class to a ComboBox m_control.
        ''' The m_control must have the "DropDown" style so it has a
        ''' TextBox.
        ''' </summary>
        ''' <param name="comboBox">ComboBox with DropDown style to
        ''' attach to.</param>
        ''' <remarks>Use the <see cref="AssignHandle"/> method to attach
        ''' this class to an arbitrary TextBox m_control using its
        ''' handle.</remarks>
        Public Sub Attach(ByVal comboBox As System.Windows.Forms.ComboBox)
            If Not (Me.Handle.Equals(IntPtr.Zero)) Then
                Me.ReleaseHandle()
            End If
            Dim theHandle As IntPtr = ComboEdithWnd(comboBox.Handle)
            Me.AssignHandle(theHandle)
            setMargin()
        End Sub

        ''' <summary>
        ''' Attaches an instance of this class to a TextBox m_control.
        ''' </summary>
        ''' <param name="textBox">TextBox to attach to.</param>
        ''' <remarks>Use the <see cref="AssignHandle"/> method to attach
        ''' this class to an arbitrary TextBox m_control using its
        ''' handle.</remarks>
        Public Sub Attach(ByVal textBox As System.Windows.Forms.TextBox)
            If Not (Me.Handle.Equals(IntPtr.Zero)) Then
                Me.ReleaseHandle()
            End If
            Me.AssignHandle(textBox.Handle)
            setMargin()
        End Sub

        ''' <summary>
        ''' Gets/sets the ImageList used to draw icons in the control.
        ''' </summary>
        Public Property ImageList() As System.Windows.Forms.ImageList
            Get
                ImageList = m_imageList
            End Get
            Set(ByVal Value As System.Windows.Forms.ImageList)
                m_imageList = Value
                setMargin()
            End Set
        End Property

        ''' <summary>
        ''' Gets/sets the 0-based icon index to draw in the control.
        ''' If the index is set < 0 then the icon is not drawn.
        ''' </summary>
        Public Property Icon() As Integer
            Get
                Icon = m_icon
            End Get
            Set(ByVal Value As Integer)
                m_icon = Value
                setMargin()
            End Set
        End Property

        ''' <summary>
        ''' Gets/sets the control to be displayed in the near margin.
        ''' The <see cref="ImageList"/> property must be <c>Nothing</c> if you
        ''' want to display a m_control.
        ''' </summary>
        Public Property Control() As System.Windows.Forms.Control
            Get
                Control = m_control
            End Get
            Set(ByVal Value As System.Windows.Forms.Control)
                m_control = Value
                setMargin()
            End Set
        End Property


        ''' <summary>
        ''' Gets/sets a class which implements the <see cref="ITextBoxMarginCustomisePainter "/>
        ''' interface to perform customised painting in the margin.
        ''' The <see cref="ImageList"/> and <see cref="Control"/> properties must
        ''' be <c>Nothing</c> if you want to use this technique.
        ''' </summary>
        Public Property CustomPainter() As ITextBoxMarginCustomisePainter
            Get
                CustomPainter = m_customPainter
            End Get
            Set(ByVal Value As ITextBoxMarginCustomisePainter)
                m_customPainter = Value
                setMargin()
            End Set
        End Property


        ''' <summary>
        ''' Sets the near margin to accommodate the specified m_control.
        ''' </summary>
        Private Sub setMargin()
            If Not (Me.Handle.Equals(IntPtr.Zero)) Then
                If Not (m_control Is Nothing) Then
                    NearMargin(Me.Handle, m_control.Width + 4)
                    moveControl()
                ElseIf (m_icon > -1) Then
                    If Not (m_imageList Is Nothing) Then
                        NearMargin(Me.Handle, m_imageList.ImageSize.Width + 4)
                    Else
                        NearMargin(Me.Handle, 20)
                    End If
                ElseIf Not (CustomPainter Is Nothing) Then
                    customWidth = CustomPainter.GetMarginWidth()
                    NearMargin(Me.Handle, customWidth)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Moves the contained m_control to the appropriate
        ''' position
        ''' </summary>
        Private Sub moveControl()
            If Not (m_control Is Nothing) Then
                Dim currentLocation As Point = m_control.Location
                Dim rcWindow As RECT = New RECT()
                GetWindowRect(Me.Handle, rcWindow)
                Dim moveTo As Point = New Point(rcWindow.left + 2, _
                 rcWindow.top + ((rcWindow.bottom - rcWindow.top) - m_control.Height) / 2)
                If (IsRightToLeft(Me.Handle)) Then
                    moveTo.X = rcWindow.right - m_control.Width - 2
                End If
                moveTo = m_control.Parent.PointToClient(moveTo)
                If Not (currentLocation.Equals(moveTo)) Then
                    m_control.Location = moveTo
                    m_control.BringToFront()
                End If
            End If
        End Sub

        ''' <summary>
        ''' Calls the base WndProc and performs WM_PAINT
        ''' processing to draw the m_icon if necessary.
        ''' </summary>
        ''' <param name="m">Windows Message</param>
        Protected Overrides Sub WndProc(ByRef m As Message)

            MyBase.WndProc(m)
            If (m_control Is Nothing) Then
                Select Case m.Msg
                    Case WM_SETFONT
                        setMargin()
                    Case WM_PAINT
                        RePaint()
                    Case WM_SETFOCUS, WM_KILLFOCUS
                        RePaint()
                    Case WM_LBUTTONDOWN, WM_RBUTTONDOWN, WM_MBUTTONDOWN
                        RePaint()
                    Case WM_LBUTTONUP, WM_RBUTTONUP, WM_MBUTTONUP
                        RePaint()
                    Case WM_LBUTTONDBLCLK, WM_RBUTTONDBLCLK, WM_MBUTTONDBLCLK
                        RePaint()
                    Case WM_KEYDOWN, WM_KEYUP, WM_CHAR
                        RePaint()
                    Case WM_MOUSEMOVE
                        If Not (m.WParam.Equals(IntPtr.Zero)) Then
                            RePaint()
                        End If
                End Select
            Else
                Select Case m.Msg
                    Case WM_PAINT
                        moveControl()
                End Select
            End If
        End Sub

        ''' <summary>
        ''' Paints the control if necessary:
        ''' </summary>
        Private Sub RePaint()

            If (((m_icon >= 0) And Not (m_imageList Is Nothing)) Or _
              Not (m_customPainter Is Nothing)) Then

                Dim rcClient As RECT = New RECT()
                GetClientRect(Me.Handle, rcClient)
                Dim rightToLeft As Boolean = IsRightToLeft(Me.Handle)

                Dim theHandle As IntPtr = Me.Handle
                Dim hdc As IntPtr = GetDC(theHandle)
                Dim gfx As Graphics = Graphics.FromHdc(hdc)

                If (m_customPainter Is Nothing) Then
                    Dim itemSize As Integer = m_imageList.ImageSize.Height
                    Dim pt As Point = New Point(0, rcClient.top + (rcClient.bottom - rcClient.top - itemSize) / 2)
                    If (rightToLeft) Then
                        pt.X = rcClient.right - itemSize - 2
                    Else
                        pt.X = 2
                    End If
					
					Try
                    m_imageList.Draw(gfx, pt.X, pt.Y, m_icon)
					Catch E As Exception
					End Try	
                Else
                    Dim rcDraw As Rectangle = New Rectangle( _
                     New Point(0, 0), New Size(customWidth, rcClient.bottom - rcClient.top))
                    m_customPainter.Draw(gfx, rcDraw, rightToLeft)
                End If

                gfx.Dispose()
                ReleaseDC(theHandle, hdc)
            End If

        End Sub

        ''' <summary>
        ''' Constructs a new instance of this class
        ''' </summary>
        Public Sub New()
            ' intentionally blank
        End Sub

    End Class

    ''' <summary>
    ''' An interface which users of the <see cref="TextBoxMarginCustomise"/>
    ''' class can use to provide a customised painting routine for the
    ''' margin area.  Create an instance of this class and install it
    ''' using the <see cref="TextBoxMarginCustomise.CustomPainter"/>
    ''' accessor.
    ''' </summary>
    Public Interface ITextBoxMarginCustomisePainter

        ''' <summary>
        ''' Called to obtain the width of the margin.
        ''' </summary>
        ''' <returns>Width of the margin</returns>
        Function GetMarginWidth() As Integer

        ''' <summary>
        ''' Called whenever the margin area needs to
        ''' be repainted.
        ''' </summary>
        ''' <param name="gfx">Graphics object to paint on.</param>
        ''' <param name="rcDraw">Boundary of margin area.</param>
        ''' <param name="rightToLeft">Whether the m_control is right
        ''' to left or not</param>
        Sub Draw(ByVal gfx As Graphics, ByVal rcDraw As Rectangle, ByVal rightToLeft As Boolean)

    End Interface


End Namespace