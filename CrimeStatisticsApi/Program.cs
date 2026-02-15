using CrimeStatisticsByLongitudeAndLatitude.Services;

var apiService = new CrimeApiService();
var summaryService = new CrimesSummaryService();

//Added a loop so the application continures to run when the user enters invalid input, allowing them to correct it without restarting the application.
bool continueRunning = true;
while (continueRunning)
{
    Console.WriteLine("Enter latitude: ");
    if (!double.TryParse(Console.ReadLine(), out var latitudeInput) ||
        latitudeInput < -90 || latitudeInput > 90)
    {
        Console.WriteLine("Invalid latitude. Must be between -90 and 90.");
        Console.WriteLine();
        continue;
    }

    Console.WriteLine("Enter longitute: ");
    if (!double.TryParse(Console.ReadLine(), out var longitudeInput) ||
        longitudeInput < -180 || longitudeInput > 180)
    {
        Console.WriteLine("Invalid longitude. Must be between -180 and 180.");
        Console.WriteLine();
        continue;
    }

    Console.WriteLine("Enter month (YYYY-MM): ");
    var month = Console.ReadLine();

    if (!IsValidMonth(month, out string errorMessage))
    {
        Console.WriteLine($"{errorMessage}");
        Console.WriteLine();
        continue;
    }

    var crimes = await apiService.GetCrimesByLocationAsync(latitudeInput, longitudeInput, month);

    Console.WriteLine("--------------------------");

    Console.WriteLine($"Total crimes returned: {crimes.Count}");
    if (crimes.Count == 0)
    {
        Console.WriteLine("No crimes found for this location and month.");
        Console.WriteLine("--------------------------");
    }
    else
    {
        
        var summary = summaryService.SummaryByCategories(crimes);

        Console.WriteLine("Crime Summary By Category");

        Console.WriteLine("-------------------------");

        foreach (var crime in summary)
        {
            Console.WriteLine($"{crime.Category}: {crime.Count}");
        }

        Console.WriteLine("--------------------------");
    }

   
    Console.WriteLine();
    Console.Write("Would you like to search again? (y/n): ");
    var response = Console.ReadLine()?.Trim().ToLower();
    continueRunning = response == "y" || response == "yes";

    if (continueRunning)
    {
        Console.WriteLine();
        Console.WriteLine("--------------------------");
        Console.WriteLine();
    }
}

Console.WriteLine("Thanks!");

static bool IsValidMonth(string month, out string errorMessage)
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