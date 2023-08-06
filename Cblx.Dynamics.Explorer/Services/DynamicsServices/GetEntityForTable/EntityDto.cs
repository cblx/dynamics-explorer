namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.ListEntityAttributes;

public class EntityDto
{
    public required string LogicalName { get; set; }
    public required string? DisplayName { get; set; }
    public required string? CustomName { get; set; }
    public required AttributeDto[] Attributes { get; set; }
}