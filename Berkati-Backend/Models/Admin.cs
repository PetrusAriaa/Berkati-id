using Berkati_Backend.Services;

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
        public string Password { get => password; set => password = value; }
        public DateTime LastLogin { get => lastLogin; set => lastLogin = value; }
        public bool IsSuperUser { get => isSuperUser; set => isSuperUser = value; }

        public Admin()
        {

        }

        private readonly AdminRepository adminRepos;
        private readonly List<Admin> ListAdmin = new();

        public bool Login(string username, string password)
        {
            ListAdmin.AddRange(adminRepos.GetAllAdmin());
            foreach (Admin admin in ListAdmin)
            {
                if (admin.Username == username && admin.Password == password)
                {
                    LastLogin = DateTime.Now;
                    return true;
                }
            }
            return false;
        }

    }
}
