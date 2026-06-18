namespace ElevatorSim.Application.Models;

/// <summary>
/// Captures the outcome of a single dispatch cycle for a floor request.
/// </summary>
/// <param name="PassengersBoarded">The number of passengers successfully boarded across all dispatched elevators.</param>
/// <param name="PassengersWaiting">The number of passengers still waiting after the dispatch cycle completed.</param>
public record ElevatorDispatchResult(int PassengersBoarded, int PassengersWaiting);
