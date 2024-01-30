using MentorsDiary.Application.Entities.Curators.Interfaces;
using MentorsDiary.Application.Entities.Curators.Repositories;
using MentorsDiary.Application.Entities.Divisions.Interfaces;
using MentorsDiary.Application.Entities.Divisions.Repositories;
using MentorsDiary.Application.Entities.Events.Interfaces;
using MentorsDiary.Application.Entities.Events.Repositories;
using MentorsDiary.Application.Entities.Files.Interfaces;
using MentorsDiary.Application.Entities.Files.Repositories;
using MentorsDiary.Application.Entities.GroupEvents.Interfaces;
using MentorsDiary.Application.Entities.GroupEvents.Repositories;
using MentorsDiary.Application.Entities.GroupEventStudents.Interfaces;
using MentorsDiary.Application.Entities.GroupEventStudents.Repositories;
using MentorsDiary.Application.Entities.Groups.Interfaces;
using MentorsDiary.Application.Entities.Groups.Repositories;
using MentorsDiary.Application.Entities.Parents.Interfaces;
using MentorsDiary.Application.Entities.Parents.Repositories;
using MentorsDiary.Application.Entities.ParentStudents.Interfaces;
using MentorsDiary.Application.Entities.ParentStudents.Repositories;
using MentorsDiary.Application.Entities.Students.Interfaces;
using MentorsDiary.Application.Entities.Students.Repositories;
using MentorsDiary.Application.Entities.Users.Interfaces;
using MentorsDiary.Application.Entities.Users.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MentorsDiary.Application.DependencyInjection;

/// <summary>
/// Class ApplicationServiceCollectionExtensions.
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    /// <summary>
    /// Adds the persistence.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        #region ПОДКЛЮЧЕНИЕ РЕПОЗИТОРИЕВ

        serviceCollection.AddScoped<ICuratorRepository, CuratorRepository>();
        serviceCollection.AddScoped<IDivisionRepository, DivisionRepository>();
        serviceCollection.AddScoped<IEventRepository, EventRepository>();
        serviceCollection.AddScoped<IServiceFileRepository, FileRepository>();
        serviceCollection.AddScoped<IGroupEventRepository, GroupEventRepository>();
        serviceCollection.AddScoped<IGroupEventStudentRepository, GroupEventStudentRepository>();
        serviceCollection.AddScoped<IGroupRepository, GroupRepository>();
        serviceCollection.AddScoped<IParentRepository, ParentRepository>();
        serviceCollection.AddScoped<IParentStudentRepository, ParentStudentRepository>();
        serviceCollection.AddScoped<IStudentRepository, StudentRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        #endregion

        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddMemoryCache();

        return serviceCollection;
    }
}