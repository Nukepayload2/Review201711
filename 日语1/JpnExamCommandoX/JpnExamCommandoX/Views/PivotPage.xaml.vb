Imports JpnExamCommandoX.ViewModels

Imports Windows.UI.Xaml.Controls
Imports Windows.UI.Xaml.Navigation

Namespace Views
    Partial Public NotInheritable Class PivotPage
        Inherits Page

        Property ViewModel As PivotViewModel = New PivotViewModel

        Public Sub New()
            Me.InitializeComponent()
            ' We use NavigationCacheMode.Required to keep track the selected item on navigation. For further information see the following links.
            ' https://msdn.microsoft.com/en-us/library/windows/apps/xaml/windows.ui.xaml.controls.page.navigationcachemode.aspx
            ' https://msdn.microsoft.com/en-us/library/windows/apps/xaml/Hh771188.aspx
            NavigationCacheMode = NavigationCacheMode.Required
            DataContext = ViewModel
        End Sub
    End Class
End Namespace
