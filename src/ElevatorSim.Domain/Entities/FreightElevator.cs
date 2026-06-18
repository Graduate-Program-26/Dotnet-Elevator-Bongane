using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Entities;

public class FreightElevator : ElevatorBase
{
    public FreightElevator(int minFloor, int maxFloor, int maxCapacity, int floorNumber, 
        ElevatorState state = ElevatorState.Stationary, ElevatorDirection direction = ElevatorDirection.Idle) 
        : base(minFloor, maxFloor, maxCapacity, floorNumber, state, direction)
    {
    }
}