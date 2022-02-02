This is a stripped down project to show the error I'm having using a ClaimsPrincipalFactory with an injected service that injects IHttpClientFactory.

The ClaimsPrincipalFactory needs to call UserService.UserInfo to get the authenticated users info along with roles. Ideally UserService should use an httpClient created by the factory so that the token is attached to the outgoing call automatically.
However doing it that way causes a run time error at runtime.