using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Divisions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Divisions.Repositories;

/// <summary>
/// Class DivisionRepository.
/// Implements the <see cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Divisions.Domains.Division}" />
/// Implements the <see cref="IDivisionRepository" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Divisions.Domains.Division}" />
/// <seealso cref="IDivisionRepository" />
public class DivisionRepository : BaseRepository<Division>, IDivisionRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DivisionRepository"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public DivisionRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {

    }
}