<<<<<<< HEAD
using System.Windows;

namespace BookingKampus.Helpers;

public static class Dialogs
{
    public static bool Confirm(string message) =>
        MessageBox.Show(message, "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
}
=======
using System.Windows;

namespace BookingKampus.Helpers;

public static class Dialogs
{
    public static bool Confirm(string message) =>
        MessageBox.Show(message, "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
}
>>>>>>> 483c16a2c6c6014653cb604fd45629248707663c
