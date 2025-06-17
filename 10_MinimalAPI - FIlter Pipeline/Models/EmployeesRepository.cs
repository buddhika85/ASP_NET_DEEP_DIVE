namespace _10_MinimalAPI___FIlter_Pipeline.Models;

public class EmployeesRepository : IEmployeesRepository
{
    private static int id = 0;
    private List<Employee> employees = new List<Employee>();

    public EmployeesRepository()
    {
        AddEmployee(new Employee("John Doe", "Engineer", 60000));
        AddEmployee(new Employee("Jane Smith", "Manager", 75000));
        AddEmployee(new Employee("Sam Brown", "Technician", 50000));
        AddEmployee(new Employee("John Doe", "Technician", 50000));
    }


    public List<Employee> GetEmployees() => employees;
    public void AddEmployee(Employee employee)
    {
        employee.Id = ++id;
        employees.Add(employee);
    }
    public void UpdateEmployee(Employee employee)
    {
        var employeeToUpdate = FindById(employee.Id);
        if (employeeToUpdate != null)
        {
            employeeToUpdate.Name = employee.Name;
            employeeToUpdate.Position = employee.Position;
            employeeToUpdate.Salary = employee.Salary;
        }
    }

    public Employee? FindById(int id) => employees.SingleOrDefault(x => x.Id == id);
    public bool IsExists(int id) => FindById(id) != null;
    public void DeleteEmployee(int id)
    {
        var employee = FindById(id);
        if (employee != null)
            employees.Remove(employee);
    }

    public void DeleteEmployee(Employee employee)
    {
        if (employee != null)
            employees.Remove(employee);
    }
}
