using CrimeStatisticsByLongitudeAndLatitude.Services;

var apiService = new CrimeApiService();
var summaryService = new CrimesSummaryService();

Console.WriteLine("Enter latitude: ");
var latitudeInput = double.Parse(Console.ReadLine());

Console.WriteLine("Enter longitute: ");
var longitudeInput = double.Parse(Console.ReadLine());

Console.WriteLine("Enter month (YYYY-MM): ");
var month = Console.ReadLine();

var crimes = await apiService.GetCrimesByLocationAsync(latitudeInput, longitudeInput, month);

Console.WriteLine("--------------------------");

Console.WriteLine($"Total crimes returned: {crimes.Count}");

Console.WriteLine("--------------------------");

var summary = summaryService.SummaryByCategories(crimes);

Console.WriteLine("Crime Summary By Category");

Console.WriteLine("-------------------------");

foreach (var crime in summary)
{
    Console.WriteLine($"{crime.Category}: {crime.Count}");
}

