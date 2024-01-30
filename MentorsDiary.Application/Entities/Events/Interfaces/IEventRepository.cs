using MentorsDiary.Application.Entities.Bases.Interfaces;
using MentorsDiary.Application.Entities.Events.Domains;

namespace MentorsDiary.Application.Entities.Events.Interfaces;

public interface IEventRepository : IBaseRepository<Event>;