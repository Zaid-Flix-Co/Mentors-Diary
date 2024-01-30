using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Bases.Repositories;
using MentorsDiary.Application.Entities.Files.Domain;
using MentorsDiary.Application.Entities.Files.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Entities.Files.Repositories;

/// <summary>
/// Class FileRepository.
/// Implements the <see cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Files.Domain.ServiceFile}" />
/// Implements the <see cref="IServiceFileRepository" />
/// </summary>
/// <seealso cref="MentorsDiary.Application.Entities.Bases.Repositories.BaseRepository{MentorsDiary.Application.Entities.Files.Domain.ServiceFile}" />
/// <seealso cref="IServiceFileRepository" />
public class FileRepository : BaseRepository<ServiceFile>, IServiceFileRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileRepository"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public FileRepository(IMentorsDiaryContext? context) : base((DbContext)context)
    {
        
    }
}