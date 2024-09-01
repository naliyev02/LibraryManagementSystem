using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.DataAccess.Repositories.Implementations;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    private readonly AppDbContext context;

    public BookRepository(AppDbContext context) : base(context)
    {
        this.context = context;
    }
}
