# Pertemuan 2 — Pemrograman Berbasis Kerangka Kerja (PBKK)

| Detail | Keterangan |
| --- | --- |
| **Nama Lengkap** | Nathanael Oliver Amadhika Yuswana |
| **NRP** | 5025241109 |
| **Mata Kuliah** | Pemrograman Berbasis Kerangka Kerja |
| **Kelas** | D |

---

Repository ini berisi dua project latihan .NET 8:

- `HelloWorld`: aplikasi console sederhana.
- `SistemDataMahasiswa`: aplikasi Windows Forms untuk mengelola data mahasiswa.

## Konsol HelloWorld

Aplikasi console sederhana yang menampilkan pesan `Hello, World!`.

### Build & Run

Dari folder utama repository:

```powershell
dotnet new console -n HelloWorld
dotnet run --project .\HelloWorld\HelloWorld.csproj
```

Dokumentasi:
<img width="523" height="65" alt="image" src="https://github.com/user-attachments/assets/6442cd02-53ce-443a-b12c-469a57cb9c97" />

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

### Build & Run

Dari folder utama repository:

```powershell
dotnet new winforms -n SistemDataMahasiswa
dotnet run --project .\SistemDataMahasiswa\SistemDataMahasiswa.csproj
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

### Dokumentasi

Tampilan Awal
<img width="1098" height="797" alt="image" src="https://github.com/user-attachments/assets/999b971c-5bf0-4b44-9e78-6d048d44bcfe" />

Penambahan Data
<img width="1098" height="802" alt="image" src="https://github.com/user-attachments/assets/3972a2a4-030c-4e09-8b78-9626322c24e8" />

Pencarian Data 
<img width="1100" height="787" alt="image" src="https://github.com/user-attachments/assets/7aff5005-cf4c-44ad-91f1-b990e17dfaf5" />
<img width="1100" height="782" alt="image" src="https://github.com/user-attachments/assets/c2443df5-0404-485e-94d7-8d75e5ea34a6" />

Tampilan Edit Data (NRP Terkunci)
<img width="1098" height="797" alt="image" src="https://github.com/user-attachments/assets/0c13c002-3062-4952-a4b5-9d819c5a32d0" />

Edit Data Berhasil
<img width="1100" height="795" alt="image" src="https://github.com/user-attachments/assets/6a8db6ba-8291-434d-bb7f-9a22f959e4a2" />

Hapus Data
<img width="1102" height="790" alt="image" src="https://github.com/user-attachments/assets/3aed6dcf-b4e1-4ab8-9257-11023a17daf1" />

Validasi Parameter Input Data
<img width="1098" height="797" alt="image" src="https://github.com/user-attachments/assets/390c95cc-096e-4be5-920c-90b22645e669" />

NRP Tidak Boleh Sama
<img width="1097" height="780" alt="image" src="https://github.com/user-attachments/assets/b4e6b8f4-c565-4657-bce9-4b167be650be" />

Validasi Input IPK
<img width="1102" height="790" alt="image" src="https://github.com/user-attachments/assets/360e6e87-8ad7-4091-afd8-d5ee2acc5a7e" />


