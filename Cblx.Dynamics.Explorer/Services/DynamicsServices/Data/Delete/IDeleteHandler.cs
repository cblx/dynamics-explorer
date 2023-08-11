namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.Data.Delete
{
    internal interface IDeleteHandler
    {
        Task DeleteAsync(string entityLogicalName, Guid id);
    }
}