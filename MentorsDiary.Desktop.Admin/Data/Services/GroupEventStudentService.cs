using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Desktop.Admin.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Desktop.Admin.Data.Services;

/// <summary>
/// Class GroupEventStudentService.
/// Implements the <see cref="GroupEventStudent" />
/// </summary>
/// <seealso cref="GroupEventStudent" />
public class GroupEventStudentService: BaseService<GroupEventStudent>
{
    /// <summary>
    /// The HTTP client
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventStudentService" /> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="cache">The cache.</param>
    public GroupEventStudentService(IHttpClientFactory clientFactory, IDistributedCache cache) : base(clientFactory, cache)
    {
        _httpClient = clientFactory.CreateClient("API");
    }

    /// <summary>
    /// Adds the students in group event.
    /// </summary>
    /// <param name="groupEventStudents">The group event students.</param>
    /// <returns>HttpResponseMessage.</returns>
    public async Task<HttpResponseMessage> AddStudentsInGroupEvent(List<GroupEventStudent> groupEventStudents)
    {
        var result = await _httpClient?.PostAsJsonAsync($"api/{BasePath}/AddStudentsInGroupEvent", groupEventStudents)!;
        return result;
    }
}