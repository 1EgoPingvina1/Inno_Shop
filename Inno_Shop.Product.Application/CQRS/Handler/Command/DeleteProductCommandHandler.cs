using Inno_Shop.Product.Application.CQRS.Command;
using Inno_Shop.Product.Persistence.Helpers.UnitOfWork;
using Inno_Shop.Product.Persistence.Interfaces;
using MediatR;

namespace Inno_Shop.Product.Application.CQRS.Handler.Command;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfwork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfwork)
    {
        _productRepository = productRepository;
        _unitOfwork = unitOfwork;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        _productRepository.DeleteProduct(product);
        await _unitOfwork.Complete();
        return Unit.Value;
    }
}