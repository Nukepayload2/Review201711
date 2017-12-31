Imports Windows.UI.Popups
Public Module Interaction
    Public Async Function MsgBoxAsync(Prompt$, Optional HasCancel As Boolean = False, Optional Title$ = Nothing, Optional OK$ = "确定", Optional Cancel$ = "取消") As Task(Of Boolean?)
        If Title Is Nothing Then
            Title = Package.Current.DisplayName
        End If
        Dim dlg As New MessageDialog(Prompt, Title)
        Dim Result As Boolean?
        Dim msg As New MessageDialog(Prompt, Title)
        msg.Commands.Add(New UICommand(OK, Sub(command) Result = True))
        msg.DefaultCommandIndex = 0
        If HasCancel Then
            msg.Commands.Add(New UICommand(Cancel, Sub(command) Result = False))
            msg.CancelCommandIndex = 1
        End If
        Dim tsk = msg.ShowAsync
        Await tsk
        Return Result
    End Function

End Module
