using LearnAndCodeAssignment7.Interfaces;
using LearnAndCodeAssignment7.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using LearnAndCodeAssignment7.DTO;
using LearnAndCodeAssignment7.Adapters;

namespace LearnAndCodeAssignment7.Providers
{
    public class LocationIQProvider : IGeoCodingService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public LocationIQProvider(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<LocationResult>> GetCoordinatesAsync(string location)
        {
            var apiKey = _config["LocationIQ:ApiKey"];

            var url =
                $"https://us1.locationiq.com/v1/search?key={apiKey}&q={location}&format=json";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("API request failed");

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<LocationIQResponse>>(json);

            return LocationIQResponseAdapter.Convert(data);
        }
    }
}


