using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Mindbox.Datebase.PostgreSQL.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString).EnableSensitiveDataLogging(), ServiceLifetime.Transient);
builder.Services.AddScoped<DbInitializer>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseItToSeedSqlServer(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/GetAllCategories", (ApplicationDbContext db) => db.Categories.AsNoTracking().ToList());
app.MapGet("/GetAllProducts", (ApplicationDbContext db) => db.Products.AsNoTracking().ToList());
app.MapGet("/GetAllProductsWithCategories", (ApplicationDbContext db) => JsonSerializer.Serialize(new { result = db.Products.Include(p => p.Categories).AsNoTracking().OrderBy(p => p.Name).ToList() }));
app.MapGet("/GetAllProductsWithCategoriesGroups", (ApplicationDbContext db) => 
{
    var productsWithCategories = db.Products
        .Include(p => p.Categories)
        .AsNoTracking()
        .OrderBy(p => p.Name)
        .ToList();

    List<ProductsWithCategoriesGroups> productsWithCategoriesGroups = new List<ProductsWithCategoriesGroups>();
        
    foreach (var product in productsWithCategories)
    {
        if (product.Categories.Any())
            foreach (var category in product.Categories)
                productsWithCategoriesGroups.Add(new ProductsWithCategoriesGroups(product.Name, category.Name));
        else
            productsWithCategoriesGroups.Add(new ProductsWithCategoriesGroups(product.Name, "-Без категории-"));
    }

    return JsonSerializer.Serialize(new { result = productsWithCategoriesGroups });
});

app.Run();
