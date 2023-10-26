using Npgsql.Internal.TypeHandlers;
using DotNetEnv;
using Npgsql;
using Sprache;

namespace Berkati_Backend.Models
{
    public class Requests
    {
        private Guid _id;
        private Guid _userId;
        private DateTime tanggal;
        private string jalan;
        private string rt;
        private string rw;
        private string kel;
        private string kec;
        private string kab_kota;
        private string provinsi;
        private string kodepos;
        private int est_jumlah;
        private string status;

        public Guid Id { get => _id; set => _id = value; }
        public Guid UserId { get => _userId; set => _userId = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public string Jalan { get => jalan; set => jalan = value; }
        public string Rt { get => rt; set => rt = value; }
        public string Rw { get => rw; set => rw = value; }
        public string Kel { get => kel; set => kel = value; }
        public string Kec { get => kec; set => kec = value; }
        public string Kab_kota { get => kab_kota; set => kab_kota = value; }
        public string Provinsi { get => provinsi; set => provinsi = value; }
        public string Kodepos { get => kodepos; set => kodepos = value; }
        public int Est_jumlah { get => est_jumlah; set => est_jumlah = value; }
        public string Status { get => status; set => status = value; }


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
                        Jalan = reader.GetString(reader.GetOrdinal("jalan")),
                        Rt = reader.GetString(reader.GetOrdinal("rt")),
                        Rw = reader.GetString(reader.GetOrdinal("rw")),
                        Kel = reader.GetString(reader.GetOrdinal("kel")),
                        Kec = reader.GetString(reader.GetOrdinal("kec")),
                        Kab_kota = reader.GetString(reader.GetOrdinal("kab_kota")),
                        Provinsi = reader.GetString(reader.GetOrdinal("provinsi")),
                        Kodepos = reader.GetString(reader.GetOrdinal("kodepos")),
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
                    Console.WriteLine(ex.Message);
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
                            Jalan = reader.GetString(reader.GetOrdinal("jalan")),
                            Rt = reader.GetString(reader.GetOrdinal("rt")),
                            Rw = reader.GetString(reader.GetOrdinal("rw")),
                            Kel = reader.GetString(reader.GetOrdinal("kel")),
                            Kec = reader.GetString(reader.GetOrdinal("kec")),
                            Kab_kota = reader.GetString(reader.GetOrdinal("kab_kota")),
                            Provinsi = reader.GetString(reader.GetOrdinal("provinsi")),
                            Kodepos = reader.GetString(reader.GetOrdinal("kodepos")),
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
                NpgsqlCommand cmd = new("INSERT INTO \"requests\" (id, tanggal, jalan, rt, rw, kel, kec, kab_kota, provinsi, kodepos, est_jumlah, status, user_id) VALUES(@id, @tanggal, @jalan, @rt, @rw, @kel, @kec, @kab_kota, @provinsi, @kodepos, @est_jumlah, @status, @user_id)", connection)
                {
                    Parameters =
                    {
                        new("id", requests.Id),
                        new("tanggal", requests.Tanggal),
                        new("jalan", requests.Jalan),
                        new("rt", requests.Rt),
                        new("rw", requests.Rw),
                        new("kel", requests.Kel),
                        new("kec", requests.Kec),
                        new("kab_kota", requests.Kab_kota),
                        new("provinsi", requests.Provinsi),
                        new("kodepos", requests.Kodepos),
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
                    throw new Exception("Database-related error occurred.", ex);
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
                NpgsqlCommand cmd = new("UPDATE \"requests\" SET tanggal=@tanggal, jalan=@jalan, rt=@rt, rw=@rw, kel=@kel, kec=@kec, kab_kota=@kab_kota, provinsi=@provinsi, kodepos=@kodepos, est_jumlah=@est_jumlah, status=@status, user_id=@user_id WHERE id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", requests.Id),
                        new("tanggal", requests.Tanggal),
                        new("jalan", requests.Jalan),
                        new("rt", requests.Rt),
                        new("rw", requests.Rw),
                        new("kel", requests.Kel),
                        new("kec", requests.Kec),
                        new("kab_kota", requests.Kab_kota),
                        new("provinsi", requests.Provinsi),
                        new("kodepos", requests.Kodepos),
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
