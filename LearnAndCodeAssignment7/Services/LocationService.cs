using LearnAndCodeAssignment7.Interfaces;
using LearnAndCodeAssignment7.Models;

namespace LearnAndCodeAssignment7.Services
{
    public class LocationService
    {
        private readonly IGeoCodingService _geocodingService;

        public LocationService(IGeoCodingService geocodingService)
        {
            _geocodingService = geocodingService;
        }

        public async Task<List<LocationResult>> GetLocationAsync(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentException("Location cannot be empty.");

            return await _geocodingService.GetCoordinatesAsync(location);
        }
    }
}
