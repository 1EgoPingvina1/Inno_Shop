using AutoMapper;
using Inno_Shop.Product.Application.CQRS.Query;
using Inno_Shop.Product.Application.DTO;
using Inno_Shop.Product.Persistence.Interfaces;
using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Handler.Query;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDTO>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }
}