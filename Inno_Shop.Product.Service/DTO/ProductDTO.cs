namespace Inno_Shop.Product.Service.DTO;

public record ProductDTO (string Name,string Description,decimal Price,bool IsAvailable,string CreatedByUserId,DateTime CreatedDate);
