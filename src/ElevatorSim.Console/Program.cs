using ElevatorSim.Domain.Entities;
using ElevatorSim.Domain.Interfaces;

// Mock elevator
IElevator elevator = new PassengerElevator();


const int minFloor = 1;
const int maxFloor = 10;

// TODO: A main loop

int passengerFloorNumber = GetPassengerFloorNumber();
// Mock passenger
Passenger passenger = new Passenger(passengerFloorNumber);
// Call elevator
passenger.CallElevator();


static int GetPassengerFloorNumber()
{
    // Enter the floor within range
    int desiredPassengerFloor;

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

    return desiredPassengerFloor;
}
static bool IsFloorWithinRange(int desiredFloor, int minFloor, int maxFloor)
{
    return desiredFloor <= maxFloor && desiredFloor >= minFloor ;
}