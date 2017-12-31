Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports JpnExamCommandoX.ViewModels

Imports Windows.UI.Xaml
Imports Windows.UI.Xaml.Controls
Imports JpnExamCommandoX.Services
Imports JpnExamCommandoX.Helpers

Namespace Views
    Partial Public NotInheritable Class WordsPage
        Inherits Page
        Property ViewModel As WordsViewModel = Singleton(Of WordsViewModel).Instance

        Private Async Sub SymbolsPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
            MediaPlayerManager.MediaPlayer = wmp
            Await ViewModel.LoadDataAsync(MasterDetailsViewControl.ViewState)
        End Sub

        Private Sub MasterDetailsViewControl_ViewStateChanged(sender As Object, e As MasterDetailsViewState) Handles MasterDetailsViewControl.ViewStateChanged
            If e = MasterDetailsViewState.Details Then
                If PivotViewModel.Instance IsNot Nothing Then
                    PivotViewModel.Instance.TitleMargin = New Thickness(52, 8, 8, 8)
                End If
            Else
                PivotViewModel.Instance.TitleMargin = New Thickness(12, 8, 8, 8)
            End If
        End Sub
    End Class
End Namespace
