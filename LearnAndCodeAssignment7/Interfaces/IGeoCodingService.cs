using LearnAndCodeAssignment7.Models;

namespace LearnAndCodeAssignment7.Interfaces
{
    public interface IGeoCodingService
    {
        Task<List<LocationResult>> GetCoordinatesAsync(string location);
    }
}
