namespace Cblx.Dynamics.Explorer.Services.DynamicsServices.ListEntityAttributes;

public class AttributeDto
{
    public required bool IsPrimaryId { get; set; }
    public required bool IsPrimaryName { get; set; }
    public required string LogicalName { get; set; }
    public required string? DisplayName { get; set; }
    public required string? CustomName { get; set; }
    public required string? DerivedType { get; set; }
    public required string AttributeType { get; set; }
}
