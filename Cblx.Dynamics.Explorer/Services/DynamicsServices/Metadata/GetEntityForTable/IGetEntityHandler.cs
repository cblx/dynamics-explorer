namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.ListEntityAttributes;

public interface IGetEntityHandler
{
    Task<EntityDto> GetAsync(string entityLogicalName);
}