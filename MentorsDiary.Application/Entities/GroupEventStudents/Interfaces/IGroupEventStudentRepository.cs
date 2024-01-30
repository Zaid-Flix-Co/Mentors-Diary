using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;

namespace MentorsDiary.Application.Entities.GroupEventStudents.Interfaces;

public interface IGroupEventStudentRepository : IBaseRepository<GroupEventStudent>
{
    Task AddStudentsInGroupEvent(IEnumerable<GroupEventStudent> groupEventStudents);
}