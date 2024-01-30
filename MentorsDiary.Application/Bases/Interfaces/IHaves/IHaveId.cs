using MentorsDiary.Application.Bases.Interfaces.ICans;

namespace MentorsDiary.Application.Bases.Interfaces.IHaves;

/// <summary>
/// Interface IHaveId
/// Extends the <see cref="ICanEntityChangeLog" />
/// Extends the <see cref="MentorsDiary.Application.Bases.Interfaces.IHaves.IHaveDescription" />
/// </summary>
/// <seealso cref="ICanEntityChangeLog" />
/// <seealso cref="MentorsDiary.Application.Bases.Interfaces.IHaves.IHaveDescription" />
public interface IHaveId : ICanEntityChangeLog, IHaveDescription
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    int Id { get; set; }
}