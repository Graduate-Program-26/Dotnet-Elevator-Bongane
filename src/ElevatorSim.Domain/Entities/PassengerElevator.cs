namespace ElevatorSim.Domain.Entities;

public class PassengerElevator : ElevatorBase
{
    int NumberOfPassengers { get; set; }
    int MaxNumberOfPeople { get; init; }
}