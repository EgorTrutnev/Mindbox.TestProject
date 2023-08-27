using Microsoft.EntityFrameworkCore;
using Mindbox.Datebase.PostgreSQL.Models;

namespace Mindbox.Datebase.PostgreSQL.Data;

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}


