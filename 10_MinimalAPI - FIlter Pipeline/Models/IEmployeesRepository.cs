namespace _10_MinimalAPI___FIlter_Pipeline.Models;
public interface IEmployeesRepository
{
    void AddEmployee(Employee employee);
    void DeleteEmployee(Employee employee);
    void DeleteEmployee(int id);
    Employee? FindById(int id);
    List<Employee> GetEmployees();
    bool IsExists(int id);
    void UpdateEmployee(Employee employee);
}