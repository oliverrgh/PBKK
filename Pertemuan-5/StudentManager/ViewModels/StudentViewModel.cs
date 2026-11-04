using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using StudentManager.Data;
using StudentManager.Models;

namespace StudentManager.ViewModels;

public class StudentViewModel : INotifyPropertyChanged
{
    private readonly StudentRepository _repository = new();
    private Student _selectedStudent = NewStudent();
    private string _searchText = "";
    private string _statusMessage = "Siap. Pastikan database StudentDB sudah dibuat.";
    private int _totalStudents;
    private int _informatikaCount;
    private int _siCount;
    private int _maleCount;
    private int _femaleCount;

    public ObservableCollection<Student> Students { get; } = new();
    public string[] JurusanOptions { get; } = ["Informatika", "Sistem Informasi"];
    public string[] GenderOptions { get; } = ["Laki-laki", "Perempuan"];

    public Student SelectedStudent { get => _selectedStudent; set => SetField(ref _selectedStudent, value); }
    public string SearchText { get => _searchText; set => SetField(ref _searchText, value); }
    public string StatusMessage { get => _statusMessage; set => SetField(ref _statusMessage, value); }
    public int TotalStudents { get => _totalStudents; private set => SetField(ref _totalStudents, value); }
    public int InformatikaCount { get => _informatikaCount; private set => SetField(ref _informatikaCount, value); }
    public int SICount { get => _siCount; private set => SetField(ref _siCount, value); }
    public int MaleCount { get => _maleCount; private set => SetField(ref _maleCount, value); }
    public int FemaleCount { get => _femaleCount; private set => SetField(ref _femaleCount, value); }

    public RelayCommand SaveCommand { get; }
    public RelayCommand DeleteCommand { get; }
    public RelayCommand ResetCommand { get; }
    public RelayCommand SearchCommand { get; }
    public RelayCommand RefreshCommand { get; }

    public StudentViewModel()
    {
        SaveCommand = new RelayCommand(Save);
        DeleteCommand = new RelayCommand(Delete);
        ResetCommand = new RelayCommand(Reset);
        SearchCommand = new RelayCommand(Search);
        RefreshCommand = new RelayCommand(LoadData);
        LoadData();
    }

    private static Student NewStudent() => new() { Jurusan = "Informatika", Gender = "Laki-laki" };

    public void LoadData()
    {
        try
        {
            ReplaceStudents(_repository.GetAll());
            RefreshStatistics();
            StatusMessage = $"Berhasil memuat {Students.Count} data mahasiswa.";
        }
        catch (Exception ex) { ShowError("memuat data", ex); }
    }

    private void Save()
    {
        if (string.IsNullOrWhiteSpace(SelectedStudent.NIM) || string.IsNullOrWhiteSpace(SelectedStudent.Nama))
        {
            MessageBox.Show("NIM dan Nama wajib diisi.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        try
        {
            if (SelectedStudent.Id == 0) _repository.Insert(SelectedStudent);
            else _repository.Update(SelectedStudent);
            LoadData();
            Reset();
            StatusMessage = "Data mahasiswa berhasil disimpan.";
        }
        catch (Exception ex) { ShowError("menyimpan data", ex); }
    }

    private void Delete()
    {
        if (SelectedStudent.Id == 0)
        {
            MessageBox.Show("Pilih mahasiswa pada tabel yang ingin dihapus.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        var confirm = MessageBox.Show($"Hapus data {SelectedStudent.Nama}?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (confirm != MessageBoxResult.Yes) return;
        try
        {
            _repository.Delete(SelectedStudent.Id);
            LoadData();
            Reset();
            StatusMessage = "Data mahasiswa berhasil dihapus.";
        }
        catch (Exception ex) { ShowError("menghapus data", ex); }
    }

    private void Reset()
    {
        SelectedStudent = NewStudent();
        StatusMessage = "Form dikosongkan.";
    }

    private void Search()
    {
        try
        {
            ReplaceStudents(string.IsNullOrWhiteSpace(SearchText) ? _repository.GetAll() : _repository.Search(SearchText.Trim()));
            RefreshStatistics();
            StatusMessage = $"Ditemukan {Students.Count} data.";
        }
        catch (Exception ex) { ShowError("mencari data", ex); }
    }

    private void ReplaceStudents(IEnumerable<Student> items)
    {
        Students.Clear();
        foreach (var student in items) Students.Add(student);
    }

    private void RefreshStatistics()
    {
        TotalStudents = Students.Count;
        InformatikaCount = Students.Count(s => s.Jurusan == "Informatika");
        SICount = Students.Count(s => s.Jurusan == "Sistem Informasi");
        MaleCount = Students.Count(s => s.Gender == "Laki-laki");
        FemaleCount = Students.Count(s => s.Gender == "Perempuan");
    }

    private void ShowError(string action, Exception ex)
    {
        StatusMessage = $"Gagal {action}: {ex.Message}";
        MessageBox.Show(StatusMessage + "\n\nPastikan SQL Server LocalDB aktif dan database StudentDB sudah dibuat.",
            "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
