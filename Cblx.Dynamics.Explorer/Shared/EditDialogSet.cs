using Cblx.Dynamics.Explorer.Models;
using System.Text.Json;
namespace Cblx.Dynamics.Explorer.Shared;

internal class EditDialogSet
{
    public ColumnInfo? Column { get; set; }
    public object? Value { get; set; }
    public object? AcceptedValue { get; set; }
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

    public DateTime? ValueAsDateTime
    {
        get => Value as DateTime?;
        set => Value = value;
    }

    public DateOnly? ValueAsDateOnly
    {
        get => Value as DateOnly?;
        set => Value = value;
    }

    public string ValueHelperText
    {
        get => JsonSerializer.Serialize(Value);
    }

    public void AcceptValue()
    {
        AcceptedValue = Value;
    }

    public bool IsDirty()
    {
        return !Equals(Value, AcceptedValue);
    }
}
