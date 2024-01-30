using MentorsDiary.Application.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MentorsDiary.Persistence.DependencyInjection;

public static class PersistenceServiceCollectionExtensions
{
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
