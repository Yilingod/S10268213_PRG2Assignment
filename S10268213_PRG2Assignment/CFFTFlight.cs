﻿using System;
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
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }

        public CFFTFlight() : base() { }
        public CFFTFlight(string flightnumber, string origin, string destination, DateTime expectedtime) : base(flightnumber, origin, destination, expectedtime)
        {
            RequestFee = 150;
        }
        public override double CalculateFees()
        {
            return base.CalculateFees() + RequestFee;
        }
        //    public override string ToString()
        //    {

        //    }

    }
}
