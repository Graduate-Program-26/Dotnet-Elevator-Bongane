using ElevatorSim.Application.Models;
using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Application.Interfaces;

/// <summary>
/// Coordinates elevator dispatch for a building.
/// </summary>
public interface IElevatorController
{
    /// <summary>Gets a read-only view of the registered elevator fleet.</summary>
    IReadOnlyList<IElevator> Elevators { get; }

    /// <summary>
    /// Dispatches one or more elevators to service the given floor request.
    /// </summary>
    /// <param name="fRequest">The floor and passenger count to service.</param>
    /// <param name="progress">
    /// Optional progress sink for UI updates. Pass <see langword="null"/> to run silently.
    /// </param>
    /// <returns>Passengers boarded and passengers still waiting.</returns>
    /// <exception cref="FloorRequestExceedsAvailableFloorsException">
    /// Thrown when the requested floor exceeds the building maximum.
    /// </exception>
    ElevatorDispatchResult DispatchElevator(FloorRequest fRequest, IDispatchProgress? progress = null);
}
