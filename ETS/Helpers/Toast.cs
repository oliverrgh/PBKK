<<<<<<< HEAD
namespace BookingKampus.Helpers;

/// <summary>Pesan singkat hasil aksi (berhasil atau gagal) yang ditampilkan di jendela utama.</summary>
public static class Toast
{
    public static event Action<string, bool> Raised;
    public static void Show(string message, bool isError = false) => Raised?.Invoke(message, isError);
}
=======
namespace BookingKampus.Helpers;

/// <summary>Pesan singkat hasil aksi (berhasil atau gagal) yang ditampilkan di jendela utama.</summary>
public static class Toast
{
    public static event Action<string, bool> Raised;
    public static void Show(string message, bool isError = false) => Raised?.Invoke(message, isError);
}
>>>>>>> 483c16a2c6c6014653cb604fd45629248707663c
