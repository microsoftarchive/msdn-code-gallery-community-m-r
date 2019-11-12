
Imports System.Windows
Imports WPF.MDI



Partial Public Class MainWindow
    Inherits Window
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub menuExit_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub userRegistration_Click(sender As Object, e As RoutedEventArgs)
        MainMdiContainer.Children.Clear()
        'Here UserRegistration is the class that you have created for mainWindow.xaml user control.
        MainMdiContainer.Children.Add(New MdiChild() With { _
         .Title = " User Registration", _
         .Height = (System.Windows.SystemParameters.PrimaryScreenHeight - 15), _
         .Width = (System.Windows.SystemParameters.PrimaryScreenWidth - 15), _
         .Style = Nothing, _
         .Content = New UserRegistration() _
        })
    End Sub

    Private Sub compRegistration_Click(sender As Object, e As RoutedEventArgs)
        MainMdiContainer.Children.Clear()
        'Here compRegistration is the class that you have created for mainWindow.xaml user control.
        MainMdiContainer.Children.Add(New MdiChild() With { _
     .Title = " Company Registration", _
     .Height = (System.Windows.SystemParameters.PrimaryScreenHeight - 15), _
     .Width = (System.Windows.SystemParameters.PrimaryScreenWidth - 15), _
     .Style = Nothing, _
     .Content = New CompRegistration() _
        })
    End Sub

End Class

