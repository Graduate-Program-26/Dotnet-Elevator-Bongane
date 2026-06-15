using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Entities;

public abstract class ElevatorBase : IElevator,
    IEntity
{
    public Guid Id { get; } = new Guid();
    public int CurrentFloorNumber { get; private set; }
    public ElevatorState State { get; private set; }
    public ElevatorDirection Direction { get; private set; }
    public int MinFloor { get; init; }
    public abstract void AddLoad(ILoad load);
    public abstract void OffLoad(ILoad load);

    public int MaxFloor { get; init; }
    
    public ElevatorBase(int minFloor , int maxFloor, int floorNumber = 0, 
        ElevatorState state = ElevatorState.Stationary, 
        ElevatorDirection direction = ElevatorDirection.Idle)
    {
        CurrentFloorNumber = floorNumber;
        MinFloor = minFloor;
        MaxFloor = maxFloor;
        State = state;
        Direction = direction;
    }

    public void ChangeState(ElevatorState state)
    {
        State = state;
    }
    
}