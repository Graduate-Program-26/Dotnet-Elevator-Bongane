using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Interfaces;

public interface IElevatorMovement
{
    /// <summary>Gets the floor the elevator is currently on.</summary>
    int CurrentFloorNumber { get; }
    /// <summary>Gets the current operational state of the elevator.</summary>
    ElevatorState State { get; }

    /// <summary>Gets the direction the elevator is currently travelling.</summary>
    ElevatorDirection Direction { get; }
}