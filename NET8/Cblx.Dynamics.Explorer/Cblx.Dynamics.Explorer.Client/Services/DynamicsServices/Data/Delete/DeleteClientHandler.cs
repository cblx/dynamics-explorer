using Cblx.Dynamics.Explorer.Client;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.Delete;

internal class DeleteClientHandler(HttpClient client) : IDeleteHandler
{
    public async Task DeleteAsync(string entityLogicalName, Guid id) 
        => await client.DeleteAsync($"{Route.GetEndpoint<IDeleteHandler>()}?{nameof(entityLogicalName)}={entityLogicalName}&id={id}");
}
