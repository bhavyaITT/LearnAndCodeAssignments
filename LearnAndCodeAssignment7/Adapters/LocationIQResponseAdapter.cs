using LearnAndCodeAssignment7.DTO;
using LearnAndCodeAssignment7.Models;

namespace LearnAndCodeAssignment7.Adapters
{
    public static class LocationIQResponseAdapter
    {
        public static List<LocationResult> Convert(List<LocationIQResponse> responses)
        {
            return responses.Select(r => new LocationResult
            {
                Address = r.DisplayName,
                Latitude = double.Parse(r.Lat),
                Longitude = double.Parse(r.Lon)
            }).ToList();
        }
    }
}
