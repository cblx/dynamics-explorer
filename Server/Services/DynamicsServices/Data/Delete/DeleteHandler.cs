namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.Delete;

internal class DeleteHandler(ExplorerHttpClient client, UserContext userContext) : IDeleteHandler
{
    public async Task DeleteAsync(string entityLogicalName, Guid id)
    {
        userContext.AssertCanWriteCurrentInstance();
        await client.HttpClient.DeleteAsync($"{entityLogicalName}({id})");
    }
}
