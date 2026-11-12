using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using StudentRegistration.Models;
using StudentRegistration.Repositories;

namespace StudentRegistration;

public partial class MainWindow : Window
{
    private readonly StudentRepository _studentRepository;
    private ICollectionView? _studentsView;

    public MainWindow()
    {
        InitializeComponent();

        _studentRepository = new StudentRepository();
        LoadStudents();
    }

    private void LoadStudents()
    {
        try
        {
            var students = _studentRepository.GetAll();
            SearchBox.Clear();
            _studentsView = CollectionViewSource.GetDefaultView(students);
            _studentsView.Filter = FilterStudent;
            StudentDataGrid.ItemsSource = _studentsView;

            int totalStudents = _studentRepository.GetTotalStudents();
            TotalStudentsText.Text = totalStudents.ToString();
            RenderProgramStatistics(_studentRepository.GetProgramStatistics());
            ConnectionStatusText.Text = "SYSTEM: ONLINE // ADO.NET OK";
            ConnectionStatusDot.Fill = new SolidColorBrush(Color.FromRgb(80, 220, 150));
            UpdateSearchStatus();
        }
        catch (Exception ex)
        {
            StatusText.Text = "Gagal memuat data dari database.";
            ConnectionStatusText.Text = "SYSTEM: OFFLINE // DATABASE ERROR";
            ConnectionStatusDot.Fill = new SolidColorBrush(Color.FromRgb(255, 100, 110));
            MessageBox.Show(
                "Tidak dapat mengambil data dari SQL Server.\n\n" +
                "Periksa apakah database StudentRegistrationDB sudah dibuat " +
                "dan connection string sudah sesuai.\n\nDetail error:\n" + ex.Message,
                "Database Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        LoadStudents();
    }

    private bool FilterStudent(object item)
    {
        if (item is not Student student)
            return false;

        string query = SearchBox.Text.Trim();
        if (query.Length == 0)
            return true;

        return Contains(student.NIM, query)
            || Contains(student.Name, query)
            || Contains(student.ProgramName, query)
            || Contains(student.Address, query);
    }

    private static bool Contains(string? value, string query) =>
        value?.Contains(query, StringComparison.OrdinalIgnoreCase) == true;

    private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        _studentsView?.Refresh();
        UpdateSearchStatus();
    }

    private void UpdateSearchStatus()
    {
        int visibleCount = _studentsView?.Cast<Student>().Count() ?? 0;
        int totalCount = _studentsView?.SourceCollection.Cast<Student>().Count() ?? 0;
        StatusText.Text = SearchBox.Text.Trim().Length == 0
            ? $"Menampilkan {totalCount} mahasiswa."
            : $"Ditemukan {visibleCount} dari {totalCount} mahasiswa untuk '{SearchBox.Text.Trim()}'.";
    }

    private void RenderProgramStatistics(
        IEnumerable<(string ProgramCode, string ProgramName, int StudentCount)> statistics)
    {
        ProgramStatisticsPanel.Children.Clear();
        foreach (var item in statistics)
        {
            var row = new System.Windows.Controls.Grid { Margin = new Thickness(0, 3, 0, 3) };
            row.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            row.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition { Width = GridLength.Auto });

            var name = new System.Windows.Controls.StackPanel();
            name.Children.Add(new System.Windows.Controls.TextBlock
            {
                Text = item.ProgramName,
                Foreground = new SolidColorBrush(Color.FromRgb(220, 230, 242)),
                FontSize = 12,
                TextTrimming = TextTrimming.CharacterEllipsis
            });
            name.Children.Add(new System.Windows.Controls.TextBlock
            {
                Text = item.ProgramCode,
                Foreground = new SolidColorBrush(Color.FromRgb(126, 150, 172)),
                FontSize = 10,
                Margin = new Thickness(0, 2, 0, 0)
            });

            var count = new System.Windows.Controls.TextBlock
            {
                Text = item.StudentCount.ToString(),
                Foreground = new SolidColorBrush(Color.FromRgb(244, 197, 66)),
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0)
            };
            System.Windows.Controls.Grid.SetColumn(count, 1);
            row.Children.Add(name);
            row.Children.Add(count);
            ProgramStatisticsPanel.Children.Add(row);
        }
    }
}
