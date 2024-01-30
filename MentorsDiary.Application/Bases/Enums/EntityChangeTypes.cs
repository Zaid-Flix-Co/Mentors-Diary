namespace MentorsDiary.Application.Bases.Enums;

/// <summary>
/// Enum EntityChangeTypes
/// </summary>
public enum EntityChangeTypes
{
    /// <summary>
    /// Без изменений
    /// </summary>
    None = 0,

    /// <summary>
    /// Добавленеи в БД
    /// </summary>
    Insert = 1,

    /// <summary>
    /// Обновление в БД
    /// </summary>
    Update = 2,

    /// <summary>
    /// Удаление из БД
    /// </summary>
    Delete = 3
}