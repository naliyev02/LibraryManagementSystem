using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.DataAccess.Repositories.Implementations;

public class BookGenreRepository : GenericRepository<BookGenre>, IBookGenreRepository
{
    private readonly AppDbContext context;

    public BookGenreRepository(AppDbContext context) : base(context)
    {
        this.context = context;
    }
}
