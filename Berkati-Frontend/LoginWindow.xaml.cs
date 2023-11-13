using System;
using System.Text;
using System.Windows;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;

public class LoginResponse
{
    public String token { get; set; }
}
namespace Berkati_Frontend
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Environment.SetEnvironmentVariable("ENV", "development");
            //Environment.SetEnvironmentVariable("ENV", "production");
            if (Environment.GetEnvironmentVariable("ENV") == "development")
            {
                Environment.SetEnvironmentVariable("LISTEN", "https://localhost:7036");
            }
            else
            {
                Environment.SetEnvironmentVariable("LISTEN", "http://18.139.1.64");
            }
        }

        private readonly HttpClient _httpClient = new();

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/admin/login";
            if (string.IsNullOrEmpty(unameInput.Text) || string.IsNullOrEmpty(passwordInput.Password.ToString()))
            {
                MessageBox.Show("Please insert username and password");
                return;
            }
            loginText.Text = null;
            loginIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.ChartDonut;
            loginIcon.Spin = true;
            string body = "{\"username\":\"" + unameInput.Text + "\",\"password\":\"" + passwordInput.Password.ToString() + "\"}";
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage res = await _httpClient.PostAsync(apiUri, content);
                if (res.IsSuccessStatusCode)
                {
                    string _res = await res.Content.ReadAsStringAsync();

                    var json = JsonConvert.DeserializeObject<LoginResponse>(_res);
                    if (!string.IsNullOrEmpty(json.token))
                    {
                        Environment.SetEnvironmentVariable("TOKEN", json.token);
                        MainWindow mainWindow = new();
                        mainWindow.Show();
                        Close();
                    }
                    else
                    {
                        loginText.Text = "Login";
                        loginIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.LoginVariant;
                        loginIcon.Spin = false;
                        MessageBox.Show("Wrong Password");
                    }
                }
                else
                {
                    MessageBox.Show("Server Timeout");
                    Console.WriteLine("SWW");
                }
            }
            catch (Exception ex)
            {
                Thread.Sleep(3000);
                loginText.Text = "Login";
                loginIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.LoginVariant;
                loginIcon.Spin = false;
                MessageBox.Show("Server Timeout: "+ ex.Message);
            }
            
        }
    }
}
