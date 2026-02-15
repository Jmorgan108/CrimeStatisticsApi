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
                             Category = g.Key,
                             Count = g.Count()
                         })
                         .OrderByDescending(c => c.Count)
                         .ToList();
        }

    }
}
