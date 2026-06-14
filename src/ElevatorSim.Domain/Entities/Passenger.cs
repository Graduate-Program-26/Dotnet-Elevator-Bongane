using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Domain.Entities;

public class Passenger
{
    public int FloorNumber { get; init; }

    public Passenger(int floorNumber)
    {
        FloorNumber = floorNumber;
    }
    
    // Passenger should call the elevator
    public FloorRequest RequestElevator()
    {
        return new FloorRequest(FloorNumber);
    }
}