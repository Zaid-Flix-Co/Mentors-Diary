using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Web.Data.Services;

/// <summary>
/// Class UserService.
/// Implements the <see cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Users.Domains.User}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Users.Domains.User}" />
public class UserService: BaseService<User>
{
    /// <summary>
    /// The authentication HTTP client
    /// </summary>
    private readonly HttpClient? _authHttpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService" /> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="cache">The cache.</param>
    public UserService(IHttpClientFactory clientFactory, IDistributedCache cache) : base(clientFactory, cache)
    {
        _authHttpClient = clientFactory.CreateClient("AUTH");
    }

    /// <summary>
    /// Create application users as an asynchronous operation.
    /// </summary>
    /// <param name="users">The users.</param>
    /// <returns>A Task&lt;HttpResponseMessage&gt; representing the asynchronous operation.</returns>
    public async Task<HttpResponseMessage> CreateApplicationUsersAsync(List<User> users)
    {
        var result = await _authHttpClient?.PostAsJsonAsync($"api/User/CreateApplicationUsers", users)!;
        return result;
    }
}