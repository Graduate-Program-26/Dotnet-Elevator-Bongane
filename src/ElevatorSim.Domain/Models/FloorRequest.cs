namespace ElevatorSim.Domain.Models;

/// <summary>
/// Represents a request to send an elevator to a specific floor for a group of passengers.
/// </summary>
/// <param name="FloorNumber">The floor passengers are waiting on.</param>
/// <param name="NumberOfPassengers">The number of passengers requesting the elevator.</param>
public record FloorRequest(int FloorNumber, int NumberOfPassengers);
