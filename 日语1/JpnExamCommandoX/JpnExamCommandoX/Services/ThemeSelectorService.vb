Imports System.Threading.Tasks

Imports JpnExamCommandoX.Helpers

Imports Windows.Storage
Imports Windows.UI
Imports Windows.UI.Xaml

Namespace Services
    Public NotInheritable Class ThemeSelectorService
        Private Sub New()
        End Sub
        Private Const SettingsKey As String = "RequestedTheme"

        Public Shared Property Theme As ElementTheme = ElementTheme.Default

        Public Shared Async Function InitializeAsync() As Task
            Theme = Await LoadThemeFromSettingsAsync()
            SetTitleBarForeColor()
        End Function

        Private Shared Sub SetTitleBarForeColor()
            If TypeOf Window.Current.Content Is FrameworkElement Then
                Dim frameworkElement = TryCast(Window.Current.Content, FrameworkElement)
                Dim theme = frameworkElement.RequestedTheme
                Dim titleBar As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar
                Select Case theme
                    Case ElementTheme.Dark
                        titleBar.ButtonForegroundColor = Colors.White
                    Case ElementTheme.Default
                        titleBar.ButtonForegroundColor = If(Application.Current.RequestedTheme = ApplicationTheme.Light, Colors.Black, Colors.White)
                    Case ElementTheme.Light
                        titleBar.ButtonForegroundColor = Colors.Black
                End Select
            End If
        End Sub

        Public Shared Async Function SetThemeAsync(theme As ElementTheme) As Task
            ThemeSelectorService.Theme = theme

            SetRequestedTheme()
            Await SaveThemeInSettingsAsync(theme)
        End Function

        Public Shared Sub SetRequestedTheme()
            If TypeOf Window.Current.Content Is FrameworkElement Then
                Dim frameworkElement = TryCast(Window.Current.Content, FrameworkElement)
                frameworkElement.RequestedTheme = Theme
                SetTitleBarForeColor()
            End If
        End Sub

        Private Shared Async Function LoadThemeFromSettingsAsync() As Task(Of ElementTheme)
            Dim cacheTheme As ElementTheme = ElementTheme.[Default]
            Dim themeName As String = Await ApplicationData.Current.LocalSettings.ReadAsync(Of String)(SettingsKey)

            If Not String.IsNullOrEmpty(themeName) Then
                [Enum].TryParse(themeName, cacheTheme)
            End If

            Return cacheTheme
        End Function

        Private Shared Async Function SaveThemeInSettingsAsync(theme As ElementTheme) As Task
            Await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, theme.ToString())
        End Function
    End Class
End Namespace
