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
using Microsoft.VisualBasic;
using System.Collections.Immutable;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            AssignFlightToAirline(flights, airlines);

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
                    DisplayBoardingGates(boardingGates);
                }
                else if (option == "3")
                {
                    AssignBoardingGate(boardingGates, flights);

                }
                else if (option == "4")
                {
                    CreateNewFlight(flights);
                        
                }
                else if (option == "5")
                {
                    DisplayAirlineFlights(airlines);
                }
                else if (option == "6")
                {
                    ;
                }
                else if (option == "7")
                {
                    DisplayFlightSchedule(flights,airlines,boardingGates);
                }
                else if (option == "8")
                {
                    AssignAllFlightToGate(flights, boardingGates,airlines);
                }
                else if (option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter again.");
                }
            }
        }
        public class InvalidInputException : Exception
        {
            public InvalidInputException(string message) : base(message) { }
        }

        static void AssignAllFlightToGate(Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, Airline> airlines)
        {
            Queue<Flight> FlightQueue = new Queue<Flight>();

            foreach (Flight flight in flights.Values)
            {
                if (!boardingGates.Values.Any(gate => gate.Flight == flight))
                {
                    FlightQueue.Enqueue(flight);
                }
            }
            int unprocessedFlight = FlightQueue.Count();
            Console.WriteLine($"Total of {unprocessedFlight} Flights are not assign to boarding gate.");

            List<BoardingGate> UnassignedGates = new List<BoardingGate>();

            foreach (BoardingGate gate in boardingGates.Values)
            {
                if (gate.Flight is null)
                {
                    UnassignedGates.Add(gate);
                }
            }
            int unprocessedGate = UnassignedGates.Count();
            Console.WriteLine($"Total of {unprocessedGate} boarding gates are not assign with any flight .");



            foreach (Flight flight in FlightQueue.ToList())
            {


                string airlineCode = flight.FlightNumber.Substring(0, 2);
                string Time = flight.ExpectedTime.ToString();
                string requestCode = "None";
                string AssignedGate = "";
                BoardingGate currectgate = new BoardingGate();

                if (flight is DDJBFlight)
                {
                    foreach (BoardingGate gate in UnassignedGates)
                    {

                        if (gate.SupportDDJB)
                        {
                            boardingGates[gate.GateName].Flight = flight;
                            requestCode = "DDJB";
                            AssignedGate = gate.GateName;
                            currectgate = gate;
                            FlightQueue.Dequeue();
                            break;
                        }

                    }
                }
                else if (flight is CFFTFlight)
                {
                    foreach (BoardingGate gate in UnassignedGates)
                    {
                        if (gate.SupportsCFFT)
                        {
                            boardingGates[gate.GateName].Flight = flight;
                            requestCode = "CFFT";
                            AssignedGate = gate.GateName;
                            currectgate = gate;
                            FlightQueue.Dequeue();
                            break;
                        }

                    }
                }
                else if (flight is LWTTFlight)
                {
                    foreach (BoardingGate gate in UnassignedGates)
                    {
                        if (gate.SupportLWTT)
                        {
                            boardingGates[gate.GateName].Flight = flight;
                            requestCode = "LWTT";
                            AssignedGate = gate.GateName;
                            currectgate = gate;
                            FlightQueue.Dequeue();
                            break;
                        }

                    }
                }
                else
                {
                    foreach (BoardingGate gate in UnassignedGates)
                    {
                        if (!gate.SupportDDJB && !gate.SupportsCFFT && !gate.SupportLWTT)
                        {
                            boardingGates[gate.GateName].Flight = flight;
                            AssignedGate = gate.GateName;
                            currectgate = gate;
                            FlightQueue.Dequeue();
                            break;
                        }
                       
                    }
                }
                UnassignedGates.Remove(currectgate);
                Console.WriteLine($"{flight.FlightNumber,-15}{airlines[airlineCode].Name,-25}{flight.Origin,-25}" +
                    $"{flight.Destination,-22}{Time,-35}{requestCode,-20}{AssignedGate}");

            }
            int processedflight = unprocessedFlight - FlightQueue.Count();
            int processedgate = unprocessedGate - UnassignedGates.Count();

            Console.WriteLine($"{processedflight} Flights and {processedgate} Boarding Gates are processed and assigned. ");

            Console.WriteLine($"{FlightQueue.Count()}% of flights processed automatically over those already assigned.");
            //Console.WriteLine($"{(unprocessedFlight/(flights.Count() - unprocessedFlight)) *100 }% Flights and {(unprocessedGate / (boardingGates.Count() - unprocessedGate)) * 100}% Boarding Gates are processed and assigned. ");
        }
        static void DisplayFlightSchedule(Dictionary<string, Flight> flights, Dictionary<string, Airline> airlines, Dictionary<string, BoardingGate> boardingGates)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine();

            Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-25}{"Origin",-25}{"Destination",-22}{"Expected Departure/Arrival Time",-37}" +
                $"{"Status",-20}{"Boarding Gate"}");
            Console.WriteLine();

            List<Flight> FlightList = new List<Flight>(flights.Values);
            FlightList.Sort();

            foreach (Flight flight in FlightList)
            {
                string airlineCode = flight.FlightNumber.Substring(0, 2);
                string Time = flight.ExpectedTime.ToString();

                string GateStatus = "Unassigned";
                foreach (BoardingGate gate in boardingGates.Values)
                {
                    if (gate.Flight == flight)
                    {
                        GateStatus = gate.GateName;
                    }
                }

                Console.WriteLine($"{flight.FlightNumber,-15}{airlines[airlineCode].Name,-25}{flight.Origin,-25}{flight.Destination,-22}" +
                    $"{Time,-35}{flight.Status,-20}{GateStatus}");
            }
            Console.WriteLine();
        }
        static void AssignFlightToAirline(Dictionary<string, Flight> flights, Dictionary<string, Airline> airlines)
        {
            Console.WriteLine("Assigning Flights to corresponding Airline...");
            foreach ( Airline airline in airlines.Values)
            {
                
                foreach (Flight flight in flights.Values)
                {
                    string airlineCode = flight.FlightNumber.Substring(0, 2);
                    if (airlineCode == airline.Code)
                    {
                        airline.Flights[flight.FlightNumber] = flight;
                    }
                }
            }
            Console.WriteLine("Finished Assigning.");
            
        }

        static void DisplayAirlineFlights(Dictionary<string, Airline> airlines)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine();

            Console.WriteLine($"{"Airline Code",-15}{"Airline Name"}");
            foreach (Airline airline in airlines.Values)
            {
                Console.WriteLine($"{airline.Code,-15}{airline.Name}");
            }
            Console.Write("Enter Airline Code: ");
            string AirlineCode = Console.ReadLine();

            Console.WriteLine("=============================================");
            Console.WriteLine("List of Flights for Singapore Airlines");
            Console.WriteLine("=============================================");

            Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-25}{"Origin",-25}{"Destination",-22}{"Expected Departure/Arrival Time"}");
            Console.WriteLine();

            foreach (Flight flight in airlines[AirlineCode].Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber,-15}{airlines[AirlineCode].Name,-25}{flight.Origin,-25}" +
                    $"{flight.Destination,-22}{flight.ExpectedTime}");

            }


        }

        static void CreateNewFlight(Dictionary<string, Flight> flights)
        {
            while (true)
            {
                Console.WriteLine("=============================================");
                Console.WriteLine("Create a new flight");
                Console.WriteLine("=============================================");
                Console.WriteLine();

                try
                {
                    Console.Write("Enter Flight Number: ");
                    string FlightNo = Console.ReadLine();

                    if (flights.ContainsKey(FlightNo))
                    {
                        throw new InvalidInputException($"Flight number '{FlightNo}' already exist.");
                    }

                    Console.Write("Enter Origin: ");
                    string origin = Console.ReadLine();

                    Console.Write("Enter Destination: ");
                    string destination = Console.ReadLine();

                    Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                    DateTime expectedTime = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
                    string requestCode = Console.ReadLine();

                    Flight NewFlight;
                    if (requestCode == "DDJB")
                    {
                        NewFlight = new DDJBFlight(FlightNo, origin, destination, expectedTime,requestCode);
                    }
                    else if (requestCode == "CFFT")
                    {
                        NewFlight = new CFFTFlight(FlightNo, origin, destination, expectedTime,requestCode);
                    }
                    else if (requestCode == "LWTT")
                    {
                        NewFlight = new LWTTFlight(FlightNo, origin, destination, expectedTime,requestCode);
                    }
                    else if(requestCode == "None")
                    {
                        requestCode = null;
                        NewFlight = new NORMFlight(FlightNo, origin, destination, expectedTime,requestCode);
                        
                    }
                    else
                    {
                        throw new InvalidInputException($"Invalid input, please enter correct special request code. ");
                    }

                    flights[FlightNo] = NewFlight;
                    using (StreamWriter writer = new StreamWriter("flights.csv",append:true))
                    {
                        writer.WriteLine($"{FlightNo},{origin},{destination},{expectedTime},{requestCode}");
                    }
                    Console.WriteLine($"Flight {FlightNo} has been added!");
                    Console.WriteLine();
                    Console.Write("Would you like to add another flight? (Y/N): ");
                    string AddAnother = Console.ReadLine();

                    if (AddAnother == "Y")
                    {
                        ;
                    }
                    else if (AddAnother == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter again.");
                    }
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Invalid format entered.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }

        }
        static void AssignBoardingGate(Dictionary<string, BoardingGate> boardingGates, Dictionary<string, Flight> flights)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Assign a Boarding Gate to a Flight");
            Console.WriteLine("=============================================");
            Console.WriteLine();

            try
            {

                Console.Write("Enter Flight Number: ");
                string FlightNo = Console.ReadLine();

                if (!flights.ContainsKey(FlightNo))
                {
                    throw new KeyNotFoundException($"Flight number '{FlightNo}' does not exist.");

                }

                Console.Write("Enter Boarding Gate Name: ");
                string GateName = Console.ReadLine();

                if (!boardingGates.ContainsKey(GateName))
                {
                    throw new KeyNotFoundException($"Boarding gate '{GateName}' does not exist.");

                }
                else if (boardingGates[GateName].Flight is not null)
                {

                    throw new InvalidInputException($"Boarding gate '{GateName}' is already assigned to flight {boardingGates[GateName].Flight.FlightNumber}.");
                }
                Flight flightAssigned = flights[FlightNo];

                boardingGates[GateName].Flight = flightAssigned;

                Console.WriteLine($"Flight Number: {flightAssigned.FlightNumber}");
                Console.WriteLine($"Origin: {flightAssigned.Origin}");
                Console.WriteLine($"Destination: {flightAssigned.Destination}");
                Console.WriteLine($"Expected Time: {flightAssigned.ExpectedTime}");

                string requestcode = "None";
                if (flightAssigned is DDJBFlight)
                {
                    requestcode = "DDJB";
                }
                else if (flightAssigned is CFFTFlight)
                {
                    requestcode = "CFFT";
                }
                else if (flightAssigned is LWTTFlight)
                {
                    requestcode = "LWTT";
                }

                BoardingGate waitingGate = boardingGates[GateName];
                Console.WriteLine($"Special Request Code: {requestcode}");
                Console.WriteLine($"Boarding Gate Name: {waitingGate.GateName}");
                Console.WriteLine($"Supports DDJB: {waitingGate.SupportDDJB}");
                Console.WriteLine($"Supports CFFT: {waitingGate.SupportsCFFT}");
                Console.WriteLine($"Supports LWTT: {waitingGate.SupportLWTT}");
                Console.WriteLine();

                while (true)
                {
                    Console.Write("Would you like to update the status of the flight? (Y/N):");

                    string? comfirm = Console.ReadLine();
                    if (comfirm == "Y")
                    {
                        Console.WriteLine("1. Delayed");
                        Console.WriteLine("2. Boarding");
                        Console.WriteLine("3. On Time");
                        Console.Write("Please select the new status of the flight?: ");
                        string? choice = Console.ReadLine();

                        if (choice == "1")
                        {
                            waitingGate.Flight.Status = "Delayed";
                            Console.WriteLine($"Flight {FlightNo} has been assigned to Boarding Gate {GateName}!");
                        }
                        else if (choice == "2")
                        {
                            waitingGate.Flight.Status = "Boarding";
                            Console.WriteLine($"Flight {FlightNo} has been assigned to Boarding Gate {GateName}!");
                        }
                        else if (choice == "3")
                        {
                            waitingGate.Flight.Status = "On Time";
                            Console.WriteLine($"Flight {FlightNo} has been assigned to Boarding Gate {GateName}!");
                        }
                        else
                        {
                            Console.WriteLine($"Invalid input,Please enter number 1 to 3.");
                        }
                    }
                    else if (comfirm == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter again.");
                    }
                }
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid format entered.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
        static void DisplayBoardingGates(Dictionary<string, BoardingGate> boardingGates)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Gate Name", -15}{"DDJB", -12}{"CFFT", -12}{"LWTT",-12}{"Flight Assigned"}");

            foreach (BoardingGate gate in boardingGates.Values)
            {
                string? flightNo = null;
                if (gate.Flight is Flight)
                {
                    flightNo = gate.Flight.FlightNumber;
                }

                Console.WriteLine($"{gate.GateName, -15}{gate.SupportDDJB, -12}{gate.SupportsCFFT, -12}{gate.SupportLWTT,-12}{flightNo}");
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
                        Flight flight = new DDJBFlight(flightnumber, origin, destination, expectedtime,request_code);
                        flights[flightnumber] = flight;
                    }
                    else if (request_code == "CFFT")
                    {
                        Flight flight = new CFFTFlight(flightnumber, origin, destination, expectedtime,request_code);
                        flights[flightnumber] = flight;
                    }
                    else if (request_code == "LWTT")
                    {
                        Flight flight = new LWTTFlight(flightnumber, origin, destination, expectedtime, request_code);
                        flights[flightnumber] = flight;
                    }
                    else
                    {
                        Flight flight = new NORMFlight(flightnumber, origin, destination, expectedtime, request_code);
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


