using _10_MinimalAPI___FIlter_Pipeline.Endpoints;
using _10_MinimalAPI___FIlter_Pipeline.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();

var app = builder.Build();

app.MapGet("/", () => "Filter Pipeline Demo");

app.AddEmployeeEndPoints();

app.Run();
