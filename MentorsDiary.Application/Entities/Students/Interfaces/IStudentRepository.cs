using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.Students.Domains;

namespace MentorsDiary.Application.Entities.Students.Interfaces;

/// <summary>
/// Interface IStudentRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Students.Domains.Student}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Students.Domains.Student}" />
public interface IStudentRepository : IBaseRepository<Student>
{

}