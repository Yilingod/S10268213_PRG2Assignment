using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Net.Http.Headers;
using System.Linq;
using S10268213_PRG2Assignment;
using System.Buffers.Text;




namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            // Loading in the spreadsheet
            Dictionary<string, Airline> airlines = LoadAirlines("airlines.csv");
            Dictionary<string, BoardingGate> boardingGates = LoadBoardingGates("boardinggates.csv");

        // Airlines dictionary
        static Dictionary<string, Airline> LoadAirlines(string filePath)
        {
            var airlines = new Dictionary<string, Airline>();

            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine(); // Skipping the header row

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] data = line.Split('\t'); // Use comma as CSV separator

                    string name = data[0]; // Name is in the first column
                    string code = data[1]; // Code is in the second column

                    // Create an Airline object and add it to the dictionary
                    Airline airline = new Airline(name, code);
                    airlines[code] = airline;
                }
            }

            return airlines;
        }

        // Load Boarding Gates method
        static Dictionary<string, BoardingGate> LoadBoardingGates(string filePath)
        {
            var boardingGates = new Dictionary<string, BoardingGate>();

            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine(); // Skip header row

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] data = line.Split('\t'); // Use comma as CSV separator

                    string gateCode = data[0]; 
                    bool ddjb = bool.Parse(data[1]);
                    bool cfft = bool.Parse(data[2]); 
                    bool lwtt = bool.Parse(data[3]); 

                    // Create a BoardingGate object and add it to the dictionary
                    BoardingGate gate = new BoardingGate(gateCode, ddjb, cfft, lwtt);
                    boardingGates[gateCode] = gate;
                }
            }

            return boardingGates;
        }
    }
}


