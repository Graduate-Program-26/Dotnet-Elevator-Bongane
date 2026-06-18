using ElevatorSim.Domain.Entities;
using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;

namespace ElevatorSim.Domain.Tests;

public class ElevatorTests
{
    private const int MinFloor = 0;
    private const int MaxFloor = 10;
    private const int MaxPeople = 10;
    
    [Fact]
    public void Step_ElevatorCannotGoBelowMinimumFloors()
    {
        IElevator elevator = new PassengerElevator(MinFloor, MaxFloor, MaxPeople);
        elevator.ChangeState(ElevatorState.Moving);
        
        Assert.Throws<ElevatorExceedsAvailableFloorsException>(() => elevator.Step(-1));
    }
    
    [Fact]
    public void Step_ElevatorCannotGoAboveMaximumFloors()
    {
        IElevator elevator = new PassengerElevator(MinFloor, MaxFloor, MaxPeople);
        elevator.ChangeState(ElevatorState.Moving);
        
        Assert.Throws<ElevatorExceedsAvailableFloorsException>(() => elevator.Step(11));
    }

    [Fact]
    public void Step_ElevatorMovingUpWhenComingFromBelow()
    {
        IElevator elevator = new PassengerElevator(MinFloor, MaxFloor, MaxPeople);
        elevator.ChangeState(ElevatorState.Moving);
        
        elevator.Step(2);
        
        Assert.Equal(ElevatorDirection.Up, elevator.Direction);
    }
    
    [Fact]
    public void Step_ElevatorMovingDownWhenComingFromAbove()
    {
        IElevator elevator = new PassengerElevator(MinFloor, MaxFloor, MaxPeople, 2);
        elevator.ChangeState(ElevatorState.Moving);
        
        elevator.Step(0);
        
        Assert.Equal(ElevatorDirection.Down, elevator.Direction);
    }
}