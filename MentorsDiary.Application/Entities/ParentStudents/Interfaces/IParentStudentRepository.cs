using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.ParentStudents.Domains;

namespace MentorsDiary.Application.Entities.ParentStudents.Interfaces;

/// <summary>
/// Interface IParentStudentRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.ParentStudents.Domains.ParentStudent}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.ParentStudents.Domains.ParentStudent}" />
public interface IParentStudentRepository : IBaseRepository<ParentStudent>
{

}