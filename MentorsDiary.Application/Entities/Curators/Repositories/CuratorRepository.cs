using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Curators.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Curators.Repositories;

/// <summary>
/// Class CuratorRepository.
/// Implements the <see cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Curators.Domains.Curator}" />
/// Implements the <see cref="ICuratorRepository" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Curators.Domains.Curator}" />
/// <seealso cref="ICuratorRepository" />
public class CuratorRepository : BaseRepository<Curator>, ICuratorRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CuratorRepository"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public CuratorRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {

    }
}