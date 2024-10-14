using AutoMapper;
using Inno_Shop.Product.Application.CQRS.Command;
using Inno_Shop.Product.Application.DTO;
using Inno_Shop.Product.Persistence.Helpers.UnitOfWork;
using Inno_Shop.Product.Persistence.Interfaces;
using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Handler.Command;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,CreateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork,
        IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }
    public async Task<CreateProductCommand> Handle(CreateProductCommand request,CancellationToken cancellationToken)
    {
        var product = new Domain.Model.Product
        { 
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };
        
        _productRepository.AddProduct(product);
        await _unitOfWork.Complete();

        return request;
    }
}