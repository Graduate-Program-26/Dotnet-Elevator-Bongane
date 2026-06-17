using ElevatorSim.Application.Models;
using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Application.Services;

public class ElevatorController
{
    private readonly int _maxNumberOfFloors;
    private readonly int _maxNumberOfElevators;
    private readonly IDispatchStrategy _strategy;
    public List<IElevator> Elevators { get; } = new List<IElevator>();

    // Limit the number of floors
    public Dictionary<int, Queue<FloorRequest>> FloorRequestsPerFloor = new Dictionary<int, Queue<FloorRequest>>();

    public ElevatorController(int maxNumberOfFloors, int maxNumberOfElevators, 
        IElevator elevator, IDispatchStrategy strategy)
    {
        _maxNumberOfFloors = maxNumberOfFloors;
        _maxNumberOfElevators = maxNumberOfElevators;
        Elevators.Add(elevator);
        _strategy = strategy;
    }
    public ElevatorController(int maxNumberOfFloors, int maxNumberOfElevators, 
        List<IElevator> elevators, IDispatchStrategy strategy)
    {
        _maxNumberOfFloors = maxNumberOfFloors;
        _maxNumberOfElevators = maxNumberOfElevators;
        Elevators.AddRange(elevators);
        _strategy = strategy;
    }

    public void AddElevators(IElevator elevator)
    {
        if (Elevators.Count >= _maxNumberOfElevators)
        {
            throw new BuildingElevatorCapacityExceededException(_maxNumberOfElevators);
        }

        Elevators.Add(elevator);
    }

    public void AddFloorRequest(FloorRequest floorRequest)
    {
        // Get specific floor and append floor request
        if (FloorRequestsPerFloor.TryGetValue(floorRequest.FloorNumber, out var floorRequests))
        {
            floorRequests.Enqueue(floorRequest);
        }
        else // If there are no requests on that floor create one
        {
            var newQueue = new Queue<FloorRequest>();
            newQueue.Enqueue(floorRequest);
            FloorRequestsPerFloor.Add(floorRequest.FloorNumber, newQueue);
        }
    }

    public ElevatorDispatchResult DispatchElevator(FloorRequest fRequest)
    {
        int numberOfPassengersReqestingElevator = fRequest.NumberOfPassengers;
        int totalCapacityAvailable = Elevators.Sum(elevator => elevator.CurrentCapacity);
        int passengersBoarded = 0;
        while (numberOfPassengersReqestingElevator > 0 && totalCapacityAvailable > 0)
        {
            // Dispatch elevator => nextBestChoice
            var nextBestChoice = _strategy.DispatchElevator(fRequest, Elevators);

            if (nextBestChoice == null)
            {
                return new ElevatorDispatchResult(0, numberOfPassengersReqestingElevator, "No elevators available");
            }
            
            // Move elevator to desired floor
            nextBestChoice.ChangeState(ElevatorState.Moving);
            while (nextBestChoice.State == ElevatorState.Moving)
            {
                nextBestChoice.Step(fRequest.FloorNumber); 
            }
            
            // Board passengers
            // Display status boarding passengers
            nextBestChoice.ChangeState(ElevatorState.DoorsOpen);
            Console.WriteLine($"Open elevator {nextBestChoice.Id} doors");
            Console.WriteLine($"Elevator has {nextBestChoice.CurrentCapacity} spaces left.");
            // Begin boarding
            while (nextBestChoice.CurrentCapacity > 0)
            {
                if (numberOfPassengersReqestingElevator <= 0)
                    break;
                nextBestChoice.Board(1);
                --numberOfPassengersReqestingElevator;
            }
            
            Console.WriteLine($"Elevator has {nextBestChoice.CurrentCapacity} spaces left.");
            
            nextBestChoice.ChangeState(ElevatorState.DoorsClosed);
            Console.WriteLine($"Close elevator {nextBestChoice.Id} doors");
            totalCapacityAvailable = Elevators.Sum(elevator => elevator.CurrentCapacity);

        }
        passengersBoarded = fRequest.NumberOfPassengers - numberOfPassengersReqestingElevator;

        return new ElevatorDispatchResult(passengersBoarded, numberOfPassengersReqestingElevator, "");
    }
}