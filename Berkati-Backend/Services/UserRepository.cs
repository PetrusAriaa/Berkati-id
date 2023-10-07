using Berkati_Backend.Models;
using Npgsql;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using DotNetEnv;

namespace Berkati_Backend.Services
{
    public class UserRepository
    {
        private readonly NpgsqlConnection connection;

        public UserRepository()
        {
            Env.Load("./Build/.env");
            string? _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            // Initialize the NpgsqlConnection in the constructor
            connection = new NpgsqlConnection(_connectionString);
        }

        public List<User> GetAllUser()
        {
            List<User> ListUser = new();
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"user\"", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new()
                    {

                        Id = reader.GetString(reader.GetOrdinal("_id")),
                        Nama = reader.GetString(reader.GetOrdinal("nama")),
                        Telp = reader.GetString(reader.GetOrdinal("telp")),
                    };

                    ListUser.Add(user);
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
            return ListUser;
        }

        public void AddUser(User user)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO \"user\" (_id, nama, telp) VALUES(@id, @nama, @telp)", connection)
                {
                    Parameters =
                    {
                        new("id", user.Id),
                        new("nama", user.Nama),
                        new("telp", user.Telp)
                    }
                };
                cmd.ExecuteNonQuery();

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        public void DeleteUser(string id)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM \"user\" WHERE _id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", id)
                    }
                };
                cmd.ExecuteNonQuery();

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE \"user\" SET nama = @nama, telp = @telp WHERE _id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", user.Id),
                        new("nama", user.Nama),
                        new("telp", user.Telp)
                    }
                };
                cmd.ExecuteNonQuery();

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
