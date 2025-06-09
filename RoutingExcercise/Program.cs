using RoutingExcercise.MiddlewareComponents;
using RoutingExcercise.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ExceptionMiddleware>();   // register middleware

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();           // use middleware

app.MapGet("/", () => "Employee CRUD - Root");

app.MapGet("/Employee", async (HttpContext context) =>
{
    var employeesStr = EmployeesRepository.GetEmployees().Select(x => $"\n\r\t{x.ToString()}").Aggregate((total, curr) => $"{total}{curr}");
    await context.Response.WriteAsync("Get All Employees");
    await context.Response.WriteAsync($"{employeesStr}");
});


app.MapGet("/Employee/{id:int}", async(HttpContext context) =>
{
    var id = Convert.ToInt32(context.Request.RouteValues["id"]);
    var employee = EmployeesRepository.FindById(id);
    if (employee == null)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync($"Employee with ID  {id} unavailable");
        return;
    }
    await context.Response.WriteAsync($"{employee}");
});

app.MapGet("/Employee/{name}", async (HttpContext context) =>
{
    var name = context.Request.RouteValues["name"]?.ToString();
    if (name == null)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync($"Employee with name {name} unavailable");
        return;
    }

    var employees = EmployeesRepository.GetEmployees().Where(x => x.Name.ToLower() == name.ToLower());
    if (!employees.Any())
    {        
        await context.Response.WriteAsync($"Employee with name {name} unavailable");
        return;
    }

    var employeesStr = employees.Select(x => $"\n\r\t{x.ToString()}").Aggregate((total, current) => $"{total}{current}");
    await context.Response.WriteAsync($"All Employees with name {name}");
    await context.Response.WriteAsync($"{employeesStr}");
});

app.MapPost("/Employee", async (HttpContext context) =>
{
    using var streamReader = new StreamReader(context.Request.Body);
    var jsonBody = await streamReader.ReadToEndAsync();
    var employee = JsonSerializer.Deserialize<Employee>(jsonBody);
    if (employee == null) 
    {
        context.Response.StatusCode = 400;      // bad request
        await context.Response.WriteAsync("Error - Valid employee needed");
        return;
    }

    context.Response.StatusCode = 201;          // created
    EmployeesRepository.AddEmployee(employee);
    await context.Response.WriteAsync("Employee Added");
});

app.MapPut("/Employee", async (HttpContext context) =>
{
    using var streamReader = new StreamReader(context.Request.Body);
    var jsonBody = await streamReader.ReadToEndAsync();
    var employee = JsonSerializer.Deserialize<Employee>(jsonBody);
    if (employee == null)
    {
        context.Response.StatusCode = 400;      // bad request
        await context.Response.WriteAsync("Error - Valid employee needed");
        return;
    }

    if (EmployeesRepository.FindById(employee.Id) == null)
    {
        context.Response.StatusCode = 404;      // bad request
        await context.Response.WriteAsync($"Error - Employee with such ID {employee.Id} unavailable to update");
        return;
    }
    EmployeesRepository.UpdateEmployee(employee);
    await context.Response.WriteAsync("Employee Updated");
});

app.MapDelete("/Employee/{id:int}", async (HttpContext context) =>
{
    if (context.Request.Headers["Authorization"] != "Buddhika")
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Error - You do not have DELETION rights");
        return;
    }
    var id = Convert.ToInt32(context.Request.RouteValues["id"]);    
    EmployeesRepository.DeleteEmployee(id);
    await context.Response.WriteAsync("Employee Deleted");
});

app.Run();
