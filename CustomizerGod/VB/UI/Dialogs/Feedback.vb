Imports System
Imports System.Net
Imports System.Collections.Specialized
Imports System.Runtime.InteropServices

Class Feedback
    Sub New()
        InitializeComponent()
        llEmail.Text = DeveloperEmail.Replace("mailto:", "")
        AddHandler llEmail.LinkClicked, Sub() OpenURL(DeveloperEmail)
        AddHandler lld2w.LinkClicked, Sub() OpenURL(d2w)
        Dim EmailString = Marshal.StringToHGlobalAuto("Email (if you want to be contacted)")
        SendMessage(tbEmail.Handle, &H1501, New IntPtr(1), EmailString)
        Marshal.FreeHGlobal(EmailString)
    End Sub
    Private Sub bSend_Click() Handles bSend.Click
        If String.IsNullOrWhiteSpace(tbFeedback.Text) Then PopupMessage(PopupStr.Enter_Feedback_Message) : Exit Sub
        AsyncPost(bSend, "Sending...", FeedbackURL, New NameValueCollection From {{"app", AppName}, {"email", tbEmail.Text}, {"feedback",
$"{tbFeedback.Text}

--
{AppName} {AppVersion}
{Environment.OSVersion.VersionString} ({OSVersion.Build.ToString})
"}},
                  Sub(e)
                      If e.Error Is Nothing Then
                          If e.Result.Length >= 3 AndAlso e.Result(0) = 50 AndAlso e.Result(1) = 48 AndAlso e.Result(2) = 48 Then '[50,48,48] == "200"
                              PopupMessage(PopupStr.Feedback_Sent)
                              Me.Close()
                          Else
                              PopupError(PopupStr.Feedback_Send_Net_Error)
                          End If
                      ElseIf TypeOf e.Error Is WebException Then
                          PopupError(PopupStr.Feedback_Send_Net_Error)
                      Else
                          OnException(e.Error)
                      End If
                  End Sub)
    End Sub
End Class