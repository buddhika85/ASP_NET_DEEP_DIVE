using Microsoft.AspNetCore.Mvc;
using ModelBindingExcercise.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome model binding excercise!");

app.MapPost("/Register", ([FromBody] RegistrationDto dto) => {
    return $"Registation successful\n\r{dto}";
}).WithParameterValidation();

app.MapGet("/Register", (RegistrationDto dto) =>
{
    return $"Registation successful\n\r{dto}";
}).WithParameterValidation();



app.Run();
