var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();      // use controller technique, not minimal API

var app = builder.Build();

app.UseRouting();           // Routing middleware for controllers

app.MapControllers();       // use endpoint handlers in controllers

app.Run();
