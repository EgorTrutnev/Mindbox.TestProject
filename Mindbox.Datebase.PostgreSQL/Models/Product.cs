using System.Text.Json.Serialization;

namespace Mindbox.Datebase.PostgreSQL.Models;

public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public virtual ICollection<Category>? Categories { get; set; }
}