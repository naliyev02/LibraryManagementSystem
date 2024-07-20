using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.DataAccess.Repositories.Implementations;

public class CoverTypeRepository : GenericRepository<CoverType>, ICoverTypeRepository
{
    private readonly AppDbContext _context;
    public CoverTypeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

}
