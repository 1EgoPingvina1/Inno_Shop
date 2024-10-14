namespace Inno_Shop.Product.Domain.Model;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string CreatedByUserId { get;  set; } 
    public DateTime CreatedDate { get; set; }
}