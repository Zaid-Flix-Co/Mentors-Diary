using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.Files.Domain;

namespace MentorsDiary.Application.Entities.Files.Interfaces;

/// <summary>
/// Interface IServiceFileRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Files.Domain.ServiceFile}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Files.Domain.ServiceFile}" />
public interface IServiceFileRepository : IBaseRepository<ServiceFile>
{
    
}