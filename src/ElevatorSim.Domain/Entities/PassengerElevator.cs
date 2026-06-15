using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;

namespace ElevatorSim.Domain.Entities;

public class PassengerElevator : ElevatorBase
{
    private int MaxNumberOfPeople { get; init; }
    public List<Passenger> Passengers { get; private set; } = new List<Passenger>();

    public PassengerElevator( int minFloor, int maxFloor, 
        int maxNumberOfPeople) : base(minFloor, maxFloor)
    {
        MaxNumberOfPeople = maxNumberOfPeople;
    }

    public override void AddLoad(ILoad load)
    {
        var newPassenger = (Passenger)load;
        // Check if passenger exists first
        if (Passengers.Exists(passenger => passenger.Id == newPassenger.Id))
        {
            Console.WriteLine("Passenger already in elevator.");
            return;
        }

        if (Passengers.Count >= MaxNumberOfPeople)
        {
            throw new CapacityExceededException(this.Id, MaxNumberOfPeople);
        }
        
        Passengers.Add(newPassenger);
    }
}