using Berkati_Backend.Models;
using Npgsql;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using DotNetEnv;

namespace Berkati_Backend.Services
{
    public class AdminRepository
    {
        private readonly NpgsqlConnection connection;

        public AdminRepository()
        {
            DotNetEnv.Env.Load("./Build/.env");

            string _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            // Initialize the NpgsqlConnection in the constructor
            connection = new NpgsqlConnection(_connectionString);
        }

        public List<Admin> GetAllAdmin()
        {
            List<Admin> ListAdmin = new();
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"admin\"", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Admin admin = new()
                    {

                        Id = reader.GetString(reader.GetOrdinal("_id")),
                        Username = reader.GetString(reader.GetOrdinal("username")),
                        Password = reader.GetString(reader.GetOrdinal("password")),
                        LastLogin = reader.GetDateTime(reader.GetOrdinal("lastLogin")),
                        IsSuperUser = reader.GetBoolean(reader.GetOrdinal("isSuperUser")),
                    };

                    ListAdmin.Add(admin);
                }

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return ListAdmin;
        }

    }
}
