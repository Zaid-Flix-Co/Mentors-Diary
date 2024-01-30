using MentorsDiary.Application.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MentorsDiary.Persistence.DependencyInjection;

/// <summary>
/// Class PersistenceServiceCollectionExtensions.
/// </summary>
public static class PersistenceServiceCollectionExtensions
{
    /// <summary>
    /// Adds the persistence.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        bool.TryParse(configuration["Logging:Console:Enabled"], out var consoleEnabled);

        var connectionString = configuration["DbConnection"];
        
        services.AddDbContext<MentorsDiaryDbContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
        });

        var logConnectionString = configuration["DbLogConnection"];
        if (logConnectionString != null)
        {
            services.AddDbContext<ContextMentorsDiaryEntityChangeLog>(opts =>
            {
                if (consoleEnabled)
                {
                    opts.UseLoggerFactory(LoggerFactory.Create(builder =>
                    {
                        builder.AddConsole();
                    }));
                    opts.EnableSensitiveDataLogging();
                    opts.EnableDetailedErrors();
                }

                opts.UseSqlServer(logConnectionString, o =>
                {
                    o.CommandTimeout(600);
                    o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            });
        }
        
        services.AddScoped<IMentorsDiaryContext>(provider => provider.GetService<MentorsDiaryDbContext>()!);
        return services;
    }
}
