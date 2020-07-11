Imports System
Imports System.Windows.Media
Namespace UI
    Class Titleless
        Const NcaCorners = 7
        Shared ReadOnly DwmInfiniteMargins As New Margins With {.cxLeftWidth = -1, .cxRightWidth = -1, .cyBottomHeight = -1, .cyTopHeight = -1}
        ReadOnly Window As Windows.Window, WindowHandle As IntPtr, NcaTop%
        Friend Shared IsDwmEnabled As Boolean = False
        Private Shared Function IsDwmCompositionEnabled() As Boolean
            Dim Enabled = False
            If DwmIsCompositionEnabled(Enabled) <> 0 Then Throw New NativeException("DwmIsCompositionEnabled")
            Return Enabled
        End Function
        Shared Sub SetTransparentBackground(Window As Windows.Window, WindowHandle As IntPtr)
            Window.Background = If(DwmExtendFrameIntoClientArea(WindowHandle, DwmInfiniteMargins) = 0, Brushes.Transparent, Brushes.White)
        End Sub
        Shared Function BrushFromArgb(c As UInteger) As Brush
            Return New SolidColorBrush(Color.FromArgb(CByte((c >> 24) And &HFF), CByte((c >> 16) And &HFF),
                                                      CByte((c >> 08) And &HFF), CByte((c >> 00) And &HFF)))
        End Function
        Shared Function GetDwmColorizationColor() As Brush
            Dim Color As UInteger
            Dim OpaqueBlend As Boolean
            DwmGetColorizationColor(Color, OpaqueBlend)
            Return BrushFromArgb(Color)
        End Function
        Sub New(Window As Windows.Window, WindowHandle As IntPtr, NcaTop%, Optional ShowBorder As Boolean = True)
            Me.Window = Window
            Me.WindowHandle = WindowHandle
            Me.NcaTop = NcaTop
            Dim hWnd = Windows.Interop.HwndSource.FromHwnd(WindowHandle)
            hWnd.CompositionTarget.BackgroundColor = Colors.Transparent
            hWnd.AddHook(AddressOf WndProc)
            If WindowsV >= 100 Then
                ChangeWindowCompositionAttribute(WindowHandle)
                'Window.Background = GetDwmColorizationColor
                If ShowBorder Then
                    Window.BorderBrush = New SolidColorBrush(Colors.Black) With {.Opacity = 0.25}
                    Window.BorderThickness = New Windows.Thickness(1)
                End If
            End If
        End Sub
        Private Function WndProc(hWnd As IntPtr, Msg%, wParam As IntPtr, lParam As IntPtr, ByRef Handled As Boolean) As IntPtr
            If (Msg = WM_ACTIVATE AndAlso Not FirstActivate) OrElse Msg = WM_DWMCOMPOSITIONCHANGED Then IsDwmEnabled = IsDwmCompositionEnabled()
            'If Msg = WM_DWMCOLORIZATIONCOLORCHANGED Then Window.Background = BrushFromArgb(CUInt(wParam.ToInt64))
            If IsDwmEnabled Then
                Return NcaProc(hWnd, Msg, wParam, lParam, Handled)
            Else
                Window.Background = Brushes.White
            End If
            Return IntPtr.Zero
        End Function
        Dim FirstActivate As Boolean = False
        Private Function NcaProc(hWnd As IntPtr, Msg%, wParam As IntPtr, lParam As IntPtr, ByRef Handled As Boolean) As IntPtr
            Dim Result As IntPtr
            If Msg = WM_ACTIVATE AndAlso Not FirstActivate Then
                SetTransparentBackground(Window, WindowHandle)
                FirstActivate = True
            ElseIf Msg = WM_DWMCOMPOSITIONCHANGED Then
                SetTransparentBackground(Window, WindowHandle)
            ElseIf Msg = WM_NCCALCSIZE AndAlso wParam = New IntPtr(1) Then
                Handled = True
            ElseIf Msg = WM_NCHITTEST Then
                Result = HitTestNCA(hWnd)
                Handled = True
            End If
            DwmDefWindowProc(hWnd, CUInt(Msg), wParam, lParam, Result)
            Return Result
        End Function
        Shared ReadOnly HitTests As NonClient()() = {New NonClient() {NonClient.HTTOPLEFT, NonClient.HTTOP, NonClient.HTTOPRIGHT},
                                                     New NonClient() {NonClient.HTLEFT, NonClient.HTCLIENT, NonClient.HTRIGHT},
                                                     New NonClient() {NonClient.HTBOTTOMLEFT, NonClient.HTBOTTOM, NonClient.HTBOTTOMRIGHT}}
        Private Function HitTestNCA(hWnd As IntPtr) As IntPtr
            Dim Cursor = Windows.Forms.Cursor.Position
            Dim WindowRect As Rect
            GetWindowRect(hWnd, WindowRect)
            Dim Row = 1, Col = 1
            If Cursor.Y >= WindowRect.Top AndAlso Cursor.Y < WindowRect.Top + NcaCorners Then
                Row = 0
            ElseIf Cursor.Y < WindowRect.Bottom AndAlso Cursor.Y >= WindowRect.Bottom - NcaCorners Then
                Row = 2
            End If
            If Cursor.X >= WindowRect.Left AndAlso Cursor.X < WindowRect.Left + NcaCorners Then
                Col = 0
            ElseIf Cursor.X < WindowRect.Right AndAlso Cursor.X >= WindowRect.Right - NcaCorners Then
                Col = 2
            End If
            Dim HitTest = HitTests(Row)(Col)
            If Row = 1 AndAlso Col = 1 AndAlso Cursor.Y >= WindowRect.Top + NcaCorners AndAlso Cursor.Y < WindowRect.Top + NcaTop Then HitTest = NonClient.HTCAPTION
            Return New IntPtr(HitTest)
        End Function
    End Class
End Namespace