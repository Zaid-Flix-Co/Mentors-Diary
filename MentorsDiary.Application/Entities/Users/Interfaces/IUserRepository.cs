using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.Users.Domains;

namespace MentorsDiary.Application.Entities.Users.Interfaces;

/// <summary>
/// Interface IUserRepository
/// Extends the <see cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Users.Domains.User}" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Interfaces.IBaseRepository{MentorsDiary.Application.Entities.Users.Domains.User}" />
public interface IUserRepository : IBaseRepository<User>
{
    
}