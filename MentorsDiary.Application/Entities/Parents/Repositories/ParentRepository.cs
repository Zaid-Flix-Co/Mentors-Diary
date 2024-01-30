using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Parents.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Parents.Repositories;

/// <summary>
/// Class ParentRepository.
/// Implements the <see cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Parents.Domains.Parent}" />
/// Implements the <see cref="IParentRepository" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Parents.Domains.Parent}" />
/// <seealso cref="IParentRepository" />
public class ParentRepository : BaseRepository<Domains.Parent>, IParentRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentRepository"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public ParentRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {

    }
}