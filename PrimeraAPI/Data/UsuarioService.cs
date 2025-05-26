using System.Security.Cryptography;
using System.Text;
using PrimeraAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace PrimeraAPI.Data
{
    public class UsuarioService
    {
        private readonly string _connectionString;
        public UsuarioService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("TiendaDB")!;
        }

        // Función para hashear la contraseña
        private string Hash(string pass)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
            return Convert.ToBase64String(bytes);

        }

        public async Task<bool> RegistroAsync(string username, string password)
        {
            var hash = Hash(password);
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var cmd = new SqlCommand(
                "INSERT INTO Usuarios (NombreUsuario, PasswordHash) VALUES (@user, @pass)", connection
            );
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@pass", hash);

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return true;

            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                return false;
            }
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string pass)
        {
            var hash = Hash(pass);
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new SqlCommand(
                "SELECT COUNT(1) FROM Usuarios WHERE NombreUsuario=@u AND PasswordHash=@h", connection
            );
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@h", hash);
            var count = (int)await cmd.ExecuteScalarAsync()!;
            return count == 1;
        }

    }
}