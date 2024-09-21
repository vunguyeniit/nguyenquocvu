using Microsoft.EntityFrameworkCore;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Repositories;

public interface IRepository<T>
{
    IQueryable<T> Select(bool isTracking = false);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
    //Task DeleteAsync(long id);
    //Workflow FirstOrDefault(Func<object, bool> value);
}

public class Repository<T> : IRepository<T> where T : class
{
    protected DataContext Context;
    public Repository(DataContext context) { Context = context; }

    public IQueryable<T> Select(bool isTracking = false)
    {
        var query = Context.Set<T>().AsQueryable();
        query = !isTracking ? query.AsNoTracking() : query;
        return query;
    }

    public void Insert(T entity) => Context.Set<T>().Add(entity);

    public void Update(T entity) => Context.Set<T>().Update(entity);

    public void Delete(T entity) => Context.Set<T>().Remove(entity);

    public async Task SaveChangesAsync() => await Context.SaveChangesAsync();

   
}