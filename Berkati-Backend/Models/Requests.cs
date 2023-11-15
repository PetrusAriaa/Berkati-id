using DotNetEnv;
using Npgsql;

namespace Berkati_Backend.Models
{
    public class Requests
    {
        private Guid _id;
        private Guid _userId;
        private DateTime tanggal;
        private string alamat;
        private string waktu;
        private int est_jumlah;
        private string? status;

        public Guid Id { get => _id; set => _id = value; }
        public Guid UserId { get => _userId; set => _userId = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public string Alamat { get => alamat; set => alamat = value; }
        public string Waktu { get => waktu; set => waktu = value; }
        public int Est_jumlah { get => est_jumlah; set => est_jumlah = value; }
        public string? Status { get => status; set => status = value; }


        private readonly NpgsqlConnection connection;
        public Requests()
        {
            Env.Load("./Build/.env");
            string? _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            connection = new NpgsqlConnection(_connectionString);
        }

        public List<Requests> GetRequests()
        {
            List<Requests> RequestsList = new();
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("SELECT * FROM \"requests\" ORDER BY \"requests\".tanggal DESC;", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Requests request = new()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        Tanggal = reader.GetDateTime(reader.GetOrdinal("tanggal")),
                        Alamat = reader.GetString(reader.GetOrdinal("alamat")),
                        Waktu = reader.GetString(reader.GetOrdinal("waktu")),
                        Est_jumlah = reader.GetInt32(reader.GetOrdinal("est_jumlah")),
                        Status = reader.GetString(reader.GetOrdinal("status")),
                        UserId = reader.GetGuid(reader.GetOrdinal("user_id")),
                    };
                    RequestsList.Add(request);
                }

            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred.", ex);
                }
                throw new Exception("Error occurred while retrieving requests.", ex);
            }
            finally
            {
                connection.Close();
            }
            return RequestsList;
        }

        public List<Requests> GetRequestByUserId(Guid userId)
        {
            List<Requests> RequestsList = new();
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("SELECT * FROM \"requests\" WHERE user_id = @user_id;", connection);
                cmd.Parameters.AddWithValue("user_id", userId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Requests request = new()
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("id")),
                            Tanggal = reader.GetDateTime(reader.GetOrdinal("tanggal")),
                            Alamat = reader.GetString(reader.GetOrdinal("alamat")),
                            Waktu = reader.GetString(reader.GetOrdinal("waktu")),
                            Est_jumlah = reader.GetInt32(reader.GetOrdinal("est_jumlah")),
                            Status = reader.GetString(reader.GetOrdinal("status")),
                            UserId = reader.GetGuid(reader.GetOrdinal("user_id")),
                        };
                        RequestsList.Add(request);
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred.", ex);
                }
                throw new Exception("Error occurred while retrieving requests.", ex);
            }
            finally
            {
                connection.Close();
            }
            return RequestsList;
        }

        public Guid AddRequest(Requests requests)
        {
            try
            {
                connection.Open();
                requests.Id = Guid.NewGuid();
                requests.Status = "REQUESTED";
                NpgsqlCommand cmd = new("INSERT INTO \"requests\" (id, tanggal, alamat, waktu, est_jumlah, status, user_id) VALUES(@id, @tanggal, @alamat, @waktu, @est_jumlah, @status, @user_id)", connection)
                {
                    Parameters =
                    {
                        new("id", requests.Id),
                        new("tanggal", requests.Tanggal),
                        new("alamat", requests.Alamat),
                        new("waktu", requests.Waktu),
                        new("est_jumlah", requests.Est_jumlah),
                        new("status", requests.Status),
                        new("user_id", requests.UserId),
                    }
                };
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occurred while creating request.", ex);
                }
                throw new Exception("Error occurred while creating request.", ex);
            }
            finally
            {
                connection.Close();
            }

            return requests.Id;
        }

        public void UpdateRequest(Requests requests)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("UPDATE \"requests\" SET tanggal=@tanggal, alamat=@alamat, waktu=@waktu, est_jumlah=@est_jumlah WHERE id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", requests.Id),
                        new("tanggal", requests.Tanggal),
                        new("alamat", requests.Alamat),
                        new("waktu", requests.Waktu),
                        new("est_jumlah", requests.Est_jumlah),
                        new("status", requests.Status),
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
                throw new Exception("Error occurred while updating request.", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void FinishRequest(Guid requestId)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("UPDATE \"requests\" SET status=@status WHERE id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", requestId),
                        new("status", "DONE")
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
                throw new Exception("Error occurred while updating request.", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteRequest(Guid requestId)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("DELETE FROM \"requests\" WHERE id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", requestId)
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
                throw new Exception("Error occurred while deleting request.", ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
