using _10_MinimalAPI___FIlter_Pipeline.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace _10_MinimalAPI___FIlter_Pipeline.Models;


public class Employee
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Position { get; set; }

    [Employee_EnsureSalary]
    [Required]
    [Range(1000, 100000)]
    public double Salary { get; set; }

    public Employee(string name, string position, double salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"{Id} {Name} {Position} {Salary}";
    }
}