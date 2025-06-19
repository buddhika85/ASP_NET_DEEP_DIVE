using _12_WebAPI_WithEFCore.Data;
using _12_WebAPI_WithEFCore.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace _12_WebAPI_WithEFCore.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Position { get; set; }

    [Employee_EnsureSalary]
    [Required]
    [Range(1000, 100000)]
    public double Salary { get; set; }


    // Foreign Key
    public int? DepartmentId { get; set; }


    // nagivation property
    public virtual Department? Department { get; set; }

}