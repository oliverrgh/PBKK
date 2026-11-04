using System.Data;
using Microsoft.Data.SqlClient;
using StudentManager.Models;

namespace StudentManager.Data;

public class StudentRepository
{
    private readonly string _connectionString =
        @"Server=LAPTOP-KCF1PSHL;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public List<Student> GetAll()
    {
        var students = new List<Student>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(
            "SELECT Id, NIM, Nama, Jurusan, Gender, Email FROM Students ORDER BY Id DESC", connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read()) students.Add(MapStudent(reader));
        return students;
    }

    public void Insert(Student student)
    {
        const string sql = "INSERT INTO Students (NIM, Nama, Jurusan, Gender, Email) VALUES (@NIM, @Nama, @Jurusan, @Gender, @Email)";
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        AddParameters(command, student);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Update(Student student)
    {
        const string sql = "UPDATE Students SET NIM=@NIM, Nama=@Nama, Jurusan=@Jurusan, Gender=@Gender, Email=@Email WHERE Id=@Id";
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        AddParameters(command, student);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = student.Id;
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("DELETE FROM Students WHERE Id=@Id", connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<Student> Search(string keyword)
    {
        var students = new List<Student>();
        const string sql = @"SELECT Id, NIM, Nama, Jurusan, Gender, Email FROM Students
                             WHERE NIM LIKE @Keyword OR Nama LIKE @Keyword OR Jurusan LIKE @Keyword
                             ORDER BY Id DESC";
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Keyword", SqlDbType.VarChar, 100).Value = $"%{keyword}%";
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read()) students.Add(MapStudent(reader));
        return students;
    }

    private static void AddParameters(SqlCommand command, Student student)
    {
        command.Parameters.Add("@NIM", SqlDbType.VarChar, 20).Value = student.NIM.Trim();
        command.Parameters.Add("@Nama", SqlDbType.VarChar, 100).Value = student.Nama.Trim();
        command.Parameters.Add("@Jurusan", SqlDbType.VarChar, 100).Value = student.Jurusan;
        command.Parameters.Add("@Gender", SqlDbType.VarChar, 20).Value = student.Gender;
        command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value =
            string.IsNullOrWhiteSpace(student.Email) ? DBNull.Value : student.Email.Trim();
    }

    private static Student MapStudent(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(0),
        NIM = reader.GetString(1),
        Nama = reader.GetString(2),
        Jurusan = reader.GetString(3),
        Gender = reader.GetString(4),
        Email = reader.IsDBNull(5) ? "" : reader.GetString(5)
    };
}
