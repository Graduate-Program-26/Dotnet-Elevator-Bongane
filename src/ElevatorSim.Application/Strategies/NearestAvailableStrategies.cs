using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Application.Strategies;

public class NearestAvailableStrategies : IDispatchStrategy
{
    
    public IElevator DispatchElevator(FloorRequest floorRequest, List<IElevator> elevators)
    {
        if (elevators.Count == 0)
        {
            throw new ArgumentNullException(nameof(elevators),"No elevators available.");
        }
        

        var dispatchedElevator = elevators.FirstOrDefault(elevator => elevator.CurrentFloorNumber == 1);
        
        if (dispatchedElevator == null)
        {
            throw new ArgumentNullException(nameof(dispatchedElevator),"No elevator could be dispatched at the moment.");
        }


        var orderdElevators = elevators.OrderBy(elevator => elevator.CurrentFloorNumber);

        
        // Determine which is the closest elevator.
        IElevator closestElevator;
        int distanceBetween = 0;
        foreach (var elevator in elevators)
        {
            // First determine the distance between the two
            if (distanceBetween < elevator.CurrentFloorNumber - floorRequest.FloorNumber)
            {
              distanceBetween = elevator.CurrentFloorNumber - floorRequest.FloorNumber;
              closestElevator = elevator;
            }
        }
        
        
        return dispatchedElevator;
    }
}