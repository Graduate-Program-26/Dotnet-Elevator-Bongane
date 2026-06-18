using System.Threading;
using ElevatorSim.Application.Interfaces;
using ElevatorSim.Console.Rendering;
using ElevatorSim.Domain.Interfaces;

namespace ElevatorSim.Console;

/// <summary>
/// Console implementation of <see cref="IDispatchProgress"/>.
/// Redraws the <see cref="StatusRenderer"/> panel on every floor step and writes
/// boarding/state-change events as text below the panel.
/// </summary>
public class ConsoleProgress : IDispatchProgress
{
    private readonly IReadOnlyList<IElevator> _elevators;

    /// <summary>
    /// Initialises a new <see cref="ConsoleProgress"/> with the fleet to display.
    /// </summary>
    /// <param name="elevators">The elevator fleet rendered on each progress event.</param>
    public ConsoleProgress(IReadOnlyList<IElevator> elevators)
    {
        _elevators = elevators;
    }

    /// <inheritdoc/>
    public void ElevatorDispatched(Guid elevatorId, int fromFloor, int toFloor)
    {
        System.Console.WriteLine($"  Elevator {ShortId(elevatorId)}: Floor {fromFloor} → {toFloor}");
    }

    /// <inheritdoc/>
    public void ElevatorMoved(Guid elevatorId, int currentFloor)
    {
        StatusRenderer.Draw(_elevators);
        Thread.Sleep(150);
    }

    /// <inheritdoc/>
    public void Arrived(Guid elevatorId, int floor)
    {
        System.Console.WriteLine($"  Elevator {ShortId(elevatorId)} arrived at Floor {floor}");
    }

    /// <inheritdoc/>
    public void PassengersBoarded(Guid elevatorId, int count, int spacesLeft)
    {
        System.Console.WriteLine($"  +{count} boarded | {spacesLeft} space(s) remaining");
    }

    /// <inheritdoc/>
    public void ElevatorChangedState(Guid elevatorId)
    {
        System.Console.WriteLine($"Open elevator {elevatorId} doors");
    }

    private static string ShortId(Guid id) => id.ToString("N")[..8];
}
