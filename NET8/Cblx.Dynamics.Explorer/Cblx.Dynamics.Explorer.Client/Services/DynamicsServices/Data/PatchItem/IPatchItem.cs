namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PatchItem;

internal interface IPatchItem
{
    Task ExecuteAsync(PatchItemRequest request);
}
