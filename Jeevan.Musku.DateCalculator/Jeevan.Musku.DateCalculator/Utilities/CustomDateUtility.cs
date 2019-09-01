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
            var diffInDays = 0;
            var fromArray = TransformDateToArray(fromDate);
            var toArray = TransformDateToArray(toDate);

            if (toArray[2] > fromArray[2])
            {
                var yearDiff = toArray[2] - fromArray[2];
                var noOfLeapYears = Math.Round((double)(yearDiff/4), MidpointRounding.ToEven);
                diffInDays = (int)(yearDiff * 365 + noOfLeapYears);
            }

            var daysInYearDiff = DaysInYear(toArray) - DaysInYear(fromArray);
            diffInDays = diffInDays + daysInYearDiff;
            return diffInDays;
        }

        //To calculate number of days with in the year
        internal int DaysInYear(int[] dateArray)
        {
            int days = 0;
            for (int i = 1; i < dateArray[1]; i++)
            {
                days = days + DaysInMonth[i];
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