using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace S10268213_PRG2Assignment
{//==========================================================
 // Student Number	: S10268213K
 // Student Name	: Gong Yilin
 // Partner Name	: Yang Ee Ming
 //==========================================================
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }
        public LWTTFlight() : base() { }
        public LWTTFlight(string flightnumber, string origin, string destination, DateTime expectedtime, string requestcode) : base(flightnumber, origin, destination, expectedtime,requestcode)
        {
            RequestFee = 500;
        }
        public override double CalculateFees()
        {
            return base.CalculateFees() + RequestFee;
        }
        public override string ToString()
         {
            return base.ToString();
        }

    }
}
