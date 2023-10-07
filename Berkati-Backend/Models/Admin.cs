using Berkati_Backend.Services;
using Npgsql.Internal.TypeHandlers;

namespace Berkati_Backend.Models
{
    public class Admin
    {
        private Guid _id;
        private string username;
        private string password;
        private DateTime lastLogin;
        private bool isSuperUser;

        public Guid Id { get => _id; set => _id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public DateTime LastLogin { get => lastLogin; set => lastLogin = value; }
        public bool IsSuperUser { get => isSuperUser; set => isSuperUser = value; }

        public Admin()
        {

        }

        public bool Login(string username, string password)
        {
            var adminRepos = new AdminRepository();
            {
                if (adminRepos.AdminLogin(username, password))
                {
                    return true;
                }
            }
            return false;
        }

        //public bool Login(string username, string password)
        //{
        //    var adminRepos = new AdminRepository();
        //    var ListAdmin = new List<Admin>();
        //    ListAdmin.AddRange(adminRepos.GetAllAdmin());
        //    foreach (Admin admin in ListAdmin)
        //    {
        //        if (admin.Username == username && admin.Password == password)
        //        {
        //            admin.LastLogin = DateTime.Now;
        //            adminRepos.UpdateAdmin(admin);
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
