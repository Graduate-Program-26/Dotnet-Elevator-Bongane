using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Application.Strategies;

/// <summary>
/// Dispatch strategy that selects the stationary elevator closest to the requested floor.
/// When distance is equal, the elevator with greater remaining capacity is preferred.
/// Moving or fully-loaded elevators are excluded from selection.
/// </summary>
public class NearestAvailableStrategies : IDispatchStrategy
{
    /// <summary>
    /// Selects the nearest available elevator for the given floor request.
    /// </summary>
    /// <param name="floorRequest">The floor and passenger count being requested.</param>
    /// <param name="elevators">The fleet of elevators to evaluate.</param>
    /// <returns>
    /// The best <see cref="IElevator"/> candidate, or <see langword="null"/> if every elevator
    /// is either moving or at full capacity.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="elevators"/> is empty.</exception>
    public IElevator? DispatchElevator(FloorRequest floorRequest, List<IElevator> elevators)
    {
        if (elevators.Count == 0)
        {
            throw new ArgumentNullException(nameof(elevators), "No elevators available.");
        }

        int floorRequestNumber = floorRequest.FloorNumber;

        var nextBestChoice = elevators
            .Where(elevator => elevator.CurrentCapacity > 0)
            .FirstOrDefault(elevator => elevator.State != ElevatorState.Moving);

        if (nextBestChoice == null)
        {
            return null;
        }

        int minimumFloorDistance = Math.Abs(nextBestChoice.CurrentFloorNumber - floorRequestNumber);
        int highestCurrentCapacityElevator = nextBestChoice.CurrentCapacity;

        foreach (var elevator in elevators)
        {
            if (Math.Abs(elevator.CurrentFloorNumber - floorRequestNumber) <
                minimumFloorDistance
                && elevator.CurrentCapacity >=
                highestCurrentCapacityElevator
                && elevator.State == ElevatorState.Stationary
               )
            {
                nextBestChoice = elevator;
                minimumFloorDistance = Math.Abs(nextBestChoice.CurrentFloorNumber - floorRequestNumber);
                highestCurrentCapacityElevator = nextBestChoice.CurrentCapacity;
            }
        }

        return nextBestChoice;
    }
}
