using ElevatorSim.Application.Services;
using ElevatorSim.Application.Strategies;
using ElevatorSim.Domain.Entities;
using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

namespace ElevatorSim.Application.Tests;

public class NearestAvailableStrategyTests
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
    public void DispatchElevator_OneElevatorGetsDispatchedAsRequiredByFloorRequests()
    {
        var elevators = NewFleet(2);
        
        var dispatchedElevator = _strategy.DispatchElevator(new FloorRequest(3, 6), elevators );
        
        Assert.Equal(dispatchedElevator.Id, elevators[0].Id);
    }
    
    [Fact]
    public void DispatchElevator_TheNearestElevatorDispatchedWhenNotOverloaded()
    {
        var elevators = NewFleet(1);
        
        // Has less capacity but should be dispatched
        elevators.Add(new PassengerElevator(MinFloor, MaxFloor, MaxPeople -6,2));
        
        var dispatchedElevator = _strategy.DispatchElevator(new FloorRequest(3, 6), elevators );
        
        Assert.Equal(dispatchedElevator.Id, elevators[1].Id);
    }
    
    
}