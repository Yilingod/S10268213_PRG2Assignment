using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number	: S10268213K
// Student Name	: Gong Yilin
// Partner Name	: Yang Ee Ming
//==========================================================
namespace S10268213_PRG2Assignment
{
    class NORMFlight : Flight
    {


        public NORMFlight() : base() { }
        public NORMFlight(string flightnumber, string origin, string destination, DateTime expectedtime,string requestcode) : base(flightnumber, origin, destination, expectedtime,requestcode)
        {

        }
        public override double CalculateFees()
        {
            return base.CalculateFees() - 50;
        }
        //    public override string ToString()
        //    {

        //    }


    }
}
