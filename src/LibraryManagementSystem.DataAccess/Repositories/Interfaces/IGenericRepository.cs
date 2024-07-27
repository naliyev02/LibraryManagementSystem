using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace LibraryManagementSystem.DataAccess.Repositories.Interfaces;

public interface IGenericRepository<T>
{
    IQueryable<T> GetAll(params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includeProperties);
    IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includeProperties);
    Task<T> GetByIdAsync(int id, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includeProperties);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    void SoftDelete(T entity);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<int> SaveAsync();
}
