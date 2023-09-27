namespace Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.GetEntity;

public class AttributeDto
{
    public required bool IsPrimaryId { get; set; }
    public required bool IsPrimaryName { get; set; }
    public required string LogicalName { get; set; }
    public required string? DisplayName { get; set; }
    public required string? CustomName { get; set; }
    public required string? DerivedType { get; set; }
    public required string AttributeType { get; set; }
    public DateTimeFormat? DateTimeFormat { get; set; }
    public bool IsEditable { get; set; }
    public required string EntityLogicalName { get; set; }
    public required bool IsValidForCreate { get; set; }
    public required bool IsValidForUpdate { get; set; }
    public string? ReferencedEntity { get; set; }
    public string LookupPropertyNameOrLogicalName => ReferencedEntity != null ?
        $"_{LogicalName}_value" : LogicalName;
}
