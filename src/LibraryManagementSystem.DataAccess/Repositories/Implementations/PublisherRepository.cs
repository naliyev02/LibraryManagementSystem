using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.DataAccess.Repositories.Implementations;

public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
{
    private readonly AppDbContext _context;
    public PublisherRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
