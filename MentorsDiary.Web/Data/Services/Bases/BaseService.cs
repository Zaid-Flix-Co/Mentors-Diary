using MentorsDiary.Application.Entities.Bases.Filters;
using Newtonsoft.Json;

namespace MentorsDiary.Web.Data.Services.Bases;

public abstract class BaseService<TEntity>(IHttpClientFactory clientFactory) : IBaseService<TEntity>
    where TEntity : class, new()
{
    private readonly HttpClient? _httpClient = clientFactory.CreateClient("API");

    protected virtual string BasePath => typeof(TEntity).Name.ToLower();

    public virtual async Task<IEnumerable<TEntity>?> GetAllAsync()
    {
        var result = await _httpClient?.GetFromJsonAsync<IEnumerable<TEntity>>($"api/{BasePath}")!;
        return result;
    }

    public virtual async Task<IEnumerable<TEntity>?> GetAllByFilterAsync(string query)
    {
        var responseMessage = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/filter/{query}", query)!;
        var result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await responseMessage.Content.ReadAsStringAsync());
        return result;
    }

    public virtual async Task<IEnumerable<TEntity>?> GetAllByFilterAsync(FilterParams query)
    {
        var responseMessage = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/filter", query)!;
        var result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await responseMessage.Content.ReadAsStringAsync());
        return result;
    }

    public virtual async Task<TEntity?> GetIdAsync(int id)
    {
        var responseMessage = await _httpClient!.PostAsJsonAsync($"api/{BasePath}/{id}", id);
        var result = JsonConvert.DeserializeObject<TEntity>(await responseMessage.Content.ReadAsStringAsync());
        return result;
    }

    public virtual async Task<HttpResponseMessage> DeleteAsync(int id)
    {
        var result = await _httpClient?.DeleteAsync($"api/{BasePath}/{id}")!;
        return result;
    }

    public virtual async Task<HttpResponseMessage> CreateAsync(TEntity entity)
    {
        var result = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/Create", entity)!;
        return result;
    }

    public virtual async Task<HttpResponseMessage> UpdateAsync(TEntity entity)
    {
        var result = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/Update", entity)!;
        return result;
    }

    public async Task<HttpResponseMessage> UploadAvatarAsync(MultipartFormDataContent content)
    {
        var result = await _httpClient?.PostAsync($"api/{BasePath}/UploadAvatar", content)!;
        return result;
    }

    public async Task<HttpResponseMessage> GetAvatarAsync(string avatarPath)
    {
        var result = await _httpClient!.GetAsync($"api/{BasePath}/GetAvatar/{avatarPath}");
        return result;
    }
}