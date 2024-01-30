using System.Net.Http;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Desktop.Admin.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Desktop.Admin.Data.Services;

/// <summary>
/// Class ParentStudentService.
/// Implements the <see cref="ParentStudent" />
/// </summary>
/// <seealso cref="ParentStudent" />
public class ParentStudentService: BaseService<ParentStudent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentStudentService" /> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="cache">The cache.</param>
    public ParentStudentService(IHttpClientFactory clientFactory, IDistributedCache cache) : base(clientFactory, cache)
    {
    }
}