using MentorsDiary.Application.DependencyInjection;
using MentorsDiary.Persistence.DependencyInjection;

namespace MentorsDiary.API;

/// <summary>
/// Class Program.
/// </summary>
public class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:7056")
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