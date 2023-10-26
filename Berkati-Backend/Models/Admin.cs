using Npgsql.Internal.TypeHandlers;
using DotNetEnv;
using Npgsql;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Identity;
using System.Reflection.PortableExecutable;

namespace Berkati_Backend.Models
{
    public class Admin
    {
        private Guid _id;
        private string username;
        private string password;
        private DateTime lastLogin;
        private bool isSuperUser;

        public Guid Id { get => _id; set => _id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public DateTime LastLogin { get => lastLogin; set => lastLogin = value; }
        public bool IsSuperUser { get => isSuperUser; set => isSuperUser = value; }

        private readonly NpgsqlConnection connection;
        
        public Admin()
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
                NpgsqlCommand cmd = new("SELECT * FROM \"admin\"", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Admin admin = new()
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
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred.", ex);
                }
                throw new Exception("Error occurred while retrieving admins.", ex); ;
                
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
                admin.Password = EncryptPassword(admin.Password);
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
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred.", ex);
                }
                throw new Exception("Error occurred while creating admin.", ex);
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
                NpgsqlCommand cmd = new("UPDATE \"admin\" SET username = @username, password=@password WHERE id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", admin.Id),
                        new("username", admin.Username),
                        new("password", admin.Password),
                    }
                };
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred.", ex);
                }
                throw new Exception("Error occurred while updating admin.", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteAdmin(Guid adminId)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("DELETE FROM \"admin\" WHERE id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", adminId)
                    }
                };
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred.", ex);
                }
                throw new Exception("Error occurred while deleting admin.", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public string EncryptPassword(string password)
        {
            string encryptedPass = BC.EnhancedHashPassword(password, 13);
            return encryptedPass;
        }
        
        public bool Login(string username, string password)
        {
            Admin admin = new();
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("SELECT * FROM \"admin\" WHERE username=@username", connection);
                cmd.Parameters.AddWithValue("username", username);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        admin.Id = reader.GetGuid(reader.GetOrdinal("id"));
                        admin.Username = reader.GetString(reader.GetOrdinal("username"));
                        admin.Password = reader.GetString(reader.GetOrdinal("password"));
                        admin.LastLogin = reader.GetDateTime(reader.GetOrdinal("last_login"));
                        admin.IsSuperUser = reader.GetBoolean(reader.GetOrdinal("is_super_user"));
                    }
                }

                if (BC.EnhancedVerify(password, admin.Password))
                {
                    admin.LastLogin = DateTime.Now;
                    NpgsqlCommand command = new("UPDATE \"admin\" SET last_login = @last_login WHERE id = @id;", connection)
                    {
                        Parameters =
                            {
                                new("last_login", admin.LastLogin),
                                new("id", admin.Id),
                            }
                    };
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred.", ex);
                }
                throw new Exception("Error occurred while login.", ex);
            }

        }

    }
}
