using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Minimal API CRUD");

app.MapGet("/Employee", () =>
{
    return TypedResults.Ok(EmployeesRepository.GetEmployees());
});

app.MapGet("/Employee/{id:int}", ([FromRoute] int id) =>
{
    var employee = EmployeesRepository.FindById(id);
    return employee is null ?
        Results.NotFound($"Employee with ID {id} unavailable.") :
        Results.Ok(employee);
});

app.MapPost("/Employee", ([FromBody] Employee employee) => { 
    if (employee == null || employee.Id <= 0)
    {
        return Results.BadRequest("Employee is not provided or invalid.");
    }
    EmployeesRepository.AddEmployee(employee);
    return Results.Created();
}).WithParameterValidation();

app.MapPut("/Employee", ([FromBody] Employee employee) => {
    if (employee == null || employee.Id <= 0)
    {
        return Results.BadRequest("Employee is not provided or invalid.");
    }
    if (!EmployeesRepository.IsExists(employee.Id))
    {
        return Results.BadRequest($"Employee with ID {employee.Id} does not exist.");
    }
    EmployeesRepository.UpdateEmployee(employee);
    return Results.Ok("Employee updated");
}).WithParameterValidation(); ;

app.MapDelete("/Employee/{id:int}", ([FromRoute] int id) => {
   
    if (!EmployeesRepository.IsExists(id))
    {
        return Results.BadRequest($"Employee with ID {id} does not exist.");
    }
    EmployeesRepository.DeleteEmployee(id);
    return Results.Ok("Employee Deleted");
});

app.Run();
