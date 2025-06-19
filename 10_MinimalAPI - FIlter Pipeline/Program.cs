using _10_MinimalAPI___FIlter_Pipeline.Endpoints;
using _10_MinimalAPI___FIlter_Pipeline.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();

builder.Services.AddOpenApi();      // Add OPEN API to services collection

var app = builder.Build();

app.MapOpenApi();                   // Apply Open API middleware to pipeline

app.UseSwaggerUI(options =>
{
    // point swagger to OPEN API generated Json file
    options.SwaggerEndpoint("openAPI/v1.json", "Employee Management Endpoints");
});

app.MapGet("/", () => "Filter Pipeline Demo");

app.AddEmployeeEndPoints();

app.Run();
