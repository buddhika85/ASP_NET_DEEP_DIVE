using _6_MinimalAPI_DI_CodeOrganization.Models;

static class EmployeesRepository
{
    private static int id = 0;
    private static List<Employee> employees = new List<Employee>();

    static EmployeesRepository()
    {
        AddEmployee(new Employee("John Doe", "Engineer", 60000));
        AddEmployee(new Employee("Jane Smith", "Manager", 75000));
        AddEmployee(new Employee("Sam Brown", "Technician", 50000));
        AddEmployee(new Employee("John Doe", "Technician", 50000));
    }
    

    public static List<Employee> GetEmployees() => employees;
    public static void AddEmployee(Employee employee)
    {
        employee.Id = ++id;
        employees.Add(employee);
    }
    public static void UpdateEmployee(Employee employee)
    {
        var employeeToUpdate = FindById(employee.Id);
        if (employeeToUpdate != null)
        {
            employeeToUpdate.Name = employee.Name;
            employeeToUpdate.Position = employee.Position;
            employeeToUpdate.Salary = employee.Salary;
        }
    }

    public static Employee? FindById(int id) => employees.SingleOrDefault(x => x.Id == id);
    public static bool IsExists(int id) => FindById(id) != null;
    public static void DeleteEmployee(int id)
    {
        var employee = FindById(id);
        if (employee != null)
            employees.Remove(employee);
    }

    public static void DeleteEmployee(Employee employee)
    {
        if (employee != null)
            employees.Remove(employee);
    }
}
