namespace Blazor.Learner.Shared.Models;

/// <summary>
/// Class BaseEntity.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public virtual int Id { get; set; }
}