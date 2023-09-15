using Berkati_Backend.Models;
using Npgsql;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using Berkati_Backend.Build;

namespace Berkati_Backend.Services
{
    public class BerkatiRepository
    {
        private readonly NpgsqlConnection connection;

        public BerkatiRepository()
        {
            // Connection string (HARUS DI HIDE)!!!!!!!!
            var conn = new ConnectionString();
            string _connectionString = conn.conn();

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
    }



}
