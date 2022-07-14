using CiriumTest.Business.AppConstants;
using CiriumTest.Business.Interface;
using CiriumTest.Business.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CiriumTest
{
    /// <summary>
    /// Programm execution class
    /// </summary>
    public class AviationBase
    {
        //Dependacy Injection
        private static readonly ServiceProvider _serviceProvider
                   = new ServiceCollection()
                       .AddTransient<IAviationRepository, AviationRepository>()
                       .AddTransient<AviationBase>()
                       .BuildServiceProvider();

        private readonly IAviationRepository _aviationRepository;

        public AviationBase() { }

        public AviationBase(IAviationRepository aviationRepository)
        {
            _aviationRepository = aviationRepository;
        }

        /// <summary>
        /// Method to initiate the application
        /// </summary>
        static void Main(string[] args)
        {
            string input = string.Empty;
            Console.WriteLine("Welcome to my Cirium aviation");
            Console.WriteLine("Enter Airbus and Boieng details for Example:  “airbus 60” or “boeing 45”");
            Console.WriteLine("Enter “calculate” to display the total numbers for each aircraft in hours:minutes");
            Console.WriteLine("Enter “exit” to stop the process");
            Console.WriteLine("");

            do
            {
                Console.Write("Please provide your input: ");

                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    string result = SetAndCalculate(input);

                    if (!string.IsNullOrWhiteSpace(result))
                        Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine(AppContants.InputCanNotBeEmpty);
                }
            }
            while (!input.Contains(AppContants.Airbus) || !input.Contains(AppContants.Boeing) || input != AppContants.Exit);
        }

        /// <summary>
        /// Used to set and validate parameters pass by the user for the calculation
        /// </summary>
        /// <param name="input">Value passed by user</param>
        public static string SetAndCalculate(string input)
        {
            string result = string.Empty;
            input = input.ToLower();
            var splitString = input.Split(' ');

            if (string.IsNullOrWhiteSpace(input))
            {
                result = AppContants.InputCanNotBeEmpty;
            }
            else if (input == AppContants.Calculate)
            {
                result = _serviceProvider.GetService<AviationBase>().Calculate();
            }
            else if (input == AppContants.Exit)
            {
                Environment.Exit(0);
            }
            else if (splitString.Length > 1)
            {
                if (splitString[0] == AppContants.Airbus || splitString[0] == AppContants.Boeing)
                {
                    decimal minute;
                    if (Decimal.TryParse(splitString[1], out minute))
                    {
                        _serviceProvider.GetService<AviationBase>().SetAviationData(splitString[0], minute);
                        result = AppContants.InputSavedForCalculation;
                    }
                    else
                    {
                        result = AppContants.EnterValidMinuteValue;
                    }
                }
                else
                {
                    result = AppContants.NotValidInput;
                }
            }
            else
            {
                result = AppContants.NotValidInput;
            }

            return result;
        }

        /// <summary>
        /// Used to call service method for the calculation
        /// </summary>
        /// <returns></returns>
        private string Calculate()
        {
            return _aviationRepository.Calculate();
        }

        /// <summary>
        /// Used to call service method to set aviation data
        /// </summary>
        /// <param name="aircraftType">AircraftType: Airbus or Boeing</param>
        /// <param name="minutes">Minutes to calculate total</param>
        private void SetAviationData(string aircraftType, decimal minutes)
        {
            _aviationRepository.SetAviationData(aircraftType, minutes);
        }
    }
}
