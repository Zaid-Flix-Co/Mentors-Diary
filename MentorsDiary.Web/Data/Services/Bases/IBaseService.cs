namespace MentorsDiary.Web.Data.Services.Bases;

public interface IBaseService<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>?> GetAllAsync();

    Task<IEnumerable<TEntity>?> GetAllByFilterAsync(string query);

    Task<TEntity?> GetIdAsync(int id);

    Task<HttpResponseMessage> DeleteAsync(int id);

    Task<HttpResponseMessage> CreateAsync(TEntity entity);

    Task<HttpResponseMessage> UpdateAsync(TEntity entity);
}