using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.Json;
using System.Windows.Navigation;

public class LoginResponse
{
    public bool Data { get; set; }
    public DateTime AccessedAt { get; set; }
}
namespace Berkati_Frontend
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private readonly HttpClient _httpClient = new();

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string apiUri = "https://localhost:7036/admin/login";
            string body = "{\"username\":\"" + unameInput.Text + "\",\"password\":\"" + passwordInput.Password.ToString() + "\"}";
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage res = await _httpClient.PostAsync(apiUri, content);
                if (res.IsSuccessStatusCode)
                {
                    string _res = await res.Content.ReadAsStringAsync();

                    var json = JsonConvert.DeserializeObject<LoginResponse>(_res);
                    if (json.Data)
                    {
                        MainWindow mainWindow = new();
                        mainWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Password");
                    }
                }
                else
                {
                    Console.WriteLine("SWW");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
