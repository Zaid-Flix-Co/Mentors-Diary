using System.Net.Http;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Desktop.Admin.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Desktop.Admin.Data.Services;

/// <summary>
/// Class GroupService.
/// Implements the <see cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Groups.Domains.Group}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Groups.Domains.Group}" />
public class GroupService: BaseService<Group>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupService" /> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="cache">The cache.</param>
    public GroupService(IHttpClientFactory clientFactory, IDistributedCache cache) : base(clientFactory, cache)
    {

    }
}