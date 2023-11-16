namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PostItem;

internal interface IPostItem
{
    Task ExecuteAsync(PostItemRequest request);
}
