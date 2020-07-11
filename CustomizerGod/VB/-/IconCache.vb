Imports System
Module IconCache
    Sub QuickClearIconCache()
        RunProcessAndGetOutput(IE4UInitPath, "-ClearIconCache", True)
    End Sub
    Sub FullClearIconCache()
        QuickClearIconCache()
        If IsExplorerRunning() Then ExitExplorer()
        NukeFile(IconCachePath)
        RunExplorer()
    End Sub
End Module
