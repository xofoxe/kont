using System;
using System.Collections.Generic;

namespace task2
{
    class Aircraft
    {
        public string Name { get; }
        public bool IsTakingOff { get; set; }

        private CommandCentre _commandCentre;

        public Aircraft(string name, CommandCentre commandCentre)
        {
            this.Name = name;
            this._commandCentre = commandCentre;
        }

        public void RequestLanding()
        {
            Console.WriteLine($"Aircraft {Name} requesting to land.");
            _commandCentre.HandleLandingRequest(this);
        }

        public void RequestTakeOff()
        {
            Console.WriteLine($"Aircraft {Name} requesting to take off.");
            _commandCentre.HandleTakeOffRequest(this);
        }
    }

    public class Runway
    {
        private static readonly Random _random = new Random();
        public readonly int Id = _random.Next(1000, 9999);
        public bool IsBusy { get; set; } = false;

        public void HighLightRed()
        {
            Console.WriteLine($"Runway {Id} is busy!");
        }

        public void HighLightGreen()
        {
            Console.WriteLine($"Runway {Id} is free!");
        }
    }
    class CommandCentre
    {
        private List<Runway> _runways = new List<Runway>();
        private Dictionary<Aircraft, Runway> _aircraftRunwayMap = new Dictionary<Aircraft, Runway>();

        public CommandCentre(Runway[] runways)
        {
            this._runways.AddRange(runways);
        }

        public void HandleLandingRequest(Aircraft aircraft)
        {
            Runway freeRunway = _runways.Find(r => !r.IsBusy);

            if (freeRunway != null)
            {
                freeRunway.IsBusy = true;
                _aircraftRunwayMap[aircraft] = freeRunway;

                Console.WriteLine($"Aircraft {aircraft.Name} has landed on runway {freeRunway.Id}.");
                freeRunway.HighLightRed();
            }
            else
            {
                Console.WriteLine($"Could not land, the runway is busy.");
            }
        }

        public void HandleTakeOffRequest(Aircraft aircraft)
        {
            if (_aircraftRunwayMap.TryGetValue(aircraft, out Runway runway))
            {
                runway.IsBusy = false;
                _aircraftRunwayMap.Remove(aircraft);

                Console.WriteLine($"Aircraft {aircraft.Name} has taken off from runway {runway.Id}.");
                runway.HighLightGreen();
            }
            else
            {
                Console.WriteLine($"Aircraft {aircraft.Name} is not assigned to any runway.");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Runway[] runways = new[] { new Runway(), new Runway() };
            CommandCentre commandCentre = new CommandCentre(runways);

            Aircraft aircraft1 = new Aircraft("fly 737", commandCentre);
            Aircraft aircraft2 = new Aircraft("fly A320", commandCentre);

            aircraft1.RequestLanding();
            aircraft2.RequestLanding();

            aircraft1.RequestTakeOff();
            aircraft2.RequestTakeOff();
        }
    }
}
