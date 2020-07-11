#If Screenshot Then
Imports System
Imports System.Drawing
Imports System.Windows.Threading
Imports System.Runtime.CompilerServices
Imports System.Windows.Input
Imports System.Windows.Controls

Module Screenshot
    <Extension> Sub SaveScreenshot(Window As Windows.Window, Path$)
        Dim w = CInt(Window.Dispatcher.Invoke(Function() Window.ActualWidth)),
            h = CInt(Window.Dispatcher.Invoke(Function() Window.ActualHeight)),
            l = CInt(Window.Dispatcher.Invoke(Function() Window.Left)),
            t = CInt(Window.Dispatcher.Invoke(Function() Window.Top))
        If WindowsV = 61 Then
            Dim c = 15
            l -= c
            t -= c
            h += c * 2
            w += c * 2
        End If
        Dim s = New Size(w, h),
            o = New Point(l, t)
        Using b = New Bitmap(w, h)
            Using g = Graphics.FromImage(b)
                g.CopyFromScreen(o, Point.Empty, s)
            End Using
            b.Save(Path & ".png", Imaging.ImageFormat.Png)
        End Using
    End Sub
End Module
Partial Class MainWindow
    Sub SleepUntilLoaded()
        While IsBusy
            Threading.Thread.Sleep(100)
        End While
        Threading.Thread.Sleep(imgData.Count)
    End Sub
    Private Sub Window_Screenshot_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        If e.Key = Key.F15 Then
            SaveScreenshot(Date.UtcNow.Ticks.ToString)
        ElseIf e.Key = Key.F16 Then
            Threading.Tasks.Task.Run(Sub() SaveScreenshots())
        End If
    End Sub
    Sub SaveScreenshots()
        IO.Directory.CreateDirectory(CStr(WindowsV))
        For i = 1 To lbSidebar.Items.Count
            Dim j = i - 1
            Dispatcher.Invoke(Sub() DirectCast(lbSidebar.Items(j), ListViewItem).IsSelected = True)
            SleepUntilLoaded()
            SaveScreenshot(WindowsV & "\" & i)
        Next
    End Sub
End Class
Partial Class ImagePreview
    Private Sub Window_Screenshot_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        If e.Key = Key.F15 Then
            SaveScreenshot("ResourcePreview")
        End If
    End Sub
End Class
#End If