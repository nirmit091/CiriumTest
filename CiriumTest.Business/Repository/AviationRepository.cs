using CiriumTest.Business.AppConstants;
using CiriumTest.Business.Interface;
using CiriumTest.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CiriumTest.Business.Repository
{
    public class AviationRepository : IAviationRepository
    {
        public static List<AircraftModel> aircraftData = new List<AircraftModel>();

        /// <summary>
        /// Used to save user input into variable for calculation
        /// </summary>
        /// <param name="aircraftType">Airbus or Boeing</param>
        /// <param name="minutes">Minutes passed in the input with aircraft</param>
        public void SetAviationData(string aircraftType, decimal minutes)
        {
            aircraftData.Add(new AircraftModel
            {
                Aircraft = aircraftType,
                Minutes = minutes
            });
        }

        /// <summary>
        /// Used to calculate Hours and minutes based on details provided in input
        /// </summary>
        /// <returns>Calcuate minutes in Hours:Minutes</returns>
        public string Calculate()
        {
            StringBuilder sb = new StringBuilder();

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            if (aircraftData.Count > 0)
            {
                var calculateData = aircraftData.GroupBy(x => x.Aircraft).Select(y => new
                {
                    Aircraft = y.Key,
                    Minutes = y.Sum(a => a.Minutes)
                }).ToList();

                if (calculateData.Count > 0)
                {
                    foreach (var item in calculateData)
                    {
                        sb.AppendLine(textInfo.ToTitleCase(item.Aircraft) + " " + string.Format("{0}:{1:00}", (int)item.Minutes / 60, item.Minutes % 60));
                    }
                }
            }

            return sb.ToString().Trim();
        }
    }
}
