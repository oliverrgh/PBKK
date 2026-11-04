<<<<<<< HEAD
using BookingKampus.Helpers;
using BookingKampus.Models;

namespace BookingKampus.Repositories;

public static class StatistikRepository
{
    public static async Task<DashboardStats> GetStatsAsync() => new()
    {
        Menunggu = await Db.ScalarAsync<int>("SELECT COUNT(*) FROM dbo.Booking WHERE [Status] = 'Menunggu'"),
        RuanganAktif = await Db.ScalarAsync<int>("SELECT COUNT(*) FROM dbo.Ruangan WHERE IsActive = 1"),
        FasilitasAktif = await Db.ScalarAsync<int>("SELECT COUNT(*) FROM dbo.Fasilitas WHERE IsActive = 1"),
        DisetujuiBulanIni = await Db.ScalarAsync<int>(
            @"SELECT COUNT(*) FROM dbo.Booking
              WHERE [Status] = 'Disetujui'
                AND YEAR(Tanggal) = YEAR(GETDATE()) AND MONTH(Tanggal) = MONTH(GETDATE())")
    };
}
=======
using BookingKampus.Helpers;
using BookingKampus.Models;

namespace BookingKampus.Repositories;

public static class StatistikRepository
{
    public static async Task<DashboardStats> GetStatsAsync() => new()
    {
        Menunggu = await Db.ScalarAsync<int>("SELECT COUNT(*) FROM dbo.Booking WHERE [Status] = 'Menunggu'"),
        RuanganAktif = await Db.ScalarAsync<int>("SELECT COUNT(*) FROM dbo.Ruangan WHERE IsActive = 1"),
        FasilitasAktif = await Db.ScalarAsync<int>("SELECT COUNT(*) FROM dbo.Fasilitas WHERE IsActive = 1"),
        DisetujuiBulanIni = await Db.ScalarAsync<int>(
            @"SELECT COUNT(*) FROM dbo.Booking
              WHERE [Status] = 'Disetujui'
                AND YEAR(Tanggal) = YEAR(GETDATE()) AND MONTH(Tanggal) = MONTH(GETDATE())")
    };
}
>>>>>>> 483c16a2c6c6014653cb604fd45629248707663c
