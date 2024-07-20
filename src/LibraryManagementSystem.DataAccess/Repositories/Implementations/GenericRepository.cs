using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Entities.Common;
using LibraryManagementSystem.DataAccess.Contexts;
using LibraryManagementSystem.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagementSystem.DataAccess.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll()
    {
        var query = _context.Set<T>().AsQueryable();

        return query;
    }

    public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression)
    {
        var query = _context.Set<T>().AsQueryable();
        
        return query.Where(expression);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var query = _context.Set<T>().AsQueryable();
        //foreach (var include in includes)
        //{
        //    query = query.Include(include);
        //}

        return await query.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        var dbEntity = _context.Set<T>().Find(entity.Id);
        if (dbEntity != null)
        {
            _context.Entry(dbEntity).CurrentValues.SetValues(entity);
            //_context.SaveChanges();
        }
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    //public void Update(T entity, params string[] includes)
    //{
    //    var query = _context.Set<T>().AsQueryable();
    //    foreach (var include in includes)
    //    {
    //        query = query.Include(include);
    //    }

    //    var entityToUpdate = query.FirstOrDefault(); // Or use FirstOrDefault if you expect only one entity

    //    if (entityToUpdate != null)
    //    {
    //        // Update the properties of the retrieved entity
    //        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);

    //        // Save changes to the database
    //        _context.SaveChanges();
    //    }
    //}

    public void SoftDelete(T entity)
    {
        entity.IsDeleted = true;
        Update(entity);
    }

    //public async Task UpdateAsync(T entity)
    //{
    //    var existingBook = await _context.Set<T>().FirstOrDefaultAsync(b => b.Id == entity.Id);

    //    if (existingBook != null)
    //    {
    //        // Əsas məlumatları yeniləyin
    //        existingBook = entity;

    //        // Əlaqəli cədvəldəki məlumatları yeniləyin
    //        if (existingBook.AuthorId != updatedBook.AuthorId)
    //        {
    //            existingBook.AuthorId = updatedBook.AuthorId;
    //            existingBook.Author = await _context.Authors.FindAsync(updatedBook.AuthorId);
    //        }

    //        // Verilənləri kontekstə qeyd edin
    //        _context.Books.Update(existingBook);
    //        await _context.SaveChangesAsync();
    //    }
    //}

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _context.Set<T>().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.AnyAsync(expression);
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync(); //Bu sekilde de yazmaq olur

}
