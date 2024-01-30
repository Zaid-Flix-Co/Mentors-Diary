using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Students.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Students.Repositories;

public class StudentRepository(IMentorsDiaryContext context) : BaseRepository<Domains.Student>((DbContext)context), IStudentRepository;