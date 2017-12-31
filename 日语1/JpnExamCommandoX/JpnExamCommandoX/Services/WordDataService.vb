Imports JpnExamCommandoX.Models
Imports Windows.Storage

Namespace Services
    Public Module WordDataService

        Public Async Function GetModelDataAsync() As Task(Of IEnumerable(Of WordDescription))
            Dim file = Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///假名汉字转换.md"))
            Dim content = Await FileIO.ReadLinesAsync(file)
            Return (Iterator Function()
                        For i = 2 To content.Count - 1
                            Dim line = content(i).Split("|"c, StringSplitOptions.RemoveEmptyEntries)
                            Yield New WordDescription(line(0), line(1), line(2))
                        Next
                    End Function)()
        End Function
    End Module
End Namespace
