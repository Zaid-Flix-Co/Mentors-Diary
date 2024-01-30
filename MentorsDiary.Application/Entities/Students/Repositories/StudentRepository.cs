using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Students.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Students.Repositories;

/// <summary>
/// Class StudentRepository.
/// Implements the <see cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Students.Domains.Student}" />
/// Implements the <see cref="IStudentRepository" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Students.Domains.Student}" />
/// <seealso cref="IStudentRepository" />
public class StudentRepository : BaseRepository<Domains.Student>, IStudentRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StudentRepository"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public StudentRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {

    }
}