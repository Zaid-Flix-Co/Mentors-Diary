using Microsoft.EntityFrameworkCore;

namespace MentorsDiary.Application.Extensions;

/// <summary>
/// Class ExtensionsEntity.
/// </summary>
public static class ExtensionsEntity
{
    /// <summary>
    /// Clears the specified database set.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dbSet">The database set.</param>
    public static void Clear<T>(this DbSet<T> dbSet) where T : class
    {
        if (dbSet.Any())
        {
            dbSet.RemoveRange(dbSet.ToList());
        }
    }
}