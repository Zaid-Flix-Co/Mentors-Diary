using MentorsDiary.Application.Entities.Divisions.Domains;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Persistence.DataSeeders;

/// <summary>
/// Class DataSeederTaskStatus.
/// </summary>
public static class DataSeederDivision
{
    /// <summary>
    /// Seeds the data.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    /// <returns>ModelBuilder.</returns>
    public static ModelBuilder SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Division>().HasData(new Division { Id = 1, Name = "Division1" });
        modelBuilder.Entity<Division>().HasData(new Division { Id = 2, Name = "Division2" });
        modelBuilder.Entity<Division>().HasData(new Division { Id = 3, Name = "Division3" });

        return modelBuilder;
    }
}