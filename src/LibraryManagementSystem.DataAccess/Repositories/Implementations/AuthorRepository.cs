using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.DataAccess.Repositories.Implementations;

public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
    private readonly AppDbContext _context;
    public AuthorRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
