using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Application.Entities.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Users.Repositories;

/// <summary>
/// Class UserRepository.
/// Implements the <see cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Users.Domains.User}" />
/// Implements the <see cref="IUserRepository" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Users.Domains.User}" />
/// <seealso cref="IUserRepository" />
public class UserRepository : BaseRepository<User>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public UserRepository(IMentorsDiaryContext context) : base((DbContext)context)
    {

    }
}