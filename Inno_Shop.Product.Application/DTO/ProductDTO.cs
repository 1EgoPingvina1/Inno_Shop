namespace Inno_Shop.Product.Application.DTO;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price{ get; set; }
    public string CreatedByUserId { get; set; } = null!;
}
