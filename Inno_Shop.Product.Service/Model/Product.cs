namespace Inno_Shop.Product.Service.Model;

/// <summary>
/// Represents a product with its properties and behavior.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets the Id of the product.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets the name of the product.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the description of the product.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the price of the product.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the product is available.
    /// </summary>
    public bool IsAvailable { get; private set; }

    /// <summary>
    /// Gets the ID of the user who created the product.
    /// </summary>
    public string CreatedByUserId { get; private set; } 

    /// <summary>
    /// Gets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedDate { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class.
    /// </summary>
    /// <param name="name">The name of the product.</param>
    /// <param name="description">The description of the product.</param>
    /// <param name="price">The price of the product.</param>
    /// <param name="isAvailable">A value indicating whether the product is available.</param>
    /// <param name="createdByUserId">The ID of the user who created the product.</param>
    public Product(string name, string description, decimal price, bool isAvailable, string createdByUserId)
    {
        Name = name;
        Description = description;
        Price = price;
        IsAvailable = isAvailable;
        CreatedByUserId = createdByUserId;
        CreatedDate = DateTime.UtcNow;
    }
}