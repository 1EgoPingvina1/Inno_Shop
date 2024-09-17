using Inno_Shop.Authentification.API.Errors;
using Inno_Shop.Product.Service.CQRS.Command;
using Inno_Shop.Product.Service.Helpers.UnitOfWork;
using Inno_Shop.Product.Service.Helpers.Validation;
using Inno_Shop.Product.Service.Interfaces;
using MediatR;

namespace Inno_Shop.Product.Service.CQRS.Handler;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProductValidator _productValidator;
    private readonly IProductRepository _ProductRepository;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, ProductValidator productValidator, IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productValidator = productValidator;
        _ProductRepository = productRepository;
    }
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = await _productValidator.ValidateAsync(request.ProductDto, cancellationToken);
        if (!validator.IsValid) 
            throw new HttpExeption(500, "Check your data on the validation");
        
        var product = new Model.Product(request.ProductDto.Name, 
            request.ProductDto.Description,
            request.ProductDto.Price,
            request.ProductDto.IsAvailable,
            request.ProductDto.CreatedByUserId);
        _ProductRepository.AddProduct(product);
        
        if (_unitOfWork.HasChanges())
            await _unitOfWork.Complete();

        return Unit.Value;
    }
}