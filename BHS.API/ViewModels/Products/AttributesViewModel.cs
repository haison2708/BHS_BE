using System.Text.Json.Serialization;

namespace BHS.API.ViewModels.Products;

public class AttributesViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<string>? AttributeValues { get; set; }
}