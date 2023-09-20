namespace Berkati_Backend.Models
{
    public class Partner
    {
        private string _id;
        private string nama;
        private string penanggungJawab;
        private string telp;
        private string email;

        public string Id { get => _id; set => _id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string PenanggungJawab { get => penanggungJawab; set => penanggungJawab = value; }
        public string Telp { get => telp; set => telp = value; }
        public string Email { get => email; set => email = value; }

        public Partner()
        {

        }
    }
}
