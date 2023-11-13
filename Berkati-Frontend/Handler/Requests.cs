using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
            public Guid? UserId { get; set; }
            public string? Nama { get; set;}
            public Guid? RequestId { get; set; }
            public string? Telp { get; set; }

        }

        public class ResponseData
        {
            public List<ResRequests> Data { get; set; }
        }

        public static async void GetRequests(StackPanel stackPanel)
        {
            var apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/user/requests";
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
        }

        public static async void CreateRequest(UserData userData)
        {
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
                string data = await res.Content.ReadAsStringAsync();
                MessageBox.Show(data);

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
        }
    }
}
