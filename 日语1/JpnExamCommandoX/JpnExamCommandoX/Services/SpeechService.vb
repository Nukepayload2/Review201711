Imports JpnExamCommandoX.Models
Imports Windows.Media.Core
Imports Windows.Media.Playback
Imports Windows.Media.SpeechSynthesis
Imports Windows.System.Display

Namespace Services

    Public Class SpeechService

        Dim _speechJp As New SpeechSynthesizer
        Dim _speechCn As New SpeechSynthesizer

        Public Async Function CheckSpeechPrerequisiteAsync() As Task(Of Boolean)
            Dim voiceJp = From v In SpeechSynthesizer.AllVoices Where v.Language = "ja-JP"
            If Not voiceJp.Any Then
                Await MsgBoxAsync("请先安装日语语言包和日语语音。",, "缺少语音包")
                Return False
            Else
                _speechJp.Voice = voiceJp.First
                Return True
            End If
        End Function

        Public Async Function PlayAsync(japanese As String,
                                        chineseTip As String,
                                        chinese As String,
                                        status As PlayStatus) As Task
            If status.IsPlaying Then Return
            Dim wmp = MediaPlayerManager.MediaPlayer
            status.IsPlaying = True
            Dim idle = status.IsPlayerIdle
            status.IsPlayerIdle = False
            Try
                If Not Await CheckSpeechPrerequisiteAsync() Then
                    Return
                End If
                Dim jaStrm = Await _speechJp.SynthesizeTextToStreamAsync(japanese)
                Dim cnStrm = Await _speechCn.SynthesizeTextToStreamAsync(chineseTip)
                Dim fkCnStrm As SpeechSynthesisStream = Nothing
                If Not String.IsNullOrEmpty(chinese) Then
                    fkCnStrm = Await _speechCn.SynthesizeTextToStreamAsync(chinese)
                End If
                Await PlaySpeechCore(wmp, jaStrm, cnStrm, fkCnStrm)
            Finally
                status.IsPlaying = False
            End Try
            status.IsPlayerIdle = idle
        End Function

        Public Async Function BrainWashAsync(models As IReadOnlyList(Of WordDescription),
                                             curModel As WordDescription,
                                             status As PlayStatus) As Task
            If Not Await CheckSpeechPrerequisiteAsync() OrElse status.IsBrainWashing Then
                Return
            End If
            Dim wmp = MediaPlayerManager.MediaPlayer
            status.IsPlayerIdle = False
            Dim data = models
            status.IsBrainWashing = True
            Dim defaultIndex = 0
            If curModel IsNot Nothing Then
                For i = 0 To data.Count - 1
                    If data(i) Is curModel Then
                        defaultIndex = i
                    End If
                Next
            End If
            Dim req As New DisplayRequest
            req.RequestActive()
            Do While status.IsBrainWashing
                For i = defaultIndex To data.Count - 1
                    Dim mdl = data(i)
                    If mdl.IsRemembered Then Continue For
                    Await PlayAsync(mdl.Japanese, mdl.ChineseTip, String.Empty, status)
                    If Not status.IsBrainWashing Then
                        Exit Do
                    End If
                Next
                Await Task.Delay(100)
                defaultIndex = 0
            Loop
            req.RequestRelease()
            status.IsBrainWashing = False
            status.IsPlayerIdle = True
        End Function

        Private Async Function PlaySpeechCore(wmp As MediaPlayerElement,
                                              jaStrm As SpeechSynthesisStream,
                                              cnStrm As SpeechSynthesisStream,
                                              fkCnStrm As SpeechSynthesisStream) As Task
            Dim ja = MediaSource.CreateFromStream(jaStrm, jaStrm.ContentType)
            Dim waiting = True
            Dim handler As TypedEventHandler(Of MediaPlayer, Object) =
            Async Sub(s, arg)
                Await wmp.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                Sub()
                    RemoveHandler wmp.MediaPlayer.MediaEnded, handler
                    Dim cnHandler As TypedEventHandler(Of MediaPlayer, System.Object) =
                        Async Sub(s2, e2)
                            Await wmp.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                            Sub()
                                RemoveHandler wmp.MediaPlayer.MediaEnded, cnHandler
                                waiting = False
                            End Sub)
                        End Sub
                    Dim playCn =
                        Sub()
                            AddHandler wmp.MediaPlayer.MediaEnded, cnHandler
                            wmp.Source = MediaSource.CreateFromStream(cnStrm, cnStrm.ContentType)
                        End Sub
                    Dim fakeCnHandler As TypedEventHandler(Of MediaPlayer, System.Object) =
                        Async Sub(s2, e2)
                            Await wmp.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                            Sub()
                                RemoveHandler wmp.MediaPlayer.MediaEnded, fakeCnHandler
                                playCn()
                            End Sub)
                        End Sub
                    If fkCnStrm Is Nothing OrElse fkCnStrm.Size < 1000 Then
                        playCn()
                    Else
                        AddHandler wmp.MediaPlayer.MediaEnded, fakeCnHandler
                        wmp.Source = MediaSource.CreateFromStream(fkCnStrm, fkCnStrm.ContentType)
                    End If
                End Sub)
            End Sub
            AddHandler wmp.MediaPlayer.MediaEnded, handler
            wmp.Source = ja
            Do While waiting
                Await Task.Delay(1)
            Loop
            jaStrm.Dispose()
            cnStrm.Dispose()
            fkCnStrm?.Dispose()
        End Function

    End Class

End Namespace
