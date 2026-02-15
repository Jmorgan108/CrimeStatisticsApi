namespace CrimeStatisticsApi.Helpers
{
    public class DecimalDegreesValidationHelper
    {
        public static bool IsValidLatitude(double latitude)
        {
            return latitude >= -90 && latitude <= 90;
        }

        public static bool IsValidLongitude(double longitude)
        {
            return longitude >= -180 && longitude <= 180;
        }
    }
}
