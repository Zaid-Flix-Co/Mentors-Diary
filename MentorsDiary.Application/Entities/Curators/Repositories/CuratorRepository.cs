using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Curators.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Curators.Repositories;

public class CuratorRepository(IMentorsDiaryContext context) : BaseRepository<Curator>((DbContext)context), ICuratorRepository;