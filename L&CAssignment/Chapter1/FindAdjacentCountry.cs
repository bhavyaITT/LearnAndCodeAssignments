namespace Chapter1;

class FindAdjacentCountry
{
    public static readonly Dictionary<string, string> CountriesWithAdjacentCountries = new Dictionary<string, string>()
    {
        { "US", "United States of America" },
        { "CA", "Canada" },
        { "MX", "Mexico" },
        { "GB", "United Kingdom" },
        { "FR", "France" },
        { "DE", "Germany" },
        { "IN", "India" },
        { "CN", "China" },
        { "JP", "Japan" },
        { "AU", "Australia" },
        { "NZ", "New Zealand" }
    };

    public static void Main(string[] args)
    {
        Console.WriteLine("Please enter a country code: ");
        string? countryCode = Console.ReadLine();
        if (string.IsNullOrEmpty(countryCode))
        {
            Console.WriteLine("Invalid input! Please enter a valid country code.");
            return;
        }
        countryCode = countryCode.Trim().ToUpper();
        string? adjacentCountry = GetAdjacentCountries(countryCode);
        if (adjacentCountry != null)
        {
            Console.WriteLine($"Adjacent countries for {countryCode}: {adjacentCountry}");
        }
        else
        {
            Console.WriteLine($"No adjacent countries found for {countryCode}.");
        }
    }

    public static string? GetAdjacentCountries(string countryCode)
    {
        foreach (var country in CountriesWithAdjacentCountries)
        {
            if (country.Key.Equals(countryCode, StringComparison.OrdinalIgnoreCase))
            {
                return country.Value;
            }
        }
        return null;
    }
}