namespace ElevatorSim.Domain.Entities;

public class PassengerElevator : ElevatorBase
{
    public int NumberOfPassengers { get; set; }
    private int MaxNumberOfPeople { get; init; }

    public PassengerElevator( int minFloor, int maxFloor, 
        int maxNumberOfPeople, int numberOfPassengers = 0) : base(minFloor, maxFloor)
    {
        NumberOfPassengers = numberOfPassengers;
        MaxNumberOfPeople = maxNumberOfPeople;
    }
}