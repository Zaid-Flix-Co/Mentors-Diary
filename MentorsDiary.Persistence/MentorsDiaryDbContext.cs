using MentorsDiary.Application.Bases.BaseUsers;
using MentorsDiary.Application.Bases.Enums;
using MentorsDiary.Application.Bases.Interfaces.ICans;
using MentorsDiary.Application.Bases.Interfaces.IHaves;
using MentorsDiary.Application.Context;
using MentorsDiary.Application.Entities.Curators.Domains;
using MentorsDiary.Application.Entities.Divisions.Domains;
using MentorsDiary.Application.Entities.EntityChangelog;
using MentorsDiary.Application.Entities.Events.Domains;
using MentorsDiary.Application.Entities.GroupEvents.Domains;
using MentorsDiary.Application.Entities.GroupEventStudents.Domains;
using MentorsDiary.Application.Entities.Groups.Domains;
using MentorsDiary.Application.Entities.Parents.Domains;
using MentorsDiary.Application.Entities.ParentStudents.Domains;
using MentorsDiary.Application.Entities.Students.Domains;
using MentorsDiary.Application.Entities.Users.Domains;
using MentorsDiary.Persistence.DataSeeders;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using static System.String;

namespace MentorsDiary.Persistence;

/// <summary>
/// Class MentorsDiaryDbContext.
/// Implements the <see cref="DbContext" />
/// Implements the <see cref="IMentorsDiaryContext" />
/// </summary>
/// <seealso cref="DbContext" />
/// <seealso cref="IMentorsDiaryContext" />
public class MentorsDiaryDbContext : DbContext, IMentorsDiaryContext
{

    /// <summary>
    /// The entity change log
    /// </summary>
    private readonly ContextMentorsDiaryEntityChangeLog _entityChangeLog;

    /// <summary>
    /// The service scope factory
    /// </summary>
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<MentorsDiaryDbContext> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MentorsDiaryDbContext" /> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="entityChangeLog">The entity change log.</param>
    /// <param name="serviceScopeFactory">The service scope factory.</param>
    /// <param name="logger">The logger.</param>
    public MentorsDiaryDbContext(DbContextOptions<MentorsDiaryDbContext> options, ContextMentorsDiaryEntityChangeLog entityChangeLog, IServiceScopeFactory serviceScopeFactory, ILogger<MentorsDiaryDbContext> logger) : base(options)
    {
        _entityChangeLog = entityChangeLog;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    /// <summary>
    /// Override this method to further configure the model that was discovered by convention from the entity types
    /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
    /// and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
    /// define extension methods on this object that allow you to configure aspects of the model that are specific
    /// to a given database.</param>
    /// <remarks><para>
    /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
    /// then this method will not be run. However, it will still run when creating a compiled model.
    /// </para>
    /// <para>
    /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
    /// examples.
    /// </para></remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder = DataSeederUser.SeedData(modelBuilder);
        DataSeederDivision.SeedData(modelBuilder);
    }

    /// <summary>
    /// Override this method to configure the database (and other options) to be used for this context.
    /// This method is called for each instance of the context that is created.
    /// The base implementation does nothing.
    /// </summary>
    /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
    /// typically define extension methods on this object that allow you to configure the context.</param>
    /// <remarks><para>
    /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
    /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
    /// the options have already been set, and skip some or all of the logic in
    /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
    /// </para>
    /// <para>
    /// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    /// for more information and examples.
    /// </para></remarks>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder
            .ConfigureWarnings(x => x.Ignore(RelationalEventId.MultipleCollectionIncludeWarning));
    }

    /// <summary>
    /// Gets the entity change log.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>EntityChangeLog.</returns>
    private EntityChangelog GetEntityChangeLog(EntityEntry entity)
    {
        var scope = _serviceScopeFactory.CreateScope();
        var userProvider = Empty;
        var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

        var changeType = EntityChangeTypes.None;
        switch (entity.State)
        {
            case EntityState.Detached:
            case EntityState.Unchanged:
                changeType = EntityChangeTypes.None;
                break;
            case EntityState.Added:
                changeType = EntityChangeTypes.Insert;
                break;
            case EntityState.Modified:
                changeType = EntityChangeTypes.Update;
                break;
            case EntityState.Deleted:
                changeType = EntityChangeTypes.Delete;
                break;
        }

        var user = Empty;
        var userName = Empty;
        var userDescription = Empty;
        var ipAddress = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString();

        var entityChangeLog = new EntityChangelog
        {
            ChangeTime = DateTime.Now,
            EntityChangeTypes = changeType,
            EntityId = string.Join(',', entity.Metadata.FindPrimaryKey()?.Properties.Select(prop => prop.PropertyInfo?.GetValue(entity.Entity))!),
            EntityName = entity.Metadata.ClrType.Name,
            UserDescription = userDescription,
            UserIpAddress = ipAddress!,
            UserName = userName
        };

        return entityChangeLog;
    }

    /// <summary>
    /// Gets the properties change log.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="entityChangeLog">The entity change log.</param>
    private void GetPropertiesChangeLog(EntityEntry entity, EntityChangelog entityChangeLog)
    {
        var excludeProperties = new List<string> { nameof(BaseUserCU.DateUpdated) };

        foreach (var prop in entity.Properties)
        {
            if (excludeProperties.Contains(prop.Metadata.Name))
                continue;

            if (prop.Metadata.IsPrimaryKey() && entity.Properties.Any(p => !p.Metadata.IsPrimaryKey()))
                continue;

            if (entity.State == EntityState.Modified && !prop.IsModified)
                continue;

            if (entity.State != EntityState.Deleted && entity.State != EntityState.Added && prop.OriginalValue?.ToString() == prop.CurrentValue?.ToString())
                continue;

            var displayName = Empty;
            var currentValueDescription = Empty;
            var oldValueDescription = Empty;

            var displayNameAttr = prop.Metadata.PropertyInfo?.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>()
                .SingleOrDefault();

            if (displayNameAttr != null)
                displayName = displayNameAttr.Name;

            if (prop.Metadata.IsForeignKey())
            {
                var foreignKey = prop.Metadata.GetContainingForeignKeys().FirstOrDefault();

                if (foreignKey?.DependentToPrincipal == null)
                    continue;

                var currentValue = prop.CurrentValue;
                var originalValue = prop.OriginalValue;

                var reference = entity.Reference(foreignKey.DependentToPrincipal.Name);

                prop.CurrentValue = originalValue;
                reference.Load();

                if (reference.TargetEntry == null || !typeof(IHaveDescription).IsAssignableFrom(reference.TargetEntry.Entity.GetType()))
                {
                    prop.CurrentValue = currentValue;
                    continue;
                }

                oldValueDescription = ((IHaveDescription)reference.TargetEntry.Entity).Description;

                prop.CurrentValue = currentValue;
                reference.Load();

                if (reference.TargetEntry == null || !typeof(IHaveDescription).IsAssignableFrom(reference.TargetEntry.Entity.GetType()))
                {
                    prop.CurrentValue = currentValue;
                    continue;
                }

                currentValueDescription = ((IHaveDescription)reference.TargetEntry.Entity).Description;
            }
            else
            {
                var currentValue = prop.CurrentValue;
                var originalValue = prop.OriginalValue;

                prop.CurrentValue = originalValue;

                if (prop.EntityEntry == null || !typeof(IHaveDescription).IsAssignableFrom(prop.EntityEntry.Entity.GetType()))
                {
                    prop.CurrentValue = currentValue;
                    continue;
                }

                oldValueDescription = ((IHaveDescription)prop.EntityEntry.Entity).Description;

                prop.CurrentValue = currentValue;

                if (prop.EntityEntry == null || !typeof(IHaveDescription).IsAssignableFrom(prop.EntityEntry.Entity.GetType()))
                {
                    prop.CurrentValue = currentValue;
                    continue;
                }

                currentValueDescription = ((IHaveDescription)prop.EntityEntry.Entity).Description;
            }

            var changeLogProperty = new EntityPropertyChangeLog
            {
                EntityChangeLog = entityChangeLog,
                OldValue = (entity.State == EntityState.Added ? "" : prop.OriginalValue?.ToString())!,
                NewValue = prop.CurrentValue?.ToString()!,
                PropertyName = prop.Metadata.Name,
                DisplayName = displayName!,
                NewValueDescription = currentValueDescription,
                OldValueDiscription = oldValueDescription
            };

            _entityChangeLog.EntityPropertyChangeLogs.Add(changeLogProperty);
        }
    }

    private void LogEntityChanges()
    {
        ChangeTracker.DetectChanges();

        try
        {
            foreach (var entity in ChangeTracker.Entries().Where(w => w.State is EntityState.Added or EntityState.Modified or EntityState.Deleted))
            {
                if (!typeof(ICanEntityChangeLog).IsAssignableFrom(entity.Metadata.ClrType)) continue;

                var entityChangeLog = GetEntityChangeLog(entity);
                _entityChangeLog.EntityChangeLogs.Add(entityChangeLog);

                GetPropertiesChangeLog(entity, entityChangeLog);

                _entityChangeLog.SaveChanges();
            }
        }
        catch (Exception e)
        {
            _logger.LogError("{Date} {Key} {Error}", DateTime.Now, "EntityChangeLog", e.ToString());
        }
    }

    private async Task LogEntityChangesAsync()
    {
        ChangeTracker.DetectChanges();

        try
        {
            foreach (var entity in ChangeTracker.Entries().Where(w => w.State is EntityState.Added or EntityState.Modified or EntityState.Deleted))
            {
                if (typeof(ICanEntityChangeLog).IsAssignableFrom(entity.Metadata.ClrType))
                {
                    var entityChangeLog = GetEntityChangeLog(entity);
                    await _entityChangeLog.EntityChangeLogs.AddAsync(entityChangeLog);

                    GetPropertiesChangeLog(entity, entityChangeLog);

                    await _entityChangeLog.SaveChangesAsync();
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError("{Date} {Key} {Error}", DateTime.Now, "EntityChangeLog", e.ToString());
        }
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        LogEntityChanges();

        try
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        finally
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
    {
        await LogEntityChangesAsync();

        try
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        finally
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }

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