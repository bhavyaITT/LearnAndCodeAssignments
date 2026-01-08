public class CustomerSearchService
{
    private readonly DbContext db;

    public CustomerSearchService(DbContext _context)
    {
        db = _context;
    }

    public List<Customer> SearchByCountry(string country)
    {
        return Search(c => c.Country.Contains(country));
    }

    public List<Customer> SearchByCompanyName(string companyName)
    {
        return Search(c => c.CompanyName.Contains(companyName));
    }

    public List<Customer> SearchByContact(string contactName)
    {
        return Search(c => c.ContactName.Contains(contactName));
    }

    private List<Customer> Search(Func<Customer, bool> predicate)
    {
        return db.customers
                 .Where(predicate)
                 .OrderBy(c => c.CustomerID)
                 .ToList();
    }
}

public class CustomerCsvExporter
{
    public string ExportToCSV(List<Customer> customers)
    {
        StringBuilder csvBuilder = new StringBuilder();

        foreach (var customer in customers)
        {
            csvBuilder.AppendFormat(
                "{0},{1},{2},{3}",
                customer.CustomerID,
                customer.CompanyName,
                customer.ContactName,
                customer.Country
            );
            csvBuilder.AppendLine();
        }

        return csvBuilder.ToString();
    }
}