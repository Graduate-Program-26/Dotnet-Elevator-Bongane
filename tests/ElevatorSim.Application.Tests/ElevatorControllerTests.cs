using ElevatorSim.Application.Models;
using ElevatorSim.Application.Services;
using ElevatorSim.Application.Strategies;
using ElevatorSim.Domain.Entities;
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
        var controller = NewController(MaxElevators);  

        Assert.Throws<BuildingElevatorCapacityExceededException>(
            () => controller.AddElevators(NewElevator()));
    }

    [Fact]
    public void AddElevator_CheckIfElevatorsFillUpToTheMax()
    {
        var elevatorController = NewController(MaxElevators);  
        
        var exception = Record.Exception(() =>
        {
            for (int i = 0; i < MaxElevators - 1; ++i)
            {
                elevatorController.AddElevators(NewElevator());
            }
        });
    // No exception thrown
        Assert.Null(exception);  
    }

    [Fact]
    public void AddFloorRequest_CheckIfElevatorCanAcceptFloorRequest()
    {
        int minElevatorFloors = 0;
        int maxElevatorFloors = 10;
        int maxNumberOfPeople = 10;
        int maxNumberOfElevators = 5;

        int maxFloorsInBuilding = 13;

        IElevator[] elevators = new PassengerElevator[maxNumberOfElevators];
        IDispatchStrategy strategy = new NearestAvailableStrategies();
        
        for (int i = 0; i < maxNumberOfElevators; ++i)
        {
            elevators[i] =new PassengerElevator(minElevatorFloors, maxElevatorFloors, maxNumberOfPeople);
        }
        
        ElevatorController elevatorController = 
            new ElevatorController(maxFloorsInBuilding, maxNumberOfElevators, elevators.ToList(),strategy);
        
        
    }
    
    // Check if floor can be requested when there are no elevators
    [Fact]
    public void DispatchElevator_ThrowsWhenNoElevatorsExist()
    {
        var controller = NewController(0);   // empty fleet

        Assert.Throws<ArgumentNullException>(
            () => controller.DispatchElevator(new FloorRequest(5, 3)));
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
}