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
        string? countryCode = TakeUserInput();
        if (countryCode == null)
        {
            return;
        }
        string? adjacentCountry = GetAdjacentCountries(countryCode);
        if(adjacentCountry == null)
        {
            Console.WriteLine($"No adjacent countries found for {countryCode}.");
        }
        else
        {
            PrintAdjacentCountries(countryCode, adjacentCountry);
        }
    }

    public static string? TakeUserInput()
    {
        Console.WriteLine("Please enter a country code: ");
        string? countryCode = Console.ReadLine();
        if (string.IsNullOrEmpty(countryCode))
        {
            Console.WriteLine("Invalid input! Please enter a valid country code.");
            return null;
        }
        countryCode = countryCode.Trim().ToUpper();
        return countryCode;
    }

    public static string? GetAdjacentCountries(string countryCode)
    {
        return CountriesWithAdjacentCountries.TryGetValue(countryCode, out var adjacentCountry) ? adjacentCountry : null;
    }

    public static void PrintAdjacentCountries(string countryCode, string adjacentCountries)
    {
        Console.WriteLine($"Adjacent countries for {countryCode}: {adjacentCountries}");
    }
}