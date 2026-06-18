namespace ElevatorSim.Domain.Exceptions;

/// <summary>
/// Thrown when adding an elevator would exceed the building's maximum elevator count.
/// </summary>
public class BuildingElevatorCapacityExceededException : Exception
{
    /// <summary>
    /// Initialises a new <see cref="BuildingElevatorCapacityExceededException"/>.
    /// </summary>
    /// <param name="elevatorBuildingCapacity">The maximum number of elevators the building supports.</param>
    public BuildingElevatorCapacityExceededException(int elevatorBuildingCapacity)
        : base($"Building only has capacity for {elevatorBuildingCapacity} elevators.")
    {
    }
}
