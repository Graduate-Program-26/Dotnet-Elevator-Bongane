namespace ElevatorSim.Domain.Exceptions;

/// <summary>
/// Thrown when an elevator is configured with a maximum floor that exceeds the building's floor count.
/// </summary>
public class ElevatorExceedsAvailableFloorsException : Exception
{
    /// <summary>
    /// Initialises a new <see cref="ElevatorExceedsAvailableFloorsException"/>.
    /// </summary>
    /// <param name="elevatorFloorMax">The maximum floor the elevator is configured for.</param>
    /// <param name="availableFloors">The number of floors the building actually has.</param>
    public ElevatorExceedsAvailableFloorsException(int elevatorFloorMax, int availableFloors)
        : base($"Elevator floors ({elevatorFloorMax}) exceed available floors ({availableFloors}).")
    {
    }
}
