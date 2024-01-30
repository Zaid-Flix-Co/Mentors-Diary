using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Application.Entities.Events.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Events.Repositories;

public class EventRepository(IMentorsDiaryContext context) : BaseRepository<Event>((DbContext)context), IEventRepository;
