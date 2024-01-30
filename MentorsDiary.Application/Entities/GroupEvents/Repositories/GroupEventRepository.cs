using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.GroupEvents.Repositories;

/// <summary>
/// Class GroupEventRepository.
/// Implements the <see cref="BaseRepository{GroupEvent}" />
/// Implements the <see cref="IGroupEventRepository" />
/// </summary>
/// <seealso cref="BaseRepository{GroupEvent}" />
/// <seealso cref="IGroupEventRepository" />
public class GroupEventRepository : BaseRepository<GroupEvent>, IGroupEventRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupEventRepository" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public GroupEventRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {
    }
}