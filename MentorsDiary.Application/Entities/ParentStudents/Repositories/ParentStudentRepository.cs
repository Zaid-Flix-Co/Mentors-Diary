using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.ParentStudents.Repositories;

public class ParentStudentRepository(IMentorsDiaryContext context) : BaseRepository<ParentStudent>((DbContext)context), IParentStudentRepository;