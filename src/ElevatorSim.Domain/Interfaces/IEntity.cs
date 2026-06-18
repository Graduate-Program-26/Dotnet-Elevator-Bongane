namespace ElevatorSim.Domain.Interfaces;

/// <summary>
/// Base contract for all identifiable domain entities.
/// </summary>
public interface IEntity
{
    /// <summary>Gets the unique identifier for this entity.</summary>
    public Guid Id { get; }
}
