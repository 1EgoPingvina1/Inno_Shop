
using Inno_Shop.Product.Persistence.Data;

namespace Inno_Shop.Product.Persistence.Helpers.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MainDbContext _context;
    public UnitOfWork(MainDbContext context) => _context = context;
    public async Task<bool> Complete() => await _context.SaveChangesAsync() > 0;
}