using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Entities.Users.Domains;
using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Persistence.DataSeeders;

/// <summary>
/// Class DataSeederUser.
/// </summary>
public static class DataSeederUser
{
    /// <summary>
    /// Seeds the data.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    /// <returns>ModelBuilder.</returns>
    public static ModelBuilder SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "user1", Password = "user1", Role = EnumRoles.Administrator, DivisionId = 1});
        modelBuilder.Entity<User>().HasData(new User { Id = 2, Name = "user2", Password = "user2", Role = EnumRoles.DeputyDirector, DivisionId = 2 });
        modelBuilder.Entity<User>().HasData(new User { Id = 3, Name = "user3", Password = "user3", Role = EnumRoles.DeputyDirector, DivisionId = 3 });
        modelBuilder.Entity<User>().HasData(new User { Id = 4, Name = "user4", Password = "user4", Role = EnumRoles.Curator, DivisionId = 2 });
        modelBuilder.Entity<User>().HasData(new User { Id = 5, Name = "user5", Password = "user5", Role = EnumRoles.Curator, DivisionId = 2 });
        modelBuilder.Entity<User>().HasData(new User { Id = 6, Name = "user6", Password = "user6", Role = EnumRoles.Curator, DivisionId = 2 });
        modelBuilder.Entity<User>().HasData(new User { Id = 7, Name = "user7", Password = "user7", Role = EnumRoles.Curator, DivisionId = 3 });
        modelBuilder.Entity<User>().HasData(new User { Id = 8, Name = "user8", Password = "user8", Role = EnumRoles.Curator, DivisionId = 3 });

        return modelBuilder;
    }
}