using Mindbox.Datebase.PostgreSQL.Data;
using Mindbox.Datebase.PostgreSQL.Models;

internal class DbInitializer
{
    internal static async Task Initialize(ApplicationDbContext _context)
    {
        ArgumentNullException.ThrowIfNull(_context, nameof(_context));

        _context.Database.EnsureCreated();

        List<Category> categories = new List<Category>();

        if (!_context.Categories.Any())
        {
            await CreateCategoriesAndProducts(_context);
        }
    }

    private static async Task CreateCategoriesAndProducts(ApplicationDbContext _context)
    {
        var categories = new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Name = "Мясо и рыба" },
            new Category { Id = Guid.NewGuid(), Name = "Напитки" },
            new Category { Id = Guid.NewGuid(), Name = "Сладости" },
            new Category { Id = Guid.NewGuid(), Name = "Хлебобулочные изделия" },
            new Category { Id = Guid.NewGuid(), Name = "Полуфабрикаты" }
        };

        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Пирожки с мясом",
                Categories = categories
                .Where(p => p.Name == "Мясо и рыба" || p.Name == "Хлебобулочные изделия")
                .ToList()
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Котлеты",
                Categories = categories
                .Where(p => p.Name == "Мясо и рыба" || p.Name == "Полуфабрикаты")
                .ToList()
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Сладкая газированная вода",
                Categories = categories
                    .Where(p => p.Name == "Напитки" || p.Name == "Сладости")
                    .ToList()
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Яблоки"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Одноразовые тарелки"
            }
        };

        await _context.Categories.AddRangeAsync(categories);
        await _context.Products.AddRangeAsync(products);
        _context.SaveChanges();
    }
}