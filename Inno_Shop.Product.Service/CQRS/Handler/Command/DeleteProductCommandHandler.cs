using Inno_Shop.Product.Service.CQRS.Command;
using Inno_Shop.Product.Service.Helpers.UnitOfWork;
using Inno_Shop.Product.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace Inno_Shop.Product.Service.CQRS.Handler.Command;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfwork;
    private readonly IAuthChecker _authChecker;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfwork,
        IAuthChecker authChecker)
    {
        _productRepository = productRepository;
        _unitOfwork = unitOfwork;
        _authChecker = authChecker;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        if (!await _authChecker.IsUserAuthenticated())
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            await _productRepository.DeleteProduct(product);
            if (_unitOfwork.HasChanges())
                await _unitOfwork.Complete();
            return Unit.Value;
        }
        throw new UnauthorizedAccessException("You are not authorized to delete this product");
    }
}