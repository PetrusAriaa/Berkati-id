using Npgsql.Internal.TypeHandlers;
using DotNetEnv;
using Npgsql;

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
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving requests.", ex);
            }
            finally
            {
                connection.Close();
            }
            return RequestsList;
        }
    }
}
