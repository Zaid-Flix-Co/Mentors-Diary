namespace MentorsDiary.Application.Entities.EntityChangelog;

/// <summary>
/// Class EntityPropertyChangeLog.
/// </summary>
public class EntityPropertyChangeLog
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    /// <value>The identifier.</value>
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор лога самой сущности.
    /// </summary>
    /// <value>The entity change log identifier.</value>
    public long? EntityChangeLogId { get; set; }

    /// <summary>
    /// Лог самой сущности.
    /// </summary>
    /// <value>The entity change log.</value>
    public virtual EntityChangelog? EntityChangeLog { get; set; }

    /// <summary>
    /// Имя свойства.
    /// </summary>
    /// <value>The name of the property.</value>
    public string? PropertyName { get; set; }

    /// <summary>
    /// Отображаемое имя свойства.
    /// </summary>
    /// <value>The display name.</value>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Старое значение.
    /// </summary>
    /// <value>The old value.</value>
    public string? OldValue { get; set; }

    /// <summary>
    /// Новое значение.
    /// </summary>
    /// <value>The new value.</value>
    public string? NewValue { get; set; }

    /// <summary>
    /// Описание старого значения.
    /// </summary>
    /// <value>The old value discription.</value>
    public string? OldValueDiscription { get; set; }

    /// <summary>
    /// Описание нового значения.
    /// </summary>
    /// <value>The new value description.</value>
    public string? NewValueDescription { get; set; }
}