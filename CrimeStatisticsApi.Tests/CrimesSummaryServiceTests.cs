using CrimeStatisticsByLongitudeAndLatitude.Models;
using CrimeStatisticsByLongitudeAndLatitude.Services;

namespace CrimeStatisticsApi.Tests
{
    public class CrimesSummaryServiceTests
    {

        private readonly CrimesSummaryService _serviceTest;

        public CrimesSummaryServiceTests()
        {
            _serviceTest = new CrimesSummaryService();
        }

        [Fact]
        public void SummaryByCategory_GroupsCorrectly()
        {
            //arrange
            var crimes = new List<Crime>
           {
               new Crime { Category = "theft" },
               new Crime { Category = "assault" },
               new Crime { Category = "assault" },
               new Crime { Category = "violent-crime" },
           };

            //act
            var result = _serviceTest.SummaryByCategories(crimes);

            //assert
            Assert.Equal(3, result.Count);
            Assert.Contains(result, r => r.Category == "theft" && r.Count == 1);
            Assert.Contains(result, r => r.Category == "assault" && r.Count == 2);
        }

        [Fact]
        public void SummaryByCategory_OrdersDescending()
        {
            //arrange
            var crimes = new List<Crime>
           {
               new Crime { Category = "theft" },
               new Crime { Category = "assault" },
               new Crime { Category = "assault" },
               new Crime { Category = "violent-crime" },
           };

            //act
            var result = _serviceTest.SummaryByCategories(crimes);

            //assert
            Assert.Equal("assault", result[0].Category);
            Assert.Equal(2, result[0].Count);
            Assert.Equal("theft", result[1].Category);
            Assert.Equal(1, result[1].Count);
        }

        [Fact]
        public void SummaryByCategory_HandlesEmptyList()
        {
            //arrange
            var crimes = new List<Crime>();

            //act
            var result = _serviceTest.SummaryByCategories(crimes);

            //assert
            Assert.Empty(result);
        }

        [Fact]
        public void SummaryByCategory_HandlesSingleCategory()
        {
            //arrange
            var crimes = new List<Crime>
           {
               new Crime { Category = "theft" },
               new Crime { Category = "theft" },
               new Crime { Category = "theft" },
           };
            //act
            var result = _serviceTest.SummaryByCategories(crimes);
            //assert
            Assert.Single(result);
            Assert.Equal("theft", result[0].Category);
            Assert.Equal(3, result[0].Count);
        }

        [Fact]
        public void SummaryByCategory_HandlesNullCategories()
        {
            //arrange
            var crimes = new List<Crime>
           {
               new Crime { Category = null },
               new Crime { Category = "theft" },
               new Crime { Category = null },
           };
            //act
            var result = _serviceTest.SummaryByCategories(crimes);
            //assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Category == null && r.Count == 2);
            Assert.Contains(result, r => r.Category == "theft" && r.Count == 1);
        }

    }
}
