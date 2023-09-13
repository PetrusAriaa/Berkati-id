namespace Berkati_Backend.Models
{
    public class Admin
    {
        private string _id;
        private string username;
        private string password;
        private DateTime lastLogin;
        private bool isSuperUser;

        public string Id { get => _id; set => _id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get=> password; set => password = value; }
        public DateTime LastLogin { get => lastLogin; set => lastLogin = value; }
        public bool IsSuperUser { get => isSuperUser; set => isSuperUser = value; }

        public Admin()
        {
        
        }
    }
}
