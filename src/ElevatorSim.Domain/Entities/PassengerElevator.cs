using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;

namespace ElevatorSim.Domain.Entities;

public class PassengerElevator : ElevatorBase
{
    private int MaxNumberOfPeople { get; init; }
    public List<Passenger> Passengers { get; private set; } = new List<Passenger>();

    public PassengerElevator( int minFloor, int maxFloor, 
        int maxNumberOfPeople, int floorNumber = 0) : 
        base(minFloor, maxFloor, maxNumberOfPeople, floorNumber)
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

        if (this.State == ElevatorState.Moving)
        {
            throw new ElevatorMovingException(this.Id);
        }

        if (this.State == ElevatorState.OutOfService)
        {
            throw new ElevatorOutOfServiceException(this.Id);
        }
        
        Passengers.Add(newPassenger);
    }

    public override void OffLoad(ILoad load)
    {
        // Check if load/passenger exists.
        var passenger = (Passenger)load;

        var offLoadedPassenger = Passengers.FirstOrDefault(p => p.Id == passenger.Id);
        if (offLoadedPassenger == null)
        {
            throw new InvalidOperationException("Passenger is not on elevator.");
        }

        Passengers.Remove(offLoadedPassenger);
    }
}