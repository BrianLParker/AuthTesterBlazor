
using AuthTester.Entities;
using System.Net.Http.Json;

namespace AuthTester.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient http;
        private readonly IConfiguration config;

        public UserService(
            IHttpClientFactory clientFactory,
            IConfiguration config
        // Http for workaround
        // , HttpClient http
        )
        {
            // If this is removed and the injected HttpClient is used instead, along with the UserInfoWorkaround, everything works fine
            http = clientFactory.CreateClient("BlazorClient.ServerApi");
            // Http for workaround
            // this.http = http;
            this.config = config;
        }

        public virtual async Task<User> UserInfo()
        {
            var uri = $"{config["ServerApi:BaseAddress"]}users/userinfo";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            try
            {
                var response = await http.SendAsync(request);

                return await response.Content.ReadFromJsonAsync<User>();
            }
            catch (Exception exc)
            {
                // TODO Log exc
            }
            return null;
        }

        // This is an example of my workaround where I passed in the token from the ClaimsPrincipalFactory
        public virtual async Task<User> UserInfoWorkaround(string token)
        {
            var uri = $"{config["ServerApi:BaseAddress"]}users/userinfo";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            request.Headers.Add("Authorization", $"Bearer {token}");

            try
            {
                var response = await http.SendAsync(request);

                return await response.Content.ReadFromJsonAsync<User>();
            }
            catch (Exception exc)
            {
                // TODO Log exc
            }
            return null;
        }
    }
}
