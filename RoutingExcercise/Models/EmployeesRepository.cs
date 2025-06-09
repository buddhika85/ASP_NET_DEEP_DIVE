using RoutingExcercise.Models;

static class EmployeesRepository
{
    private static List<Employee> employees = new List<Employee>
    {
        new Employee(1, "John Doe", "Engineer", 60000),
        new Employee(2, "Jane Smith", "Manager", 75000),
        new Employee(3, "Sam Brown", "Technician", 50000),
        new Employee(3, "John Doe", "Technician", 50000)
    };

    public static List<Employee> GetEmployees() => employees;
    public static void AddEmployee(Employee employee) => employees.Add(employee);
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

    public static void DeleteEmployee(int id)
    {
        var employee = FindById(id);
        if (employee != null)
            employees.Remove(employee);
    }
}
