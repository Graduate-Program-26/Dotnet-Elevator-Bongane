using ElevatorSim.Domain.Enums;

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
    void ElevatorDispatched(int elevatorId, int fromFloor, int toFloor);

    /// <summary>
    /// Called once per floor stepped while the elevator is in transit.
    /// </summary>
    void ElevatorMoved();

    /// <summary>
    /// Called each time a passenger boards the elevator during a boarding cycle.
    /// </summary>
    /// <param name="passengersBoarded">The number of passengers that just boarded.</param>
    /// <param name="elevatorCapacity">The remaining capacity after boarding.</param>
    void PassengersBoarded( int passengersBoarded, int elevatorCapacity);

    /// <summary>
    /// Called when the elevator transitions to a new <see cref="ElevatorSim.Domain.Enums.ElevatorState"/>.
    /// </summary>
    /// <param name="elevatorId">The identifier of the elevator whose state changed.</param>
    /// /// <param name="elevatorState">The changed state of the elevator.</param>
    public void ElevatorChangedState(int elevatorId, ElevatorState elevatorState);
}
