using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Share;

namespace WpfApp1.Network
{
    internal class WebApi
    {
        private const string _urlServer = "http://localhost:5000/";
        private RestClient _client;

        public WebApi()
        {
            _client = new RestClient(_urlServer);
        }

        public async Task<IRestResponse> Authorize(string username, string password)
        {
            var request = new RestRequest(_urlServer + "account/login", Method.POST, DataFormat.Json)
                .AddJsonBody(new LoginModel { Username = username, Password = password});
            request.AddHeader("content-type", "application/json");

            var result = await _client.ExecuteAsync(request);

            if (result is not null && result.Cookies.Count > 0)
            {
                SetAuthenticator(result.Cookies.First());
            }

            return result;
        }

        public async Task<IRestResponse> GetInfo()
        {
            var request = new RestRequest(_urlServer + "account/getinfo", Method.GET, DataFormat.Json);
            request.AddHeader("content-type", "application/json");

            var result = await _client.ExecuteAsync(request);
            return result;
        }

        private void SetAuthenticator(RestResponseCookie cookie)
        {
            ApiAuthenticator.AddAuthenticator(_client, cookie);
        }
    }
}
