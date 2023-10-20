using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;

namespace Cblx.Dynamics.Explorer.Services.Authenticator;

public class DynamicsAuthorizationMessageHandler(DynamicsConfig config, IMemoryCache memoryCache) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = await memoryCache.GetOrCreateAsync(config.Name, async (entry) =>
        {
            Console.WriteLine($"*** ACQUIRING NEW TOKEN FOR {config.Name} ***");
            var authenticationContext = new AuthenticationContext(config.Authority);
            var credential = new ClientCredential(config.ClientId, config.ClientSecret);
            var authentication = await authenticationContext.AcquireTokenAsync(config.ResourceUrl, credential);
            entry.AbsoluteExpiration = authentication.ExpiresOn;
            return AuthenticationHeaderValue.Parse($"Bearer {authentication.AccessToken}");
        });
        request.Headers.Authorization = authorizationHeader;
        return await base.SendAsync(request, cancellationToken);
    }
}