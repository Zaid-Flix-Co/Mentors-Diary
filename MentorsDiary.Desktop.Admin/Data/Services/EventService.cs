using System.Net.Http;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Desktop.Admin.Data.Services.Bases;
using Microsoft.Extensions.Caching.Distributed;

namespace MentorsDiary.Desktop.Admin.Data.Services;

/// <summary>
/// Class EventService.
/// Implements the <see cref="Event" />
/// </summary>
/// <seealso cref="Event" />
public class EventService: BaseService<Event>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventService" /> class.
    /// </summary>
    /// <param name="clientFactory">The client factory.</param>
    /// <param name="cache">The cache.</param>
    public EventService(IHttpClientFactory clientFactory, IDistributedCache cache) : base(clientFactory, cache)
    {

    }
}