using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.DataAccess.Repositories.Implementations;

public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
{
    private readonly AppDbContext _context;
    public LanguageRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
