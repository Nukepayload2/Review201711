Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Threading.Tasks

Imports Microsoft.Toolkit.Uwp.UI.Controls

Imports JpnExamCommandoX.Helpers
Imports JpnExamCommandoX.Models
Imports JpnExamCommandoX.Services

Namespace ViewModels
    Public Class WordsViewModel
        Inherits Observable

        Private _selected As WordDescription

        Public Property Selected As WordDescription
            Get
                Return _selected
            End Get
            Set
                [Set](_selected, Value)
            End Set
        End Property

        Dim _playStatus As New PlayStatus
        Public Property PlayStatus As PlayStatus
            Get
                Return _playStatus
            End Get
            Set(value As PlayStatus)
                [Set](_playStatus, value)
            End Set
        End Property

        Public Property SampleItems As New ObservableCollection(Of WordDescription)

        Public Async Function LoadDataAsync(viewState As MasterDetailsViewState) As Task
            SampleItems.Clear()

            Dim data = Await WordDataService.GetModelDataAsync()

            For Each item In data
                SampleItems.Add(item)
            Next

            If viewState = MasterDetailsViewState.Both Then
                Selected = SampleItems.First()
            End If
        End Function
    End Class
End Namespace
