namespace _12_WebAPI_WithEFCore.Models.Repositories
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void DeleteEmployee(int id);
        Employee? FindById(int id);
        List<Employee> GetEmployees();
        bool IsExists(int id);
        void UpdateEmployee(Employee employee);
    }
}
