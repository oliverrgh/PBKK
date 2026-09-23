# Kalkulator Sederhana

Aplikasi kalkulator desktop berbasis Windows Forms dengan .NET 8. Aplikasi ini memiliki tampilan terang dan menyediakan operasi aritmatika dasar.

## Teknologi

- .NET 8
- C#
- Windows Forms
- Visual Studio atau Visual Studio Code dengan .NET SDK

## Struktur Proyek

```text
Pertemuan-3/
|- KalkulatorSederhana/
|  |- Form1.cs
|  |- Form1.Designer.cs
|  |- Program.cs
|  `- KalkulatorSederhana.csproj
`- README.md
```

## Build & Run

Buka terminal pada folder `Pertemuan-3`, kemudian jalankan:

```powershell
dotnet new winforms -n KalkulatorSederhana
dotnet run --project .\KalkulatorSederhana\KalkulatorSederhana.csproj
```

## Fitur

- Operasi penjumlahan, pengurangan, perkalian, dan pembagian.
- Input bilangan desimal.
- Tombol persen (`%`).
- Tombol perubahan tanda bilangan (`+/-`).
- Tombol hapus satu karakter (`DEL`).
- Tombol reset (`C`).
- Dukungan tombol keyboard:
  - `Enter` untuk menghitung hasil.
  - `Escape` untuk reset.
  - `Backspace` untuk menghapus satu karakter.
  - Tombol operator pada numpad untuk memilih operasi.
- Pesan validasi ketika pengguna mencoba membagi bilangan dengan nol.

## Skenario Pengujian

Jalankan aplikasi, lalu ikuti setiap langkah berikut. Pastikan menekan tombol `C` sebelum memulai skenario baru agar kondisi awal kembali ke `0`.

| ID | Skenario | Langkah Pengujian | Hasil yang Diharapkan |
| --- | --- | --- | --- |
| TC-01 | Penjumlahan | Tekan `8`, `+`, `7`, lalu `=`. | Display menunjukkan `15`. |
| TC-02 | Pengurangan | Tekan `20`, `-`, `6`, lalu `=`. | Display menunjukkan `14`. |
| TC-03 | Perkalian | Tekan `9`, `*`, `5`, lalu `=`. | Display menunjukkan `45`. |
| TC-04 | Pembagian | Tekan `81`, `/`, `9`, lalu `=`. | Display menunjukkan `9`. |
| TC-05 | Bilangan desimal | Tekan `1`, `.`, `5`, `+`, `2`, `.`, `25`, lalu `=`. | Display menunjukkan `3.75`. |
| TC-06 | Persentase | Tekan `5`, `0`, lalu `%`. | Display menunjukkan `0.5`. |
| TC-07 | Perubahan tanda | Tekan `7`, lalu `+/-`. | Display berubah menjadi `-7`. Tekan `+/-` lagi untuk kembali menjadi `7`. |
| TC-08 | Hapus karakter | Tekan `1`, `2`, `3`, lalu `DEL`. | Display berubah dari `123` menjadi `12`. |
| TC-09 | Reset kalkulator | Masukkan operasi apa pun, lalu tekan `C`. | Display kembali menjadi `0` dan operasi yang sedang berjalan dibatalkan. |
| TC-10 | Pembagian dengan nol | Tekan `8`, `/`, `0`, lalu `=`. | Display menunjukkan pesan `Tidak bisa dibagi 0`. |
| TC-11 | Keyboard | Tekan angka dan operator menggunakan keyboard, lalu tekan `Enter`. | Perhitungan berjalan seperti saat tombol pada layar digunakan. |
| TC-12 | Operator berurutan | Tekan `10`, `+`, `5`, `*`, `2`, lalu `=`. | Hasil dihitung berdasarkan urutan input kalkulator dan display menunjukkan `30`. |

## Hasil Pengujian Terakhir

- Perintah `dotnet build` berhasil.
- Tidak ada error kompilasi.
- Tidak ada warning kompilasi.

## Catatan

Kalkulator menggunakan urutan operasi berdasarkan tombol yang ditekan. Aplikasi tidak menggunakan aturan prioritas operator matematika seperti kalkulator ilmiah.
