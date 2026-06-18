using ElevatorSim.Application.Interfaces;
using ElevatorSim.Console.Rendering;
using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Console;

/// <summary>
/// Drives the interactive elevator simulation loop.
/// Renders the status panel, accepts user commands, and delegates dispatch to
/// <see cref="IElevatorController"/>.
/// </summary>
public class SimulationRunner
{
    private readonly IElevatorController _controller;
    private readonly IDispatchProgress _progress;
    private readonly int _minFloor;
    private readonly int _maxFloor;

    /// <summary>
    /// Initialises the runner with its dependencies and floor bounds.
    /// </summary>
    /// <param name="controller">The elevator controller used to dispatch floor requests.</param>
    /// <param name="progress">The progress sink that renders live updates during dispatch.</param>
    /// <param name="minFloor">The lowest floor number accepted as valid input.</param>
    /// <param name="maxFloor">The highest floor number accepted as valid input.</param>
    public SimulationRunner(IElevatorController controller, IDispatchProgress progress,
        int minFloor, int maxFloor)
    {
        _controller = controller;
        _progress = progress;
        _minFloor = minFloor;
        _maxFloor = maxFloor;
    }

    /// <summary>
    /// Starts the simulation loop. Runs until the user chooses to quit.
    /// </summary>
    public void Run()
    {
        bool running = true;
        while (running)
        {
            StatusRenderer.Draw(_controller.Elevators);

            System.Console.WriteLine();
            System.Console.WriteLine("[1] Call an elevator   [q] Quit");
            System.Console.Write("Choose an option: ");
            string? choice = System.Console.ReadLine()?.Trim().ToLower();

            switch (choice)
            {
                case "q":
                    running = false;
                    break;

                case "1":
                    HandleCallElevator();
                    break;

                default:
                    System.Console.WriteLine("Unknown option. Press Enter to continue...");
                    System.Console.ReadLine();
                    break;
            }
        }

        System.Console.WriteLine("Simulation ended.");
    }

    private void HandleCallElevator()
    {
        FloorRequest fRequest = GetFloorRequest();

        try
        {
            var result = _controller.DispatchElevator(fRequest, _progress);

            System.Console.WriteLine();
            System.Console.WriteLine(
                $"{result.PassengersBoarded} passenger(s) boarded, " +
                $"{result.PassengersWaiting} still waiting.");
        }
        catch (FloorRequestExceedsAvailableFloorsException ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
        }
        catch (ElevatorExceedsAvailableFloorsException ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
        }

        System.Console.WriteLine("Press Enter to continue...");
        System.Console.ReadLine();
    }

    private FloorRequest GetFloorRequest()
    {
        int floor = ReadFloor();
        int passengers = ReadPassengerCount();
        return new FloorRequest(floor, passengers);
    }

    private int ReadFloor()
    {
        while (true)
        {
            System.Console.Write($"Enter your desired floor ({_minFloor}-{_maxFloor}): ");

            if (!int.TryParse(System.Console.ReadLine(), out int floor))
            {
                System.Console.WriteLine("Please enter a valid number.");
                continue;
            }

            if (floor < _minFloor || floor > _maxFloor)
            {
                System.Console.WriteLine($"Floor must be between {_minFloor} and {_maxFloor}.");
                continue;
            }

            return floor;
        }
    }

    private static int ReadPassengerCount()
    {
        while (true)
        {
            System.Console.Write("Enter the number of passengers: ");

            if (!int.TryParse(System.Console.ReadLine(), out int count) || count <= 0)
            {
                System.Console.WriteLine("Please enter a positive number.");
                continue;
            }

            return count;
        }
    }
}
