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
    if (context.Request.Method == "GET")
    {
        if (context.Request.Path.StartsWithSegments("/"))
        {
            await context.Response.WriteAsync("GET /");
        }
        else if (context.Request.Path.StartsWithSegments("/employees"))
        {
            await context.Response.WriteAsync("GET /employees\r\n");
            foreach (var emp in EmployeesRepository.GetEmployees())
            {
                await context.Response.WriteAsync($"{emp.Name}: {emp.Position}\r\n");
            }
        }
    }
    else if (context.Request.Method == "POST")
    {
        if (context.Request.Path.StartsWithSegments("/employees"))
        {
            using var reader = new StreamReader(context.Request.Body);
            string body = await reader.ReadToEndAsync();                            // json
            Employee? employee = JsonSerializer.Deserialize<Employee>(body);
            if (employee != null) 
            { 
                EmployeesRepository.AddEmployee(employee);
                await context.Response.WriteAsync($"\r\n{employee.Name}: {employee.Position} Added");
            }
            else
            {
                await context.Response.WriteAsync($"\r\nError - Unable to Deserialize employee from HTTP Body");
            }
        }
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
