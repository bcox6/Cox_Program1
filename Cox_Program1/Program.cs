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

        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {
            count = 0;
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
            for (int i = 0; i < gasNames.Length; ++i)
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
            double psi = pressure * 0.000145038;
            Console.WriteLine($"The Pressure of the selected gas is: {pressure} pascal and {psi} PSI");
        }

        static double PaToPSI(double pascals)
        {
            double returnValue = 0.0;

            return returnValue;
        }

        private static string AskUserInput(string question)
        {
            Console.Write(question);
            return Console.ReadLine();
        }

        private static void ReadAllLinesFromCsv(string csvPath, out string[] gasNames, out double[] gasWeight)
        {
            string[] readText = File.ReadAllLines(csvPath);
            gasNames = new string[readText.Length - 1];
            gasWeight = new double[readText.Length - 1];
            for (int i = 0; i < readText.Length; ++i)
            {
                if (i > 0)
                {
                    string[] splitText = readText[i].Split(',');
                    gasNames[i - 1] = splitText[0];
                    gasWeight[i - 1] = double.Parse(splitText[1]);
                }
            }
        }

        static void Main(string[] args)
        {
            //Declaring Variables.
            string[] gasNames;
            double[] molecularWeight;
            int elementCount = 0;
            string another = "y";
            bool error;
            string userAnswer;
            double userVolumne = 0.0;
            double userMass = 0.0;
            double userTemperature = 0.0;
            double weight;
            double calcPressure;

            //Starting Program
            Console.Write(DisplayHeader());
            ReadAllLinesFromCsv(@".\MolecularWeightsGasesAndVapors (1).csv", out gasNames, out molecularWeight);
            DisplayGasNames(gasNames, elementCount);
            while (another == "y")
            {
                error = false;
                userAnswer = AskUserInput("Enter the name of the gas: ");
                weight = GetMolecularWeightFromName(userAnswer, gasNames, molecularWeight, elementCount);
                if (weight > 0.0)
                {
                    do
                    {
                        try
                        {
                            userVolumne = double.Parse(AskUserInput("Enter the volumne of the gas in cubic meters: "));
                            userMass = double.Parse(AskUserInput("Enter the mass of the gas in grams: "));
                            userTemperature = double.Parse(AskUserInput("Enter the temperature of the gas in celcius: "));
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

                    calcPressure = Pressure(userMass, userVolumne, userTemperature, weight);

                    DisplayPressure(calcPressure);

                    if (error != true)
                    {
                        Console.Write("\nDo Another(y / n): ");
                        another = Console.ReadLine();
                    }
                }
                Console.WriteLine("Thank you for using my program to calculate the pressure of gas. Please come back and try again.");
            }
        }
    }

}

