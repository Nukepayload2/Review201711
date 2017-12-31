Imports System.Threading.Tasks

Imports JpnExamCommandoX.Services

Imports Windows.ApplicationModel.Activation

Namespace Activation
    Friend Class DefaultLaunchActivationHandler
        Inherits ActivationHandler(Of LaunchActivatedEventArgs)
        Private ReadOnly _navElement As Type
    
        Public Sub New(navElement As Type)
            _navElement = navElement
        End Sub
    
        Protected Overrides Async Function HandleInternalAsync(args As LaunchActivatedEventArgs) As Task
            ' When the navigation stack isn't restored, navigate to the first page and configure
            ' the new page by passing required information in the navigation parameter
            NavigationService.Navigate(_navElement, args.Arguments)

            Await Task.CompletedTask
        End Function

        Protected Overrides Function CanHandleInternal(args As LaunchActivatedEventArgs) As Boolean
            ' None of the ActivationHandlers has handled the app activation
            Return NavigationService.Frame.Content Is Nothing
        End Function
    End Class
End Namespace
