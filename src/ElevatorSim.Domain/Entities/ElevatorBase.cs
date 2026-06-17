using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Enums;

namespace ElevatorSim.Domain.Entities;

public abstract class ElevatorBase : IElevator
{
    public Guid Id { get; } = Guid.NewGuid();
    public int CurrentFloorNumber { get; set; }
    public int CurrentCapacity { get; set; }
    public int MaxCapacity { get; init; }
    public ElevatorState State { get; private set; }
    public ElevatorDirection Direction { get; private set; }
    public int MinFloor { get; init; }
    public abstract void AddLoad(ILoad load);
    public abstract void OffLoad(ILoad load);

    public int MaxFloor { get; init; }
    
    public ElevatorBase(int minFloor , int maxFloor, int maxCapacity, int floorNumber, 
         ElevatorState state = ElevatorState.Stationary, 
        ElevatorDirection direction = ElevatorDirection.Idle)
    {
        CurrentFloorNumber = floorNumber;
        MinFloor = minFloor;
        MaxFloor = maxFloor;
        MaxCapacity = maxCapacity;
        CurrentCapacity = maxCapacity;
        State = state;
        Direction = direction;
    }

    public void ChangeState(ElevatorState state)
    {
        State = state;
    }

    public void ChangeCurrentFloorNumber(int currentFloorNumber)
    {
        CurrentFloorNumber = currentFloorNumber;
    }
    public void Step(int targetFloor)
    {
        if (State != ElevatorState.Moving)
            return;                          // nothing to do if not moving

        if (CurrentFloorNumber < targetFloor)
        {
            CurrentFloorNumber++;            // move up one floor
            this.Direction = ElevatorDirection.Up;
        }
        else if (CurrentFloorNumber > targetFloor)
        {
            CurrentFloorNumber--;            // move down one floor
            this.Direction = ElevatorDirection.Down;
        }
        this.ShowStatus();
        if (CurrentFloorNumber == targetFloor)
        {
            State = ElevatorState.Stationary;
            Direction = ElevatorDirection.Idle;
        }
    }

    public void ShowStatus()
    {
        Console.WriteLine(
            $"Elevator {this.Id} | Floor: {this.CurrentFloorNumber} | " +
            $"Direction: {this.Direction} | State: {this.State} | " +
            $"Capacity: {this.CurrentCapacity}");
    }

    public void ChangeCurrentCapacity(int currentCapacity)
    {
        CurrentCapacity = currentCapacity;
    }

    public void Board(int load)
    {
        CurrentCapacity -= load;
        Console.WriteLine($"Elevator {this.Id} boarding on 1 passenger on floor {this.CurrentFloorNumber}.");
    }
}