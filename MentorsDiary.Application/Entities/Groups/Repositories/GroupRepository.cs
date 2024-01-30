using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Groups.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Groups.Repositories;

/// <summary>
/// Class GroupRepository.
/// Implements the <see cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Groups.Domains.Group}" />
/// Implements the <see cref="IGroupRepository" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Groups.Domains.Group}" />
/// <seealso cref="IGroupRepository" />
public class GroupRepository : BaseRepository<Domains.Group>, IGroupRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupRepository"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public GroupRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {

    }
}