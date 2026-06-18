using ElevatorSim.Domain.Enums;
using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;

namespace ElevatorSim.Domain.Entities;

/// <summary>
/// An elevator that carries passengers.
/// Enforces capacity, movement, and service-state rules on boarding.
/// </summary>
public class PassengerElevator : ElevatorBase
{
   
    /// <summary>
    /// Initialises a new <see cref="PassengerElevator"/>.
    /// </summary>
    /// <param name="minFloor">The lowest floor this elevator services.</param>
    /// <param name="maxFloor">The highest floor this elevator services.</param>
    /// <param name="maxNumberOfPeople">Maximum passenger capacity.</param>
    /// <param name="floorNumber">Starting floor; defaults to 0.</param>
    public PassengerElevator(int minFloor, int maxFloor,
        int maxNumberOfPeople, int floorNumber = 0) :
        base(minFloor, maxFloor, maxNumberOfPeople, floorNumber)
    {
       
    }
    
}
