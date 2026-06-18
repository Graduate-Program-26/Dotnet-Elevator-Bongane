namespace ElevatorSim.Application.Interfaces;

/// <summary>
/// Receives progress notifications during a dispatch cycle, allowing the presentation
/// layer to update the UI as the elevator moves and passengers board.
/// </summary>
public interface IDispatchProgress
{
    /// <summary>
    /// Called when an elevator has been selected and is about to depart for the requested floor.
    /// </summary>
    /// <param name="elevatorId">The identifier of the dispatched elevator.</param>
    /// <param name="fromFloor">The floor the elevator is departing from.</param>
    /// <param name="toFloor">The floor the elevator is heading to.</param>
    void ElevatorDispatched(Guid elevatorId, int fromFloor, int toFloor);

    /// <summary>
    /// Called once per floor stepped while the elevator is in transit.
    /// </summary>
    /// <param name="elevatorId">The identifier of the moving elevator.</param>
    /// <param name="currentFloor">The floor the elevator has just reached.</param>
    void ElevatorMoved(Guid elevatorId, int currentFloor);

    /// <summary>
    /// Called when the elevator has arrived at the requested floor and stopped.
    /// </summary>
    /// <param name="elevatorId">The identifier of the elevator that arrived.</param>
    /// <param name="floor">The floor the elevator arrived at.</param>
    void Arrived(Guid elevatorId, int floor);

    /// <summary>
    /// Called each time a passenger boards the elevator during a boarding cycle.
    /// </summary>
    /// <param name="elevatorId">The identifier of the elevator being boarded.</param>
    /// <param name="passengersBoarded">The number of passengers that just boarded.</param>
    /// <param name="elevatorCapacity">The remaining capacity after boarding.</param>
    void PassengersBoarded(Guid elevatorId, int passengersBoarded, int elevatorCapacity);

    /// <summary>
    /// Called when the elevator transitions to a new <see cref="ElevatorSim.Domain.Enums.ElevatorState"/>.
    /// </summary>
    /// <param name="elevatorId">The identifier of the elevator whose state changed.</param>
    void ElevatorChangedState(Guid elevatorId);
}
