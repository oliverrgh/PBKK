namespace StudentRegistration.Models;

/// <summary>
/// Model untuk tabel Programs (program studi), bukan entry point aplikasi.
/// </summary>
public class Program
{
    public int ProgramId { get; set; }
    public string ProgramCode { get; set; } = string.Empty;
    public string ProgramName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
