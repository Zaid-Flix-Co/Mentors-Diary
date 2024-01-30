using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.GroupEvents.Domains;

namespace MentorsDiary.Application.Entities.GroupEvents.Interfaces;

/// <summary>
/// Interface IGroupEventRepository
/// Extends the <see cref="IBaseRepository{GroupEvent}" />
/// </summary>
/// <seealso cref="IBaseRepository{GroupEvent}" />
public interface IGroupEventRepository : IBaseRepository<GroupEvent>
{
}