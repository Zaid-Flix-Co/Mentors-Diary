using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

public class UserService(IHttpClientFactory clientFactory) : BaseService<User>(clientFactory)
{
    private readonly HttpClient? _authHttpClient = clientFactory.CreateClient("AUTH");

    public async Task<HttpResponseMessage> CreateApplicationUsersAsync(List<User> users)
    {
        var result = await _authHttpClient?.PostAsJsonAsync($"api/User/CreateApplicationUsers", users)!;
        return result;
    }
}