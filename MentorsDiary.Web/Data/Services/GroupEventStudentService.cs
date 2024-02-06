using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Web.Data.Services.Bases;

namespace MentorsDiary.Web.Data.Services;

public class GroupEventStudentService(IHttpClientFactory clientFactory) : BaseService<GroupEventStudent>(clientFactory)
{
    private readonly HttpClient _httpClient = clientFactory.CreateClient("API");

    public async Task<HttpResponseMessage> AddStudentsInGroupEvent(List<GroupEventStudent> groupEventStudents)
    {
        var result = await _httpClient.PostAsJsonAsync($"api/{BasePath}/AddStudentsInGroupEvent", groupEventStudents)!;
        return result;
    }
}