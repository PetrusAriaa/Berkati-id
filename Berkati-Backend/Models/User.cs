namespace Berkati_Backend.Models
{
    public class User
    {
        private string _id;
        private string nama;
        private string telp;
        private string? _reqID;
        private List<UserRequests>? requests;

        public string Id { get => _id; set => _id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Telp { get => telp; set => telp = value; }
        public string? ReqID { get => _reqID; set => _reqID = value; }
        public List<UserRequests>? Requests { get => requests; set => requests = value; }

        public User() { }
    }
}
