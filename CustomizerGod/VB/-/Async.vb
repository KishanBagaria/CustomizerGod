Imports System
Imports System.Windows
Imports System.Net
Imports System.Collections.Specialized
Imports System.Threading.Tasks

Module Async
    Sub AsyncDo(Action As Action, ActionFinish As Action)
        Task.Run(Action).ContinueWith(
            Sub(t As Task)
                Try
                    ActionFinish()
                    If t.Exception IsNot Nothing Then
                        For Each e In t.Exception.InnerExceptions
                            OnException(e)
                        Next
                    End If
                Catch e As Exception
                    OnException(e)
                End Try
            End Sub, TaskScheduler.FromCurrentSynchronizationContext)
    End Sub
    Sub AsyncDo(b As Controls.Button, NewText$, Action As Action, Optional ActionFinish As Action = Nothing)
        Dim OriginalText = b.Content
        b.IsEnabled = False
        b.Content = NewText
        AsyncDo(Action, Sub()
                            b.Content = OriginalText
                            b.IsEnabled = True
                            If ActionFinish IsNot Nothing Then ActionFinish()
                        End Sub)
    End Sub
    Sub AsyncDo(b As Controls.Button, el As FrameworkElement, NewText$, Action As Action)
        el.IsEnabled = False
        Dim OriginalText = b.Content
        b.Content = NewText
        AsyncDo(Action, Sub()
                            b.Content = OriginalText
                            el.IsEnabled = True
                        End Sub)
    End Sub
    Sub AsyncPost(b As Forms.Button, NewText$, URL$, Params As NameValueCollection, Action As Action(Of UploadValuesCompletedEventArgs))
        Dim OriginalText = b.Text
        b.Enabled = False
        b.Text = NewText
        Using wc = New WebClient
            AddHandler wc.UploadValuesCompleted, Sub(s, e)
                                                     b.Text = OriginalText
                                                     b.Enabled = True
                                                     Action(e)
                                                 End Sub
            wc.UploadValuesAsync(New Uri(URL), Params)
        End Using
    End Sub
    'Sub AsyncDo(b As Forms.Button, bNewText$, Action As Action)
    '    Dim bOldText = b.Text
    '    b.Enabled = False
    '    b.Text = bNewText
    '    AsyncDo(Action, Sub()
    '                        b.Text = bOldText
    '                        b.Enabled = True
    '                    End Sub)
    'End Sub
    'Sub AsyncDo(b As Forms.MenuItem, bNewText$, Action As Action)
    '    Dim bOldText = b.Text
    '    b.Enabled = False
    '    b.Text = bNewText
    '    AsyncDo(Action, Sub()
    '                        b.Text = bOldText
    '                        b.Enabled = True
    '                    End Sub)
    'End Sub
End Module