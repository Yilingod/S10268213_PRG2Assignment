using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        //public bool AddFlight(Flight flight)
        //{

        //}
        //public double CalculateFees()
        //{

        //}
        //public bool RemoveFlight(Flight flight)
        //{

        //}
        //public override string ToString()
        //{

        //}


    }
}
