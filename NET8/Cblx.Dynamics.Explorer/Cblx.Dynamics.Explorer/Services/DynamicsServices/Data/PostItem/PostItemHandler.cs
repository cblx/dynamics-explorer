using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Data.PostItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.PostItem;

internal class PostItemHandler(ExplorerHttpClient client) : IPostItem
{
    public async Task ExecuteAsync(PostItemRequest request)
        => await client.HttpClient.PostAsJsonAsync(request.EntitySetName, request.Data);
}
