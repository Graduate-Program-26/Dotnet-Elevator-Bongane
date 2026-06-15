namespace ElevatorSim.Domain.Exceptions;

public class BuildingElevatorCapacityExceededException:Exception
{
    public BuildingElevatorCapacityExceededException(int elevatorBuildingCapacity) 
        : base($"Building only has capacity for {elevatorBuildingCapacity} elevators.")
    {
        
    }
}