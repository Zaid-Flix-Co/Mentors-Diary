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

namespace MentorsDiary.Application.Context;

/// <summary>
/// Interface IMentorsDiaryContext
/// </summary>
public interface IMentorsDiaryContext
{
    #region ENTITIES

    /// <summary>
    /// Gets or sets the curators.
    /// </summary>
    /// <value>The curators.</value>
    public DbSet<Curator> Curators { get; set; }

    /// <summary>
    /// Gets or sets the divisions.
    /// </summary>
    /// <value>The divisions.</value>
    public DbSet<Division> Divisions { get; set; }

    /// <summary>
    /// Gets or sets the events.
    /// </summary>
    /// <value>The events.</value>
    public DbSet<Event> Events { get; set; }

    /// <summary>
    /// Gets or sets the group events.
    /// </summary>
    /// <value>The group events.</value>
    public DbSet<GroupEvent> GroupEvents { get; set; }

    /// <summary>
    /// Gets or sets the groups.
    /// </summary>
    /// <value>The groups.</value>
    public DbSet<Group> Groups { get; set; }

    /// <summary>
    /// Gets or sets the parents.
    /// </summary>
    /// <value>The parents.</value>
    public DbSet<Parent> Parents { get; set; }

    /// <summary>
    /// Gets or sets the students.
    /// </summary>
    /// <value>The students.</value>
    public DbSet<Student> Students { get; set; }

    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    /// <value>The users.</value>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the group event student.
    /// </summary>
    /// <value>The group event student.</value>
    public DbSet<GroupEventStudent> GroupEventStudents { get; set; }

    /// <summary>
    /// Gets or sets the parent students.
    /// </summary>
    /// <value>The parent students.</value>
    public DbSet<ParentStudent> ParentStudents { get; set; }

    #endregion
}