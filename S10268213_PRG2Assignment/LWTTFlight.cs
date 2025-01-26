using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace S10268213_PRG2Assignment
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }
        public LWTTFlight() : base() { }
        public LWTTFlight(string flightnumber, string origin, string destination, DateTime expectedtime) : base(flightnumber, origin, destination, expectedtime)
        {
            RequestFee = 500;
        }


        //    public override double CalculateFees()
        //    {

        //    }
        //    public override string ToString()
        //    {

        //    }
        
    }
}
