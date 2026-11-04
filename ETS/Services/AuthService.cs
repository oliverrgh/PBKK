<<<<<<< HEAD
using BookingKampus.Helpers;
using BookingKampus.Repositories;

namespace BookingKampus.Services;

public static class AuthService
{
    public static string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public static async Task LoginAsync(string username, string password)
    {
        var user = await UserRepository.GetByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new AppException("Username atau kata sandi salah.");
        if (!user.IsActive)
            throw new AppException("Akun ini dinonaktifkan. Hubungi administrator.");

        Session.SignIn(user);
    }
}
=======
using BookingKampus.Helpers;
using BookingKampus.Repositories;

namespace BookingKampus.Services;

public static class AuthService
{
    public static string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public static async Task LoginAsync(string username, string password)
    {
        var user = await UserRepository.GetByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new AppException("Username atau kata sandi salah.");
        if (!user.IsActive)
            throw new AppException("Akun ini dinonaktifkan. Hubungi administrator.");

        Session.SignIn(user);
    }
}
>>>>>>> 483c16a2c6c6014653cb604fd45629248707663c
