using System.Net.Http;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Desktop.Admin.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Desktop.Admin.Data.Services;

/// <summary>
/// Class DivisionService.
/// Implements the <see cref="Division" />
/// </summary>
/// <seealso cref="Division" />
public class DivisionService: BaseService<Division>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DivisionService" /> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="cache">The cache.</param>
    public DivisionService(IHttpClientFactory clientFactory, IDistributedCache cache) : base(clientFactory, cache)
    {

    }
}