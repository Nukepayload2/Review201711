Imports System.Threading
Imports JpnExamCommandoX.Models
Imports Newtonsoft.Json
Imports Windows.Storage

Namespace Services
    Public Module WordDataService

        Public Async Function GetModelDataAsync() As Task(Of IEnumerable(Of WordDescription))
            Dim file = Await ApplicationData.Current.LocalFolder.CreateFileAsync("traininglog.json", CreationCollisionOption.OpenIfExists)
            Dim json = Await FileIO.ReadTextAsync(file)
            Dim items As IEnumerable(Of WordDescription)
            If json.Length < 2 Then
                items = Await ReloadModelDataAsync()
                Await SaveModelDataAsync(items, file)
            Else
                items = JsonConvert.DeserializeObject(Of WordDescription())(json)
            End If

            Return items
        End Function

        Dim saving As New AsyncLocal(Of Boolean)
        Public Async Function SaveModelDataAsync(items As IEnumerable(Of WordDescription), Optional file As StorageFile = Nothing) As Task
            Do While saving.Value
                Await Task.Delay(10)
            Loop
            saving.Value = True
            If file Is Nothing Then
                file = Await ApplicationData.Current.LocalFolder.CreateFileAsync("traininglog.json", CreationCollisionOption.OpenIfExists)
            End If
            Dim content = JsonConvert.SerializeObject(items)
            Await FileIO.WriteTextAsync(file, content)
            saving.Value = False
        End Function

        Public Async Function ReloadModelDataAsync() As Task(Of IEnumerable(Of WordDescription))
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
