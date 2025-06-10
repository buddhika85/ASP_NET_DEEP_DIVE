using Microsoft.AspNetCore.Mvc;
using RoutingExcercise.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Model Binding Demo!");


app.MapGet("/Employees", () =>
{
    return "Get All Employees";
});


// From Route
//app.MapGet("/Employee/{id:int}", ([FromRoute(Name = "id")] int employeeId) =>
//{
//    return $"Get employee with ID {employeeId}";
//});


// From Query
//app.MapGet("/Employee", ([FromQuery(Name = "id")] int employeeId) =>
//{
//    return $"Get employee with ID {employeeId} - Query String";
//});


// From Headers
app.MapGet("/Employee", ([FromHeader] int id) =>
{
    return $"Get employee with ID {id}";
});

app.MapPost("/Employee", ([FromBody] Employee emp) =>
{
    // logic ...
    return $"Employee added {emp.ToString()}";

}).WithParameterValidation();

app.Run();

public class Employee
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Position { get; set; }

    [Required]
    [Range(1000, 100000)]
    [Employee_EnsureSalary] // custom attribute - if manager salary >100,000
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



