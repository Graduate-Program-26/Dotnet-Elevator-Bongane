namespace ElevatorSim.Domain.Exceptions;

public class CapacityExceededException : Exception
{
    
    public Guid ElevatorId { get; }
    public int MaxCapacity { get; }
    
    public CapacityExceededException(Guid elevatorId, int maxCapacity)
        : base($"Elevator {elevatorId} cannot exceed capacity of {maxCapacity}.")
    {
        ElevatorId = elevatorId;
        MaxCapacity = maxCapacity;
    }

}