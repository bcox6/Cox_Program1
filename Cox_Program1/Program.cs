/*
 * 
 * Program.cs
 * 06/1/2021
 * 
*/

using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Cox_Program1
{
    class Program
    {
        private static string DisplayHeader()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Objective:\n");
            stringBuilder.Append("Calculate the pressure exerted by a gas in a container given the following inputs from the user: the name of " +
                                 "the gas, the volume of the gas container in cubic meters, the weight of the gas in grams and the temperature " +
                                 "of the gas in degrees Celsius.\n");
            return stringBuilder.ToString();
        }

        static void GetMolecularWeights(out string[] gasNames, out double[] molecularWeights, out int count, string csvPath)
        {

            string[] readText = File.ReadAllLines(csvPath);
            gasNames = new string[readText.Length - 1];
            molecularWeights = new double[readText.Length - 1];
            count = readText.Length - 1;
            for (int i = 0; i < readText.Length; ++i)
            {
                if (i > 0)
                {
                    string[] splitText = readText[i].Split(',');
                    gasNames[i - 1] = splitText[0];
                    molecularWeights[i - 1] = double.Parse(splitText[1]);
                }
            }
        }

        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Console.WriteLine("Gas Names\n");
            for (int i = 0; i < gasNames.Length; i++)
            {
                Console.Write("{0,-20}",gasNames[i]);
                if (i % 3 == 2)
                    Console.WriteLine();
            }
            Console.WriteLine(stringBuilder.ToString());
        }

        private static double GetMolecularWeightFromName(string gasName, string[] gasNames, double[] molecularWeights, int countGases)
        {
            for (int i = 0; i < countGases; i++)
            {
                if (gasName.Equals(gasNames[i], StringComparison.CurrentCultureIgnoreCase))
                {
                    return molecularWeights[i];
                }
            }
            Console.WriteLine("Gas not found in the list. Verify name is correct.");
            return 0.0;
        }

        static double Pressure(double mass, double vol, double temp, double molecularWeight)
        {
            double returnValue = 0.0;
            double R = 8.3145;

            double numOfMoles = NumberOfMoles(mass, molecularWeight);
            double tempKelvin = CelciusToKelvin(temp);

            returnValue = (R * numOfMoles * tempKelvin) / vol;

            return returnValue;
        }

        static double NumberOfMoles(double mass, double molecularWeight)
        {
            return mass/molecularWeight;
        }

        static double CelciusToKelvin(double celcius)
        {
            return celcius + 273.15;
        }

        private static void DisplayPressure(double pressure)
        {
            double psi = PaToPSI(pressure);
            Console.WriteLine($"The Pressure of the selected gas is: {pressure} pascal and {psi} PSI");
        }

        static double PaToPSI(double pascals)
        {
            return pascals * 0.000145038;
        }

        private static string AskUserInputString(string question)
        {
            Console.Write(question);
            return Console.ReadLine();
        }

        private static double AskUserInputDouble(string question)
        {
            double returnValue = 0.0;
            bool error;
            do
            {
                //Error catching when trying to parse out the users information into a double.
                try
                {
                    returnValue = double.Parse(AskUserInputString(question));
                    error = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a number.");
                    error = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("An unknown exception has occured.");
                    error = true;
                }
            } while (error == true);
            return returnValue;
        }

        static void Main(string[] args)
        {
            //Declaring Variables.
            string[] gasNames;
            double[] molecularWeight;
            int elementCount = 0;
            int totalCount = 0;
            string another = "y";
            string userAnswer;
            double userVolumne = 0.0;
            double userMass = 0.0;
            double userTemperature = 0.0;
            double weight;
            double calcPressure;

            //Starting Program
            Console.Write(DisplayHeader());

            //Filling out the arrays from csv.
            GetMolecularWeights(out gasNames, out molecularWeight, out totalCount, @".\MolecularWeightsGasesAndVapors (1).csv");

            //Displaying the Gas Names in 3 columns.
            DisplayGasNames(gasNames, elementCount);
            while (another == "y")
            {
                userAnswer = AskUserInputString("Enter the name of the gas: ");
                weight = GetMolecularWeightFromName(userAnswer, gasNames, molecularWeight, totalCount);
                if (weight > 0.0)
                {

                    userVolumne = AskUserInputDouble("Enter the volumne of the gas in cubic meters: ");
                    userMass = AskUserInputDouble("Enter the mass of the gas in grams: ");
                    userTemperature = AskUserInputDouble("Enter the temperature of the gas in celcius: ");

                    calcPressure = Pressure(userMass, userVolumne, userTemperature, weight);

                    DisplayPressure(calcPressure);
                    Console.Write("\nDo Another(y/n): ");
                    another = Console.ReadLine();
                }
            }
            Console.WriteLine("\nThank you for using my program to calculate the pressure of gas. Please come back and try again.");
        }
    }

}

