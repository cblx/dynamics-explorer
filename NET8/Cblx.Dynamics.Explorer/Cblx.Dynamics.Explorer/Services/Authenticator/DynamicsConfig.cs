namespace Cblx.Dynamics.Explorer.Services.Authenticator;

public record DynamicsConfig
{
    public string Name { get; set; } = "default";
    public string Authority { get; set; } = "";
    public string ResourceUrl { get; set; } = "";
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
}