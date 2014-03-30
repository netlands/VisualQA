Namespace vbAccelerator.Components.Win32


    ''' <summary>
    ''' Enumerated flag values for the mouse gestures supported by 
    ''' the MouseGesture class.
    ''' </summary>
    <FlagsAttribute()> _
    Public Enum MouseGestureTypes As Integer
        ''' <summary>
        ''' No mouse gesture.
        ''' </summary>
        NoGesture = &H0
        ''' <summary>
        ''' Mouse Gesture move north
        ''' </summary>
        NorthGesture = &H1
        ''' <summary>
        ''' Mouse Gesture move south
        ''' </summary>
        SouthGesture = &H2
        ''' <summary>
        ''' Mouse Gesture move east
        ''' </summary>
        EastGesture = &H4
        ''' <summary>
        ''' Mouse Gesture move west
        ''' </summary>
        WestGesture = &H8
        ''' <summary>
        ''' Mouse Gesture move north-east
        ''' </summary>
        NorthThenEastGesture = &H10
        ''' <summary>
        ''' Mouse Gesture move south-east
        ''' </summary>
        SouthThenEastGesture = &H20
        ''' <summary>
        ''' Mouse Gesture move south-west
        ''' </summary>
        SouthThenWestGesture = &H40
        ''' <summary>
        ''' Mouse Gesture move north-west
        ''' </summary>		
        NorthThenWestGesture = &H80
        ''' <summary>
        ''' Mouse Gesture move north-east
        ''' </summary>
        EastThenNorthGesture = &H100
        ''' <summary>
        ''' Mouse Gesture move south-east
        ''' </summary>
        EastThenSouthGesture = &H200
        ''' <summary>
        ''' Mouse Gesture move south-west
        ''' </summary>
        WestThenSouthGesture = &H400
        ''' <summary>
        ''' Mouse Gesture move north-west
        ''' </summary>		
        WestThenNorthGesture = &H800
        ''' <summary>
        ''' All mouse gestures
        ''' </summary>
        AllGestureTypes = &HFFF
    End Enum

    ''' <summary>
    ''' Holds the arguments for a gesture event.  The <c>acceptGesture</c>
    ''' property is used to tell the class which raises the message whether
    ''' the consuming application acknowledged the gesture and therefore to 
    ''' cancel the right mouse up event.
    ''' </summary>
    Public Class MouseGestureEventArgs
        Inherits EventArgs

        Private m_gestureType As MouseGestureTypes
        Private m_gestureStartPosition As Point
        Private m_gestureEndPosition As Point
        Private m_acceptGesture As Boolean

        ''' <summary>
        ''' Gets the gesture type.
        ''' </summary>
        Public ReadOnly Property GestureType() As MouseGestureTypes
            Get
                Return Me.m_gestureType
            End Get
        End Property

        ''' <summary>
        ''' Gets the mouse location for the point at which the gesture
        ''' was started, relative to the screen.
        ''' </summary>
        Public ReadOnly Property GestureStartPosition() As Point
            Get
                Return Me.m_gestureStartPosition
            End Get
        End Property

        ''' <summary>
        ''' Gets the mouse location for the point at which the gesture
        ''' was ended, relative to the screen.
        ''' </summary>
        Public ReadOnly Property GestureEndPosition() As Point
            Get
                Return Me.m_gestureEndPosition
            End Get
        End Property

        ''' <summary>
        ''' Gets/sets whether the gesture has been processed by the 
        ''' application.  By default, gestures are presumed to be unaccepted,
        ''' in which case the standard right mouse up behaviour will be 
        ''' activated.  By setting Me property to <c>true</c> the right
        ''' mouse up is filtered and the application can process the gesture.
        ''' </summary>
        Public Property AcceptGesture() As Boolean
            Get
                Return Me.m_acceptGesture
            End Get
            Set(ByVal Value As Boolean)
                Me.m_acceptGesture = Value
            End Set
        End Property

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="gestureType">Type of gesture which was detected</param>
        ''' <param name="gestureStartPosition">Position of mouse relative to screen when gesture
        ''' was started</param>
        ''' <param name="gestureEndPosition">Position of mouse relative to screen when gesture
        ''' was completed</param>
        Public Sub New( _
                ByVal gestureType As MouseGestureTypes, _
                ByVal gestureStartPosition As Point, _
                ByVal gestureEndPosition As Point _
            )
            Me.m_gestureType = gestureType
            Me.m_gestureStartPosition = gestureStartPosition
            Me.m_gestureEndPosition = gestureEndPosition
            Me.m_acceptGesture = False
        End Sub

    End Class

    ''' <summary>
    ''' Represents the method which handles the <c>MouseGesture</c> event
    ''' raised by the <c>MouseGestureFilter</c> class.
    ''' </summary>
    Public Delegate Sub MouseGestureEventHandler(ByVal sender As Object, ByVal args As MouseGestureEventArgs)

    ''' <summary>
    ''' A Windows Message Loop filter which enables mouse gestures to 
    ''' be detected over any control or window.
    ''' </summary>
    ''' <remarks>Controls which perform processing on Right Mouse
    ''' Down (rather than the standard Right Mouse Up) will still
    ''' perform the right mouse action regardless of whether a gesture
    ''' is made.</remarks>
    Public Class MouseGestureFilter
        Implements IMessageFilter

        ''' <summary>
        ''' 
        ''' </summary>
        Public Event MouseGesture As MouseGestureEventHandler

        Private Declare Auto Function PostMessage Lib "user32" ( _
            ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

        Private Const WM_ACTIVATE As Integer = &H6
        Private Const WM_RBUTTONDOWN As Integer = &H204
        Private Const WM_MOUSEMOVE As Integer = &H200
        Private Const WM_RBUTTONUP As Integer = &H205

        ''' <summary>
        ''' The default absolute number of pixels the mouse must travel
        ''' in any direction for the gesture to be acknowledged.
        ''' </summary>
        Private Const DEFAULT_HYSTERESIS_PIXELS As Integer = 8

        ''' <summary>
        ''' How far does the mouse have to move before it is 
        ''' interpreted as a gesture?
        ''' </summary>
        Protected hysteresis As Integer = DEFAULT_HYSTERESIS_PIXELS
        ''' <summary>
        ''' The configured mouse gesture types
        ''' </summary>
        Private m_gestureTypes As MouseGestureTypes = MouseGestureTypes.NoGesture
        ''' <summary>
        ''' Whether we are checking for a gesture or not.
        ''' </summary>
        Private checkingGesture As Boolean = False
        ''' <summary>
        ''' The recorded mouse gesture during gesture checking
        ''' </summary>
        Private recordedGesture As MouseGestureTypes = MouseGestureTypes.NoGesture
        ''' <summary>
        ''' <c>ArrayList</c> of mouse points recorded during gesture.
        ''' </summary>
        Private gesture As ArrayList = Nothing


        ''' <summary>
        ''' Gets/sets the mouse gesture types to look for.
        ''' </summary>
        Public Property GestureTypes() As MouseGestureTypes
            Get
                Return Me.m_gestureTypes
            End Get
            Set(ByVal Value As MouseGestureTypes)
                Me.m_gestureTypes = Value
            End Set
        End Property


        ''' <summary>
        ''' Prefilters all application messages to check whether
        ''' the message is a gesture or not.
        ''' </summary>
        ''' <param name="m">The Windows message to prefilter</param>
        ''' <returns><c>true</c> if the message should be filtered (was a 
        ''' processed gesture), <c>false</c> otherwise.</returns>
        Public Function PreFilterMessage( _
                ByRef m As Message _
            ) As Boolean Implements System.Windows.Forms.IMessageFilter.PreFilterMessage

            Dim retValue As Boolean = False

            If (Me.m_gestureTypes > 0) Then
                If (Me.checkingGesture) Then
                    If (m.Msg = WM_MOUSEMOVE) Then
                        AddToMouseGesture()
                    ElseIf (m.Msg = WM_RBUTTONUP) Then
                        retValue = EndMouseGesture()
                        If (retValue) Then
                            ' Windows will skip the next mouse down if we consume
                            ' a mouse up.  m cannot be modified, despite being byref,
                            ' so post a new one to a location which is offscreen:
                            Dim offScreen As Integer = &H7FFF7FFF
                            PostMessage(m.HWnd, WM_RBUTTONUP, m.WParam.ToInt32(), offScreen)
                        End If
                    ElseIf (m.Msg = WM_ACTIVATE) Then
                        Me.checkingGesture = False
                    End If

                ElseIf (m.Msg = WM_RBUTTONDOWN) Then
                    BeginMouseGesture()
                End If
            End If

            Return retValue

        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        Private Sub BeginMouseGesture()
            gesture = New ArrayList()
            gesture.Add(Cursor.Position)
            Me.checkingGesture = True
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        Private Sub AddToMouseGesture()
            gesture.Add(Cursor.Position)
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Private Function EndMouseGesture() As Boolean

            Me.checkingGesture = False

            Dim retValue As Boolean = False

            '' add the end point:
            gesture.Add(Cursor.Position)

            '' get start and end:
            Dim first As Point = gesture(0)
            Dim last As Point = gesture(gesture.Count - 1)

            '' check which directions we register a change in:
            Dim xDiff As Integer = first.X - last.X
            Dim yDiff As Integer = first.Y - last.Y

            Dim north, south, east, west As Boolean

            If (Math.Abs(yDiff) > DEFAULT_HYSTERESIS_PIXELS) Then
                north = (yDiff > 0)
                south = Not (north)
            End If
            If (Math.Abs(xDiff) > DEFAULT_HYSTERESIS_PIXELS) Then
                west = (xDiff > 0)
                east = Not (west)
            End If

            '' check for very narrow angles as these are probably not compound gestures
            If ((north Or south) And (east Or west)) Then
                If (Math.Abs(xDiff) > Math.Abs(yDiff)) Then
                    If ((Math.Abs(xDiff) / (Math.Abs(yDiff) * 1.0)) > 7.0) Then
                        north = False
                        south = False
                    End If
                Else
                    If ((Math.Abs(yDiff) / (Math.Abs(xDiff) * 1.0)) > 7.0) Then
                        east = False
                        west = False
                    End If
                End If
            End If

            recordedGesture = MouseGestureTypes.NoGesture

            If (north Or south) Then
                If (east Or west) Then
                    ' compound gesture
                    recordedGesture = interpretCompoundGesture(first, last, north, south, east, west)
                Else
                    ' vertical gesture:
                    If (north) Then
                        recordedGesture = MouseGestureTypes.NorthGesture
                    Else
                        recordedGesture = MouseGestureTypes.SouthGesture
                    End If
                End If
            ElseIf (east Or west) Then
                ' horizontal gesture
                If (east) Then
                    recordedGesture = MouseGestureTypes.EastGesture
                Else
                    recordedGesture = MouseGestureTypes.WestGesture
                End If
            End If

            If Not (recordedGesture = MouseGestureTypes.NoGesture) Then
                If Not ((GestureTypes And recordedGesture) = 0) Then
                    Dim args As MouseGestureEventArgs = New MouseGestureEventArgs( _
                     recordedGesture, first, last)
                    RaiseEvent MouseGesture(Me, args)
                    retValue = args.AcceptGesture
                End If
            End If

            Return retValue
        End Function

        Private Function interpretCompoundGesture( _
            ByVal first As Point, ByVal last As Point, _
            ByVal north As Boolean, ByVal south As Boolean, ByVal east As Boolean, ByVal west As Boolean _
            ) As MouseGestureTypes

            Dim retValue As MouseGestureTypes = MouseGestureTypes.NoGesture

            ' draw a diagonal line between start & end
            ' and determine if most points are y above 
            ' the line or not:
            Dim pointAbove As Integer = 0
            Dim pointBelow As Integer = 0
            Dim point As Point
            For Each point In gesture
                Dim diagY As Integer = ((point.X - first.X) * (first.Y - last.Y)) / (first.X - last.X) + first.Y
                If (point.Y > diagY) Then
                    pointAbove += 1
                Else
                    pointBelow += 1
                End If
            Next

            If (north) Then
                If (east) Then
                    If (pointAbove > pointBelow) Then
                        retValue = MouseGestureTypes.EastThenNorthGesture
                    Else
                        retValue = MouseGestureTypes.NorthThenEastGesture
                    End If
                Else
                    If (pointAbove > pointBelow) Then
                        retValue = MouseGestureTypes.WestThenNorthGesture
                    Else
                        retValue = MouseGestureTypes.NorthThenWestGesture
                    End If
                End If
            ElseIf (south) Then
                If (east) Then
                    If (pointAbove > pointBelow) Then
                        retValue = MouseGestureTypes.SouthThenEastGesture
                    Else
                        retValue = MouseGestureTypes.EastThenSouthGesture
                    End If
                Else
                    If (pointAbove > pointBelow) Then
                        retValue = MouseGestureTypes.SouthThenWestGesture
                    Else
                        retValue = MouseGestureTypes.WestThenSouthGesture
                    End If
                End If
            End If

            Return retValue

        End Function



        ''' <summary>
        ''' Constructs a default instance of Me class.  The class
        ''' checks for all <c>MouseGestureTypes</c>.
        ''' </summary>		 
        Public Sub New()
            Me.m_gestureTypes = MouseGestureTypes.AllGestureTypes
        End Sub

        ''' <summary>
        ''' Constructs a new instance of Me class and starts checking for
        ''' the specified mouse gestures.
        ''' </summary>
        ''' <param name="gestureTypes"></param>
        Public Sub New(ByVal gestureTypes As MouseGestureTypes)
            Me.m_gestureTypes = gestureTypes
        End Sub

    End Class

End Namespace
