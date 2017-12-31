Namespace Models

    Public Class PlayStatus
        Implements INotifyPropertyChanged

        Dim _isPlaying As Boolean
        Public Property IsPlaying As Boolean
            Get
                Return _isPlaying
            End Get
            Set
                If Not _isPlaying.Equals(Value) Then
                    _isPlaying = Value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsPlaying)))
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsBrainWashEnabled)))
                End If
            End Set
        End Property

        Dim _isBrainWashing As Boolean
        Public Property IsBrainWashing As Boolean
            Get
                Return _isBrainWashing
            End Get
            Set
                If Not IsBrainWashing.Equals(Value) Then
                    _isBrainWashing = Value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsBrainWashing)))
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsBrainWashEnabled)))
                End If
            End Set
        End Property

        Dim _IsPlayerIdle As Boolean = True
        Public Property IsPlayerIdle As Boolean
            Get
                Return _IsPlayerIdle
            End Get
            Set
                If Not _IsPlayerIdle.Equals(Value) Then
                    _IsPlayerIdle = Value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsPlayerIdle)))
                End If
            End Set
        End Property

        Public ReadOnly Property IsBrainWashEnabled As String
            Get
                Return Not IsPlaying OrElse IsBrainWashing
            End Get
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class

End Namespace
