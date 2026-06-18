using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Interfaces;

/// <summary>
/// Represents an elevator within the building simulation.
/// </summary>
public interface IElevator : IEntity, IElevatorCapacity, IElevatorMovement
{
    /// <summary>
    /// Transitions the elevator to the specified state.
    /// </summary>
    /// <param name="state">The target <see cref="ElevatorState"/>.</param>
    void ChangeState(ElevatorState state);
    
    /// <summary>
    /// Reduces available capacity by <paramref name="load"/> to represent boarded passengers.
    /// </summary>
    /// <param name="load">The number of passengers boarding.</param>
    void Board(int load);

    /// <summary>
    /// Moves the elevator one floor toward <paramref name="targetFloor"/>.
    /// Sets <see cref="State"/> to <see cref="ElevatorState.Stationary"/> upon arrival.
    /// </summary>
    /// <param name="targetFloor">The destination floor.</param>
    void Step(int targetFloor);
}
