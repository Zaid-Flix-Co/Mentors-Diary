using System.Net.Http;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Desktop.Admin.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Desktop.Admin.Data.Services;

/// <summary>
/// Class ParentService.
/// Implements the <see cref="Parent" />
/// </summary>
/// <seealso cref="Parent" />
public class ParentService: BaseService<Parent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentService" /> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="cache">The cache.</param>
    public ParentService(IHttpClientFactory clientFactory, IDistributedCache cache) : base(clientFactory, cache)
    {

    }
}