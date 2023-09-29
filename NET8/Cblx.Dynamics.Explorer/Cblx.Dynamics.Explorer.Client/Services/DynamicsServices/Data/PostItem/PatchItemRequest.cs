namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PostItem;

public class PostItemRequest
{
    public string EntitySetName { get; set; } = string.Empty;
    public required Dictionary<string, object?> Data { get; set; }
}