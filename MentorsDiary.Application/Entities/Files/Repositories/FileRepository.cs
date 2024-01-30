using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Files.Domain;
using MentorsDiary.Application.Entities.Files.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Files.Repositories;

public class FileRepository(IMentorsDiaryContext context) : BaseRepository<ServiceFile>((DbContext)context), IServiceFileRepository;