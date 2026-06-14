using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Entities;

public class Passenger
{
    public int FloorNumber { get; set; }

    public Passenger(int floorNumber)
    {
        FloorNumber = floorNumber;
    }
    
    // Passenger should call the elevator
    public void CallElevator()
    {
        
    }
}