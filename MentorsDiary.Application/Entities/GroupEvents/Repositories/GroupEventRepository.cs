using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.GroupEvents.Repositories;

public class GroupEventRepository(IMentorsDiaryContext context) : BaseRepository<GroupEvent>((DbContext)context), IGroupEventRepository;