using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Application.Strategies;

public class NearestAvailableStrategies : IDispatchStrategy
{
    public List<IElevator> Elevators { get; } = new List<IElevator>();
    // Limit the number of floors
    public Dictionary<int, Queue<FloorRequest>> FloorRequestsPerFloor = new Dictionary<int, Queue<FloorRequest>>();
    public Queue<FloorRequest> FloorRequests { get; }

    public void AddElevators(IElevator elevator)
    {
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
    
    public IElevator DispatchElevator(FloorRequest floorRequest)
    {
        if (Elevators.Count == 0)
        {
            throw new ArgumentNullException(nameof(Elevators),"No elevators available.");
        }
        
        var dispatchedElevator = Elevators.FirstOrDefault(elevator => elevator.FloorNumber == 1);
        
        if (dispatchedElevator == null)
        {
            throw new ArgumentNullException(nameof(dispatchedElevator),"No elevator could be dispatched at the moment.");
        }

        var orderdElevators = Elevators.OrderBy(elevator => elevator.FloorNumber);
        
        // Determine which is the closest elevator.
        IElevator closestElevator;
        int distanceBetween = 0;
        foreach (var elevator in Elevators)
        {
            // First determine the distance between the two
            if (distanceBetween < elevator.FloorNumber - floorRequest.FloorNumber)
            {
              distanceBetween = elevator.FloorNumber - floorRequest.FloorNumber;
              closestElevator = elevator;
            }
        }
        
        
        return dispatchedElevator;
    }
}