using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Net.Http.Headers;

namespace Berkati_Frontend.Handler
{
    internal class Partner
    {


        private static readonly HttpClient _httpClient = new();


        public class PartnerData
        {
            public Guid? Id { get; set; }
            public string? Nama { get; set; }
            public string? PenanggungJawab { get; set; }
            public string? Telp { get; set; }
            public string? Email { get; set; }
        }


        private class PartnerList
        {
            public List<PartnerData>? Data { get; set; }
        }


        public static async void GetPartnerData(DataGrid dataGrid)
        {
            var apiUrl = Environment.GetEnvironmentVariable("LISTEN") + "/partner";
            try
            {
                HttpResponseMessage res = await _httpClient.GetAsync(apiUrl);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kesalahan koneksi");
                    return;
                }
                var _res = await res.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<PartnerList>(_res);
                dataGrid.ItemsSource = json?.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public static async void AddPartner(PartnerData partner, DataGrid datagrid)
        {
            var apiUrl = Environment.GetEnvironmentVariable("LISTEN") + "/partner";
            string body = "{\"nama\":\"" + partner.Nama + "\",\"penanggungJawab\":\"" + partner.PenanggungJawab + "\",\"telp\":\"" + partner.Telp + "\",\"email\":\"" + partner.Email + "\"}";
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage res = await _httpClient.PostAsync(apiUrl, content);
                if (!res.IsSuccessStatusCode)
                {
                    var _res = await res.Content.ReadAsStringAsync();
                    MessageBox.Show("Gagal menambahkan partner", _res);
                    return;
                }
                MessageBox.Show("Berhasil menambahkan partner");
                GetPartnerData(datagrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static async void EditPartner(PartnerData partner, DataGrid dataGrid)
        {
            var apiUrl = Environment.GetEnvironmentVariable("LISTEN") + "/partner/" + partner.Id;
            string body = "{\"nama\":\"" + partner.Nama + "\",\"penanggungJawab\":\"" + partner.PenanggungJawab + "\",\"telp\":\"" + partner.Telp + "\",\"email\":\"" + partner.Email + "\"}";
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage res = await _httpClient.PutAsync(apiUrl, content);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal mengedit partner");
                    return;
                }
                MessageBox.Show("Berhasil mengedit partner");
                GetPartnerData(dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static async void DeletePartner(PartnerData partner, DataGrid dataGrid)
        {
            var apiUri = Environment.GetEnvironmentVariable("LISTEN") + "/partner/" + partner.Id;
            try
            {
                HttpResponseMessage res = await _httpClient.DeleteAsync(apiUri);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal menghapus partner");
                    return;     
                }
                MessageBox.Show("Berhasil menghapus partner");
                GetPartnerData(dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
