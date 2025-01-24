using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268213_PRG2Assignment
{
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportDDJB { get; set; }
        public bool SupportLWTT { get; set; }
        public Flight Flight { get; set; }
        public BoardingGate() { }
        public BoardingGate(string gatename,bool scfft,bool sddjb,bool slwtt,Flight flight)
        {
            GateName = gatename;
            SupportDDJB = sddjb;
            SupportLWTT = slwtt;
            SupportsCFFT = scfft;
            Flight = flight;
        }
        public double CalculateFees()
        {

        }
        public override string ToString()
        {

        }
    }
}
