Imports System
Imports System.Linq
Imports System.Collections.Generic
Namespace Resources
    Module ResFile
        Function ParseRESFile(Stream As IO.Stream) As Dictionary(Of Tuple(Of String, String), Byte())
            Dim Resources As New Dictionary(Of Tuple(Of String, String), Byte())
            Using Stream
                Using br = New IO.BinaryReader(Stream)
                    While Stream.Position <> Stream.Length
                        Dim rh As ResourceHeader
                        rh.DataSize = br.ReadUInt32
                        rh.HeaderSize = br.ReadUInt32
                        If rh.HeaderSize > Short.MaxValue Then Throw New AppException($"RES file is invalid. Invalid resource header at offset {Stream.Position - 8}; size={rh.HeaderSize}")
                        If br.ReadUInt16 = &HFFFF Then
                            rh.Type = "#" & br.ReadUInt16
                        Else
                            Stream.Position -= 2
                            rh.Type = br.ReadUTF16
                        End If
                        If br.ReadUInt16 = &HFFFF Then
                            rh.Name = "#" & br.ReadUInt16
                        Else
                            Stream.Position -= 2
                            rh.Name = br.ReadUTF16
                        End If
                        rh.DataVersion = br.ReadUInt32
                        rh.MemoryFlags = DirectCast(br.ReadUInt16, MemoryFlags)
                        rh.LanguageId = br.ReadUInt16
                        rh.Version = br.ReadUInt32
                        rh.Characteristics = br.ReadUInt32
                        Stream.DWORDAlign()
                        Dim Data = br.ReadBytes(CInt(rh.DataSize))
                        Dim Key = New Tuple(Of String, String)(rh.Type, rh.Name)
                        If Data.Length > 0 Then
                            If Resources.ContainsKey(Key) Then
                                Key = New Tuple(Of String, String)(rh.Type, rh.Name & "-" & rh.LanguageId)
                            End If
                            If Resources.ContainsKey(Key) Then
                                Key = New Tuple(Of String, String)(rh.Type, rh.Name & "-" & rh.LanguageId & "-" & rh.Version & "-" & rh.DataVersion)
                            End If
                            Try
                                Resources.Add(Key, Data)
                            Catch e As ArgumentException
                                PopupError("Unable to parse the RES file. Please email the file you're trying to extract to hi@kishan.info so that I can look into this and fix it.")
                            End Try
                        End If
                        Stream.DWORDAlign()
                    End While
                End Using
            End Using
            Return Resources
        End Function
        Sub ExtractRESFile(Stream As IO.Stream, FolderPath$)
            Dim Resources = ParseRESFile(Stream)
            For Each r In Resources
                Dim Data As Byte(),
                    Extension = "",
                    Type = r.Key.Item1
                Select Case Type
                    Case RT_BITMAP
                        Data = PrependBitmapFileHeader(r.Value)
                        Extension = ".bmp"
                    Case RT_ICON
                        Data = CreateIconDirFromIconImage(r.Value)
                        Extension = ".ico"
                    Case RT_GROUP_ICON
                        Dim GrpIconDirEntries = GetIconDirEntries(r.Value, GrpIconDirEntrySize),
                        Icons = From gide In GrpIconDirEntries Select Resources(New Tuple(Of String, String)("#3", "#" & BitConverter.ToUInt16(gide, 12))) 'offset of nID = 12
                        Data = CreateIconDirFromIconsData(GrpIconDirEntries, Icons)
                        Extension = ".ico"
                    Case Else
                        Data = r.Value
                End Select
                If Type.StartsWith("#") Then ResTypeFriendlyNames.TryGetValue(Type, Type)
                Dim NewFolderPath = IO.Path.Combine(FolderPath, Type)
                IO.Directory.CreateDirectory(NewFolderPath)
                IO.File.WriteAllBytes(IO.Path.Combine(NewFolderPath, r.Key.Item2 & Extension), Data)
            Next
        End Sub
    End Module
End Namespace