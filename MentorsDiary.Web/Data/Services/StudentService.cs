using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Web.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Web.Data.Services;

/// <summary>
/// Class StudentService.
/// Implements the <see cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Students.Domains.Student}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Students.Domains.Student}" />
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