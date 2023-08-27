using System.Text.Json.Serialization;

namespace Mindbox.Datebase.PostgreSQL.Models;

public class Category
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Product>? Products { get; set; }
}