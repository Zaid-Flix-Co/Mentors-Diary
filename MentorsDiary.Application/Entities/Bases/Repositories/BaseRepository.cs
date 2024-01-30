using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Bases.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MentorsDiary.Application.Entities.Bases.Filters;

namespace MentorsDiary.Application.Entities.Bases.Repositories;

public abstract class BaseRepository<T>(DbContext context) : IBaseRepository<T>
    where T : class, IHaveId, IHaveName
{
    protected readonly DbContext Context = context;

    public async Task<T?> GetById(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>?> GetAll()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>?> GetAllByFilter(string query)
    {
        return await Context.Set<T>()
            .Where(t => t.Name == query)
            .ToListAsync();
    }

    public async Task<IEnumerable<T>?> GetAllByFilter(FilterParams query)
    {
        var result = await Filter<T>.FilteredData(new List<FilterParams> { query }, await GetAll() ?? Array.Empty<T>());
        return result;
    }

    public Task<IEnumerable<T>?> Find(Expression<Func<T, bool>> expression)
    {
        return Task.FromResult<IEnumerable<T>?>(Context?.Set<T>().Where(expression));
    }

    public async Task Add(T entity)
    {
        Context.Set<T>().Add(entity);
        await Context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }
}