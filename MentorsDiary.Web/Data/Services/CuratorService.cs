using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Web.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Web.Data.Services;

/// <summary>
/// Class CuratorService.
/// Implements the <see cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Curators.Domains.Curator}" />
/// </summary>
/// <seealso cref="MentorsDiary.Web.Services.Bases.BaseService{MentorsDiary.Application.Entities.Curators.Domains.Curator}" />
public class CuratorService: BaseService<Curator>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CuratorService" /> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="cache">The cache.</param>
    public CuratorService(IHttpClientFactory clientFactory, IDistributedCache cache) : base(clientFactory, cache)
    {

    }
}