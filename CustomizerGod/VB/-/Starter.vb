Imports System
Imports Microsoft.VisualBasic.ApplicationServices

Module EntryPoint
    <STAThread> Sub Main(Args$())
        Dim sim = New SingleInstanceManager
        sim.Run(Args)
    End Sub
End Module
Class SingleInstanceManager : Inherits WindowsFormsApplicationBase
    Dim App As App
    Sub New()
        IsSingleInstance = True
    End Sub
    Protected Overrides Function OnStartup(e As StartupEventArgs) As Boolean
        App = New App
        App.Run()
        Return False
    End Function
    Protected Overrides Sub OnStartupNextInstance(e As StartupNextInstanceEventArgs)
        If mw.WindowState = Windows.WindowState.Minimized Then mw.WindowState = Windows.WindowState.Normal
        mw.Activate()
    End Sub
End Class