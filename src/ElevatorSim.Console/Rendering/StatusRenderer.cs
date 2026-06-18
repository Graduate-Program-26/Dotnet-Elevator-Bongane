using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Interfaces;

namespace ElevatorSim.Console.Rendering;

/// <summary>
/// Renders a colour-coded elevator status panel to the console.
/// </summary>
public static class StatusRenderer
{
    /// <summary>
    /// Clears the console and redraws the full elevator status panel.
    /// Each elevator is printed in a colour that reflects its current <see cref="ElevatorState"/>.
    /// </summary>
    /// <param name="elevators">The fleet to display.</param>
    public static void Draw(IReadOnlyList<IElevator> elevators)
    {
        System.Console.Clear();
        System.Console.WriteLine("=== Elevator Status ===\n");

        foreach (var e in elevators)
        {
            SetColor(e.State);
            System.Console.WriteLine(
                $"Elevator {e.Id} | Floor {e.CurrentFloorNumber,2} | " +
                $"{e.Direction,-6} | {e.State,-12} | " +
                $"{e.CurrentCapacity}/{e.MaxCapacity}");
            System.Console.ResetColor();
        }
        System.Console.WriteLine();
    }

    private static void SetColor(ElevatorState elevatorState) =>
        System.Console.ForegroundColor = elevatorState switch
        {
            ElevatorState.Moving     => ConsoleColor.Yellow,
            ElevatorState.DoorsOpen  => ConsoleColor.Cyan,
            ElevatorState.Stationary => ConsoleColor.Green,
            _                        => ConsoleColor.Gray
        };
}
