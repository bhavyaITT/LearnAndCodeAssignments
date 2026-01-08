public class Employee
{
    public int Id { get; }
    public string Name { get; }
    public string Department { get; }
    public bool IsWorking { get; private set; }

    public Employee(int id, string name, string department)
    {
        Id = id;
        Name = name;
        Department = department;
        IsWorking = true;
    }
}

public class EmployeeRepository
{
    public void Save(Employee employee)
    {
        // Save employee to database
    }
}

class EmployeeReportService
{
    public void printXmlReport(Employee employee){}

    public void printCsvReport(Employee employee){}
};

public class EmployeeService
{
    public void Terminate(Employee employee){}

    public bool IsWorking(Employee employee){}
}