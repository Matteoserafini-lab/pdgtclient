using Gtk;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PDGTClient
{
    class Client
    {
        private string token = "";
        private static string baseUrl = "https://dati-covid.herokuapp.com/";
        // private static string baseUrl = "http://localhost:36745/";
        public async Task home(Label display){
            using HttpClient client = new HttpClient();
                display.Text = await client.GetStringAsync(baseUrl);
        }

        public async Task<string> login(Label display, Entry n, Entry p, Label lb){  
            if(n.Text == ""){
                display.Text = "{\n\t'error': 'no username provided'\n}";
                return "412\tPrecondition Failed";
            }
            string url = $"{baseUrl}account/{n.Text}";
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("password", p.Text);
            HttpResponseMessage response = await client.GetAsync(url);
            p.Text = "";
            display.Text =  $"{JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync())}";
            if(response.IsSuccessStatusCode){
                token = response.Headers.TryGetValues("Token", out var values) ? values.FirstOrDefault() : null;
                lb.Text = "logged in";
            } else {
                lb.Text = "not logged in";
            }
            return $"\t{(int)response.StatusCode}\t{response.StatusCode}";
        }

        public async Task<string> signup(Label display, Entry n, Entry p){
            if(n.Text == ""){
                display.Text = "{\n\t'error': 'no username provided'\n}";
                return "412\tPrecondition Failed";
            }
            if(p.Text == ""){
                display.Text = "{\n\t'error': 'no password provided'\n}";
                return "412 Precondition Failed";
            }  
            string url = $"{baseUrl}account/";
            using HttpClient client = new HttpClient();
            var name = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("name", n.Text)
            });
            client.DefaultRequestHeaders.Add("password", p.Text);
            HttpResponseMessage response = await client.PostAsync(url, name);
            p.Text = "";
            display.Text = $"{JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync())}";
            return $"\t{(int)response.StatusCode}\t{response.StatusCode}";
        }
        
        public async Task<string> update(Label display, Entry n, Entry p){
            if(n.Text == ""){
                display.Text = "{\n\t'error': 'no username provided'\n}";
                return "412\tPrecondition Failed";
            }
            if(p.Text == ""){
                display.Text = "{\n\t'error': 'no password provided'\n}";
                return "412 Precondition Failed";
            }
            string url = $"{baseUrl}account/{n.Text}";
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("password", p.Text);
            client.DefaultRequestHeaders.Add("token", token);
            var action = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("action", "password change")
            });
            HttpResponseMessage response = await client.PatchAsync(url, action);
            p.Text = "";
            display.Text = $"{JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync())}";
            return $"\t{(int)response.StatusCode}\t{response.StatusCode}";
        }
        public async Task<string> logout(Label display, Entry n, Entry p, Label lb){
            if(n.Text == ""){
                display.Text = "{\n\t'error': 'no username provided'\n}";
                return "412\tPrecondition Failed";
            }
            string url = $"{baseUrl}account/{n.Text}";
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("password", p.Text);
            client.DefaultRequestHeaders.Add("token", this.token);
            var action = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("action", "logout")
            });
            HttpResponseMessage response = await client.PutAsync(url, action);
            p.Text = "";
            if(response.IsSuccessStatusCode){
                lb.Text = "not logged in";
            } else {
                lb.Text = "logged in";
            }
            display.Text = $"{JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync())}";
            return $"\t{(int)response.StatusCode}\t{response.StatusCode}";
        }
        public async Task<string> delete(Label display, Entry n, Label lb){  
            if(n.Text == ""){
                display.Text = "{\n\t'error': 'no username provided'\n}";
                return "412\tPrecondition Failed";
            }  
            string url = $"{baseUrl}account/{n.Text}";
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Token", token);
            HttpResponseMessage response = await client.DeleteAsync(url);
            display.Text = $"{JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync())}";
            if(response.IsSuccessStatusCode){
                lb.Text = "not logged in";
            } else {
                lb.Text = "logged in";
            }
            return $"\t{(int)response.StatusCode}\t{response.StatusCode}";
        }
        public async Task<string> search(Label display, Entry n, List<string> ls, Entry de){
            if(n.Text == ""){
                display.Text = "{\n\t'error': 'no username provided'\n}";
                return "412\tPrecondition Failed";
            }
            string date = (de.Text != "") ? de.Text : Date();
            if(ls.Count == 0) {
                string url2 = $"{baseUrl}italy/{n.Text}/{date}";
                using HttpClient client2 = new HttpClient();
                client2.DefaultRequestHeaders.Add("Token", token);
                display.Text = "search in progress";
                HttpResponseMessage response2 = await client2.GetAsync(url2);
                display.Text = $"{JsonConvert.DeserializeObject(await response2.Content.ReadAsStringAsync())}";
                return $"\t{(int)response2.StatusCode}\t{response2.StatusCode}";
            }
            string body = JsonConvert.SerializeObject(ls);
            string url = $"{baseUrl}regions/{n.Text}/{date}";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = new StringContent(body, Encoding.UTF8 , "application/json")
            };
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Token", token);
            display.Text = "search in progress";
            HttpResponseMessage response = await client.SendAsync(request);
            display.Text = $"{JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync())}";
            return $"\t{(int)response.StatusCode}\t{response.StatusCode}";
        }

        private string Date(){
            return ((int)DateTime.Now.Hour < 18) ?  DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") : DateTime.Today.ToString("yyyy-MM-dd");
        }
    }
}