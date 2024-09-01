using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.DataAccess.Repositories.Implementations;

public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    private readonly AppDbContext context;

    public GenreRepository(AppDbContext context) : base(context)
    {
        this.context = context;
    }
}
