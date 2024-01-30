using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.Groups.Domains;

namespace MentorsDiary.Application.Entities.Groups.Interfaces;

/// <summary>
/// Interface IGroupRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Groups.Domains.Group}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Groups.Domains.Group}" />
public interface IGroupRepository : IBaseRepository<Group>
{
    
}