using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number	: S10268213K
// Student Name	: Gong Yilin
// Partner Name	: Yang Ee Ming
//==========================================================
namespace S10268213_PRG2Assignment
{
    class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string,Flight> Flights { get; set; } = new Dictionary<string,Flight>();
        public Airline() { }
        public Airline(string name,string code)
        {
            Name = name;
            Code = code;
            
        }


        public bool AddFlight(Flight flight)
        {
            if (flight.AirlineCode == Code)
            {
                if (!Flights.ContainsValue(flight))
                {
                    Flights[flight.FlightNumber] = flight;
                    return true;
                }
                
            }
            return false;
            
        }

        public double CalculateFees()
        {
            double totalFee = 0;

            foreach (var flight in Flights.Values)
            {
                if (flight.Destination == "Singapore (SIN)")
                {
                    totalFee += 500; // Arriving flight fee
                }
                else if (flight.Origin == "Singapore (SIN)")
                {
                    totalFee += 800; // Departing flight fee
                }
            }

            return totalFee;
        }
        public bool RemoveFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Remove(flight.FlightNumber);
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Name: {Name}\t Code: {Code}";
        }


    }
}
