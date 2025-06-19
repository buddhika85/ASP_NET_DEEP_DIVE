using _12_WebAPI_WithEFCore.Models;
using Microsoft.EntityFrameworkCore;


namespace _12_WebAPI_WithEFCore.Data;

public class CompanyDbContext : DbContext
{
    // getting connection string from Program.cs
    public CompanyDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Department>()               // A department has
                .HasMany(x => x.Employees)              // many employees
                .WithOne(x => x.Department)             // An employee has one department
                .HasForeignKey(x => x.DepartmentId);    // employee table has FK department ID

        // initial seed data
        modelBuilder.Entity<Department>().HasData(
                    new Department { Id = 1, Name = "Sales", Description = "Sales Department" },
                    new Department { Id = 2, Name = "Engineering", Description = "Engineering Department" },
                    new Department { Id = 3, Name = "QA", Description = "Quanlity Assurance" }
            );

        modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, Name = "John Doe", Position = "Engineer", Salary = 60000, DepartmentId = 2 },
                    new Employee { Id = 2, Name = "Jane Smith", Position = "Manager", Salary = 75000, DepartmentId = 1 },
                    new Employee { Id = 3, Name = "Sam Brown", Position = "QA", Salary = 50000, DepartmentId = 3 },
                    new Employee { Id = 4, Name = "John Doe", Position = "Technician", Salary = 50000, DepartmentId = 2 }
           );
    }
}
