using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Application.Entities.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Users.Repositories;

public class UserRepository(IMentorsDiaryContext context) : BaseRepository<User>((DbContext)context), IUserRepository;