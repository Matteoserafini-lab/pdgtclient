using Gtk;
using System.Net.Http;
using System.Threading.Tasks;


namespace PDGTClient
{
    class Client
    {
        
        private static string baseUrl = "https://sheltered-thicket-14507.herokuapp.com/";
        // private static string baseUrl = "http://localhost:35101/";
        public async Task home(Label display){
            using HttpClient client = new HttpClient();
                display.Text = await client.GetStringAsync(baseUrl);
        }
    }
}