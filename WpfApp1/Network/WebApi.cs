using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Share;

namespace WpfApp1.Network
{
    internal class WebApi
    {
        private const string _urlServer = "http://localhost:5000/";
        private RestClient _client;
        private RestResponseCookie _cookies;

        public WebApi()
        {
            _client = new RestClient(_urlServer);
        }

        public async Task<IRestResponse> Authorize(string username, string password)
        {
            var request = new RestRequest(_urlServer + "account/login", Method.POST, DataFormat.Json).AddJsonBody(new LoginModel { Username = username, Password = password});
            request.AddHeader("content-type", "application/json");

            var result = await _client.ExecuteAsync(request);

            return result;
        }

        public async Task<IRestResponse> GetInfo()
        {
            var request = new RestRequest(_urlServer + "account/getinfo", Method.GET, DataFormat.Json);
            request.AddHeader("content-type", "application/json");
            request.AddCookie(_cookies.Name, _cookies.Value);

            var result = await _client.ExecuteAsync(request);

            return result;
        }

        public void SetCookie(RestResponseCookie cookies)
        {
            _cookies = cookies;
        }
    }
}
