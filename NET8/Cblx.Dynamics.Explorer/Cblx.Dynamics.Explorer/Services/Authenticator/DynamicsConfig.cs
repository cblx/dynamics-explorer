namespace Cblx.Dynamics.Explorer.Services.Authenticator;

public record DynamicsConfig
{
    public string Group { get; set; } = "Group";
    public string Name { get; set; } = "default";
    public string Key => $"_cblx.dynamics_{Group}_{Name}";
    public string Authority { get; set; } = "";
    public string ResourceUrl { get; set; } = "";
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
}