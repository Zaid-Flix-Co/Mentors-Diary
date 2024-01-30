using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Entities.Bases.Filters;
using System.Linq.Expressions;

namespace MentorsDiary.Application.Entities.Bases.Interfaces;

public interface IBaseRepository<T> 
    where T : class, IHaveId, IHaveName
{
    Task<T?> GetById(int id);

    Task<IEnumerable<T>?> GetAll();

    Task<IEnumerable<T>?> GetAllByFilter(string query);

    Task<IEnumerable<T>?> GetAllByFilter(FilterParams query);

    Task<IEnumerable<T>?> Find(Expression<Func<T, bool>> expression);

    Task Add(T entity);

    Task Update(T entity);

    Task Remove(T entity);
}