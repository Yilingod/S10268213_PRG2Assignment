using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268213_PRG2Assignment
{
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public Flight() { }
        public Flight(string flightnumber,string origin,string destination,DateTime expectedtime,string status)
        {
            this.FlightNumber = flightnumber;
            this.Origin = origin;
            this.Destination = destination;
            this.ExpectedTime = expectedtime;
            this.Status = status;

        }
        public abstract double CalculateFees();
        public override string ToString()
        {
            return $"Flight Number:{FlightNumber}   Origin:{Origin}   Destination:{Destination}  Expected Time:{ExpectedTime}  Status:{Status}"
        }
    }
}
