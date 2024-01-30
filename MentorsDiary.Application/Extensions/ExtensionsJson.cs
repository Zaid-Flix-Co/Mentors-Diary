using Newtonsoft.Json;

namespace MentorsDiary.Application.Extensions;

/// <summary>
/// Class ExtensionsJson.
/// </summary>
public static class ExtensionsJson
{
    /// <summary>
    /// Deserializes the object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="s">The s.</param>
    /// <returns>System.Nullable&lt;T&gt;.</returns>
    public static T DeserializeObject<T>(this string? s) where T : new()
    {
        return JsonConvert.DeserializeObject<T>(s ?? string.Empty) ?? new T();
    }
}