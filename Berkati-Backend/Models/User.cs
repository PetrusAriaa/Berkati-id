using Npgsql.Internal.TypeHandlers;
using DotNetEnv;
using Npgsql;
using Sprache;

namespace Berkati_Backend.Models
{
    public class User
    {
        private Guid _id;
        private string nama;
        private string telp;
        private List<Requests>? requests;

        public Guid Id { get => _id; set => _id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Telp { get => telp; set => telp = value; }
        public List<Requests>? Requests { get => requests; set => requests = value; }

        private readonly NpgsqlConnection connection;

        public User() 
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
                NpgsqlCommand cmd = new("SELECT * FROM \"user\"", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        Nama = reader.GetString(reader.GetOrdinal("nama")),
                        Telp = reader.GetString(reader.GetOrdinal("telp")),
                    };
                    Requests _request = new();
                    user.Requests = _request.GetRequestByUserId(user.Id);
                    ListUser.Add(user);
                }
            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred.", ex);
                }
                throw new Exception("Error occurred while retrieving users.", ex);
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
                Guid? userId = CheckUser(user.Nama, user.Telp);

                if (!userId.HasValue)
                {
                    connection.Open();
                    user.Id = Guid.NewGuid();
                    NpgsqlCommand cmd = new("INSERT INTO \"user\" (id, nama, telp) VALUES(@id, @nama, @telp)", connection)
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
                else
                {
                    user.Id = userId.Value;
                }

                Requests _request = new();
                foreach (var request in user.Requests)
                {

                    request.UserId = user.Id;
                    Guid ids = _request.AddRequest(request);
                }

             }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred while creating user.", ex);
                }
                throw new Exception("Error occurred while creating user.", ex);
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
                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM \"requests\" WHERE user_id = @user_id; DELETE FROM \"user\" WHERE id = @id;", connection))
                {
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred.", ex);
                }
                throw new Exception("Error occurred while deleting user.", ex);
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
                NpgsqlCommand cmd = new("UPDATE \"user\" SET nama = @nama, telp = @telp WHERE id = @id;", connection)
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
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred.", ex);
                }
                throw new Exception("Error occurred while updating user.", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public Guid? CheckUser(string nama, string telp)
        {
            Guid? result = null;
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("SELECT id FROM \"user\" WHERE nama=@nama AND telp=@telp;", connection);
                cmd.Parameters.AddWithValue("nama", nama);
                cmd.Parameters.AddWithValue("telp", telp);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = reader.GetGuid(reader.GetOrdinal("id"));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred while checking user.", ex);
                }
                throw new Exception("Error occurred while checking user.", ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
