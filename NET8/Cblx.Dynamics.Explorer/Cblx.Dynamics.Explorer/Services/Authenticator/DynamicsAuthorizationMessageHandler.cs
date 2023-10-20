using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

namespace Cblx.Dynamics.Explorer.Services.Authenticator;

public class DynamicsAuthorizationMessageHandler(DynamicsConfig config, IMemoryCache memoryCache) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = await memoryCache.GetOrCreateAsync(config.Name, async (entry) =>
        {
            Console.WriteLine($"*** ACQUIRING NEW TOKEN FOR {config.Name} ***");
            var app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                .WithClientSecret(config.ClientSecret)
                .WithAuthority(config.Authority)
                .Build();
            // Estudar sobre token caches. Ainda não entendi => https://learn.microsoft.com/pt-br/entra/msal/dotnet/how-to/token-cache-serialization?tabs=aspnetcore
            var authResult = await app.AcquireTokenForClient([$"{config.ResourceUrl}/.default"]).ExecuteAsync();
            entry.AbsoluteExpiration = authResult.ExpiresOn;
            return AuthenticationHeaderValue.Parse($"Bearer {authResult.AccessToken}");
        });
        request.Headers.Authorization = authorizationHeader;
        return await base.SendAsync(request, cancellationToken);
    }
}