<<<<<<< HEAD
using System.Windows;
using BookingKampus.ViewModels;

namespace BookingKampus.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        Loaded += (_, _) => UsernameBox.Focus();
    }

    async void OnLoginClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is not LoginViewModel vm) return;
        vm.Password = PasswordBox.Password;
        await vm.LoginCommand.ExecuteAsync(null);
        PasswordBox.Clear();
    }
}
=======
using System.Windows;
using BookingKampus.ViewModels;

namespace BookingKampus.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        Loaded += (_, _) => UsernameBox.Focus();
    }

    async void OnLoginClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is not LoginViewModel vm) return;
        vm.Password = PasswordBox.Password;
        await vm.LoginCommand.ExecuteAsync(null);
        PasswordBox.Clear();
    }
}
>>>>>>> 483c16a2c6c6014653cb604fd45629248707663c
