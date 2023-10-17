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
            Env.Load("./Build/.env");

            string? _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            connection = new NpgsqlConnection(_connectionString);
        }

        public virtual List<Admin> GetAllAdmin()
        {
            List<Admin> ListAdmin = new();
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new ("SELECT * FROM \"admin\"", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Admin admin = new ()
                    {

                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        Username = reader.GetString(reader.GetOrdinal("username")),
                        Password = reader.GetString(reader.GetOrdinal("password")),
                        LastLogin = reader.GetDateTime(reader.GetOrdinal("last_login")),
                        IsSuperUser = reader.GetBoolean(reader.GetOrdinal("is_super_user")),
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

        public Guid AddAdmin(Admin admin)
        {
            try
            {
                connection.Open();
                admin.Id = Guid.NewGuid();
                NpgsqlCommand cmd = new("INSERT INTO \"admin\" (id, username, password, last_login, is_super_user) VALUES(@id, @username, @password, @last_login, @is_super_user)", connection)
                {
                    Parameters =
                    {
                        new("id", admin.Id),
                        new("username", admin.Username),
                        new("password", admin.Password),
                        new("last_login", DateTime.Now),
                        new("is_super_user", admin.IsSuperUser)
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

            return admin.Id;
        }

        public void UpdateAdmin(Admin admin)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new ("UPDATE \"admin\" SET username=@username, password=@password WHERE id = @id;", connection)
                {
                    Parameters =
                    {
                        new("username", admin.Username),
                        new("password", admin.Password),
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

        public bool AdminLogin(string username, string password)
        {
            List<Admin> ListAdmin = new();
            ListAdmin.AddRange(GetAllAdmin());
            foreach (Admin admin in ListAdmin)
            {
                if (admin.Username == username && admin.Password == password)
                {
                    admin.LastLogin = DateTime.Now;
                    UpdateAdmin(admin);
                    return true;
                }
            }
            return false;

        }

    }
}
