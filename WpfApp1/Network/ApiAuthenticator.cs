using RestSharp;
using RestSharp.Authenticators;

namespace WpfApp1.Network
{
    class ApiAuthenticator : IAuthenticator
    {
#pragma warning disable CS0618
        private readonly HttpCookie _authCookie;

        private ApiAuthenticator(HttpCookie authCookie)
        {
            _authCookie = authCookie;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddCookie(_authCookie.Name, _authCookie.Value);
        }

        public static void AddAuthenticator(IRestClient client, HttpCookie authCookie)
        {
            client.Authenticator = new ApiAuthenticator(authCookie);
        }

        public static void AddAuthenticator(IRestClient client, RestResponseCookie authCookie)
        {
            client.Authenticator = new ApiAuthenticator(authCookie.HttpCookie);
        }
#pragma warning restore CS0618
    }
}
