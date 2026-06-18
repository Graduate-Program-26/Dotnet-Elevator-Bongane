using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Interfaces;

/// <summary>
/// Represents an elevator within the building simulation.
/// </summary>
public interface IElevator : IEntity
{
    /// <summary>Gets the floor the elevator is currently on.</summary>
    int CurrentFloorNumber { get; }

    /// <summary>Gets the number of additional passengers the elevator can still accept.</summary>
    int CurrentCapacity { get; }

    /// <summary>Gets the maximum number of passengers this elevator can carry.</summary>
    int MaxCapacity { get; }

    /// <summary>Gets the current operational state of the elevator.</summary>
    ElevatorState State { get; }

    /// <summary>Gets the direction the elevator is currently travelling.</summary>
    ElevatorDirection Direction { get; }

    /// <summary>Gets the highest floor this elevator can reach.</summary>
    int MaxFloor { get; }

    /// <summary>Gets the lowest floor this elevator can reach.</summary>
    int MinFloor { get; }
    
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
