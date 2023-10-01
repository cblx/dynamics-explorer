using Cblx.Dynamics.Explorer.Models;
using Cblx.Dynamics.Explorer.Services.DynamicsServices.Metadata.ListOptionsHandler;
using System.Net.Http.Json;
using System.Web;

namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.ListOptionsHandler;
internal class ListOptionsClientHandler(HttpClient client) : IListOptionsHandler
{
    public async Task<PicklistOption[]> GetAsync(string entityLogicalName, string attributeLogicalName, string derivedTypeName)
        => (await client.GetFromJsonAsync<PicklistOption[]>($"{Route.GetEndpoint<IListOptionsHandler>()}?{nameof(entityLogicalName)}={entityLogicalName}&{nameof(attributeLogicalName)}={attributeLogicalName}&{nameof(derivedTypeName)}={HttpUtility.UrlEncode(derivedTypeName)}"))!;


}
