using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;

namespace MentorsDiary.Application.Entities.GroupEventStudents.Interfaces;

/// <summary>
/// Interface IGroupEventStudentRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.GroupEventStudents.Domains.GroupEventStudent}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.GroupEventStudents.Domains.GroupEventStudent}" />
public interface IGroupEventStudentRepository : IBaseRepository<GroupEventStudent>
{
    /// <summary>
    /// Adds the students in group event.
    /// </summary>
    /// <param name="groupEventStudents">The group event students.</param>
    /// <returns>Task.</returns>
    Task AddStudentsInGroupEvent(IEnumerable<GroupEventStudent> groupEventStudents);
}