Imports System
Imports System.Runtime.InteropServices

Module PEChecksum
    Sub UpdatePEChecksum(FilePath$)
        Using hFile = CreateFile(FilePath, IO.FileAccess.ReadWrite, IO.FileShare.None, IntPtr.Zero, IO.FileMode.Open, IO.FileAttributes.Normal, IntPtr.Zero)
            If hFile.IsInvalid Then Throw New NativeException("CreateFile " & FilePath)
            Dim hFileMapping, pBaseAddress As IntPtr
            Try
                hFileMapping = CreateFileMapping(hFile, IntPtr.Zero, FileMapProtection.PAGE_READWRITE, 0, 0, Nothing)
                If hFileMapping = IntPtr.Zero Then Throw New NativeException("CreateFileMapping " & FilePath)

                pBaseAddress = MapViewOfFile(hFileMapping, FileMapAccess.FILE_MAP_ALL_ACCESS, 0, 0, UIntPtr.Zero)
                If pBaseAddress = IntPtr.Zero Then Throw New NativeException("MapViewOfFile " & FilePath)

                Dim li As LargeInteger
                GetFileSizeEx(hFile, li)
                Dim FileSize = CUInt(li.QuadPart)

                Dim NewChecksum As UInteger
                Dim pNTHeader = CheckSumMappedFile(pBaseAddress, FileSize, 0, NewChecksum)
                If pNTHeader = IntPtr.Zero Then Throw New NativeException("CheckSumMappedFile " & FilePath)

                'RemoveIntegrityFlag(pNTHeader, 94)
                Marshal.WriteInt32(pNTHeader, 88, CInt(NewChecksum))
            Finally
                UnmapViewOfFile(pBaseAddress)
                CloseHandle(hFileMapping)
            End Try
        End Using
    End Sub
    Sub RemoveIntegrityFlag(Pointer As IntPtr, Offset%)
        Dim DC = BitConverter.ToUInt16({Marshal.ReadByte(Pointer, Offset), Marshal.ReadByte(Pointer, Offset + 1)}, 0)
        If DirectCast(DC, DllCharacteristics).HasFlag(DllCharacteristics.FORCE_INTEGRITY) Then
            Dim NewDC = BitConverter.GetBytes(DC And Not DllCharacteristics.FORCE_INTEGRITY)
            Marshal.WriteByte(Pointer, Offset, NewDC(0))
            Marshal.WriteByte(Pointer, Offset + 1, NewDC(1))
        End If
    End Sub
End Module
