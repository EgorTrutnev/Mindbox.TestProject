public class ProductsWithCategoriesGroups
{
    public ProductsWithCategoriesGroups(string productName, string categoryName)
    {
        ProductName = productName;
        CategoryName = categoryName;
    }

    public string ProductName { get; set; }
    public string CategoryName { get; set; }
}