Namespace ViewModels
    Public Class PivotViewModel
        Implements INotifyPropertyChanged

        Public Shared ReadOnly Property Instance As PivotViewModel

        Sub New()
            _Instance = Me
        End Sub

        Public ReadOnly Property Title As String
            Get
                Return Package.Current.DisplayName
            End Get
        End Property

        Dim _TitleMargin As New Thickness(12, 8, 8, 8)
        Public Property TitleMargin As Thickness
            Get
                Return _TitleMargin
            End Get
            Set
                If Not _TitleMargin.Equals(Value) Then
                    _TitleMargin = Value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TitleMargin)))
                End If
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    End Class
End Namespace
