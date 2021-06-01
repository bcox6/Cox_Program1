/*
 * 
 * Cox_Program1
 * bcox6@cnm.edu
 * Program.cs
 * 06/1/2021
 * 
*/

using System;
using System.Text;

namespace Cox_Program1
{
    class Program
    {
        private static string DisplayHeader()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Name: Brian Cox\n");
            stringBuilder.Append("Program Name: Cox_Program1\n");
            stringBuilder.Append("Objective:\n");
            stringBuilder.Append("Calculate the pressure exerted by a gas in a container given the following inputs from the user: the name of " +
                                 "the gas, the volume of the gas container in cubic meters, the weight of the gas in grams and the temperature " +
                                 "of the gas in degrees Celsius.");
            return stringBuilder.ToString();
        }

        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {

        }

        private static void DisplayGasNames(string[] gasNames, int countGases)
        {

        }

        private static double GetMolecularWeightFromName(string gasName, string[] gasNames, double[] molecularWeights, int countGases)
        {

        }

        static double Pressure(double mass, double vol, double temp, double molecularWeight)
        {

        }

        static double NumberOfMoles(double mass, double molecularWeight)
        {

        }

        static double CelciusToKelvin(double celcius)
        {

        }

        private static void DisplayPresure(double pressure)
        {

        }

        static double PaToPSI(double pascals)
        {

        }

        static void Main(string[] args)
        {
            Console.Write(DisplayHeader());
        }
    }
}
