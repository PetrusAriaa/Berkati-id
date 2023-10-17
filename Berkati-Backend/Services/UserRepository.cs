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
            connection = new NpgsqlConnection(_connectionString);
        }

        public List<User> GetAllUser()
        {
            List<User> ListUser = new();
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new ("SELECT * FROM \"user\"", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
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

        public Guid AddUser(User user)
        {
            try
            {
                connection.Open();
                user.Id = Guid.NewGuid();
                NpgsqlCommand cmd = new ("INSERT INTO \"user\" (id, nama, telp) VALUES(@id, @nama, @telp)", connection)
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
            return user.Id;
        }
        public void DeleteUser(Guid userId)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new ("DELETE FROM \"user\" WHERE id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", userId)
                    }
                };
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
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
                NpgsqlCommand cmd = new ("UPDATE \"user\" SET nama = @nama, telp = @telp WHERE id = @id;", connection)
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
