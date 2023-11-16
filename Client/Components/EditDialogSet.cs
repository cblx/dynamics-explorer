using Cblx.Dynamics.Explorer.Client.Services.DynamicsServices.Metadata.GetEntity;
using System.Text.Json;
namespace Cblx.Dynamics.Explorer.Shared;

public class EditDialogSet
{
    public AttributeDto? Attribute { get; set; }
    public object? Value { get; set; }
    public string? FormattedValue { get; set; }
    public object? OriginalValue { get; set; }
    public bool? ValueAsBoolean
    {
        get => Value as bool?;
        set => Value = value;
    }

    public string? ValueAsString
    {
        get => Value as string;
        set => Value = value;
    }

    public decimal? ValueAsDecimal
    {
        get => Value as decimal?;
        set => Value = value;
    }

    public double? ValueAsDouble
    {
        get => Value as double?;
        set => Value = value;
    }

    public int? ValueAsInt32
    {
        get => Value as int?;
        set => Value = value;
    }

    public long? ValueAsInt64
    {
        get => Value as long?;
        set => Value = value;
    }

    public string ValueHelperText
    {
        get => JsonSerializer.Serialize(Value);
    }
}
