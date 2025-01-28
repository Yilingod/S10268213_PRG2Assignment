using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Net.Http.Headers;
using System.Linq;
using S10268213_PRG2Assignment;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

//==========================================================
// Student Number	: S10268213K
// Student Name	: Gong Yilin
// Partner Name	: Yang Ee Ming
//==========================================================

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {

            // Loading in the spreadsheet
            Dictionary<string, Airline> airlines = LoadAirlines("airlines.csv");
            Dictionary<string, BoardingGate> boardingGates = LoadBoardingGates("boardinggates.csv");
            Dictionary<string, Flight> flights = LoadFlights("flights.csv");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            while (true)
            {
                DisplayMenu();
                string? option = Console.ReadLine();
                Console.WriteLine();

                if (option == "1")
                {
                    ListAllFlights(flights, airlines);
                }
                else if (option == "2")
                {
                    ;
                }
                else if (option == "3")
                {
                    ;

                }
                else if (option == "4")
                {
                    ;
                }
                else if(option == "5")
                {
                    ;
                }
                else if (option == "6")
                {
                    ;
                }
                else if (option == "7")
                {
                    ;
                }
                else if(option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter again.");
                }

            }
            
        }
        static void DisplayBoaardingGates(Dictionary<string, BoardingGate> boardingGates)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Gate Name", -12}{"DDJB", -10}{"CFFT", -10}{"LWTT"}");

            foreach (BoardingGate gate in boardingGates.Values)
            {
                Console.WriteLine($"{gate.GateName, -12}{gate.SupportDDJB, -10}{gate.SupportsCFFT, -10}{gate.SupportLWTT}");
            }


        }
        static void ListAllFlights(Dictionary<string, Flight> flights, Dictionary<string, Airline> airlines)
        {
            Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-25}{"Origin",-25}{"Destination",-22}{"Expected Departure/Arrival Time"}");
            Console.WriteLine();
            foreach (Flight flight in flights.Values)
            {
                string airlineCode = flight.FlightNumber.Substring(0, 2);
                string Time = flight.ExpectedTime.ToString();
                Console.WriteLine($"{flight.FlightNumber,-15}{airlines[airlineCode].Name,-25}{flight.Origin,-25}{flight.Destination,-22}{Time}");
            }
            Console.WriteLine();
        }
        static Dictionary<string, Flight> LoadFlights(string filePath)
        {
            Console.WriteLine("Loading Flights...");
            var flights = new Dictionary<string, Flight>();
            int flightNum = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine(); // Skip header row

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] data = line.Split(','); // Use comma as CSV separator

                    string flightnumber = data[0];
                    string origin = data[1];
                    string destination = data[2];
                    DateTime expectedtime = Convert.ToDateTime(data[3]);
                    string request_code = data[4];


                    if (request_code == "DDJB")
                    {
                        Flight flight = new DDJBFlight(flightnumber, origin, destination, expectedtime);
                        flights[flightnumber] = flight;
                    }
                    else if (request_code == "CFFT")
                    {
                        Flight flight = new CFFTFlight(flightnumber, origin, destination, expectedtime);
                        flights[flightnumber] = flight;
                    }
                    else if (request_code == "LWTT")
                    {
                        Flight flight = new LWTTFlight(flightnumber, origin, destination, expectedtime);
                        flights[flightnumber] = flight;
                    }
                    else
                    {
                        Flight flight = new NORMFlight(flightnumber, origin, destination, expectedtime);
                        flights[flightnumber] = flight;
                    }
                    flightNum++;
                }
            }
            Console.WriteLine($"{flightNum} Flights Loaded!");
            return flights;
        }
        static Dictionary<string, BoardingGate> LoadBoardingGates(string filePath)
        {
            Console.WriteLine("Loading Boarding Gates...");
            var boardingGates = new Dictionary<string, BoardingGate>();
            int boardingGateNum = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine(); // Skip header row

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] data = line.Split(','); // Use comma as CSV separator

                    string gateCode = data[0];
                    bool DDJB = bool.Parse(data[1]);
                    bool CFFT = bool.Parse(data[2]);
                    bool LWTT = bool.Parse(data[3]);

                    // Create a BoardingGate object and add it to the dictionary
                    BoardingGate gate = new BoardingGate(gateCode, DDJB, CFFT, LWTT);
                    boardingGates[gateCode] = gate;
                    boardingGateNum++;
                }
            }
            Console.WriteLine($"{boardingGateNum} Boarding Gates Loaded!");
            return boardingGates;
        }
        static Dictionary<string, Airline> LoadAirlines(string filePath)
        {
            Console.WriteLine("Loading Airlines...");
            var airlines = new Dictionary<string, Airline>();
            int airlineNum = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine(); // Skipping the header row

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] data = line.Split(","); // Use comma as CSV separator

                    string name = data[0]; // Name is in the first column
                    string code = data[1]; // Code is in the second column

                    // Create an Airline object and add it to the dictionary
                    Airline airline = new Airline(name, code);
                    airlines[code] = airline;
                    airlineNum++;
                }
            }
            Console.WriteLine($"{airlineNum} Airlines Loaded!");
            return airlines;
        }

        static void DisplayMenu()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Welcome to Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine("1. List All Flights");
            Console.WriteLine("2. List Boarding Gates");
            Console.WriteLine("3. Assign a Boarding Gate to a Flight");
            Console.WriteLine("4. Create Flight");
            Console.WriteLine("5. Display Airline Flights");
            Console.WriteLine("6. Modify Flight Details");
            Console.WriteLine("7. Display Flight Schedule");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.Write("Please select your option: ");
        }
        
    }
}


