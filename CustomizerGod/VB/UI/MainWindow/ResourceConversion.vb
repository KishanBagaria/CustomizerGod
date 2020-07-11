Imports System
Imports System.Windows
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.Text

Partial Class MainWindow
    Shared DLLPrettyNames As New Dictionary(Of String, String) From {
        {"shell32.dll", "Shell Icons"},
        {"imageres.dll", "General Icons"},
        {"imagesp1.dll", "Drive Icons"},
        {"zipfldr.dll", "ZIP Folder Icons"}
    }
    Private Sub bRenameRC_Click() Handles bRenameRC.Click
        Dim OFD = FileDialog(FilterRC)
        If OFD.ShowDialog = Forms.DialogResult.OK Then
            AsyncDo(bRenameRC, "Renaming Resources...",
                    Sub()
                        Dim RCPath = OFD.FileName
                        Dim RCFolder = IO.Path.GetDirectoryName(RCPath)
                        For Each m As Match In Regex.Matches(IO.File.ReadAllText(RCPath), "(.+?) (.+?) ""(.+?)""")
                            Dim ID = m.Groups(1).Value
                            Dim Type = m.Groups(2).Value
                            Dim FileName = m.Groups(3).Value
                            Dim Ext = IO.Path.GetExtension(FileName)
                            Dim ResOldPath = IO.Path.Combine(RCFolder, FileName)
                            Dim NewDirPath = IO.Path.Combine(RCFolder, Type)
                            Dim ResNewPath = IO.Path.Combine(NewDirPath, ID & Ext)
                            IO.Directory.CreateDirectory(NewDirPath)
                            If IO.File.Exists(ResOldPath) AndAlso Not IO.File.Exists(ResNewPath) Then IO.File.Move(ResOldPath, ResNewPath)
                        Next
                        OpenFolder(RCFolder)
                    End Sub)
        End If
    End Sub
    Private Sub bExtractRES_Click() Handles bExtractRES.Click
        Dim OFD = FileDialog(FilterRES)
        If OFD.ShowDialog = Forms.DialogResult.OK Then
            AsyncDo(bExtractRES, "Extracting Resources...",
                    Sub()
                        Dim ResPath = OFD.FileName
                        Dim ResFolder = IO.Path.Combine(IO.Path.GetDirectoryName(ResPath), IO.Path.GetFileNameWithoutExtension(ResPath))
                        CustomizerGod.Resources.ExtractRESFile(New IO.FileStream(ResPath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read), ResFolder)
                        OpenFolder(ResFolder)
                    End Sub)
        End If
    End Sub
    Shared ICT As ICryptoTransform
    Shared Sub InitCSP()
        If ICT Is Nothing Then
            Dim Key = Encoding.ASCII.GetBytes("B3DM60P7")
            ICT = New DESCryptoServiceProvider With {.Key = Key, .IV = Key}.CreateDecryptor()
        End If
    End Sub
    Shared Sub DecryptDES(EncFilePath$, DecFilePath$)
        Using cs = New CryptoStream(New IO.FileStream(EncFilePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read), ICT, CryptoStreamMode.Read)
            Using fs = New IO.FileStream(DecFilePath, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
                cs.CopyTo(fs)
            End Using
        End Using
    End Sub
    Private Sub bExtractiPack_Click() Handles bExtractiPack.Click
        Dim OFD = FileDialog(FilterEXE)
        If OFD.ShowDialog = Forms.DialogResult.OK Then
            AsyncDo(bExtractiPack, "Extracting Resources...",
                   Sub()
                       Dim ExePath = OFD.FileName
                       Dim TmpFolder = IO.Path.Combine(TempDirPath, Guid.NewGuid.ToString)
                       RunProcessAndGetOutput(_7zaPath, $"e -y -ir!*.res -ir!*.ipack -o""{TmpFolder}"" -- ""{ExePath}""")
                       If IO.Directory.Exists(TmpFolder) Then
                           Dim EncryptedArchives = IO.Directory.GetFiles(TmpFolder, "*.ipack")
                           If EncryptedArchives.Length > 0 Then
                               InitCSP()
                               For Each FilePath In EncryptedArchives
                                   Dim DecryptedPath = IO.Path.GetFileNameWithoutExtension(FilePath)
                                   DecryptDES(FilePath, DecryptedPath)
                                   RunProcessAndGetOutput(_7zaPath, $"e -y -ir!*.res -o""{TmpFolder}"" -- ""{DecryptedPath}""")
                               Next
                           End If
                           Dim ResPaths = IO.Directory.GetFiles(TmpFolder, "*.res")
                           Dim ExtractedFolder = IO.Path.Combine(IO.Path.GetDirectoryName(ExePath), IO.Path.GetFileNameWithoutExtension(ExePath))
                           For Each ResPath In ResPaths
                               Dim OldName = IO.Path.GetFileNameWithoutExtension(ResPath)
                               Dim NewName = DLLPrettyNames.GetValueOrDefault(OldName.ToLowerInvariant, OldName)
                               If NewName <> OldName Then NewName &= $" ({OldName})"
                               Dim ResFolder = IO.Path.Combine(ExtractedFolder, NewName)
                               CustomizerGod.Resources.ExtractRESFile(New IO.FileStream(ResPath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read), ResFolder)
                               IO.File.Delete(ResPath)
                           Next
                           OpenFolder(ExtractedFolder)
                       Else
                           PopupError(ExePath & " doesn't contain any .res files!")
                       End If
                   End Sub)
        End If
    End Sub
End Class
