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
        
<<<<<<< Updated upstream
        var dispatchedElevator = Elevators.FirstOrDefault(elevator => elevator.FloorNumber == 1);
=======
        var dispatchedElevator = elevators.FirstOrDefault(elevator => elevator.CurrentFloorNumber == 1);
>>>>>>> Stashed changes
        
        if (dispatchedElevator == null)
        {
            throw new ArgumentNullException(nameof(dispatchedElevator),"No elevator could be dispatched at the moment.");
        }

<<<<<<< Updated upstream
        var orderdElevators = Elevators.OrderBy(elevator => elevator.FloorNumber);
=======
        var orderdElevators = elevators.OrderBy(elevator => elevator.CurrentFloorNumber);
>>>>>>> Stashed changes
        
        // Determine which is the closest elevator.
        IElevator closestElevator;
        int distanceBetween = 0;
        foreach (var elevator in elevators)
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