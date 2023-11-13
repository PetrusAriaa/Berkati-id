using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows;
using System.Text;
using System.Net.Http.Headers;

namespace Berkati_Frontend.Handler
{
    internal class Admin
    {


        private static readonly HttpClient _httpClient = new();


        public class AdminData
        {
            public Guid? Id { get; set; }
            public string? Username { get; set; }
            public string? Password { get; set; }
            public string? IsSuperUser { get; set; }
            public DateTime? LastLogin { get; set; }
        }


        private class AdminList
        {
            public List<AdminData>? Data { get; set; }
        }


        public static async void GetAdminData(DataGrid dataGrid)
        {
            var apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/admin";
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("TOKEN"));
                HttpResponseMessage res = await _httpClient.GetAsync(apiUri);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kesalahan koneksi");
                    return;
                }
                var content = await res.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<AdminList>(content);
                dataGrid.ItemsSource = json?.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static async void AddAdmin(AdminData admin, DataGrid dataGrid)
        {
            var apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/admin";
            string body = "{\"username\":\"" + admin.Username + "\",\"password\":\"" + admin.Password + "\"}";
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage res = await _httpClient.PostAsync(apiUri, content);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal menambahkan admin");
                    return;
                }
                MessageBox.Show("Berhasil menambahkan admin");
                GetAdminData(dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static async void DeleteAdmin(AdminData admin, DataGrid dataGrid)
        {
            var apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/admin/" + admin.Id;
            try
            {
                HttpResponseMessage res = await _httpClient.DeleteAsync(apiUri);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal menghapus admin");
                    return;
                }
                MessageBox.Show("Berhasil menghapus admin");
                GetAdminData(dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
