namespace Cblx.Dynamics.Explorer.Services.Authenticator;

public interface IDynamicsAuthenticator
{
    Task<string> GetAccessToken(DynamicsConfig config);
}
