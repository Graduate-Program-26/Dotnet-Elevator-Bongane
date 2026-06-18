namespace ElevatorSim.Domain.Exceptions;

/// <summary>
/// Thrown when a floor request targets a floor that does not exist in the building.
/// </summary>
public class FloorRequestExceedsAvailableFloorsException : Exception
{
    /// <summary>
    /// Initialises a new <see cref="FloorRequestExceedsAvailableFloorsException"/>.
    /// </summary>
    /// <param name="floorRequests">The floor number that was requested.</param>
    /// <param name="availableFloors">The highest floor available in the building.</param>
    public FloorRequestExceedsAvailableFloorsException(int floorRequests, int availableFloors)
        : base($"Floor requests ({floorRequests}) exceed the available floors ({availableFloors}).")
    {
    }
}
