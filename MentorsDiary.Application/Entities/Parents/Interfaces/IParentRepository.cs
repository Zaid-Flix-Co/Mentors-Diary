using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.Parents.Domains;

namespace MentorsDiary.Application.Entities.Parents.Interfaces;

/// <summary>
/// Interface IParentRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Parents.Domains.Parent}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Parents.Domains.Parent}" />
public interface IParentRepository : IBaseRepository<Parent>
{
    
}