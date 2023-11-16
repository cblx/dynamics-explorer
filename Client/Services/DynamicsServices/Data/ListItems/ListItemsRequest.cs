namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.ListItems;

public class ListItemsRequest
{
    public required string EntitySetName { get; set; }
    public required int Skip { get; set; }
    public required int Take { get; set; }
}