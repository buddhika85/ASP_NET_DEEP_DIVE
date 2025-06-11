using _5_MinimalAPI.Results;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();       // problem details standard JSON

var app = builder.Build();

app.UseExceptionHandler();                  // FIRST - handle exceptions display standard JSON
app.UseStatusCodePages();                   // display status codes as standard JSON

app.MapGet("/", () => new HtmlResult("<h2>Minimal API CRUD</h2>"));

app.MapGet("/Employee", () =>
{
    return TypedResults.Ok(EmployeesRepository.GetEmployees());
});

app.MapGet("/Employee/{id:int}", ([FromRoute] int id) =>
{
    var employee = EmployeesRepository.FindById(id);
    return employee is null 
        ? Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new [] { $"Employee with ID {id} unavailable." } }
            }) 
        : TypedResults.Ok(employee);
});

app.MapPost("/Employee", ([FromBody] Employee employee) => { 
    if (employee == null)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            { "employee", new [] { "Employee information not provided" } }
        });
    }
    EmployeesRepository.AddEmployee(employee);

    // URL get by ID, Object created
    return TypedResults.Created(uri:$"/employee/{employee.Id}", value:employee);

}).WithParameterValidation();

app.MapPut("/Employee/{id:int}", ([FromRoute] int id, [FromBody] Employee employee) => {
    if (employee == null)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            { "employee", new [] { "Employee information not provided" } }
        });
    }
    if (employee.Id != id)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            { "employee", new [] { "Employee ids provided do not match." } }
        });
    }
    if (!EmployeesRepository.IsExists(employee.Id))
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            { "id", new [] { $"Employee with ID {employee.Id} does not exist." } }
        });
    }
    EmployeesRepository.UpdateEmployee(employee);

    // PUT should return No Content if successful
    return TypedResults.NoContent();
}).WithParameterValidation(); ;

app.MapDelete("/Employee/{id:int}", ([FromRoute] int id) => {

    var employeeToDelete = EmployeesRepository.FindById(id);
    if (employeeToDelete == null)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            { "id", new [] { $"Employee with ID {id} does not exist." } }
        });
    }
    EmployeesRepository.DeleteEmployee(employeeToDelete);
    return TypedResults.Ok(employeeToDelete);
});

app.Run();
