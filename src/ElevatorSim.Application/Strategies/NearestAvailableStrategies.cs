using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Application.Strategies;

public class NearestAvailableStrategies : IDispatchStrategy
{
    
    public IElevator? DispatchElevator(FloorRequest floorRequest, List<IElevator> elevators)
    {
        if (elevators.Count == 0)
        {
            throw new ArgumentNullException(nameof(elevators),"No elevators available.");
        }
        
        int floorRequestNumber = floorRequest.FloorNumber;
        
        // Determine which is the closest elevator.
        var nextBestChoice = elevators.Where(elevator => elevator.CurrentCapacity != 0).FirstOrDefault();
        // If all elevators are full
        if (nextBestChoice == null)
        {
            return null;
        }
        
        int minimumFloorDistance = Math.Abs(nextBestChoice.CurrentFloorNumber - floorRequestNumber);
        int highestCurrentCapacityElevator = nextBestChoice.CurrentCapacity;
        // Selecting the best elevator
        foreach (var elevator in elevators)
        {
            if (Math.Abs(elevator.CurrentFloorNumber - floorRequestNumber) <
                minimumFloorDistance // Check which is the closest elevator by distance
                && elevator.CurrentCapacity >=
                highestCurrentCapacityElevator // Chooses the last elevator on the list which satisfies all conditions
                && elevator.State == ElevatorState.Stationary
               )
            {
                nextBestChoice = elevator;
                minimumFloorDistance = Math.Abs(nextBestChoice.CurrentFloorNumber - floorRequestNumber);
                highestCurrentCapacityElevator = nextBestChoice.CurrentCapacity;
            }
        }
        
        Console.WriteLine(
            $"Dispatching elevator {nextBestChoice.Id} on Floor {nextBestChoice.CurrentFloorNumber} " +
            $"to Floor {floorRequest.FloorNumber}.");
        
        return nextBestChoice;
    }
}