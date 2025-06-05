using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);       // creates Kestrel server
var app = builder.Build();                              // create web application

//app.MapGet("/", () => "Hello World!");                  // middleware pipeline starts, executes ONLY when the Kestral receives HTTP request and creates HTTP Context

//app.Run(async (HttpContext  context) =>
//{
//    await context.Response.WriteAsync($"METHOD: {context.Request.Method}\r\n");
//    await context.Response.WriteAsync($"URL: {context.Request.Path}\r\n\r\n");
//    await context.Response.WriteAsync($"HEADERS: \r\n");
//    foreach (var key in context.Request.Headers.Keys)
//    {
//        await context.Response.WriteAsync($"{key}: \t{context.Request.Headers[key]}\r\n");
//    }
//});

app.Run(async (HttpContext context) =>
{  
    if (context.Request.Path.StartsWithSegments("/"))
    {
        await context.Response.WriteAsync($"{context.Request.Path}/{context.Request.Method}");
    }
    else if (context.Request.Path.StartsWithSegments("/employees"))
    {
        switch(context.Request.Method)
        {
            case "GET":
            {
                await context.Response.WriteAsync("GET /employees\r\n");
                foreach (var emp in EmployeesRepository.GetEmployees())
                {
                    await context.Response.WriteAsync($"{emp.Name}: {emp.Position}\r\n");
                }
                break;
            }
            case "POST":
            {
                using var reader = new StreamReader(context.Request.Body);
                string body = await reader.ReadToEndAsync();                            // json string
                Employee? employee = JsonSerializer.Deserialize<Employee>(body);
                if (employee != null)
                {
                    EmployeesRepository.AddEmployee(employee);
                    await context.Response.WriteAsync($"\r\n{employee.Name}: {employee.Position} Added");
                }
                else
                {
                    await context.Response.WriteAsync($"\r\nError - Unable to Deserialize employee from HTTP Body - Create");
                }
                break;
            }
            case "PUT":
            {
                using var reader = new StreamReader(context.Request.Body);
                string body = await reader.ReadToEndAsync();                            // json string
                Employee? employee = JsonSerializer.Deserialize<Employee>(body);
                if (employee != null)
                {
                    if (EmployeesRepository.FindById(employee.Id) == null)
                    {
                        await context.Response.WriteAsync($"\r\nError - {employee.Id}: ID valued employee unavailable");
                    }
                    else
                    {
                        EmployeesRepository.UpdateEmployee(employee);
                        await context.Response.WriteAsync($"\r\n{employee.Name}: {employee.Position} Updated");
                    }
                }
                else
                {
                    await context.Response.WriteAsync($"\r\nError - Unable to Deserialize employee from HTTP Body - Update");
                }
                break;
            }                   
            case "DELETE":
            {
                if (context.Request.Path.StartsWithSegments("/employees"))
                {
                    if (context.Request.Headers["Authorization"] != "Buddhika")
                    {
                        await context.Response.WriteAsync($"\r\nError - You are unauthorised to DELETE");
                    }
                    else if (context.Request.Query.ContainsKey("id") &&
                        int.TryParse(context.Request.Query["id"], out int id))
                    {
                        var employee = EmployeesRepository.FindById(id);
                        if (employee == null)
                        {
                            await context.Response.WriteAsync($"\r\nError - {id}: ID valued employee unavailable");
                        }
                        else
                        {
                            EmployeesRepository.DeleteEmployee(id);
                            await context.Response.WriteAsync($"\r\n{employee.Name}: {employee.Position} Deleted");
                        }
                    }
                    else
                    {
                        await context.Response.WriteAsync($"\r\nError - Unable to extract ID value from query string - DELETE");
                    }
                }                        
                break; 
            }
            default:
            {
                await context.Response.WriteAsync("Unknown HTTP REQUEST METHOD");
                break;
            }
        }            
    }
    else
    {
        await context.Response.WriteAsync($"Location: {context.Request.Path}");
    }  

});

app.Run();                                              // starts the Kestral server, host the application on it


static class EmployeesRepository
{
    private static List<Employee> employees = new List<Employee>
    {
        new Employee(1, "John Doe", "Engineer", 60000),
        new Employee(2, "Jane Smith", "Manager", 75000),
        new Employee(3, "Sam Brown", "Technician", 50000)
    };

    public static List<Employee> GetEmployees() => employees;
    public static void AddEmployee(Employee employee) => employees.Add(employee);
    public static void UpdateEmployee(Employee employee)
    {
        var employeeToUpdate = FindById(employee.Id);
        if (employeeToUpdate != null)
        {
            employeeToUpdate.Name = employee.Name;
            employeeToUpdate.Position = employee.Position;
            employeeToUpdate.Salary = employee.Salary;
        }
    }

    public static Employee? FindById(int id) => employees.SingleOrDefault(x => x.Id == id);

    public static void DeleteEmployee(int id)
    {
        var employee = FindById(id);
        if (employee != null)
            employees.Remove(employee);
    }
}

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public double Salary { get; set; }

    public Employee(int id, string name, string position, double salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }
}
