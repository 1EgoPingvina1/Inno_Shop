using AutoMapper;
using Inno_Shop.Product.Service.CQRS.Query;
using Inno_Shop.Product.Service.DTO;
using Inno_Shop.Product.Service.Interfaces;
using MediatR;

namespace Inno_Shop.Product.Service.CQRS.Handler.Query;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery,ProductDTO>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDTO> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        return _mapper.Map<Model.Product,ProductDTO>(product);
    }
}