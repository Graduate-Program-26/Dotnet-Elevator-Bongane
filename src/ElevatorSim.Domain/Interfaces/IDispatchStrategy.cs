using ElevatorSim.Domain.Models;

namespace ElevatorSim.Domain.Interfaces;

/// <summary>
/// Defines an algorithm for selecting which elevator should service a floor request.
/// </summary>
public interface IDispatchStrategy
{
    /// <summary>
    /// Selects the most suitable elevator for the given floor request.
    /// </summary>
    /// <param name="floorRequest">The floor and passenger count being requested.</param>
    /// <param name="elevators">The full fleet of elevators available for selection.</param>
    /// <returns>
    /// The best <see cref="IElevator"/> candidate, or <see langword="null"/> if no
    /// suitable elevator is available.
    /// </returns>
    public IElevator? DispatchElevator(FloorRequest floorRequest, List<IElevator> elevators);
}
