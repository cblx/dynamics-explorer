namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.GetEntity;

public interface IGetEntityHandler
{
    Task<EntityDto> GetAsync(string entityLogicalName);
}