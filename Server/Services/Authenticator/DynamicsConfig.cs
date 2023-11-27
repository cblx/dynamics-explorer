namespace Cblx.Dynamics.Explorer.Services.Authenticator;

public record DynamicsConfig
{
    public string Group { get; set; } = "Group";
    public string Name { get; set; } = "default";
    public string Key => CreateKey(Group, Name);
    public string Authority { get; set; } = "";
    public string ResourceUrl { get; set; } = "";
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
    public string DefaultAccess { get; set; } = DynamicsAccess.None;
    public DynamicsUserAccess[] Users { get; set; } = [];
    public static string CreateKey(string group, string name) => $"_cblx.dynamics_{group}_{name}";
}
