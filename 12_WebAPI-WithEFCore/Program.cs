using _12_WebAPI_WithEFCore.Data;
using _12_WebAPI_WithEFCore.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt",
        rollingInterval: RollingInterval.Day)       // daily a new file will be created
    .CreateLogger();
builder.Services.AddSerilog();

builder.Services.AddDbContext<CompanyDbContext>(options =>
{
    // use SQL Server
    // use connection string with name 'Default' coming from AppSettings.json
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Add services to the container.

builder.Services.AddControllers();

//bool showNewData = bool.Parse( builder.Configuration["ShowNewData"] );


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();


//bool showNewData = bool.Parse(app.Configuration["ShowNewData"]);

app.Run();
