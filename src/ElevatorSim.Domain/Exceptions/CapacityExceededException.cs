namespace ElevatorSim.Domain.Exceptions;

/// <summary>
/// Thrown when a boarding attempt would exceed an elevator's maximum passenger capacity.
/// </summary>
public class CapacityExceededException : Exception
{
    /// <summary>Gets the identifier of the elevator that is at capacity.</summary>
    public Guid ElevatorId { get; }

    /// <summary>Gets the maximum capacity that was exceeded.</summary>
    public int MaxCapacity { get; }

    /// <summary>
    /// Initialises a new <see cref="CapacityExceededException"/>.
    /// </summary>
    /// <param name="elevatorId">The identifier of the elevator at capacity.</param>
    /// <param name="maxCapacity">The capacity limit that was reached.</param>
    public CapacityExceededException(Guid elevatorId, int maxCapacity)
        : base($"Elevator {elevatorId} cannot exceed capacity of {maxCapacity}.")
    {
        ElevatorId = elevatorId;
        MaxCapacity = maxCapacity;
    }
}
