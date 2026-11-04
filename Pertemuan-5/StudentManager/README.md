# Student Management System (WPF .NET 8)

Project latihan WPF menggunakan C#, XAML, MVVM, Data Binding, ADO.NET, dan SQL Server LocalDB.

## Persyaratan
- Windows 10/11
- .NET 8 SDK
- SQL Server Express LocalDB
- VS Code dengan extension C# Dev Kit (opsional, membantu IntelliSense)

## Menjalankan project
1. Buka folder `StudentManager` di VS Code.
2. Pastikan .NET 8 tersedia dengan `dotnet --list-sdks`.
3. Jalankan `dotnet restore`.
4. Buat database menggunakan isi file `database.sql` melalui SQL Server Management Studio atau ekstensi MSSQL di VS Code.
5. Pastikan instance LocalDB tersedia. Di PowerShell jalankan `sqllocaldb info` lalu `sqllocaldb start MSSQLLocalDB` jika diperlukan.
6. Jalankan aplikasi dengan `dotnet run`.

## Package
Project menggunakan `Microsoft.Data.SqlClient`. Package akan ditambahkan oleh generator melalui `dotnet add package`.

## Fitur
- Tambah dan edit data mahasiswa
- Hapus data mahasiswa dengan konfirmasi
- Pencarian berdasarkan NIM, nama, atau jurusan
- DataGrid dan pemilihan baris untuk mengisi form
- Statistik total mahasiswa, jurusan, dan gender
- Pemisahan Model, ViewModel, Repository, dan View (XAML)

## Catatan
- WPF hanya berjalan di Windows.
- Statistik mengikuti data yang sedang ditampilkan. Setelah pencarian, statistik ikut menghitung hasil pencarian.
- Jika koneksi database gagal, pastikan LocalDB aktif dan database/tabel sudah dibuat.
