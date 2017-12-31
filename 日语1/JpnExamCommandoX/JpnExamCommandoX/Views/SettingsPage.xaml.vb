Imports JpnExamCommandoX.ViewModels

Imports Windows.UI.Xaml
Imports Windows.UI.Xaml.Controls

Namespace Views
    Public NotInheritable Partial Class SettingsPage
        Inherits Page
            property ViewModel as SettingsViewModel = New SettingsViewModel
        ' TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere
        Public Sub New()
            InitializeComponent()
            ViewModel.Initialize()
        End Sub
    End Class
End Namespace
