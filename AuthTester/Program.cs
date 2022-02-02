using AuthTester;
using AuthTester.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
    // Replace the Okta placeholders with your Okta values in the appsettings.json file.
    options.ProviderOptions.Authority = builder.Configuration.GetValue<string>("Okta:Authority");
    options.ProviderOptions.ClientId = builder.Configuration.GetValue<string>("Okta:ClientId");
    options.ProviderOptions.ResponseType = "code";
}).AddAccountClaimsPrincipalFactory<ClaimsPrincipalFactory>();

builder.Services.AddScoped<CorsRequestAuthorizationMessageHandler>();
builder.Services
    .AddHttpClient("BlazorClient.ServerApi", client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServerApi:BaseAddress")))
    .AddHttpMessageHandler<CorsRequestAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorClient.ServerApi"));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IUserService, UserService>();

await builder.Build().RunAsync();
