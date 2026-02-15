using CrimeStatisticsApi.Models;
using CrimeStatisticsByLongitudeAndLatitude.Models;

namespace CrimeStatisticsByLongitudeAndLatitude.Services
{
    public class CrimesSummaryService
    {
        public List<CrimeSummary> SummaryByCategories(List<Crime> crimes)
        {
            return crimes.GroupBy(c => c.Category)
                         .Select(g => new CrimeSummary
                         {
                             Category = FormatCategoryName(g.Key),
                             Count = g.Count()
                         })
                         .OrderByDescending(c => c.Count)
                         .ToList();
        }

        private string FormatCategoryName(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return "Unknown";

            return string.Join(" ", category.Split('-')
                .Select(word => char.ToUpper(word[0]) + word.Substring(1)));
        }
    }
}
