using CrimeStatisticsApi.Helpers;
using CrimeStatisticsByLongitudeAndLatitude.Services;

var apiService = new CrimeApiService();
var summaryService = new CrimesSummaryService();

//Added a loop so the application continures to run when the user enters invalid input, allowing them to correct it without restarting the application.
bool continueRunning = true;
while (continueRunning)
{
    Console.WriteLine("Enter latitude: ");
    if (!double.TryParse(Console.ReadLine(), out var latitudeInput) ||
        !DecimalDegreesValidationHelper.IsValidLatitude(latitudeInput))
    {
        Console.WriteLine("Invalid latitude. Must be between -90 and 90.");
        Console.WriteLine();
        continue;
    }

    Console.WriteLine("Enter longitute: ");
    if (!double.TryParse(Console.ReadLine(), out var longitudeInput) ||
        !DecimalDegreesValidationHelper.IsValidLatitude(latitudeInput))
    {
        Console.WriteLine("Invalid longitude. Must be between -180 and 180.");
        Console.WriteLine();
        continue;
    }

    Console.WriteLine("Enter month (YYYY-MM): ");
    var month = Console.ReadLine();

    if (!DateValidationHelper.IsValidMonth(month, out string errorMessage))
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

