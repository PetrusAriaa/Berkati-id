using Berkati_Backend.Models;
using DotNetEnv;
using Npgsql;

namespace Berkati_Backend.Services
{
    public class RequestsRepository
    {
        private readonly NpgsqlConnection connection;

        public RequestsRepository()
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
                NpgsqlCommand cmd = new("SELECT * FROM \"user\", requests WHERE \"user\".id = requests.user_id ORDER BY requests.tanggal DESC;", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Requests request = new()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        Tanggal = reader.GetDateTime(reader.GetOrdinal("tanggal")),
                        Jalan = reader.GetString(reader.GetOrdinal("telp")),
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
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return RequestsList;
        }
    }
}
