using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;

namespace AuthTester.Services
{
    public class ClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        private IUserService userService { get; set; }

        public ClaimsPrincipalFactory(
            IAccessTokenProviderAccessor accessor,
            IUserService userService
        )
        : base(accessor)
        {
            this.userService = userService;
        }

        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(
            RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);

            if (!user.Identity.IsAuthenticated)
            {
                return user;
            }

            // All of this commented out code is what I did as a workaround to get this to work.
            /*var accessTokenResult = await TokenProvider.RequestAccessToken();
            var accessToken = string.Empty;

            if (accessTokenResult.TryGetToken(out var token))
            {
                accessToken = token.Value;
            }

            if (accessToken.Length < 1)
            {
                return user;
            }

            var userInfo = await userService.UserInfoWorkaround(accessToken);*/

            // It seems to me like this should work.
            // However in UserService, injecting the IHttpClientFactory and then calling CreateClient causes a runtime error
            var userInfo = await userService.UserInfo();
            var identity = user.Identity as ClaimsIdentity;
            if (userInfo != null)
            {
                foreach (var role in userInfo.UserRoles)
                {
                    identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ApplicationRole.Name));
                }
            }

            return user;
        }
    }
}
