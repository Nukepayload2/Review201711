Imports JpnExamCommandoX.Helpers
Imports JpnExamCommandoX.Services
Imports JpnExamCommandoX.ViewModels
Imports Newtonsoft.Json

Namespace Models

    Public Class WordDescription
        Implements INotifyPropertyChanged

        Sub New()

        End Sub

        Public Sub New(japanese As String, japaneseHan As String, chineseTip As String)
            Me.Japanese = japanese
            Me.JapaneseHan = japaneseHan
            Me.ChineseTip = chineseTip
        End Sub

        Public Property Japanese As String
        Public Property JapaneseHan As String
        Public Property ChineseTip As String

        Dim _IsRemembered As Boolean
        Public Property IsRemembered As Boolean
            Get
                Return _IsRemembered
            End Get
            Set
                If Not _IsRemembered.Equals(Value) Then
                    _IsRemembered = Value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsRemembered)))
                    Dim tsk = WordDataService.SaveModelDataAsync(Singleton(Of WordsViewModel).Instance.SampleItems)
                End If
            End Set
        End Property

        <JsonIgnore>
        Public ReadOnly Property ToggleRememberedCommand As New RelayCommand(Sub() IsRemembered = Not IsRemembered)

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace
