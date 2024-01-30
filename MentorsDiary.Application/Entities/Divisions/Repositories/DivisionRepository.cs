using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Divisions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Divisions.Repositories;

public class DivisionRepository(IMentorsDiaryContext context) : BaseRepository<Division>((DbContext)context), IDivisionRepository;