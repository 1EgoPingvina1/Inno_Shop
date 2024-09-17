using Inno_Shop.Product.Service.Data;

namespace Inno_Shop.Product.Service.Helpers.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MainDbContext _context;
    public UnitOfWork(MainDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Complete() => await _context.SaveChangesAsync() > 0;

    public bool HasChanges() => _context.ChangeTracker.HasChanges();
}