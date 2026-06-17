using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Domain.Entities;

public class Passenger : IEntity, ILoad
{
    public int FloorNumber { get; init; }

    public Passenger(int floorNumber)
    {
        Id = Guid.NewGuid();
        FloorNumber = floorNumber;
    }
    
    // Passenger should call the elevator
    // public FloorRequest RequestElevator()
    // {
    //     return new FloorRequest(FloorNumber);
    // }

    public Guid Id { get; }
}