using Inno_Shop.Product.Service.CQRS.Command;
using Inno_Shop.Product.Service.Helpers.UnitOfWork;
using Inno_Shop.Product.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Inno_Shop.Product.Service.CQRS.Handler.Command;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService; 

    public CreateProductCommandHandler(IUnitOfWork unitOfWork,IProductRepository productRepository, IAuthorizationService authorizationService)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }
    public async Task<Unit> Handle(CreateProductCommand request,CancellationToken cancellationToken)
    {
        var product = new Model.Product
        { 
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
        };
        _productRepository.AddProduct(product);
        
        if (_unitOfWork.HasChanges())
            await _unitOfWork.Complete();

        return Unit.Value;
    }
}