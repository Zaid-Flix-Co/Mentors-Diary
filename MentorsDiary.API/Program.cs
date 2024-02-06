using MentorsDiary.API.Helpers;
using MentorsDiary.Application.DependencyInjection;
using MentorsDiary.Persistence.DependencyInjection;
#if RELEASE
using MentorsDiary.Persistence.DependencyInjection;
#endif

namespace MentorsDiary.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            #if RELEASE
            options.DocumentFilter<PrefixDocumentFilter>();
            #endif
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                corsPolicyBuilder =>
                {
                    corsPolicyBuilder.WithOrigins("http://localhost:7056")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        #region CUSTOM SERVICES

        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);

        #endregion

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseCors("AllowSpecificOrigin");

        app.MapControllers();

        app.Run();
    }
}