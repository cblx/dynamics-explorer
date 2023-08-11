namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.Delete;

internal class DeleteHandler(ExplorerHttpClient client) : IDeleteHandler
{
    public async Task DeleteAsync(string entityLogicalName, Guid id) 
        => await client.HttpClient.DeleteAsync($"{entityLogicalName}({id})");
}
