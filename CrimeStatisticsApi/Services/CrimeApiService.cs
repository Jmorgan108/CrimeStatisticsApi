using CrimeStatisticsByLongitudeAndLatitude.Models;
using System.Text.Json;

namespace CrimeStatisticsByLongitudeAndLatitude.Services
{
    public class CrimeApiService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<Crime>> GetCrimesByLocationAsync(double latitude, double longitude, string month)
        {
            string apiUrl = $"https://data.police.uk/api/crimes-street/all-crime?lat={latitude}&lng={longitude}&date={month}";

            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            
            var crimes = JsonSerializer.Deserialize<List<Crime>>(content);

            return crimes ?? new List<Crime>();
        }
    }
}
