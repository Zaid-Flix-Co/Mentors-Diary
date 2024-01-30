using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.ParentStudents.Repositories;

/// <summary>
/// Class ParentStudentRepository.
/// Implements the <see cref="BaseRepository{ParentStudent}" />
/// Implements the <see cref="IParentStudentRepository" />
/// </summary>
/// <seealso cref="BaseRepository{ParentStudent}" />
/// <seealso cref="IParentStudentRepository" />
public class ParentStudentRepository : BaseRepository<ParentStudent>, IParentStudentRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParentStudentRepository"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public ParentStudentRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {
    }
}