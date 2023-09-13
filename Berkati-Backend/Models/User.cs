namespace Berkati_Backend.Models
{
    public class User
    {
        private string _id;
        private string nama;
        private string telp;

        public string Id { get => _id; set => _id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Telp { get => telp; set => telp = value; }

        public User() { }
    }
}
