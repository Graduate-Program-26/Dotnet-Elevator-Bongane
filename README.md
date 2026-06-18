# Elevator Simulation

A C# console application that simulates elevator movement in a large building, dispatching elevators efficiently to minimise passenger wait time.

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

Verify your installation:

```bash
dotnet --version
# expected: 10.x.x
```

---

## Setup

```bash
git clone <repo-url>
cd Dotnet-Elevator-Bongane
```

---

## Running the simulation

```bash
dotnet run --project src/ElevatorSim.Console
```

The console displays a live status panel showing every elevator's current floor, direction, state, and remaining capacity. From the menu:

- Press `1` to call an elevator — enter a target floor (1–10) and the number of waiting passengers.
- Press `q` to quit.

---

## Running the tests

```bash
dotnet test
```

All 13 tests should pass. Test projects:

| Project | Coverage |
|---|---|
| `ElevatorSim.Domain.Tests` | Elevator movement, load/unload, capacity and state validation |
| `ElevatorSim.Application.Tests` | Controller dispatch, fleet capacity, floor range validation |

---

## Project structure

```
src/
  ElevatorSim.Domain/          Core entities, interfaces, enums, exceptions
  ElevatorSim.Application/     Dispatch logic, strategies, controller
  ElevatorSim.Infrastructure/  (Reserved for future persistence/logging)
  ElevatorSim.Console/         Entry point, rendering, user interaction
tests/
  ElevatorSim.Domain.Tests/
  ElevatorSim.Application.Tests/
```

The solution follows Clean Architecture: dependencies point inward — Console → Application → Domain. Infrastructure is isolated and carries no domain logic.

---

## Dispatching algorithm

The `NearestAvailableStrategies` implementation selects the closest stationary elevator with available capacity to a requested floor. When a single elevator cannot accommodate all waiting passengers, additional dispatches loop until either all passengers are boarded or fleet capacity is exhausted. Moving elevators are excluded from selection.

---

## Assumptions

- The building has floors 1–10 and supports up to 3 elevators by default.
- Each elevator holds a maximum of 10 passengers.
- Elevators start at floor 1 (floor 0 for the second elevator — configurable via constructor).
- A floor request that exceeds the building's maximum floor throws a domain exception; the console surfaces a readable error message rather than a stack trace.
- Passengers are tracked as anonymous headcounts via `Board(int load)` during dispatch; the `AddLoad(ILoad)` / `OffLoad(ILoad)` API is reserved for named-passenger scenarios.
- `FreightElevator` and `HighSpeedElevator` types are intended extensions of `ElevatorBase`; the dispatch strategy works against `IElevator` and requires no changes to support them.
- Infrastructure layer is scaffolded but not yet implemented; it is intended for future persistence or structured logging.
