Public Class TextBoxMarginColourPainter
    Implements vbAccelerator.Components.Controls.ITextBoxMarginCustomisePainter

    Private m_color As Color = Color.FromKnownColor(KnownColor.Control)

    ''' <summary>
    ''' Gets/sets the color.
    ''' </summary>
    Public Property Color() As System.Drawing.Color
        Get
            Color = m_color
        End Get
        Set(ByVal Value As System.Drawing.Color)
            m_color = Value
        End Set
    End Property


    ''' <summary>
    ''' Called to obtain the width of the margin.
    ''' </summary>
    ''' <returns>Width of the margin</returns>
    Public Function GetMarginWidth() As Integer _
        Implements vbAccelerator.Components.Controls.ITextBoxMarginCustomisePainter.GetMarginWidth

        GetMarginWidth = 18

    End Function

    ''' <summary>
    ''' Called whenever the margin area needs to
    ''' be repainted.
    ''' </summary>
    ''' <param name="gfx">Graphics object to paint on.</param>
    ''' <param name="rcDraw">Boundary of margin area.</param>
    ''' <param name="rightToLeft">Whether the control is right 
    ''' to left or not</param>
    Public Sub Draw(ByVal gfx As Graphics, ByVal rcDraw As Rectangle, ByVal rightToLeft As Boolean) _
        Implements vbAccelerator.Components.Controls.ITextBoxMarginCustomisePainter.Draw

        Dim rcColor As Rectangle = New Rectangle( _
          rcDraw.Location, rcDraw.Size)
        rcColor.X += 2
        rcColor.Y += (rcColor.Height - 14) / 2
        rcColor.Width = 14
        rcColor.Height = 14

        Dim br As Brush = New SolidBrush(Me.m_color)
        gfx.FillRectangle(br, rcColor)
        br.Dispose()
        gfx.DrawRectangle(SystemPens.Highlight, rcColor)

    End Sub

    Public Sub New(ByVal theColor As Color)
        m_color = theColor
    End Sub


End Class
