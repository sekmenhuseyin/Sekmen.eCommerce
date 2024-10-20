namespace Sekmen.Commerce.Services.Products.Domain.Products;

public class Product
{
    [Key] public int Id { get; protected init; }
    [Required] public string Name { get; protected set; }
    [Range(1, 1000)] public double Price { get; protected set; }
    public string Description { get; protected set; }
    public string CategoryName { get; protected set; }
    public string ImageUrl { get; protected set; }

    protected Product()
    {
    }

    public Product(string name, double price, string description, string categoryName, string imageUrl) : this()
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price;
        Description = description;
        CategoryName = categoryName;
        ImageUrl = imageUrl;
    }

    public void Update(string name, double price, string description, string categoryName, string imageUrl)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price;
        Description = description;
        CategoryName = categoryName;
        ImageUrl = imageUrl;
    }
}