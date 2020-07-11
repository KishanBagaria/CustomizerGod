Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms

Class DictionaryViewer
    Sub New()
        InitializeComponent()
        SetWindowTheme(lv.Handle, "explorer", Nothing)
        AddHandler Resize, Sub() ch2.Width = Width - 250
    End Sub
    Overloads Shared Sub Show(Groups As IEnumerable(Of String), Dictionaries As IEnumerable(Of Dictionary(Of String, String)), FilePath$)
        Dim dv = New DictionaryViewer With {.Text = FilePath}
        For Each g In Groups
            dv.lv.Groups.Add(g, g)
        Next
        For i = 0 To Dictionaries.Count - 1
            For Each Item In Dictionaries(i)
                dv.lv.Items.Add(New ListViewItem({Item.Key, Item.Value}, dv.lv.Groups(i)))
            Next
        Next
        dv.ShowDialog()
    End Sub
End Class