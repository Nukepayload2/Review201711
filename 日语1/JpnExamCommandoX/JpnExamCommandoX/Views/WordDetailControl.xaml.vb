Imports JpnExamCommandoX.Helpers
Imports JpnExamCommandoX.Models
Imports JpnExamCommandoX.Services
Imports JpnExamCommandoX.ViewModels
Imports Windows.Media.Core
Imports Windows.Media.Playback
Imports Windows.Media.SpeechSynthesis
Imports Windows.UI.Xaml
Imports Windows.UI.Xaml.Controls

Namespace Views
    Partial Public NotInheritable Class WordDetailControl
        Inherits UserControl
        Public Property ViewModel As WordsViewModel = Singleton(Of WordsViewModel).Instance

        Public Property MasterMenuItem As WordDescription
            Get
                Return TryCast(GetValue(MasterMenuItemProperty), WordDescription)
            End Get
            Set
                SetValue(MasterMenuItemProperty, Value)
            End Set
        End Property

        Public Shared ReadOnly MasterMenuItemProperty As DependencyProperty = DependencyProperty.Register("MasterMenuItem", GetType(WordDescription), GetType(WordDetailControl), New PropertyMetadata(Nothing, AddressOf OnMasterMenuItemPropertyChanged))

        Dim speech As New SpeechService

        Private Shared Sub OnMasterMenuItemPropertyChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim control = TryCast(d, WordDetailControl)
            control.ForegroundElement.ChangeView(0, 0, 1)
            control.InkTraining.InkPresenter.StrokeContainer.Clear()
        End Sub

        Private Sub BtnClearInk_Click(sender As Object, e As RoutedEventArgs) Handles BtnClearInk.Click
            InkTraining.InkPresenter.StrokeContainer.Clear()
        End Sub

        Private Sub BtnLoopPlay_Click(sender As Object, e As RoutedEventArgs) Handles BtnLoopPlay.Click
            Dim mdl = ViewModel
            If BtnLoopPlay.IsChecked Then
                Dim tsk = speech.BrainWashAsync(mdl.SampleItems, mdl.Selected, mdl.PlayStatus)
            Else
                mdl.PlayStatus.IsBrainWashing = False
            End If
        End Sub

        Private Sub BtnPlay_Click(sender As Object, e As RoutedEventArgs) Handles BtnPlay.Click
            Dim mdl = ViewModel
            Dim selected As WordDescription = mdl.Selected
            Dim japanese As String = selected.Japanese
            Dim tsk = speech.PlayAsync(selected.Japanese, selected.ChineseTip, String.Empty, mdl.PlayStatus)
        End Sub

        Private Sub WordDetailControl_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
            InkTraining.InkPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Touch Or Windows.UI.Core.CoreInputDeviceTypes.Pen Or Windows.UI.Core.CoreInputDeviceTypes.Mouse
        End Sub
    End Class
End Namespace
