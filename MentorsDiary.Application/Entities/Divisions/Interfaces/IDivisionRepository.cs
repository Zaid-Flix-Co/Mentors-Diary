using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.Divisions.Domains;

namespace MentorsDiary.Application.Entities.Divisions.Interfaces;

/// <summary>
/// Interface IDivisionRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Divisions.Domains.Division}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Divisions.Domains.Division}" />
public interface IDivisionRepository : IBaseRepository<Division>
{
    
}