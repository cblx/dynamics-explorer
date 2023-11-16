namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PatchItem;

public class PatchItemRequest
{
    public Guid Id { get; set; }
    public string EntitySetName { get; set; } = string.Empty;
    public required Dictionary<string, object?> Data { get; set; }
}