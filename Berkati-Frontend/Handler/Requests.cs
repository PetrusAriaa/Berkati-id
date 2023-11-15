using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Berkati_Frontend.Themes;
using Newtonsoft.Json;

namespace Berkati_Frontend.Handler
{
    public class Requests
    {
        private static readonly HttpClient _httpClient = new();

        public class UserData
        {
            public string? Nama { get; set; }
            public string? Telp { get; set; }
            public List<RequestsData>? Requests { get; set; }
        }
        public class RequestsData
        {
            public string? Alamat { get; set; }
            public string? Waktu { get; set; }
            public DateTime? Tanggal { get; set; }
            public int? Est_jumlah { get; set; }
            public string? Status { get; set; }
        }

        public class ResRequests:RequestsData
        {
            public Guid UserId { get; set; }
            public string? Nama { get; set;}
            public Guid RequestId { get; set; }
            public string? Telp { get; set; }

        }

        public class ResponseData
        {
            public List<ResRequests>? Data { get; set; }
        }



        public static async void GetRequests(StackPanel stackPanel, Button btnRefresh)
        {
            var apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/user/requests";
            btnRefresh.IsEnabled = false;
            try
            {
                HttpResponseMessage res = await _httpClient.GetAsync(apiUri);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kesalahan koneksi");
                    return;
                }
                var _res = await res.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<ResponseData>(_res);
                
                foreach (var item in json.Data)
                {
                    RequestCard card = new(item);
                    stackPanel.Children.Add(card);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnRefresh.IsEnabled = true;
            }
        }

        public static async void CreateRequest(UserData userData, Button btn)
        {
            btn.IsEnabled = false;
            var apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/user";
            UserData u = new()
            {
                Nama = userData.Nama,
                Telp = userData.Telp,
                Requests = userData.Requests,
            };
            string body = JsonConvert.SerializeObject(u);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage res = await _httpClient.PostAsync(apiUri, content);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal menambahkan request");
                    return;
                }
                MessageBox.Show("Berhasil menambahkan request");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { btn.IsEnabled = true; }
        }

        public async static void DeleteRequest(ResRequests data, Button btn)
        {
            btn.IsEnabled = false;
            btn.Content = "Processing...";
            string apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/requests/" + data.RequestId;
            try
            {
                HttpResponseMessage res = await _httpClient.DeleteAsync(apiUri);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal menghapus data");
                    return;
                }
                MessageBox.Show("Berhasil menghapus data. Tekan tombol Refresh untuk perbarui data");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btn.IsEnabled = true;
            }
            finally
            {
                btn.Content = "Delete";
            }
        }

        public static async Task<bool> UpdateRequest(RequestsData request, Guid requestId, Button btn)
        {
            btn.IsEnabled = false;
            btn.Content = "Processing...";
            string apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/requests/" + requestId.ToString();
            string body = JsonConvert.SerializeObject(request);
            var content = new StringContent(body, Encoding.UTF8, "application/json");         
            try
            {
                HttpResponseMessage res = await _httpClient.PutAsync(apiUri, content);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal mengubah data");
                    btn.IsEnabled = true;
                    btn.Content = "Edit";
                    return false;
                }
                MessageBox.Show("Berhasil mengubah data");
                btn.IsEnabled = true;
                btn.Content = "Edit";
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                btn.IsEnabled = true;
                btn.Content = "Edit";
                return false;
            }
           
        }
        
        public static async void FinishRequest(Guid requestId, Button btnEdit, Button btnDelete, Button btnDone, TextBlock status)
        {
            btnDone.IsEnabled = false;
            string apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/requests/finish/" + requestId;
            var data = new
            {
                id = requestId
            };
            var serialized = JsonConvert.SerializeObject(data);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage res = await _httpClient.PutAsync(apiUri, content);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kesalahan Koneksi");
                    return;
                }
                status.Text = "DONE";
                status.Foreground = new SolidColorBrush(ColorTheme.SUCCESS_2);
                btnEdit.Visibility = Visibility.Collapsed;
                btnDone.Visibility = Visibility.Collapsed;
                MessageBox.Show("Layanan Selesai!");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnDone.IsEnabled = true;
                return;
            }
        }
    }
}
