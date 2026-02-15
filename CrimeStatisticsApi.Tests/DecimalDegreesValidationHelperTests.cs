using CrimeStatisticsApi.Helpers;

namespace CrimeStatisticsApi.Tests;

public class DecimalDegreesValidationHelperTests
{
        [Theory]
        [InlineData(-90, true)]
        [InlineData(0, true)]
        [InlineData(90, true)]
        [InlineData(-91, false)]
        [InlineData(91, false)]
        public void IsValidLatitude_ReturnsCorrectResult(double latitude, bool expected)
        {
            // Act
            var result = DecimalDegreesValidationHelper.IsValidLatitude(latitude);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-180, true)]
        [InlineData(0, true)]
        [InlineData(180, true)]
        [InlineData(-181, false)]
        [InlineData(181, false)]
        public void IsValidLongitude_ReturnsCorrectResult(double longitude, bool expected)
        {
            // Act
            var result = DecimalDegreesValidationHelper.IsValidLongitude(longitude);

            // Assert
            Assert.Equal(expected, result);
        }
}
