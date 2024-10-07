using AutoMapper;
using Inno_Shop.Product.Application.CQRS.Command;
using Inno_Shop.Product.Application.DTO;
using Inno_Shop.Product.Persistence.Helpers.UnitOfWork;
using Inno_Shop.Product.Persistence.Interfaces;
using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Handler.Command;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork,IProductRepository productRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<ProductDTO> Handle(CreateProductCommand request,CancellationToken cancellationToken)
    {
        var product = new Domain.Product
        { 
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };
        _productRepository.AddProduct(product);
        await _unitOfWork.Complete();

        return _mapper.Map<ProductDTO>(product);
    }
}