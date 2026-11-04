using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentManager.Models;

public class Student : INotifyPropertyChanged
{
    private int _id;
    private string _nim = "";
    private string _nama = "";
    private string _jurusan = "Informatika";
    private string _gender = "Laki-laki";
    private string _email = "";

    public int Id { get => _id; set => SetField(ref _id, value); }
    public string NIM { get => _nim; set => SetField(ref _nim, value); }
    public string Nama { get => _nama; set => SetField(ref _nama, value); }
    public string Jurusan { get => _jurusan; set => SetField(ref _jurusan, value); }
    public string Gender { get => _gender; set => SetField(ref _gender, value); }
    public string Email { get => _email; set => SetField(ref _email, value); }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
