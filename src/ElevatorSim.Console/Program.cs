using ElevatorSim.Application.Interfaces;
using ElevatorSim.Application.Services;
using ElevatorSim.Application.Strategies;
using ElevatorSim.Console;
using ElevatorSim.Domain.Entities;
using ElevatorSim.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

const int minFloor = 1;
const int maxFloor = 10;
const int maxNumberOfPeople = 10;
const int maxElevators = 3;

var services = new ServiceCollection();

services.AddSingleton<IDispatchStrategy, NearestAvailableStrategies>();

services.AddSingleton<IElevatorController>(serviceProvider =>
{
    var strategy = serviceProvider.GetRequiredService<IDispatchStrategy>();
    var fleet = new List<IElevator>
    {
        new PassengerElevator(minFloor, maxFloor, maxNumberOfPeople),
        new PassengerElevator(minFloor, maxFloor, maxNumberOfPeople, 2),
    };
    return new ElevatorController(maxFloor, maxElevators, fleet, strategy);
});

services.AddSingleton<IDispatchProgress>(sp =>
{
    var controller = sp.GetRequiredService<IElevatorController>();
    return new ConsoleProgress(controller.Elevators);
});

services.AddSingleton<SimulationRunner>(sp => new SimulationRunner(
    sp.GetRequiredService<IElevatorController>(),
    sp.GetRequiredService<IDispatchProgress>(),
    minFloor,
    maxFloor));

var provider = services.BuildServiceProvider();

provider.GetRequiredService<SimulationRunner>().Run();