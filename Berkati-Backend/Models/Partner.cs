using Npgsql.Internal.TypeHandlers;
using DotNetEnv;
using Npgsql;

namespace Berkati_Backend.Models
{
    public class Partner
    {
        private Guid _id;
        private string nama;
        private string penanggungJawab;
        private string telp;
        private string email;

        public Guid Id { get => _id; set => _id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string PenanggungJawab { get => penanggungJawab; set => penanggungJawab = value; }
        public string Telp { get => telp; set => telp = value; }
        public string Email { get => email; set => email = value; }


        private readonly NpgsqlConnection connection;
        public Partner()
        {
            Env.Load("./Build/.env");

            string? _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            connection = new NpgsqlConnection(_connectionString);
        }

        public virtual List<Partner> GetAllPartner()
        {
            List<Partner> ListPartner = new();
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("SELECT * FROM \"partner\"", connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Partner partner = new()
                    {

                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        Nama = reader.GetString(reader.GetOrdinal("nama")),
                        PenanggungJawab = reader.GetString(reader.GetOrdinal("penanggung_jawab")),
                        Telp = reader.GetString(reader.GetOrdinal("telp")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                    };

                    ListPartner.Add(partner);
                }

            }
            catch (Exception ex)
            {
                if (ex is NpgsqlException)
                {
                    throw new Exception("Database-related error occured.", ex);
                }
                throw new Exception("Error occurred while retrieving partners.", ex); ;

            }
            finally
            {
                connection.Close();
            }
            return ListPartner;
        }

        public Guid AddPartner(Partner partner)
        {
            try
            {
                connection.Open();
                partner.Id = Guid.NewGuid();
                NpgsqlCommand cmd = new("INSERT INTO \"partner\" (id, nama, penanggung_jawab, telp, email) VALUES(@id, @nama, @penanggung_jawab, @telp, @email)", connection)
                {
                    Parameters =
                    {
                        new("id", partner.Id),
                        new("nama", partner.Nama),
                        new("penanggung_jawab", partner.PenanggungJawab),
                        new("telp", partner.Telp),
                        new("email", partner.Email)
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
                throw new Exception("Error occurred while creating partner.", ex);
            }
            finally
            {
                connection.Close();
            }

            return partner.Id;
        }

        public void UpdatePartner(Partner partner)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("UPDATE \"partner\" SET nama = @nama, penanggung_jawab=@penanggung_jawab, telp=@telp, email=@email WHERE id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", partner.Id),
                        new("nama", partner.Nama),
                        new("penanggung_jawab", partner.PenanggungJawab),
                        new("telp", partner.Telp),
                        new("email", partner.Email)
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
                throw new Exception("Error occurred while updating partner.", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeletePartner(Guid partnerId)
        {
            try
            {
                connection.Open();
                NpgsqlCommand cmd = new("DELETE FROM \"partner\" WHERE id = @id;", connection)
                {
                    Parameters =
                    {
                        new("id", partnerId)
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
                throw new Exception("Error occurred while deleting partner.", ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
