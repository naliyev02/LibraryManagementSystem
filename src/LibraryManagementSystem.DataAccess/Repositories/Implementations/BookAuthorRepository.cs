using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.DataAccess.Repositories.Implementations;

public class BookAuthorRepository : GenericRepository<BookAuthor>, IBookAuthorRepository
{
    private readonly AppDbContext _context;
    public BookAuthorRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
