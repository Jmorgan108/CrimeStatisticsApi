using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeStatisticsApi.Helpers
{
    public class DateValidationHelper
    {
        public static bool IsValidMonth(string month, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(month) || month.Length != 7 || month[4] != '-')
            {
                errorMessage = "Invalid format. Please use YYYY-MM (e.g., 2024-11)";
                return false;
            }

            // Try to parse the month using the correct format by checking the year and month parts separately
            var parts = month.Split('-');
            if (!int.TryParse(parts[0], out var year) || !int.TryParse(parts[1], out var monthNum))
            {
                errorMessage = "Invalid month format. Please use YYYY-MM";
                return false;
            }

            if (monthNum < 1 || monthNum > 12)
            {
                errorMessage = "Month must be between 01 and 12";
                return false;
            }

            // Check if date is in the future
            var inputDate = new DateTime(year, monthNum, 1);
            if (inputDate > DateTime.Now)
            {
                errorMessage = "Please enter a date previous to today";
                return false;
            }

            return true;
        }


    }
}
