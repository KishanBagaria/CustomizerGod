Imports System
Imports System.Net
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Threading
Imports System.Windows.Documents

Module Web
    Const GitHubGistLink = "https://gist.githubusercontent.com/KishanBagaria/",
          LatestVerLink = GitHubGistLink & "4e4ed061c87e954525ce/raw",
          ChangelogLink = GitHubGistLink & "88fb10532d5a57b9bf6d/raw",
          CalloutsLink = GitHubGistLink & "80c84e92a7597151ea50/raw"

    Friend WithEvents dtWebChecker As New DispatcherTimer With {.Interval = TimeSpan.FromSeconds(1)}
    ReadOnly DP As Dispatcher = Application.Current.Dispatcher
    Sub dtWebChecker_Tick() Handles dtWebChecker.Tick
        Task.Run(AddressOf CheckUpdates)
        dtWebChecker.Stop()
    End Sub
    Function IsNewerVersion(NewVersion$) As Boolean
        Return New Version(NewVersion).CompareTo(New Version(AppVersion)) > 0
    End Function
    Sub NotifyForUpdate(LatestVersionText$, RawChangelog$)
        Dim ChangelogSB = New Text.StringBuilder
        Dim TotalLines = 0
        Dim IndexOfVersion = RawChangelog.IndexOf(AppVersion & ":")
        If IndexOfVersion > 0 Then
            For Each Line In RawChangelog.Remove(IndexOfVersion).Split(LF)
                If Line.StartsWith("-") Then
                    ChangelogSB.AppendLine(Line)
                    If TotalLines > 16 Then Exit For
                    TotalLines += 1
                End If
            Next
        End If
        Dim Changelog = If(ChangelogSB.Length = 0, "", StrFormat(PopupStr.Update_Changelog, ChangelogSB.ToString.Trim))
        Dim Text = StrFormat(PopupStr.Update_Available, LatestVersionText, AppVersion, Changelog)
        If Question(Text, PopupStr.Update_Available_Heading) Then
            OpenURL(AppLink)
        Else
            ShowUpdateCallout(LatestVersionText)
        End If
    End Sub
    Sub ShowWebCallout(Text$)
        If Not String.IsNullOrWhiteSpace(Text) Then
            DP.Invoke(Sub() mw.ShowCallout(GetInlines(Text.Trim)))
        End If
    End Sub
    Sub ShowUpdateCallout(LatestVersion$)
        DP.Invoke(Sub() mw.ShowCallout({
                                      MakeHyperLink($"{AppName} {LatestVersion}", AppLink),
                                      New Run(StrFormat(PopupStr.Update_Callout, AppVersion))
                                      }))
    End Sub
    Sub CheckUpdates()
        If IO.File.Exists(OfflineCalloutPath) Then
            If (Date.UtcNow - IO.File.GetLastWriteTimeUtc(OfflineCalloutPath)).TotalHours <= 12 Then
                ShowWebCallout(IO.File.ReadAllText(OfflineCalloutPath, Text.Encoding.UTF8))
                Exit Sub
            End If
        End If
        Try
            Using wc = New WebClient
                Dim LatestVersion = wc.DownloadString(LatestVerLink)
                If IsNewerVersion(LatestVersion) Then
                    NotifyForUpdate(LatestVersion, wc.DownloadString(ChangelogLink))
                Else
                    Dim CalloutText = wc.DownloadString(CalloutsLink)
#If DEBUG Then
                    CalloutText = "Like CustomizerGod? Share it with your friends on [Facebook]<https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2Fwww.door2windows.com%2Fcustomizergod%2F&t=CustomizerGod> and [Twitter]<https://twitter.com/intent/tweet?url=http%3A%2F%2Fwww.door2windows.com%2Fcustomizergod%2F&text=CustomizerGod&via=door2windows>"
#End If
                    IO.File.WriteAllText(OfflineCalloutPath, CalloutText, Text.Encoding.UTF8)
                    ShowWebCallout(CalloutText)
                End If
            End Using
        Catch
        End Try
    End Sub
End Module