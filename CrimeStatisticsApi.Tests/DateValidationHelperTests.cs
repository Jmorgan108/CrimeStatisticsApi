using CrimeStatisticsApi.Helpers;

namespace CrimeStatisticsApi.Tests;

public class DateValidationHelperTests
{
    [Theory]
    [InlineData("2024-11", true)]
    [InlineData("2024-01", true)]
    [InlineData("2045-12", false)] // Future date
    [InlineData("2024-13", false)] // Invalid month
    [InlineData("2024-00", false)] // Invalid month
    [InlineData("2024-1", false)]  // Wrong format
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValidMonth_ReturnsCorrectResult(string month, bool expected)
    {
        // Act
        var result = DateValidationHelper.IsValidMonth(month, out _);

        // Assert
        Assert.Equal(expected, result);
    }
}
