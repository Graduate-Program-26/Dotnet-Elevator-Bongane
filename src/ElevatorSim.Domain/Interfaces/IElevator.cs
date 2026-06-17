using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Interfaces;

public interface IElevator : IEntity
{
    int CurrentFloorNumber { get; }
    int CurrentCapacity { get; }
    int MaxCapacity { get; }
    ElevatorState State { get; }
    ElevatorDirection Direction { get; }
    int MaxFloor { get; }
    int MinFloor { get; }
    public void AddLoad(ILoad load);
    public void OffLoad(ILoad load);
    public void ChangeState(ElevatorState state);
    public void ChangeCurrentFloorNumber(int currentFloorNumber);
    public void ChangeCurrentCapacity(int currentCapacity);
    public void Board(int load);
    public void Step(int targetFloor);
    public void ShowStatus();
}