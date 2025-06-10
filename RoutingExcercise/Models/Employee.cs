using System.ComponentModel.DataAnnotations;

namespace RoutingExcercise.Models;


public class Employee
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Position { get; set; }

    [Required]
    [Range(1000, 100000)]
    public double Salary { get; set; }

    public Employee(int id, string name, string position, double salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"{Id} {Name} {Position} {Salary}";
    }
}