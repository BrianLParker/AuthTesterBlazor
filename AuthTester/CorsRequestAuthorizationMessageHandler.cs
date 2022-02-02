using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace AuthTester
{
    public class CorsRequestAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CorsRequestAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigationManager, IConfiguration config)
            : base(provider, navigationManager)
        {
            ConfigureHandler(new[] { config["ServerApi:BaseAddress"] });
        }
    }
}
