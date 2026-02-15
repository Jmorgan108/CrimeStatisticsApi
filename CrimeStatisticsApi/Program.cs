using CrimeStatisticsByLongitudeAndLatitude.Services;

var apiService = new CrimeApiService();
var summaryService = new CrimesSummaryService();

//Input Validation for latitude and longitude
Console.WriteLine("Enter latitude: ");
if (!double.TryParse(Console.ReadLine(), out var latitudeInput) ||
    latitudeInput < -90 || latitudeInput > 90)
{
    Console.WriteLine("Invalid latitude. Must be between -90 and 90.");
    return;
}

Console.WriteLine("Enter longitute: ");
if (!double.TryParse(Console.ReadLine(), out var longitudeInput) ||
    longitudeInput < -180 || longitudeInput > 180)
{
    Console.WriteLine("Invalid longitude. Must be between -180 and 180.");
    return;
}

Console.WriteLine("Enter month (YYYY-MM): ");
var month = Console.ReadLine();

// Validate format
if (string.IsNullOrWhiteSpace(month) || month.Length != 7 || month[4] != '-')
{
    Console.WriteLine("Invalid format. Please use YYYY-MM (e.g., 2024-11)");
    return;
}

// Try to parse the month
var parts = month.Split('-');
if (!int.TryParse(parts[0], out var year) || !int.TryParse(parts[1], out var monthNum))
{
    Console.WriteLine("Invalid month format. Please use YYYY-MM");
    return;
}

if (monthNum < 1 || monthNum > 12)
{
    Console.WriteLine("Month must be between 01 and 12");
    return;
}

// Check if date is in the future
var inputDate = new DateTime(year, monthNum, 1);
if (inputDate > DateTime.Now)
{
    Console.WriteLine("Please enter a date previous to today");
    return;
}

var crimes = await apiService.GetCrimesByLocationAsync(latitudeInput, longitudeInput, month);

Console.WriteLine("--------------------------");

Console.WriteLine($"Total crimes returned: {crimes.Count}");
if (crimes.Count == 0)
{
    Console.WriteLine("No crimes found for this location and month.");
    return;
}

Console.WriteLine("--------------------------");

var summary = summaryService.SummaryByCategories(crimes);

Console.WriteLine("Crime Summary By Category");

Console.WriteLine("-------------------------");

foreach (var crime in summary)
{
    Console.WriteLine($"{crime.Category}: {crime.Count}");
}

