namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.ListTablesForMenu;
public interface IListEntitiesForMenuHandler
{
    Task<EntityDto[]> GetAsync();
}