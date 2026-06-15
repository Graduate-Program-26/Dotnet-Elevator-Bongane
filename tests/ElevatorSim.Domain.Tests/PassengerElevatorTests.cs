using ElevatorSim.Domain.Entities;
using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;

namespace ElevatorSim.Domain.Tests;

public class PassengerElevatorTests
{
    [Fact]
    public void AddLoad_CheckIfPassengersGetAdded()
    {
        int minFloor = 0;
        int maxFloor = 10;
        int maxNumberOfPeople = 10;
        PassengerElevator elevator = new PassengerElevator(minFloor, maxFloor, maxNumberOfPeople);
        Passenger passenger = new Passenger(2);
        elevator.AddLoad(passenger);
        
        Assert.Single(elevator.Passengers);
    }

    [Fact]
    public void AddLoad_CheckIfMaxPassengerThresholdIsNotExceeded()
    {
        int minFloor = 0;
        int maxFloor = 10;
        int maxNumberOfPeople = 10;
        int floorNumber = 1;
        PassengerElevator elevator = new(minFloor, maxFloor, maxNumberOfPeople);
       
        for (int i = 0; i < 10; i++)
        {
            elevator.AddLoad( new Passenger(floorNumber));
        }
        
        Assert.Throws<CapacityExceededException>(() => elevator.AddLoad(new Passenger(1)));
    }
    
    [Fact]
    public void AddLoad_FillsToExactCapacityWithoutThrowing()
    {
        int minFloor = 0;
        int maxFloor = 10;
        int maxNumberOfPeople = 10;
        PassengerElevator elevator = new PassengerElevator(minFloor, maxFloor, maxNumberOfPeople);

        var exception = Record.Exception(() =>
        {
            for (int i = 0; i < 10; i++)
                elevator.AddLoad(new Passenger(1));
        });

        // No exception thrown
        Assert.Null(exception);   
    }
}