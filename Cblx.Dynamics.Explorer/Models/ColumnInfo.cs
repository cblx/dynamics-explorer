using System.Xml.Linq;

namespace Cblx.Dynamics.Explorer.Models;

public class ColumnInfo
{
    public ColumnInfo(XElement propertyElement,DynamicsComponentFactory factory)
    {

        var entityElement = propertyElement.Parent!;
        var entityOptions = factory.GetTableOptions(entityElement);
        DisplayName = propertyElement.Attribute("Name")!.Value;
        TypeName = propertyElement.Attribute("Type")!.Value;
        OriginalName = DisplayName;
        IsEditable = !new string[]
        {
            "createdon",
            "importsequencenumber",
            "modifiedon",
            "overriddencreatedon",
            "timezoneruleversionnumber",
            "utcconversiontimezonecode",
            "versionnumber",
            "_createdby_value",
            "_createdonbehalfby_value",
            "_modifiedby_value",
            "_modifiedonbehalfby_value",
            "_ownerid_value",
            "_owningbusinessunit_value",
            "_owninguser_value",
            "_owningteam_value"
        }.Contains(OriginalName);

        var options = entityOptions?.Columns.FirstOrDefault(c => c.Name == OriginalName);
        IsPrimaryKey = entityElement
                        .Descendants()
                        .Any(d => d.Name.LocalName == "Key"
                                  &&
                                  d.Descendants()
                                   .Any(dd => dd.Attribute("Name")!.Value == propertyElement.Attribute("Name")!.Value)
                        );
        if(options != null)
        {
            DisplayName = options.FriendlyName;
            OriginalName = options.Name;
        }

        if (IsForeignKey)
        {
            var navigationPropertyElement = entityElement
                .Descendants()
                .FirstOrDefault(d => d.Name.LocalName == "NavigationProperty"
                                     &&
                                     d.Descendants()
                                      .Any(dd => dd.Attribute("Property")!.Value == OriginalName));
            // Don't know why this happens, but it does
            if(navigationPropertyElement == null)
            {
                return;
            }
            NavigationName = navigationPropertyElement?.Attribute("Name")!.Value;
            var foreignTableName = navigationPropertyElement!.Attribute("Type")!.Value.Split('.').Last();
            if(factory.FindTableByOriginalName(foreignTableName) is TableInfo foreignTable)
            {
                ForeignTable = foreignTable;
                return;
            }
            // TODO: performance could be improved here
            var foreignTableElement = entityElement.Parent!
                                        .Descendants()
                                        .FirstOrDefault(d => d.Name.LocalName == "EntityType" 
                                                            && d.Attribute("Name")!.Value == foreignTableName);
            ForeignTable = factory.CreateTable(foreignTableElement!);
        }
    }
    
    public bool IsEditable { get; }

    public bool IsPrimaryKey { get; }
    public bool IsForeignKey { get => OriginalName.StartsWith('_') && OriginalName.EndsWith("_value"); }
    public string DisplayName { get; }
    public string TypeName { get; }
    public string OriginalName { get; }
    public bool HasFriendlyName => DisplayName != OriginalName;
    public string? NavigationName { get; set; }
    public TableInfo? ForeignTable { get; set; }
}