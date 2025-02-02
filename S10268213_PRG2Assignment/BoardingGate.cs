using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//==========================================================
// Student Number	: S10268213K
// Student Name	: Gong Yilin
// Partner Name	: Yang Ee Ming
//==========================================================
namespace S10268213_PRG2Assignment
{
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportDDJB { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportLWTT { get; set; }
        public Flight Flight { get; set; } = null;
        public BoardingGate() { }
        public BoardingGate(string gatename, bool sddjb, bool scfft,bool slwtt)
        {
            GateName = gatename;
            SupportDDJB = sddjb;
            SupportsCFFT = scfft;
            SupportLWTT = slwtt;

        }
        public double CalculateFees()
        {
            double GateFee = 300;
            return GateFee;
        }
        public override string ToString()
        {
            return $"Gate Name: {GateName}\t DDJB: {SupportDDJB}\t CFFT:{SupportsCFFT}\t LWTT: {SupportLWTT}";
        }
    }
}
