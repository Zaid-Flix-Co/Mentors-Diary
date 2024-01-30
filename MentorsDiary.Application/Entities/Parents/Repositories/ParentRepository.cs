using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.Parents.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Parents.Repositories;

public class ParentRepository(IMentorsDiaryContext context) : BaseRepository<Parent>((DbContext)context), IParentRepository;