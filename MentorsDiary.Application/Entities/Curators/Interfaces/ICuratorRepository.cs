using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.Curators.Domains;

namespace MentorsDiary.Application.Entities.Curators.Interfaces;

/// <summary>
/// Interface ICuratorRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Curators.Domains.Curator}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Curators.Domains.Curator}" />
public interface ICuratorRepository : IBaseRepository<Curator>
{

}