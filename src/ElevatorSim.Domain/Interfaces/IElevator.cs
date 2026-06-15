using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Interfaces;

public interface IElevator
{
    int FloorNumber { get; }
    ElevatorState State { get; }
    ElevatorDirection Direction { get; }
    int MaxFloor { get; }
    int MinFloor { get; }
    public void AddLoad(ILoad load);
    public void OffLoad(ILoad load);
}