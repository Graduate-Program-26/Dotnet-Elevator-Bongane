using ElevatorSim.Application.Services;
using ElevatorSim.Application.Strategies;
using ElevatorSim.Domain.Entities;
using ElevatorSim.Domain.Interfaces;
using ElevatorSim.Domain.Models;

const int minFloor = 1;
const int maxFloor = 10;
const int maxNumberOfPeople = 10;


// Mock elevator
IElevator elevator1 = new PassengerElevator(minFloor,maxFloor,maxNumberOfPeople);
IElevator elevator2 = new PassengerElevator(minFloor, maxFloor, maxNumberOfPeople, 2);

IElevator[] elevators = new[] { elevator1, elevator2 };
// TODO: A main loop

IDispatchStrategy strategy = new NearestAvailableStrategies();
ElevatorController elevatorController = new ElevatorController(maxFloor, 3, elevators.ToList(), strategy);

FloorRequest fRequest = GetPassengerFloorNumber();


elevatorController.DispatchElevator(fRequest);

static FloorRequest GetPassengerFloorNumber()
{
    // Enter the floor within range
    int desiredPassengerFloor, numberOfPassengers;

    while (true)
    {
        Console.Write("Enter your desired floor (1-10): ");

        if (!int.TryParse(Console.ReadLine(), out desiredPassengerFloor))
        {
            Console.WriteLine("Please enter a valid number.");
            continue;
        }

        if (!IsFloorWithinRange(desiredPassengerFloor, 1, 10))
        {
            Console.WriteLine("Floor must be between 1 and 10.");
            continue;
        }

        break;
    }

    while (true)
    {
        Console.Write("Enter the number of passengers: ");
        
        if (!int.TryParse(Console.ReadLine(), out numberOfPassengers))
        {
            Console.WriteLine("Please enter a valid number.");
            continue;
        }
        
        break;
    }

    return new FloorRequest(desiredPassengerFloor, numberOfPassengers);
}
static bool IsFloorWithinRange(int desiredFloor, int minFloor, int maxFloor)
{
    return desiredFloor <= maxFloor && desiredFloor >= minFloor ;
}