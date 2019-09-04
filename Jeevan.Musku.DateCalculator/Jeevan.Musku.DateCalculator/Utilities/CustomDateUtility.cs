using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebGrease.Css.Extensions;

namespace Jeevan.Musku.DateCalculator.Utilities
{
    public class CustomDateUtility
    {
        //Month and number of days in that month as a dictionary
        internal IDictionary<int, int> DaysInMonth = new Dictionary<int, int>()
        {{1, 31}, {2, 28}, {3,31}, {4, 30}, {5, 31}, {6,30}, {7, 31}, {8, 31}, {9,30}, {10, 31}, {11, 30}, {12,31} };

        //To validate the model, if the given dates were valid and toDate is greater than fromDate.
        //Used by CustomDate DateValidator.
        public bool CheckIfDatesValid(string fromDate, string toDate)
        {
            var fromArray = TransformDateToArray(fromDate);
            var toArray = TransformDateToArray(toDate);
            if (toArray[2] > fromArray[2])
            {
                return true;
            }
            else if (toArray[2] == fromArray[2])
            {
                if (toArray[1] > fromArray[1])
                {
                    return true;
                }
                else if (toArray[1] == fromArray[1])
                {
                    if (toArray[0] > fromArray[0])
                    {
                        return true;
                    }
                    else if (toArray[0] == fromArray[0])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //To calculate the difference between dates in number of days.
        public int CalculateDifferenceInDays(string fromDate, string toDate)
        {
            int diffInDays = 0;
            var fromArray = TransformDateToArray(fromDate);
            var toArray = TransformDateToArray(toDate);

            if (toArray[2] > fromArray[2])
            {
                diffInDays = (toArray[2] - fromArray[2]) * 365;
                diffInDays += CalculateLeapYearDays(fromArray, toArray);
            }

            int daysInYearDiff = DaysInYear(toArray) - DaysInYear(fromArray);
            diffInDays += daysInYearDiff;
            return diffInDays;
        }

        //Calculate Leap Year days
        internal int CalculateLeapYearDays(int[] fromArray, int[] toArray)
        {
            int leapYearDays = 0;

            int leapYear = fromArray[2];
            while (!IsLeapYear(leapYear))
            {
                leapYear++;
            }

            leapYearDays = (int)Math.Round(((toArray[2] - leapYear) / 4.0), MidpointRounding.ToEven);

            leapYearDays = leapYearDays > 1 ? leapYearDays - 1 : 0;

            if (IsLeapYear(fromArray[2]))
            {
                leapYearDays += fromArray[1] < 3 ? 1 : 0;
            }

            if (IsLeapYear(toArray[2]))
            {
                leapYearDays += toArray[1] > 2 ? 1 : 0;
            }
            return leapYearDays;
        }

        //Leap Year Check
        internal bool IsLeapYear(int year)
        {
            if ((year % 400) == 0)
                return true;
            else if ((year % 100) == 0)
                return false;
            else if ((year % 4) == 0)
                return true;
            else
                return false;
        }

        //To calculate number of days with in the year
        internal int DaysInYear(int[] dateArray)
        {
            int days = 0;
            for (int i = 1; i < dateArray[1]; i++)
            {
                days += DaysInMonth[i];
            }
            return days + dateArray[0];
        }

        //To convert string into integer array
        internal int[] TransformDateToArray(string date)
        {
            var dateArray = date.Split('/');
            return dateArray.Select(s => Convert.ToInt32(s)).ToArray();
        }
    }
}