var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endPoints =>
{
    endPoints.MapGet("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Get Employees");
    });

    endPoints.MapPost("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Post Employee");
    });

    endPoints.MapPut("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Put Employee");
    });

    endPoints.MapDelete("/employees/{id}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Delete Employee = {context.Request.RouteValues["id"]}");
    });
});



app.Run();
