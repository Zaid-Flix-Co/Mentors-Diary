using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Application.Entities.Events.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Events.Repositories;

/// <summary>
/// Class EventRepository.
/// Implements the <see cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Events.Domains.Event}" />
/// Implements the <see cref="IEventRepository" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Events.Domains.Event}" />
/// <seealso cref="IEventRepository" />
public class EventRepository : BaseRepository<Event>, IEventRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventRepository"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public EventRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {

    }
}
