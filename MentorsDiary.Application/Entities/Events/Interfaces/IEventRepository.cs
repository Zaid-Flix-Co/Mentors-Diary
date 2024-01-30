using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.Events.Domains;

namespace MentorsDiary.Application.Entities.Events.Interfaces;

/// <summary>
/// Interface IEventRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Events.Domains.Event}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Events.Domains.Event}" />
public interface IEventRepository : IBaseRepository<Event>
{

}
