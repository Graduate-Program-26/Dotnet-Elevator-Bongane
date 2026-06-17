namespace ElevatorSim.Domain.Exceptions;

public class ElevatorExceedsAvailableFloorsException : Exception
{
    public ElevatorExceedsAvailableFloorsException(int elevatorFloorMax, int availableFloors) 
        : base($"Elevator floors ({elevatorFloorMax}) exceed available floors ({availableFloors}).")
    {
        
    }
}