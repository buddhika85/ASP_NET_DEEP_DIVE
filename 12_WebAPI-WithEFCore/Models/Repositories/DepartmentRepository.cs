using _12_WebAPI_WithEFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace _12_WebAPI_WithEFCore.Models.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly CompanyDbContext dbContext;

    public DepartmentRepository(CompanyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void AddDepartment(Department department)
    {
        dbContext.Add(department);
        dbContext.SaveChanges();
    }

    public void DeleteDepartment(int id)
    {
        var toDelete = FindById(id);
        if (toDelete != null)
        {
            dbContext.Departments.Remove(toDelete);
            dbContext.SaveChanges();
        }
    }

    public Department? FindById(int id)
    {
        return dbContext.Departments.Find(id);
    }

    public List<Department> GetDepartments()
    {
        return dbContext.Departments.AsNoTracking().ToList();
    }

    public bool IsExists(int id)
    {
        return FindById(id) != null;
    }

    public void UpdateDepartment(Department department)
    {
        var toUpdate = FindById(department.Id);
        if (toUpdate != null)
        {
            toUpdate.Description = department.Description;
            toUpdate.Name = department.Name;
            dbContext.SaveChanges();
        }
    }
}
