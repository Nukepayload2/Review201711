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
                End If
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace
