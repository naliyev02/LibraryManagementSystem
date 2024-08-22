using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;

namespace LibraryManagementSystem.DataAccess.Repositories.Implementations;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly AppDbContext context;

    public CategoryRepository(AppDbContext context) : base(context)
    {
        this.context = context;
    }
}
