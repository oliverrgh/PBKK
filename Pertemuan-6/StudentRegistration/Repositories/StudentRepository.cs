using Microsoft.Data.SqlClient;
using StudentRegistration.Models;
    
namespace StudentRegistration.Repositories;

public class StudentRepository
{
    private readonly string ConnectionString;

    public StudentRepository(string? connectionString = null)
    {
        ConnectionString = connectionString ?? DatabaseConfig.ConnectionString;
    }

    public List<Student> GetAll()
    {
        var students = new List<Student>();

        const string sql = """
            SELECT
                s.StudentId,
                s.NIM,
                s.Name,
                s.ProgramId,
                p.ProgramCode,
                p.ProgramName,
                s.BirthDate,
                s.Address,
                s.PhoneNumber,
                s.CreatedAt,
                s.UpdatedAt
            FROM Students AS s
            INNER JOIN Programs AS p
                ON s.ProgramId = p.ProgramId
            ORDER BY s.StudentId;
            """;

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(sql, connection);

        connection.Open();

        using var reader = command.ExecuteReader();

        int studentIdOrdinal = reader.GetOrdinal("StudentId");
        int nimOrdinal = reader.GetOrdinal("NIM");
        int nameOrdinal = reader.GetOrdinal("Name");
        int programIdOrdinal = reader.GetOrdinal("ProgramId");
        int programCodeOrdinal = reader.GetOrdinal("ProgramCode");
        int programNameOrdinal = reader.GetOrdinal("ProgramName");
        int birthDateOrdinal = reader.GetOrdinal("BirthDate");
        int addressOrdinal = reader.GetOrdinal("Address");
        int phoneNumberOrdinal = reader.GetOrdinal("PhoneNumber");
        int createdAtOrdinal = reader.GetOrdinal("CreatedAt");
        int updatedAtOrdinal = reader.GetOrdinal("UpdatedAt");

        while (reader.Read())
        {
            students.Add(new Student
            {
                StudentId = reader.GetInt32(studentIdOrdinal),
                NIM = reader.GetString(nimOrdinal),
                Name = reader.GetString(nameOrdinal),
                ProgramId = reader.GetInt32(programIdOrdinal),
                ProgramCode = reader.GetString(programCodeOrdinal),
                ProgramName = reader.GetString(programNameOrdinal),
                BirthDate = reader.IsDBNull(birthDateOrdinal)
                    ? null
                    : reader.GetDateTime(birthDateOrdinal),
                Address = reader.IsDBNull(addressOrdinal)
                    ? string.Empty
                    : reader.GetString(addressOrdinal),
                PhoneNumber = reader.IsDBNull(phoneNumberOrdinal)
                    ? string.Empty
                    : reader.GetString(phoneNumberOrdinal),
                CreatedAt = reader.GetDateTime(createdAtOrdinal),
                UpdatedAt = reader.IsDBNull(updatedAtOrdinal)
                    ? null
                    : reader.GetDateTime(updatedAtOrdinal)
            });
        }

        return students;
    }

    public int GetTotalStudents()
    {
        const string sql = "SELECT COUNT(*) FROM Students;";

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(sql, connection);

        connection.Open();
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public List<(string ProgramCode, string ProgramName, int StudentCount)> GetProgramStatistics()
    {
        var statistics = new List<(string ProgramCode, string ProgramName, int StudentCount)>();
        const string sql = """
            SELECT p.ProgramCode, p.ProgramName, COUNT(s.StudentId) AS StudentCount
            FROM Programs AS p
            LEFT JOIN Students AS s ON s.ProgramId = p.ProgramId
            GROUP BY p.ProgramCode, p.ProgramName
            ORDER BY p.ProgramName;
            """;

        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            statistics.Add((
                reader.GetString(0),
                reader.GetString(1),
                reader.GetInt32(2)));
        }

        return statistics;
    }
}
