using MentorsDiary.Application.Bases.Enums;

namespace MentorsDiary.Application.Entities.EntityChangelog;

/// <summary>
/// Class EntityChangelog.
/// </summary>
public class EntityChangelog
{
    /// <summary>
    /// Идентификатор записи в БД.
    /// </summary>
    /// <value>The identifier.</value>
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор сущности в БД.
    /// </summary>
    /// <value>The entity identifier.</value>
    public string? EntityId { get; set; }

    /// <summary>
    /// Имя сущности.
    /// </summary>
    /// <value>The name of the entity.</value>
    public string? EntityName { get; set; }

    /// <summary>
    /// Тип изменения сущности.
    /// </summary>
    /// <value>The type of the entity change.</value>
    public EntityChangeTypes EntityChangeTypes { get; set; }

    /// <summary>
    /// Время изменения.
    /// </summary>
    /// <value>The change time.</value>
    public DateTime ChangeTime { get; set; }

    /// <summary>
    /// Пользователь, который внес изменения.
    /// </summary>
    /// <value>The name of the user.</value>
    public string? UserName { get; set; }

    /// <summary>
    /// IP address пользователя.
    /// </summary>
    /// <value>The user IP address.</value>
    public string? UserIpAddress { get; set; }

    /// <summary>
    /// Описание пользователя(ID Employee).
    /// </summary>
    /// <value>The user description.</value>
    public string? UserDescription { get; set; }

    /// <summary>
    /// Gets or sets the entity property change logs.
    /// </summary>
    /// <value>The entity property change logs.</value>
    public virtual IEnumerable<EntityPropertyChangeLog>? EntityPropertyChangeLogs { get; set; }
}