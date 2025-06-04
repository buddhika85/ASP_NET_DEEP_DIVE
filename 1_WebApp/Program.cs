var builder = WebApplication.CreateBuilder(args);       // creates Kestrel server
var app = builder.Build();                              // create web application

app.MapGet("/", () => "Hello World!");                  // middleware pipeline starts, executes ONLY when the Kestral receives HTTP request and creates HTTP Context

app.Run();                                              // starts the Kestral server, host the application on it
