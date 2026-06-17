namespace ElevatorSim.Domain.Exceptions;

public class FloorRequestExceedsAvailableFloorsException : Exception
{
    public FloorRequestExceedsAvailableFloorsException(int floorRequests, int availableFloors) 
        : base($"Floor requests ({floorRequests}) exceed the available floors ({availableFloors}).")
    {
        
    }
}