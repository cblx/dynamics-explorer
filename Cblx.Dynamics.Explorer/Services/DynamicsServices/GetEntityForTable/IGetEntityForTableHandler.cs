namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.ListEntityAttributes;

public interface IGetEntityForTableHandler
{
    Task<EntityDto> GetAsync(string entityLogicalName);
}