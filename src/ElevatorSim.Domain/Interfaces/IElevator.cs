using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Interfaces;

public interface IElevator
{
    int Id { get; }
    int Floor { get; }
    ElevatorState State { get; }
    ElevatorDirection Direction { get; }
}