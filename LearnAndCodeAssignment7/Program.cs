using LearnAndCodeAssignment7.Interfaces;
using LearnAndCodeAssignment7.Providers;
using LearnAndCodeAssignment7.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeoLocationBoundaryApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddSingleton<IConfiguration>(config);
            services.AddHttpClient();
            services.AddTransient<IGeoCodingService, LocationIQProvider>();
            services.AddTransient<LocationService>();
            var provider = services.BuildServiceProvider();
            var locationService = provider.GetRequiredService<LocationService>();

            Console.Write("Enter location: ");
            string? location = Console.ReadLine();

            try
            {
                var results = await locationService.GetLocationAsync(location);

                if (results.Count == 0)
                {
                    Console.WriteLine("No results found.");
                    return;
                }

                Console.WriteLine("\nResults:\n");

                foreach (var result in results)
                {
                    Console.WriteLine("Address   : " + result.Address);
                    Console.WriteLine("Latitude  : " + result.Latitude);
                    Console.WriteLine("Longitude : " + result.Longitude);
                    Console.WriteLine();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: " + exception.Message);
            }
        }
    }
}