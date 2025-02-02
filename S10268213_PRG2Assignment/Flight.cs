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
    abstract class Flight : IComparable<Flight>
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public string SpecialRequestCode { get; set; }
        public string AirlineCode { get; set; }
        public BoardingGate BoardingGateAssigned { get; set; } = null;
        public Flight() { }
        public Flight(string flightnumber,string origin,string destination,DateTime expectedtime,string requestcode)
        {
            FlightNumber = flightnumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedtime;
            Status = "Scheduled";

            if (requestcode == "")
            {
                SpecialRequestCode = "None";
            }
            else
            {
                SpecialRequestCode = requestcode;
            }

            AirlineCode = flightnumber.Substring(0, 2);

        }
        public int CompareTo(Flight other)
        {
            return this.ExpectedTime.CompareTo(other.ExpectedTime); // Descending order
        }

        public virtual double CalculateFees()
        {
            double fees = 0;
            if (Origin == "Singapore (SIN)")
            {
                //Departure fee
                fees += 800;
            }
            else
            {
                //Arriving fee
                fees += 500;
                if (Origin == "Dubai (DXB)" || Origin == "Bangkok (BKK)" || Origin == "Tokyo (NRT)")
                {
                    fees -= 25;
                }
            }

            bool Before_11am = ExpectedTime < Convert.ToDateTime("11:00 am");
            bool After_9pm = ExpectedTime > Convert.ToDateTime("9:00 am");
            if (Before_11am || After_9pm )
            {
                fees -= 110;
            }
            return fees;
        }


        public override string ToString()
        {
            return $"Flight Number:{FlightNumber}   Origin:{Origin}   Destination:{Destination}  Expected Time:{ExpectedTime}  Status:{Status}";
        }
    }
}
