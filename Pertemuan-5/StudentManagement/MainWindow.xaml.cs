using System.Windows;
using StudentManagement.ViewModels;

namespace StudentManagement;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new StudentViewModel();
    }
}