using MentorsDiary.Application.Entities.Bases.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MentorsDiary.Web.Data.Services.Bases;

/// <summary>
/// Class BaseService.
/// Implements the <see cref="IBaseService{TEntity}" />
/// </summary>
/// <typeparam name="TEntity">The type of the t entity.</typeparam>
/// <seealso cref="IBaseService{TEntity}" />
public abstract class BaseService<TEntity> : IBaseService<TEntity> 
    where TEntity : class, new()
{
    /// <summary>
    /// The HTTP client
    /// </summary>
    private readonly HttpClient? _httpClient;

    /// <summary>
    /// The cache
    /// </summary>
    private readonly IDistributedCache _cache;

    /// <summary>
    /// Gets the base path.
    /// </summary>
    /// <value>The base path.</value>
    protected virtual string BasePath => typeof(TEntity).Name.ToLower();

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseService{TEntity}"/> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="cache">The cache.</param>
    protected BaseService(IHttpClientFactory clientFactory, IDistributedCache cache)
    {
        _cache = cache;
        _httpClient = clientFactory.CreateClient("API");
    }

    /// <summary>
    /// Get all as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public virtual async Task<IEnumerable<TEntity>?> GetAllAsync()
    {
        //try
        //{
        //    var value = await _cache.GetStringAsync($"{typeof(TEntity).Name.ToUpper()}_ALL");
        //    if (value != null)
        //    {
        //        var entities = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(value);
        //        return entities;
        //    }

        //    var result = await _httpClient?.GetFromJsonAsync<IEnumerable<TEntity>>($"api/{BasePath}")!;

        //    value = JsonConvert.SerializeObject(result);
        //    await _cache.SetStringAsync($"{typeof(TEntity).Name.ToUpper()}_ALL", value, new DistributedCacheEntryOptions
        //    {
        //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        //    });

        //    return result;
        //}
        //catch
        //{
            var result = await _httpClient?.GetFromJsonAsync<IEnumerable<TEntity>>($"api/{BasePath}")!;
            return result;
        //}
    }

    /// <summary>
    /// Get all by filter as an asynchronous operation.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public virtual async Task<IEnumerable<TEntity>?> GetAllByFilterAsync(string query)
    {
        var responseMessage = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/filter/{query}", query)!;
        var result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await responseMessage.Content.ReadAsStringAsync());
        return result;
    }

    /// <summary>
    /// Get all by filter as an asynchronous operation.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>A Task&lt;HttpResponseMessage&gt; representing the asynchronous operation.</returns>
    public virtual async Task<IEnumerable<TEntity>?> GetAllByFilterAsync(FilterParams query)
    {
        var responseMessage = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/filter", query)!;
        var result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await responseMessage.Content.ReadAsStringAsync());
        return result;
    }

    /// <summary>
    /// Get identifier as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>A Task&lt;TEntity&gt; representing the asynchronous operation.</returns>
    public virtual async Task<TEntity?> GetIdAsync(int id)
    {
        var responseMessage = await _httpClient!.PostAsJsonAsync($"api/{BasePath}/{id}", id);
        var result = JsonConvert.DeserializeObject<TEntity>(await responseMessage.Content.ReadAsStringAsync());
        return result;
    }

    /// <summary>
    /// Delete as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>A Task&lt;HttpResponseMessage&gt; representing the asynchronous operation.</returns>
    public virtual async Task<HttpResponseMessage> DeleteAsync(int id)
    {
        var result = await _httpClient?.DeleteAsync($"api/{BasePath}/{id}")!;
        return result;
    }

    /// <summary>
    /// Create as an asynchronous operation.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A Task&lt;HttpResponseMessage&gt; representing the asynchronous operation.</returns>
    public virtual async Task<HttpResponseMessage> CreateAsync(TEntity entity)
    {
        var result = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/Create", entity)!;
        return result;
    }

    /// <summary>
    /// Update as an asynchronous operation.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A Task&lt;HttpResponseMessage&gt; representing the asynchronous operation.</returns>
    public virtual async Task<HttpResponseMessage> UpdateAsync(TEntity entity)
    {
        var result = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/Update", entity)!;
        return result;
    }

    /// <summary>
    /// Upload avatar as an asynchronous operation.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns>A Task&lt;HttpResponseMessage&gt; representing the asynchronous operation.</returns>
    public async Task<HttpResponseMessage> UploadAvatarAsync(MultipartFormDataContent content)
    {
        var result = await _httpClient?.PostAsync($"api/{BasePath}/UploadAvatar", content)!;
        return result;
    }

    /// <summary>
    /// Get avatar as an asynchronous operation.
    /// </summary>
    /// <param name="avatarPath">The avatar path.</param>
    /// <returns>A Task&lt;HttpResponseMessage&gt; representing the asynchronous operation.</returns>
    public async Task<HttpResponseMessage> GetAvatarAsync(string avatarPath)
    {
        var result = await _httpClient!.GetAsync($"api/{BasePath}/GetAvatar/{avatarPath}");
        return result;
    }
}