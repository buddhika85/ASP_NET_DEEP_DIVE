
using _12_WebAPI_WithEFCore.Data;

namespace _12_WebAPI_WithEFCore.Models.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly CompanyDbContext dbContext;

    public EmployeeRepository(CompanyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void AddEmployee(Employee employee)
    {
        dbContext.Employees.Add(employee);
        dbContext.SaveChanges();
    }

    public void DeleteEmployee(int id)
    {
        var employee = FindById(id);
        if (employee != null)
        {
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
        }
    }

    public Employee? FindById(int id)
    {
        // find by PK
        return dbContext.Employees.Find(id);
    }

    public List<Employee> GetEmployees()
    {
        return dbContext.Employees.ToList();
    }

    public bool IsExists(int id)
    {
        return FindById(id) != null;
    }

    public void UpdateEmployee(Employee employee)
    {
        var employeeToUpdate = FindById(employee.Id);
        if (employeeToUpdate != null)
        {
            employeeToUpdate.Name = employee.Name;
            employeeToUpdate.Position = employee.Position;
            employeeToUpdate.DepartmentId = employee.DepartmentId;
            dbContext.SaveChanges();
        }
    }
}
