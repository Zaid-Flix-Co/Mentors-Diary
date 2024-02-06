using MentorsDiary.Application.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MentorsDiary.Persistence.DependencyInjection;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        bool.TryParse(configuration["Logging:Console:Enabled"], out var consoleEnabled);

        #if DEBUG
        var connectionString = configuration["ConnectionStrings:MentorsDiaryDebugConnection"];
        #elif RELEASE
        var connectionString = configuration["ConnectionStrings:MentorsDiaryReleaseConnection"];
        #endif

        services.AddDbContext<MentorsDiaryDbContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseNpgsql(connectionString);
            options.EnableSensitiveDataLogging();
        });

        services.AddScoped<IMentorsDiaryContext>(provider => provider.GetService<MentorsDiaryDbContext>()!);
        return services;
    }
}
