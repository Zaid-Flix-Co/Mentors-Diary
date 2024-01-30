using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Groups.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Groups.Repositories;

public class GroupRepository(IMentorsDiaryContext context) : BaseRepository<Domains.Group>((DbContext)context), IGroupRepository;