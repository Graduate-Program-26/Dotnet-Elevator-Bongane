using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Application.Services;

public class ElevatorController
{
    private readonly int _maxNumberOfFloors;
    private readonly int _maxNumberOfElevators;
    public List<IElevator> Elevators { get; } = new List<IElevator>();
    // Limit the number of floors
    public Dictionary<int, Queue<FloorRequest>> FloorRequestsPerFloor = new Dictionary<int, Queue<FloorRequest>>();

    public ElevatorController(int maxNumberOfFloors, int maxNumberOfElevators)
    {
        _maxNumberOfFloors = maxNumberOfFloors;
        _maxNumberOfElevators = maxNumberOfElevators ;
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
}