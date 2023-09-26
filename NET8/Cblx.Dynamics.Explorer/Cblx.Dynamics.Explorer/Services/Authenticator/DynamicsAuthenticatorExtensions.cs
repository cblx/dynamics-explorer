using System.Net.Http.Headers;

namespace Cblx.Dynamics.Explorer.Services.Authenticator;

public static class DynamicsAuthenticatorExtensions
{
    public static async Task<AuthenticationHeaderValue> GetAuthenticationHeaderValue(this IDynamicsAuthenticator authenticator, DynamicsConfig config)
    {
        string accessToken = await authenticator.GetAccessToken(config);
        return AuthenticationHeaderValue.Parse($"Bearer {accessToken}");
    }
}
