# Pertemuan 5 — Pemrograman Berbasis Kerangka Kerja (PBKK)

| Detail | Keterangan |
| --- | --- |
| **Nama Lengkap** | Nathanael Oliver Amadhika Yuswana |
| **NRP** | 5025241109 |
| **Mata Kuliah** | Pemrograman Berbasis Kerangka Kerja |
| **Kelas** | D |

---

Repository ini berisi project latihan aplikasi desktop **Student Management System** menggunakan WPF dan .NET 8. Aplikasi digunakan untuk mengelola data mahasiswa yang tersimpan di SQL Server.

## Student Management System

Aplikasi dibangun dengan C#, XAML, pola MVVM, data binding, ADO.NET, dan SQL Server.

### Fitur

- Menambahkan dan mengubah data mahasiswa.
- Menghapus data mahasiswa dengan konfirmasi.
- Mencari data berdasarkan NIM, nama, atau jurusan.
- Menampilkan data dalam tabel dan mengisi form saat baris dipilih.
- Menampilkan jumlah mahasiswa, mahasiswa per jurusan, dan mahasiswa berdasarkan gender.
- Menyimpan data secara persisten di SQL Server.

Data mahasiswa mencakup NIM, nama, jurusan, gender, dan email. NIM dan nama wajib diisi.

### Persyaratan

- Windows 10/11.
- .NET 8 SDK.
- SQL Server yang dapat diakses dari komputer, misalnya SQL Server Express LocalDB.
- SQL Server Management Studio (SSMS) atau ekstensi MSSQL di VS Code untuk menjalankan skrip database (opsional).

### Persiapan Database

1. Buka dan jalankan seluruh isi `StudentManager/database.sql` melalui SSMS atau ekstensi MSSQL. Skrip membuat database `StudentDB`, tabel `Students`, dan empat data contoh.
2. Buka `StudentManager/Data/StudentRepository.cs` dan sesuaikan nilai `_connectionString` dengan instance SQL Server di komputer. Contoh untuk LocalDB:

   ```csharp
   @"Server=(localdb)\MSSQLLocalDB;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;"
   ```

3. Jika menggunakan LocalDB, pastikan instance tersedia. Dari PowerShell, periksa dengan `sqllocaldb info` dan jalankan `sqllocaldb start MSSQLLocalDB` bila diperlukan.

> Connection string yang ada di project saat ini berisi nama server tertentu. Ubah sebelum menjalankan aplikasi jika server tersebut tidak tersedia di komputer Anda.

### Build & Run

Dari folder utama repository:

```powershell
dotnet restore .\Pertemuan-5\StudentManager\StudentManager.csproj
dotnet run --project .\Pertemuan-5\StudentManager\StudentManager.csproj
```

### Cara Penggunaan

1. Isi NIM, nama, jurusan, gender, dan email pada form.
2. Gunakan tombol simpan untuk menambahkan data atau memperbarui data yang sedang dipilih.
3. Pilih baris di tabel untuk memuat datanya ke form.
4. Gunakan tombol hapus untuk menghapus data terpilih, lalu konfirmasi penghapusan.
5. Masukkan kata kunci pencarian untuk mencari berdasarkan NIM, nama, atau jurusan. Kosongkan pencarian untuk memuat semua data.
6. Gunakan tombol reset untuk mengosongkan form.

Statistik mengikuti data yang sedang ditampilkan, sehingga hasil pencarian juga memengaruhi angka statistik.

### Dokumentasi

Simpan screenshot pada folder `Pertemuan-5/dokumentasi/`, lalu gunakan nama file pada gambar di bawah. Screenshot sebaiknya menampilkan jendela aplikasi secara utuh, termasuk tabel, statistik, dan pesan status yang relevan.

#### 1. Tampilan Awal

Tampilkan aplikasi setelah berhasil terhubung ke database dan memuat data contoh. Pastikan tabel berisi data mahasiswa dan kartu statistik menampilkan jumlah total, jurusan, serta gender.

![Tampilan awal aplikasi dan statistik](./dokumentasi/tampilan-awal.png)

#### 2. Penambahan Data Berhasil

Isi form dengan data mahasiswa baru yang belum ada di database, lalu klik **Simpan Data**. Tampilkan data baru pada tabel dan pesan bahwa data berhasil disimpan.

![Penambahan data mahasiswa berhasil](./dokumentasi/tambah-data.png)

#### 3. Memilih Data untuk Diedit

Pilih salah satu baris pada tabel. Tampilkan data terpilih yang terisi pada form sebagai bukti bahwa baris dapat digunakan untuk mengubah data.

![Data terpilih tampil pada form](./dokumentasi/pilih-data.png)

#### 4. Perubahan Data Berhasil

Ubah salah satu informasi mahasiswa yang dipilih, simpan perubahan, lalu tampilkan nilai terbaru pada tabel beserta pesan keberhasilan.

![Perubahan data mahasiswa berhasil](./dokumentasi/edit-data.png)

#### 5. Pencarian Data

Masukkan kata kunci yang cocok dengan NIM, nama, atau jurusan, lalu klik **Cari**. Tampilkan hasil yang sesuai dan jumlah hasil pada pesan status. Statistik di bagian atas mengikuti data hasil pencarian.

![Hasil pencarian mahasiswa](./dokumentasi/pencarian-data.png)

#### 6. Konfirmasi dan Hasil Penghapusan

Pilih baris yang akan dihapus dan klik **Hapus Data Terpilih**. Dokumentasikan dialog konfirmasi, kemudian ambil screenshot lagi setelah memilih **Yes** untuk menunjukkan baris sudah hilang dan pesan keberhasilan tampil.

![Konfirmasi penghapusan data](./dokumentasi/konfirmasi-hapus.png)

![Data berhasil dihapus](./dokumentasi/hapus-data.png)

#### 7. Validasi Input Wajib

Kosongkan NIM atau nama, lalu klik **Simpan Data**. Tampilkan pesan validasi yang meminta NIM dan nama diisi.

![Validasi NIM dan nama wajib diisi](./dokumentasi/validasi-input.png)

#### 8. Reset Form

Isi beberapa kolom atau pilih data dari tabel, lalu klik **Reset Form**. Tampilkan form yang kembali kosong dengan pilihan jurusan dan gender default serta pesan status reset.

![Form berhasil direset](./dokumentasi/reset-form.png)
### Struktur

- `StudentManager/App.xaml` dan `StudentManager/App.xaml.cs`: konfigurasi dan titik awal aplikasi WPF.
- `StudentManager/MainWindow.xaml`: tampilan aplikasi.
- `StudentManager/MainWindow.xaml.cs`: inisialisasi jendela dan ViewModel.
- `StudentManager/Models/Student.cs`: model data mahasiswa dan notifikasi perubahan properti.
- `StudentManager/ViewModels/StudentViewModel.cs`: state tampilan, perintah, validasi, statistik, serta alur aplikasi.
- `StudentManager/Data/StudentRepository.cs`: operasi CRUD dan pencarian ke SQL Server.
- `StudentManager/RelayCommand.cs`: implementasi perintah untuk menghubungkan aksi UI dengan ViewModel.
- `StudentManager/database.sql`: pembuatan database, tabel, dan data contoh.
- `StudentManager/StudentManager.csproj`: konfigurasi project .NET 8 WPF dan dependensi SqlClient.

### Catatan

- WPF hanya dapat dijalankan di Windows.
- Jika aplikasi menampilkan kesalahan koneksi, periksa connection string, status SQL Server/LocalDB, dan pastikan database `StudentDB` sudah dibuat.

