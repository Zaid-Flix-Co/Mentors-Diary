using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MentorsDiary.Persistence;

public class MentorsDiaryDbContext(
    DbContextOptions<MentorsDiaryDbContext> options)
    : DbContext(options), IMentorsDiaryContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.ConfigureWarnings(x => x.Ignore(RelationalEventId.MultipleCollectionIncludeWarning));
    }
    
    #region ENTITIES

    public DbSet<Curator> Curators { get; set; } = null!;

    public DbSet<Division> Divisions { get; set; } = null!;

    public DbSet<Event> Events { get; set; } = null!;

    public DbSet<GroupEvent> GroupEvents { get; set; } = null!;

    public DbSet<Group> Groups { get; set; } = null!;

    public DbSet<Parent> Parents { get; set; } = null!;

    public DbSet<Student> Students { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<GroupEventStudent> GroupEventStudents { get; set; } = null!;

    public DbSet<ParentStudent> ParentStudents { get; set; } = null!;

    #endregion
}