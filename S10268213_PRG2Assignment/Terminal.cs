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
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string,Airline> Airlines { get; set; }
        public Dictionary<string,Flight> Flights { get; set; }
        public Dictionary<string,BoardingGate> BoardingGates { get; set; }
        public Dictionary<string,double> GateFees { get; set; }

        public Terminal() { }
        public Terminal(string terminalName,Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, double> gateFees)
        {
            TerminalName = terminalName;
            Airlines = airlines;
            Flights = flights;
            BoardingGates = boardingGates;
            GateFees = gateFees;

        }
        public bool AddAirline(Airline airline)
        {
            if (!Airlines.ContainsKey(airline.Code))
            {
                Airlines[airline.Code] = airline;
                return true;
            }
            return false;
        }

        public bool AddBoardingGate(BoardingGate gate)
        {
            if (!BoardingGates.ContainsKey(gate.GateName))
            {
                BoardingGates[gate.GateName] = gate;
                return true;
            }
            return false;
        }

    }
}
