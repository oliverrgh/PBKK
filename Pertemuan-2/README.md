# Pertemuan 2 - Project .NET

Repository ini berisi dua project latihan .NET 8:

- `HelloWorld`: aplikasi console sederhana.
- `SistemDataMahasiswa`: aplikasi Windows Forms untuk mengelola data mahasiswa.

## Persyaratan Umum

- .NET SDK 8.0 atau lebih baru.
- Windows diperlukan untuk menjalankan project `SistemDataMahasiswa`.

## HelloWorld

Aplikasi console sederhana yang menampilkan pesan `Hello, World!`.

### Menjalankan

Dari folder utama repository:

```powershell
dotnet run --project .\HelloWorld\HelloWorld.csproj
```

Output:

```text
Hello, World!
```

### Struktur

- `HelloWorld/Program.cs`: titik masuk aplikasi.
- `HelloWorld/HelloWorld.csproj`: konfigurasi project .NET 8 console.

## Sistem Data Mahasiswa

Aplikasi desktop Windows Forms untuk mengelola data mahasiswa.

### Fitur

- Menambahkan data mahasiswa.
- Mengubah data mahasiswa yang dipilih.
- Menghapus data mahasiswa.
- Mencari data berdasarkan NRP atau nama.
- Menampilkan data dalam tabel.
- Memvalidasi input NRP, nama, program studi, dan IPK antara 0 sampai 4.

### Menjalankan

Dari folder utama repository:

```powershell
dotnet run --project .\SistemDataMahasiswa\SistemDataMahasiswa.csproj
```

Untuk membuat build tanpa menjalankan aplikasi:

```powershell
dotnet build .\SistemDataMahasiswa\SistemDataMahasiswa.csproj
```

### Cara Penggunaan

1. Isi NRP, nama lengkap, program studi, dan IPK.
2. Klik `SIMPAN DATA` untuk menambahkan data.
3. Pilih baris pada tabel, lalu klik `EDIT TERPILIH` untuk mengubah data.
4. Pilih baris pada tabel, lalu klik `HAPUS TERPILIH` untuk menghapus data.
5. Masukkan NRP atau nama pada kolom pencarian, lalu klik `CARI`.
6. Klik `RESET` untuk mengosongkan form.

Data aplikasi disimpan sementara di memori dan akan hilang ketika aplikasi ditutup.

### Struktur

- `SistemDataMahasiswa/Program.cs`: titik masuk aplikasi Windows Forms.
- `SistemDataMahasiswa/Form1.cs`: layout, warna, event tombol, validasi, dan interaksi pengguna.
- `SistemDataMahasiswa/Mahasiswa.cs`: model data mahasiswa.
- `SistemDataMahasiswa/MahasiswaService.cs`: proses tambah, ubah, hapus, tampil, dan pencarian data.
- `SistemDataMahasiswa/SistemDataMahasiswa.csproj`: konfigurasi project .NET 8 Windows Forms.
