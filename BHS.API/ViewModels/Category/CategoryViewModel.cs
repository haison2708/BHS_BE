using System.Text.Json.Serialization;

namespace BHS.API.ViewModels.Category;

public class CategoryViewModel
{
    public int Id { get; set; }
    public string? CategoryCode { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public int Sort { get; set; }
    public int ParentId { get; set; }
    public int Level { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<CategoryViewModel>? Category { get; set; }
}