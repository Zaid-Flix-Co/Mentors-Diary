using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Desktop.Admin.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Desktop.Admin.Data.Services;

/// <summary>
/// Class UserService.
/// Implements the <see cref="User" />
/// </summary>
/// <seealso cref="User" />
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