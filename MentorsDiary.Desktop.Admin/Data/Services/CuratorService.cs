using System.Net.Http;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Desktop.Admin.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Desktop.Admin.Data.Services;

/// <summary>
/// Class CuratorService.
/// Implements the <see cref="Curator" />
/// </summary>
/// <seealso cref="Curator" />
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