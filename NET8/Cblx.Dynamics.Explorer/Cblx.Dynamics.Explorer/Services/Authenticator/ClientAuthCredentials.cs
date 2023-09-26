using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Cblx.Dynamics.Explorer.Services.Authenticator;
public class ClientAuthCredentials
{
    public ClientCredential? Credentials { get; set; }
    public AuthenticationResult? Authentication { get; set; }
}
