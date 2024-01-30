namespace MentorsDiary.Web.Data.Services.Bases;

/// <summary>
/// Interface IBaseService
/// </summary>
/// <typeparam name="TEntity">The type of the t entity.</typeparam>
public interface IBaseService<TEntity> where TEntity : class
{
    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <returns>Task&lt;System.Nullable&lt;IEnumerable&lt;TEntity&gt;&gt;&gt;.</returns>
    Task<IEnumerable<TEntity>?> GetAllAsync();

    /// <summary>
    /// Gets all by filter asynchronous.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>Task&lt;System.Nullable&lt;IEnumerable&lt;TEntity&gt;&gt;&gt;.</returns>
    Task<IEnumerable<TEntity>?> GetAllByFilterAsync(string query);

    /// <summary>
    /// Gets the identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Task&lt;System.Nullable&lt;TEntity&gt;&gt;.</returns>
    Task<TEntity?> GetIdAsync(int id);

    /// <summary>
    /// Deletes the asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
    Task<HttpResponseMessage> DeleteAsync(int id);

    /// <summary>
    /// Creates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
    Task<HttpResponseMessage> CreateAsync(TEntity entity);

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
    Task<HttpResponseMessage> UpdateAsync(TEntity entity);
}