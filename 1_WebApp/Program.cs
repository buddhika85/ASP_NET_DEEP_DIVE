var builder = WebApplication.CreateBuilder(args);       // creates Kestrel server
var app = builder.Build();                              // create web application

//app.MapGet("/", () => "Hello World!");                  // middleware pipeline starts, executes ONLY when the Kestral receives HTTP request and creates HTTP Context

app.Run(async (HttpContext  context) =>
{
    await context.Response.WriteAsync($"METHOD: {context.Request.Method}\r\n");
    await context.Response.WriteAsync($"URL: {context.Request.Path}\r\n\r\n");
    await context.Response.WriteAsync($"HEADERS: \r\n");
    foreach (var key in context.Request.Headers.Keys)
    {
        await context.Response.WriteAsync($"{key}: \t{context.Request.Headers[key]}\r\n");
    }
});

app.Run();                                              // starts the Kestral server, host the application on it
