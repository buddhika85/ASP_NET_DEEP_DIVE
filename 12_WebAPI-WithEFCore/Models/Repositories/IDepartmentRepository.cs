using _12_WebAPI_WithEFCore.Data;

namespace _12_WebAPI_WithEFCore.Models.Repositories
{
    public interface IDepartmentRepository
    {
        void AddDepartment(Department department);
        void DeleteDepartment(int id);
        Department? FindById(int id);
        List<Department> GetDepartments();
        bool IsExists(int id);
        void UpdateDepartment(Department department);
    }
}
