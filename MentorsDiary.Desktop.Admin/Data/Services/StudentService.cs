using System.Net.Http;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Desktop.Admin.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Desktop.Admin.Data.Services;

/// <summary>
/// Class StudentService.
/// Implements the <see cref="Student" />
/// </summary>
/// <seealso cref="Student" />
public class StudentService: BaseService<Student>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StudentService" /> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="cache">The cache.</param>
    public StudentService(IHttpClientFactory clientFactory, IDistributedCache cache) : base(clientFactory, cache)
    {

    }
}