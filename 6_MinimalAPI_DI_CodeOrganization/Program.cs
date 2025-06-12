using _6_MinimalAPI_DI_CodeOrganization.Endpoints;
using _6_MinimalAPI_DI_CodeOrganization.Results;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();       // problem details standard JSON

builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();

var app = builder.Build();

app.UseExceptionHandler();                  // FIRST - handle exceptions display standard JSON
app.UseStatusCodePages();                   // display status codes as standard JSON

app.MapGet("/", () => new HtmlResult("<h2>Minimal API CRUD</h2>"));


// all employee endpoints added
app.MapEmployeeEndpoints();


app.Run();
