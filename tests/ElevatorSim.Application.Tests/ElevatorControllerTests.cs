using ElevatorSim.Application.Models;
using ElevatorSim.Application.Services;
using ElevatorSim.Application.Strategies;
using ElevatorSim.Domain.Entities;
using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Application.Tests;

public class ElevatorControllerTests
{
    private const int MinFloor = 0;
    private const int MaxFloor = 10;
    private const int MaxPeople = 10;
    private const int MaxElevators = 5;
    private const int MaxFloorsInBuilding = 13;

    private readonly IDispatchStrategy _strategy = new NearestAvailableStrategies();

    private PassengerElevator NewElevator() => new(MinFloor, MaxFloor, MaxPeople);

    private List<IElevator> NewFleet(int count)
    {
        var list = new List<IElevator>();
        for (int i = 0; i < count; i++) list.Add(NewElevator());
        return list;
    }

    private ElevatorController NewController(int elevatorCount) =>
        new(MaxFloorsInBuilding, MaxElevators, NewFleet(elevatorCount), _strategy);
    
    [Fact]
    public void AddElevator_ThrowsWhenExceedingMaxElevators()
    {
        var elevatorController = NewController(MaxElevators);  

        Assert.Throws<BuildingElevatorCapacityExceededException>(
            () => elevatorController.AddElevators(NewElevator()));
    }

    [Fact]
    public void AddElevator_CheckIfElevatorsFillUpToTheMax()
    {
        var exception = Record.Exception(() =>
        {
            var elevatorController =
                new ElevatorController(MaxFloorsInBuilding, MaxElevators, NewFleet(MaxElevators), _strategy);
        });
    // No exception thrown
        Assert.Null(exception);  
    }
    
    // Check if floor can be requested when there are no elevators
    [Fact]
    public void DispatchElevator_WhenNoElevatorsAvailableZeroPassengersBoatded()
    {
        var elevatorController = NewController(0);   // empty fleet
        
        var result = elevatorController.DispatchElevator(new FloorRequest(5, 3));
        
        Assert.Equal(0, result.PassengersBoarded);
        Assert.Equal(3, result.PassengersWaiting);
    }
    
    [Fact]
    public void DispatchElevator_ReturnsElevatorCapacityFullWhenThereIsNoSpaceLeft()
    {
        var elevatorController = NewController(2);
        var request = new FloorRequest(5, 25); 

        ElevatorDispatchResult result = elevatorController.DispatchElevator(request);
        Assert.Equal(20, result.PassengersBoarded);
        Assert.Equal(5, result.PassengersWaiting);
    }

    [Fact]
    public void DispatchElevator_CannotDispatchMovingElevator()
    {
        var elevatorController = NewController(1);
        var request = new FloorRequest(5, 15);

        var elevator = new PassengerElevator(MinFloor, MaxFloor, MaxPeople, request.FloorNumber);
        elevator.ChangeState(ElevatorState.Moving);
        elevatorController.AddElevators(elevator);

        ElevatorDispatchResult result = elevatorController.DispatchElevator(request);
        Assert.Equal(10, result.PassengersBoarded);
        Assert.Equal(5, result.PassengersWaiting);
    }

    [Fact]
    public void DispatchElevator_FloorRequestIsWithinBuildingMaxFloors()
    {
        var evaluatorController = NewController(2) ;

        Assert.Throws<FloorRequestExceedsAvailableFloorsException>(
            () => evaluatorController.DispatchElevator(new FloorRequest(39, 5)));
    }
    
}