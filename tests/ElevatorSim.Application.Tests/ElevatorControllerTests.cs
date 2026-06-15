using ElevatorSim.Application.Services;
using ElevatorSim.Domain.Entities;
using ElevatorSim.Domain.Exceptions;
using ElevatorSim.Domain.Interfaces;

namespace ElevatorSim.Application.Tests;

public class ElevatorControllerTests
{
    [Fact]
    public void AddElevator_CannotExceedMaxNumberOfElevators()
    {
        int minElevatorFloors = 0;
        int maxElevatorFloors = 10;
        int maxNumberOfPeople = 10;
        int maxNumberOfElevators = 5;

        int maxFloorsInBuilding = 13;
        ElevatorController elevatorController = new ElevatorController(maxFloorsInBuilding, maxNumberOfElevators);
        
        IElevator[] elevators = new PassengerElevator[maxNumberOfElevators];
        for (int i = 0; i < maxNumberOfElevators; ++i)
        {
            elevatorController.AddElevators(new PassengerElevator(minElevatorFloors, 
                maxElevatorFloors, maxNumberOfPeople));
        }
        
        

        Assert.Throws<BuildingElevatorCapacityExceededException>(
            () => elevatorController.AddElevators(new PassengerElevator(0,10, 10)));
        
    }

    [Fact]
    public void AddElevator_CheckIfElevatorsFillUpToTheMax()
    {
        int minElevatorFloors = 0;
        int maxElevatorFloors = 10;
        int maxNumberOfPeople = 10;
        int maxNumberOfElevators = 5;

        int maxFloorsInBuilding = 13;
        ElevatorController elevatorController = new ElevatorController(maxFloorsInBuilding, maxNumberOfElevators);

        IElevator[] elevators = new PassengerElevator[maxNumberOfElevators];
        var exception = Record.Exception(() =>
        {
            for (int i = 0; i < maxNumberOfElevators; ++i)
            {
                elevatorController.AddElevators(new PassengerElevator(minElevatorFloors,
                    maxElevatorFloors, maxNumberOfPeople));
            }
        });

    // No exception thrown
        Assert.Null(exception);  
    }
}